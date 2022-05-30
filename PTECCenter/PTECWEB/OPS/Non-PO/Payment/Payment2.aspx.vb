Imports System.IO
Imports System.Web.Script.Serialization
Imports ClosedXML.Excel
Public Class Payment2
    Inherits System.Web.UI.Page
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public detailtable As DataTable '= createdetailtable()
    Public maintable As DataTable '= createmaintable()
    Public head As DataTable
    Public menutable As DataTable
    Public PermissionOwner As DataSet '= createtable()
    Public total_cost As String
    Public total_completebill As String
    Public total_incompletebill As String
    Public total_vat As String
    Public total_tax As String
    Public total As String
    Public chkunsave As Integer = 0


    Public verify As Boolean = False
    Public approval As Boolean = False
    Public flag As Boolean = True

    Public allOwner As String
    Public at As String
    Public approver As String
    Public verifier As String
    Public now_action As String

    Dim md_code As String
    Dim fm_code As String
    Dim dm_code As String
    Dim sm_code As String
    Dim am_code As String

    Public account_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objNonpo As New NonPO
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier

        Dim nonpoDs = New DataSet
        Dim statusid As Integer = 0
        Dim createby As Integer = 0

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
        usercode = Session("usercode")


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        txtDuedate.Attributes.Add("readonly", "readonly")
        txtinvoicedate.Attributes.Add("readonly", "readonly")

        If Not IsPostBack() Then
            head = createHeadtable()
            detailtable = createdetailtable()
            maintable = createmaintable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()



            'insert test comment
            'With CommentTable
            '    .Rows.Add(0, "test", "ธนพล", Now(), "ธนพล", Now(), "ADV201000001")
            '    .Rows.Add(1, "test1", "ธนพล", Now(), "ธนพล", Now(), "ADV201000001")
            '    .Rows.Add(2, "test23", "ทศกัน", Now(), "ทศกัน", Now(), "ADV201000001")
            'End With

            'insert test AttachTable
            'With AttachTable
            '    .Rows.Add(0, "test", "http://google.com/", "tttt")
            '    .Rows.Add(1, "test1", "http://google.com/", "ADV201000001")
            'End With


            objbranch.SetComboBranch(cboBranch, Session("usercode"))
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
            SetCboUsers(cboOwner)
            setmaindefault()

            objdep.SetCboDepartmentBybranch(cboDep, 0)
            objNonpo.SetCboPurpose(cboPP)
            objNonpo.SetCboAccountCode(cboAccountCode, Session("userid"))
            'objsupplier.SetCboVendor(cboVendor, "")
            objsupplier.SetCboVendorByName(cboVendor, "")
            objNonpo.SetCboBu(cboBU)
            objNonpo.SetCboPj(cboPJ)

            If Not Request.QueryString("NonpoCode") Is Nothing Then
                Session("detailtable_payment") = detailtable
                'If Not Session("status_payment") = "edit" Then
                Session("status_payment") = "read"
                ' End If
                Try
                    findNonPO()

                    statusid = maintable.Rows(0).Item("statusid")
                    createby = maintable.Rows(0).Item("createby")
                    chkuser(createby, Request.QueryString("NonpoCode"))

                    account_code = objNonpo.NonPOPermisstionAccount(Request.QueryString("NonpoCode"))

                    If (account_code.IndexOf(Session("usercode").ToString) > -1) And
                    (maintable.Rows(0).Item("statusid") = 7) Then
                        Session("status_payment") = "account"

                    End If

                    If (maintable.Rows(0).Item("statusid") = 2 Or maintable.Rows(0).Item("statusid") = 15) Then
                        PermissionOwner = chkPermissionNonPO(Request.QueryString("NonpoCode"))

                        at = "วิ่งเส้น : " + PermissionOwner.Tables(0).Rows(0).Item("at").ToString
                        approver = "ผู้มีสิทธิอนุมัติ : " + PermissionOwner.Tables(0).Rows(0).Item("approver").ToString
                        verifier = "ผู้ตรวจ : " + PermissionOwner.Tables(0).Rows(0).Item("verifier").ToString
                        now_action = "ผู้ที่ต้องปฏิบัติงาน : " + PermissionOwner.Tables(0).Rows(1).Item("approver").ToString + PermissionOwner.Tables(0).Rows(1).Item("verifier").ToString
                    End If

                    If (Session("usercode") = md_code Or
                    Session("usercode") = fm_code Or
                    Session("usercode") = dm_code Or
                    Session("usercode") = sm_code Or
                    Session("usercode") = am_code) And
                    (maintable.Rows(0).Item("statusid") = 2 Or maintable.Rows(0).Item("statusid") = 15) Then
                        Session("status_payment") = "write"
                        'Dim SearchWithinThis As String = "ABCDEFGHIJKLMNOP"
                        'Dim SearchForThis As String = "DEF"
                        'Dim FirstCharacter As Integer = SearchWithinThis.IndexOf(SearchForThis)





                        For Each row As DataRow In PermissionOwner.Tables(0).Rows
                            If row("status").ToString = "now" Then
                                If row("approver").ToString.IndexOf("MD") > -1 Then
                                    If (md_code.IndexOf(Session("usercode")) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("FM") > -1 Then
                                    If (fm_code.IndexOf(Session("usercode")) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("DM") > -1 Then
                                    If (dm_code.IndexOf(Session("usercode")) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("SM") > -1 Then
                                    If (sm_code.IndexOf(Session("usercode")) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("AM") > -1 Then
                                    If (am_code.IndexOf(Session("usercode")) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If maintable.Rows(0).Item("statusid") = 2 Then
                                    If row("verifier").ToString.IndexOf("MD") > -1 Then
                                        If (md_code.IndexOf(Session("usercode")) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("FM") > -1 Then
                                        If (fm_code.IndexOf(Session("usercode")) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("DM") > -1 Then
                                        If (dm_code.IndexOf(Session("usercode")) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("SM") > -1 Then
                                        If (sm_code.IndexOf(Session("usercode")) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("AM") > -1 Then
                                        If (am_code.IndexOf(Session("usercode")) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    End If
                                End If
                            End If

                        Next row
endprocess:

                    End If

                    sethead(head)
                    setmain(maintable)
                    'SetBtn(1)
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            ElseIf Not Request.QueryString("f") Is Nothing Then
                Dim objjob As New jobs
                Dim ds As New DataSet
                If Not Request.QueryString("code_ref") Is Nothing And Request.QueryString("code_ref_dtl") Is Nothing Then
                    Session("status_payment") = "new"
                    codeRef.Text = Request.QueryString("code_ref").ToString
                    ds = objjob.setNonPODtl_by_coderef(Request.QueryString("f").ToString, Request.QueryString("code_ref").ToString, "", Session("usercode").ToString)
                    head = ds.Tables(0)
                    sethead(head)
                End If
            Else
                Session("status_payment") = "new"

            End If



            Session("head_payment") = head
            Session("detailtable_payment") = detailtable
            Session("maintable_payment") = maintable
            Session("comment_payment") = CommentTable
            Session("attatch_payment") = AttachTable
        Else

            If Not String.IsNullOrEmpty(Request.QueryString("NonpoCode")) Then
                account_code = objNonpo.NonPOPermisstionAccount(Request.QueryString("NonpoCode"))
            End If

            head = Session("head_payment")
            detailtable = Session("detailtable_payment")
            maintable = Session("maintable_payment")
            AttachTable = Session("attatch_payment")
            CommentTable = Session("comment_payment")

            Try
                statusid = maintable.Rows(0).Item("statusid")
                createby = maintable.Rows(0).Item("createby")

            Catch ex As Exception
                statusid = 0
                createby = 0
            End Try
            Dim target = Request.Form("__EVENTTARGET")
            If target = "deletedetail" Then
                Dim argument As String = Request("__EVENTARGUMENT")
                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(argument)
                deleteDetail(json("nonpodtlid"), json("rows"), json("user"))
            End If
        End If
        SetBtn(statusid, createby)
        'SetMenu()
        checkunsave()
    End Sub
    Private Sub chkuser(userid As Integer, nonpocode As String)
        Dim objuser As New Users
        Dim NonPOPermissionTable As New DataTable
        Try
            NonPOPermissionTable = objuser.NonPOPermissionRead(userid, nonpocode)
            md_code = NonPOPermissionTable.Rows(0).Item("md_code")
            fm_code = NonPOPermissionTable.Rows(0).Item("fm_code")
            dm_code = NonPOPermissionTable.Rows(0).Item("dm_code")
            sm_code = NonPOPermissionTable.Rows(0).Item("sm_code")
            am_code = NonPOPermissionTable.Rows(0).Item("am_code")
            chkuser_sub(md_code, userid)
            chkuser_sub(fm_code, userid)
            chkuser_sub(dm_code, userid)
            chkuser_sub(sm_code, userid)
            chkuser_sub(am_code, userid)
            allOwner = "[ md : " + md_code + "],[ fm : " + fm_code + "],[ dm : " + dm_code + "],[ sm : " + sm_code + "],[ am : " + am_code + "]"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub chkuser_sub(allusercode As String, userid As Integer)

        Dim strSplit As Array
        Dim i As Integer

        strSplit = allusercode.Split(",")

        For i = 0 To strSplit.Length - 1
            If Not Session("userid") = userid And
                Not Session("usercode") = strSplit(i) And
                Not Session("secid").ToString = "2" And
                Not Session("depid").ToString = "2" And
                Not Session("depid").ToString = "4" Then
                flag = False
            End If

        Next
    End Sub

    Private Function chkPermissionNonPO(nonpocode As String) As DataSet
        Dim objnonpo As New NonPO
        Dim npoPermission As New DataSet
        Try
            npoPermission = objnonpo.NonPOPermission(nonpocode)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('NonPO_PermisstionOwner fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return npoPermission
    End Function

    Private Sub sethead(dt As DataTable)
        With dt

            codeRef.Text = .Rows(0).Item("coderef").ToString
            'amount.Text = String.Format("{0:n2}", .Rows(0).Item("amount"))
            txtremark.Text = .Rows(0).Item("remark").ToString

        End With

    End Sub

    Private Sub setmaindefault()

        Dim objsec As New Section

        cboOwner.SelectedIndex = cboOwner.Items.IndexOf(cboOwner.Items.FindByValue(Session("userid").ToString))
        cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid").ToString))
        cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
        objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
        cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(Session("secid").ToString))

        txtCreateDate.Text = Now()

        cboOwner.Attributes.Add("disabled", "True")
        cboSection.Attributes.Add("disabled", "True")
        cboDepartment.Attributes.Add("disabled", "True")
        cboBranch.Attributes.Add("disabled", "True")


    End Sub

    Private Sub setmain(dt As DataTable)

        Dim objsec As New Section
        With dt
            Select Case .Rows(0).Item("statusid").ToString
                Case = "1" '1 : รอยืนยัน
                    statusnonpo.Attributes.Add("class", "btn btn-info")
                    If .Rows(0).Item("createby").ToString = Session("userid") Then
                        Session("status_payment") = "edit"
                    End If
                Case = "2" '2 : รออนุมัติ
                    statusnonpo.Attributes.Add("class", "btn btn-warning")
                Case = "4" '4 : ขอเอกสารเพิ่มเติม
                    statusnonpo.Attributes.Add("class", "btn btn-info")
                Case = "5" '5 : ไม่ผ่านการตรวจสอบ
                    statusnonpo.Attributes.Add("class", "btn btn-danger")
                Case = "6" '6 : ไม่ผ่าน
                    statusnonpo.Attributes.Add("class", "btn btn-danger")
                Case = "7" '7 : รอบัญชีตรวจสอบ
                    statusnonpo.Attributes.Add("class", "btn btn-warning")
                Case = "8" '8 : รอเอกสารตัวจริง
                    statusnonpo.Attributes.Add("class", "btn btn-warning")
                Case = "9" '9 : ได้รับเอกสารตัวจริง
                    statusnonpo.Attributes.Add("class", "btn btn-secondary")
                Case = "14" '14 : ยกเลิก
                    statusnonpo.Attributes.Add("class", "btn btn-danger")
                Case = "15" '15 : รอตราวจสอบ
                    statusnonpo.Attributes.Add("class", "btn btn-warning")
                Case = "17" '17 : การเงินได้รับเอกสาร
                    statusnonpo.Attributes.Add("class", "btn btn-secondary")
            End Select
            statusnonpo.Text = .Rows(0).Item("statusname").ToString


            cboOwner.Attributes.Remove("disabled")
            cboSection.Attributes.Remove("disabled")
            cboDepartment.Attributes.Remove("disabled")
            cboBranch.Attributes.Remove("disabled")

            cboOwner.SelectedIndex = cboOwner.Items.IndexOf(cboOwner.Items.FindByValue(.Rows(0).Item("createby").ToString))
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(.Rows(0).Item("branchid").ToString))
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(.Rows(0).Item("depid").ToString))
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
            cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(.Rows(0).Item("secid").ToString))

            txtCreateDate.Text = .Rows(0).Item("createdate").ToString
            txtCreateDate.ToolTip = .Rows(0).Item("createdate").ToString
            txtpmno.Text = .Rows(0).Item("nonpocode").ToString
            txtpmno.ToolTip = .Rows(0).Item("nonpocode").ToString


            cboOwner.Attributes.Add("disabled", "True")
            cboSection.Attributes.Add("disabled", "True")
            cboDepartment.Attributes.Add("disabled", "True")
            cboBranch.Attributes.Add("disabled", "True")

            If .Rows(0).Item("payby").ToString = "cheque" Then
                chkCheque.Checked = True
            ElseIf .Rows(0).Item("payby").ToString = "chequecounter" Then
                chkChequeCounter.Checked = True
            ElseIf .Rows(0).Item("payby").ToString = "cashiercheque" Then
                chkCashierCheque.Checked = True
            ElseIf .Rows(0).Item("payby").ToString = "tt" Then
                chkTT.Checked = True
            ElseIf .Rows(0).Item("payby").ToString = "eft" Then
                chkEFT.Checked = True
            ElseIf .Rows(0).Item("payby").ToString = "deductsell" Then
                chkdeductSell.Checked = True
            ElseIf .Rows(0).Item("payby").ToString = "pcx" Then
                chkPXC.Checked = True
            End If

            txtDuedate.Text = .Rows(0).Item("duedate").ToString
            txtNote.Text = .Rows(0).Item("detail").ToString

            cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue(.Rows(0).Item("vendorcode").ToString))

            chkVat.Checked = .Rows(0).Item("vat_wait")
            If (Session("status_payment") = "new" Or Session("status_payment") = "edit" Or Session("status_payment") = "account") Then
                cboVendor.Attributes.Remove("disabled")

                chkCheque.Attributes.Remove("disabled")
                chkChequeCounter.Attributes.Remove("disabled")
                chkCashierCheque.Attributes.Remove("disabled")
                chkTT.Attributes.Remove("disabled")
                chkEFT.Attributes.Remove("disabled")
                chkPXC.Attributes.Remove("disabled")
                chkdeductSell.Attributes.Remove("disabled")

                If Not Session("status_payment") = "account" Then
                    txtNote.Attributes.Remove("readonly")
                Else
                    txtNote.Attributes.Add("readonly", "readonly")
                End If

            Else
                cboVendor.Attributes.Add("disabled", "True")

                chkCheque.Attributes.Add("disabled", "True")
                chkChequeCounter.Attributes.Add("disabled", "True")
                chkCashierCheque.Attributes.Add("disabled", "True")
                chkTT.Attributes.Add("disabled", "True")
                chkEFT.Attributes.Add("disabled", "True")
                chkPXC.Attributes.Add("disabled", "True")
                chkdeductSell.Attributes.Add("disabled", "True")

                txtNote.Attributes.Add("readonly", "readonly")
            End If
        End With


    End Sub
    'Private Sub SetMenu()
    '    Select Case Session("status_payment")
    '        Case = "new"
    '            'ช่อง ปุ่ม เพิ่มรายการ
    '            btnFromAddDetail.Visible = True
    '            FromAddDetail.Visible = True

    '            'ปุ่ม & status 
    '            statusnonpo.Visible = False

    '            'กล่อง comment & attatch file
    '            card_comment.Visible = False
    '            card_attatch.Visible = False
    '        Case = "read"
    '            'ช่อง ปุ่ม เพิ่มรายการ
    '            btnFromAddDetail.Visible = True
    '            FromAddDetail.Visible = True

    '            'ปุ่ม & status 
    '            statusnonpo.Visible = False

    '            'กล่อง comment & attatch file
    '            card_comment.Visible = False
    '            card_attatch.Visible = False
    '        Case = "edit"
    '            'ช่อง ปุ่ม เพิ่มรายการ
    '            btnFromAddDetail.Visible = True
    '            FromAddDetail.Visible = True

    '            'ปุ่ม & status 
    '            statusnonpo.Visible = False

    '            'กล่อง comment & attatch file
    '            card_comment.Visible = True
    '            card_attatch.Visible = True
    '        Case = "write"
    '            'ช่อง ปุ่ม เพิ่มรายการ
    '            btnFromAddDetail.Visible = False
    '            FromAddDetail.Visible = False

    '            'ปุ่ม & status 
    '            statusnonpo.Visible = True

    '            'กล่อง comment & attatch file
    '            card_comment.Visible = True
    '            card_attatch.Visible = True
    '        Case = "account"
    '            'ช่อง ปุ่ม เพิ่มรายการ
    '            btnFromAddDetail.Visible = True
    '            FromAddDetail.Visible = True

    '            'ปุ่ม & status 
    '            statusnonpo.Visible = True

    '            'กล่อง comment & attatch file
    '            card_comment.Visible = True
    '            card_attatch.Visible = True
    '    End Select


    'End Sub

    Private Sub checkunsave()
        For i = 0 To detailtable.Rows.Count - 1
            If detailtable.Rows(i).Item("nonpodtl_id") = 0 Or detailtable.Rows(i).Item("row") = 0 Or Not detailtable.Rows(i).Item("status") = "read" Then
                chkunsave = 1
                GoTo endprocess
            End If
        Next i
endprocess:
    End Sub
    Private Sub SetBtn(statusid As String, createby As Integer)
        Select Case statusid
            Case = "0" '0 : new

                btnSave.Enabled = True
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnExport.Visible = False
                btnPrint.Visible = False

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = True
                FromAddDetail.Visible = True
                btnAddDetails.Visible = True

                'ปุ่ม & status 
                statusnonpo.Visible = False

                'กล่อง comment & attatch file
                card_comment.Visible = False
                card_attatch.Visible = False

                btnAddAttatch.Visible = False
            Case = "1" '1 : รอยืนยัน
                If createby = Session("userid") Then
                    btnSave.Enabled = False
                    btnUpdate.Enabled = True
                    btnConfirm.Enabled = True
                    btnCancel.Enabled = True

                    Session("status_payment") = "edit"

                    'ช่อง ปุ่ม เพิ่มรายการ

                    btnFromAddDetail.Visible = True
                    btnAddDetails.Visible = True
                    FromAddDetail.Visible = True
                Else
                    btnSave.Enabled = False
                    btnUpdate.Enabled = False
                    btnConfirm.Enabled = False
                    btnCancel.Enabled = False

                    Session("status_payment") = "read"

                    'ช่อง ปุ่ม เพิ่มรายการ

                    btnFromAddDetail.Visible = False
                    FromAddDetail.Visible = False
                    btnAddDetails.Visible = False


                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "disbtndelete()"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End If


                btnExport.Visible = False
                btnPrint.Visible = True

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = True
            Case = "2" '2 : รออนุมัติ
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                If createby = Session("userid") Then
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If

                btnExport.Visible = True
                btnPrint.Visible = True

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                btnAddDetails.Visible = False
                FromAddDetail.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "4" '4 : ขอเอกสารเพิ่มเติม
                If createby = Session("userid") Then
                    btnSave.Enabled = False
                    btnUpdate.Enabled = False
                    btnConfirm.Enabled = True
                    btnCancel.Enabled = True

                    btnAddAttatch.Visible = True

                Else
                    btnSave.Enabled = False
                    btnUpdate.Enabled = False
                    btnConfirm.Enabled = False
                    btnCancel.Enabled = False

                    btnAddAttatch.Visible = False

                End If
                Session("status_payment") = "read"


                btnExport.Visible = False
                btnPrint.Visible = True

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                btnAddDetails.Visible = False
                FromAddDetail.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True


                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "5" '5 : ไม่ผ่านการตรวจสอบ (ไม่ใช่แล้ว)
                'btnSave.Enabled = False
                'btnUpdate.Enabled = False
                'btnConfirm.Enabled = False
                'btnCancel.Enabled = False
            Case = "6" '6 : ไม่ผ่าน
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False


                btnExport.Visible = False
                btnPrint.Visible = True

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                btnAddDetails.Visible = False
                FromAddDetail.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "7" '7 : รอบัญชีตรวจสอบ

                btnExport.Visible = False
                btnPrint.Visible = True

                'ช่อง ปุ่ม เพิ่มรายการ
                If account_code.IndexOf(Session("usercode").ToString) > -1 Then

                    btnExport.Visible = True

                    btnSave.Enabled = False
                    btnUpdate.Enabled = True
                    btnConfirm.Enabled = False
                    btnCancel.Enabled = False

                    btnFromAddDetail.Visible = True
                    FromAddDetail.Visible = True
                    btnAddDetails.Visible = True

                    btnAddAttatch.Visible = True
                Else
                    btnSave.Enabled = False
                    btnUpdate.Enabled = False
                    btnConfirm.Enabled = False
                    btnCancel.Enabled = False

                    btnFromAddDetail.Visible = False
                    FromAddDetail.Visible = False
                    btnAddDetails.Visible = False

                    btnAddAttatch.Visible = False

                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "disbtndelete()"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End If

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "8" '8 : รอเอกสารตัวจริง
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False


                btnExport.Visible = False
                btnPrint.Visible = True

                If account_code.IndexOf(Session("usercode").ToString) > -1 Then
                    btnExport.Visible = True
                End If

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False
                btnAddDetails.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "9" '9 : ได้รับเอกสารตัวจริง
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False


                btnExport.Visible = False
                btnPrint.Visible = True

                If account_code.IndexOf(Session("usercode").ToString) > -1 Then
                    btnExport.Visible = True
                End If

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False
                btnAddDetails.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "14" '14 : ยกเลิก
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False


                btnExport.Visible = False
                btnPrint.Visible = True

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False
                btnAddDetails.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "15" '15 : รอตรวจสอบ
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False


                btnExport.Visible = False
                btnPrint.Visible = True

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False
                btnAddDetails.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "17" '17 : การเงินได้รับเอกสาร
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False


                btnExport.Visible = False
                btnPrint.Visible = True

                If account_code.IndexOf(Session("usercode").ToString) > -1 Then
                    btnExport.Visible = True
                End If

                'ช่อง ปุ่ม เพิ่มรายการ
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False
                btnAddDetails.Visible = False

                'ปุ่ม & status 
                statusnonpo.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Select
    End Sub

    Private Function createHeadtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("frm", GetType(String))
        dt.Columns.Add("coderef", GetType(String))
        dt.Columns.Add("amount", GetType(String))
        dt.Columns.Add("remark", GetType(String))

        Return dt
    End Function
    Private Function createmaintable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("nonpoid", GetType(Double)) 'new =0
        dt.Columns.Add("nonpocode", GetType(String))
        dt.Columns.Add("payby", GetType(String))
        dt.Columns.Add("coderef", GetType(String))
        dt.Columns.Add("limit", GetType(Double))
        dt.Columns.Add("statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("detail", GetType(String))

        dt.Columns.Add("branchid", GetType(String))
        dt.Columns.Add("depid", GetType(String))
        dt.Columns.Add("secid", GetType(String))

        dt.Columns.Add("chkpayback", GetType(Integer))
        dt.Columns.Add("chkdeductsell", GetType(Integer))
        dt.Columns.Add("payback_amount", GetType(Double))
        dt.Columns.Add("deductsell_amount", GetType(Double))

        dt.Columns.Add("vendorcode", GetType(String))
        dt.Columns.Add("totalforcheck", GetType(String))

        dt.Columns.Add("DueDate", GetType(String))

        dt.Columns.Add("vat_wait", GetType(Integer))

        dt.Columns.Add("withdraw_by", GetType(String))
        dt.Columns.Add("withdraw_date", GetType(String))
        dt.Columns.Add("service_by", GetType(String))
        dt.Columns.Add("service_date", GetType(String))
        dt.Columns.Add("verify_by", GetType(String))
        dt.Columns.Add("verify_date", GetType(String))
        dt.Columns.Add("approval_by", GetType(String))
        dt.Columns.Add("approval_date", GetType(String))
        '---------------------------------------
        dt.Columns.Add("updateby", GetType(String))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("updateby_name", GetType(String))
        dt.Columns.Add("createby_name", GetType(String))

        Return dt
    End Function

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("row", GetType(Integer))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("nonpodtl_id", GetType(Integer))
        dt.Columns.Add("accountcodeid", GetType(String))
        dt.Columns.Add("accountcode", GetType(String))
        dt.Columns.Add("depid", GetType(Integer))
        dt.Columns.Add("depname", GetType(String))
        dt.Columns.Add("buid", GetType(Integer))
        dt.Columns.Add("buname", GetType(String))
        dt.Columns.Add("ppid", GetType(Integer))
        dt.Columns.Add("ppname", GetType(String))
        dt.Columns.Add("pjid", GetType(Integer))
        dt.Columns.Add("pjname", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("vat_per", GetType(Integer))
        dt.Columns.Add("tax_per", GetType(Integer))
        dt.Columns.Add("vat", GetType(Double))
        dt.Columns.Add("tax", GetType(Double))
        dt.Columns.Add("cost_total", GetType(Double))
        dt.Columns.Add("detail", GetType(String))
        dt.Columns.Add("vendorname", GetType(String))
        dt.Columns.Add("vendorcode", GetType(String))
        dt.Columns.Add("invoice", GetType(String))
        dt.Columns.Add("taxid", GetType(String))
        dt.Columns.Add("invoicedate", GetType(String))
        dt.Columns.Add("nobill", GetType(Boolean))
        dt.Columns.Add("incompletebill", GetType(Boolean))

        Return dt
    End Function
    Private Function createtablecomment() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("commentid", GetType(Integer))
        dt.Columns.Add("commentdetail", GetType(String))
        dt.Columns.Add("updateby", GetType(String))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("coderef", GetType(String))

        Return dt
    End Function
    Private Function createtableAttach() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("imagename", GetType(String))
        dt.Columns.Add("url", GetType(String))
        dt.Columns.Add("show", GetType(String))
        dt.Columns.Add("checked", GetType(Integer))

        Return dt
    End Function

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim cnt_cost As Integer
        'Dim amountpayBack As Double
        'Dim amountdedusctsell As Double
        Try
            'cost = Convert.ToDouble(detailtable.Compute("SUM(cost)", String.Empty))
            cnt_cost = detailtable.Rows.Count
        Catch ex As Exception
            cnt_cost = 0
        End Try
        'Try
        '    amountpayBack = Convert.ToDouble(txtamountpayBack.Text)
        'Catch ex As Exception
        '    amountpayBack = 0
        'End Try
        'Try
        '    amountdedusctsell = Convert.ToDouble(txtamountdedusctsell.Text)
        'Catch ex As Exception
        '    amountdedusctsell = 0
        'End Try
        If cboVendor.SelectedItem.Value = "" Then
            result = False
            msg = "กรุณาใส่ Vendor"
            GoTo endprocess
        End If
        If Not chkCheque.Checked And
            Not chkChequeCounter.Checked And
            Not chkCashierCheque.Checked And
            Not chkTT.Checked And
            Not chkEFT.Checked And
            Not chkdeductSell.Checked And
            Not chkPXC.Checked Then
            result = False
            msg = "กรุณาเลือกช่องทางจ่ายโดย"
            GoTo endprocess
        End If
        If txtNote.Text.Trim() = "" Then
            result = False
            msg = "กรุณาใส่จุดประสงค์"
            GoTo endprocess
        End If
        If txtDuedate.Text.Trim() = "" Then
            result = False
            msg = "กรุณาเลือก Duedate"
            GoTo endprocess
        End If
        If cnt_cost <= 0 Then
            result = False
            msg = "กรุณาใส่รายการ"
            GoTo endprocess
        End If

        'If amountdedusctsell < 0 Then
        '    result = False
        '    msg = "กรุณาใส่จำนวนเต็ม"
        '    GoTo endprocess
        'End If
endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function
    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
    End Sub

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        For i = 0 To detailtable.Rows.Count - 1
            If detailtable.Rows(i).Item("nonpodtl_id") = 0 Or detailtable.Rows(i).Item("row") = 0 Or Not detailtable.Rows(i).Item("status") = "read" Then
                chkunsave = 1
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('กรุณาบันทึกรายการ');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End If
        Next i
        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("NonpoCode"), txtComment.Text.Trim(), Session("userid"))
            findNonPO()
            txtComment.Text = ""

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))

endprocess:
    End Sub

    'Public Sub AddDetails()

    '    Dim cost As Double
    '    Try
    '        cost = Double.Parse(txtPrice.Text)
    '    Catch ex As Exception
    '        cost = 0
    '    End Try

    '    Dim row As DataRow
    '    row = detailtable.NewRow()
    '    row("row") = detailtable.Rows.Count + 1
    '    row("nonpodtl_id") = hiddenAdvancedetailid.Value 'df 0
    '    row("accountcodeid") = cboAccountCode.SelectedItem.Value
    '    row("accountcode") = cboAccountCode.SelectedItem.Text
    '    row("depid") = cboDep.SelectedItem.Value
    '    row("depname") = cboDep.SelectedItem.Text
    '    row("buid") = cboBU.SelectedItem.Value
    '    row("buname") = cboBU.SelectedItem.Text
    '    row("ppid") = cboPP.SelectedItem.Value
    '    row("ppname") = cboPP.SelectedItem.Text

    '    row("cost") = cost
    '    row("detail") = txtDetail.Text
    '    row("vendorname") = cboVendor.SelectedItem.Text
    '    row("vendorcode") = cboVendor.SelectedItem.Value


    '    detailtable.Rows.Add(row)
    '    'detailtable.Rows.Add(0, cboJobType.SelectedItem.Value, cboJobType.SelectedItem.Text, txtAssetCode.Text, txtAssetName.Text,
    '    '                    qty, cboUnit.SelectedItem.Value, cboUnit.SelectedItem.Text, cost, cboSupplier.SelectedItem.Value,
    '    '                    cboSupplier.SelectedItem.Text, urgent, cboPolicy.SelectedValue, reqdate, txtJobDetail.Text, 0)

    '    Session("detailtable_payment") = detailtable

    'End Sub
    'Private Sub updateDetails(indexrow As Integer)
    '    Dim cost As Double
    '    Try
    '        cost = Double.Parse(txtPrice.Text)
    '    Catch ex As Exception
    '        cost = 0
    '    End Try

    '    'update detail
    '    With detailtable.Rows(indexrow)
    '        .Item("row") = row.Value 'df 0
    '        .Item("nonpodtl_id") = hiddenAdvancedetailid.Value 'df 0
    '        .Item("accountcodeid") = cboAccountCode.SelectedItem.Value
    '        .Item("accountcode") = cboAccountCode.SelectedItem.Text
    '        .Item("depid") = cboDep.SelectedItem.Value
    '        .Item("depname") = cboDep.SelectedItem.Text
    '        .Item("buid") = cboBU.SelectedItem.Value
    '        .Item("buname") = cboBU.SelectedItem.Text
    '        .Item("ppid") = cboPP.SelectedItem.Value
    '        .Item("ppname") = cboPP.SelectedItem.Text

    '        .Item("cost") = cost
    '        .Item("detail") = txtDetail.Text
    '        .Item("vendorname") = cboVendor.SelectedItem.Text
    '        .Item("vendorcode") = cboVendor.SelectedItem.Value
    '    End With

    'End Sub

    Private Sub findNonPO()
        Dim nonpoDs = New DataSet
        Dim objNonpo As New NonPO
        Try
            nonpoDs = objNonpo.NonPO_Find(Request.QueryString("NonpoCode"))
            '-- table 0 = Head
            '-- table 1 = main
            '-- table 2 = detail
            '-- table 3 = attach
            '-- table 4 = comment

            head = nonpoDs.Tables(0)
            maintable = nonpoDs.Tables(1)
            detailtable = nonpoDs.Tables(2)
            AttachTable = nonpoDs.Tables(3)
            CommentTable = nonpoDs.Tables(4)

            Session("head_payment") = head
            Session("detailtable_payment") = detailtable
            Session("maintable_payment") = maintable
            Session("comment_payment") = CommentTable
            Session("attatch_payment") = AttachTable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub cleardetail()
        row.Value = 0
        'nextrow.Value = 0
        hiddenAdvancedetailid.Value = 0

        cboAccountCode.SelectedIndex = -1
        cboDep.SelectedIndex = -1
        cboBU.SelectedIndex = -1
        cboPP.SelectedIndex = -1
        cboPJ.SelectedIndex = -1

        'cboVendor.SelectedIndex = -1

        txtPrice.Text = "0"
        txtVat.Text = "0"
        txtTax.Text = "0"
        txtDetail.Text = ""
    End Sub

    Private Sub cboAccountCode_PreRender(sender As Object, e As EventArgs) Handles cboAccountCode.PreRender
        Dim objNonpo As New NonPO
        Dim yy As String = cboAccountCode.SelectedValue
        cboAccountCode.Items.Clear()

        objNonpo.SetCboAccountCode(cboAccountCode, Session("userid"))
        cboAccountCode.Text = yy
    End Sub

    Private Sub cboVendor_PreRender(sender As Object, e As EventArgs) Handles cboVendor.PreRender
        Dim objsupplier As New Supplier
        Dim yy As String = cboVendor.SelectedValue
        cboVendor.Items.Clear()

        objsupplier.SetCboVendorByName(cboVendor, "")

        cboVendor.Text = yy
    End Sub

    Private Sub ClearAdvance_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim totalincompletebill As Double
        Dim totalcompletebill As Double
        Dim totalcost As Double
        Dim cost As Double
        Dim vat As Double
        Dim tax As Double
        Try
            cost = Convert.ToDouble(detailtable.Compute("SUM(cost_total)", String.Empty))
        Catch ex As Exception
            cost = 0
        End Try
        Try
            vat = Convert.ToDouble(detailtable.Compute("SUM(vat)", String.Empty))
        Catch ex As Exception
            vat = 0
        End Try
        Try
            tax = Convert.ToDouble(detailtable.Compute("SUM(tax)", String.Empty))
        Catch ex As Exception
            tax = 0
        End Try
        Try
            totalcost = Convert.ToDouble(detailtable.Compute("SUM(cost)", String.Empty))
        Catch ex As Exception
            totalcost = 0
        End Try
        Try
            totalincompletebill = Convert.ToDouble(detailtable.Compute("SUM(cost)", "incompletebill or nobill"))
        Catch ex As Exception
            totalincompletebill = 0
        End Try
        Try
            totalcompletebill = Convert.ToDouble(detailtable.Compute("SUM(cost)", "not incompletebill and not nobill"))
        Catch ex As Exception
            totalcompletebill = 0
        End Try

        total_cost = String.Format("{0:n2}", totalcost)
        total_completebill = String.Format("{0:n2}", totalcompletebill)
        total_incompletebill = String.Format("{0:n2}", totalincompletebill)
        total_vat = String.Format("{0:n2}", vat)
        total_tax = String.Format("({0:n2})", (tax))
        total = String.Format("{0:n2}", cost)
        'total = Format(cost, "0.00")
    End Sub
    <System.Web.Services.WebMethod>
    Public Shared Function addAttach(ByVal user As String, ByVal url As String, ByVal description As String, ByVal nonpocode As String)
        Dim objNonpo As New NonPO
        Dim id As String = ""
        Try
            id = objNonpo.NonPO_Attatch_Save(nonpocode, url, description, user)
        Catch ex As Exception
            Return "fail"
        End Try
        Return id

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function deleteAttach(ByVal attachid As Integer, userid As Integer)

        Dim objNonpo As New NonPO
        Try
            objNonpo.Delete_Attach_By_attachid(attachid, userid)
        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function


    <System.Web.Services.WebMethod>
    Public Function addoreditdetail(ByVal rows As Integer, ByVal status As String, ByVal nonpodtl_id As Integer, ByVal accountcodeid As Integer,
                                           ByVal accountcode As String, ByVal depid As Integer, ByVal depname As String,
                                           ByVal buid As Integer, ByVal buname As String, ByVal ppid As Integer, ByVal ppname As String, ByVal pjid As Integer, ByVal pjname As String,
                                           ByVal cost As Double, ByVal vat As Integer, ByVal tax As Integer, ByVal detail As String,
                                           ByVal vendorname As String, ByVal vendorcode As String,
                                           ByVal invoice As String, ByVal taxid As String, ByVal invoicedate As String)
        Dim cntrow As Integer = detailtable.Rows.Count + 1
        Try
            If status = "undefined" Then
                Dim row As DataRow
                row = detailtable.NewRow()
                row("row") = cntrow
                row("status") = "new"
                row("nonpodtl_id") = nonpodtl_id
                row("accountcodeid") = accountcodeid
                row("accountcode") = accountcode
                row("depid") = depid
                row("depname") = depname
                row("buid") = buid
                row("buname") = buname
                row("ppid") = ppid
                row("ppname") = ppname
                row("pjid") = pjid
                row("pjname") = pjname

                row("cost") = cost
                row("vat_per") = vat
                row("tax_per") = tax
                row("vat") = cost * vat / 100
                row("tax") = cost * tax / 100
                row("cost_total") = (cost + cost * vat / 100) - cost * tax / 100
                row("detail") = detail
                row("vendorname") = vendorname
                row("vendorcode") = vendorcode
                row("invoice") = invoice
                row("taxid") = taxid
                row("invoicedate") = invoicedate


                detailtable.Rows.Add(row)
            Else
                With detailtable.Rows(detailtable.Rows.IndexOf(detailtable.Select("row='" & rows & "'")(0)))
                    .Item("row") = rows
                    .Item("status") = "edit"
                    .Item("nonpodtl_id") = nonpodtl_id
                    .Item("accountcodeid") = accountcodeid
                    .Item("accountcode") = accountcode
                    .Item("depid") = depid
                    .Item("depname") = depname
                    .Item("buid") = buid
                    .Item("buname") = buname
                    .Item("ppid") = ppid
                    .Item("ppname") = ppname
                    .Item("pjid") = pjid
                    .Item("pjname") = pjname

                    .Item("cost") = cost
                    .Item("vat_per") = vat
                    .Item("tax_per") = tax
                    .Item("vat") = cost * vat / 100
                    .Item("tax") = cost * tax / 100
                    .Item("cost_total") = (cost + cost * vat / 100) - cost * tax / 100
                    .Item("detail") = detail
                    .Item("vendorname") = vendorname
                    .Item("vendorcode") = vendorcode
                    .Item("invoice") = invoice
                    .Item("taxid") = taxid
                    .Item("invoicedate") = invoicedate
                End With
            End If

        Catch ex As Exception
            Return "fail"
            GoTo endprocess
        End Try
        If rows = 0 Then
            Return cntrow
        Else
            Return rows
        End If

endprocess:

    End Function

    <System.Web.Services.WebMethod>
    Public Function deleteDetail(ByVal nonpodtlid As Integer, ByVal rows As Integer, user As String)

        Try
            If nonpodtlid = 0 Then
                detailtable.Rows.Remove(detailtable.Select("row='" & rows & "'")(0))
            Else

                Dim objNonpo As New NonPO
                objNonpo.deleteDetailbyNonpodtlid(nonpodtlid, user)
                detailtable.Rows.Remove(detailtable.Select("nonpodtl_id='" & nonpodtlid & "'")(0))
            End If

        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function updateComment(ByVal userid As Integer, ByVal msg As String, commentid As Integer)
        Dim approval As New Approval
        Try
            approval.Update_Comment_By_commentid(commentid, msg, userid)
        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function
    <System.Web.Services.WebMethod>
    Public Shared Function deleteComment(ByVal commentid As Integer, userid As Integer)
        Dim approval As New Approval
        Try
            approval.Delete_Comment_By_commentid(commentid, userid)
        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function changeChecked(ByVal attatchid As Integer, ByVal chked As Boolean, ByVal userid As Integer)

        Dim objnonpo As New NonPO
        Dim dt As DataTable
        Try
            dt = objnonpo.checkAttatch(attatchid, chked, userid)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        saveorupdate()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim cost As Double
        cost = Convert.ToDouble(detailtable.Compute("SUM(cost_total)", String.Empty))
        If account_code.IndexOf(Session("usercode").ToString) > -1 And
                    (maintable.Rows(0).Item("statusid") = 7) Then
            If cost > maintable.Rows(0).Item("totalforcheck") Then
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "invalidtotal();"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End If
            'Else
            '    If cost > head.Rows(0).Item("amount") Then
            '        Dim scriptKey As String = "alert"
            '        'Dim javaScript As String = "alert('" & ex.Message & "');"
            '        Dim javaScript As String = "invalidtotal();"
            '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            '        GoTo endprocess
            '    End If
        End If
        saveorupdate()
endprocess:
    End Sub
    Private Sub saveorupdate()
        If validatedata() Then
            'If Session("status") = "new" Then
            'If maintable.Rows.Count = 0 Then
            updatehead()
            'End If
            Save()
            'Else
            '    SaveEdit()
            'End If
        End If

    End Sub

    Private Sub Save()
        Dim objNonpo As New NonPO
        Dim payno As String = ""

        payno = txtpmno.Text
        Try
            payno = objNonpo.SavePayment(payno, maintable, detailtable, Session("usercode"))
            txtpmno.Text = payno
            Session("status_payment") = "edit"


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & payno)
endprocess:
    End Sub

    Private Sub updatehead()
        Dim userid As Double
        Dim userowner As Double

        userid = Session("userid")
        Dim payby As String = ""
        If chkCheque.Checked Then
            payby = "cheque"
        ElseIf chkChequeCounter.Checked Then
            payby = "chequecounter"
        ElseIf chkCashierCheque.Checked Then
            payby = "cashiercheque"
        ElseIf chkTT.Checked Then
            payby = "tt"
        ElseIf chkEFT.Checked Then
            payby = "eft"
        ElseIf chkdeductSell.Checked Then
            payby = "deductsell"
        ElseIf chkPXC.Checked Then
            payby = "pcx"
        End If

        If cboOwner.SelectedItem.Value = 0 Then
            userowner = userid
        Else
            userowner = cboOwner.SelectedItem.Value
        End If
        If maintable.Rows.Count > 0 Then
            'update
            With maintable.Rows(0)
                .Item("payby") = payby
                .Item("vendorcode") = cboVendor.SelectedItem.Value
                .Item("DueDate") = txtDuedate.Text.Trim()
                .Item("detail") = txtNote.Text.Trim()
                .Item("vat_wait") = If(chkVat.Checked, 1, 0)
            End With
        Else
            'Dim amountpayBack As Double
            'Dim amountdedusctsell As Double



            'insert
            With maintable
                .Rows.Add(0, "", payby, codeRef.Text.Trim(), 0, 0, "", txtNote.Text.Trim(),
                              cboBranch.SelectedItem.Value, cboDepartment.SelectedItem.Value, cboSection.SelectedItem.Value,
                              0, 0,
                              0, 0,
                              cboVendor.SelectedItem.Value, "",
                              txtDuedate.Text.Trim(), chkVat.Checked,
                              userowner, Date.Now.ToString, "", "", "", "", "", "",
                              userowner, Date.Now.ToString, userowner, Date.Now.ToString, userowner, userowner)

            End With

        End If
        Session("maintable_payment") = maintable
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue.IndexOf("Yes") > -1 Then
            Confirm(Request.QueryString("NonpoCode").ToString.Trim, Session("usercode"))
            confirmValue = "No"
        End If
    End Sub
    Private Sub Confirm(nonpocode As String, usercode As String)
        Dim objnonpo As New NonPO

        Try
            objnonpo.Confirm(nonpocode, usercode)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Session("status_payment") = "read"
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnDisApproval_Click(sender As Object, e As EventArgs) Handles btnDisApproval.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_NotAllow(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('DisApproval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_Allow(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_Verify(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnPass_ServerClick(sender As Object, e As EventArgs) Handles btnPass.ServerClick
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_Pass(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnEdit_ServerClick(sender As Object, e As EventArgs) Handles btnEdit.ServerClick
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AccountEdit(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnReject_ServerClick(sender As Object, e As EventArgs) Handles btnReject.ServerClick
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_Reject(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnWDoc_ServerClick(sender As Object, e As EventArgs) Handles btnWDoc.ServerClick
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_Complete(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Complete fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub btnFNS_Rec_Doc_ServerClick(sender As Object, e As EventArgs) Handles btnFNS_Rec_Doc.ServerClick
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_FNS_ReceiveDoc(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String, nonpocode As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & nonpocode & "_" & Date.Now.ToString
            Dim encode As String
            If (maintable.Rows(0).Item("statusid") = 7) Then
                encode = "(preview)"
            Else
                encode = "(final)"
            End If
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(filename)
            encode += Convert.ToBase64String(byt)

            'Dim decode As String
            'Dim b As Byte() = Convert.FromBase64String(encode)
            'decode = System.Text.Encoding.UTF8.GetString(b)

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & encode & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub

    Private Sub btnAddDetails_Click(sender As Object, e As EventArgs) Handles btnAddDetails.Click
        Dim res As String = Request.Form("confirm_value")
        Dim strarr() As String
        strarr = res.Split("}")
        res = strarr(0) + "}"
        Dim jss As New JavaScriptSerializer
        Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)

        Dim rows As Integer = json("rows").Trim
        Dim status As String = json("status").Trim
        Dim nonpodtl_id As Integer = json("nonpodtl_id").Trim
        Dim accountcodeid As String = json("accountcodeid").Trim
        Dim accountcode As String = json("accountcode").Trim
        Dim depid As Integer = json("depid").Trim
        Dim depname As String = json("depname").Trim
        Dim buid As Integer = json("buid").Trim
        Dim buname As String = json("buname").Trim
        Dim ppid As Integer = json("ppid").Trim
        Dim ppname As String = json("ppname").Trim
        Dim pjid As Integer = json("pjid").Trim
        Dim pjname As String = json("pjname").Trim
        Dim cost As Double = json("cost").Trim
        Dim vat As Integer = json("vat").Trim
        Dim tax As Integer = json("tax").Trim
        Dim detail As String = json("detail").Trim
        Dim vendorname As String = json("vendorname").Trim
        Dim vendorcode As String = json("vendorcode").Trim
        Dim invoice As String = json("invoice").Trim
        Dim taxid As String = json("taxid").Trim
        Dim invoicedate As String = json("invoicedate").Trim
        Dim nobill As Boolean = json("nobill").Trim
        Dim incompletebill As Boolean = json("incompletebill").Trim

        invoice = invoice.Replace(" ", "")

        Dim cntrow As Integer = detailtable.Rows.Count + 1
        While detailtable.Rows.IndexOf(detailtable.Select("row='" & cntrow & "'").FirstOrDefault()) > -1
            cntrow += 1
        End While
        Try
            If status = "undefined" Then
                Dim row As DataRow
                row = detailtable.NewRow()
                row("row") = cntrow
                row("status") = "new"
                row("nonpodtl_id") = nonpodtl_id
                row("accountcodeid") = accountcodeid
                row("accountcode") = accountcode
                row("depid") = depid
                row("depname") = depname
                row("buid") = buid
                row("buname") = buname
                row("ppid") = ppid
                row("ppname") = ppname
                row("pjid") = pjid
                row("pjname") = pjname

                row("cost") = cost
                row("vat_per") = vat
                row("tax_per") = tax
                row("vat") = FormatNumber(cost * vat / 100, 2)
                row("tax") = FormatNumber(cost * tax / 100, 2)
                row("cost_total") = FormatNumber((cost + cost * vat / 100) - cost * tax / 100, 2)
                row("detail") = detail
                row("vendorname") = vendorname
                row("vendorcode") = vendorcode
                row("invoice") = invoice
                row("taxid") = taxid
                row("invoicedate") = invoicedate
                row("nobill") = nobill
                row("incompletebill") = incompletebill

                detailtable.Rows.Add(row)
            Else
                With detailtable.Rows(detailtable.Rows.IndexOf(detailtable.Select("row='" & rows & "'")(0)))
                    .Item("row") = rows
                    .Item("status") = "edit"
                    .Item("nonpodtl_id") = nonpodtl_id
                    .Item("accountcodeid") = accountcodeid
                    .Item("accountcode") = accountcode
                    .Item("depid") = depid
                    .Item("depname") = depname
                    .Item("buid") = buid
                    .Item("buname") = buname
                    .Item("ppid") = ppid
                    .Item("ppname") = ppname
                    .Item("pjid") = pjid
                    .Item("pjname") = pjname

                    .Item("cost") = cost
                    .Item("vat_per") = vat
                    .Item("tax_per") = tax
                    .Item("vat") = FormatNumber(cost * vat / 100, 2)
                    .Item("tax") = FormatNumber(cost * tax / 100, 2)
                    .Item("cost_total") = FormatNumber((cost + cost * vat / 100) - cost * tax / 100, 2)
                    .Item("detail") = detail
                    .Item("vendorname") = vendorname
                    .Item("vendorcode") = vendorcode
                    .Item("invoice") = invoice
                    .Item("taxid") = taxid
                    .Item("invoicedate") = invoicedate
                    .Item("nobill") = nobill
                    .Item("incompletebill") = incompletebill
                End With
            End If

            Session("detailtable_payment") = detailtable
            checkunsave()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('adddetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnDowload_Click(sender As Object, e As EventArgs) Handles btnDowload.Click
        Dim createdate As String
        createdate = txtCreateDate.Text.Substring(6, 4) & txtCreateDate.Text.Substring(3, 2) & txtCreateDate.Text.Substring(0, 2)
        Dim objnonpo As New NonPO
        Try
            Dim dt As DataTable
            dt = objnonpo.Nonpo_Export(Request.QueryString("NonpoCode"), chkGroupVAT.Checked, chkGroupVendor.Checked)
            ExportToExcel(dt, Session("usercode"), createdate, Request.QueryString("NonpoCode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_Cancel(Request.QueryString("NonpoCode"), Session("usercode"))
            Session("status_payment") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Payment/Payment2.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub
End Class