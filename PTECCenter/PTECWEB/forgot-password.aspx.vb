Public Class forgotpassword
    Inherits System.Web.UI.Page
    Dim usertable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("usercode") = "PAB"
        'Session("username") = "Pison Anekboonyapirom"

        If Not (Session("usercode") Is Nothing) Then
            Response.Redirect("default.aspx")
        End If
        If Not IsPostBack() Then

            preReset.Visible = True
            postReset.Visible = False

        End If
    End Sub

    Function RandomString(r As Random, digit As Integer)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim sb As New StringBuilder
        Dim cnt As Integer = digit
        For i As Integer = 1 To cnt
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim objUsers As New Users
        Dim r As New Random
        Dim randomstr As String = RandomString(r, 4)

        Try
            Dim newpass As String = RandomString(r, 6)
            objUsers.forgotPassword(txtUsercode.Text.ToString, randomstr, newpass, txtEmail.Text.ToString)

            preReset.Visible = False
            postReset.Visible = True

            lbcodeRef.Text = "กรุณาเช็คกล่องข้อความใน email Coderef : " & randomstr
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class