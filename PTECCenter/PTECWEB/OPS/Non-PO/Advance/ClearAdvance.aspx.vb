Public Class ClearAdvance
    Inherits System.Web.UI.Page
    Public Shared AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public Shared detailtable As DataTable '= createdetailtable()
    Public maintable As DataTable '= createmaintable()
    Public head As DataTable
    Public menutable As DataTable
    Public total As String
    Public chkunsave As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objNonpo As New NonPO
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier

        Dim nonpoDs = New DataSet

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


        If Not IsPostBack() Then
            head = createHeadtable()
            detailtable = createdetailtable()
            maintable = createmaintable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()



            'insert test comment
            With CommentTable
                .Rows.Add(0, "test", "ธนพล", Now(), "ธนพล", Now(), "ADV201000001")
                .Rows.Add(1, "test1", "ธนพล", Now(), "ธนพล", Now(), "ADV201000001")
                .Rows.Add(2, "test23", "ทศกัน", Now(), "ทศกัน", Now(), "ADV201000001")
            End With

            'insert test AttachTable
            With AttachTable
                .Rows.Add(0, "test", "http://google.com/", "tttt")
                .Rows.Add(1, "test1", "http://google.com/", "ADV201000001")
            End With


            objbranch.SetComboBranch(cboBranch, Session("usercode"))
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
            SetCboUsers(cboOwner)
            setmaindefault()

            objdep.SetCboDepartmentBybranch(cboDep, 0)
            objNonpo.SetCboPurpose(cboPP)
            objNonpo.SetCboAccountCode(cboAccountCode, Session("userid"))
            objsupplier.SetCboVendor(cboVendor, "")


            Dim dt As New DataTable

            dt.Columns.Add("buid", GetType(Integer))
            dt.Columns.Add("bunamename", GetType(String))
            dt.Rows.Add(0, "")
            dt.Rows.Add(1, "Oil")
            dt.Rows.Add(2, "NonOil")
            cboBU.DataSource = dt
            cboBU.DataValueField = ("buid")
            cboBU.DataTextField = ("bunamename")
            cboBU.DataBind()

            If Not Request.QueryString("NonpoCode") Is Nothing Then
                Session("detailtable") = detailtable
                If Not Session("status_clearadvance") = "edit" Then
                    Session("status_clearadvance") = "read"
                End If
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
                    'CommentTable = nonpoDs.Tables(4)
                    sethead(head)
                    setmain(maintable)
                    SetBtn(1)
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            ElseIf Not Request.QueryString("f") Is Nothing Then
                Dim objjob As New jobs
                Dim ds As New DataSet
                If Not Request.QueryString("code_ref") Is Nothing And Request.QueryString("code_ref_dtl") Is Nothing Then
                    Session("status_clearadvance") = "new"
                    codeRef.Text = Request.QueryString("code_ref").ToString
                    ds = objjob.setNonPODtl_by_coderef(Request.QueryString("f").ToString, Request.QueryString("code_ref").ToString, "", Session("usercode").ToString)
                    head = ds.Tables(0)
                    sethead(head)
                End If
            Else
                Session("status_clearadvance") = "new"

            End If



            Session("detailtable_clearadvance") = detailtable
            Session("maintable_clearadvance") = maintable
            Session("comment_clearadvance") = CommentTable
            Session("attatch_clearadvance") = AttachTable
        Else

            detailtable = Session("detailtable_clearadvance")
            maintable = Session("maintable_clearadvance")
            AttachTable = Session("attatch_clearadvance")
            CommentTable = Session("comment_clearadvance")

        End If
        SetMenu()
        checkunsave()
    End Sub

    Private Sub sethead(dt As DataTable)
        With dt

            codeRef.Text = .Rows(0).Item("coderef").ToString
            amount.Text = String.Format("{0:n2}", .Rows(0).Item("amount"))
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
                Case = "1"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '1 : รอยืนยัน
                Case = "2"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '2 : รออนุมัติ
                Case = "4"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '4 : ขอเอกสารเพิ่มเติม
                Case = "5"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '5 : ไม่ผ่านการตรวจสอบ
                Case = "6"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '6 : ไม่ผ่านการอนุมัติ
                Case = "7"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '7 : รอบัญชีตรวจสอบ
                Case = "8"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '8 : รอเอกสารตัวจริง
                Case = "9"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '9 : ได้รับเอกสารตัวจริง
                Case = "14"
                    statusnonpo.Attributes.Add("class", "btn btn-info") '14 : ยกเลิก
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
            txtadvno.Text = .Rows(0).Item("nonpocode").ToString
            txtadvno.ToolTip = .Rows(0).Item("nonpocode").ToString


            cboOwner.Attributes.Add("disabled", "True")
            cboSection.Attributes.Add("disabled", "True")
            cboDepartment.Attributes.Add("disabled", "True")
            cboBranch.Attributes.Add("disabled", "True")
            txtamountpayBack.Attributes.Add("disabled", "True")
            txtamountdedusctsell.Attributes.Add("disabled", "True")


        End With


    End Sub
    Private Sub SetMenu()
        Select Case Session("status_clearadvance")
            Case = "new"
                btnFromAddDetail.Visible = True
                FromAddDetail.Visible = True

                statusnonpo.Visible = False
                btnExport.Visible = True
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "read"
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False

                statusnonpo.Visible = True
                btnExport.Visible = True
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "write"
                btnFromAddDetail.Visible = False
                FromAddDetail.Visible = False

                statusnonpo.Visible = True
                btnExport.Visible = False
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "edit"
                btnFromAddDetail.Visible = True
                FromAddDetail.Visible = True

                statusnonpo.Visible = True
                btnExport.Visible = True
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
        End Select


    End Sub

    Private Sub checkunsave()
        For i = 0 To detailtable.Rows.Count - 1
            If detailtable.Rows(i).Item("nonpodtl_id") = 0 Then
                chkunsave = 1
                GoTo endprocess
            End If
        Next i
