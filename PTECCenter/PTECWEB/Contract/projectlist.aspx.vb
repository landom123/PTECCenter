

Imports System.IO

Imports ClosedXML.Excel
Public Class projectlist
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public projecttable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")


        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'gsmtable = Session("gsmtable")
            'BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("project") Is Nothing) Then
                projecttable = Session("project")
                BindData()
            Else
                List()
            End If
        End If

    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("projectno", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("remark", GetType(String))
        dt.Columns.Add("agstatus", GetType(Date))
        dt.Columns.Add("approveby", GetType(String))
        dt.Columns.Add("approvedate", GetType(String))
        dt.Columns.Add("link", GetType(String))


        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = projecttable
        gvData.DataBind()
    End Sub

    Protected Sub List()
        'Dim InvoiceDate As String
        Dim objprj As New Project

        'InvoiceDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        projecttable = objprj.List()
        Session("project") = projecttable
        BindData()
    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound


    End Sub

End Class