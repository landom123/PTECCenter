Public Class UserManage
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public maintable As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usercode, username, dep As String
        usercode = Session("usercode")
        username = Session("username")
        dep = Session("dep")
        Dim objbranch As New Branch
        Dim objdepart As New Department
        Dim objsection As New Section
        Dim objposition As New Position


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        If Session("menulist") Is Nothing Then
            If Not String.IsNullOrEmpty(usercode) Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            End If
        Else
            menutable = Session("menulist")
        End If

        If Not IsPostBack() Then

            objbranch.SetComboBranch(cboBranch)
            objdepart.SetCboDepartment(cboDepart, 0)
            objsection.SetCboSection(cboSection, 0)
            objposition.SetCboPosition(cboPosition)
            Session("status") = "new"
            If Session("maintable_usermanage") IsNot Nothing Then
                showdata(Session("maintable_usermanage"))
            End If

            SetMenu()
        Else

            maintable = Session("maintable_usermanage")
        End If
    End Sub


    Private Sub SetMenu()
        Select Case Session("status")
            Case = "new"
                txtUsercode.Attributes.Remove("readonly")

                btnNew.Enabled = True
                btnSave.Enabled = True
                btnUpdate.Enabled = False
                btnResetPassword.Enabled = False
            Case = "edit"

                txtUsercode.Attributes.Add("readonly", "true")

                btnNew.Enabled = True
                btnSave.Enabled = False
                btnUpdate.Enabled = True
                btnResetPassword.Enabled = True
        End Select
    End Sub
    Private Sub addUser()

        Dim users As New Users
        Dim usercode As String
        Try

            usercode = users.AddUser(txtUserName.Text.Trim,
                          txtUsercode.Text.Trim,
                          cboBranch.SelectedItem.Value,
                          cboDepart.SelectedItem.Value,
                          cboSection.SelectedItem.Value,
                          cboPosition.SelectedItem.Value,
                          txtEmail.Text.Trim,
                          txtUsercode.Text.Trim.ToLower + "@rpcthai.com")
            findUser(usercode)
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertSuccess();"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub findUser(usercode As String)
        Dim users As New Users
        Try
            maintable = users.Find("", usercode.Trim)
            If maintable.Rows.Count > 0 Then
                Session("maintable_usermanage") = maintable
                showdata(maintable)
            End If
            SetMenu()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub showdata(mytable As DataTable)
        If mytable.Rows.Count > 0 Then
            With mytable.Rows(0)

                txtUsercode.Text = .Item("UserCode")
                txtUserName.Text = .Item("name")

                cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(.Item("BranchID").ToString))
                cboDepart.SelectedIndex = cboDepart.Items.IndexOf(cboDepart.Items.FindByValue(.Item("depid").ToString))
                cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(.Item("secid").ToString))
                cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(.Item("PositionID").ToString))
                txtEmail.Text = .Item("email")

                Session("status") = "edit"

            End With
        End If

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Session("status") = "new"
        ClearText()
        SetMenu()
    End Sub
    Private Sub ClearText()
        txtUsercode.Text = ""
        txtUserName.Text = ""
        txtEmail.Text = ""

        cboBranch.SelectedIndex = -1
        cboDepart.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboPosition.SelectedIndex = -1

        If maintable IsNot Nothing Then
            maintable.Rows.Clear()
        End If

        Session("maintable_usermanage") = maintable
        Session("status") = "new"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        addUser()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        addUser()
    End Sub


    Private Sub txtUsercode_TextChanged(sender As Object, e As EventArgs) Handles txtUsercode.TextChanged
        findUser(txtUsercode.Text.Trim)
    End Sub
    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        Dim scriptKey As String = "alert"
        'Dim javaScript As String = "alert('" & ex.Message & "');"
        Dim javaScript As String = "resetPassword();"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    End Sub


    <System.Web.Services.WebMethod>
    Public Shared Function resetPassword(ByVal usercode As String, ByVal pass As String)
        Dim user As New Users

        Try
            user.resetPassword(usercode, pass)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function
End Class