Imports System.Drawing

Public Class AdvanceRequest
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public cntdt As Integer
    Public sumitem As Double

    Public PermissionOwner As DataSet '= createtable()
    Public Shared AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()

    Public verify As Boolean = False
    Public approval As Boolean = False
    Public flag As Boolean = True

    Public allOwner As String
    Public at As String
    Public approver As String
    Public verifier As String


    Dim md_code As String
    Dim fm_code As String
    Dim dm_code As String
    Dim sm_code As String
    Dim am_code As String

    Dim account_code As String = "SPP"

    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objNonPO As New NonPO
        Dim nonpods = New DataSet

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
            detailtable = createtableDetail()
            AttachTable = createtableAttach()
            CommentTable = createtablecomment()


            If Not Request.QueryString("ADV") Is Nothing Then

                Try
                    nonpods = objNonPO.NonPO_AdvanceRQ_Find(Request.QueryString("ADV"))
                    detailtable = nonpods.Tables(0)
                    AttachTable = nonpods.Tables(1)
                    CommentTable = nonpods.Tables(2)



                    If Not Session("status") = "edit" Then
                        Session("status") = "read"
                    End If

                    chkuser(detailtable.Rows(0).Item("createby"))

                    If (Session("usercode") = md_code Or
                    Session("usercode") = fm_code Or
                    Session("usercode") = dm_code Or
                    Session("usercode") = sm_code Or
                    Session("usercode") = am_code) And
                    (detailtable.Rows(0).Item("statusrqid") = 2) Then
                        Session("status") = "write"
                        'Dim SearchWithinThis As String = "ABCDEFGHIJKLMNOP"
                        'Dim SearchForThis As String = "DEF"
                        'Dim FirstCharacter As Integer = SearchWithinThis.IndexOf(SearchForThis)
                        PermissionOwner = chkPermissionNonPO(Request.QueryString("ADV"))
                        'deadline = PermissionOwner.Tables(0).Rows(0).Item("deadline").ToString

                        'For Each rows As DataRow In PermissionOwner.Tables(0).Rows
                        '    If rows("OwnerStatus") Then
                        '        ownerapproval += rows("sendto").ToString + ","
                        '    End If
                        'Next rows
                        'If ownerapproval.Length > 0 Then
                        '    ownerapproval = ownerapproval.Remove(ownerapproval.Length - 1, 1) 'ใช้ในหน้า html
                        'End If
                        If PermissionOwner.Tables(0).Rows.Count > 0 Then
                            at = "วิ่งเส้น : " + PermissionOwner.Tables(0).Rows(0).Item("at").ToString
                            approver = "ผู้มีสิทธิอนุมัติ : " + PermissionOwner.Tables(0).Rows(0).Item("approver").ToString
                            verifier = "ผู้ตรวจ : " + PermissionOwner.Tables(0).Rows(0).Item("verifier").ToString
                        End If




                        For Each row As DataRow In PermissionOwner.Tables(0).Rows
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

                        Next row
