Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Windows

Public Class frmJobs
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public detailtable As DataTable '= createdetailtable()
    Public maintable As DataTable '= createmaintable()
    Public nozzletable As DataTable
    Public approvertable As DataTable
    Public assetsNozzletable As DataTable
    Public menutable As DataTable
    Public owner As Integer
    Public branchid As Integer
    Public jobtypeid As Integer
    Public cntdt As Integer

    Public jobcateid As Integer
    Public jobgroupid As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier
        Dim objpolicy As New Policy
        Dim objAssets As New Assets

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        'txtDocDate.Text = Now
        txtDocDate.Attributes.Add("readonly", "readonly")
        'Session("username") = "PAB"

        txtDueDate.Attributes.Add("readonly", "readonly")
        owner = 0

        Dim usercode As String
        usercode = Session("usercode")

        'chkuser(usercode)

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


        If Not IsPostBack() Then
            detailtable = createdetailtable()
            maintable = createmaintable()
            nozzletable = createnozzletable()
            ViewState("nozzletable") = nozzletable
            ViewState("detailtable") = detailtable
            ViewState("maintable") = maintable
            ViewState("status") = "new"
            objStatus = "new"

            ClearText()
            objbranch.SetComboBranch(cboBranch, usercode)
            objdep.SetCboDepartment(cboDepartment, 0)
            objdep.SetCboDepartmentforjobtype(cboDepForJobType)
            objsec.SetCboSection(cboSection, 0)
            objsupplier.SetCboSupplier(cboSupplier)
            'objsupplier.SetCboSupplierByComid(cboSupplier, "1") '1 PURE
            'objdep.SetCboDepartment(cboDepartment, 0)

            'SetCboBranch(cboBranch)
            'SetCboDepartment(cboDepartment)
            'SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
            'objAssets.SetCboAssets(cboAsset, 0)
            objAssets.SetCboAssetsByName(cboAsset, 0)

            SetCboJobType(cboJobType, usercode, "actived")


            SetCboUnit(cboUnit)
            SetCboUsers(cboOwner)
            'ViewState("jobtype") = cboJobType.SelectedItem.Value

            jobtypeid = cboJobType.SelectedItem.Value
            objpolicy.setComboPolicyByJobTypeID(cboPolicy, cboJobType.SelectedItem.Value)

            If Not Request.QueryString("jobno") Is Nothing Then
                'objpolicy.setComboPolicyByJobTypeID(cboPolicy, cboJobType.SelectedItem.Value)
                'txtDocDate.Text = Now()
                ViewState("jobno") = Request.QueryString("jobno")
                txtJobno.Text = ViewState("jobno")
                FindJobNo(txtJobno.Text)

            Else
                If Not Session("branchid") Is Nothing Then
                    cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid")))
                    FindPositionInPump(cboBranch.SelectedItem.Value)
                    findNozzle(cboBranch.SelectedItem.Value)
                End If
                ViewState("status") = "new"
                objStatus = "new"
                txtCreateBy.Text = Session("username")
                txtCreateDate.Text = Now()
            End If

            SetMenu()
        Else
            objStatus = ViewState("status")

            'If Not (ViewState("detailtable") Is Nothing) Then
            detailtable = ViewState("detailtable")
            maintable = ViewState("maintable")
            nozzletable = ViewState("nozzletable")
            assetsNozzletable = ViewState("assetsNozzletable")
            'If maintable.Rows.Count > 0 Then
            '    showdata(maintable)
            'End If

            'End If
        End If
    End Sub
    Private Sub chkuser(usercode As String)
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String
        Dim result As Boolean = False


        Dim objmenu As New menu

        Try
            result = objmenu.chkuser(usercode, "O001")
            If result = False Then
                javaScript = "alertWarning('คุณไม่มีสิทธิ์ใช้งาน กรุณาติดต่อ Admin');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Response.Redirect("~\default.aspx")
            End If
        Catch ex As Exception
            javaScript = "alert('" + ex.Message + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try


    End Sub
    Private Sub FindJobNo(jobno As String)
        Dim job As New jobs
        Dim mydataset As DataSet
        'Dim maintable As DataTable
        Dim usercode As String = Session("usercode")
        ViewState("jobno") = jobno
        'itemtable
        Try
            mydataset = job.Find(usercode, jobno)
            maintable = mydataset.Tables(0)
            detailtable = mydataset.Tables(1)
            nozzletable = mydataset.Tables(2)
            approvertable = mydataset.Tables(3)
            'itemtable = mydataset.Tables(1)
            'ViewState("itemtable") = itemtable

            showdata(maintable)
            ViewState("maintable") = maintable
            ViewState("detailtable") = detailtable
            ViewState("nozzletable") = nozzletable
            ViewState("approvertable") = approvertable
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('Find Fail')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            ClearText()
        End Try
    End Sub

    Private Sub showdata(mytable As DataTable)
        With mytable.Rows(0)
            owner = .Item("owner")
            txtDocDate.Text = .Item("jobdate")
            'txtOwner.Text = .Item("jobowner")
            cboOwner.SelectedIndex = cboOwner.Items.IndexOf(cboOwner.Items.FindByValue(.Item("jobowner")))
            txtJobno.Text = .Item("jobno")
            txtCreateBy.Text = .Item("createby")
            txtCreateDate.Text = .Item("createdate")
            txtStatus.Text = .Item("statusname")
            Select Case .Item("statusid")
                Case = 1 'แจ้งงาน
                    ViewState("status") = "edit"
                    objStatus = "edit"
                Case = 2 'ยืนยันแจ้งงาน
                    ViewState("status") = "confirm"
                    objStatus = "confirm"
                Case = 3 'กำลังดำเนินการ
                    ViewState("status") = "action"
                    objStatus = "action"
                Case = 4 'ปิดงาน
                    ViewState("status") = "close"
                    objStatus = "close"
                Case = 5 'ยกเลิก
                    ViewState("status") = "cancel"
                    objStatus = "cancel"
            End Select

            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(.Item("branchid")))
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(.Item("depid")))
            'SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
            cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(.Item("secid")))



            BindDataNozzle()
        End With
    End Sub
    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobdetailid", GetType(Double))
        dt.Columns.Add("jobno", GetType(String))
        dt.Columns.Add("jobtypeid", GetType(Integer))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("assetid", GetType(Integer))
        dt.Columns.Add("assetposition", GetType(String))
        dt.Columns.Add("assetcode", GetType(String))
        dt.Columns.Add("assetname", GetType(String))
        dt.Columns.Add("quantity", GetType(Double))
        dt.Columns.Add("unitid", GetType(Double))
        dt.Columns.Add("unit", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("supplierid", GetType(Double))
        dt.Columns.Add("supplier", GetType(String))
        dt.Columns.Add("policyid", GetType(Double))
        dt.Columns.Add("policy", GetType(String))
        dt.Columns.Add("requestdate", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("statusid", GetType(Double))
        dt.Columns.Add("owner", GetType(Integer))
        dt.Columns.Add("followup_status", GetType(String))
        dt.Columns.Add("attatch", GetType(String))
        dt.Columns.Add("brand", GetType(String))
        dt.Columns.Add("model", GetType(String))
        dt.Columns.Add("nozzle", GetType(String))
        dt.Columns.Add("reqApproval", GetType(Boolean))
        'dt.Columns.Add("vendor_code", GetType(String))



        Return dt
    End Function
    Private Function createmaintable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobid", GetType(Double)) 'new =0
        dt.Columns.Add("jobno", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("jobdate", GetType(String))
        dt.Columns.Add("jobowner", GetType(Double))
        dt.Columns.Add("statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("statusname", GetType(String)) 'new=new
        dt.Columns.Add("branchid", GetType(Double))
        dt.Columns.Add("depid", GetType(Double))
        dt.Columns.Add("secid", GetType(Double))
        dt.Columns.Add("owner", GetType(Integer))

        Return dt
    End Function
    Private Function createnozzletable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("nozzle_ID", GetType(Integer))
        dt.Columns.Add("nozzle_No", GetType(String))
        dt.Columns.Add("branchid", GetType(Integer))
        dt.Columns.Add("rowNumber", GetType(Integer))
        dt.Columns.Add("brand", GetType(String))
        dt.Columns.Add("expiryDate", GetType(String))
        dt.Columns.Add("productType", GetType(String))
        dt.Columns.Add("positionOnAssest", GetType(String))
        dt.Columns.Add("url", GetType(String))
        dt.Columns.Add("active", GetType(Integer))
        dt.Columns.Add("AssetID", GetType(String))
        dt.Columns.Add("UpdateBycode", GetType(String))
        dt.Columns.Add("UpdateBy", GetType(Integer))
        dt.Columns.Add("UpdateDate", GetType(String))
        dt.Columns.Add("jobref", GetType(String))


        Return dt
    End Function
    Private Sub SetMenu()
        Select Case objStatus
            Case = "new"
                btnSave.Enabled = True
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = False
                btnCancel.Disabled = True
            Case = "edit"
                If owner = 1 Then
                    btnSave.Enabled = True
                    btnConfirm.Enabled = True
                Else
                    btnSave.Enabled = False
                    btnConfirm.Enabled = False
                End If
                btnPrint.Enabled = True
                btnCancel.Disabled = False
            Case = "confirm"
                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                btnCancel.Disabled = False
            Case = "action"
                btnSave.Enabled = False
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                btnCancel.Disabled = True
            Case = "close"
                btnSave.Enabled = False
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                btnCancel.Disabled = True
            Case = "cancel"
                btnSave.Enabled = False
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                btnCancel.Disabled = True
                'Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "<script type='text/javascript'>chkButtonCancel('btnCancel'," & ViewState("status") & ");"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "chkButtonCancel('btnCancel'," & ViewState("status") & ")", True)
        End Select


    End Sub
    Private Sub ClearText()
        txtDocDate.Text = Now()
        'txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        'txtDueDate.Text = Now()
        txtDueDate.Text = ""
        'txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        txtJobno.Text = ""
        'txtCreateDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        cboOwner.SelectedIndex = 0
        txtStatus.Text = ""
        txtCreateBy.Text = Session("username")
        txtCreateDate.Text = Now()
        cboBranch.SelectedIndex = 0
        cboDepartment.SelectedIndex = 0
        cboSection.SelectedIndex = 0
        cboUnit.SelectedIndex = 1
        txtQuantity.Text = "1"
        detailtable.Rows.Clear()
        maintable.Rows.Clear()
        ViewState("detailtable") = detailtable
        ViewState("maintable") = maintable
        ViewState("status") = "new"
        ViewState("jobno") = Nothing


        detailtable.Rows.Clear()
    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validatedata() Then
            'If ViewState("status") = "new" Then
            If maintable.Rows.Count = 0 Then
                updatehead()
            End If
            Save()
            'Else
            '    SaveEdit()
            'End If
        Else
            txtJobDetail.Focus()
        End If
    End Sub
    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If cboBranch.SelectedItem.Value = "" Or cboBranch.SelectedItem.Value = 0 Then
            result = False
            msg = "กรุณาเลือกสาขา"
            GoTo endprocess
        End If
        If cboOwner.SelectedIndex < 0 Then
            result = False
            msg = "กรุณาเลือกผู้แจ้ง"
            GoTo endprocess
        End If
        If String.IsNullOrEmpty(txtDocDate.Text) Then
            result = False
            msg = "กรุณาระบุวันที่แจ้ง"
            GoTo endprocess
        End If
        If detailtable.Rows.Count = 0 Then
            result = False
            msg = "กรุณาเพิ่มรายการย่อย โดยกดปุ่ม + ด้านล่าง"
            GoTo endprocess
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function

    Private Sub Save()
        Dim job As New jobs
        Dim jobno As String = ""
        Dim secid As Integer

        jobno = txtJobno.Text
        If cboSection.SelectedIndex < 0 Then
            secid = 0
        Else
            secid = cboSection.SelectedItem.Value
        End If
        Try
            jobno = job.Save(jobno, maintable, detailtable, Session("usercode"))
            txtJobno.Text = jobno
            ViewState("jobno") = jobno
            ViewState("status") = "edit"
            objStatus = "edit"
            txtStatus.Text = "แจ้งงาน"


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../OPS/jobs.aspx?jobno=" & jobno)
endprocess:
    End Sub
    'Private Sub SaveEdit()
    '    Dim job As New jobs
    '    Dim jobcode As String = ""
    '    Dim secid As Integer
    '    If cboSection.SelectedIndex < 0 Then
    '        secid = 0
    '    Else
    '        secid = cboSection.SelectedItem.Value
    '    End If
    '    Try
    '        jobcode = job.Save(maintable, detailtable)
    '        txtJobno.Text = jobcode
    '        ViewState("jobcode") = jobcode
    '        ViewState("status") = "edit"
    '    Catch ex As Exception
    '        Dim scriptKey As String = "UniqueKeyForThisScript"
    '        Dim javaScript As String = "<script type='text/javascript'>msgalert('" & ex.Message & "');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
    '    End Try
    'End Sub
    Private Function validateDetail() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If cboBranch.SelectedItem.Value = "" Or cboBranch.SelectedItem.Value = 0 Then
            result = False
            msg = "กรุณาเลือกสาขา"
            GoTo endprocess
        End If
        If cboJobType.SelectedItem.Value.ToString = "" Then
            result = False
            msg = "กรุณาเลือกประเภทงาน"
            GoTo endprocess
        ElseIf cboJobType.SelectedItem.Value = 16 Then
            If String.IsNullOrEmpty(txtAssetName.Text) And String.IsNullOrEmpty(txtBrand.Text) And String.IsNullOrEmpty(txtModel.Text) Then
                result = False
                msg = "กรุณาระบุรหัสทรัพสินทร์ หรือ ชื่อยี่ห้อ หรือ รุ่น"
                GoTo endprocess
            End If
        ElseIf cboJobType.SelectedItem.Value = 1 Then
            'If cboPosition.SelectedIndex < 0 And String.IsNullOrEmpty(txtAssetName.Text) Then
            'result = False
            'msg = "กรุณาเลือกตำแหน่งตู้ หรือ กรอกรหัส FA.. ของตู้จ่าย"
            'GoTo endprocess
            'End If
        End If

        If cboJobType.SelectedItem.Text.ToString.IndexOf("ตีตรา") > -1 Then
            If String.IsNullOrEmpty(lblnozzlehidden.Text.ToString) Then
                result = False
                msg = "กรุณาระบุมือจ่ายที่จะตีตรา"
                GoTo endprocess
            End If

        End If

        If String.IsNullOrEmpty(txtJobDetail.Text) Then
            result = False
            msg = "กรุณาระบุรายละเอียด"
            GoTo endprocess
        End If
        If cboPolicy.SelectedItem.Value = 0 Then
            ' If String.IsNullOrEmpty(txtDueDate.Text) Then
            result = False
            msg = "กรุณาเลือก ความเร่งด่วน"
            GoTo endprocess
            'End If
        End If


endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
        'endprocess:


        Return result
    End Function


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        objStatus = "new"
        ViewState("status") = "new"
        ClearText()
        SetMenu()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim s As String = "window.open('../OPS/Jobs_Report_JobForm.aspx?jobcode=" & Request.QueryString("jobno").ToString() &
    "&supplierid=0', '_blank');"

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
    End Sub

    Private Sub ViewReport(jobcode As String)
        Dim rpt As New ReportDocument()
        Dim rptname As String = "\ops\reports\rpt_ops_form.rpt"
        Try
            'Dim test As String = Server.MapPath(rptname)
            rpt.Load(Server.MapPath(rptname))

            rpt.SetDatabaseLogon(dbLogin, dbPassword, dbServer, dbOPS)
            'rpt.Refresh()

            For Each TAB As Table In rpt.Database.Tables
                Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
                logoninfo.ConnectionInfo.UserID = dbLogin
                logoninfo.ConnectionInfo.Password = dbPassword
                logoninfo.ConnectionInfo.ServerName = dbServer
                logoninfo.ConnectionInfo.DatabaseName = dbOPS
                TAB.ApplyLogOnInfo(logoninfo)
            Next


            rpt.SetParameterValue("@jobcode", jobcode)


            ViewState("rpt") = rpt
            Response.Write("<script>")
            Response.Write("window.open('../ReportViewer.aspx','_blank')")
            Response.Write("</script>")


            'Me.CrystalReportViewer1.ReportSource = ViewState("rpt")
            'Me.CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alert('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try



    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        FindAsset()
    End Sub

    Private Sub FindAsset()
        Dim ass As New Assets
        Dim mydataset As DataSet
        Try
            mydataset = ass.Find(txtAssetCode.Text)
            If mydataset.Tables(0).Rows.Count > 0 Then
                txtAssetName.Text = mydataset.Tables(0).Rows(0).Item("name")
            Else
                txtAssetCode.Text = ""
                txtAssetName.Text = ""
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('ไม่มีรหัสทรัพสินนี้');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

        'confirm
        If Confirm(txtJobno.Text, Session("username")) Then
            Response.Redirect("../OPS/jobs.aspx?jobno=" & txtJobno.Text)

        End If

    End Sub
    'get email by user
    Private Function getEmailList() As String
        Dim user As New Users
        Dim result As String
        'Dim jobtypeid As Integer=0
        Try
            result = user.EmailList(Session("username"))
        Catch ex As Exception
            result = False
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alert('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
        Return result
    End Function
    'get email by jobno and  jobtypeid
    Private Function getEmailList(jobtypeid As Integer) As String
        Dim job As New jobs
        Dim result As String
        'Dim jobtypeid As Integer=0
        Try
            result = job.EmailList(ViewState("jobno"), jobtypeid)
        Catch ex As Exception
            result = False
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alert('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
        Return result
    End Function
    Private Function Confirm(jobno As String, usercode As String) As Boolean
        Dim job As New jobs
        Dim result As Boolean = True

        Try
            job.Confirm(jobno, usercode)
            ViewState("status") = "confirm"
            objStatus = "confirm"
            txtStatus.Text = "ยืนยัน"
            SetMenu()
        Catch ex As Exception
            result = False
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return result
    End Function

    Private Sub btnAddDetail_Click(sender As Object, e As EventArgs) Handles btnAddDetail.Click
        Dim result As Boolean = True

        result = validateDetail()


        If result Then
            Try
                updatehead()
                AddDetails()
                cleardetail()
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                Dim javaScript As String = "alertWarning('AddDetail fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try


        End If
    End Sub
    Private Sub cleardetail()
        txtJobDetail.Text = ""
        'txtDueDate.Text = Now
        txtDueDate.Text = ""
        cboPolicy.SelectedIndex = -1
        cboSupplier.SelectedIndex = -1
        'cboPosition.SelectedIndex = -1
        txtCost.Text = "0"
        cboUnit.SelectedIndex = 1
        txtQuantity.Text = "1"
        txtAssetCode.Text = ""
        txtAssetName.Text = ""
        lblattatch.Text = ""
        lblnozzle.Text = ""
        lblnozzlehidden.Text = ""
    End Sub

    Private Sub updatehead()
        Dim userid As Double
        Dim jobowner As Double

        userid = Session("userid")

        If cboOwner.SelectedItem.Value = 0 Then
            jobowner = userid
        Else
            jobowner = cboOwner.SelectedItem.Value
        End If
        If maintable.Rows.Count > 0 Then
            'update
            With maintable.Rows(0)
                .Item("jobdate") = txtDocDate.Text
                .Item("jobowner") = jobowner
                .Item("branchid") = cboBranch.SelectedItem.Value
                .Item("depid") = cboDepartment.SelectedItem.Value
                .Item("secid") = cboSection.SelectedItem.Value
            End With
        Else
            'insert
            With maintable
                .Rows.Add(0, "", Date.Now.ToString, txtCreateBy.Text, txtDocDate.Text, jobowner,
                          0, "NEW", cboBranch.SelectedItem.Value, cboDepartment.SelectedItem.Value, cboSection.SelectedItem.Value, 0)
            End With

        End If
        ViewState("maintable") = maintable
    End Sub
    Private Sub AddDetails()

        Dim qty As Double
        Try
            qty = Double.Parse(txtQuantity.Text)
        Catch ex As Exception
            qty = 0
        End Try

        Dim cost As Double
        Try
            cost = Double.Parse(txtCost.Text)
        Catch ex As Exception
            cost = 0
        End Try
        Dim reqdate As String
        If String.IsNullOrEmpty(txtDueDate.Text) Then
            reqdate = DBNull.Value.ToString
        Else
            reqdate = txtDueDate.Text
        End If

        Dim urgent As Integer = 0

        Dim fileName As String = System.IO.Path.GetFileName(FileUpload1.FileName)
        Dim row As DataRow
        row = detailtable.NewRow()
        row("jobdetailid") = 0
        row("jobno") = txtJobno.Text
        row("jobtypeid") = cboJobType.SelectedItem.Value
        row("jobtype") = cboJobType.SelectedItem.Text
        'If cboPosition.SelectedIndex > -1 Then
        '    row("assetid") = cboPosition.SelectedItem.Value
        '    row("assetposition") = cboPosition.SelectedItem.Text
        'End If
        row("assetcode") = txtAssetCode.Text
        row("assetname") = txtAssetName.Text
        row("quantity") = qty
        row("unitid") = cboUnit.SelectedItem.Value
        row("unit") = cboUnit.SelectedItem.Text
        row("cost") = cost
        row("supplierid") = cboSupplier.SelectedItem.Value
        row("supplier") = cboSupplier.SelectedItem.Text
        row("policyid") = cboPolicy.SelectedItem.Value
        row("policy") = cboPolicy.SelectedItem.Text
        row("requestdate") = reqdate
        row("details") = txtJobDetail.Text
        row("statusid") = 0
        row("owner") = 0
        row("followup_status") = ""
        row("attatch") = lblattatch.Text
        row("brand") = txtBrand.Text
        row("model") = txtModel.Text
        row("nozzle") = lblnozzlehidden.Text
        'row("vendor_code") = ""

        detailtable.Rows.Add(row)
        'detailtable.Rows.Add(0, cboJobType.SelectedItem.Value, cboJobType.SelectedItem.Text, txtAssetCode.Text, txtAssetName.Text,
        '                    qty, cboUnit.SelectedItem.Value, cboUnit.SelectedItem.Text, cost, cboSupplier.SelectedItem.Value,
        '                    cboSupplier.SelectedItem.Text, urgent, cboPolicy.SelectedValue, reqdate, txtJobDetail.Text, 0)

        ViewState("detailtable") = detailtable

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If (FileUpload1.HasFile) Then
            Dim allow_send_pic() As String = {".jpg", ".png", ".gif", ".pdf"}
            Dim Extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
            If Array.IndexOf(allow_send_pic, Extension.ToLower()) = -1 Then
                Dim scriptKey As String = "alert"
                Dim javaScript As String = "alertWarning('Upload ได้เฉพาะ jpg png gif pdf');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Else
                If System.IO.File.Exists("D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\" & FileUpload1.FileName) Then
                    Dim scriptKey As String = "alert"
                    Dim javaScript As String = "alertWarning('ชื่อไฟล์ซ้ำกันไม่ได้');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Else
                    Dim attatchName As New jobs
                    Dim fileName As String
                    fileName = attatchName.GetAttatchName()
                    Dim rootPath As String = "D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\"
                    Dim savePath As String '= "D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\"
                    'Dim fileName As String = FileUpload1.FileName
                    savePath = rootPath
                    savePath += fileName
                    savePath += Extension
                    Do While System.IO.File.Exists(savePath)
                        fileName = attatchName.GetAttatchName()
                        savePath = rootPath
                        savePath += fileName
                        savePath += Extension
                    Loop
                    FileUpload1.SaveAs(savePath)
                    fileName += Extension
                    lblattatch.Text = fileName
                End If
            End If
        End If
    End Sub


    Private Function FindPositionInPump(branchid As String) As Boolean
        Dim objjobs As New jobs
        Try
            If cboJobType.SelectedItem.Value = 1 Then
                'objjobs.SetPositionInPumpList(cboPosition, branchid)
            End If
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
        Return True
    End Function

    Private Function setDueDate(policyid As String) As Boolean
        Dim objjobs As New jobs
        Dim mydataset As DataSet
        Try
            mydataset = objjobs.setDueDateByPolicyID(policyid, txtDocDate.Text)
            txtDueDate.Text = mydataset.Tables(0).Rows(0).Item("duedate")
            Return True
        Catch ex As Exception
            txtDueDate.Text = ""
            Return False
        End Try
    End Function

    Private Sub cboPolicy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPolicy.SelectedIndexChanged
        setDueDate(cboPolicy.SelectedItem.Value)
    End Sub

    Private Sub cboDepForJobType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepForJobType.SelectedIndexChanged

        cboJobType.SelectedIndex = -1
        SetCboJobTypeByDepID(cboJobType, cboDepForJobType.SelectedItem.Value, "actived")

        'SetCboJobCate(cboJobCate, cboDepForJobType.SelectedValue)

    End Sub
    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        If Not String.IsNullOrEmpty(cboBranch.SelectedItem.Value) Then
            branchid = cboBranch.SelectedItem.Value

            Dim objAssets As New Assets
            Dim objdep As New Department
            objdep.SetCboDepartment(cboDepartment, branchid)

            FindPositionInPump(branchid)
            findNozzle(cboBranch.SelectedItem.Value)
            objAssets.SetCboAssetsByName(cboAsset, 0)
        End If

    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection(cboSection, depid)


        'SetCboJobCate(cboJobCate, cboDepartment.SelectedValue)

    End Sub
    Private Sub cboJobType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobType.SelectedIndexChanged
        Dim objpolicy As New Policy

        jobtypeid = cboJobType.SelectedItem.Value
        objpolicy.setComboPolicyByJobTypeID(cboPolicy, jobtypeid)
        setDueDate(cboPolicy.SelectedItem.Value)
        If jobtypeid = 1 Then
            If Not cboBranch.SelectedItem.Value = "" Then
                FindPositionInPump(cboBranch.SelectedItem.Value)
            Else
                Dim scriptKey As String = "alert"
                Dim javaScript As String = "alertWarning('กรุณาเลือกสาขา');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        End If
        If cboJobType.SelectedItem.Text.ToString.IndexOf("ตีตรา") > -1 Then
            If Not cboBranch.SelectedItem.Value = "" Then
                findNozzle(cboBranch.SelectedItem.Value)
            Else
                Dim scriptKey As String = "alert"
                Dim javaScript As String = "alertWarning('กรุณาเลือกสาขา');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        End If

    End Sub

    Private Sub cboAsset_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAsset.SelectedIndexChanged
        txtAssetCode.Text = cboAsset.SelectedItem.Text.ToString.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)(0).Trim()
        FindAsset()
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function CancelByCode(ByVal jobno As String, ByVal message As String, ByVal updateby As String)
        Dim job As New jobs

        Dim result As Boolean
        Try
            result = job.Cancel(jobno, message, updateby)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function

    'Private Sub cboJobCate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobCate.SelectedIndexChanged
    '    Try
    '        'Dim objpolicy As New Policy

    '        'jobcateid = cboJobCate.SelectedItem.Value
    '        'objpolicy.setComboPolicyByJobTypeID(cboPolicy, jobcateid)
    '        'If jobtypeid = 1 Then
    '        '    If Not cboBranch.SelectedItem.Value = "" Then
    '        '        FindPositionInPump(cboBranch.SelectedItem.Value)
    '        '    Else
    '        '        Dim scriptKey As String = "alert"
    '        '        Dim javaScript As String = "alertWarning('กรุณาเลือกสาขา');"
    '        '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '        '    End If
    '        'End If

    '        SetCboJobGroup(cboJobGroup, cboJobCate.SelectedValue)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cboJobGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobGroup.SelectedIndexChanged
    '    Try
    '        'Dim objpolicy As New Policy

    '        'jobcateid = cboJobCate.SelectedItem.Value
    '        'objpolicy.setComboPolicyByJobTypeID(cboPolicy, jobcateid)
    '        'If jobtypeid = 1 Then
    '        '    If Not cboBranch.SelectedItem.Value = "" Then
    '        '        FindPositionInPump(cboBranch.SelectedItem.Value)
    '        '    Else
    '        '        Dim scriptKey As String = "alert"
    '        '        Dim javaScript As String = "alertWarning('กรุณาเลือกสาขา');"
    '        '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '        '    End If
    '        'End If

    '        SetCboJobDescription(cboJobType, cboJobGroup.SelectedValue)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    Private Sub BindDataAssetsNozzle()
        'cntdt = assetsNozzletable.Rows.Count
        cntdt = assetsNozzletable.Select("jobref is null or jobref=''").Length
        gvAssetsNozzle.Caption = "ทั้งหมด " & cntdt & " รายการ"
        gvAssetsNozzle.DataSource = assetsNozzletable
        gvAssetsNozzle.DataBind()
    End Sub
    Private Sub BindDataNozzle()
        cntdt = nozzletable.Rows.Count
        gvNozzle.Caption = "ทั้งหมด " & cntdt & " รายการ"
        gvNozzle.DataSource = nozzletable
        gvNozzle.DataBind()
    End Sub
    Private Sub findNozzle(branchid As Integer)
        Dim objassets As New Assets

        Try
            If cboJobType.SelectedItem.Text.ToString.IndexOf("ตีตรา") > -1 Then
                assetsNozzletable = objassets.AssesNozzle_list(branchid)
                BindDataAssetsNozzle()
                ViewState("assetsNozzletable") = assetsNozzletable
            End If
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find nozzle fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnSetNozzle_Click(sender As Object, e As EventArgs) Handles btnSetNozzle.Click
        Dim confirmValue As String = Request.Form("setNozzle")
        'Dim rawresp As String = "[{""id"":""1""},{""id"":""2""},{""id"":""3""},{""id"":""4""},{""id"":""5""},{""id"":""6""},{""id"":""7""}]"

        Dim result = JsonConvert.DeserializeObject(Of ArrayList)(confirmValue)
        Dim token As JToken
        Dim positionAll As String = ""
        Dim codeAll As String = ""
        Dim position
        Dim code
        If result IsNot Nothing Then

            Dim objjob As New jobs

            Dim jyncode As String = objjob.Jobs_Get_RunningNO_JTN()
            nozzletable = createnozzletable()

            For Each value As Object In result
                token = JObject.Parse(value.ToString())
                position = token.SelectToken("position")
                code = token.SelectToken("code")

                positionAll = positionAll & "(" & position & "),"
                codeAll = codeAll & code & ","


                Dim row As DataRow
                row = nozzletable.NewRow()
                With assetsNozzletable.Rows(assetsNozzletable.Rows.IndexOf(assetsNozzletable.Select("nozzle_No='" & code & "'")(0)))
                    row("nozzle_ID") = .Item("nozzle_ID")
                    row("nozzle_No") = .Item("nozzle_No")
                    row("branchid") = .Item("branchid")
                    row("rowNumber") = .Item("rowNumber")
                    row("brand") = .Item("brand")
                    row("expiryDate") = .Item("expiryDate")
                    row("productType") = .Item("productType")
                    row("positionOnAssest") = .Item("positionOnAssest")
                    row("url") = .Item("url")
                    row("active") = .Item("active")
                    row("AssetID") = .Item("AssetID")
                    row("UpdateBycode") = .Item("UpdateBycode")
                    row("UpdateBy") = .Item("UpdateBy")
                    row("UpdateDate") = .Item("UpdateDate")
                    row("jobref") = .Item("jobref")
                End With
                nozzletable.Rows.Add(row)

            Next value
            'BindData()
            lblnozzle.Text = positionAll
            lblnozzle.ToolTip = "จำนวน (" & result.Count & ") มือจ่าย"
            lblnozzlehidden.Text = codeAll

            BindDataNozzle()
            ViewState("nozzletable") = nozzletable
        End If
endprocess:
    End Sub

    Private Sub gvAssetsNozzle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAssetsNozzle.RowDataBound
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Not String.IsNullOrEmpty(Data.Item("jobref").ToString) Then
                e.Row.Visible = False
            End If
        End If
    End Sub

    Private Sub txtDocDate_TextChanged(sender As Object, e As EventArgs) Handles txtDocDate.TextChanged
        If cboPolicy.SelectedItem.Value > 0 Then
            setDueDate(cboPolicy.SelectedItem.Value)
        End If
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function approveHierachy(ByVal jobdtlid As String, ByVal type As String, ByVal updateby As String)
        Dim job As New jobs

        Dim result As Boolean
        Try
            result = job.JobsDetails_Approval(jobdtlid, type, updateby)

        Catch ex As Exception
            Return False
        End Try
        Return "success"

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function rejectHierachy(ByVal jobdtlid As String, ByVal message As String, ByVal type As String, ByVal updateby As String)
        Dim job As New jobs

        Dim result As Boolean
        Try
            result = job.JobsDetails_Reject(jobdtlid, type, message, updateby)

        Catch ex As Exception
            Return False
        End Try
        Return "success"

    End Function

    Public Function hasPermisstionApprove(dtlid As Integer, type As String) As Boolean
        Dim objjobs As New jobs
        Dim allApprover As String

        If detailtable IsNot Nothing AndAlso detailtable.Rows.Count > 0 Then
            ' ค้นหาแถวที่ dtlid ตรงกัน
            Dim foundRows = detailtable.Select("jobdetailid = " & dtlid)

            ' ตรวจสอบว่ามีแถวที่พบและค่า followup_status เป็น "รออนุมัติตามสายบังคับบัญชา"
            If Not foundRows.Length > 0 Then Return False
            If Not (foundRows(0).Item("followup_status").ToString() = "รออนุมัติตามสายบังคับบัญชา" And type = "hierarchy") And Not (foundRows(0).Item("followup_status").ToString() = "รออนุมัติจากหน่วยงาน" And type = "manageroperator") Then Return False
        End If

        Try
            allApprover = objjobs.JobsDetails_PermisstionApprove(dtlid, type)
            Dim approvers As String() = allApprover.Split(","c)

            ' Check if the usercode exists in the array of approvers
            Return approvers.Contains(Session("usercode").ToString())
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class