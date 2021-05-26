Public Class branchpermissionadd
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim branchid As String = Request.QueryString("branchid")
        Dim usercode As String = Session("usercode")
        Dim objbranch As New Branch

        Dim result As Boolean
        Try
            result = objbranch.PermissionAdd(usercode, branchid)
            ' Session("status") = "cancel"
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('เกิดข้อผิดพลาด Assets Item Delete');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Response.Redirect("branchpermission.aspx")
    End Sub

End Class