Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmJobs
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public detailtable As DataTable '= createdetailtable()
    Public maintable As DataTable '= createmaintable()
    Public menutable As DataTable
    Public owner As Integer
    Public branchid As Integer
    Public jobtypeid As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier
        Dim objpolicy As New Policy

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        txtDocDate.Text = Now
        txtDocDate.Attributes.Add("readonly", "readonly")
        'Session("username") = "PAB"

        txtDueDate.Attributes.Add("readonly", "readonly")
        owner = 0

        Dim usercode As String
        usercode = Session("usercode")

        chkuser(usercode)

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If



        If Not IsPostBack() Then
            detailtable = createdetailtable()
            maintable = createmaintable()
            Session("detailtable") = detailtable
            Session("maintable") = maintable
            Session("status") = "new"
            objStatus = "new"

            ClearText()
            objbranch.SetComboBranch(cboBranch, usercode)
            objdep.SetCboDepartment(cboDepartment, 0)
            objdep.SetCboDepartmentforjobtype(cboDepForJobType)
            objsec.SetCboSection(cboSection, 0)
            objsupplier.SetCboSupplier(cboSupplier)
            'objdep.SetCboDepartment(cboDepartment, 0)

            'SetCboBranch(cboBranch)
            'SetCboDepartment(cboDepartment)
            'SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
            SetCboJobType(cboJobType, usercode)
            SetCboUnit(cboUnit)
            SetCboUsers(cboOwner)
            'Session("jobtype") = cboJobType.SelectedItem.Value

            jobtypeid = cboJobType.SelectedItem.Value
            objpolicy.setComboPolicyByJobTypeID(cboPolicy, cboJobType.SelectedItem.Value)

            If Not Request.QueryString("jobno") Is Nothing Then
                'txtDocDate.Text = Now()
                Session("jobno") = Request.QueryString("jobno")
                txtJobno.Text = Session("jobno")
                FindJobNo(txtJobno.Text)

            Else
                If Not Session("branchid") Is Nothing Then
                    cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid")))
                    If cboJobType.SelectedItem.Value = 1 Then
                        FindPositionInPump(cboBranch.SelectedItem.Value)
                    End If
                End If
                Session("status") = "new"
                objStatus = "new"
                txtCreateBy.Text = Session("username")
                txtCreateDate.Text = Now()
            End If

            SetMenu()
        Else
            objStatus = Session("status")

            'If Not (Session("detailtable") Is Nothing) Then
            detailtable = Session("detailtable")
            maintable = Session("maintable")
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
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript ,true)
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
        Session("jobno") = jobno
        'itemtable
        Try
            mydataset = job.Find(usercode, jobno)
            maintable = mydataset.Tables(0)
            detailtable = mydataset.Tables(1)
            'itemtable = mydataset.Tables(1)
            'Session("itemtable") = itemtable

            If maintable.Rows.Count > 0 Then
                showdata(maintable)
                Session("maintable") = mydataset.Tables(0)
                Session("detailtable") = mydataset.Tables(1)
            End If
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
                Case = 1
                    Session("status") = "edit"
                    objStatus = "edit"
                Case = 2
                    Session("status") = "confirm"
                    objStatus = "confirm"
                Case = 3
                    Session("status") = "action"
                    objStatus = "action"
                Case = 4
                    Session("status") = "close"
                    objStatus = "close"
                Case = 5
                    Session("status") = "cancel"
                    objStatus = "cancel"
            End Select

            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(.Item("branchid")))
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(.Item("depid")))
            'SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
            cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(.Item("secid")))



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
    Private Sub SetMenu()
        Select Case objStatus
            Case = "new"
                btnSave.Enabled = True
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = False
                'btnCancel.Enabled = False
            Case = "edit"
                If owner = 1 Then
                    btnSave.Enabled = True
                    btnConfirm.Enabled = True
                Else
                    btnSave.Enabled = False
                    btnConfirm.Enabled = False
                End If
                btnPrint.Enabled = True
                'btnCancel.Enabled = True
            Case = "confirm"
                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                'btnCancel.Enabled = True
            Case = "action"
                btnSave.Enabled = False
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                'btnCancel.Enabled = False
            Case = "close"
                btnSave.Enabled = False
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                'btnCancel.Enabled = False
            Case = "cancel"
                btnSave.Enabled = False
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False
                btnPrint.Enabled = True
                'btnCancel.Enabled = False
                'Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "<script type='text/javascript'>chkButtonCancel('btnCancel'," & Session("status") & ");"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "chkButtonCancel('btnCancel'," & Session("status") & ")", True)
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
        Session("detailtable") = detailtable
        Session("maintable") = maintable
        Session("status") = "new"
        Session("jobno") = Nothing


        detailtable.Rows.Clear()
    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validatedata() Then
            'If Session("status") = "new" Then
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
            msg = "กรุณา ใส่รายละเอียดงาน ในการแจ้ง"
            GoTo endprocess
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('validatedata Fail')"
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
            Session("jobno") = jobno
            Session("status") = "edit"
            objStatus = "edit"
            txtStatus.Text = "แจ้งงาน"
            Response.Redirect("../OPS/jobs.aspx?jobno=" & jobno)


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
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
    '        Session("jobcode") = jobcode
    '        Session("status") = "edit"
    '    Catch ex As Exception
    '        Dim scriptKey As String = "UniqueKeyForThisScript"
    '        Dim javaScript As String = "<script type='text/javascript'>msgalert('" & ex.Message & "');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
    '    End Try
    'End Sub
    Private Function validateDetail() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If cboJobType.SelectedItem.Value = 1 Then
            If cboPosition.SelectedIndex < 0 Then
                result = False
                msg = "กรุณาเลือกตำแหน่งตู้"
                GoTo endprocess
            End If
        ElseIf cboJobType.SelectedItem.Value = 16 Then
            If String.IsNullOrEmpty(txtAssetName.Text) And String.IsNullOrEmpty(txtbrand.Text) And String.IsNullOrEmpty(txtModel.Text) Then
                result = False
                msg = "กรุณาระบุรหัสทรัพสินทร์ หรือ ชื่อยี่ห้อ หรือ รุ่น"
                GoTo endprocess
            End If
        End If
        If Not cboPolicy.SelectedItem.Value = 0 Then
            If String.IsNullOrEmpty(txtDueDate.Text) Then
                result = False
                msg = "กรุณาระบุวันที่ต้องการ"
                GoTo endprocess
            End If
        End If
        If String.IsNullOrEmpty(txtJobDetail.Text) Then
            result = False
            msg = "กรุณาระบุรายละเอียด"
            GoTo endprocess
        End If
        If cboBranch.SelectedItem.Value = "" Then
            result = False
            msg = "กรุณาเลือกสาขา"
            GoTo endprocess
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
        Session("status") = "new"
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


            Session("rpt") = rpt
            Response.Write("<script>")
            Response.Write("window.open('../ReportViewer.aspx','_blank')")
            Response.Write("</script>")


            'Me.CrystalReportViewer1.ReportSource = Session("rpt")
            'Me.CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alert('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try



    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
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
            result = job.EmailList(Session("jobno"), jobtypeid)
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
            Session("status") = "confirm"
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


        If result = True Then
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
        cboPosition.SelectedIndex = -1
        txtCost.Text = "0"
        cboUnit.SelectedIndex = 1
        txtQuantity.Text = "1"
        txtAssetCode.Text = ""
        txtAssetName.Text = ""
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
        Session("maintable") = maintable
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
        If cboJobType.SelectedItem.Value = 1 Then
            row("assetid") = cboPosition.SelectedItem.Value
            row("assetposition") = cboPosition.SelectedItem.Text
        End If
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

        detailtable.Rows.Add(row)
        'detailtable.Rows.Add(0, cboJobType.SelectedItem.Value, cboJobType.SelectedItem.Text, txtAssetCode.Text, txtAssetName.Text,
        '                    qty, cboUnit.SelectedItem.Value, cboUnit.SelectedItem.Text, cost, cboSupplier.SelectedItem.Value,
        '                    cboSupplier.SelectedItem.Text, urgent, cboPolicy.SelectedValue, reqdate, txtJobDetail.Text, 0)

        Session("detailtable") = detailtable

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
                    Dim savePath As String = "D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\"
                    'Dim fileName As String = FileUpload1.FileName
                    savePath += fileName
                    savePath += Extension
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
            objjobs.SetPositionInPumpList(cboPosition, branchid)
            Return True
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function setDueDate(policyid As String) As Boolean
        Dim objjobs As New jobs
        Dim mydataset As DataSet
        Try
            mydataset = objjobs.setDueDateByPolicyID(policyid)
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
        SetCboJobTypeByDepID(cboJobType, cboDepForJobType.SelectedItem.Value)
    End Sub
    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        branchid = cboBranch.SelectedItem.Value

        Dim objdep As New Department
        objdep.SetCboDepartment(cboDepartment, branchid)

        If cboJobType.SelectedItem.Value = 1 Then
            FindPositionInPump(branchid)
        End If
    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection(cboSection, depid)
    End Sub
    Private Sub cboJobType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobType.SelectedIndexChanged
        Dim objpolicy As New Policy

        jobtypeid = cboJobType.SelectedItem.Value
        objpolicy.setComboPolicyByJobTypeID(cboPolicy, jobtypeid)
        If jobtypeid = 1 Then
            If Not cboBranch.SelectedItem.Value = "" Then
                FindPositionInPump(cboBranch.SelectedItem.Value)
            Else
                Dim scriptKey As String = "alert"
                Dim javaScript As String = "alertWarning('กรุณาเลือกสาขา');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        End If
    End Sub
End Class