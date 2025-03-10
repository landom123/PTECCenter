Imports ClosedXML.Excel.XLPredefinedFormat

Public Class ApprovalManage_APP
    Inherits System.Web.UI.Page

    Public menutable As DataTable

    'Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public detailtable As DataTable '= createdetailtable()
    'Public itemtable As DataTable = createdetailtable()
    Public usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        usercode = Session("usercode")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

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
        Dim objjob As New jobs

        If Not IsPostBack() Then
            detailtable = createtable()
            CommentTable = createtablecomment()

            If Not Request.QueryString("approvalcode") Is Nothing Then
                Dim approvalcode = Request.QueryString("approvalcode")
                Dim new_approvalcode = Split(approvalcode, "/")(0)
                Dim category As String = Split(new_approvalcode, Right(new_approvalcode, 9))(0)
                Dim nonpoDs = New DataSet
                If category = "APP" Then
                    objapproval.SetCboApprovalStatus(cboStatusFollow)
                Else
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('APP only!')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    GoTo endprocess
                End If
                Try

                    approvaldataset = objapproval.Approval_Find(Request.QueryString("approvalcode"))
                    detailtable = approvaldataset.Tables(0)
                    CommentTable = approvaldataset.Tables(4)
                    With detailtable.Rows(0)
                        lbapprovalcode.Text = .Item("approvalcode")
                        badgeapprovalcode.InnerText = .Item("statusname")
                        cboStatusFollow.SelectedIndex = cboStatusFollow.Items.IndexOf(cboStatusFollow.Items.FindByValue(.Item("statusid")))
                        txtDay.Text = .Item("approvalday").ToString
                        txtPrice.Text = .Item("price")
                        badgeapprovalcode.HRef = "../approval/Approval.aspx?approvalcode=" & Request.QueryString("approvalcode")
                    End With

                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try


            End If

            Session("maintable_APP") = detailtable
            Session("comment_APP") = CommentTable
            'Session("attatch_APP") = AttachTable
        Else

            detailtable = Session("maintable_APP")
            'AttachTable = Session("attatch_APP")
            CommentTable = Session("comment_APP")
        End If
endprocess:
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
    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        Dim approval As New Approval
        Try
            approval.Approval_Save_Comment(Request.QueryString("approvalcode"), txtComment.Text.Trim(), Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Response.Redirect("../Master/ApprovalManage_APP.aspx?approvalcode=" & Request.QueryString("approvalcode"))
    End Sub

    Private Function validatedata(validateType) As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        Select Case validateType
            Case "status"
                If Not cboStatusFollow.SelectedItem.Value > 0 Then
                    result = False
                    msg = "กรุณาเลือกสถานะ"
                    GoTo endprocess
                End If
            Case "price"
                If Not txtPrice.Text > 0 Then
                    result = False
                    msg = "กรุณากรอกค่าใช้จ่าย"
                    GoTo endprocess
                End If
            Case "day"
                If Not txtDay.Text > 0 Then
                    result = False
                    msg = "กรุณากรอกจำนวนวัน"
                    GoTo endprocess
                End If

        End Select




endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If

        Return result
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validatedata("status") Then
            Dim approval As New Approval
            Try
                approval.ManageStatus(Request.QueryString("approvalcode"), cboStatusFollow.SelectedItem.Value, Session("usercode"))

            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('Save fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            Response.Redirect("../Master/ApprovalManage_APP.aspx?approvalcode=" & Request.QueryString("approvalcode"))
endprocess:
        End If
    End Sub
    Private Sub btnSavePrice_Click(sender As Object, e As EventArgs) Handles btnSavePrice.Click
        If validatedata("price") Then
            Dim approval As New Approval
            Try
                approval.ManagePrice(Request.QueryString("approvalcode"), txtPrice.Text.Trim, Session("usercode"))

            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('Save fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            Response.Redirect("../Master/ApprovalManage_APP.aspx?approvalcode=" & Request.QueryString("approvalcode"))
endprocess:
        End If
    End Sub

    Private Sub btnSaveDay_Click(sender As Object, e As EventArgs) Handles btnSaveDay.Click
        If validatedata("day") Then
            Dim approval As New Approval
            Try
                approval.ManageDay(Request.QueryString("approvalcode"), txtDay.Text.Trim, Session("usercode"))

            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('Save fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            Response.Redirect("../Master/ApprovalManage_APP.aspx?approvalcode=" & Request.QueryString("approvalcode"))
endprocess:
        End If
    End Sub
End Class