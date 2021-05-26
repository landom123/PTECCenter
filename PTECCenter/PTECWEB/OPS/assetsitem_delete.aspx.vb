Public Class assetsitem_delete
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim assetcode As String = Request.QueryString("assetcode")
        Dim itemtypeid As Double = Request.QueryString("assetitemtyeid")
        Dim detail As String = Request.QueryString("detail")
        Dim username As String = Session("username")
        Dim ass As New Assets
        Try
            ass.ItemDel(assetcode, itemtypeid, username, detail)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('เกิดข้อผิดพลาด Assets Item Delete');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Response.Redirect("../ops/Assets.aspx?assetcode=" & assetcode)
    End Sub

End Class