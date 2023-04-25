

Public Class contractinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable


    Public clienttable As DataTable = createClient()
    Public assetstable As DataTable = createAsset()
    Public onetimetable As DataTable = createOnetime()
    Public fixtabletable As DataTable = createFix()
    Public flexibletable As DataTable = createFlex()
    Public paymenttable As DataTable = createPayment()

    Public usercode, username, projectno, contractno, contracttype As String


    'Public projectid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        txtContractDate.Attributes.Add("readonly", "readonly")
        txtContractActiveDate.Attributes.Add("readonly", "readonly")
        txtprojectno.Attributes.Add("readonly", "readonly")

        'txtBegindate.Attributes.Add("readonly", "readonly")
        'txtEndDate.Attributes.Add("readonly", "readonly")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            projectno = Session("projectno")
            contractno = Session("contractno")
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            clienttable = Session("client")
            BindDataClient()

            assetstable = Session("assets")
            BindDataAssets()

            onetimetable = Session("onetime")
            BindDataOnetime()

            flexibletable = Session("flex")
            BindDataFlex()

            fixtabletable = Session("fix")
            BindDataFix()

            paymenttable = Session("payment")
            BindDataPayment()

        Else

            SetCboContractType(cboContractType)

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            txtbranch.Text = Request.QueryString("branch")
            txtBranch1.Text = Request.QueryString("branch")

            projectno = Request.QueryString("projectno")
            contractno = Request.QueryString("agreeno")
            Session("projectno") = projectno
            Session("contractno") = contractno
            txtprojectno.Text = projectno
            Find(contractno)

            If loadRequestProject() = False Then
                Exit Sub
            End If
            'If Not (Session("client") Is Nothing) Then
            '    clienttable = Session("client")
            '    BindDataClient()
            'End If
        End If


    End Sub
    Private Sub Find(contractno As String)
        Dim objContract As New Contract
        Dim mydataset As DataSet
        If contractno = "" Then
            NewData()
        Else
            Try
                mydataset = objContract.Find(contractno)

                ViewContract(mydataset.Tables(0))

                clienttable = mydataset.Tables(1)
                Session("client") = clienttable
                BindDataClient()

                assetstable = mydataset.Tables(2)
                Session("assets") = assetstable
                BindDataAssets()

                onetimetable = mydataset.Tables(3)
                Session("onetime") = onetimetable
                BindDataOnetime()

                flexibletable = mydataset.Tables(4)
                Session("flex") = flexibletable
                BindDataFlex()

                fixtabletable = mydataset.Tables(5)
                Session("fix") = fixtabletable
                BindDataFix()

                paymenttable = mydataset.Tables(6)
                Session("paymenttable") = paymenttable
                BindDataPayment()


            Catch ex As Exception
                'show error
                Dim err As String
                err = Strings.Replace(ex.Message, "'", " ")
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('001 : " & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        End If
    End Sub

    Public Sub BindDataFlex()
        gvPaymentFlex.DataSource = flexibletable
        gvPaymentFlex.DataBind()
    End Sub

    Public Sub BindDataFix()
        gvPaymentFix.DataSource = fixtabletable
        gvPaymentFix.DataBind()
    End Sub

    Public Sub BindDataPayment()
        gvPayment.DataSource = paymenttable
        gvPayment.DataBind()
    End Sub

    Public Sub BindDataClient()
        gvClient.DataSource = clienttable
        gvClient.DataBind()
    End Sub

    Public Sub BindDataAssets()
        gvAssets.DataSource = assetstable
        gvAssets.DataBind()
    End Sub

    Public Sub BindDataOnetime()
        gvOneTime.DataSource = onetimetable
        gvOneTime.DataBind()
    End Sub

    Public Sub ViewContract(mydatatable As DataTable)
        With mydatatable
            If .Rows.Count = 0 Then
                'show msg nodata
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('002 : ไม่พบข้อมูล')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Else
                txtprojectno.Text = .Rows(0).Item("projectno")
                txtbranch.Text = .Rows(0).Item("branch")
                txtContractNo.Text = .Rows(0).Item("agno")
                txtLawContractNo.Text = .Rows(0).Item("LawContractNo")
                'txtContractDate.Text = .Rows(0).Item("agdate")
                'txtContractActiveDate.Text = .Rows(0).Item("agactivedate")

                txtContractDate.Text = DateAdd(DateInterval.Year, 543, CDate(.Rows(0).Item("agdate")))
                txtContractActiveDate.Text = DateAdd(DateInterval.Year, 543, CDate(.Rows(0).Item("agactivedate")))

                txtBegindate.Text = DateAdd(DateInterval.Year, 543, CDate(.Rows(0).Item("agdate")))
                txtEndDate.Text = DateAdd(DateInterval.Year, 543, CDate(.Rows(0).Item("agactivedate")))

                cboContractType.SelectedIndex = cboContractType.Items.IndexOf(cboContractType.Items.FindByText(.Rows(0).Item("agtype")))

                txtcontractPeriod.Text = .Rows(0).Item("ConperiodY")
                txtcontractPeriod2.Text = .Rows(0).Item("ConperiodM")
                txtRentalReg.Text = .Rows(0).Item("ConProvince")
                txtBranch1.Text = txtbranch.Text
                txtDays.Text = .Rows(0).Item("DayDueDate")
                txtRentPer.Text = .Rows(0).Item("RentPer")
                txtPlant.Text = .Rows(0).Item("AreaPlot")

                SetButton(.Rows(0).Item("status"))
            End If

        End With
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

                btnAssets.Enabled = True
                btnFlexible.Enabled = True
                btnClient.Enabled = True
                btnFix.Enabled = True
                btnPayment.Enabled = True

            Case "ยืนยัน(รออนุมัติ)", "ยกเลิก", "อนุมัติ", "Reject"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnSave.Enabled = False
                'btnNewAgree.Enabled = True
                btnNew.Enabled = False
                btnDel.Enabled = False

                btnAssets.Enabled = False
                btnFlexible.Enabled = False
                btnClient.Enabled = False
                btnFix.Enabled = False
                btnPayment.Enabled = False
        End Select
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewData()
    End Sub

    Private Sub NewData()
        SetButton("New")
        clienttable.Rows.Clear()
        BindDataClient()
        txtContractNo.Text = ""
        txtContractActiveDate.Text = Date.Now
        txtContractDate.Text = Date.Now

        txtBegindate.Text = Date.Now
        txtEndDate.Text = Date.Now

        cboContractType.SelectedIndex = -1
        contractno = ""
        'txtSaleVolume.Text = ""
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("projectinfo.aspx?projectno=" & projectno)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateData() Then
            SaveData()
        End If
    End Sub


    Private Sub SetCboContractType(obj As Object)
        Dim ag As New Contract

        obj.DataSource = ag.ContractTypeCbo()
        obj.DataValueField = "agtypeid"
        obj.DataTextField = "agtype"
        obj.DataBind()
    End Sub

    Private Function ValidateData() As Boolean
        Dim result As Boolean = True
        Dim scriptKey As String
        Dim javaScript As String

        If String.IsNullOrEmpty(txtbranch.Text) Then
            result = False
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('003 : กรุณาระบุข้อมูลสาขา')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
        Return result
    End Function

    Private Sub SaveData()
        Dim scriptKey As String
        Dim javaScript As String
        Dim chkerr As Integer = 0

        Dim contractdate, contractactivedate As DateTime

        Dim dBeginDate, dEndDate As Date

        Dim objcontract As New Contract
        Dim contracttype, lawcontractno As String

        lawcontractno = txtLawContractNo.text
        contracttype = cboContractType.SelectedItem.Text
        Try
            'contractdate = Date.Parse(txtContractDate.Text)
            contractdate = DateAdd(DateInterval.Year, -543, Date.Parse(txtContractDate.Text))

            dBeginDate = DateAdd(DateInterval.Year, -543, Date.Parse(txtBegindate.Text))
            dEndDate = DateAdd(DateInterval.Year, -543, Date.Parse(txtEndDate.Text))

        Catch ex As Exception
            'contractdate = Date.Now
            'txtContractDate.Text = Date.Now.ToString

            contractdate = DateAdd(DateInterval.Year, -543, Date.Now)
            txtContractDate.Text = DateAdd(DateInterval.Year, -543, Date.Now).ToString

            dBeginDate = DateAdd(DateInterval.Year, -543, Date.Parse(txtBegindate.Text))
            dEndDate = DateAdd(DateInterval.Year, -543, Date.Parse(txtEndDate.Text))
            txtBegindate.Text = DateAdd(DateInterval.Year, -543, Date.Now).ToString
            txtEndDate.Text = DateAdd(DateInterval.Year, -543, Date.Now).ToString

        End Try
        Try
            'contractactivedate = Date.Parse(txtContractActiveDate.Text)

            contractactivedate = DateAdd(DateInterval.Year, -543, Date.Parse(txtContractActiveDate.Text))

        Catch ex As Exception
            'contractactivedate = Date.Now
            'txtContractActiveDate.Text = Date.Now.ToString

            contractactivedate = DateAdd(DateInterval.Year, -543, Date.Now)
            txtContractActiveDate.Text = DateAdd(DateInterval.Year, -543, Date.Now).ToString

        End Try

        Try
            contractno = objcontract.Save(txtprojectno.Text, contracttype, txtContractNo.Text, lawcontractno, contractdate, contractactivedate, usercode _
                                          , dBeginDate, dEndDate, CInt(txtcontractPeriod.Text), CInt(txtcontractPeriod2.Text), 0 _
                                          , txtRentalReg.Text, txtbranch.Text, CInt(txtDays.Text), CDbl(txtPlant.Text), CDbl(txtRentPer.Text))
            txtContractNo.Text = contractno
            Session("contractno") = contractno
            SetButton("บันทึก")
        Catch ex As Exception
            Dim err As String = Strings.Replace(ex.Message, "'", "")
            chkerr = 100
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('004 : เกิดข้อผิดพลาด'" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        If chkerr = 0 Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub

    Private Function createPayment() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("paymentid", GetType(Double))
        dt.Columns.Add("paidtype", GetType(String))
        dt.Columns.Add("bankcode", GetType(String))
        dt.Columns.Add("bankbranchcode", GetType(String))
        dt.Columns.Add("bankbranchname", GetType(String))
        dt.Columns.Add("accountno", GetType(String))
        dt.Columns.Add("accountname", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function
    Private Function createClient() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("No", GetType(Integer))
        dt.Columns.Add("clientid", GetType(Double))
        dt.Columns.Add("clientno", GetType(String))
        dt.Columns.Add("client", GetType(String))
        dt.Columns.Add("clientaddress", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function
    Private Function createAsset() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("assetsno", GetType(String))
        dt.Columns.Add("assetstype", GetType(String))
        dt.Columns.Add("landno", GetType(String))
        dt.Columns.Add("surveyno", GetType(String))
        dt.Columns.Add("subdistrict", GetType(String))
        dt.Columns.Add("district", GetType(String))
        dt.Columns.Add("province", GetType(String))
        dt.Columns.Add("renttype", GetType(String))
        dt.Columns.Add("rai", GetType(String))
        dt.Columns.Add("ngan", GetType(String))
        dt.Columns.Add("wa", GetType(String))
        'dt.Columns.Add("gps", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function
    Private Function createFix() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("fixid", GetType(Double))
        dt.Columns.Add("assetsno", GetType(String))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("recurring", GetType(String))
        dt.Columns.Add("frequency", GetType(String))
        dt.Columns.Add("begindate", GetType(DateTime))
        dt.Columns.Add("enddate", GetType(DateTime))
        dt.Columns.Add("duedate", GetType(DateTime))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Function createFlex() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("fixid", GetType(Double))
        dt.Columns.Add("assetsno", GetType(String))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("recurring", GetType(String))
        dt.Columns.Add("frequency", GetType(String))
        dt.Columns.Add("begindate", GetType(DateTime))
        dt.Columns.Add("enddate", GetType(DateTime))
        dt.Columns.Add("duedate", GetType(DateTime))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Function createOnetime() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("onetimeid", GetType(Double))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("duedate", GetType(String))
        dt.Columns.Add("amount", GetType(String))
        dt.Columns.Add("payer", GetType(String))
        dt.Columns.Add("remark", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function
    Private Sub btnClient_Click(sender As Object, e As EventArgs) Handles btnClient.Click
        If chkagree() Then
            Response.Redirect("clientinfo.aspx?contractno=" & contractno & "&clientno=")
        End If
    End Sub

    Private Sub btnFix_Click(sender As Object, e As EventArgs) Handles btnFix.Click
        If chkagree() Then
            Response.Redirect("fixibleinfo.aspx?contractno=" & contractno & "&fixno=")
        End If
    End Sub

    Private Sub btnFlexible_Click(sender As Object, e As EventArgs) Handles btnFlexible.Click
        If chkagree() Then
            Response.Redirect("flexibleinfo.aspx?contractno=" & contractno & "&flexno=")
        End If
    End Sub

    Private Function chkagree() As Boolean
        Dim result As Boolean = False

        If String.IsNullOrEmpty(txtContractNo.Text) Then
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('กรุณาบันทึกสัญญาก่อน')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        Else
            result = True
        End If

        Return result
    End Function
    Private Sub btnAssets_Click(sender As Object, e As EventArgs) Handles btnAssets.Click

        If chkagree() Then


            assetsinfo.iBuconType = cboContractType.SelectedValue
            assetsinfo.BuconTypeName = cboContractType.SelectedItem.ToString

            Response.Redirect("assetsinfo.aspx?contractno=" & contractno & "&assetsno=" )
        End If

    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        If chkagree() Then
            Response.Redirect("paymentinfo.aspx?contractno=" & contractno & "&paymentno=")
        End If
    End Sub

    Private Sub btnOnetime_Click(sender As Object, e As EventArgs) Handles btnOnetime.Click
        If chkagree() Then
            Response.Redirect("onetimeinfo.aspx?contractno=" & contractno & "&onetimeid=")
        End If
    End Sub

    Private Sub btnEstimate_Click(sender As Object, e As EventArgs) Handles btnEstimate.Click
        If Not String.IsNullOrEmpty(txtContractNo.Text) Then
            Response.Redirect("estimate_payment_byContract.aspx?agreeno=" & txtContractNo.Text & "&projectno=" & projectno)
        End If
    End Sub

    Private Sub cbocontracttype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboContractType.SelectedIndexChanged
        Session(contracttype) = cboContractType.SelectedItem.Text
    End Sub

    Private Function loadRequestProject() As Boolean
        Try
            Dim objReq As New clsRequestContract
            Dim dt As New DataTable

            dt = objReq.loadRequestProject(txtprojectno.Text)

            For Each dr As DataRow In dt.Rows
                cboContractType.SelectedValue = dr("ContractID")
                txtBegindate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
                txtEndDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("EndDate")))

                txtContractDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
                txtContractActiveDate.Text = DateAdd(DateInterval.Year, 543, CDate(dr("BeginDate")))
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