endprocess:
    End Sub
    Private Sub SetBtn(statusid As String)
        Select Case statusid
            Case = "1" 'รออนุมัติ
                btnSave.Enabled = True
                'btnSaveDetail.Enabled = False
                btnConfirm.Enabled = False

                btnExport.Disabled = True
                btnPrint.Visible = False

                btnPass.Visible = True
                btnEdit.Visible = True
                btnReject.Visible = True
            Case Else
                btnPass.Visible = False
                btnEdit.Visible = False
                btnReject.Visible = False
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
        dt.Columns.Add("nonpodtl_id", GetType(Integer))
        dt.Columns.Add("accountcodeid", GetType(Integer))
        dt.Columns.Add("accountcode", GetType(String))
        dt.Columns.Add("depid", GetType(Integer))
        dt.Columns.Add("depname", GetType(String))
        dt.Columns.Add("buid", GetType(Integer))
        dt.Columns.Add("buname", GetType(String))
        dt.Columns.Add("ppid", GetType(Integer))
        dt.Columns.Add("ppname", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("detail", GetType(String))
        dt.Columns.Add("vendorname", GetType(String))
        dt.Columns.Add("vendorcode", GetType(String))

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

        dt.Columns.Add("imageid", GetType(Integer))
        dt.Columns.Add("imagename", GetType(String))
        dt.Columns.Add("url", GetType(String))
        dt.Columns.Add("show", GetType(String))

        Return dt
    End Function

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim cost As Double
        Try
            cost = Convert.ToDouble(detailtable.Compute("SUM(cost)", String.Empty))
        Catch ex As Exception
            cost = 0
        End Try
        If cost <= 0 Then
            result= False
            msg="กรุณาใส่รายการ"
            GoTo endprocess
            End If
            If txtamountpayBack.Text < 0 Then
            result = False
            msg = "กรุณาใส่จำนวนเต็ม"
            GoTo endprocess
        End If
        If txtamountdedusctsell.Text < 0 Then
            result = False
            msg = "กรุณาใส่จำนวนเต็ม"
            GoTo endprocess
        End If
        If String.IsNullOrEmpty(codeRef.Text) Then
            result = False
            msg = "ต้องมีรหัสอ้างอิง"
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
    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
    End Sub

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        Dim approval As New Approval
        Try
            approval.Save_Comment_By_Code(Request.QueryString("paymentcode"), txtComment.Text.Trim(), Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))
