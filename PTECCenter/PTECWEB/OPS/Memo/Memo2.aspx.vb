Imports System.Drawing

Public Class Memo2
    Inherits System.Web.UI.Page

    Public menutable As DataTable

    Public detailtable As DataTable '= createdetailtable()
    Public PermissionOwner As DataSet '= createtable()
    Public CommentTable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim attatch As New Attatch

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

        If Not IsPostBack() Then
        Else
            detailtable = Session("detailtable")
        End If
    End Sub
End Class