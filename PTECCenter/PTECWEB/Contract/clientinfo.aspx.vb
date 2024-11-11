

Public Class clientinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, clientno, contractno, projectno As String
    Public clientid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New TitleName

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########

        'txtBirthday.Attributes.Add("readonly", "readonly")


        cboSex.Items.Add("Male")
        cboSex.Items.Add("Female")
        'Dim objsupplier As New Supplier

        'SetCboContractLand(cboContractLand)
        'SetCboContractBuild(cboContractBu)
        'SetCboContractDayRent(cboContractDayRent)

        If IsPostBack() Then

            contractno = Session("contractno")
            clientno = Session("clientno")
            lblContracttype.Text = Session("contracttype")
        Else
            contractno = Request.QueryString("contractno")
            clientno = Request.QueryString("clientno")
            projectno = Session("projectno")

            lblContracttype.Text = Session("contracttype")

            Session("contractno") = contractno
            Session("clientno") = clientno



            If Not String.IsNullOrEmpty(clientno) Then
                FindData(clientno)
            Else
                txtContractNo.Text = contractno
                SetButton("NEW")
            End If

            If loadRequestProject() = False Then
                Exit Sub
            End If



        End If

    End Sub

    Public Sub SetButton(chkstatus As String)

        Select Case chkstatus
            Case = "บันทึก", "SAVE", "New", "ส่งกลับแก้ไข"
                lblStatus.Text = chkstatus
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkGreen
                btnSave.Enabled = True
                'btnNewAgree.Enabled = True
                btnNew.Enabled = True
                btnDel.Enabled = False

            Case "ยืนยัน(รออนุมัติ)", "ยกเลิก", "อนุมัติ", "Reject"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnSave.Enabled = False
                'btnNewAgree.Enabled = True
                btnNew.Enabled = False
                btnDel.Enabled = False

        End Select
    End Sub

    Private Sub SetCboContractLand(obj As Object)
        Dim ag As New Contract
        obj.DataSource = ag.loadContractLand(0)
        obj.DataValueField = "ID"
        obj.DataTextField = "LanName"
        obj.DataBind()
    End Sub

    Private Sub SetCboContractBuild(obj As Object)
        Dim ag As New Contract
        obj.DataSource = ag.loadContractBuild(0, 0)
        obj.DataValueField = "ID"
        obj.DataTextField = "BuName"
        obj.DataBind()
    End Sub

    Private Sub SetCboContractDayRent(obj As Object)
        Dim ag As New Contract
        obj.DataSource = ag.loadContractDayRent(0, 0, 0)
        obj.DataValueField = "ID"
        obj.DataTextField = "DrName"
        obj.DataBind()
    End Sub
    Private Sub FindData(clientno As String)
        Dim mytable As DataTable
        Dim client As New Client
        Try
            mytable = client.Find(clientno)
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
            txtContractNo.Text = contractno
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

            txtAddress1.Text = .Item("address1")
            txtSubdistrict1.Text = .Item("subdistrict1")
            txtDistrict1.Text = .Item("district1")
            txtProvince1.Text = .Item("province1")
            txtPostcode1.Text = .Item("postcode1")

            SetButton(.Item("status"))
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
        SetButton("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim clientno As String
        Dim err, scriptKey, javaScript As String
        If validateData() Then
            Try
                clientno = Save()
                txtClientNo.Text = clientno
                Session("clientno") = clientno

            Catch ex As Exception
                err = ex.Message.ToString.Replace("'", "")
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

            If String.IsNullOrEmpty(err) Then
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
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
        Dim client As New Client
        Dim clientno As String,
            clientname As String, cardid As String, sex As String,
            companyname As String, birthday As DateTime, mobile As String,
            tel As String, email As String, line As String,
            address As String, subdistrict As String, district As String,
            province As String, postcode As String,
            address1 As String, subdistrict1 As String, district1 As String,
            province1 As String, postcode1 As String

        clientno = txtClientNo.Text
        clientname = txtName.Text
        cardid = txtCardID.Text
        sex = Strings.Left(cboSex.Text, 1)
        companyname = txtCompany.Text
        If txtBirthday.Text = "" Then txtBirthday.Text = "01/01/1900"
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

        address1 = txtAddress1.Text
        subdistrict1 = txtSubdistrict1.Text
        district1 = txtDistrict1.Text
        province1 = txtProvince1.Text
        postcode1 = txtPostcode1.Text

        result = client.Save(contractno, clientno, usercode, clientname, cardid, sex, companyname, birthday, mobile, tel, email, line _
                             , address, subdistrict, district, province, postcode, address1, subdistrict1, district1, province1, postcode1)

        Return result
    End Function

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub

    Private Function loadRequestProject() As Boolean
        Try
            Dim objReq As New clsRequestContract
            Dim dt As New DataTable

            dt = objReq.loadRequestProject(projectno)
            If dt Is Nothing Then Return False
            For Each dr As DataRow In dt.Rows

                txtName.Text = dr("CustName")
                txtCardID.Text = dr("CardID")
                cboSex.Text = dr("Gender")
                txtCompany.Text = dr("Company")
                txtMobile.Text = dr("Mobile")
                txtTel.Text = dr("Tel")
                txtEmail.Text = dr("Email")
                txtLine.Text = dr("Line")

                txtAddress.Text = dr("Address")
                txtAddress1.Text = dr("Address")
                txtSubdistrict.Text = dr("SubDistrict")
                txtSubdistrict1.Text = dr("SubDistrict")
                txtDistrict.Text = dr("District")
                txtDistrict1.Text = dr("District")
                txtProvince.Text = dr("Province")
                txtProvince1.Text = dr("Province")
                txtPostcode.Text = dr("PostCode")
                txtPostcode1.Text = dr("PostCode")
            Next



            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err '"alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function
End Class