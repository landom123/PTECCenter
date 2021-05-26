Public Class UserManage
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usercode, username, dep As String
        usercode = Session("usercode")
        username = Session("username")
        dep = Session("dep")
        Dim objbranch As New Branch
        Dim objdepart As New Department
        Dim objsection As New Section
        Dim objposition As New Position

        If Session("menulist") Is Nothing Then
            If Not String.IsNullOrEmpty(usercode) Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            End If
        Else
            menutable = Session("menulist")
        End If

        If Not IsPostBack() Then
            If Session("usercode") Is Nothing Then
                Response.Redirect("~/login.aspx")
            End If
            objbranch.SetComboBranch(cboBranch)
            objdepart.SetCboDepartment(cboDepart, 0)
            objsection.SetCboSection(cboSection, 0)
            objposition.SetCboPosition(cboPosition)
        End If
    End Sub

End Class