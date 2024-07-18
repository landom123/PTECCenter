Public Class NonPOManage_ADV
    Inherits System.Web.UI.Page

    Public menutable As DataTable

    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public nonpodt As DataTable '= createdetailtable()
    'Public itemtable As DataTable = createdetailtable()
    Public usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objNonpo As New NonPO
        Dim attatch As New Attatch
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
        Dim urlCurrent As String = Request.Url.ToString()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "")
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
            nonpodt = createmaintable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()

            If Not Request.QueryString("NonpoCode") Is Nothing Then
                Dim nonpocode = Request.QueryString("NonpoCode")
                Dim category As String = Split(nonpocode, Right(nonpocode, 9))(0)
                Dim nonpoDs = New DataSet
                If category = "ADV" Then
                    objNonpo.SetCboStatusbyNonpocategory(cboStatusFollow, category)
                Else
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('ADV only!')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    GoTo endprocess
                End If
                Try

                    attatch.SetCboMyfile(cboMyfile, Session("userid"))
                    nonpoDs = objNonpo.NonPO_AdvanceRQ_Find(Request.QueryString("NonpoCode"))

                    nonpodt = nonpoDs.Tables(0)
                    AttachTable = nonpoDs.Tables(1)
                    CommentTable = nonpoDs.Tables(2)

                    With nonpodt.Rows(0)
                        lbapprovalcode.Text = .Item("advancerequestcode")
                        badgeapprovalcode.InnerText = .Item("statusrqname")
                        cboStatusFollow.SelectedIndex = cboStatusFollow.Items.IndexOf(cboStatusFollow.Items.FindByValue(.Item("statusrqid")))
                        badgeapprovalcode.HRef = "../Non-PO/Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("NonpoCode")
                    End With

                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try


            End If

            Session("maintable_payment") = nonpodt
            Session("comment_payment") = CommentTable
            Session("attatch_payment") = AttachTable
        Else

            nonpodt = Session("maintable_payment")
            AttachTable = Session("attatch_payment")
            CommentTable = Session("comment_payment")
        End If
endprocess:
    End Sub
    Private Function createmaintable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("advancerequestcode", GetType(String))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("amount_more", GetType(Double))
        dt.Columns.Add("statusrqid", GetType(Integer))
        dt.Columns.Add("statusrqname", GetType(String))

        dt.Columns.Add("detail", GetType(String))

        dt.Columns.Add("approvalrqby", GetType(String))
        dt.Columns.Add("approvalrqdate", GetType(String))
        dt.Columns.Add("verifyrqby", GetType(String))
        dt.Columns.Add("verifyrqdate", GetType(String))
        dt.Columns.Add("accountverifyrqby", GetType(String))
        dt.Columns.Add("accountverifyrqdate", GetType(String))

        dt.Columns.Add("duedate", GetType(String))

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

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("NonpoCode"), txtComment.Text.Trim(), Session("userid"))
            txtComment.Text = ""

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Master/NonPOManage_ADV.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))

endprocess:
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objnonpo As New NonPO
        Try
            objnonpo.ManageNonPO(Request.QueryString("NonpoCode"), cboStatusFollow.SelectedItem.Value, Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Master/NonPOManage_ADV.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub
End Class