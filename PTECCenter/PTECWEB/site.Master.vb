Public Class site
    Inherits System.Web.UI.MasterPage
    Public menutable As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        'Session.RemoveAll()
        Session.Abandon()
        Response.Redirect("~\default.aspx")
    End Sub
End Class