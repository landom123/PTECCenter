Imports System.IO
Public Class FrmRemind
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public itemtable As DataTable = createdetailtable()
    Public menutable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
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
            itemtable = LoadRemind(usercode)
            Session("remindtable") = itemtable
            BindData()
        Else
            itemtable = Session("remindtable")
            BindData()
        End If
    End Sub
    Private Sub BindData()
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("RemindInfoFrm.aspx?remindid=0")
    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub



    Private Function LoadRemind(usercode As String) As DataTable
        Dim result As DataTable
        Dim remindobj As New Remind
        Try
            result = remindobj.List(usercode)
            Session("itemtable") = result
        Catch ex As Exception

        End Try
        Return result
    End Function
    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("remindid", GetType(Double))
        dt.Columns.Add("emailmessage", GetType(String))
        'dt.Columns.Add("email", GetType(String))
        dt.Columns.Add("reminddate", GetType(String))
        dt.Columns.Add("activeid", GetType(Integer))

        Return dt
    End Function

End Class