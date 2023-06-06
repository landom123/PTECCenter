Imports System.Globalization
Imports System.Reflection.Emit
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Controls
Imports DocumentFormat.OpenXml.EMMA
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class requestcontract
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, assetsno, contractno, projectno As String
    'Public clientid As Double = 0

    Dim dtBranch As New DataTable
    Dim dtStatus As New DataTable
    Dim iDocIDOr As Integer

    'Dim sMode As String = "NEW"

    Public clienttable As DataTable = create()

    Dim objCo As New clsRequestContract

    Dim objPay As New clsPayment

#Region "Function"

    Private Sub SetCboContractType(obj As Object)
        Dim ag As New Contract

        obj.DataSource = ag.ContractTypeCbo()
        obj.DataValueField = "agtypeid"
        obj.DataTextField = "agtype"
        obj.DataBind()
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
    Private Sub FindData(assetsno As String)
        Dim mytable As DataTable
        Dim objassets As New ContractAssets
        Try
            mytable = objassets.Find(assetsno)
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
            'If .Item("fullarea") = True Then
            '    rdoFull.Checked = True
            'Else
            '    rdoPart.Checked = True
            'End If
            txtdocuno.Text = contractno

            'txtLandno.Text = .Item("landno")
            'txtSurveyNo.Text = .Item("surveyno")
            'txtSubDistrict.Text = .Item("subdistrict")
            'txtDistrict.Text = .Item("district")
            'txtProvince.Text = .Item("province")
            'txtRai.Text = .Item("area_rai")
            'txtNgan.Text = .Item("area_ngan")
            'txtWa.Text = .Item("area_wa")
            'txtGPS.Text = .Item("gps")

            SetButton(.Item("status"))

        End With
    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("No", GetType(Integer))
        dt.Columns.Add("clientid", GetType(Double))
        dt.Columns.Add("clientno", GetType(String))
        dt.Columns.Add("client", GetType(String))
        dt.Columns.Add("clientaddress", GetType(String))
        dt.Columns.Add("link", GetType(String))
        Return dt
    End Function

    Public Sub BindDataClient()
        gvData.DataSource = clienttable
        gvData.DataBind()
    End Sub
    Private Sub Clear()
        Dim objReq As New clsRequestContract
        txtName.Text = ""
        txtCardID.Text = ""
        cboSex.SelectedIndex = 0
        txtCompany.Text = ""
        txtMobile.Text = ""
        txtTel.Text = ""
        txtEmail.Text = ""
        txtLine.Text = ""
        txtAddress.Text = ""
        txtSubDistrict.Text = ""
        txtDistrict.Text = ""
        txtProvince.Text = ""
        txtPostcode.Text = ""

        iDocIDOr = 0

        cboBranch.SelectedIndex = 0
        cboSex.SelectedIndex = 0
        cboContractType.SelectedIndex = 0
        cboStatus.SelectedIndex = 0

        txtDocAction.Text = "NEW"
        txtdocuno.Text = objReq.GetDocRun(1, 0)
        SetButton("New")
    End Sub

    Private Function validateData() As Boolean
        Dim result As Boolean = True
        Dim err, scriptKey, javaScript As String

        'If String.IsNullOrEmpty(txtName.Text) Then
        '    result = False
        '    err = "กรุณาระบุชื่อ นามสกุล คู่สัญญา"
        'End If

        If result = False Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Function Save() As String

        Dim dtinfo As New DateTimeFormatInfo
        Dim result As String = Nothing
        Dim objReq As New clsRequestContract

        Dim DocuNo, Branch, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, Address, SubDistrict, District, Province, PostCode, CreateBy, RegistryTo As String
        Dim ContractID, StatusID As Integer
        Dim dContractBegindate, dContractEndDate, CreateDate As Date


        Branch = cboBranch.SelectedValue
        ContractID = cboContractType.SelectedValue
        dContractBegindate = DateAdd(DateInterval.Year, -543, CDate(txtContractBeginDate.Text))
        dContractEndDate = DateAdd(DateInterval.Year, -543, CDate(txtContractEndDate.Text))
        CustName = txtName.Text
        CardID = txtCardID.Text
        Gender = cboSex.Text
        Company = txtCompany.Text
        Mobile = txtMobile.Text
        Tel = txtTel.Text
        Email = txtEmail.Text
        Line = txtLine.Text
        StatusID = cboStatus.SelectedValue
        Address = txtAddress.Text
        SubDistrict = txtSubDistrict.Text
        District = txtDistrict.Text
        Province = txtProvince.Text
        PostCode = txtPostcode.Text
        CreateDate = Now.Date
        CreateBy = usercode
        'CreateDate = Now.Date
        'CreateBy = usercode
        DocuNo = txtdocuno.Text
        RegistryTo = txtRentalReg.Text


        If txtDocAction.Text = "NEW" Then
            txtdocuno.Text = objReq.GetDocRun(1, 1)
            DocuNo = txtdocuno.Text
            result = objReq.AddRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, RegistryTo)
        ElseIf txtDocAction.Text = "EDIT" Then
            result = objReq.UpdateRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, CInt(txtDocIDAction.Text), RegistryTo)
        End If

        txtDocIDAction.Text = 0
        txtDocAction.Text = "NEW"
        'Clear()

        Return result
    End Function

    Private Function loadRequest(iDocID As Integer) As Boolean

        Try
            Dim objReq As New clsRequestContract

            Dim dt As New DataTable

            dt = objReq.LoadRequest(iDocID)
            Clear()
            For Each dr As DataRow In dt.Rows
                txtdocuno.Text = dr("DocuNo")
                iDocIDOr = dr("ID")
                txtDocIDAction.Text = dr("ID")
                cboBranch.SelectedValue = dr("Branch")
                cboStatus.SelectedValue = dr("StatusID")
                cboContractType.SelectedValue = dr("ContractID")
                'txtContractBeginDate.Text = CDate(dr("BeginDate"))
                'txtContractEndDate.Text = CDate(dr("EndDate"))

                txtContractBeginDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
                txtContractEndDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("EndDate")))

                txtName.Text = dr("CustName")
                txtCardID.Text = dr("CardID")
                cboSex.SelectedValue = dr("Gender")
                txtCompany.Text = dr("Company")
                txtMobile.Text = dr("Mobile")
                txtTel.Text = dr("Tel")
                txtEmail.Text = dr("Email")
                txtLine.Text = dr("Line")

                txtAddress.Text = dr("Address")
                txtSubDistrict.Text = dr("SubDistrict")
                txtDistrict.Text = dr("District")
                txtProvince.Text = dr("Province")
                txtPostcode.Text = dr("PostCode")
                txtcontractPeriod.Text = dr("PeriodYear")
                txtcontractPeriod2.Text = dr("PeriodMonth")
                txtDocAction.Text = "EDIT"
            Next

            If loadContractPer() = False Then
                Return False
            End If

            If loadContractCompany() = False Then
                Return False
            End If

            If loadContractFix() = False Then
                Return False
            End If

            If loadCustContract2(txtdocuno.Text) = False Then
                Return False
            End If

            If loadContractFix() = False Then
                Return False
            End If

            If loadContractPayment() = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Private Function AddRequest() As Boolean

        Try

            Dim dtinfo As New DateTimeFormatInfo
            Dim result As String = Nothing
            Dim objReq As New clsRequestContract

            Dim DocuNo, Branch, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, Address, SubDistrict, District, Province, PostCode, CreateBy, RegistryTo As String
            Dim ContractID, StatusID As Integer
            Dim dContractBegindate, dContractEndDate, CreateDate As Date

            Dim err, scriptKey, javaScript As String

            If txtName.Text = "" Then
                err = "กรุณาตรวจระบุ ชื่อคู่สัญญา "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtCardID.Text = "" Then
                err = "กรุณาตรวจสอย เลขที่บัตรประชาชน "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtTaxID.Text = "" Then
                err = "กรุณาตรวจสอย เลขที่ผู้เสียภาษี "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAddress.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtSubDistrict.Text = "" Then
                err = "กรุณาตรวจสอย ที่ยู่ตำบล "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtDistrict.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่อำเภอ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtProvince.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่จังหวัด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtPostcode.Text = "" Then
                err = "กรุณาตรวจสอย รหัสไปรษณีย์ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            Branch = cboBranch.SelectedValue
            ContractID = cboContractType.SelectedValue
            dContractBegindate = DateAdd(DateInterval.Year, -543, CDate(txtContractBeginDate.Text))
            dContractEndDate = DateAdd(DateInterval.Year, -543, CDate(txtContractEndDate.Text))
            CustName = txtName.Text
            CardID = txtCardID.Text
            Gender = cboSex.Text
            Company = txtCompany.Text
            Mobile = txtMobile.Text
            Tel = txtTel.Text
            Email = txtEmail.Text
            Line = txtLine.Text
            StatusID = cboStatus.SelectedValue
            Address = txtAddress.Text
            SubDistrict = txtSubDistrict.Text
            District = txtDistrict.Text
            Province = txtProvince.Text
            PostCode = txtPostcode.Text
            CreateDate = Now.Date
            CreateBy = usercode
            'CreateDate = Now.Date
            'CreateBy = usercode
            DocuNo = txtdocuno.Text
            RegistryTo = txtRentalReg.Text

            If txtDocAction.Text = "NEW" Then
                txtdocuno.Text = objReq.GetDocRun(1, 1)
                DocuNo = txtdocuno.Text
                result = objReq.AddRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, RegistryTo)
            ElseIf txtDocAction.Text = "EDIT" Then
                result = objReq.UpdateRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, CInt(txtDocIDAction.Text), RegistryTo)
            End If


            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function
    Private Function AddContractPersonal() As Boolean
        Try
            Dim err, scriptKey, javaScript As String

            If txtName.Text = "" Then
                err = "กรุณาตรวจระบุ ชื่อคู่สัญญา "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtCardID.Text = "" Then
                err = "กรุณาตรวจสอย เลขที่บัตรประชาชน "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtTaxID.Text = "" Then
                err = "กรุณาตรวจสอย เลขที่ผู้เสียภาษี "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAddress.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtSubDistrict.Text = "" Then
                err = "กรุณาตรวจสอย ที่ยู่ตำบล "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtDistrict.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่อำเภอ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtProvince.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่จังหวัด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtPostcode.Text = "" Then
                err = "กรุณาตรวจสอย รหัสไปรษณีย์ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If



            'BrCode, BrName, Addr, SubDistrict, District, Province, PostCode, Tel, Contract, Remark, CreateDate, Createby

            If objCo.AddContractPerson(txtdocuno.Text, 1, txtName.Text, txtCardID.Text, txtTaxID.Text, cboSex.SelectedValue, txtAddress.Text, txtSubDistrict.Text, txtDistrict.Text, txtProvince.Text _
                                       , txtPostcode.Text, txtTel.Text, txtLine.Text, txtEmail.Text, usercode) = False Then
                Return False
            End If

            txtName.Text = ""
            txtCardID.Text = ""
            txtAddress.Text = ""
            txtSubDistrict.Text = ""
            txtDistrict.Text = ""
            txtProvince.Text = ""
            txtPostcode.Text = ""
            txtTel.Text = ""
            txtLine.Text = ""
            txtEmail.Text = ""

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function
    Private Function loadContractPer() As Boolean
        Try
            Dim dt As New DataTable

            dt = objCo.loadContractPersaonal(txtdocuno.Text)

            gvContractPer.DataSource = dt
            gvContractPer.DataBind()

            'Dim numrows As Integer
            'Dim numcells As Integer
            'Dim i As Integer
            'Dim j As Integer
            'Dim r As TableRow
            'Dim c As TableCell
            '' Generate rows and cells
            'numrows = dt.Rows.Count
            'numcells = dt.Columns.Count
            'For j = 0 To numrows - 1
            '    r = New TableRow()
            '    For i = 0 To numcells - 1
            '        c = New TableCell()
            '        'c.Controls.Add(New LiteralControl("row " & j & ", cell " & i))
            '        'If IsNumeric(dt.Rows(j).Item(i).ToString) = False Then
            '        c.Controls.Add(New LiteralControl(dt.Rows(j).Item(i).ToString))
            '        'Else
            '        '    c.Controls.Add(New LiteralControl(FormatNumber(dt.Rows(j).Item(i).ToString, 2)))
            '        'End If

            '        r.Cells.Add(c)
            '    Next i
            '  
            '    tblContractPer.Rows.Add(r)
            'Next j



            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function AddContractCompany() As Boolean
        Try
            Dim err, scriptKey, javaScript As String

            If txtCompany.Text = "" Then
                err = "กรุณาตรวจระบุ ชื่อบริษัท "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAddrCom.Text = "" Then
                err = "กรุณาตรวจสอบ เลขที่บริษัท "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtSubDistrictCom.Text = "" Then
                err = "กรุณาตรวจสอบ ตำบล/แขวง "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtDistrictCom.Text = "" Then
                err = "กรุณาตรวจสอบ อำเภอ/เขต "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtProvinceCom.Text = "" Then
                err = "กรุณาตรวจสอบ จังหวัด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtPostCodeCom.Text = "" Then
                err = "กรุณาตรวจสอบ รหัสไปรษณีย์ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtProvince.Text = "" Then
                err = "กรุณาตรวจสอบ ที่อยู่จังหวัด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtCustNameComPer.Text = "" Then
                err = "กรุณาตรวจสอบ ชื่อกรรมการ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAddrComPer.Text = "" Then
                err = "กรุณาตรวจสอบ ที่อยู่ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtSubDistrictComPer.Text = "" Then
                err = "กรุณาตรวจสอบ ตำบล/แขวง "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtDistrictComPer.Text = "" Then
                err = "กรุณาตรวจสอบ อำเภอ/เขต "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtProvinceComPer.Text = "" Then
                err = "กรุณาตรวจสอบ จังหวัด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtPostCodeComPer.Text = "" Then
                err = "กรุณาตรวจสอบ ไปรษณีย์ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If objCo.AddContractCompany(txtdocuno.Text, txtCompany.Text, txtAddrCom.Text, txtSubDistrictCom.Text, txtDistrictCom.Text, txtProvinceCom.Text, txtPostCodeCom.Text _
                                        , txtMobile.Text, txtCustNameComPer.Text, txtCardIDComPer.Text, txtTaxIDComPer.Text, txtAddrComPer.Text, txtSubDistrictComPer.Text _
                                        , txtDistrictComPer.Text, txtProvinceComPer.Text, txtPostCodeComPer.Text, txtTel.Text, txtLine.Text, txtEmail.Text, usercode) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function loadContractCompany() As Boolean
        Try
            Dim dt As New DataTable

            dt = objCo.loadContractCompany(txtdocuno.Text)

            gvContractCompanyPer.DataSource = dt
            gvContractCompanyPer.DataBind()

            'Dim numrows As Integer
            'Dim numcells As Integer
            'Dim i As Integer
            'Dim j As Integer
            'Dim r As TableRow
            'Dim c As TableCell
            '' Generate rows and cells
            'numrows = dt.Rows.Count
            'numcells = dt.Columns.Count
            'For j = 0 To numrows - 1
            '    r = New TableRow()
            '    For i = 0 To numcells - 1
            '        c = New TableCell()
            '        'c.Controls.Add(New LiteralControl("row " & j & ", cell " & i))
            '        'If IsNumeric(dt.Rows(j).Item(i).ToString) = False Then
            '        c.Controls.Add(New LiteralControl(dt.Rows(j).Item(i).ToString))
            '        'Else
            '        '    c.Controls.Add(New LiteralControl(FormatNumber(dt.Rows(j).Item(i).ToString, 2)))
            '        'End If

            '        r.Cells.Add(c)
            '    Next i
            '    'MyTable.Rows.Add(r)
            '    tblContractCompany.Rows.Add(r)
            'Next j



            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Sub SetCboPaymentType(obj As Object)
        Dim payment As New Payment

        obj.DataSource = payment.PaymentType_List()
        obj.DataValueField = "paymenttypeid"
        obj.DataTextField = "paymenttype"
        obj.DataBind()
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

    Private Function AddContractFix() As Boolean
        Try
            Dim err, scriptKey, javaScript As String
            Dim iDueType As Integer

            Dim dBeginDateFix, dEndDateFix, dDueDateFix As Date

            If txtAmountFix.Text = "" Then
                err = "กรุณาตรวจระบุ มูลค่า "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If rdoMonthFix.Checked = False And rdoYearFix.Checked = False Then
                err = "กรุณาตรวจสอบ รอบการจ่าย "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtFrequencyFix.Text = "" Then
                err = "กรุณาตรวจสอบ ความถี่การจ่าย "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtDueDateFix.Text = "" Then
                err = "กรุณาตรวจสอบ DueDate "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtBeginDateFix.Text = "" Then
                err = "กรุณาตรวจสอบ วันที่เริ่มต้น "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtEndDateFix.Text = "" Then
                err = "กรุณาตรวจสอบ วันที่สิ้นสุด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If rdoMonthFix.Checked = True Then
                iDueType = 1
            Else
                iDueType = 2
            End If

            dBeginDateFix = DateAdd(DateInterval.Year, -543, Date.Parse(txtBegindate.Text))
            dEndDateFix = DateAdd(DateInterval.Year, -543, Date.Parse(txtEnddate.Text))
            dDueDateFix = DateAdd(DateInterval.Year, -543, Date.Parse(txtDueDateFix.Text))

            If objCo.AddContractFixiblePayment(txtdocuno.Text, cboPayType.SelectedValue, iDueType, CInt(txtFrequencyFix.Text), dDueDateFix, CDbl(txtAmountFix.Text) _
                                               , dBeginDateFix, dEndDateFix, usercode) = False Then
                Return False
            End If

            'usercode

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function loadContractFix() As Boolean
        Try
            Dim dt As New DataTable

            dt = objCo.loadContractFixiblePayment(txtdocuno.Text)

            gvContractFix.DataSource = dt
            gvContractFix.DataBind()

            'Dim numrows As Integer
            'Dim numcells As Integer
            'Dim i As Integer
            'Dim j As Integer
            'Dim r As TableRow
            'Dim c As TableCell
            '' Generate rows and cells
            'numrows = dt.Rows.Count
            'numcells = dt.Columns.Count
            'For j = 0 To numrows - 1
            '    r = New TableRow()
            '    For i = 0 To numcells - 1
            '        c = New TableCell()
            '        'c.Controls.Add(New LiteralControl("row " & j & ", cell " & i))
            '        'If IsNumeric(dt.Rows(j).Item(i).ToString) = False Then
            '        c.Controls.Add(New LiteralControl(dt.Rows(j).Item(i).ToString))
            '        'Else
            '        '    c.Controls.Add(New LiteralControl(FormatNumber(dt.Rows(j).Item(i).ToString, 2)))
            '        'End If

            '        r.Cells.Add(c)
            '    Next i
            '    'MyTable.Rows.Add(r)
            '    tblContractFix.Rows.Add(r)
            'Next j



            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function loadBank() As Boolean
        Try
            Dim objReq As New clsRequestContract
            Dim dt As New DataTable

            dt = objPay.loadBank

            cboBank.DataSource = dt
            cboBank.DataValueField = "ID"
            cboBank.DataTextField = "BankName"
            cboBank.DataBind()

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

    Private Function loadCustContract2(sDocNo As String) As Boolean
        Try
            Dim objReq As New clsRequestContract
            Dim dt, dt2 As New DataTable

            dt = objPay.loadCustContract2(sDocNo)

            cboPayCust.DataSource = dt
            cboPayCust.DataValueField = "PersonName"
            cboPayCust.DataTextField = "PersonName"
            cboPayCust.DataBind()



            dt2 = objPay.loadCustContract2(sDocNo)

            cboTaxCust.DataSource = dt2
            cboTaxCust.DataValueField = "PersonName"
            cboTaxCust.DataTextField = "PersonName"
            cboTaxCust.DataBind()


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

    Private Function AddContractPayment() As Boolean
        Try
            Dim err, scriptKey, javaScript As String
            Dim iPayType As Integer

            If rdoTrans.Checked = False And rdoCheque.Checked = False Then
                err = "กรุณาตรวจสอบ วิธีชำระ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If cboBank.SelectedValue = "" Then
                err = "กรุณาตรวจสอบ ธนาคาร "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAccountNo.Text = "" Then
                err = "กรุณาตรวจสอบ เลขที่บัญชี "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAccountName.Text = "" Then
                err = "กรุณาตรวจสอบ ชื่อบัญชี "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If cboPayCust.SelectedValue = "" Then
                err = "กรุณาตรวจสอบ สั่งจ่าย "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If cboTaxCust.SelectedValue = "" Then
                err = "กรุณาตรวจสอบ หักภาษี ณ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If rdoTrans.Checked = True Then
                iPayType = 1
            Else
                iPayType = 2
            End If


            If objCo.AddContractPayment(txtdocuno.Text, iPayType, cboBank.SelectedValue, txtBankbranchname.Text, txtAccountNo.Text, txtAccountName.Text _
                                        , cboPayCust.SelectedValue, cboTaxCust.SelectedValue, txtACCode.Text, usercode) = False Then
                Return False
            End If

            'usercode

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function loadContractPayment() As Boolean
        Try
            Dim dt As New DataTable

            dt = objCo.loadContractPayment(txtdocuno.Text)

            gvPayment.DataSource = dt
            gvPayment.DataBind()

            'Dim numrows As Integer
            'Dim numcells As Integer
            'Dim i As Integer
            'Dim j As Integer
            'Dim r As TableRow
            'Dim c As TableCell
            '' Generate rows and cells
            'numrows = dt.Rows.Count
            'numcells = dt.Columns.Count
            'For j = 0 To numrows - 1
            '    r = New TableRow()
            '    For i = 0 To numcells - 1
            '        c = New TableCell()
            '        'c.Controls.Add(New LiteralControl("row " & j & ", cell " & i))
            '        'If IsNumeric(dt.Rows(j).Item(i).ToString) = False Then
            '        c.Controls.Add(New LiteralControl(dt.Rows(j).Item(i).ToString))
            '        'Else
            '        '    c.Controls.Add(New LiteralControl(FormatNumber(dt.Rows(j).Item(i).ToString, 2)))
            '        'End If

            '        r.Cells.Add(c)
            '    Next i
            '    'MyTable.Rows.Add(r)
            '    tblPayment.Rows.Add(r)
            'Next j



            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

