Imports System.Drawing

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim am_id As String
    Dim rm_id As String
    Dim dm_id As String
    Public flag As Boolean = True
    Public approval As Boolean = False
    Public deadline As String
    Public ownerapproval As String = String.Empty

    Public detailtable As DataTable '= createtable()
    Public PermissionOwner As DataSet '= createtable()
    Public CommentTable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        Dim fileImgName As String = My.Settings.fullurl

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
        usercode = Session("usercode")


        If Not IsPostBack() Then
            objapproval.SetCboApproval(cboApproval)
            'If Not Session("positionid") = "10" Then
            'objbranch.SetComboBranchByAreaid(cboBranch, Session("areaid").ToString, 1)
            'Else
            'objbranch.SetComboBranch(cboBranch, usercode)
            'End If
            objbranch.SetComboBranch(cboBranch, usercode)
            detailtable = createtable()
            CommentTable = createtablecomment()
            If Not Request.QueryString("approvalcode") Is Nothing Then
                Try
                    approvaldataset = objapproval.Approval_Find(Request.QueryString("approvalcode"))
                    detailtable = approvaldataset.Tables(0)
                    CommentTable = approvaldataset.Tables(4)
                    Session("detailtable") = detailtable
                    If approvaldataset.Tables(1).Rows.Count > 0 Then
                        fileImgName += approvaldataset.Tables(1).Rows(0).Item("imagename")
                        img1.Attributes.Add("src", fileImgName)
                    End If
                    If detailtable.Rows(0).Item("statusid") = 4 Then
                        If approvaldataset.Tables(2).Rows.Count > 0 Then
                            Dim fileImgAfterName As String = My.Settings.fullurl
                            fileImgAfterName += approvaldataset.Tables(2).Rows(0).Item("imagename")
                            'bt_img1.Attributes.Add("href", fileImgAfterName)
                            img2.Attributes.Add("src", fileImgAfterName)
                        End If
                        If approvaldataset.Tables(3).Rows.Count > 0 Then
                            Dim fileImgBillName As String = My.Settings.fullurl
                            fileImgBillName += approvaldataset.Tables(3).Rows(0).Item("imagename")
                            img3.Attributes.Add("src", fileImgBillName)
                        End If
                    End If
                    If Not Session("status") = "edit" Then
                        Session("status") = "read"
                    End If
                    chkuser(detailtable.Rows(0).Item("createby"))
                    showdata(detailtable)
                    SetBtn(detailtable.Rows(0).Item("statusid"))
                    If (Session("userid") = am_id Or
                    Session("userid") = rm_id Or
                    Session("userid") = dm_id Or
                    Session("secid").ToString = "2"
                    ) And
                    (detailtable.Rows(0).Item("statusid") = 1) Then
                        If Not Session("secid").ToString = "2" Then
                            Session("status") = "write"
                        End If

                        PermissionOwner = chkPermissionApproval(Request.QueryString("approvalcode"))
                        deadline = PermissionOwner.Tables(0).Rows(0).Item("deadline").ToString

                        'Dim dt As DataTable
                        'If PermissionOwner.Tables.Count > 1 Then 'ถ้ามีมากกว่า 1 ตารางให้ใช้ตารางที่มี row น้อยกว่า
                        'If PermissionOwner.Tables(0).Rows.Count > 0 And
                        'PermissionOwner.Tables(1).Rows.Count > 0 Then
                        'If PermissionOwner.Tables(0).Rows.Count >= PermissionOwner.Tables(1).Rows.Count Then
                        'dt = PermissionOwner.Tables(1)
                        'Else
                        ' dt = PermissionOwner.Tables(0)
                        'End If
                        'Else
                        'dt = PermissionOwner.Tables(0)
                        'End If
                        'Else
                        'dt = PermissionOwner.Tables(0)
                        'End If
                        For Each rows As DataRow In PermissionOwner.Tables(0).Rows
                            If rows("OwnerStatus") Then
                                ownerapproval += rows("sendto").ToString + ","
                            End If
                        Next rows
                        If ownerapproval.Length > 0 Then
                            ownerapproval = ownerapproval.Remove(ownerapproval.Length - 1, 1) 'ใช้ในหน้า html
                        End If

                        For Each row As DataRow In PermissionOwner.Tables(0).Rows
                            If row("OwnerStatus") Then
                                If row("sendto").ToString = "AM" Then
                                    If (Session("userid") = am_id) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                ElseIf row("sendto").ToString = "RM" Then
                                    If (Session("userid") = rm_id) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                ElseIf row("sendto").ToString = "DM" Then
                                    If (Session("userid") = dm_id) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If
                            End If
                        Next row
