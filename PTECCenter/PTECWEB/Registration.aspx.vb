Public Class Registration
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not IsPostBack() Then

        Else

        End If
    End Sub

End Class