endprocess:
    End Sub
    Public Sub AddDetails()

        Dim cost As Double
        Try
            cost = Double.Parse(txtPrice.Text)
        Catch ex As Exception
            cost = 0
        End Try

        Dim row As DataRow
        row = detailtable.NewRow()
        row("row") = detailtable.Rows.Count + 1
        row("nonpodtl_id") = hiddenAdvancedetailid.Value 'df 0
        row("accountcodeid") = cboAccountCode.SelectedItem.Value
        row("accountcode") = cboAccountCode.SelectedItem.Text
        row("depid") = cboDep.SelectedItem.Value
        row("depname") = cboDep.SelectedItem.Text
        row("buid") = cboBU.SelectedItem.Value
        row("buname") = cboBU.SelectedItem.Text
        row("ppid") = cboPP.SelectedItem.Value
        row("ppname") = cboPP.SelectedItem.Text

        row("cost") = cost
        row("detail") = txtDetail.Text
        row("vendorname") = cboVendor.SelectedItem.Text
        row("vendorcode") = cboVendor.SelectedItem.Value


        detailtable.Rows.Add(row)
        'detailtable.Rows.Add(0, cboJobType.SelectedItem.Value, cboJobType.SelectedItem.Text, txtAssetCode.Text, txtAssetName.Text,
        '                    qty, cboUnit.SelectedItem.Value, cboUnit.SelectedItem.Text, cost, cboSupplier.SelectedItem.Value,
        '                    cboSupplier.SelectedItem.Text, urgent, cboPolicy.SelectedValue, reqdate, txtJobDetail.Text, 0)

        Session("detailtable_clearadvance") = detailtable

    End Sub
    Private Sub updateDetails(indexrow As Integer)
        Dim cost As Double
        Try
            cost = Double.Parse(txtPrice.Text)
        Catch ex As Exception
            cost = 0
        End Try

        'update detail
        With detailtable.Rows(indexrow)
            .Item("row") = row.Value 'df 0
            .Item("nonpodtl_id") = hiddenAdvancedetailid.Value 'df 0
            .Item("accountcodeid") = cboAccountCode.SelectedItem.Value
            .Item("accountcode") = cboAccountCode.SelectedItem.Text
            .Item("depid") = cboDep.SelectedItem.Value
            .Item("depname") = cboDep.SelectedItem.Text
            .Item("buid") = cboBU.SelectedItem.Value
            .Item("buname") = cboBU.SelectedItem.Text
            .Item("ppid") = cboPP.SelectedItem.Value
            .Item("ppname") = cboPP.SelectedItem.Text

            .Item("cost") = cost
            .Item("detail") = txtDetail.Text
            .Item("vendorname") = cboVendor.SelectedItem.Text
            .Item("vendorcode") = cboVendor.SelectedItem.Value
        End With

    End Sub

    Private Sub cleardetail()
        row.Value = 0
        nextrow.Value = 0
        hiddenAdvancedetailid.Value = 0

        cboAccountCode.SelectedIndex = -1
        cboDep.SelectedIndex = -1
        cboBU.SelectedIndex = -1
        cboPP.SelectedIndex = -1
        cboVendor.SelectedIndex = -1

        txtPrice.Text = "0"
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

        objsupplier.SetCboVendor(cboVendor, "")

        cboVendor.Text = yy
    End Sub

    Private Sub ClearAdvance_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim cost As Double
        Try
            cost = Convert.ToDouble(detailtable.Compute("SUM(cost)", String.Empty))
        Catch ex As Exception
            cost = 0
        End Try
        'total = String.Format("{0:n2}", cost)
        total = Format(cost, "0.00")
    End Sub
    <System.Web.Services.WebMethod>
    Public Shared Function addAttach(ByVal user As String, ByVal url As String, ByVal description As String, ByVal nonpocode As String)
        Dim objNonpo As New NonPO
        Try
            Dim id As String
            id = objNonpo.NonPO_Attatch_Save(nonpocode, url, description, user)
        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function addoreditdetail(ByVal rows As Integer, ByVal nonpodtl_id As Integer, ByVal accountcodeid As Integer,
                                           ByVal accountcode As String, ByVal depid As Integer, ByVal depname As String,
                                           ByVal buid As Integer, ByVal buname As String, ByVal ppid As Integer, ByVal ppname As String,
                                           ByVal cost As Double, ByVal detail As String,
                                           ByVal vendorname As String, ByVal vendorcode As String)
        Dim cntrow As Integer = detailtable.Rows.Count + 1
        Try
            If rows = 0 Then
                Dim row As DataRow
                row = detailtable.NewRow()
                row("row") = cntrow
                row("nonpodtl_id") = nonpodtl_id
                row("accountcodeid") = accountcodeid
                row("accountcode") = accountcode
                row("depid") = depid
                row("depname") = depname
                row("buid") = buid
                row("buname") = buname
                row("ppid") = ppid
                row("ppname") = ppname

                row("cost") = cost
                row("detail") = detail
                row("vendorname") = vendorname
                row("vendorcode") = vendorcode


                detailtable.Rows.Add(row)
            Else
                With detailtable.Rows(detailtable.Rows.IndexOf(detailtable.Select("row='" & rows & "'")(0)))
                    .Item("row") = rows
                    .Item("nonpodtl_id") = nonpodtl_id
                    .Item("accountcodeid") = accountcodeid
                    .Item("accountcode") = accountcode
                    .Item("depid") = depid
                    .Item("depname") = depname
                    .Item("buid") = buid
                    .Item("buname") = buname
                    .Item("ppid") = ppid
                    .Item("ppname") = ppname

                    .Item("cost") = cost
                    .Item("detail") = detail
                    .Item("vendorname") = vendorname
                    .Item("vendorcode") = vendorcode
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
    Public Shared Function deleteDetail(ByVal nonpodtlid As Integer, ByVal rows As Integer, user As String)

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
        End If
    End Sub

    Private Sub Save()
        Dim objNonpo As New NonPO
        Dim advno As String = ""

        advno = txtadvno.Text
        Try
            advno = objNonpo.SaveAdvance(advno, maintable, detailtable, Session("usercode"))
            txtadvno.Text = advno
            Session("status_clearadvance") = "edit"


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../Advance/ClearAdvance.aspx?NonpoCode=" & advno)
endprocess:
    End Sub

    Private Sub updatehead()
        Dim userid As Double
        Dim userowner As Double

        userid = Session("userid")

        If cboOwner.SelectedItem.Value = 0 Then
            userowner = userid
        Else
            userowner = cboOwner.SelectedItem.Value
        End If
        If maintable.Rows.Count > 0 Then
            'update
            With maintable.Rows(0)
                .Item("advno") = txtadvno.Text
            End With
        Else
            'insert
            With maintable
                .Rows.Add(0, "", codeRef.Text.Trim(), 0, 0, "", "",
                          cboBranch.SelectedItem.Value, cboDepartment.SelectedItem.Value, cboSection.SelectedItem.Value,
                          chkpayBack.Checked, chkdeductSell.Checked,
                          txtamountpayBack.Text.Trim(), txtamountdedusctsell.Text.Trim,
                          "",
                          userowner, Date.Now.ToString, "", "", "", "", "", "",
                          userowner, Date.Now.ToString, userowner, Date.Now.ToString, userowner, userowner)

            End With

        End If
        Session("maintable_clearadvance") = maintable
    End Sub
End Class