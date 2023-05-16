

Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.Diagnostics.Contracts
Imports System.Globalization
Imports System.Net
Imports System.Reflection
Imports System.Runtime.Remoting
Imports System.Windows.Controls

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

            'Dim objprj As New Project
            'dtBranch = objprj.loadBranch
            'cboBranch.DataSource = dtBranch
            'cboBranch.DataValueField = "branch_code"
            'cboBranch.DataTextField = "branch_name"
            'cboBranch.DataBind()


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

            Dim objprj As New Project
            dtBranch = objprj.loadBranch
            cboBranch.DataSource = dtBranch
            cboBranch.DataValueField = "branch_code"
            cboBranch.DataTextField = "branch_name"
            cboBranch.DataBind()

            dtStatus = objprj.loadStatus(1)
            cboStatus.DataSource = dtStatus
            cboStatus.DataValueField = "ID"
            cboStatus.DataTextField = "StatusName"
            cboStatus.DataBind()

            SetCboContractType(cboContractType)

            cboSex.Items.Add("Male")
            cboSex.Items.Add("Female")

            'txtdocuno.Text = Request.QueryString("agreeno")
            Dim objReq As New clsRequestContract

            txtdocuno.Text = objReq.GetDocRun(1, 0)
            BindDataClient()

        End If
        'txtDocAction.Text = "NEW"

    End Sub

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

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
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

        Dim DocuNo, Branch, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, Address, SubDistrict, District, Province, PostCode, CreateBy As String
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


        If txtDocAction.Text = "NEW" Then
            txtdocuno.Text = objReq.GetDocRun(1, 1)
            DocuNo = txtdocuno.Text
            result = objReq.AddRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy)
        ElseIf txtDocAction.Text = "EDIT" Then
            result = objReq.UpdateRequest(DocuNo, Branch, ContractID, dContractBegindate, dContractEndDate, CustName, CardID, Gender, Company, Mobile, Tel, Email, Line, StatusID, Address, SubDistrict _
                                , District, Province, PostCode, CreateDate, CreateBy, CInt(txtDocIDAction.Text))
        End If

        txtDocIDAction.Text = 0
        txtDocAction.Text = "NEW"
        Clear()

        Return result
    End Function

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
                txtDocAction.Text = "EDIT"
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

End Class