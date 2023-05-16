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



        If Not IsPostBack Then


            itemtable = createdetailtable()

            objsupplier.SetCboSupplier(cboSupplier)
        Else


        End If

        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdetailid", GetType(Integer))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("cost", GetType(Double))


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

    Private Sub searchCostCommited_list()

        Dim objjob As New jobs
        Dim detailtable As New DataTable
        Try


            itemtable = objjob.Jobs_CostCommited_list(cboSupplier.SelectedItem.Value)

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
End Class