Public Class BranchPermission
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public branchtable As DataTable
    Public username, usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objBranch As New Branch


        username = Session("username")
        usercode = Session("usercode")


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable

            branchtable = objBranch.ListAllForSelected(usercode)
            'Session("branchtable") = branchtable
        Else
            branchtable = objBranch.ListAllForSelected(usercode)
            menutable = Session("menulist")
            'Session("branchtable") = branchtable
        End If
    End Sub

End Class