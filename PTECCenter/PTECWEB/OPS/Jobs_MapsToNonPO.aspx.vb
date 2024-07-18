Imports System.Drawing
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Jobs_MapsToNonPO

    Inherits System.Web.UI.Page
    'Dim objStatus As String
    'Dim jobno As String
    'Dim jobdetailid As Integer
    Public menutable As DataTable
    Public itemtable As DataTable '= createdetailtable()
    'Public maintable As DataTable
    'Public stepsuppilertable As DataTable
    'Public assessmenttable As DataTable
    'Public ratetable As DataTable
    'Public AttachTable As DataTable '= createtable()
    'Public CommentTable As DataTable '= createtable()

    'Public followuptable As DataTable = CreateFollowup()
    Public cntdt As Integer
    Dim usercode, username As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objjob As New jobs
        Dim objbranch As New Branch
        Dim objsupplier As New Supplier
        'Dim attatch As New Attatch

        username = Session("username")
        usercode = Session("usercode")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If
        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "")
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########



        If Not IsPostBack Then
            objbranch.SetComboBranch(cboBranch, "")

            itemtable = createdetailtable()

            objsupplier.SetCboSupplier(cboSupplier)
        Else

            Dim target = Request.Form("__EVENTTARGET")
            If target = "searchCostCommited_list" Then

                Dim argument As String = Request("__EVENTARGUMENT")
                If argument = "focus" Then
                    searchCostCommited_list()
                End If

            End If

        End If

        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdetailid", GetType(Integer))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("detailpayment", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("visiblestatus", GetType(String))



        Return dt
    End Function
    Private Sub cboSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSupplier.SelectedIndexChanged

        searchCostCommited_list()
    End Sub

    Private Sub BindData()
        cntdt = itemtable.Rows.Count
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Public Sub searchCostCommited_list()

        Dim objjob As New jobs
        Dim detailtable As New DataTable
        Try


            itemtable = objjob.Jobs_CostCommited_list(cboSupplier.SelectedItem.Value, chkvisible.Checked, txtjobcode.Text.ToString, cboBranch.SelectedValue)

            Session("joblist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnJTN_Click(sender As Object, e As EventArgs) Handles btnJTN.Click
        Dim confirmValue As String = Request.Form("delete_value")
        'Dim rawresp As String = "[{""id"":""1""},{""id"":""2""},{""id"":""3""},{""id"":""4""},{""id"":""5""},{""id"":""6""},{""id"":""7""}]"

        Dim result = JsonConvert.DeserializeObject(Of ArrayList)(confirmValue)
        Dim token As JToken
        Dim id
        Dim code
        If result IsNot Nothing Then

            Dim objjob As New jobs

            Dim jyncode As String = objjob.Jobs_Get_RunningNO_JTN()

            For Each value As Object In result
                token = JObject.Parse(value.ToString())
                id = token.SelectToken("id")
                code = token.SelectToken("code")
                Try
                    Dim idjtn As String = objjob.Jobs_MapToNonPO_Save(jyncode, id, Session("usercode"))
                    'Dim objapproval As New Approval
                    'objapproval.deleteDetailbycrossid(id, Session("usercode"))

                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    'Dim javaScript As String = "alert('" & ex.Message & "');"
                    Dim javaScript As String = "alertWarning('Delete fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    GoTo endprocess
                End Try
            Next value
            'BindData()
            Response.Redirect("../OPS/Non-PO/Payment/Payment2.aspx?f=JOB&code_ref=" + jyncode)
        End If
endprocess:
    End Sub

    Private Sub gvRemind_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRemind.RowCommand
        If e.CommandName = "Select" Then
            'Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            'Reference the GridView Row.
            Dim row As GridViewRow = gvRemind.Rows(rowIndex)

            'Fetch value of Name.

            Dim code As String = TryCast(row.FindControl("lbjobcode"), Label).Text
            Dim dtlid As String = TryCast(row.FindControl("lbid"), Label).Text

            'Fetch value of Country.
            'Dim country As String = row.Cells(1).Text


            Dim objjob As New jobs

            objjob.UpdateUnlockCost(code, dtlid, usercode)

            searchCostCommited_list()
            Response.Write("<script>window.open ('jobs_Close.aspx?jobno=" + code + "&jobdetailid=" + dtlid + "','_blank');</script>")
        ElseIf e.CommandName = "visible" Then
            Dim a As String
            'Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            'Reference the GridView Row.
            Dim row As GridViewRow = gvRemind.Rows(rowIndex)

            'Fetch value of Name.

            Dim code As String = TryCast(row.FindControl("lbjobcode"), Label).Text
            Dim dtlid As String = TryCast(row.FindControl("lbid"), Label).Text

            'Fetch value of Country.
            'Dim country As String = row.Cells(1).Text


            Dim objjob As New jobs

            objjob.Costvisible(code, dtlid, usercode)

            searchCostCommited_list()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        searchCostCommited_list()
    End Sub

    Private Sub chkvisible_CheckedChanged(sender As Object, e As EventArgs) Handles chkvisible.CheckedChanged

        searchCostCommited_list()
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("visiblestatus") = "__hide" Then
                e.Row.CssClass = "opacity-50"
            End If
        End If
    End Sub
End Class