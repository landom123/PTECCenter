Public Class ChangePassword
    Inherits System.Web.UI.Page
    Public usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        usercode = Session("usercode")
        username = Session("username")
        Dim status As String = Request.QueryString("status")
        If Session("usercode") Is Nothing Then
            Response.Redirect("~/login.aspx")
        End If
        If Not IsPostBack() Then
            If status = "change" Then
                lblMessage.Text = "เข้าระบบครั้งแรก กรุณาเปลี่ยนรหัสผ่าน"
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not String.IsNullOrEmpty(txtpassword.Text) _
            And Not String.IsNullOrEmpty(txtconfirmPassword.Text) Then
            If txtpassword.Text <> txtconfirmPassword.Text Then
                lblMessage.Text = "รหัสผ่านทั้ง 2 ไม่ตรงกัน"
            Else
                Dim user As New Users
                Try
                    user.ChangePassword(usercode.ToUpper, txtpassword.Text)
                    Response.Redirect("~\default.aspx")
                Catch ex As Exception
                    lblMessage.Text = ex.Message
                End Try
            End If
        End If
    End Sub

End Class