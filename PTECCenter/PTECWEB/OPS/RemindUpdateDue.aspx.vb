Public Class RemindUpdateDue
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public itemtable As DataTable = createdetailtable()
    Public menutable As DataTable
    Public usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
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


        If Not IsPostBack() Then
            SetCboBranch()
        Else
            itemtable = Session("remindtable")
            BindData()
        End If
    End Sub
    Private Sub BindData()
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        '*** remindid ***'
        Dim lblID As Label = CType(e.Row.FindControl("lblID"), Label)
        If Not IsNothing(lblID) Then
            lblID.Text = e.Row.DataItem("remindid")
        End If

        '*** message ***'
        Dim lblsubject As Label = CType(e.Row.FindControl("lblsubject"), Label)
        If Not IsNothing(lblsubject) Then
            lblsubject.Text = e.Row.DataItem("emailmessage")
        End If
        '*** lbldate ***'
        Dim lbldate As Label = CType(e.Row.FindControl("lbldate"), Label)
        If Not IsNothing(lbldate) Then
            lbldate.Text = e.Row.DataItem("duedate")
        End If

    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub SetCboBranch()
        Dim branch As New Branch
        cboBranch.DataSource = branch.List(usercode)
        cboBranch.DataValueField = "branchid"
        cboBranch.DataTextField = "name"
        cboBranch.DataBind()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        itemtable = LoadRemind(usercode, cboBranch.SelectedValue)
        Session("remindtable") = itemtable
        BindData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim rows As Integer = gvRemind.Rows.Count
        Dim remind As New Remind

        For i = 0 To rows - 1
            Dim row As GridViewRow = gvRemind.Rows(i)
            Dim id As String = row.Cells(0).Text
            Dim chkSelect As CheckBox = DirectCast(row.FindControl("chkitem"), CheckBox)
            Dim remindid As Label = CType(row.FindControl("lblid"), Label)
            Dim reminddate As DateTime = Convert.ToDateTime(txtDueDate.Text)

            If chkSelect.Checked = True Then
                Try
                    remind.UpdateRemindDate(usercode, remindid.Text, reminddate)
                Catch ex As Exception
                    i = rows + 1
                    Response.Write("<script language=javascript>alert('" & ex.Message & "')</script>")
                End Try

            End If

        Next i
    End Sub

    Private Function LoadRemind(usercode As String, branchid As Integer) As DataTable
        Dim result As DataTable
        Dim remindobj As New Remind
        Try
            result = remindobj.List(usercode, branchid)
            Session("itemtable") = result
        Catch ex As Exception

        End Try
        Return result
    End Function
    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("remindid", GetType(Double))
        dt.Columns.Add("reminddetail", GetType(String))
        dt.Columns.Add("email", GetType(String))
        dt.Columns.Add("reminddate", GetType(String))
        dt.Columns.Add("activeid", GetType(Integer))

        Return dt
    End Function

End Class