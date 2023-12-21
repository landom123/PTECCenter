Imports System.Globalization
Imports System.IO
Imports System.Windows.Controls
Imports DocumentFormat.OpenXml.Bibliography
Imports DocumentFormat.OpenXml.Spreadsheet

'Imports System.Drawing

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports DocumentFormat.OpenXml.ExtendedProperties
Imports System.Data.SqlTypes
Imports System.Web.DynamicData
'Imports DocumentFormat.OpenXml.Drawing.Charts
'Imports DocumentFormat.OpenXml.Wordprocessing
Imports QRCoder.PayloadGenerator.SwissQrCode
Imports System.ComponentModel.Design
Imports System.Reflection
Imports DocumentFormat.OpenXml.Wordprocessing
Imports DocumentFormat.OpenXml.Office.CustomUI
Imports DocumentFormat.OpenXml.Office2010.CustomUI

Public Class requestcontract2
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, assetsno, contractno, projectno As String
    'Public clientid As Double = 0
    Public DocID As Integer

    Dim dtBranch As New DataTable
    Dim dtStatus As New DataTable
    Dim dtCompany As New DataTable

    Dim iDocIDOr As Integer

    'Dim sMode As String = "NEW"

    Public clienttable As DataTable = create()

    Dim objCo As New clsRequestContract

    Dim objPay As New clsPayment

    Dim bByte1(0) As Byte
    Dim bByte2(0) As Byte

    Dim sMode As String = "NEW"

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
                'btnDel.Enabled = False

            Case "ยืนยัน(รออนุมัติ)", "ยกเลิก", "อนุมัติ", "Reject"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnSave.Enabled = False
                'btnNewAgree.Enabled = True
                btnNew.Enabled = False
                'btnDel.Enabled = False
        End Select

        If usercode = "JTW" Then
            btnApprove.Enabled = True
        Else
            btnApprove.Enabled = False
        End If

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
        cboMainContact.SelectedIndex = 0
        cboContractType.SelectedIndex = 0
        cboCompany.SelectedIndex = 0

        txtContractNo.Text = 0
        txtContractBeginDate.Text = ""
        txtContractEndDate.Text = ""
        txtcontractPeriod.Text = 0
        txtcontractPeriod2.Text = 0
        txtRentalReg.Text = 0


        'personal
        txtCustCode.Text = ""
        txtName.Text = ""
        txtCardID.Text = ""
        txtTaxID.Text = ""
        cboSex.SelectedIndex = 0
        txtAddress.Text = ""
        txtSubDistrict.Text = ""
        txtDistrict.Text = ""
        txtProvince.Text = ""
        txtPostcode.Text = ""
        txtPerTel.Text = ""
        txtPerLine.Text = ""
        txtPerEmail.Text = ""

        txtSendAddr.Text = ""
        txtSendSubdistrict.Text = ""
        txtSendDistrict.Text = ""
        txtSendProvince.Text = ""
        txtSendPostCode.Text = ""
        txtSendTel.Text = ""
        txtContractPer.Text = ""
        TxtRelationPer.Text = ""
        txtContractTelPer.Text = ""
        gvContractPer.DataSource = Nothing
        gvContractPer.DataBind()

        'company
        txtCompany.Text = ""
        txtAddrCom.Text = ""
        txtSubDistrictCom.Text = ""
        txtDistrictCom.Text = ""
        txtProvinceCom.Text = ""
        txtPostCodeCom.Text = ""
        txtMobile.Text = ""

        txtCustNameComPer.Text = ""
        txtCardIDComPer.Text = ""
        txtTaxIDComPer.Text = ""
        cboGenderComPer.SelectedIndex = 0
        txtAddrComPer.Text = ""
        txtSubDistrictComPer.Text = ""
        txtDistrictComPer.Text = ""
        txtProvinceComPer.Text = ""
        txtPostCodeComPer.Text = ""
        txtTel.Text = ""
        txtLine.Text = ""
        txtEmail.Text = ""

        txtComAddrSend.Text = ""
        txtComSubdistrictSend.Text = ""
        txtComDistrictSend.Text = ""
        txtComProvinceSend.Text = ""
        txtComPostCodeSend.Text = ""
        txtComTelSend.Text = ""
        txtComContractCom.Text = ""
        txtComRelation.Text = ""
        txtComTelCom.Text = ""
        gvContractCompanyPer.DataSource = Nothing
        gvContractCompanyPer.DataBind()

        'condition
        'cboPayType.SelectedIndex = 0
        txtAmountFix.Text = ""
        txtFrequencyFix.Text = ""
        txtDueDateFix.Text = ""
        txtBeginDateFix.Text = ""
        txtEndDateFix.Text = ""

        cboContractLand.SelectedIndex = 0
        cboContractBu.SelectedIndex = 0
        cboContractDayRent.SelectedIndex = 0
        gvContractFix.DataSource = Nothing
        gvContractFix.DataBind()

        'payment
        cboBank.SelectedIndex = 0
        txtBankbranchcode.Text = ""
        txtBankbranchname.Text = ""
        txtAccountNo.Text = ""
        txtAccountName.Text = ""

        cboPayCust.SelectedIndex = 0
        cboTaxCust.SelectedIndex = 0
        txtACCode.Text = ""
        gvPayment.DataSource = Nothing
        gvPayment.DataBind()


        'asset
        txtAssetsNo.Text = ""
        cboAssetType.SelectedIndex = 0
        txtLandno.Text = ""
        txtLandNo2.Text = ""
        txtSurveyNo.Text = ""
        txtAssetAddr.Text = ""
        txtAssetSubdistrict.Text = ""
        txtAssetDistrict.Text = ""
        txtAssetProvince.Text = ""
        txtAssPostCode.Text = ""
        txtRai.Text = 0
        txtNgan.Text = 0
        txtWa.Text = "0.00"
        txtRoadTo.Text = 0
        txtAssetDocno.Text = 0
        txtAssetSurvey.Text = 0
        txtRai1.Text = 0
        txtNgan1.Text = 0
        txtWa1.Text = "0.00"
        txtbuNo.Text = ""
        txtRai2.Text = 0
        txtNgan2.Text = 0
        txtWa2.Text = "0.00"
        txtAssetRemark.Text = ""
        txtAssetRemark2.Text = ""

        gvAsset.DataSource = Nothing
        gvAsset.DataBind()

        'PowerBook
        cboCompany.SelectedIndex = 0
        cboContractType.SelectedIndex = 0
        txtIssueDateBook.Text = ""
        txtDocDateBook.Text = ""
        txtEmpfr.Text = ""
        txtEmpto.Text = ""
        txtWitness1.Text = ""
        txtWitness2.Text = ""
        txtObj1.Text = ""
        txtObj2.Text = ""
        txtObj3.Text = ""
        txtOth1.Text = ""
        txtOth2.Text = ""
        txtOth3.Text = ""
        txtEmpto2.Text = ""
        txtEmpto3.Text = ""
        txtBrCodePowerBook.Text = ""
        txtAddrPowerBook.Text = ""
        txtContactPowerBook.Text = ""

        'contract Other

        cboCompany.SelectedIndex =
        lblCompany4.Text = ""
        cboContractType.SelectedIndex = 0
        lblContractType4.Text = ""
        txtCustomerparty.Text = ""
        txtCustomerparty1.Text = ""
        txtCustomerparty2.Text = ""
        txtAddr4.Text = ""
        txtContact4.Text = ""
        txtBranchContract4.Text = ""
        txtAmount4.Text = ""
        txtPeriod.Text = ""
        txtStartDate.Text = ""
        txtBeginDate4.Text = ""
        txtFineAmnt4.Text = ""
        txtPerformanceIns.Text = ""
        txtConditionIns.Text = ""
        txtConditionAmnt.Text = ""
        txtRemark4.Text = ""
        txtRemark41.Text = ""
        txtRemark42.Text = ""
        txtRemark43.Text = ""
        txtEndDate4.Text = ""
        chkEndDate4.Checked = False
        txtDueDate4.Text = ""
        chkDueDate4.Checked = False
        txtPayInsuranceOth.Text = 0
        txtPayBeforeOth.Text = 0
        txtPayFineOth.Text = 0
        txtPayWaterOth.Text = 0
        txtPayElectricOth.Text = 0
        txtPayCenterOth.Text = 0
        txtRoomNumOth.Text = 0
        txtAddrOther.Text = ""

        txtDueDateOth.Text = ""
        txtRentPerMonthOth.Text = 0

        'contractnonOil
        lblContractTypeNonOil.Text = ""
        rdoMonthNonOil.Checked = False
        rdoYearNonOil.Checked = False
        rdoOnceNonOil.Checked = False
        rdoFreeNonOil.Checked = False
        txtFrequencyNonOil.Text = ""
        txtDueDateNonOil.Text = ""
        txtBeginDateNonOil.Text = ""
        txtEndDateNonOil.Text = ""
        txtSizeNonOil.Text = ""
        txtRoomNumberNonOil.Text = ""
        txtBusinessTypeNonOil.Text = ""
        txtCustNameNonOil.Text = ""
        txtIDCardNonOil.Text = ""
        txtTaxIDNonOil.Text = ""
        txtHomeIDNonOil.Text = ""
        txtSubDistrictNonOil.Text = ""
        txtDistrictNonOil.Text = ""
        txtProvinceNonOil.Text = ""
        txtPostCodeNonOil.Text = ""
        txtContactNonOil.Text = ""
        txtTelNonOil.Text = ""
        txtLineNonOil.Text = ""
        txtEmailNonOil.Text = ""
        txtPayNonOilService.Text = 0
        txtPayNonOilInsurance.Text = 0
        txtPayNonOilWater.Text = 0
        txtPqyNonOilElectrice.Text = 0
        txtPayNonOilCenter.Text = 0
        txtPayNonOilGarbag.Text = 0
        txtPayNonOilLabel.Text = 0
        txtPayNonOilRemarkOther.Text = 0
        txtNonOilRemarkOther.Text = ""
        txtNonOilRemarkOther.Text = ""
        Session("ItemNoNonOil") = 0

        'contractRentJoin
        txtLandno.Text = ""
        txtSurveyNo.Text = ""
        txtLandNo2.Text = ""
        txtAssetAddr.Text = ""
        txtAssetSubdistrict.Text = ""
        txtAssetDistrict.Text = ""
        txtAssetProvince.Text = ""
        txtAssPostCode.Text = ""
        txtRai.Text = 0
        txtNgan.Text = 0
        txtWa.Text = 0.00
        cboAssetType.SelectedIndex = 0
        Session("ItemRentJoin") = 0


        Session("ItemNo") = 0

        Session("ItemNo") = 0

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
        Dim ContractID, StatusID, GroupConID, PeriodYear, PeriodMonth, GroupConID_SubItem, CompanyID As Integer
        Dim dContractBegindate, dContractEndDate, CreateDate As Date

        CompanyID = cboCompany.SelectedValue
        Branch = cboBranch.SelectedValue
        ContractID = cboContractType.SelectedValue
        GroupConID = cboMainContact.SelectedValue
        GroupConID_SubItem = cboContractType.SelectedValue

        If IsDate(txtContractBeginDate.Text) = True Then
            dContractBegindate = DateAdd(DateInterval.Year, -543, CDate(txtContractBeginDate.Text))
        Else
            dContractBegindate = "1900-01-01"
        End If

        If IsDate(txtContractEndDate.Text) = True Then
            dContractEndDate = DateAdd(DateInterval.Year, -543, CDate(txtContractEndDate.Text))
        Else
            dContractEndDate = "1900-01-01"
        End If

        CustName = txtName.Text
        CardID = txtCardID.Text
        Gender = cboSex.Text
        Company = txtCompany.Text
        Mobile = txtMobile.Text
        Tel = txtTel.Text
        Email = txtEmail.Text
        Line = txtLine.Text
        'StatusID = cboStatus.SelectedValue
        StatusID = 1
        Address = txtAddress.Text
        SubDistrict = txtSubDistrict.Text
        District = txtDistrict.Text
        Province = txtProvince.Text
        PostCode = txtPostcode.Text
        CreateDate = Now.Date
        CreateBy = usercode
        PeriodYear = CInt(txtcontractPeriod.Text)
        PeriodMonth = CInt(txtcontractPeriod2.Text)
        DocuNo = txtdocuno.Text
        RegistryTo = txtRentalReg.Text
        contractno = txtContractNo.Text

        If txtDocAction.Text = "NEW" Then
            txtdocuno.Text = objReq.GetDocRun(1, 1)
            DocuNo = txtdocuno.Text
            '    result = objReq.AddRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
            '                        , District, Province, PostCode, CreateDate, CreateBy, PeriodYear, PeriodMonth, RegistryTo, contractno, GroupConID, GroupConID_SubItem, CompanyID)

            'ElseIf txtDocAction.Text = "EDIT" Then
            '    result = objReq.UpdateRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
            '                        , District, Province, PostCode, CreateDate, CreateBy, CInt(txtDocIDAction.Text), RegistryTo, contractno, GroupConID, GroupConID_SubItem, PeriodYear, PeriodMonth, CompanyID)
        End If

        result = objReq.AddRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, PeriodYear, PeriodMonth, RegistryTo, contractno, GroupConID, GroupConID_SubItem, CompanyID)

        'txtDocIDAction.Text = 0
        'txtDocAction.Text = "NEW"
        'Clear()

        If disableTab(cboMainContact.SelectedValue) = False Then
            Return result
        End If

        Return result
    End Function

    Private Function loadRequest(iDocID As Integer) As Boolean

        Try
            Dim objReq As New clsRequestContract

            Dim dt As New DataTable

            Session("sDocNo") = ""
            Session("Registryto") = ""

            Session("BeginDate") = ""
            Session("EndDate") = ""

            dt = objReq.LoadRequest(iDocID)
            Clear()
            For Each dr As DataRow In dt.Rows
                txtdocuno.Text = dr("DocuNo")
                iDocIDOr = dr("ID")
                txtDocIDAction.Text = dr("ID")
                cboBranch.SelectedValue = dr("Branch")

                cboCompany.SelectedValue = dr("CompanyID")

                'cboStatus.SelectedValue = dr("StatusID")
                'lblStatus.Text = cboStatus.SelectedItem.ToString

                cboMainContact.SelectedValue = dr("GroupConID")

                SetCboSubContact(cboContractType, cboMainContact.SelectedValue)

                cboContractType.SelectedValue = dr("GroupConID_SubItem")

                disableTab(cboMainContact.SelectedValue)

                'txtContractBeginDate.Text = CDate(dr("BeginDate"))
                'txtContractEndDate.Text = CDate(dr("EndDate"))

                If IsDate(dr("BeginDate")) = True Then
                    'txtContractBeginDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
                    txtContractBeginDate.Text = CDate(dr("BeginDate"))
                    Session("BeginDate") = txtContractBeginDate.Text
                Else
                    txtContractBeginDate.Text = ""
                    Session("BeginDate") = ""
                End If

                If IsDate(dr("EndDate")) = True Then
                    'txtContractEndDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("EndDate")))
                    txtContractEndDate.Text = CDate(dr("EndDate"))
                    Session("EndDate") = txtContractEndDate.Text
                Else
                    txtContractEndDate.Text = ""
                    Session("EndDate") = ""
                End If

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

                Session("PeriodYear") = txtcontractPeriod.Text
                Session("PeriodMonth") = txtcontractPeriod2.Text

                Session("Registryto") = dr("Registryto")
                txtRentalReg.Text = dr("Registryto")
                txtContractNo.Text = dr("ContractNo")

                lblStatus.Text = dr("StatusName")

                If dr("StatusID") = 4 Then
                    btnSave.Enabled = False
                    btnApprove.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnApprove.Enabled = True
                End If


                If (Session("positionid") = 1 Or Session("positionid") = 2 Or Session("positionid") = 3 Or Session("positionid") = 4) _
                    And dr("StatusID") <> 4 Then

                    btnApprove.Enabled = True
                Else
                    btnApprove.Enabled = False
                End If

                txtDocAction.Text = "EDIT"
            Next

            Session("sDocNo") = txtdocuno.Text

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

            If loadContractAsset() = False Then
                Return False
            End If

            If loadContractAssetMain() = False Then
                Return False
            End If
            If loadContractAssetSublet() = False Then
                Return False
            End If
            'If loadContractFix() = False Then
            '    Return False
            'End If

            If loadContractPayment() = False Then
                Return False
            End If

            If loadContractAtt4(iDocID) = False Then
                Return False
            End If

            If loadContractOth(txtdocuno.Text) = False Then
                Return False
            End If

            If loadContractPowerBook(txtdocuno.Text) = False Then
                Return False
            End If

            If loadContractRentJoin(txtdocuno.Text) = False Then
                Return False
            End If

            If loadAtt3() = False Then
                Return False
            End If

            If loadAtt3Add2() = False Then
                Return False
            End If

            If loadAtt4() = False Then
                Return False
            End If

            If loadContractNonOil() = False Then
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

            Dim DocuNo, Branch, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, Address, SubDistrict, District, Province, PostCode, CreateBy, RegistryTo, ContractNo As String
            Dim ContractID, StatusID, GroupConID, PeriodYear, PeriodMonth, GroupConID_SubItem, CompanyID As Integer
            Dim dContractBegindate, dContractEndDate, CreateDate As Date

            Dim err, scriptKey, javaScript As String

            'If txtName.Text = "" Then
            '    err = "กรุณาตรวจระบุ ชื่อคู่สัญญา "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtCardID.Text = "" Then
            '    err = "กรุณาตรวจสอย เลขที่บัตรประชาชน "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtTaxID.Text = "" Then
            '    err = "กรุณาตรวจสอย เลขที่ผู้เสียภาษี "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtAddress.Text = "" Then
            '    err = "กรุณาตรวจสอย ที่อยู่ "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtSubDistrict.Text = "" Then
            '    err = "กรุณาตรวจสอย ที่ยู่ตำบล "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtDistrict.Text = "" Then
            '    err = "กรุณาตรวจสอย ที่อยู่อำเภอ "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtProvince.Text = "" Then
            '    err = "กรุณาตรวจสอย ที่อยู่จังหวัด "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtPostcode.Text = "" Then
            '    err = "กรุณาตรวจสอย รหัสไปรษณีย์ "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            CompanyID = cboCompany.SelectedValue
            Branch = cboBranch.SelectedValue
            ContractID = cboContractType.SelectedValue
            GroupConID = cboMainContact.SelectedValue
            GroupConID_SubItem = cboContractType.SelectedValue
            'dContractBegindate = DateAdd(DateInterval.Year, -543, CDate(txtContractBeginDate.Text))
            'dContractEndDate = DateAdd(DateInterval.Year, -543, CDate(txtContractEndDate.Text))

            dContractBegindate = CDate(txtContractBeginDate.Text)
            dContractEndDate = CDate(txtContractEndDate.Text)

            CustName = txtName.Text
            CardID = txtCardID.Text
            Gender = cboSex.Text
            Company = txtCompany.Text
            Mobile = txtMobile.Text
            Tel = txtTel.Text
            Email = txtEmail.Text
            Line = txtLine.Text
            'StatusID = cboStatus.SelectedValue
            StatusID = 1
            Address = txtAddress.Text
            SubDistrict = txtSubDistrict.Text
            District = txtDistrict.Text
            Province = txtProvince.Text
            PostCode = txtPostcode.Text
            CreateDate = Now.Date
            CreateBy = usercode
            PeriodYear = CInt(txtcontractPeriod.Text)
            PeriodMonth = CInt(txtcontractPeriod2.Text)
            DocuNo = txtdocuno.Text
            RegistryTo = txtRentalReg.Text
            ContractNo = txtContractNo.Text

            If txtDocAction.Text = "NEW" Then
                txtdocuno.Text = objReq.GetDocRun(1, 1)
                DocuNo = txtdocuno.Text
                result = objReq.AddRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, PeriodYear, PeriodMonth, RegistryTo, ContractNo, GroupConID, GroupConID_SubItem, CompanyID)
            ElseIf txtDocAction.Text = "EDIT" Then
                result = objReq.UpdateRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, CInt(txtDocIDAction.Text), RegistryTo, ContractNo, GroupConID, GroupConID_SubItem, PeriodYear, PeriodMonth, CompanyID)
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
                err = "กรุณาตรวจสอบ เลขที่บัตรประชาชน "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtTaxID.Text = "" Then
                err = "กรุณาตรวจสอบ เลขที่ผู้เสียภาษี "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtAddress.Text = "" Then
                err = "กรุณาตรวจสอบ ที่อยู่ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtSubDistrict.Text = "" Then
                err = "กรุณาตรวจสอบ ที่ยู่ตำบล "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If txtDistrict.Text = "" Then
                err = "กรุณาตรวจสอบ ที่อยู่อำเภอ "
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

            If txtPostcode.Text = "" Then
                err = "กรุณาตรวจสอบ รหัสไปรษณีย์ "
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('Error')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Return False
            End If

            If objCo.AddContractPerson(txtdocuno.Text, 1, txtName.Text, txtCardID.Text, txtTaxID.Text, cboSex.SelectedValue, txtAddress.Text, txtSubDistrict.Text, txtDistrict.Text, txtProvince.Text _
                                       , txtPostcode.Text, txtTel.Text, txtLine.Text, txtEmail.Text, usercode, txtPerWitness1.Text, txtPerWitness2.Text) = False Then
                Return False
            End If

            If AddContractPersonalSend() = False Then
                Return False
            End If

            If AddContractPersonalContract() = False Then
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

    Private Function AddContractPersonalSend() As Boolean
        Try

            If objCo.AddContractPersonSendAddr(txtdocuno.Text, txtSendAddr.Text, txtSendSubdistrict.Text, txtSendDistrict.Text, txtSendProvince.Text _
                                       , txtSendPostCode.Text, txtSendTel.Text, usercode) = False Then
                Return False
            End If


            txtSendAddr.Text = ""
            txtSendSubdistrict.Text = ""
            txtSendDistrict.Text = ""
            txtSendProvince.Text = ""
            txtSendPostCode.Text = ""
            txtSendTel.Text = ""


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

    Private Function AddContractPersonalContract() As Boolean
        Try

            If objCo.AddContractPersonContract(txtdocuno.Text, txtContractPer.Text, TxtRelationPer.Text, txtContractTelPer.Text, usercode) = False Then
                Return False
            End If


            txtContractPer.Text = ""
            TxtRelationPer.Text = ""
            txtContractTelPer.Text = ""


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
            Dim dtSend As New DataTable
            Dim dtCon As New DataTable

            Dim iRow As Integer = 1

            dt = objCo.loadContractPersaonal(txtdocuno.Text)

            gvContractPer.DataSource = dt
            gvContractPer.DataBind()

            Session("Contact1") = ""
            Session("Contact2") = ""
            Session("Contact3") = ""
            Session("Contact4") = ""

            For Each dr As DataRow In dt.Rows
                Select Case iRow
                    Case 1
                        Session("Contact1") = dr("PersonName")
                    Case 2
                        Session("Contact2") = dr("PersonName")
                    Case 3
                        Session("Contact3") = dr("PersonName")
                    Case 4
                        Session("Contact4") = dr("PersonName")
                End Select
                iRow += 1
            Next


            dtSend = objCo.loadContractPersaonalSendAddr(txtdocuno.Text)

            For Each dr As DataRow In dtSend.Rows
                txtSendAddr.Text = dr("Addr")
                txtSendSubdistrict.Text = dr("SubDistrict")
                txtSendDistrict.Text = dr("District")
                txtSendProvince.Text = dr("Province")
                txtSendPostCode.Text = dr("PostCode")
                txtSendTel.Text = dr("Tel")
            Next


            dtCon = objCo.loadContractPersaonalContract(txtdocuno.Text)
            For Each dr As DataRow In dtCon.Rows
                txtContractPer.Text = dr("ContractName")
                TxtRelationPer.Text = dr("Relation")
                txtContractTelPer.Text = dr("Tel")

            Next

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

            'If txtProvince.Text = "" Then
            '    err = "กรุณาตรวจสอบ ที่อยู่จังหวัด "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtCustNameComPer.Text = "" Then
            '    err = "กรุณาตรวจสอบ ชื่อกรรมการ "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtAddrComPer.Text = "" Then
            '    err = "กรุณาตรวจสอบ ที่อยู่ "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtSubDistrictComPer.Text = "" Then
            '    err = "กรุณาตรวจสอบ ตำบล/แขวง "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtDistrictComPer.Text = "" Then
            '    err = "กรุณาตรวจสอบ อำเภอ/เขต "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtProvinceComPer.Text = "" Then
            '    err = "กรุณาตรวจสอบ จังหวัด "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            'If txtPostCodeComPer.Text = "" Then
            '    err = "กรุณาตรวจสอบ ไปรษณีย์ "
            '    scriptKey = "UniqueKeyForThisScript"
            '    javaScript = err ' "alertSuccess('Error')"
            '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '    Return False
            'End If

            If objCo.AddContractCompany(txtdocuno.Text, txtCompany.Text, txtAddrCom.Text, txtSubDistrictCom.Text, txtDistrictCom.Text, txtProvinceCom.Text, txtPostCodeCom.Text _
                                        , txtMobile.Text, txtCustNameComPer.Text, txtCardIDComPer.Text, txtTaxIDComPer.Text, txtAddrComPer.Text, txtSubDistrictComPer.Text _
                                        , txtDistrictComPer.Text, txtProvinceComPer.Text, txtPostCodeComPer.Text, txtTel.Text, txtLine.Text, txtEmail.Text, usercode _
                                        , txtComWitness1.Text, txtComWitness2.Text) = False Then
                Return False
            End If

            If AddContractCompanySend() = False Then
                Return False
            End If

            If AddContractCompanyContract() = False Then
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

    Private Function AddContractCompanySend() As Boolean
        Try

            If objCo.AddContractCompanySend(txtdocuno.Text, txtComAddrSend.Text, txtComSubdistrictSend.Text, txtComDistrictSend.Text, txtComProvinceSend.Text _
                                            , txtComPostCodeSend.Text, txtComTelSend.Text, usercode) = False Then
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

    Private Function AddContractCompanyContract() As Boolean
        Try

            If objCo.AddContractCompanyContract(txtdocuno.Text, txtComContractCom.Text, txtComRelation.Text, txtComTelCom.Text) = False Then
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
            Dim dtSend As DataTable
            Dim dtCom As DataTable

            dt = objCo.loadContractCompany(txtdocuno.Text)

            gvContractCompanyPer.DataSource = dt
            gvContractCompanyPer.DataBind()


            dtSend = objCo.loadContractCompanySend(txtdocuno.Text)

            For Each dr As DataRow In dtSend.Rows
                txtComAddrSend.Text = dr("Addr")
                txtComSubdistrictSend.Text = dr("SubDistrict")
                txtComDistrictSend.Text = dr("District")
                txtComProvinceSend.Text = dr("Province")
                txtComPostCodeSend.Text = dr("PostCode")
                txtComTelSend.Text = dr("Tel")
            Next


            dtCom = objCo.loadContractCompanyContract(txtdocuno.Text)
            For Each dr As DataRow In dtCom.Rows
                txtComContractCom.Text = dr("ContractName")
                txtComRelation.Text = dr("Relation")
                txtComTelCom.Text = dr("Tel")

            Next


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

        obj.DataSource = objCo.loadPayType(cboMainContact.SelectedValue)
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

            If rdoMonthFix.Checked = False And rdoYearFix.Checked = False And rdoOnceFix.Checked = False Then
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
            iDueType = 3

            'dBeginDateFix = DateAdd(DateInterval.Year, -543, Date.Parse(txtBeginDateFix.Text))
            'dEndDateFix = DateAdd(DateInterval.Year, -543, Date.Parse(txtEndDateFix.Text))
            'dDueDateFix = DateAdd(DateInterval.Year, -543, Date.Parse(txtDueDateFix.Text))

            dBeginDateFix = Date.Parse(txtBeginDateFix.Text)
            dEndDateFix = Date.Parse(txtEndDateFix.Text)
            dDueDateFix = Date.Parse(txtDueDateFix.Text)

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
            Dim dt1 As New DataTable
            Dim dt2 As New DataTable

            dt = objCo.loadContractFixiblePayment(txtdocuno.Text)

            gvContractFix.DataSource = dt
            gvContractFix.DataBind()

            dt1 = objCo.loadContractFixView(txtdocuno.Text)
            gvContractFix1.DataSource = dt1
            gvContractFix1.DataBind()

            'If dt.Rows.Count > 0 Then
            '    Try
            '        dt1 = dt.Select("PayTypeID=5", "ID DESC").CopyToDataTable()
            '        gvContractFix1.DataSource = dt1
            '        gvContractFix1.DataBind()
            '    Catch ex As Exception

            '    End Try

            '    Try
            '        dt2 = dt.Select("PayTypeID=6", "ID DESC").CopyToDataTable()
            '        gvContractFix2.DataSource = dt2
            '        gvContractFix2.DataBind()
            '    Catch ex As Exception

            '    End Try

            'End If


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

    Private Function loadContractAsset() As Boolean
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim iRow As Integer = 0

            dt = objCo.loadAsset(txtdocuno.Text)

            gvAsset.DataSource = dt
            gvAsset.DataBind()

            dt2 = objCo.loadAsset2(txtdocuno.Text)

            For Each dr As DataRow In dt2.Rows
                Select Case iRow
                    Case 0
                        txtAtt2DocNo1.Text = dr("DocNo")
                        txtAtt2No1.Text = dr("ServayNo")
                        txtAtt2RoadTo.Text = dr("LongRoad")
                        txtAtt2Rai1.Text = dr("Rai")
                        txtAtt2Ngan1.Text = dr("Ngan")
                        txtAtt2Wa1.Text = dr("Wa")
                        txtAtt2Remark1.Text = dr("Remark")
                        txtAtt2Remark11.Text = dr("Remark1")

                        txtAtt2Rai11.Text = dr("RaiS")
                        txtAtt2Ngan11.Text = dr("NganS")
                        txtAtt2Wa11.Text = dr("WaS")

                        txtAtt2Rai12.Text = "0"
                        txtAtt2Ngan12.Text = "0"
                        txtAtt2Wa12.Text = "0.00"

                        txtAtt2Rai13.Text = "0"
                        txtAtt2Ngan13.Text = "0"
                        txtAtt2Wa13.Text = "0.00"

                        txtAtt2Rai14.Text = "0"
                        txtAtt2Ngan14.Text = "0"
                        txtAtt2Wa14.Text = "0.00"

                        If IIf(IsDBNull(dr("pic1")), "", dr("pic1")) <> "" Then
                            image3.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(dr("pic1"))
                        End If
                        If IIf(IsDBNull(dr("pic1")), "", dr("pic2")) <> "" Then
                            image4.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(dr("pic2"))
                        End If


                    Case 1
                        txtAtt2DocNo2.Text = dr("DocNo")
                        txtAtt2No2.Text = dr("ServayNo")
                        txtAtt2RoadTo2.Text = dr("LongRoad")
                        txtAtt2Rai2.Text = dr("Rai")
                        txtAtt2Ngan2.Text = dr("Ngan")
                        txtAtt2Wa2.Text = dr("Wa")
                        txtAtt2Remark2.Text = dr("Remark")
                        txtAtt2Remark22.Text = dr("Remark1")

                        txtAtt2Rai21.Text = dr("RaiS")
                        txtAtt2Ngan21.Text = dr("NganS")
                        txtAtt2Wa21.Text = dr("WaS")

                        txtAtt2Rai22.Text = "0"
                        txtAtt2Ngan22.Text = "0"
                        txtAtt2Wa22.Text = "0.00"

                        txtAtt2Rai23.Text = "0"
                        txtAtt2Ngan23.Text = "0"
                        txtAtt2Wa23.Text = "0.00"

                        txtAtt2Rai24.Text = "0"
                        txtAtt2Ngan24.Text = "0"
                        txtAtt2Wa24.Text = "0.00"


                        If IIf(IsDBNull(dr("pic1")), "", dr("pic1")) <> "" Then
                            image5.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(dr("pic1"))
                        End If
                        If IIf(IsDBNull(dr("pic1")), "", dr("pic2")) <> "" Then
                            image6.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(dr("pic2"))
                        End If
                End Select
                iRow += 1
            Next


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

    Private Function loadContractAssetMain() As Boolean
        Try
            Dim dt As New DataTable
            Dim iRow As Integer = 0

            dt = objCo.loadAssetMain(txtdocuno.Text)

            gvAssetMain.DataSource = dt
            gvAssetMain.DataBind()

            'For Each dr As DataRow In dt.Rows
            '    Select Case CInt(dr("ItemNo"))
            '        Case 0

            '        Case 1
            '            txtAtt2DocNo2.Text = dr("Wa")
            '            txtAtt2No2.Text = dr("SurveyNo")

            '            txtAtt2Rai12.Text = dr("Rai")
            '            txtAtt2Ngan12.Text = dr("Ngan")
            '            txtAtt2Wa12.Text = dr("Wa")

            '            txtAtt2Rai22.Text = dr("Rai")
            '            txtAtt2Ngan22.Text = dr("Ngan")
            '            txtAtt2Wa22.Text = dr("Ngan")
            '        Case 2
            '            txtAtt2Rai13.Text = dr("Rai")
            '            txtAtt2Ngan13.Text = dr("Ngan")
            '            txtAtt2Wa13.Text = dr("Wa")


            '            txtAtt2Rai23.Text = dr("Rai")
            '            txtAtt2Ngan23.Text = dr("Ngan")
            '            txtAtt2Wa23.Text = dr("Wa")

            '        Case 3
            '            txtAtt2Rai14.Text = dr("Rai")
            '            txtAtt2Ngan14.Text = dr("Ngan")
            '            txtAtt2Wa14.Text = dr("Wa")

            '            txtAtt2Rai24.Text = dr("Rai")
            '            txtAtt2Ngan24.Text = dr("Ngan")
            '            txtAtt2Wa24.Text = dr("Wa")
            '        Case 99

            '    End Select
            '    iRow += 1
            'Next

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

    Private Function loadContractAssetSublet() As Boolean
        Try
            Dim dt As New DataTable
            Dim iRow As Integer = 0

            dt = objCo.loadAssetSublet(txtdocuno.Text, txtAssetDocno.Text)

            gvSublet.DataSource = dt
            gvSublet.DataBind()

            For Each dr As DataRow In dt.Rows
                Select Case CInt(dr("ItemNo"))
                    Case 0

                    Case 1
                        txtAtt2DocNo2.Text = dr("Wa")
                        txtAtt2No2.Text = dr("SurveyNo")

                        txtAtt2Rai12.Text = dr("Rai")
                        txtAtt2Ngan12.Text = dr("Ngan")
                        txtAtt2Wa12.Text = dr("Wa")

                        txtAtt2Rai22.Text = dr("Rai")
                        txtAtt2Ngan22.Text = dr("Ngan")
                        txtAtt2Wa22.Text = dr("Ngan")
                    Case 2
                        txtAtt2Rai13.Text = dr("Rai")
                        txtAtt2Ngan13.Text = dr("Ngan")
                        txtAtt2Wa13.Text = dr("Wa")

                        txtAtt2Rai23.Text = dr("Rai")
                        txtAtt2Ngan23.Text = dr("Ngan")
                        txtAtt2Wa23.Text = dr("Wa")

                    Case 3
                        txtAtt2Rai14.Text = dr("Rai")
                        txtAtt2Ngan14.Text = dr("Ngan")
                        txtAtt2Wa14.Text = dr("Wa")

                        txtAtt2Rai24.Text = dr("Rai")
                        txtAtt2Ngan24.Text = dr("Ngan")
                        txtAtt2Wa24.Text = dr("Wa")
                    Case 99

                End Select
                iRow += 1
            Next

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
            Dim iRow As Integer = 1

            dt = objCo.loadContractPayment(txtdocuno.Text)

            gvPayment.DataSource = dt
            gvPayment.DataBind()

            Session("AccName1") = ""
            Session("AccName2") = ""
            Session("AccName3") = ""
            Session("AccName4") = ""

            Session("BankName1") = ""
            Session("BranchName1") = ""
            Session("AccCode1") = ""
            Session("BankName2") = ""
            Session("BranchName2") = ""
            Session("AccCode2") = ""
            Session("BankName3") = ""
            Session("BranchName3") = ""
            Session("AccCode3") = ""
            Session("BankName4") = ""
            Session("BranchName4") = ""
            Session("AccCode4") = ""

            For Each dr As DataRow In dt.Rows
                Select Case iRow
                    Case 1
                        Session("AccName1") = dr("AccName")
                        Session("BankName1") = dr("BankName")
                        Session("BranchName1") = dr("BranchName")
                        Session("AccCode1") = dr("AccCode")
                    Case 2
                        Session("AccName2") = dr("AccName")
                        Session("BankName2") = dr("BankName")
                        Session("BranchName2") = dr("BranchName")
                        Session("AccCode2") = dr("AccCode")
                    Case 3
                        Session("AccName3") = dr("AccName")
                        Session("BankName3") = dr("BankName")
                        Session("BranchName3") = dr("BranchName")
                        Session("AccCode3") = dr("AccCode")
                    Case 4
                        Session("AccName4") = dr("AccName")
                        Session("BankName4") = dr("BankName")
                        Session("BranchName4") = dr("BranchName")
                        Session("AccCode4") = dr("AccCode")
                End Select
                iRow += 1
            Next

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

    Private Function loadContractAtt4(iID As Integer) As Boolean

        Try
            Dim dt As New DataTable

            'Dim numrows As Integer
            'Dim numcells As Integer
            'Dim i As Integer
            'Dim j As Integer
            'Dim r As TableRow
            'Dim c As TableCell

            dt = objCo.loadContractAtt4(iID)
            'numrows = dt.Rows.Count
            'numcells = dt.Columns.Count
            'For j = 0 To numrows - 1
            '    r = New TableRow()
            '    For i = 0 To numcells - 1
            '        c = New TableCell()
            '        c.Controls.Add(New LiteralControl(dt.Rows(j).Item(i).ToString))
            '        r.Cells.Add(c)
            '    Next i
            '    tblAtt4.Rows.Add(r)
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

    Private Sub SetCboAssetType(obj As Object)
        Dim asset As New ContractAssets

        obj.DataSource = asset.AssetsType_Cbo
        obj.DataValueField = "assetstypeid"
        obj.DataTextField = "assetstype"
        obj.DataBind()
    End Sub
    Private Sub SetCboMainContract(obj As Object)

        obj.DataSource = objCo.loadMainContract
        obj.DataValueField = "ID"
        obj.DataTextField = "MainName"
        obj.DataBind()
    End Sub

    Private Sub SetCboMainContact(obj As Object)

        obj.DataSource = objCo.loadMainContact
        obj.DataValueField = "ID"
        obj.DataTextField = "MainConName"
        obj.DataBind()
    End Sub

    Private Sub SetCboSubContact(obj As Object, iIndex As Integer)

        obj.DataSource = objCo.loadSubContact(iIndex)
        obj.DataValueField = "ItemNo"
        obj.DataTextField = "SubConName"
        obj.DataBind()
    End Sub
    Private Sub SetCboTaxpayGround(obj As Object)

        obj.DataSource = objCo.loadTaxPayGround
        obj.DataValueField = "ID"
        obj.DataTextField = "GroundName"
        obj.DataBind()
    End Sub
    Private Sub SetCboTaxpayBuild(obj As Object)

        obj.DataSource = objCo.loadTaxPayBuil
        obj.DataValueField = "ID"
        obj.DataTextField = "BuildName"
        obj.DataBind()
    End Sub

    Private Sub SetCboFrequency(obj As Object)

        obj.DataSource = objCo.loadFrequencyPayment
        obj.DataValueField = "ID"
        obj.DataTextField = "SeqName"
        obj.DataBind()
    End Sub

    Private Function convertByte2(oFile As FileUpload) As Byte
        Try
            'Select Case 1
            '    Case 1
            '        ''*** Read Binary Data ***'


            'Dim imbByte1(oFile.PostedFile.InputStream.Length) As Byte
            '        fiUpload.PostedFile.InputStream.Read(imbByte1, 0, imbByte1.Length)

            '        'Session(imbByte1) = imbByte1
            '    Case 2
            '        ''*** Read Binary Data ***'
            '        Dim imbByte2(oFile.PostedFile.InputStream.Length) As Byte
            '        fiUpload.PostedFile.InputStream.Read(imbByte2, 0, imbByte2.Length)

            '        'Session(imbByte2) = imbByte2
            'End Select

            'Dim imbByte(oFile.PostedFile.InputStream.Length) As Byte
            'Return oFile.PostedFile.InputStream.Read(imbByte, 0, imbByte.Length)


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Private Function convertByte(oFile As String) As Byte
        Try


            Dim rawData() As Byte
            Dim fs As FileStream
            Dim FileSize As UInt32

            'fs = New FileStream("c:\image.png", FileMode.Open, FileAccess.Read)
            fs = New FileStream(oFile, FileMode.Open, FileAccess.Read)
            FileSize = fs.Length

            rawData = New Byte(FileSize) {}
            fs.Read(rawData, 0, FileSize)
            'fs.Close()

            Return fs.Read(rawData, 0, FileSize)

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Private Function loadContractOth(sDocNo As String) As Boolean
        Try

            Dim dtCoOth As New DataTable

            dtCoOth = objCo.loadContractOth(sDocNo)

            For Each dr As DataRow In dtCoOth.Rows
                cboMainContact.SelectedValue = dr("GroupConID")

                SetCboSubContact(cboContractType, cboMainContact.SelectedValue)

                cboCompany.SelectedValue = dr("CompanyID")
                lblCompany4.Text = cboCompany.SelectedItem.Text

                cboContractType.SelectedValue = dr("GroupConID_SubItem")
                lblContractType4.Text = cboContractType.SelectedItem.Text

                txtCustomerparty.Text = dr("Customerparty")
                txtCustomerparty1.Text = dr("Customerparty1")
                txtCustomerparty2.Text = dr("Customerparty2")
                txtAddr4.Text = dr("Addr")
                txtContact4.Text = dr("Contact")
                txtBranchContract4.Text = dr("Branch")
                txtAmount4.Text = dr("Amount")
                txtPeriod.Text = dr("Period")


                'txtBeginDate4.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))

                If IsDate(dr("DocDate")) = True Then
                    'txtStartDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DocDate")))
                    txtStartDate.Text = CDate(dr("DocDate"))
                Else
                    txtStartDate.Text = ""
                End If

                If IsDate(dr("BeginDate")) = True Then
                    'txtBeginDate4.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
                    txtBeginDate4.Text = CDate(dr("BeginDate"))
                Else
                    txtBeginDate4.Text = ""
                End If

                txtFineAmnt4.Text = dr("FineAmnt")
                txtPerformanceIns.Text = dr("PerformanceIns")
                txtConditionIns.Text = dr("ConditionIns")
                txtConditionAmnt.Text = dr("ConditionAmnt")
                txtRemark4.Text = dr("Remark")
                txtRemark41.Text = dr("Remark1")
                txtRemark42.Text = dr("Remark2")
                txtRemark43.Text = dr("Remark3")
                'txtRemark44.text = dr("Remark4")

                If dr("chkEndDate") = 1 Then
                    chkEndDate4.Checked = True
                Else
                    chkEndDate4.Checked = False
                End If

                If IsDate(dr("EndDate")) = True Then
                    'txtEndDate4.Text = DateAdd(DateInterval.Year, 543, CDate(dr("EndDate")))
                    txtEndDate4.Text = CDate(dr("EndDate"))
                Else
                    txtEndDate4.Text = ""
                End If

                If dr("chkEndDate") = 1 Then
                    chkDueDate4.Checked = True
                Else
                    chkDueDate4.Checked = False
                End If

                If IsDate(dr("DueDate")) = True Then
                    'txtDueDate4.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DueDate")))
                    txtDueDate4.Text = CDate(dr("DueDate"))
                Else
                    txtDueDate4.Text = ""
                End If


                txtPayInsuranceOth.Text = FormatNumber(CDbl(dr("PayInsurance")), 2)
                txtPayBeforeOth.Text = FormatNumber(CDbl(dr("PayBefore")), 2)
                txtPayFineOth.Text = FormatNumber(CDbl(dr("PayFine")), 2)
                txtPayWaterOth.Text = FormatNumber(CDbl(dr("PayWater")), 2)
                txtPayElectricOth.Text = FormatNumber(CDbl(dr("PayElectrict")), 2)
                txtPayCenterOth.Text = FormatNumber(CDbl(dr("PayCenter")), 2)
                txtRoomNumOth.Text = CInt(dr("RoomNum"))
                txtAddrOther.Text = dr("AddrLocation")

                txtDueDateOth.Text = dr("DueDay")
                txtRentPerMonthOth.Text = FormatNumber(CDbl(dr("RentPerMonth")), 2)

                If disableTab(cboMainContact.SelectedValue) = False Then
                    Return False
                End If

            Next

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

    Private Function loadContractPowerBook(sDocNo As String) As Boolean
        Try

            Dim dtCoOth As New DataTable

            dtCoOth = objCo.loadContractPowerBook(sDocNo)

            For Each dr As DataRow In dtCoOth.Rows
                cboMainContact.SelectedValue = dr("GroupConID")

                SetCboSubContact(cboContractType, cboMainContact.SelectedValue)

                cboCompany.SelectedValue = dr("CompanyID")
                cboContractType.SelectedValue = dr("ContractTypeID")
                lblContractBook.Text = cboContractType.SelectedItem.Text

                'txtIssueDateBook.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DueDate")))
                'txtDocDateBook.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DocDate")))

                If IsDate(dr("DueDate")) = True Then
                    'txtIssueDateBook.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DueDate")))
                    txtIssueDateBook.Text = CDate(dr("DueDate"))
                Else
                    txtIssueDateBook.Text = ""
                End If

                If IsDate(dr("DocDate")) = True Then
                    'txtDocDateBook.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DocDate")))
                    txtDocDateBook.Text = CDate(dr("DocDate"))
                Else
                    txtDocDateBook.Text = ""
                End If

                txtEmpfr.Text = dr("Empfr")
                txtEmpto.Text = dr("Empto")
                txtEmpto2.Text = dr("Empto2")
                txtEmpto3.Text = dr("Empto3")
                txtWitness1.Text = dr("Witness1")
                txtWitness2.Text = dr("Witness2")
                txtObj1.Text = dr("Obj1")
                txtObj2.Text = dr("Obj2")
                txtObj3.Text = dr("Obj3")
                txtOth1.Text = dr("Oth1")
                txtOth2.Text = dr("Oth2")
                txtOth3.Text = dr("Oth3")
                txtContactPowerBook.Text = dr("Contact")
                txtBrCodePowerBook.Text = dr("BrCode")
                txtAddrPowerBook.Text = dr("Addr")
                'CreateBy = usercode
                Session("ItemNo") = dr("ItemNo")

                If disableTab(cboMainContact.SelectedValue) = False Then
                    Return False
                End If

            Next

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

    Private Function loadContractRentJoin(sDocNo As String) As Boolean
        Try

            Dim dtCoRent As New DataTable

            dtCoRent = objCo.loadContractRentJoin(sDocNo)

            For Each dr As DataRow In dtCoRent.Rows
                cboMainContact.SelectedValue = dr("GroupConID")

                SetCboSubContact(cboContractType, cboMainContact.SelectedValue)

                cboCompany.SelectedValue = dr("CompanyID")
                cboContractType.SelectedValue = dr("ContractTypeID")
                lblContractTypeRentJoin.Text = cboContractType.SelectedItem.Text

                'txtDocDateRentJoin.Text = DateAdd(DateInterval.Year, +543, CDate(dr("DocDate")))
                'txtBeginDateRentJoin.Text = DateAdd(DateInterval.Year, +543, CDate(dr("BeginDate")))
                'txtEndDateRentJoin.Text = DateAdd(DateInterval.Year, +543, CDate(dr("EndDate")))

                txtDocDateRentJoin.Text = CDate(dr("DocDate"))
                txtBeginDateRentJoin.Text = CDate(dr("BeginDate"))
                txtEndDateRentJoin.Text = CDate(dr("EndDate"))


                txtVendorContract1.Text = dr("VendorContract1")
                txtVendorContract2.Text = dr("VendorContract2")
                txtVendorContract3.Text = dr("VendorContract3")
                txtVendorSign1.Text = dr("VendorSign1")
                txtVendorSign2.Text = dr("VendorSign2")
                txtVendorSign3.Text = dr("VendorSign3")
                txtWitnessRentJoin.Text = dr("Witness")
                txtAddrRentJoin.Text = dr("Addr")
                txtContactRentJoin.Text = dr("Contact")
                txtBranchRentJoin.Text = dr("Branch")
                txtDuePayRentJoin.Text = dr("DuePay")
                txtRentAreaRentJoin1.Text = dr("RentArea1")
                txtRentAreaRentJoin2.Text = dr("RentArea2")
                txtRentAreaRentJoin3.Text = dr("RentArea3")
                txtRentAreaRentJoin4.Text = dr("RentArea4")
                txtOthRentJoin1.Text = dr("Oth1")
                txtOthRentJoin2.Text = dr("Oth2")
                txtOthRentJoin3.Text = dr("Oth3")
                txtOthRentJoin4.Text = dr("Oth4")

                If disableTab(cboMainContact.SelectedValue) = False Then
                    Return False
                End If

            Next

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

    Private Function loadAtt3() As Boolean
        Try

            Dim dt As New DataTable

            dt = objCo.loadAtt3(txtdocuno.Text)

            gvAtt3Add1.DataSource = dt
            gvAtt3Add1.DataBind()

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

    Private Function loadAtt3Add2() As Boolean
        Try

            Dim dt As New DataTable

            dt = objCo.loadAtt3Add2(txtdocuno.Text)

            gvAtt3Add2.DataSource = dt
            gvAtt3Add2.DataBind()

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

    Private Function loadAtt4() As Boolean
        Try

            Dim dt As New DataTable

            dt = objCo.loadAtt4(txtdocuno.Text)

            gvAtt4.DataSource = dt
            gvAtt4.DataBind()

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
    Private Function loadContractNonOil() As Boolean
        Try

            Dim dt As New DataTable

            dt = objCo.loadContractNonOil(txtdocuno.Text, 0)
            'gvPayTypeNonOil.DataSource = dt
            'gvPayTypeNonOil.DataBind()

            For Each dr As DataRow In dt.Rows
                lblContractTypeNonOil.Text = dr("ContractTypeName")
                'txtAmountNonOil.Text = FormatNumber(CDbl(dr("Amnt")), 2)
                txtFrequencyNonOil.Text = dr("Frequency")
                txtSizeNonOil.Text = dr("Size")
                txtRoomNumberNonOil.Text = dr("RoomNum")
                txtBusinessTypeNonOil.Text = dr("BusinessType")
                'txtCustCodeNonOil.Text = dr("CustCode")
                txtCustNameNonOil.Text = dr("CustName")
                txtIDCardNonOil.Text = dr("CardID")
                txtTaxIDNonOil.Text = dr("TaxID")
                txtHomeIDNonOil.Text = dr("Addr")
                txtSubDistrictNonOil.Text = dr("SubDistrict")
                txtDistrictNonOil.Text = dr("District")
                txtProvinceNonOil.Text = dr("Province")
                txtPostCodeNonOil.Text = dr("PostCode")
                txtContactNonOil.Text = dr("Contact")
                txtTelNonOil.Text = dr("Tel")
                txtLineNonOil.Text = dr("Line")
                txtEmailNonOil.Text = dr("Email")

                txtPayNonOilService.Text = dr("PayService")
                txtPayNonOilInsurance.Text = dr("PayInsurance")
                txtPayNonOilCenter.Text = dr("PayCenter")
                txtPayNonOilGarbag.Text = dr("PayGarbag")
                txtPayNonOilWater.Text = dr("PayWater")
                txtPqyNonOilElectrice.Text = dr("PayElectrict")
                txtPayNonOilLabel.Text = dr("PayLabel")
                txtPayNonOilRemarkOther.Text = dr("PayOther")
                'txtNonOilRemarkOther.Text = dr("Remark")

                'cboPayTypeNonOil.SelectedValue = dr("PayTypeID")

                Select Case dr("PayRound")
                    Case 1
                        rdoMonthNonOil.Checked = True
                    Case 2
                        rdoYearNonOil.Checked = True
                    Case 3
                        rdoOnceNonOil.Checked = True
                    Case 4
                        rdoFreeNonOil.Checked = True
                End Select

                'txtDueDateNonOil.Text = DateAdd(DateInterval.Year, 543, CDate(dr("DueDate")))
                'txtBeginDateNonOil.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
                'txtEndDateNonOil.Text = DateAdd(DateInterval.Year, 543, CDate(dr("EndDate")))

                txtDueDateNonOil.Text = CDate(dr("DueDate"))
                txtBeginDateNonOil.Text = CDate(dr("BeginDate"))
                txtEndDateNonOil.Text = CDate(dr("EndDate"))

            Next

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
            'contractno = Request.QueryString("contractno")
            'assetsno = Request.QueryString("assetsno")


            'Session("contractno") = contractno
            'Session("assetsno") = assetsno

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'If Not String.IsNullOrEmpty(assetsno) Then
            '    FindData(assetsno)
            'Else
            '    txtdocuno.Text = contractno
            '    SetButton("NEW")
            'End If

            If Not Request.QueryString("DocID") Is Nothing Then
                'objpolicy.setComboPolicyByJobTypeID(cboPolicy, cboJobType.SelectedItem.Value)
                'txtDocDate.Text = Now()
                Session("iDocID") = Request.QueryString("DocID")
                'txtJobno.Text = Session("jobno")
                'FindJobNo(txtJobno.Text)

            Else
                'If Not Session("branchid") Is Nothing Then
                '    cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid")))
                '    If cboJobType.SelectedItem.Value = 1 Then
                '        FindPositionInPump(cboBranch.SelectedItem.Value)
                '    End If
                'End If
                'Session("status") = "new"
                'objStatus = "new"
                'txtCreateBy.Text = Session("username")
                'txtCreateDate.Text = Now()
            End If

            btnApprove.Enabled = False

            SetCboContractLand(cboContractLand)
            SetCboContractBuild(cboContractBu)
            SetCboContractDayRent(cboContractDayRent)

            SetCboAssetType(cboAssetType)

            SetCboMainContract(cboMainContract)
            SetCboTaxpayGround(cboTaxpayground)
            SetCboTaxpayBuild(cboTaxpaybuild)

            SetCboMainContact(cboMainContact)
            SetCboSubContact(cboContractType, 1)
            'SetCboFrequency(cboFrequency)

            If loadBank() = False Then
                Exit Sub
            End If

            SetCboPaymentType(cboPayType)
            'SetCboPaymentType(cboPayTypeNonOil)

            Dim objprj As New Project
            dtBranch = objprj.loadBranch
            cboBranch.DataSource = dtBranch
            cboBranch.DataValueField = "BrCode"
            cboBranch.DataTextField = "Brname"
            cboBranch.DataBind()

            dtCompany = objCo.loadCompany
            cboCompany.DataSource = dtCompany
            cboCompany.DataValueField = "ID"
            cboCompany.DataTextField = "ComName"
            cboCompany.DataBind()

            Session("ItemNo") = 0

            cboSex.Items.Add("Male")
            cboSex.Items.Add("Female")
            cboSex.Items.Add("Not Specified")

            cboGenderComPer.Items.Add("Male")
            cboGenderComPer.Items.Add("Female")
            cboGenderComPer.Items.Add("Not Specified")

            'txtdocuno.Text = Request.QueryString("agreeno")
            Dim objReq As New clsRequestContract

            txtdocuno.Text = objReq.GetDocRun(1, 0)

            DocID = Session("iDocID")
            If loadRequest(DocID) = False Then
                Exit Sub
            End If

            disableTab(cboMainContact.SelectedValue)

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
        'projectno = Session("requestcontract.aspx")
        'Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
        Response.Redirect("requestcontract.aspx")
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
            Dim index As Integer = gvContractPer.SelectedRow.RowIndex
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
            txtPerWitness1.Text = gvContractPer.SelectedRow.Cells(13).Text
            txtPerWitness2.Text = gvContractPer.SelectedRow.Cells(14).Text

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
            Dim index As Integer = gvContractCompanyPer.SelectedRow.RowIndex
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
            txtComWitness1.Text = gvContractCompanyPer.SelectedRow.Cells(20).Text
            txtComWitness2.Text = gvContractCompanyPer.SelectedRow.Cells(21).Text

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
            Dim index As Integer = gvContractFix.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvContractFix.SelectedRow.Cells(7).Text)
            Dim iItemNo As Integer = CInt(gvContractFix.SelectedRow.Cells(8).Text)

            txtFrequencyFix.Text = gvContractFix.SelectedRow.Cells(2).Text
            'txtDueDateFix.Text = DateAdd(DateInterval.Year, 543, Date.Parse(gvContractFix.SelectedRow.Cells(3).Text))
            'txtBeginDateFix.Text = DateAdd(DateInterval.Year, 543, Date.Parse(gvContractFix.SelectedRow.Cells(4).Text))
            'txtEndDateFix.Text = DateAdd(DateInterval.Year, 543, Date.Parse(gvContractFix.SelectedRow.Cells(5).Text))

            txtDueDateFix.Text = Date.Parse(gvContractFix.SelectedRow.Cells(3).Text)
            txtBeginDateFix.Text = Date.Parse(gvContractFix.SelectedRow.Cells(4).Text)
            txtEndDateFix.Text = Date.Parse(gvContractFix.SelectedRow.Cells(5).Text)

            txtAmountFix.Text = CDbl(gvContractFix.SelectedRow.Cells(6).Text)

            'DateAdd(DateInterval.Year, -543, Date.Parse(txtEndDateFix.Text))

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

    'Protected Sub OnRowDataBound_AssetMain(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvAssetMain, "Select$" & e.Row.RowIndex)
    '            e.Row.Attributes("style") = "cursor:pointer"
    '        End If
    '    Catch ex As Exception
    '        Dim err, scriptKey, javaScript As String
    '        err = ex.Message
    '        scriptKey = "UniqueKeyForThisScript"
    '        javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '    End Try

    'End Sub

    Protected Sub OnRowDataBound_AssetMain(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvAssetMain, "Select$" & e.Row.RowIndex)
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

    Protected Sub OnSelectedIndexChanged_AssetMain(sender As Object, e As EventArgs)
        Try
            Dim dt As New DataTable
            Dim index As Integer = gvAssetMain.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvAssetMain.SelectedRow.Cells(12).Text)
            Dim iItemNo As Integer = CInt(gvAssetMain.SelectedRow.Cells(13).Text)
            Session("ItemRentJoin") = iItemNo

            txtLandno.Text = gvAssetMain.SelectedRow.Cells(1).Text
            txtSurveyNo.Text = gvAssetMain.SelectedRow.Cells(2).Text
            txtLandNo2.Text = gvAssetMain.SelectedRow.Cells(3).Text
            txtAssetAddr.Text = gvAssetMain.SelectedRow.Cells(7).Text
            txtAssetSubdistrict.Text = gvAssetMain.SelectedRow.Cells(8).Text
            txtAssetDistrict.Text = gvAssetMain.SelectedRow.Cells(9).Text
            txtAssetProvince.Text = gvAssetMain.SelectedRow.Cells(10).Text
            txtAssPostCode.Text = gvAssetMain.SelectedRow.Cells(11).Text
            txtRai.Text = gvAssetMain.SelectedRow.Cells(4).Text
            txtNgan.Text = gvAssetMain.SelectedRow.Cells(5).Text
            txtWa.Text = FormatNumber(CDbl(gvAssetMain.SelectedRow.Cells(6).Text), 2)
            cboAssetType.SelectedIndex = gvAssetMain.SelectedRow.Cells(14).Text


            dt = objCo.loadAssetSublet(txtdocuno.Text, gvAssetMain.SelectedRow.Cells(1).Text)

            gvSublet.DataSource = dt
            gvSublet.DataBind()

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBound_AssetSublet(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSublet, "Select$" & e.Row.RowIndex)
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

    Protected Sub OnSelectedIndexChanged_AssetSublet(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvSublet.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvSublet.SelectedRow.Cells(7).Text)
            Dim iItemNo As Integer = CInt(gvSublet.SelectedRow.Cells(8).Text)

            txtAssetDocno.Text = gvSublet.SelectedRow.Cells(0).Text
            txtAssetSurvey.Text = gvSublet.SelectedRow.Cells(1).Text
            txtRai1.Text = gvSublet.SelectedRow.Cells(3).Text
            txtNgan1.Text = gvSublet.SelectedRow.Cells(4).Text
            txtWa1.Text = gvSublet.SelectedRow.Cells(5).Text
            txtRoadTo.Text = gvSublet.SelectedRow.Cells(6).Text

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Protected Sub OnSelectedIndexChangedPayment(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvPayment.SelectedRow.RowIndex
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

    Protected Sub OnRowDataBoundAsset(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvAsset, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"

                Dim dr1 As DataRowView = CType(e.Row.DataItem, DataRowView)
                If IsDBNull(dr1("pic1")) = False Then
                    Dim imageUrl1 As String = "data:image/jpg;base64," & Convert.ToBase64String(CType(dr1("pic1"), Byte()))
                    'image1.ImageUrl = imageUrl1
                    Session("imageUrl1") = imageUrl1
                    image3.ImageUrl = imageUrl1
                End If

                Dim dr2 As DataRowView = CType(e.Row.DataItem, DataRowView)
                If IsDBNull(dr2("pic2")) = False Then
                    Dim imageUrl2 As String = "data: Image/jpg;base64," & Convert.ToBase64String(CType(dr2("pic2"), Byte()))
                    'image2.ImageUrl = imageUrl2
                    Session("imageUrl2") = imageUrl2
                    image4.ImageUrl = imageUrl2
                End If

            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChangingAsset(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvAsset.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvAsset.SelectedRow.Cells(24).Text)
            Dim iItemNo As Integer = CInt(gvAsset.SelectedRow.Cells(25).Text)
            Session("IDAsset") = iDocID
            Session("ItemNoAsset") = iItemNo

            txtLandno.Text = gvAsset.SelectedRow.Cells(0).Text
            txtSurveyNo.Text = gvAsset.SelectedRow.Cells(2).Text
            txtAssetAddr.Text = gvAsset.SelectedRow.Cells(3).Text
            txtAssetSubdistrict.Text = gvAsset.SelectedRow.Cells(4).Text
            txtAssetDistrict.Text = gvAsset.SelectedRow.Cells(5).Text
            txtAssetProvince.Text = gvAsset.SelectedRow.Cells(6).Text
            txtAssPostCode.Text = gvAsset.SelectedRow.Cells(7).Text
            txtRoadTo.Text = gvAsset.SelectedRow.Cells(8).Text
            txtRai.Text = gvAsset.SelectedRow.Cells(9).Text
            txtNgan.Text = gvAsset.SelectedRow.Cells(10).Text
            txtWa.Text = FormatNumber(CDbl(gvAsset.SelectedRow.Cells(11).Text), 2)
            txtAssetDocno.Text = gvAsset.SelectedRow.Cells(12).Text
            txtAssetSurvey.Text = gvAsset.SelectedRow.Cells(8).Text
            txtRai1.Text = gvAsset.SelectedRow.Cells(13).Text
            txtNgan1.Text = gvAsset.SelectedRow.Cells(14).Text
            txtWa1.Text = FormatNumber(CDbl(gvAsset.SelectedRow.Cells(15).Text), 2)
            txtbuNo.Text = gvAsset.SelectedRow.Cells(16).Text
            txtRai2.Text = gvAsset.SelectedRow.Cells(17).Text
            txtNgan2.Text = gvAsset.SelectedRow.Cells(18).Text
            txtWa2.Text = FormatNumber(CDbl(gvAsset.SelectedRow.Cells(19).Text), 2)
            txtAssetRemark.Text = gvAsset.SelectedRow.Cells(20).Text
            txtAssetRemark2.Text = gvAsset.SelectedRow.Cells(21).Text
            txtLandNo2.Text = gvAsset.SelectedRow.Cells(22).Text
            'txtRai1.Text = gvAsset.SelectedRow.Cells(7).Text
            cboAssetType.SelectedValue = CInt(gvAsset.SelectedRow.Cells(1).Text)

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChangedAsset(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvAsset.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvAsset.SelectedRow.Cells(25).Text)
            Dim iItemNo As Integer = CInt(gvAsset.SelectedRow.Cells(26).Text)

            Session("IDAsset") = iDocID
            Session("ItemNoAsset") = iItemNo

            txtLandno.Text = gvAsset.SelectedRow.Cells(0).Text
            txtSurveyNo.Text = gvAsset.SelectedRow.Cells(2).Text
            txtAssetAddr.Text = gvAsset.SelectedRow.Cells(3).Text
            txtAssetSubdistrict.Text = gvAsset.SelectedRow.Cells(4).Text
            txtAssetDistrict.Text = gvAsset.SelectedRow.Cells(5).Text
            txtAssetProvince.Text = gvAsset.SelectedRow.Cells(6).Text
            txtAssPostCode.Text = gvAsset.SelectedRow.Cells(7).Text
            txtRoadTo.Text = gvAsset.SelectedRow.Cells(8).Text
            txtRai.Text = gvAsset.SelectedRow.Cells(9).Text
            txtNgan.Text = gvAsset.SelectedRow.Cells(10).Text
            txtWa.Text = FormatNumber(CDbl(gvAsset.SelectedRow.Cells(11).Text), 2)
            txtAssetDocno.Text = gvAsset.SelectedRow.Cells(12).Text
            txtAssetSurvey.Text = gvAsset.SelectedRow.Cells(8).Text
            txtRai1.Text = gvAsset.SelectedRow.Cells(13).Text
            txtNgan1.Text = gvAsset.SelectedRow.Cells(14).Text
            txtWa1.Text = FormatNumber(CDbl(gvAsset.SelectedRow.Cells(15).Text), 2)
            txtbuNo.Text = gvAsset.SelectedRow.Cells(16).Text
            txtRai2.Text = gvAsset.SelectedRow.Cells(17).Text
            txtNgan2.Text = gvAsset.SelectedRow.Cells(18).Text
            txtWa2.Text = FormatNumber(CDbl(gvAsset.SelectedRow.Cells(19).Text), 2)
            txtAssetRemark.Text = gvAsset.SelectedRow.Cells(20).Text
            txtAssetRemark2.Text = gvAsset.SelectedRow.Cells(21).Text
            txtLandNo2.Text = gvAsset.SelectedRow.Cells(22).Text
            'txtRai1.Text = gvAsset.SelectedRow.Cells(7).Text

            'Dim dr1 As DataRowView = gvAsset.SelectedRow.DataItem  'CType(gvAsset.SelectedRow, DataRowView)
            'If IsDBNull(dr1("pic1")) = False Then
            '    Dim imageUrl1 As String = "data:image/jpg;base64," & Convert.ToBase64String(CType(dr1("pic1"), Byte()))
            '    image1.ImageUrl = imageUrl1
            'End If

            cboAssetType.SelectedValue = CInt(gvAsset.SelectedRow.Cells(1).Text)

            Dim dt As New DataTable
            Dim iRow As Integer = 0

            dt = objCo.loadAsset_ItemNo(txtdocuno.Text, iItemNo)
            For Each dr As DataRow In dt.Rows
                image1.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(dr("Pic1"))
                image2.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(dr("Pic2"))
            Next


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBoundAtt3Add1(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvAtt3Add1, "Select$" & e.Row.RowIndex)
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

    Protected Sub OnSelectedIndexChangedAtt3Add1(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvAtt3Add1.SelectedRow.RowIndex
            'Dim iDocID As Integer = CInt(gvAtt3Add1.SelectedRow.Cells(25).Text)
            'Dim iItemNo As Integer = CInt(gvAtt3Add1.SelectedRow.Cells(26).Text)


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBoundAtt3Add2(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvAtt3Add2, "Select$" & e.Row.RowIndex)
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

    Protected Sub OnSelectedIndexChangedAtt3Add2(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvAtt3Add2.SelectedRow.RowIndex
            'Dim iDocID As Integer = CInt(gvAtt3Add1.SelectedRow.Cells(25).Text)
            'Dim iItemNo As Integer = CInt(gvAtt3Add1.SelectedRow.Cells(26).Text)


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnRowDataBound_PayTypeNonOil(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvPayTypeNonOil, "Select$" & e.Row.RowIndex)
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

    Protected Sub OnSelectedIndexChanged_PayNonOil(sender As Object, e As EventArgs)
        Try
            Dim dtNonOil As New DataTable

            Dim index As Integer = gvPayTypeNonOil.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvPayTypeNonOil.SelectedRow.Cells(0).Text)
            Dim iItemNo As Integer = CInt(gvPayTypeNonOil.SelectedRow.Cells(1).Text)

            Session("IDNonOil") = iDocID
            Session("ItemNoNonOil") = iItemNo

            dtNonOil = objCo.loadContractNonOil(txtdocuno.Text, iItemNo)

            For Each dr As DataRow In dtNonOil.Rows
                lblContractTypeNonOil.Text = dr("ContractTypeName")

                Select Case CInt(dr("PayRound"))
                    Case 1
                        rdoMonthNonOil.Checked = True
                        rdoYearNonOil.Checked = False
                        rdoOnceNonOil.Checked = False
                        rdoFreeNonOil.Checked = False
                    Case 2
                        rdoMonthNonOil.Checked = False
                        rdoYearNonOil.Checked = True
                        rdoOnceNonOil.Checked = False
                        rdoFreeNonOil.Checked = False
                    Case 3
                        rdoMonthNonOil.Checked = False
                        rdoYearNonOil.Checked = False
                        rdoOnceNonOil.Checked = True
                        rdoFreeNonOil.Checked = False
                    Case 4
                        rdoMonthNonOil.Checked = False
                        rdoYearNonOil.Checked = False
                        rdoOnceNonOil.Checked = False
                        rdoFreeNonOil.Checked = True
                End Select

                txtFrequencyNonOil.Text = dr("Frequency")

                If IsDate(dr("DueDate")) = True Then
                    'txtDueDateNonOil.Text = DateAdd(DateInterval.Year, 543, Date.Parse(dr("DueDate")))
                    txtDueDateNonOil.Text = Date.Parse(dr("DueDate"))
                Else
                    txtDueDateNonOil.Text = ""
                End If

                If IsDate(dr("BeginDate")) = True Then
                    'txtBeginDateNonOil.Text = DateAdd(DateInterval.Year, 543, Date.Parse(dr("BeginDate")))
                    txtBeginDateNonOil.Text = Date.Parse(dr("BeginDate"))
                Else
                    txtBeginDateNonOil.Text = ""
                End If

                If IsDate(dr("EndDate")) = True Then
                    'txtEndDateNonOil.Text = DateAdd(DateInterval.Year, 543, Date.Parse(dr("EndDate")))
                    txtEndDateNonOil.Text = Date.Parse(dr("EndDate"))
                Else
                    txtEndDateNonOil.Text = ""
                End If

                txtSizeNonOil.Text = dr("Size")
                txtNonOilWide.Text = dr("WideLong")
                txtRoomNumberNonOil.Text = dr("RoomNum")
                txtBusinessTypeNonOil.Text = dr("BusinessType")
                txtCustNameNonOil.Text = dr("CustName")
                txtIDCardNonOil.Text = dr("CardID")
                txtTaxIDNonOil.Text = dr("TaxID")
                txtHomeIDNonOil.Text = dr("Addr")
                txtSubDistrictNonOil.Text = dr("SubDistrict")
                txtDistrictNonOil.Text = dr("District")
                txtProvinceNonOil.Text = dr("Province")
                txtPostCodeNonOil.Text = dr("PostCode")
                txtContactNonOil.Text = dr("Contact")
                txtTelNonOil.Text = dr("Tel")
                txtLineNonOil.Text = dr("Line")
                txtEmailNonOil.Text = dr("Email")
                txtPayNonOilService.Text = FormatNumber(CDbl(dr("PayService")), 2)
                txtPayNonOilInsurance.Text = FormatNumber(CDbl(dr("PayInsurance")), 2)
                txtPayNonOilWater.Text = FormatNumber(CDbl(dr("PayWater")), 2)
                txtPqyNonOilElectrice.Text = FormatNumber(CDbl(dr("PayElectrict")), 2)
                txtPayNonOilCenter.Text = FormatNumber(CDbl(dr("PayCenter")), 2)
                txtPayNonOilGarbag.Text = FormatNumber(CDbl(dr("PayGarbag")), 2)
                txtPayNonOilLabel.Text = FormatNumber(CDbl(dr("PayLabel")), 2)
                txtPayNonOilRemarkOther.Text = FormatNumber(CDbl(dr("PayOther")), 2)
                txtNonOilRemarkOther.Text = dr("RemarkOther")
                txtNonOilRemark.Text = dr("Remark")
                cboContractType.SelectedValue = dr("GroupConID_SubItem")
            Next

            If disableTab(cboMainContact.SelectedValue) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnCheckedChanged_chkEndDate4(sender As Object, e As EventArgs)
        Try

            'If chkEndDate4.Checked = True Then
            '    txtEndDate4.Enabled = False
            'Else
            '    txtEndDate4.Enabled = True
            'End If

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChanged_cboBranch(sender As Object, e As EventArgs)
        Try
            Dim dtBr As New DataTable


            txtBrCodePowerBook.Text = cboBranch.SelectedValue

            dtBr = objCo.loadBranchInf(cboBranch.SelectedValue)

            For Each dr As DataRow In dtBr.Rows
                txtAddrPowerBook.Text = dr("Addr")
            Next


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

            'If AddContractPersonalSend() = False Then
            '    Exit Sub
            'End If

            'If AddContractPersonalContract() = False Then
            '    Exit Sub
            'End If

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

    Protected Sub txtContractBeginDate_TextChanged(sender As Object, e As EventArgs) 'Handles txtContractBeginDate.TextChanged
        Try
            If IsDate(txtContractBeginDate.Text) = True And IsDate(txtContractEndDate.Text) = True Then
                'Dim BeginDate As DateTime
                'Dim EndDatetime As DateTime

                txtcontractPeriod.Text = 0
                txtcontractPeriod2.Text = 0
                'txtcontractPeriod.Text = DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtContractBeginDate.Text), DateTime.Parse(txtContractEndDate.Text))
                ''txtcontractPeriod2.Text = DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse(txtContractBeginDate.Text), DateTime.Parse(txtContractEndDate.Text)) - (DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtContractBeginDate.Text), DateTime.Parse(txtContractEndDate.Text)) * 12)

                'txtcontractPeriod2.Text = DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse(txtContractBeginDate.Text), DateTime.Parse(txtContractEndDate.Text)) _
                '    - (DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtContractBeginDate.Text), DateTime.Parse(txtContractEndDate.Text)) * 12)

                'BeginDate = DateTime.Parse(txtContractBeginDate.Text)
                'EndDatetime = DateAdd(DateInterval.Day, 1, DateTime.Parse(txtContractEndDate.Text))
                'txtcontractPeriod.Text = DateDiff(DateInterval.Year, BeginDate, EndDatetime) / 12
                'txtcontractPeriod2.Text = DateDiff(DateInterval.Year, BeginDate, EndDatetime) * 12%


                Dim dt As New DataTable

                dt = objCo.loadPeriod(DateTime.Parse(txtContractBeginDate.Text), DateAdd(DateInterval.Day, 1, DateTime.Parse(txtContractEndDate.Text)))

                For Each dr As DataRow In dt.Rows
                    txtcontractPeriod.Text = dr("sYear")
                    txtcontractPeriod2.Text = dr("sMonth")
                Next

            End If

            disableTab(cboMainContact.SelectedValue)

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

            If loadContractFix() = False Then
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

            If loadContractPayment() = False Then
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

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i As Integer
        'Make the selected menu item reflect the correct imageurl
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                Menu1.Items(i).ImageUrl = "selectedtab.gif"
            Else
                Menu1.Items(i).ImageUrl = "unselectedtab.gif"

            End If
        Next

    End Sub

    Private Sub btnAddTypeAsset_Click(sender As Object, e As EventArgs) Handles btnAddTypeAsset.Click
        Try
            If objCo.AddAssetType(txtAssetType.Text) = False Then
                Exit Sub
            End If

            SetCboAssetType(cboAssetType)

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAddasset_Click(sender As Object, e As EventArgs) Handles btnAddasset.Click
        Try

            If objCo.AddContractAsset(txtdocuno.Text, txtLandno.Text, cboAssetType.SelectedValue, txtSurveyNo.Text, txtAssetAddr.Text, txtAssetSubdistrict.Text _
                                      , txtAssetDistrict.Text, txtAssetProvince.Text, txtAssPostCode.Text, txtAssetDocno.Text, txtAssetSurvey.Text, CDbl(txtRai.Text) _
                                      , CDbl(txtNgan.Text), CDbl(txtWa.Text), CDbl(txtRai1.Text), CDbl(txtNgan1.Text), CDbl(txtWa1.Text) _
                                      , txtbuNo.Text, CDbl(txtRai2.Text), CDbl(txtNgan2.Text), CDbl(txtWa2.Text), txtAssetRemark.Text, txtAssetRemark2.Text, usercode _
                                      , txtLandNo2.Text) = False Then
                Exit Sub
            End If

            UploadPic()

            If loadContractAsset() = False Then
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

    Protected Sub UploadPic()

        Dim iID As Integer
        Dim iItem As Integer
        Dim bytes As Byte()
        Dim bytes2 As Byte()

        iID = Session("IDAsset")
        iItem = Session("ItemNoAsset")

        Using br As BinaryReader = New BinaryReader(fiUpload.PostedFile.InputStream)
            bytes = br.ReadBytes(fiUpload.PostedFile.ContentLength)
        End Using

        Using br2 As BinaryReader = New BinaryReader(fiUpload2.PostedFile.InputStream)
            bytes2 = br2.ReadBytes(fiUpload2.PostedFile.ContentLength)
        End Using


        Dim constr As String = ConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString
        Using conn As SqlConnection = New SqlConnection(constr)
            Dim sql As String = "Update TT_Asset SET pic1=@pic1,pic2=@pic2 WHERE ID=@iID AND ItemNo=@iItem"
            Using cmd As SqlCommand = New SqlCommand(sql, conn)
                'cmd.Parameters.AddWithValue("@Name", Path.GetFileName(fiUpload.PostedFile.FileName))
                'cmd.Parameters.AddWithValue("@ContentType", fiUpload.PostedFile.ContentType)
                cmd.Parameters.AddWithValue("@pic1", bytes)
                cmd.Parameters.AddWithValue("@pic2", bytes2)
                cmd.Parameters.AddWithValue("@iID", iID)
                cmd.Parameters.AddWithValue("@iItem", iItem)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        End Using

        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Protected Sub UploadPicOth()

        Dim iID As Integer
        'Dim iItem As Integer
        Dim bytes As Byte()
        Dim bytes2 As Byte()

        iID = Session("IDOth")
        'iItem = Session("ItemNoOth")

        Using br As BinaryReader = New BinaryReader(FileUpload5.PostedFile.InputStream)
            bytes = br.ReadBytes(FileUpload5.PostedFile.ContentLength)
        End Using

        Using br2 As BinaryReader = New BinaryReader(FileUpload6.PostedFile.InputStream)
            bytes2 = br2.ReadBytes(FileUpload6.PostedFile.ContentLength)
        End Using


        Dim constr As String = ConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString
        Using conn As SqlConnection = New SqlConnection(constr)
            Dim sql As String = "Update TT_ContractOth SET pic1=@pic1,pic2=@pic2 WHERE ID=@iID "
            Using cmd As SqlCommand = New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@pic1", bytes)
                cmd.Parameters.AddWithValue("@pic2", bytes2)
                cmd.Parameters.AddWithValue("@iID", iID)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        End Using

        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Protected Sub UploadPicPowerBook()

        Dim iID As Integer
        Dim iItem As Integer
        Dim bytes As Byte()
        Dim bytes2 As Byte()

        iID = Session("IDPower")
        iItem = Session("ItemNoPower")

        Using br As BinaryReader = New BinaryReader(FileUpload1.PostedFile.InputStream)
            bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength)
        End Using

        Using br2 As BinaryReader = New BinaryReader(FileUpload2.PostedFile.InputStream)
            bytes2 = br2.ReadBytes(FileUpload2.PostedFile.ContentLength)
        End Using


        Dim constr As String = ConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString
        Using conn As SqlConnection = New SqlConnection(constr)
            Dim sql As String = "Update TT_PowerBook SET pic1=@pic1,pic2=@pic2 WHERE ID=@iID AND ItemNo=@iItem"
            Using cmd As SqlCommand = New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@pic1", bytes)
                cmd.Parameters.AddWithValue("@pic2", bytes2)
                cmd.Parameters.AddWithValue("@iID", iID)
                cmd.Parameters.AddWithValue("@ItemNo", iItem)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        End Using

        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Protected Sub UploadPicNonOil()

        Dim iID As Integer
        Dim iItem As Integer
        Dim bytes As Byte()
        Dim bytes2 As Byte()

        iID = Session("IDNonOil")

        iItem = Session("ItemNoNonOil")


        Using br As BinaryReader = New BinaryReader(FileUpload3.PostedFile.InputStream)
            bytes = br.ReadBytes(FileUpload3.PostedFile.ContentLength)
        End Using

        Using br2 As BinaryReader = New BinaryReader(FileUpload4.PostedFile.InputStream)
            bytes2 = br2.ReadBytes(FileUpload4.PostedFile.ContentLength)
        End Using


        Dim constr As String = ConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString
        Using conn As SqlConnection = New SqlConnection(constr)
            Dim sql As String = "Update TT_ContractNonOil SET pic1=@pic1,pic2=@pic2 WHERE ID=@iID AND ItemNo=@iItem"
            Using cmd As SqlCommand = New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@pic1", bytes)
                cmd.Parameters.AddWithValue("@pic2", bytes2)
                cmd.Parameters.AddWithValue("@iID", iID)
                cmd.Parameters.AddWithValue("@ItemNo", iItem)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        End Using

        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Private Sub btnUpload1_Click(sender As Object, e As EventArgs) Handles btnUpload1.Click
        Try


            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            fiUpload.SaveAs(folderPath & Path.GetFileName(fiUpload.FileName))
            image1.ImageUrl = "~/Files/" + fiUpload.FileName
            image1.ImageAlign = ImageAlign.Middle

            Dim iID As Integer
            Dim iItem As Integer
            Dim bytes As Byte()
            'Dim bytes2 As Byte()

            iID = Session("IDAsset")
            iItem = Session("ItemNoAsset")

            Using br As BinaryReader = New BinaryReader(fiUpload.PostedFile.InputStream)
                bytes = br.ReadBytes(fiUpload.PostedFile.ContentLength)
            End Using


            Dim constr As String = ConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString
            Using conn As SqlConnection = New SqlConnection(constr)
                Dim sql As String = "Update TT_Asset SET pic1=@pic1 WHERE ID=@iID AND ItemNo=@iItem"
                Using cmd As SqlCommand = New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@pic1", bytes)
                    cmd.Parameters.AddWithValue("@iID", iID)
                    cmd.Parameters.AddWithValue("@iItem", iItem)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                End Using
            End Using


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnUpload2_Click(sender As Object, e As EventArgs) Handles btnUpload2.Click
        Try

            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            fiUpload2.SaveAs(folderPath & Path.GetFileName(fiUpload2.FileName))
            image2.ImageUrl = "~/Files/" + fiUpload2.FileName
            image2.ImageAlign = ImageAlign.Middle

            Dim iID As Integer
            Dim iItem As Integer
            Dim bytes As Byte()
            'Dim bytes2 As Byte()

            iID = Session("IDAsset")
            iItem = Session("ItemNoAsset")

            Using br As BinaryReader = New BinaryReader(fiUpload2.PostedFile.InputStream)
                bytes = br.ReadBytes(fiUpload2.PostedFile.ContentLength)
            End Using


            Dim constr As String = ConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString
            Using conn As SqlConnection = New SqlConnection(constr)
                Dim sql As String = "Update TT_Asset SET pic2=@pic2 WHERE ID=@iID AND ItemNo=@iItem"
                Using cmd As SqlCommand = New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@pic2", bytes)
                    cmd.Parameters.AddWithValue("@iID", iID)
                    cmd.Parameters.AddWithValue("@iItem", iItem)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                End Using
            End Using


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Protected Sub View(sender As Object, e As EventArgs)

        Try
            'Dim embed As String = "<object data=""{0}"" type=""application/pdf"" width=""500px"" height=""300px"">"
            'embed += "If you are unable to view file, you can download from <a href = ""{0}"">here</a>"
            'embed += " or download <a target = ""_blank"" href = ""http://get.adobe.com/reader/"">Adobe PDF Reader</a> to view the file."
            'embed += "</object>"
            'ltEmbed.Text = String.Format(embed, ResolveUrl("~/Files/Mudassar_Khan.pdf"))

            Dim dt As New DataTable
            Dim sDoc As String = ""
            dt = objCo.loadViewDocument(cboMainContract.SelectedValue, cboTaxpayground.SelectedValue, cboTaxpaybuild.SelectedValue)
            sDoc = "D:\Doc_Contact\"
            For Each dr As DataRow In dt.Rows
                sDoc = "D:\Doc_Contact\" & dr("DocNo") & ".docx"
            Next

            Process.Start(sDoc)

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub cboMainContact_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cboMainContact.SelectedIndexChanged
        Try
            SetCboSubContact(cboContractType, cboMainContact.SelectedValue)

            If disableTab(cboMainContact.SelectedValue) = False Then
                Exit Sub
            End If

            SetCboPaymentType(cboPayType)
            'SetCboPaymentType(cboPayTypeNonOil)

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Protected Sub cboContractType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Select Case cboMainContact.SelectedValue
                Case 1, 5
                    lblContractTypeRentJoin.Text = cboMainContact.SelectedItem.Text
                    'lblAtt3Type.Text = cboContractType.SelectedItem.Text
                    'lblAtt3Type2.Text = cboContractType.SelectedItem.Text
                Case 2
                    lblContractTypeNonOil.Text = cboContractType.SelectedItem.Text
                Case 3
                    lblContractType4.Text = cboContractType.SelectedItem.Text
                    lblContractBook.Text = cboContractType.SelectedItem.Text
                Case 4
                    lblContractBook.Text = cboContractType.SelectedItem.Text
            End Select

            If disableTab(cboMainContact.SelectedValue) = False Then
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

    Private Function disableTab(iIndex As Integer) As Boolean

        Try

            Select Case iIndex
                Case 4
                    Menu1.Visible = False
                    MultiView1.Visible = False
                    Menu2.Visible = True
                    MultiView2.Visible = True
                    Menu3.Visible = False
                    MultiView3.Visible = False
                    Menu4.Visible = False
                    MultiView4.Visible = False
                Case 3
                    Menu1.Visible = False
                    MultiView1.Visible = False
                    Menu2.Visible = False
                    MultiView2.Visible = False
                    Menu3.Visible = False
                    MultiView3.Visible = False
                    Menu4.Visible = True
                    MultiView4.Visible = True

                Case 2
                    Menu1.Visible = False
                    MultiView1.Visible = False
                    Menu2.Visible = False
                    MultiView2.Visible = False
                    Menu3.Visible = True
                    MultiView3.Visible = True
                    Menu4.Visible = False
                    MultiView4.Visible = False

                Case 1, 5

                    Menu1.Visible = True
                    MultiView1.Visible = True
                    Menu2.Visible = False
                    MultiView2.Visible = False
                    Menu3.Visible = False
                    MultiView3.Visible = False
                    Menu4.Visible = False
                    MultiView4.Visible = False
                    'Menu1.Items(0).Enabled = False
                    'Menu1.Items(1).Enabled = False
                    'Menu1.Items(2).Enabled = False
                    'Menu1.Items(3).Enabled = False
                    'Menu1.Items(4).Enabled = False
                    'Menu1.Items(5).Enabled = False
                    'Menu1.Items(6).Enabled = False
                    'Menu1.Items(7).Enabled = False
                    'Menu1.Items(8).Enabled = False
                    'Menu1.Items(9).Enabled = False
                    'Menu1.Items(10).Enabled = False
                    'Menu1.Items(11).Enabled = False
                    'Menu1.Items(12).Enabled = True
                    'MultiView1.SetActiveView(Tab13)

            End Select

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
    Private Sub btnEditasset_Click(sender As Object, e As EventArgs) Handles btnEditasset.Click
        Try

            'UploadPic()

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try

            'If objCo.AddApprove(txtdocuno.Text, cboStatus.SelectedValue, Now(), usercode, "") = False Then
            '    Exit Sub
            'End If
            If objCo.AddApprove(txtdocuno.Text, 0, Now(), usercode, "") = False Then
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

    Private Sub txtCustCode_TextChanged(sender As Object, e As EventArgs) 'Handles txtCustCode.TextChanged
        For Each dr As DataRow In objCo.LoadVendor(txtCustCode.Text).Rows
            txtCustCode.Text = dr("VendorCode")
            txtName.Text = dr("VendorName")
        Next
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cboMainContact.SelectedIndexChanged
        Try

            lblCompany4.Text = cboCompany.SelectedItem.Text


            If disableTab(cboMainContact.SelectedValue) = False Then
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

    Protected Sub txtEndDate4_TextChanged(sender As Object, e As EventArgs) 'Handles txtContractBeginDate.TextChanged
        Try
            txtPeriod.Text = ""
            txtPeriod.Text = DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtBeginDate4.Text), DateTime.Parse(txtEndDate4.Text)) & " ปี "
            txtPeriod.Text = txtPeriod.Text & DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse(txtBeginDate4.Text), DateTime.Parse(txtEndDate4.Text)) - (DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtBeginDate4.Text), DateTime.Parse(txtEndDate4.Text)) * 12) & " เดือน"

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Protected Sub txtBeginDate4_TextChanged(sender As Object, e As EventArgs) 'Handles txtContractBeginDate.TextChanged
        Try
            txtPeriod.Text = ""
            txtPeriod.Text = DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtBeginDate4.Text), DateTime.Parse(txtEndDate4.Text)) & " ปี "
            txtPeriod.Text = txtPeriod.Text & DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse(txtBeginDate4.Text), DateTime.Parse(txtEndDate4.Text)) - (DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse(txtBeginDate4.Text), DateTime.Parse(txtEndDate4.Text)) * 12) & " เดือน"

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAddContractOth_Click(sender As Object, e As EventArgs) Handles btnAddContractOth.Click
        Try
            Dim CompanyID, chkEndDate, chkDueDate, RoomNum As Integer
            Dim DocDate, BeginDate, EndDate, DueDate As Date
            Dim Customerparty, Customerparty1, Customerparty2, Addr, Contact, Branch, Amount, Period, FineAmnt, PerformanceIns, ConditionIns, ConditionAmnt _
                                     , Remark, Remark1, Remark2, Remark3, Remark4, AddrLocation, DueDay As String

            Dim PayInsurance, PayBefore, PayFine, PayWater, PayElectrict, PayCenter, RentPerMonth As Decimal

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

            CompanyID = cboCompany.SelectedValue
            'DocDate = Now.Date ' DateAdd(DateInterval.Year, -543, CDate(txtdocdate.Text))

            If IsDate(txtStartDate.Text) = True Then
                'DocDate = DateAdd(DateInterval.Year, -543, CDate(txtStartDate.Text))
                DocDate = CDate(txtStartDate.Text)
            Else
                DocDate = "1900-01-01"
            End If

            Customerparty = txtCustomerparty.Text
            Customerparty1 = txtCustomerparty1.Text
            Customerparty2 = txtCustomerparty2.Text
            Addr = txtAddr4.Text
            Contact = txtContact4.Text
            Branch = txtBranchContract4.Text
            Amount = txtAmount4.Text
            Period = txtPeriod.Text

            'BeginDate = DateAdd(DateInterval.Year, -543, CDate(txtBeginDate4.Text))

            If IsDate(txtBeginDate4.Text) = True Then
                'BeginDate = DateAdd(DateInterval.Year, -543, CDate(txtBeginDate4.Text))
                BeginDate = CDate(txtBeginDate4.Text)
            Else
                BeginDate = "1900-01-01"
            End If

            If IsDate(txtEndDate4.Text) = True Then
                'EndDate = DateAdd(DateInterval.Year, -543, CDate(txtEndDate4.Text))
                EndDate = CDate(txtEndDate4.Text)
            Else
                EndDate = "1900-01-01"
            End If

            If IsDate(txtDueDate4.Text) = True Then
                'DueDate = DateAdd(DateInterval.Year, -543, CDate(txtDueDate4.Text))
                DueDate = CDate(txtDueDate4.Text)
            Else
                DueDate = "1900-01-01"
            End If

            FineAmnt = txtFineAmnt4.Text
            PerformanceIns = txtPerformanceIns.Text
            ConditionIns = txtConditionIns.Text
            ConditionAmnt = txtConditionAmnt.Text
            Remark = txtRemark4.Text
            Remark1 = txtRemark41.Text
            Remark2 = txtRemark42.Text
            Remark3 = txtRemark43.Text
            Remark4 = "" 'txtRemark44.Text

            If chkEndDate4.Checked = True Then
                chkEndDate = 1
            Else
                chkEndDate = 0
            End If

            If chkDueDate4.Checked = True Then
                chkDueDate = 1
            Else
                chkDueDate = 0
            End If

            If IsNumeric(txtPayInsuranceOth.Text) = True Then
                PayInsurance = CDbl(txtPayInsuranceOth.Text)
            Else
                PayInsurance = 0
            End If

            If IsNumeric(txtPayBeforeOth.Text) = True Then
                PayBefore = CDbl(txtPayBeforeOth.Text)
            Else
                PayBefore = 0
            End If

            If IsNumeric(txtPayFineOth.Text) = True Then
                PayFine = CDbl(txtPayFineOth.Text)
            Else
                PayFine = 0
            End If

            If IsNumeric(txtPayWaterOth.Text) = True Then
                PayWater = CDbl(txtPayWaterOth.Text)
            Else
                PayWater = 0
            End If

            If IsNumeric(txtPayElectricOth.Text) = True Then
                PayElectrict = CDbl(txtPayElectricOth.Text)
            Else
                PayElectrict = 0
            End If

            If IsNumeric(txtPayCenterOth.Text) = True Then
                PayCenter = CDbl(txtPayCenterOth.Text)
            Else
                PayCenter = 0
            End If

            If IsNumeric(txtRoomNumOth.Text) = True Then
                RoomNum = CInt(txtRoomNumOth.Text)
            Else
                RoomNum = 0
            End If

            If IsNumeric(txtRentPerMonthOth.Text) = True Then
                RentPerMonth = CDbl(txtRentPerMonthOth.Text)
            Else
                RentPerMonth = 0
            End If
            AddrLocation = txtAddrOther.Text
            DueDay = txtDueDateOth.Text

            If AddRequest() = False Then
                Exit Sub
            End If

            If objCo.AddContractOther(txtdocuno.Text, CompanyID, DocDate, Customerparty, Customerparty1, Customerparty2, Addr, Contact, Branch, Amount, [Period], BeginDate, EndDate, DueDate, FineAmnt, PerformanceIns _
                                , ConditionIns, ConditionAmnt, Remark, Remark1, Remark2, Remark3, Remark4, chkEndDate, chkDueDate, PayInsurance, PayBefore _
                                , PayFine, PayWater, PayElectrict, PayCenter, RoomNum, AddrLocation, DueDay, RentPerMonth) = False Then
                Exit Sub
            End If


            Dim dtOth As New DataTable
            dtOth = objCo.loadContractOth(txtdocuno.Text)

            For Each dr As DataRow In dtOth.Rows
                Session("IDOth") = dr("ID")
            Next
            'Session("IDOth") = iDocID
            'Session("ItemNoOth") = iItemNo

            'iID = Session("IDOth")
            'iItem = Session("ItemNoOth")

            UploadPicOth()

            Clear()

            Dim message As String = "Save Successfully."
            Dim sb As New System.Text.StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("alert('")
            sb.Append(message)
            sb.Append("')};")
            sb.Append("</script>")
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Session("sDocnoReport") = txtdocuno.Text
        Session("ContractType") = cboMainContact.SelectedValue

        Response.Redirect("rptComtract.aspx")
    End Sub

    Private Sub btnAddPowerBook_Click(sender As Object, e As EventArgs) Handles btnAddPowerBook.Click
        Try
            Dim DueDate, DocDate As Date
            Dim ContractTypeID, CompanyID, ItemNo As Integer
            Dim Empfr, Empto, Witness1, Witness2, Obj1, Obj2, Obj3, Oth1, Oth2, Oth3, CreateBy, Empto2, Empto3, BrCode, Addr, Contact As String

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

            CompanyID = cboCompany.SelectedValue
            ContractTypeID = cboContractType.SelectedValue
            'DueDate = DateAdd(DateInterval.Year, -543, CDate(txtIssueDateBook.Text))
            'DocDate = DateAdd(DateInterval.Year, -543, CDate(txtDocDateBook.Text))

            If IsDate(txtIssueDateBook.Text) = True Then
                'DueDate = DateAdd(DateInterval.Year, -543, CDate(txtIssueDateBook.Text))
                DueDate = CDate(txtIssueDateBook.Text)
            Else
                DueDate = "1900-01-01"
            End If

            If IsDate(txtDocDateBook.Text) = True Then
                'DocDate = DateAdd(DateInterval.Year, -543, CDate(txtDocDateBook.Text))
                DocDate = CDate(txtDocDateBook.Text)
            Else
                DocDate = "1900-01-01"
            End If


            Empfr = txtEmpfr.Text
            Empto = txtEmpto.Text
            Witness1 = txtWitness1.Text
            Witness2 = txtWitness2.Text
            Obj1 = txtObj1.Text
            Obj2 = txtObj2.Text
            Obj3 = txtObj3.Text
            Oth1 = txtOth1.Text
            Oth2 = txtOth2.Text
            Oth3 = txtOth3.Text
            CreateBy = usercode
            Empto2 = txtEmpto2.Text
            Empto3 = txtEmpto3.Text
            BrCode = txtBrCodePowerBook.Text
            Addr = txtAddrPowerBook.Text
            Contact = txtContactPowerBook.Text
            ItemNo = Session("ItemNo")

            If AddRequest() = False Then
                Exit Sub
            End If

            If objCo.AddPowerBook(txtdocuno.Text, ContractTypeID, DueDate, DocDate, Empfr, Empto, Witness1, Witness2, Obj1, Obj2, Obj3, Oth1, Oth2, Oth3 _
                                 , CreateBy, CompanyID, Empto2, Empto3, BrCode, Addr, Contact, ItemNo) = False Then
                Exit Sub
            End If

            Dim dtPower As New DataTable

            dtPower = objCo.loadContractPowerBook(txtdocuno.Text)

            For Each dr As DataRow In dtPower.Rows
                Session("IDPower") = dr("ID")
                Session("ItemNoPower") = dr("ItemNO")
            Next

            UploadPicPowerBook()


            Clear()

            Dim message As String = "Save Successfully."
            Dim sb As New System.Text.StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("alert('")
            sb.Append(message)
            sb.Append("')};")
            sb.Append("</script>")
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAddRentJoin_Click(sender As Object, e As EventArgs) Handles btnAddRentJoin.Click
        Try
            Dim ContractTypeID As Integer
            Dim DocDate, BeginDate, EndDate As Date
            Dim VendorContract1, VendorContract2, VendorContract3, VendorSign1, VendorSign2, VendorSign3, Witness, Addr, Contact, Branch, DuePay, RentArea1,
                                 RentArea2, RentArea3, RentArea4, Oth1, Oth2, Oth3, Oth4, CreateBy As String

            ContractTypeID = cboContractType.SelectedValue
            'DocDate = DateAdd(DateInterval.Year, -543, CDate(txtDocDateRentJoin.Text))
            'BeginDate = DateAdd(DateInterval.Year, -543, CDate(txtBeginDateRentJoin.Text))
            'EndDate = DateAdd(DateInterval.Year, -543, CDate(txtEndDateRentJoin.Text))

            DocDate = CDate(txtDocDateRentJoin.Text)
            BeginDate = CDate(txtBeginDateRentJoin.Text)
            EndDate = CDate(txtEndDateRentJoin.Text)

            VendorContract1 = txtVendorContract1.Text
            VendorContract2 = txtVendorContract2.Text
            VendorContract3 = txtVendorContract3.Text
            VendorSign1 = txtVendorSign1.Text
            VendorSign2 = txtVendorSign2.Text
            VendorSign3 = txtVendorSign3.Text
            Witness = txtWitnessRentJoin.Text
            Addr = txtAddrRentJoin.Text
            Contact = txtContactRentJoin.Text
            Branch = txtBranchRentJoin.Text
            DuePay = txtDuePayRentJoin.Text
            RentArea1 = txtRentAreaRentJoin1.Text
            RentArea2 = txtRentAreaRentJoin2.Text
            RentArea3 = txtRentAreaRentJoin3.Text
            RentArea4 = txtRentAreaRentJoin4.Text
            Oth1 = txtOthRentJoin1.Text
            Oth2 = txtOthRentJoin2.Text
            Oth3 = txtOthRentJoin3.Text
            Oth4 = txtOthRentJoin4.Text
            CreateBy = usercode

            If objCo.AddContractRentJoin(txtdocuno.Text, ContractTypeID, DocDate, VendorContract1, VendorContract2, VendorContract3, VendorSign1, VendorSign2, VendorSign3, Witness, Addr, Contact, Branch, BeginDate, EndDate, DuePay, RentArea1,
                                 RentArea2, RentArea3, RentArea4, Oth1, Oth2, Oth3, Oth4, CreateBy) = False Then
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

    Private Sub btnAtt3Add_Click(sender As Object, e As EventArgs) Handles btnAtt3Add.Click
        Try

            If objCo.AddAtt3(txtdocuno.Text, txtAtt3Type.Text, txtAtt3Size.Text) = False Then
                Exit Sub
            End If

            If loadAtt3() = False Then
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

    Private Sub btnAtt3Add2_Click(sender As Object, e As EventArgs) Handles btnAtt3Add2.Click
        Try

            If objCo.AddAtt3Add2(txtdocuno.Text, txtAtt3Type2.Text, txtAtt3SizeAdd2.Text) = False Then
                Exit Sub
            End If

            If loadAtt3Add2() = False Then
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

    Private Sub btnAddNonOil_Click(sender As Object, e As EventArgs) Handles btnAddNonOil.Click
        Try
            Dim iPayRound, ItemNo As Integer
            Dim dDueDate, dBeginDate, dEndDate As Date
            Dim err, scriptKey, javaScript As String
            err = ""
            If rdoMonthNonOil.Checked = True Then
                iPayRound = 1
            End If

            If rdoYearNonOil.Checked = True Then
                iPayRound = 2
            End If

            If rdoOnceNonOil.Checked = True Then
                iPayRound = 3
            End If

            If rdoFreeNonOil.Checked = True Then
                iPayRound = 4
            End If

            If IsNumeric(CDbl(txtPayNonOilService.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPayNonOilInsurance.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPayNonOilWater.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPqyNonOilElectrice.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPayNonOilCenter.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPayNonOilGarbag.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPayNonOilLabel.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
            If IsNumeric(CDbl(txtPayNonOilRemarkOther.Text)) = False Then
                err = "Input Numeric only"
                scriptKey = "UniqueKeyForThisScript"
                javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If

            If IsDate(txtDueDateNonOil.Text) = True Then
                'dDueDate = DateAdd(DateInterval.Year, -543, CDate(txtDueDateNonOil.Text))
                dDueDate = CDate(txtDueDateNonOil.Text)
            Else
                dDueDate = "1900-01-01"
            End If


            If IsDate(txtBeginDateNonOil.Text) = True Then
                'dBeginDate = DateAdd(DateInterval.Year, -543, CDate(txtBeginDateNonOil.Text))
                dBeginDate = CDate(txtBeginDateNonOil.Text)
            Else
                dBeginDate = "1900-01-01"
            End If

            If IsDate(txtEndDateNonOil.Text) = True Then
                'dEndDate = DateAdd(DateInterval.Year, -543, CDate(txtEndDateNonOil.Text))
                dEndDate = CDate(txtEndDateNonOil.Text)
            Else
                dEndDate = "1900-01-01"
            End If

            If Session("ItemNoNonOil") IsNot Nothing Then
                ItemNo = Session("ItemNoNonOil")
            Else
                ItemNo = 0
            End If


            If IsDate(txtBeginDateNonOil.Text) = True Then
                'dBeginDate = DateAdd(DateInterval.Year, -543, CDate(txtBeginDateNonOil.Text))
                dBeginDate = CDate(txtBeginDateNonOil.Text)
            Else
                dBeginDate = "1900-01-01"
            End If

            If IsDate(txtEndDateNonOil.Text) = True Then
                'dEndDate = DateAdd(DateInterval.Year, -543, CDate(txtEndDateNonOil.Text))
                dEndDate = CDate(txtEndDateNonOil.Text)
            Else
                dEndDate = "1900-01-01"
            End If

            If Session("ItemNoNonOil") IsNot Nothing Then
                ItemNo = Session("ItemNoNonOil")
            Else
                ItemNo = 0
            End If


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

            If AddRequest() = False Then
                Exit Sub
            End If

            If objCo.AddContractNonOil(txtdocuno.Text, lblContractTypeNonOil.Text, 0, 20 _
                                       , iPayRound, CInt(txtFrequencyNonOil.Text), dDueDate, dBeginDate, dEndDate, txtSizeNonOil.Text, txtRoomNumberNonOil.Text _
                                       , txtBusinessTypeNonOil.Text, "", txtCustNameNonOil.Text, txtIDCardNonOil.Text, txtTaxIDNonOil.Text _
                                       , txtHomeIDNonOil.Text, txtSubDistrictNonOil.Text, txtDistrictNonOil.Text, txtProvinceNonOil.Text, txtPostCodeNonOil.Text _
                                       , txtContactNonOil.Text, txtTelNonOil.Text, txtLineNonOil.Text, txtEmailNonOil.Text _
                                       , CDbl(txtPayNonOilService.Text), CDbl(txtPayNonOilInsurance.Text), CDbl(txtPayNonOilWater.Text) _
                                       , CDbl(txtPqyNonOilElectrice.Text), CDbl(txtPayNonOilCenter.Text), CDbl(txtPayNonOilGarbag.Text) _
                                       , CDbl(txtPayNonOilLabel.Text), CDbl(txtPayNonOilRemarkOther.Text), txtNonOilRemarkOther.Text _
                                      , txtNonOilWide.Text, ItemNo, txtNonOilRemark.Text, cboContractType.SelectedValue) = False Then

                Exit Sub
            End If

            Dim dtNon As New DataTable

            dtNon = objCo.loadContractNonOil2(txtdocuno.Text)

            For Each dr As DataRow In dtNon.Rows
                Session("IDNonOil") = dr("ID")
                Session("ItemNoNonOil") = dr("ItemNo")

            Next

            UploadPicNonOil()

            Clear()

            Dim message As String = "Save Successfully."
            Dim sb As New System.Text.StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("alert('")
            sb.Append(message)
            sb.Append("')};")
            sb.Append("</script>")
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())

            If loadContractNonOil() = False Then
                Exit Sub
            End If


            'If loadContractNonOil() = False Then
            '    Exit Sub
            'End If

            Clear()

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnAtt4Add_Click(sender As Object, e As EventArgs) Handles btnAtt4Add.Click
        Try

            Dim dAmount As Decimal

            If IsNumeric(txtAtt4Amnt.Text) = True Then
                dAmount = CDbl(txtAtt4Amnt.Text)
            Else
                dAmount = 0
            End If


            If objCo.AddAtt4(txtdocuno.Text, txtAtt4ServiceRate.Text, dAmount) = False Then
                Exit Sub
            End If

            If loadAtt4() = False Then
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

    Private Sub btnAddAssetSublet_Click(sender As Object, e As EventArgs) Handles btnAddAssetSublet.Click
        Try

            Dim iItemNo As Integer = 0

            'If Session("ItemRentJoin") <> 0 Then
            '    iItemNo = Session("ItemRentJoin")
            'End If

            If objCo.AddContractAssetSublet(txtdocuno.Text, txtAssetDocno.Text, txtAssetSurvey.Text, "", txtRai1.Text _
                                            , txtNgan1.Text, txtWa1.Text, txtRoadTo.Text) = False Then
                Exit Sub
            End If

            'If loadContractAssetMain() = False Then
            '    Exit Sub
            'End If

            If loadContractAssetSublet() = False Then
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

    Private Sub btnAddAssetMain_Click(sender As Object, e As EventArgs) Handles btnAddAssetMain.Click
        Try

            Dim iItemNo As Integer = 0

            If Session("ItemRentJoin") IsNot Nothing Then
                iItemNo = Session("ItemRentJoin")
            End If

            If objCo.AddContractAssetMain(txtdocuno.Text, txtLandno.Text, txtLandNo2.Text, cboAssetType.SelectedValue, txtSurveyNo.Text, txtAssetAddr.Text _
                                          , txtAssetSubdistrict.Text, txtAssetDistrict.Text, txtAssetProvince.Text, txtAssPostCode.Text _
                                          , txtRai.Text, txtNgan.Text, txtWa.Text, iItemNo) = False Then
                Exit Sub
            End If

            If loadContractAssetMain() = False Then
                Exit Sub
            End If

            If loadContractAssetSublet() = False Then
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

    Private Sub btnUploadPicOth1_Click(sender As Object, e As EventArgs) Handles btnUploadPicOth1.Click
        Try
            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            FileUpload5.SaveAs(folderPath & Path.GetFileName(FileUpload5.FileName))
            image7.ImageUrl = "~/Files/" + FileUpload5.FileName
            image7.ImageAlign = ImageAlign.Middle
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub btnUploadPicOth2_Click(sender As Object, e As EventArgs) Handles btnUploadPicOth2.Click
        Try
            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            FileUpload6.SaveAs(folderPath & Path.GetFileName(FileUpload6.FileName))
            image8.ImageUrl = "~/Files/" + FileUpload6.FileName
            image8.ImageAlign = ImageAlign.Middle
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnUploadPicPower1_Click(sender As Object, e As EventArgs) Handles btnUploadPicPower1.Click
        Try
            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            FileUpload1.SaveAs(folderPath & Path.GetFileName(FileUpload1.FileName))
            image9.ImageUrl = "~/Files/" + FileUpload1.FileName
            image9.ImageAlign = ImageAlign.Middle
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnUploadPicPower2_Click(sender As Object, e As EventArgs) Handles btnUploadPicPower2.Click
        Try
            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            FileUpload2.SaveAs(folderPath & Path.GetFileName(FileUpload2.FileName))
            image10.ImageUrl = "~/Files/" + FileUpload2.FileName
            image10.ImageAlign = ImageAlign.Middle
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnUploadPicNon1_Click(sender As Object, e As EventArgs) Handles btnUploadPicNon1.Click
        Try
            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            FileUpload3.SaveAs(folderPath & Path.GetFileName(FileUpload3.FileName))
            image11.ImageUrl = "~/Files/" + FileUpload3.FileName
            image11.ImageAlign = ImageAlign.Middle
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnUploadPicNon2_Click(sender As Object, e As EventArgs) Handles btnUploadPicNon2.Click
        Try
            Dim folderPath As String = Server.MapPath("~/Files/")

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If


            FileUpload4.SaveAs(folderPath & Path.GetFileName(FileUpload4.FileName))
            image12.ImageUrl = "~/Files/" + FileUpload4.FileName
            image12.ImageAlign = ImageAlign.Middle
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

End Class