Public Class JobsDetail_delete
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jobno As String = Request.QueryString("jobno")
        Dim jobdetailid As Double = Request.QueryString("jobdetailid")
        Dim usercode As String = Session("usercode")
        Dim job As New jobs
        Dim result As Boolean
        Try
            result = job.detail_Del(jobno, jobdetailid, usercode)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('เกิดข้อผิดพลาด Assets Item Delete');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Response.Redirect("jobs.aspx?jobno=" & jobno)
    End Sub

End Class