#End Region
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New TitleName
        usercode = Session("usercode")
        username = Session("username")

        'txtBirthday.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            contractno = Session("contractno")
            assetsno = Session("assetsno")

        Else
            contractno = Request.QueryString("contractno")
            assetsno = Request.QueryString("assetsno")


            Session("contractno") = contractno
            Session("assetsno") = assetsno

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not String.IsNullOrEmpty(assetsno) Then
                FindData(assetsno)
            Else
                txtdocuno.Text = contractno
                SetButton("NEW")
            End If

            SetCboPaymentType(cboPayType)

            SetCboContractLand(cboContractLand)
            SetCboContractBuild(cboContractBu)
            SetCboContractDayRent(cboContractDayRent)


            If loadBank() = False Then
                Exit Sub
            End If


            Dim objprj As New Project
            dtBranch = objprj.loadBranch
            cboBranch.DataSource = dtBranch
            cboBranch.DataValueField = "BrCode"
            cboBranch.DataTextField = "Brname"
            cboBranch.DataBind()

            dtStatus = objprj.loadStatus(1)
            cboStatus.DataSource = dtStatus
            cboStatus.DataValueField = "ID"
            cboStatus.DataTextField = "StatusName"
            cboStatus.DataBind()

            SetCboContractType(cboContractType)

            cboSex.Items.Add("Male")
            cboSex.Items.Add("Female")
            cboSex.Items.Add("Not Specified")

            cboGenderComPer.Items.Add("Male")
            cboGenderComPer.Items.Add("Female")
            cboGenderComPer.Items.Add("Not Specified")

            'txtdocuno.Text = Request.QueryString("agreeno")
            Dim objReq As New clsRequestContract

            txtdocuno.Text = objReq.GetDocRun(1, 0)
            BindDataClient()

        End If
        'txtDocAction.Text = "NEW"

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Dim assetsno As String
        Dim err, scriptKey, javaScript As String
        err = ""
        If validateData() Then
            Try
                assetsno = Save()
                'txtClientNo.Text = clientno
                Session("assetsno") = assetsno

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

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        projectno = Session("projectno")
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub

    'Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
    '    Dim objgsm As New gsm
    '    Dim rowIndex As Integer
    '    Dim row As GridViewRow
    '    Dim clientid As Double
    '    Dim clientno As String

    '    If e.CommandName = "View" Then
    '        ''Determine the RowIndex of the Row whose Button was clicked.
    '        rowIndex = Convert.ToInt32(e.CommandArgument)
    '        row = gvData.Rows(rowIndex)

    '        clientid = Double.Parse(TryCast(row.FindControl("lblid"), Label).Text)
    '        clientno = TryCast(row.FindControl("lblclientno"), Label).Text
    '        Try
    '            Response.Redirect("clientinfo.aspx?clientno=" & clientno)
    '        Catch ex As Exception
    '            Dim msg As String = Strings.Replace(ex.Message, "'", " ")
    '            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between find data : " & msg & "');", True)
    '        End Try


    '    End If

    'End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            Dim objReq As New clsRequestContract
            Dim dtinfo As New DateTimeFormatInfo
            Dim dt As New DataTable

            Dim dBegindate, dEnddate As Date

            dBegindate = DateAdd(DateInterval.Year, -543, CDate(txtBegindate.Text))
            dEnddate = DateAdd(DateInterval.Year, -543, CDate(txtEnddate.Text))

            dt = objReq.FindRequest(dBegindate.ToString("yyyyMMdd", dtinfo), dEnddate.ToString("yyyyMMdd", dtinfo), 1)

            gvData.DataSource = dt
            gvData.DataBind()


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvData, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvData.SelectedRow.Cells(9).Text)

            If loadRequest(iDocID) = False Then
                Exit Sub
            End If
            'Dim country As String = gvData.SelectedRow.Cells(1).Text
            'Dim message As String = "Row Index: " & index & "\nName: " & name + "\nCountry: " & country
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" + message + "');", True)
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBoundPer(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvContractPer, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChangedPer(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvContractPer.SelectedRow.Cells(11).Text)
            Dim iItemNo As Integer = CInt(gvContractPer.SelectedRow.Cells(12).Text)

            txtName.Text = gvContractPer.SelectedRow.Cells(0).Text
            txtCardID.Text = gvContractPer.SelectedRow.Cells(1).Text
            txtTaxID.Text = gvContractPer.SelectedRow.Cells(2).Text
            txtAddress.Text = gvContractPer.SelectedRow.Cells(3).Text
            txtSubDistrict.Text = gvContractPer.SelectedRow.Cells(4).Text
            txtDistrict.Text = gvContractPer.SelectedRow.Cells(5).Text
            txtProvince.Text = gvContractPer.SelectedRow.Cells(6).Text
            txtPostcode.Text = gvContractPer.SelectedRow.Cells(7).Text
            txtPerTel.Text = gvContractPer.SelectedRow.Cells(8).Text
            txtPerLine.Text = gvContractPer.SelectedRow.Cells(9).Text
            txtPerEmail.Text = gvContractPer.SelectedRow.Cells(10).Text

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBoundCompanyPer(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvContractCompanyPer, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChangedCompanyPer(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvContractCompanyPer.SelectedRow.Cells(18).Text)
            Dim iItemNo As Integer = CInt(gvContractCompanyPer.SelectedRow.Cells(19).Text)

            txtCompany.Text = gvContractCompanyPer.SelectedRow.Cells(0).Text
            txtAddrCom.Text = gvContractCompanyPer.SelectedRow.Cells(1).Text
            txtSubDistrictCom.Text = gvContractCompanyPer.SelectedRow.Cells(2).Text
            txtDistrictCom.Text = gvContractCompanyPer.SelectedRow.Cells(3).Text
            txtProvinceCom.Text = gvContractCompanyPer.SelectedRow.Cells(4).Text
            txtPostCodeCom.Text = gvContractCompanyPer.SelectedRow.Cells(5).Text
            txtMobile.Text = gvContractCompanyPer.SelectedRow.Cells(6).Text

            txtCustNameComPer.Text = gvContractCompanyPer.SelectedRow.Cells(7).Text
            txtCardIDComPer.Text = gvContractCompanyPer.SelectedRow.Cells(8).Text
            txtTaxIDComPer.Text = gvContractCompanyPer.SelectedRow.Cells(9).Text
            txtAddrComPer.Text = gvContractCompanyPer.SelectedRow.Cells(10).Text
            txtSubDistrictComPer.Text = gvContractCompanyPer.SelectedRow.Cells(11).Text
            txtDistrictComPer.Text = gvContractCompanyPer.SelectedRow.Cells(12).Text
            txtProvinceComPer.Text = gvContractCompanyPer.SelectedRow.Cells(13).Text
            txtPostCodeComPer.Text = gvContractCompanyPer.SelectedRow.Cells(14).Text
            txtTel.Text = gvContractCompanyPer.SelectedRow.Cells(15).Text
            txtLine.Text = gvContractCompanyPer.SelectedRow.Cells(16).Text
            txtEmail.Text = gvContractCompanyPer.SelectedRow.Cells(17).Text


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBoundContractFix(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvContractFix, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChangedContractFix(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvContractFix.SelectedRow.Cells(7).Text)
            Dim iItemNo As Integer = CInt(gvContractFix.SelectedRow.Cells(8).Text)

            txtFrequencyFix.Text = gvContractFix.SelectedRow.Cells(2).Text
            txtDueDateFix.Text = gvContractFix.SelectedRow.Cells(3).Text
            txtBeginDateFix.Text = gvContractFix.SelectedRow.Cells(4).Text
            txtEndDateFix.Text = gvContractFix.SelectedRow.Cells(5).Text
            txtAmountFix.Text = CDbl(gvContractFix.SelectedRow.Cells(6).Text)

            cboPayType.SelectedValue = CInt(gvContractFix.SelectedRow.Cells(9).Text)

            Select Case CInt(gvContractFix.SelectedRow.Cells(10).Text)
                Case 1
                    rdoMonthFix.Checked = True
                Case 2
                    rdoYearFix.Checked = True
            End Select

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBoundPayment(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvPayment, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChangedPayment(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvPayment.SelectedRow.Cells(8).Text)
            Dim iItemNo As Integer = CInt(gvPayment.SelectedRow.Cells(9).Text)

            txtBankbranchname.Text = gvPayment.SelectedRow.Cells(2).Text
            txtAccountNo.Text = gvPayment.SelectedRow.Cells(3).Text
            txtAccountName.Text = gvPayment.SelectedRow.Cells(4).Text
            txtACCode.Text = gvPayment.SelectedRow.Cells(7).Text

            cboBank.SelectedValue = CInt(gvPayment.SelectedRow.Cells(11).Text)
            cboPayCust.Text = (gvPayment.SelectedRow.Cells(5).Text)
            cboTaxCust.Text = (gvPayment.SelectedRow.Cells(6).Text)

            Select Case CInt(gvPayment.SelectedRow.Cells(10).Text)
                Case 1
                    rdoTrans.Checked = True
                Case 2
                    rdoCheque.Checked = True
            End Select

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub


    Private Sub btnAddBranch_Click(sender As Object, e As EventArgs) Handles btnAddBranch.Click
        Try
            Dim err, scriptKey, javaScript As String

            If txtBranchCodeNew.Text = "" Then
                err = "กรุณาตรวจสอย รหัสสาขา "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Exit Sub
            End If

            If txtAddr.Text = "" Then
                err = "กรุณาตรวจสอย เลขที่สาขา "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Exit Sub
            End If

            If txtSubDistrictNew.Text = "" Then
                err = "กรุณาตรวจสอย ที่ยู่ตำบล "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Exit Sub
            End If

            If txtDistrictNew.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่อำเภอ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Exit Sub
            End If

            If txtProvinceNew.Text = "" Then
                err = "กรุณาตรวจสอย ที่อยู่จังหวัด "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Exit Sub
            End If

            If txtPostCodeNew.Text = "" Then
                err = "กรุณาตรวจสอย รหัสไปรษณีย์ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Exit Sub
            End If

            Dim objCo As New clsRequestContract

            'BrCode, BrName, Addr, SubDistrict, District, Province, PostCode, Tel, Contract, Remark, CreateDate, Createby

            If objCo.Addbranch(txtBranchCodeNew.Text, txtBranchCodeNew.Text, txtAddr.Text, txtSubDistrictNew.Text, txtDistrictNew.Text, txtProvinceNew.Text _
                    , txtPostCodeNew.Text, txtTelNew.Text, "", txtRemarkNew.Text, usercode) = False Then
                Exit Sub
            End If

            txtBranchCodeNew.Text = ""
            txtAddr.Text = ""
            txtSubDistrictNew.Text = ""
            txtDistrictNew.Text = ""
            txtProvinceNew.Text = ""
            txtPostCodeNew.Text = ""
            txtTelNew.Text = ""
            txtRemarkNew.Text = ""


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAddPerCon_Click(sender As Object, e As EventArgs) Handles btnAddPerCon.Click
        Try
            If AddRequest() = False Then
                Exit Sub
            End If

            If AddContractPersonal() = False Then
                Exit Sub
            End If

            If loadContractPer() = False Then
                Exit Sub
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try


    End Sub

    Private Sub btnAddCompanyPer_Click(sender As Object, e As EventArgs) Handles btnAddCompanyPer.Click
        Try
            If AddContractCompany() = False Then
                Exit Sub
            End If

            If loadContractCompany() = False Then
                Exit Sub
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try


    End Sub

    Private Sub txtContractBeginDate_TextChanged(sender As Object, e As EventArgs) Handles txtContractBeginDate.TextChanged
        Try

            txtcontractPeriod.Text = DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtContractBeginDate.Text), DateTime.Parse(txtContractEndDate.Text))

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAddcontractFix_Click(sender As Object, e As EventArgs) Handles btnAddcontractFix.Click
        Try


            If AddContractFix() = False Then
                Exit Sub
            End If



        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAddPayment_Click(sender As Object, e As EventArgs) Handles btnAddPayment.Click
        Try


            If AddContractPayment() = False Then
                Exit Sub
            End If



        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class