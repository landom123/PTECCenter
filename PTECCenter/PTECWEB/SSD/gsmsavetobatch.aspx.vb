Public Class gsmsavetobatch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim br_code As String = Request.QueryString("br_code")
        Dim closedate As String = Request.QueryString("closedate")
        Dim userid As Double = Session("userid")

        Dim objgsm As New gsm
        Try
            objgsm.Send_GSM_to_Batch(br_code, closedate, 1, userid)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('เกิดข้อผิดพลาด Send gam to batch');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Response.Redirect(Request.UrlReferrer.ToString())
    End Sub

End Class