endprocess:
                        End If
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            Else
                Session("status") = "new"
                If Not Session("branchid") Is Nothing Then
                    cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid")))
                End If
            End If
        Else
            detailtable = Session("detailtable")
        End If
        SetMenu()


    End Sub

    Private Sub Approval_Save(fullfilename As String)
        Dim approval As New Approval
        Dim approvaltable As DataTable
        Dim approvalid As Integer
        Dim day As Integer
        If String.IsNullOrEmpty(txtDay.Text) Then
            day = 0
        Else
            day = txtDay.Text
        End If
        Try
            approvaltable = approval.Approval_Save(cboApproval.SelectedItem.Value, txtName.Text.Trim(), txtDetail.Text.Trim(), txtPrice.Text.Trim(),
                                                  day, cboBranch.SelectedItem.Value, Session("usercode"))
            approvalid = approvaltable.Rows(0).Item("approvalid")
            If Not String.IsNullOrEmpty(fullfilename) Then
                approval.Image_Save(fullfilename, "Approval_BF", approvalid, Session("usercode"))
            End If
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & approvaltable.Rows(0).Item("code"))


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Function Confirm(approvalcode As String, usercode As String) As Boolean
        Dim approval As New Approval
        Dim result As Boolean = True

        Try
            approval.Confirm(approvalcode, usercode)
            result = True
        Catch ex As Exception
            result = False
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return result
    End Function
    Private Function chkPermissionApproval(approvalcode As String) As DataSet
        Dim permissionApproval As New Approval
        Dim appPermission As New DataSet
        Try
            appPermission = permissionApproval.ApprovalPermission(approvalcode)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return appPermission
    End Function
    Private Sub chkuser(userid As Integer)
        Dim approval_permission As New Users
        Dim appPermissionTable As New DataTable
        Try
            appPermissionTable = approval_permission.ApprovalPermissionRead(userid)
            am_id = appPermissionTable.Rows(0).Item("am_id")
            rm_id = appPermissionTable.Rows(0).Item("rm_id")
            dm_id = appPermissionTable.Rows(0).Item("dm_id")
            If Not Session("userid") = userid And
            Not Session("userid") = am_id And
            Not Session("userid") = rm_id And
            Not Session("userid") = dm_id And
            Not Session("secid").ToString = "2" Then
                flag = False
            End If
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub showdata(mytable As DataTable)
        Dim objbranch As New Branch
        With mytable.Rows(0)
            txtApprovalcode.Text = .Item("approvalcode").ToString
            txtStatus.Text = .Item("statusname").ToString
            Select Case .Item("statusid")
                Case = "1"
                    txtStatus.BackColor = Color.LightYellow
                Case = "2"
                    txtStatus.BackColor = Color.GreenYellow
                Case = "3"
                    txtStatus.BackColor = Color.IndianRed
                Case = "4"
                    txtStatus.BackColor = Color.Gray
                Case = "6"
                    txtStatus.BackColor = Color.LightGray
                Case = "7"
                    txtStatus.BackColor = Color.LightBlue
                Case = "8"
                    txtStatus.BackColor = Color.LightCoral
                Case = "9"
                    txtStatus.BackColor = Color.OrangeRed
                Case = "10"
                    txtStatus.BackColor = Color.MediumPurple
                Case = "11"
                    txtStatus.BackColor = Color.Brown
            End Select

            txtName.Text = .Item("name_request").ToString
            txtCreateBy.Text = .Item("name").ToString
            txtDocDate.Text = .Item("createdate").ToString
            txtCloseDate.Text = .Item("approvalclosedate").ToString
            txtOwnerApprovalName.Text = .Item("ownerapprovalname").ToString

            objbranch.SetComboBranchByAreaid(cboBranch, "0", Session("userid"), 1) 'แก้ bug ไม่โชว์สาขา
            cboApproval.SelectedIndex = cboApproval.Items.IndexOf(cboApproval.Items.FindByValue(.Item("approvallistid")))
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(.Item("branchid")))
            If Session("status") = "edit" Then
                txtPrice.Attributes.Add("type", "number")
                txtPrice.Text = .Item("price")
            Else
                txtPrice.Attributes.Remove("type")
                txtPrice.Text = String.Format("{0:n}", .Item("price"))
            End If

            txtOwnerApprovaldate.Text = .Item("ownerapprovaldate").ToString
            txtDay.Text = .Item("approvalday").ToString
            txtCloseDetail.Text = .Item("message_closeapproval").ToString
            txtDetail.Text = .Item("approvaldetail").ToString
            txtOwnerDisApprovalName.Text = .Item("ownerdisapprovalname").ToString
            txtDisApproval.Text = .Item("message_disapproval").ToString
            txtSupportby.Text = .Item("supportby").ToString
            txtSupportdate.Text = .Item("supportstartdate").ToString
            txtSupportFinished.Text = .Item("supportenddate").ToString

        End With
    End Sub

    Private Sub SetMenu()
        Select Case Session("status")
            Case = "new"
                cboApproval.Attributes.Remove("disabled")
                cboBranch.Attributes.Remove("disabled")
                lbcboApprovalMandatory.Visible = True
                lbtxtNameMandatory.Visible = True
                lbcboBranchMandatory.Visible = True
                lbDetailMandatory.Visible = True
                txtPrice.ReadOnly = False
                txtDay.ReadOnly = False
                txtDetail.ReadOnly = False
            Case = "read"
                cboApproval.Attributes.Add("disabled", "True")
                cboBranch.Attributes.Add("disabled", "True")
                lbcboApprovalMandatory.Visible = False
                lbtxtNameMandatory.Visible = False
                lbcboBranchMandatory.Visible = False
                lbDetailMandatory.Visible = False
                txtPrice.ReadOnly = True
                txtDay.ReadOnly = True
                txtDetail.ReadOnly = True
                txtName.ReadOnly = True
            Case = "write"
                cboApproval.Attributes.Add("disabled", "True")
                cboBranch.Attributes.Add("disabled", "True")
                lbcboApprovalMandatory.Visible = False
                lbtxtNameMandatory.Visible = False
                lbcboBranchMandatory.Visible = False
                lbDetailMandatory.Visible = False
                txtPrice.ReadOnly = True
                txtDay.ReadOnly = True
                txtDetail.ReadOnly = True
                txtName.ReadOnly = True
            Case = "edit"
                lbcboApprovalMandatory.Visible = True
                lbtxtNameMandatory.Visible = True
                lbcboBranchMandatory.Visible = True
                lbDetailMandatory.Visible = True
                txtPrice.ReadOnly = False
                txtDay.ReadOnly = False
                txtDetail.ReadOnly = False
                txtName.ReadOnly = False

        End Select
    End Sub
    Private Sub SetBtn(statusid As String)
        Select Case statusid
            Case = "1" 'รออนุมัติ
                btnConfirm.Visible = False
                btnCancel.Visible = True
                btnClose.Visible = False
                btnEdit.Visible = False
            Case = "2"  'อนุมัติ
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = True
                btnEdit.Visible = False
            Case = "3"  'ไม่อนุมัติ
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnEdit.Visible = False
            Case = "4"  'ปิดงาน
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnEdit.Visible = False
            Case = "5"  'เลยกำหนด
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = True
                btnEdit.Visible = False
            Case = "6"  'ยกเลิกการแจ้ง
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnEdit.Visible = False
            Case = "7"  'รอผู้แจ้งยืนยัน
                btnConfirm.Visible = True
                btnCancel.Visible = True
                btnClose.Visible = False
                btnEdit.Visible = True
            Case = "8"  'รอประสานงานยืนยัน
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnEdit.Visible = False
            Case = "9"  'กำลังจัดทำเอกสาร
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnEdit.Visible = False
            Case = "10"  'เอกสารครบถ้วน
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = True
                btnEdit.Visible = False
            Case = "11"  'ส่งเอกสารให้ผู้เกี่ยวข้อง
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnEdit.Visible = False
        End Select
    End Sub
    Private Function createtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("approvalcode", GetType(String))
        dt.Columns.Add("approvallistid", GetType(Integer))
        dt.Columns.Add("approvaldetail", GetType(String))
        dt.Columns.Add("ownerapprovalname", GetType(Integer))
        dt.Columns.Add("message_closeapproval", GetType(String))
        dt.Columns.Add("statusid", GetType(Integer))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("branchid", GetType(Integer))
        dt.Columns.Add("depid", GetType(Integer))
        dt.Columns.Add("updateby", GetType(Integer))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(Integer))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("name", GetType(String))
        dt.Columns.Add("working", GetType(Boolean))

        Return dt
    End Function
    Private Function createtablecomment() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("commentid", GetType(Integer))
        dt.Columns.Add("commentdetail", GetType(String))
        dt.Columns.Add("updateby", GetType(Integer))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(Integer))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("approvalid", GetType(Integer))

        Return dt
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim approval As New Approval
        Dim approvalcode As String

        Try
            approvalcode = approval.Approval_Cancel(txtApprovalcode.Text.Trim(), Session("usercode"))
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & approvalcode)


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            If Confirm(txtApprovalcode.Text, Session("usercode")) Then
                Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))
            End If
            confirmValue = "No"
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim imgname As New Approval
        Dim fullfilename As String = String.Empty
        If (FileUpload1.HasFile) Then
            Dim Extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
            Dim savePath As String = "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
            Dim di As String = System.IO.Path.GetDirectoryName(FileUpload1.FileName)
            Dim oldpath As String = di + FileUpload1.FileName

            Try
                Dim fileName As String
                fileName = imgname.GetImageName()
                savePath += fileName
                savePath += Extension
                FileUpload1.SaveAs(savePath)
                fullfilename += fileName
                fullfilename += Extension
                'img1.Attributes.Add("src", a)
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('upload file fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        End If
        Approval_Save(fullfilename)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Response.Redirect("../approval/approvalClose.aspx?approvalcode=" & Request.QueryString("approvalcode"))
    End Sub

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim approval As New Approval
        Dim approvalcode As String

        Try
            approvalcode = approval.Approval_Allow(txtApprovalcode.Text.Trim(), Session("usercode"))
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & approvalcode)


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function disApprovalByCode(ByVal approvalcode As String, ByVal message As String, ByVal updateby As String)
        Dim Approval As New Approval

        Try
            Approval.Approval_NotAllow(approvalcode, message, updateby)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Session("status") = "edit"
        Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))
    End Sub

    Private Sub btnCancelEdit_Click(sender As Object, e As EventArgs) Handles btnCancelEdit.Click
        Session("status") = "read"
        Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))
    End Sub

    Private Sub btnSaveEdit_Click(sender As Object, e As EventArgs) Handles btnSaveEdit.Click
        Dim approval As New Approval
        Dim day As Integer
        If String.IsNullOrEmpty(txtDay.Text) Then
            day = 0
        Else
            day = txtDay.Text
        End If
        Try
            approval.Approval_Edit(Request.QueryString("approvalcode"), cboApproval.SelectedItem.Value, txtName.Text.Trim(), txtDetail.Text.Trim(), txtPrice.Text.Trim(),
                                                  cboBranch.SelectedItem.Value, day, Session("userid"))
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        Dim approval As New Approval
        Try
            approval.Approval_Save_Comment(Request.QueryString("approvalcode"), txtComment.Text.Trim(), Session("userid"))
            Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnSupportAllow_Click(sender As Object, e As EventArgs) Handles btnSupportAllow.Click
        Dim approval As New Approval

        Try
            approval.Approval_Support_Allow(Request.QueryString("approvalcode"), Session("usercode"))
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SupportAllow fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnSupportFinished_Click(sender As Object, e As EventArgs) Handles btnSupportFinished.Click
        Dim approval As New Approval

        Try
            approval.Approval_Support_Completed(Request.QueryString("approvalcode"), Session("usercode"))
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SupportFinished fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnSupportForaward_Click(sender As Object, e As EventArgs) Handles btnSupportForaward.Click
        Dim approval As New Approval

        Try
            approval.Approval_Support_Foraward(Request.QueryString("approvalcode"), Session("usercode"))
            Session("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("approvalcode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SupportForaward fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class