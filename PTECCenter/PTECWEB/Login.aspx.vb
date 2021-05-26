Public Class Login
    Inherits System.Web.UI.Page
    Dim usertable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("usercode") = "PAB"
        'Session("username") = "Pison Anekboonyapirom"

        If Not (Session("usercode") Is Nothing) Then
            Response.Redirect("default.aspx")
        End If
        If Not IsPostBack() Then

        Else

        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Not String.IsNullOrEmpty(txtUsername.Text) _
            And Not String.IsNullOrEmpty(txtPassword.Text) Then
            Dim user As New Users
            Try
                usertable = user.Login(txtUsername.Text, txtPassword.Text)
                If usertable.Rows(0).Item("password") = 2 Then
                    lblMessage.Text = "ชื่อผู้ใช้ หรือรหัสผ่านไม่ถูกต้อง"
                Else
                    Session("userid") = usertable.Rows(0).Item("userid")
                    Session("usercode") = usertable.Rows(0).Item("usercode")
                    Session("username") = usertable.Rows(0).Item("name")
                    Session("dep") = usertable.Rows(0).Item("dep")
                    Session("branchid") = usertable.Rows(0).Item("branchid")
                    Session("secid") = usertable.Rows(0).Item("secid")
                    Session("positionid") = usertable.Rows(0).Item("PositionID")
                    Session("areaid") = usertable.Rows(0).Item("areaid")

                    If usertable.Rows(0).Item("changepassword") = 0 Then
                        Response.Redirect("~\ADMIN\changepassword.aspx?status=change")
                    Else
                        If Session("pre_page") IsNot Nothing Then
                            Response.Redirect(Session("pre_page"))
                        Else
                            Response.Redirect("default.aspx")
                        End If
                    End If
                End If
            Catch ex As Exception
                lblMessage.Text = ex.Message
            End Try

        End If
    End Sub
End Class