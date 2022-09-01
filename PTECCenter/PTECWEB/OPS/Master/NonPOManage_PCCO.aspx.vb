Public Class NonPOManage_PCCO
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

        Dim objjob As New jobs

        If Not IsPostBack() Then
            nonpodt = createmaintable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()

            If Not Request.QueryString("NonpoCode") Is Nothing Then
                Dim nonpocode = Request.QueryString("NonpoCode")
                Dim category As String = Split(nonpocode, Right(nonpocode, 9))(0)
                Dim nonpoDs = New DataSet
                If category = "PCCO" Then
                    objNonpo.SetCboStatusbyNonpocategory(cboStatusFollow, category)
                Else
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('PCCO only!')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    GoTo endprocess
                End If
                Try
                    Attatch.SetCboMyfile(cboMyfile, Session("userid"))
                    nonpoDs = objNonpo.NonPO_Find(Request.QueryString("NonpoCode"))

                    nonpodt = nonpoDs.Tables(1)
                    AttachTable = nonpoDs.Tables(3)
                    CommentTable = nonpoDs.Tables(4)

                    With nonpodt.Rows(0)
                        lbapprovalcode.Text = .Item("nonpocode")
                        badgeapprovalcode.InnerText = .Item("statusname")
                        cboStatusFollow.SelectedIndex = cboStatusFollow.Items.IndexOf(cboStatusFollow.Items.FindByValue(.Item("statusid")))
                        badgeapprovalcode.HRef = "../Non-PO/PettyCash/PettyCashCO2.aspx?NonpoCode=" & Request.QueryString("NonpoCode")
                    End With

                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try


            End If


            Session("maintable_pcco") = nonpodt
            Session("comment_pcco") = CommentTable
            Session("attatch_pcco") = AttachTable
        Else

            nonpodt = Session("maintable_pcco")
            AttachTable = Session("attatch_pcco")
            CommentTable = Session("comment_pcco")
        End If
endprocess:
    End Sub
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
        Response.Redirect("../Master/NonPOManage_PCCO.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))

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
        Response.Redirect("../Master/NonPOManage_PCCO.aspx?NonpoCode=" & Request.QueryString("NonpoCode"))
endprocess:
    End Sub
End Class