endprocess:
                    ElseIf (account_code.IndexOf(Session("usercode").ToString) > -1) And (detailtable.Rows(0).Item("statusrqid") = 3) Then
                        Session("status") = "account"
                        verify = True
                    End If

                    showdata(detailtable)
                    SetBtn(detailtable.Rows(0).Item("statusrqid"), detailtable.Rows(0).Item("createby"))
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            Else
                Session("status") = "new"
            End If

            Session("detailtable_advancerq") = detailtable
            Session("comment_advancerq") = CommentTable
            Session("attatch_advancerq") = AttachTable
        Else
            detailtable = Session("detailtable_advancerq")
            AttachTable = Session("attatch_advancerq")
            CommentTable = Session("comment_advancerq")
        End If

        SetMenu()
    End Sub

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
    Private Sub chkuser(userid As Integer)
        Dim objuser As New Users
        Dim NonPOPermissionTable As New DataTable
        Try
            searchjobslist(userid)
            NonPOPermissionTable = objuser.NonPOPermissionRead(userid)
            md_code = NonPOPermissionTable.Rows(0).Item("md_code")
            fm_code = NonPOPermissionTable.Rows(0).Item("fm_code")
            dm_code = NonPOPermissionTable.Rows(0).Item("dm_code")
            sm_code = NonPOPermissionTable.Rows(0).Item("sm_code")
            am_code = NonPOPermissionTable.Rows(0).Item("am_code")
            'chkuser_sub(md_code, userid)
            'chkuser_sub(fm_code, userid)
            'chkuser_sub(dm_code, userid)
            'chkuser_sub(sm_code, userid)
            'chkuser_sub(am_code, userid)
            allOwner = "[ md : " + md_code + "],[ fm : " + fm_code + "],[ dm : " + dm_code + "],[ sm : " + sm_code + "],[ am : " + am_code + "]"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

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
    Private Function createtableDetail() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("advancerequestcode", GetType(String))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("statusrqid", GetType(Integer))
        dt.Columns.Add("statusrqname", GetType(String))

        dt.Columns.Add("detail", GetType(String))

        dt.Columns.Add("approvalrqby", GetType(String))
        dt.Columns.Add("approvalrqdate", GetType(String))
        dt.Columns.Add("verifyrqby", GetType(String))
        dt.Columns.Add("verifyrqdate", GetType(String))

        dt.Columns.Add("updateby", GetType(Integer))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(Integer))
        dt.Columns.Add("createdate", GetType(String))

        dt.Columns.Add("ownerid", GetType(Integer))

        dt.Columns.Add("updateby_name", GetType(String))
        dt.Columns.Add("createby_name", GetType(String))
        dt.Columns.Add("ownerby_name", GetType(String))

        Return dt
    End Function

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdate", GetType(Date))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("closedate", GetType(Date))
        dt.Columns.Add("detailFollow", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Sub SetBtn(statusid As String, createby As Integer)
        'in advance use statusrqid 1,2,3,6,11,12
        Select Case statusid
            Case = "1" 'รอยืนยัน
                txtStatusRq.BackColor = Color.LightBlue
                btnConfirm.Visible = True
                btnCancel.Visible = True
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = True
                btnClearAdvance.Visible = False

                If createby = Session("userid") Then
                    btnAddAttatch.Visible = True
                Else
                    btnAddAttatch.Visible = False
                End If

            Case = "2" 'รออนุมัติ
                txtStatusRq.BackColor = Color.LightYellow
                btnConfirm.Visible = False
                btnCancel.Visible = True
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False

                btnAddAttatch.Visible = False

            Case = "3" 'รอการเงินตรวจสอบ
                txtStatusRq.BackColor = Color.LightCoral
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False

                If account_code.IndexOf(Session("usercode").ToString) > -1 Then
                    btnAddAttatch.Visible = True
                Else
                    btnAddAttatch.Visible = False
                End If
            Case = "6" 'ไม่ผ่านการอนุมัติ
                txtStatusRq.BackColor = Color.IndianRed
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False

                btnAddAttatch.Visible = False

            Case = "11" 'รอเคลียร์ค้างชำระ
                txtStatusRq.BackColor = Color.Brown
                txtStatusRq.ForeColor = Color.White
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = True

                btnAddAttatch.Visible = False

            Case = "12" 'ชำระเงินเสร็จสิ้น
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False
                txtStatusRq.BackColor = Color.GreenYellow

                btnAddAttatch.Visible = False

            Case = "14" 'ยกเลิก
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False
                txtStatusRq.BackColor = Color.LightGray

                btnAddAttatch.Visible = False

        End Select
    End Sub

    Private Sub showdata(mytable As DataTable)
        Dim objbranch As New Branch
        With mytable.Rows(0)
            txtNonPOcode.Text = .Item("advancerequestcode").ToString
            txtStatusRq.Text = .Item("statusrqname").ToString

            txtbalance.Text = String.Format("{0:n2}", .Item("balance"))

            txtCreateBy.Text = .Item("createby_name").ToString
            txtDocDate.Text = .Item("createdate").ToString
            'txtOwnerby.Text = .Item("ownerby_name").ToString

            txtApprovalby.Text = .Item("approvalrqby").ToString
            txtApprovalDate.Text = .Item("approvalrqdate").ToString

            txtSupportby.Text = .Item("verifyrqby").ToString
            txtSupportdate.Text = .Item("verifyrqdate").ToString
            If Session("status") = "edit" Then
                txtamount.Attributes.Add("type", "number")
                txtamount.Text = .Item("amount")
            Else
                txtamount.Attributes.Remove("type")
                txtamount.Text = String.Format("{0:n2}", .Item("amount"))
            End If

            txtdetail.Text = .Item("detail").ToString

        End With
    End Sub

    Private Sub SetMenu()
        Select Case Session("status")
            Case = "new"
                lbMandatoryamount.Visible = True
                lbMandatorydetail.Visible = True
                txtamount.ReadOnly = False
                txtdetail.ReadOnly = False


                'กล่อง comment & attatch file
                card_comment.Visible = False
                card_attatch.Visible = False

            Case = "read"
                lbMandatoryamount.Visible = False
                lbMandatorydetail.Visible = False
                txtamount.ReadOnly = True
                txtdetail.ReadOnly = True
                'searchjobslist()


                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "write"
                lbMandatoryamount.Visible = False
                lbMandatorydetail.Visible = False
                txtamount.ReadOnly = True
                txtdetail.ReadOnly = True
                'searchjobslist()


                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "edit"
                lbMandatoryamount.Visible = True
                lbMandatorydetail.Visible = True
                txtamount.ReadOnly = False
                txtdetail.ReadOnly = False
                'searchjobslist()


                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "account"
                lbMandatoryamount.Visible = True
                lbMandatorydetail.Visible = True
                txtamount.ReadOnly = False
                txtdetail.ReadOnly = False
                'searchjobslist()

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True


        End Select
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objNonPO As New NonPO
        Dim dt As DataTable
        Try
            dt = objNonPO.NonPO_AdvanceRequest_Save(txtamount.Text.Trim(), txtdetail.Text.Trim(), Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & dt.Rows(0).Item("code"))
endprocess:
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Session("status") = "edit"
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))
    End Sub

    Private Sub btnCancelEdit_Click(sender As Object, e As EventArgs) Handles btnCancelEdit.Click
        Session("status") = "read"
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))
    End Sub

    Private Sub btnSaveEdit_Click(sender As Object, e As EventArgs) Handles btnSaveEdit.Click
        Dim objnonpo As New NonPO
        Try
            objnonpo.NonPO_AdvanceRequest_Edit(Request.QueryString("ADV").ToString, txtamount.Text.Trim(), txtdetail.Text.Trim(), Session("userid"))
            Session("status") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AdvanceRequest_Confirm(Request.QueryString("ADV").Trim, Session("usercode"))
            Session("status") = "read"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AdvanceRequest_Cancel(Request.QueryString("ADV").Trim, Session("usercode"))
            Session("status") = "read"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub

    Private Sub btnClearAdvance_Click(sender As Object, e As EventArgs) Handles btnClearAdvance.Click
        Response.Redirect("../Advance/ClearAdvance.aspx?f=ADV&code_ref=" & Request.QueryString("ADV"))

    End Sub
    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 6
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("status") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("status") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("status") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("status") = "ชำระเงินเสร็จสิ้น" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("status") = "ไม่ผ่านการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("status") = "รอการเงินตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightCoral
            ElseIf Data.Item("status") = "รอเคลียร์ค้างชำระ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Brown
            End If
        End If
    End Sub

    Private Sub searchjobslist(userid As Integer)

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            itemtable = objNonPO.AdvanceRQList_For_Owner(userid, 11)

            Session("joblist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search advlist fail');"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindData()

        cntdt = itemtable.Rows.Count
        sumitem = Convert.ToDouble(itemtable.Compute("SUM(amount)", String.Empty))
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AdvanceRequest_Allow(Request.QueryString("ADV"), Session("usercode"))
            Session("status") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))
endprocess:
    End Sub

    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AdvanceRequest_Verify(Request.QueryString("ADV"), Session("usercode"))
            Session("status") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))
endprocess:
    End Sub
    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        Dim approval As New Approval
        Try
            approval.Save_Comment_By_Code(Request.QueryString("ADV"), txtComment.Text.Trim(), Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub
End Class