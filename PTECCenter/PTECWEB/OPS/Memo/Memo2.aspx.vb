Imports System.Drawing

Public Class Memo2
    Inherits System.Web.UI.Page

    Public menutable As DataTable

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

    Public detailtable As DataTable '= createdetailtable()
    Public PermissionOwner As DataSet '= createtable()
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim attatch As New Attatch

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

        If Not IsPostBack() Then
            detailtable = createdetailtable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()

            If Not Request.QueryString("MemoCode") Is Nothing Then

                Try
                    attatch.SetCboMyfile(cboMyfile, Session("userid"))
                    findMemo()
                    setmain(detailtable)

                    statusid = detailtable.Rows(0).Item("statusid")
                    createby = detailtable.Rows(0).Item("ownerid")
                    chkuser(createby)

                    If (statusid = 1) Then
                        PermissionOwner = chkPermission(Request.QueryString("MemoCode"))

                        at = "วิ่งเส้น : " + PermissionOwner.Tables(0).Rows(0).Item("at").ToString
                        approver = "ผู้มีสิทธิอนุมัติ : " + PermissionOwner.Tables(0).Rows(0).Item("approver").ToString
                        verifier = "ผู้ตรวจ : " + PermissionOwner.Tables(0).Rows(0).Item("verifier").ToString
                        now_action = "ผู้ที่ต้องปฏิบัติงาน : " + PermissionOwner.Tables(0).Rows(1).Item("approver").ToString + PermissionOwner.Tables(0).Rows(1).Item("verifier").ToString
                    End If

                    setPermission(usercode, statusid)
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            End If
        Else
            detailtable = Session("detailtable_memo")
            CommentTable = Session("comment_memo")
            AttachTable = Session("attatch_memo")

            Try
                statusid = detailtable.Rows(0).Item("statusid")
            Catch ex As Exception
                statusid = 0
            End Try
            Try
                createby = detailtable.Rows(0).Item("ownerid")

            Catch ex As Exception
                createby = 0
            End Try
        End If




        SetBtn(statusid, createby)
    End Sub
    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("memo_code", GetType(String))
        dt.Columns.Add("commitdate", GetType(String))
        dt.Columns.Add("name", GetType(String))
        dt.Columns.Add("type_name", GetType(String))
        dt.Columns.Add("sentto", GetType(String))
        dt.Columns.Add("sentcc", GetType(String))
        dt.Columns.Add("subject", GetType(String))
        dt.Columns.Add("amount", GetType(String))
        dt.Columns.Add("statusid", GetType(Integer))
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("approvalby", GetType(String))
        dt.Columns.Add("approvaldate", GetType(String))
        dt.Columns.Add("verifyby", GetType(String))
        dt.Columns.Add("verifydate", GetType(String))
        dt.Columns.Add("memodtl_type", GetType(String))
        dt.Columns.Add("memodtl_Content", GetType(String))

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

    Private Sub chkuser(userid As Integer)
        Dim objuser As New Users
        Dim NonPOPermissionTable As New DataTable
        Try
            NonPOPermissionTable = objuser.NonPOPermissionRead(userid)
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
        'สำเช็คสิทธิการเข้าถึง URL นี้ ดูต่างอย่าง flag ได้ที่หน้า ขออนุมัติ ROD -- OPS\approval\Approval.aspx
        Dim strSplit As Array
        Dim i As Integer

        strSplit = allusercode.Split(",")

        For i = 0 To strSplit.Length - 1
            If Not Session("userid") = userid And
                Not Session("usercode") = strSplit(i) And
                Not Session("secid").ToString = "2" And
                Not Session("secid").ToString = "35" And
                Not Session("depid").ToString = "24" And
                Not Session("depid").ToString = "25" And
                Not Session("depid").ToString = "2" And
                Not Session("depid").ToString = "4" Then
                flag = False
            End If

        Next
    End Sub
    Private Sub findMemo()
        Dim memoDs = New DataSet
        Dim objMemo As New Memo
        Try
            memoDs = objMemo.Memo_Find(Request.QueryString("MemoCode"))
            '-- table 0 = Head
            '-- table 1 = main
            '-- table 2 = detail
            '-- table 3 = attach
            '-- table 4 = comment

            detailtable = memoDs.Tables(0)
            AttachTable = memoDs.Tables(1)
            CommentTable = memoDs.Tables(2)

            Session("detailtable_memo") = detailtable
            Session("comment_memo") = CommentTable
            Session("attatch_memo") = AttachTable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub setmain(dt As DataTable)
        With dt
            'Select Case .Rows(0).Item("statusid").ToString
            '    Case = "1" '1 : รออนุมัติ
            '        txtStatus.Attributes.Add("class", "btn btn-warning")
            '    Case = "2" '2 : ได้รับการอนุมัติ
            '        txtStatus.Attributes.Add("class", "btn btn-success")
            '    Case = "3" '3 : ไม่ผ่านการอนุมัติ
            '        txtStatus.Attributes.Add("class", "btn btn-danger")
            '    Case = "4" '4 : ยกเลิก
            '        txtStatus.Attributes.Add("class", "btn btn-danger")
            'End Select
            Select Case .Rows(0).Item("statusid").ToString
                Case = "1" '1 : รอยืนยัน
                    statusmemo.Attributes.Add("class", "btn btn-warning")
                Case = "2" '2 : ได้รับการอนุมัติ
                    statusmemo.Attributes.Add("class", "btn btn-success")
                Case = "3" '3 : ไม่ผ่านการอนุมัติ
                    statusmemo.Attributes.Add("class", "btn btn-danger")
                Case = "4" '4 : ยกเลิก
                    statusmemo.Attributes.Add("class", "btn btn-danger")
            End Select
            statusmemo.Text = .Rows(0).Item("statusname").ToString




            '------------------Head--------------------------
            txtMemoCode.Text = .Rows(0).Item("Memo_Code").ToString

            txtName.Text = .Rows(0).Item("ownername").ToString
            txtTo.Text = .Rows(0).Item("sendto").ToString
            txtCc.Text = .Rows(0).Item("sendcc").ToString
            'txtStatus.InnerText = .Rows(0).Item("StatusName").ToString
            txtMemoDate.Text = .Rows(0).Item("CommitDate").ToString
            txtSubject.Text = .Rows(0).Item("Subject").ToString
            txtMemoType.Text = .Rows(0).Item("Type_Name").ToString
            txtAmount.Text = String.Format("{0:n2}", .Rows(0).Item("amount"))




            '------------------Signatory-----------------------

            txtVeriftyBy.Text = .Rows(0).Item("verifyby").ToString
            txtVeriftyDate.Text = .Rows(0).Item("verifydate").ToString
            txtApprovalBy.Text = .Rows(0).Item("approvalby").ToString
            txtApprovalDate.Text = .Rows(0).Item("approvaldate").ToString



            '----------------Content-----------------------

            If .Rows(0).Item("MemoDtl_Type").ToString = "DETAIL" Then
                divcontent__file.Visible = False
                divcontent__text.Visible = True

                txtContent.Text = .Rows(0).Item("MemoDtl_Content").ToString()

            ElseIf .Rows(0).Item("MemoDtl_Type").ToString = "FILE" Then
                divcontent__file.Visible = True
                divcontent__text.Visible = False

                fileContent.Src = .Rows(0).Item("MemoDtl_Content").ToString()

            End If
        End With
    End Sub

    Private Sub SetBtn(statusid As String, createby As Integer)
        Select Case statusid
            Case = "1" '1 : รออนุมัติ
                If createby = Session("userid") Then
                    btnCancel.Enabled = True

                Else
                    btnCancel.Enabled = False
                End If

                btnAddAttatch.Visible = True
            Case = "2" '2 : ได้รับการอนุมัติ
                btnCancel.Enabled = False

                btnAddAttatch.Visible = False
            Case = "3" '3 : ไม่ผ่านการอนุมัติ
                btnCancel.Enabled = False

                btnAddAttatch.Visible = False
            Case = "4" '4 : ยกเลิก
                btnCancel.Enabled = False

                btnAddAttatch.Visible = False
        End Select
    End Sub



    Private Function chkPermission(code As String) As DataSet
        Dim objMemo As New Memo
        Dim npoPermission As New DataSet
        Try
            npoPermission = objMemo.MemoPermission(code)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('NonPO_PermisstionOwner fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return npoPermission
    End Function

    Private Sub setPermission(usercode As String, statusid As Integer)
        If (usercode = md_code Or
                    usercode = fm_code Or
                    usercode = dm_code Or
                    usercode = sm_code Or
                    usercode = am_code) And
                    PermissionOwner IsNot Nothing And
                    (detailtable.Rows(0).Item("statusid") = 1) Then
            For Each row As DataRow In PermissionOwner.Tables(0).Rows
                If row("status").ToString = "now" Then
                    If row("approver").ToString.IndexOf("MD") > -1 Then
                        If (md_code.IndexOf(usercode) > -1) Then
                            approval = True
                            GoTo endprocess
                        End If
                    End If

                    If row("approver").ToString.IndexOf("FM") > -1 Then
                        If (fm_code.IndexOf(usercode) > -1) Then
                            approval = True
                            GoTo endprocess
                        End If
                    End If

                    If row("approver").ToString.IndexOf("DM") > -1 Then
                        If (dm_code.IndexOf(usercode) > -1) Then
                            approval = True
                            GoTo endprocess
                        End If
                    End If

                    If row("approver").ToString.IndexOf("SM") > -1 Then
                        If (sm_code.IndexOf(usercode) > -1) Then
                            approval = True
                            GoTo endprocess
                        End If
                    End If

                    If row("approver").ToString.IndexOf("AM") > -1 Then
                        If (am_code.IndexOf(usercode) > -1) Then
                            approval = True
                            GoTo endprocess
                        End If
                    End If

                    If detailtable.Rows(0).Item("statusid") = 1 Then
                        If row("verifier").ToString.IndexOf("MD") > -1 Then
                            If (md_code.IndexOf(usercode) > -1) Then
                                verify = True
                                GoTo endprocess
                            End If
                        ElseIf row("verifier").ToString.IndexOf("FM") > -1 Then
                            If (fm_code.IndexOf(usercode) > -1) Then
                                verify = True
                                GoTo endprocess
                            End If
                        ElseIf row("verifier").ToString.IndexOf("DM") > -1 Then
                            If (dm_code.IndexOf(usercode) > -1) Then
                                verify = True
                                GoTo endprocess
                            End If
                        ElseIf row("verifier").ToString.IndexOf("SM") > -1 Then
                            If (sm_code.IndexOf(usercode) > -1) Then
                                verify = True
                                GoTo endprocess
                            End If
                        ElseIf row("verifier").ToString.IndexOf("AM") > -1 Then
                            If (am_code.IndexOf(usercode) > -1) Then
                                verify = True
                                GoTo endprocess
                            End If
                        End If
                    End If
                End If

            Next row
endprocess:

        End If
    End Sub

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click

        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("MemoCode"), txtComment.Text.Trim(), Session("userid"))
            findMemo()
            setmain(detailtable)
            txtComment.Text = ""

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:
    End Sub

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click

        Dim objMemo As New Memo
        Try
            objMemo.Memo_Allow(Request.QueryString("MemoCode"), Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect(Request.RawUrl)
endprocess:
    End Sub

    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click

        Dim objapproval As New Approval

        Try
            objapproval.ApprovalHO_Verify(Request.QueryString("MemoCode"), Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect(Request.RawUrl)
endprocess:
    End Sub

    Private Sub btnDisApproval_Click(sender As Object, e As EventArgs) Handles btnDisApproval.Click

        Dim objMemo As New Memo
        Try
            objMemo.Memo_NotAllow(Request.QueryString("MemoCode"), Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Allow fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect(Request.RawUrl)
endprocess:
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Dim objMemo As New Memo
        Try
            objMemo.Memo_Cancel(Request.QueryString("MemoCode"), Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect(Request.RawUrl)
endprocess:
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("MemoRequest.aspx")
    End Sub
End Class