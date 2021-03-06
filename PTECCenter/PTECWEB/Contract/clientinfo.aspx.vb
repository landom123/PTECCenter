

Imports System.IO

Imports ClosedXML.Excel
Public Class clientinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, clientno As String
    Public clientid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New titlename

        usercode = Session("usercode")
        username = Session("username")

        txtBirthday.Attributes.Add("readonly", "readonly")


        cboSex.Items.Add("Male")
        cboSex.Items.Add("Female")
        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

        Else
            clientno = Request.QueryString("clientno")

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            If Not String.IsNullOrEmpty(clientno) Then
                FindData(clientno)
            End If

        End If

    End Sub
    Private Sub FindData(clientno As String)
        Dim mytable As DataTable
        Dim client As New AgreeClient
        Try
            mytable = client.Client_Info(clientno)
            ShowData(mytable)
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            txtClientNo.Text = .Item("clientno")
            txtName.Text = .Item("clientname")
            txtCardID.Text = .Item("cardid")
            cboSex.SelectedIndex = cboSex.Items.IndexOf(cboSex.Items.FindByValue(.Item("sex")))
            txtCompany.Text = .Item("companyname")
            txtBirthday.Text = .Item("birthday")
            txtMobile.Text = .Item("mobile")
            txtTel.Text = .Item("tel")
            txtEmail.Text = .Item("email")
            txtLine.Text = .Item("line")
            txtCreateBy.Text = .Item("createby")
            txtCreateDate.Text = .Item("createdate")
            txtAddress.Text = .Item("address")
            txtSubdistrict.Text = .Item("subdistrict")
            txtDistrict.Text = .Item("district")
            txtProvince.Text = .Item("province")
            txtPostcode.Text = .Item("postcode")

        End With
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub
    Private Sub Clear()
        txtClientNo.Text = ""
        txtName.Text = ""
        txtCardID.Text = ""

        txtCompany.Text = ""
        txtBirthday.Text = ""
        txtMobile.Text = ""
        txtTel.Text = ""
        txtEmail.Text = ""
        txtLine.Text = ""
        txtCreateBy.Text = ""
        txtCreateDate.Text = ""
        txtAddress.Text = ""
        txtSubdistrict.Text = ""
        txtDistrict.Text = ""
        txtProvince.Text = ""
        txtPostcode.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim clientno As String
        Dim err, scriptKey, javaScript As String
        If validateData() Then
            Try
                clientno = Save()
                txtClientNo.Text = clientno
                Session("clientno") = clientno
                err = "บันทึกข้อมูลเรียบร้อย"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Catch ex As Exception
                err = ex.Message.ToString.Replace("'", "")
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

        End If
    End Sub
    Private Function validateData() As Boolean
        Dim result As Boolean = True
        Dim err, scriptKey, javaScript As String

        If String.IsNullOrEmpty(txtName.Text) Then
            result = False
            err = "กรุณาระบุชื่อ นามสกุล คู่สัญญา"
        End If

        If result = False Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Function Save() As String
        Dim result As String
        Dim client As New AgreeClient
        Dim clientno As String,
            clientname As String, cardid As String, sex As String,
            companyname As String, birthday As DateTime, mobile As String,
            tel As String, email As String, line As String,
            address As String, subdistrict As String, district As String,
            province As String, postcode As String
        clientno = txtClientNo.Text
        clientname = txtName.Text
        cardid = txtCardID.Text
        sex = Strings.Left(cboSex.Text, 1)
        companyname = txtCompany.Text
        birthday = DateTime.Parse(txtBirthday.Text)
        mobile = txtMobile.Text
        tel = txtTel.Text
        email = txtEmail.Text
        line = txtLine.Text
        address = txtAddress.Text
        subdistrict = txtSubdistrict.Text
        district = txtDistrict.Text
        province = txtProvince.Text
        postcode = txtPostcode.Text


        result = client.Client_Save_Info(clientno, usercode, clientname, cardid, sex, companyname,
birthday, mobile, tel, email, line, address, subdistrict, district, province, postcode)




        Return result
    End Function

End Class