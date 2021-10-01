Public Class PaymentPage
    Inherits System.Web.UI.Page
    Public Shared AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public menutable As DataTable
    Public detailtable As DataTable '= createdetailtable()
    Public maintable As DataTable '= createmaintable()
    Public urlref As String = ""
    Public total As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch
        Dim approval As New Approval
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier

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
            maintable = createmaintable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()

            Session("detailtable_payment") = detailtable
            Session("maintable_payment") = maintable
            Session("comment_payment") = CommentTable
            Session("attatch_payment") = AttachTable
            Session("status") = "new"

            'insert comment
            With CommentTable
                .Rows.Add(0, "test", "ธนพล", Now(), "ธนพล", Now(), "ADV201000001")
                .Rows.Add(1, "test1", "ธนพล", Now(), "ธนพล", Now(), "ADV201000001")
                .Rows.Add(2, "test23", "ทศกัน", Now(), "ทศกัน", Now(), "ADV201000001")
            End With

            'insert AttachTable
            With AttachTable
                .Rows.Add(0, "test", "http://google.com/", "tttt")
                .Rows.Add(1, "test1", "http://google.com/", "ADV201000001")
            End With


            'objsupplier.SetCboVendorByName(cboName, Session("username"))

            objbranch.SetComboBranch(cboBranch, Session("usercode"))
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
            'objsupplier.SetCboVendorByName(cboName, "")
            'objsupplier.SetCboVendorByName(cboVendor, "")
            SetCboUsers(cboOwner)
            objsupplier.SetCboVendorByName(cboVendor, "")
            setHead()

            If Not Request.QueryString("NonpoCode") Is Nothing Then

                Session("status_clearadvance") = "new"
            ElseIf Not Request.QueryString("f") Is Nothing Then
                Dim objjob As New jobs
                Dim ds As New DataSet
                If Not Request.QueryString("code_ref") Is Nothing And Not Request.QueryString("code_ref_dtl") Is Nothing Then
                    Session("status_clearadvance") = "New"
                    codeRef.Text = Request.QueryString("code_ref").ToString + "_" + Request.QueryString("code_ref_dtl").ToString
                    ds = objjob.setNonPODtl_by_coderef(Request.QueryString("f").ToString, Request.QueryString("code_ref").ToString, Request.QueryString("code_ref_dtl").ToString, Session("usercode").ToString)

                    detailtable = ds.Tables(0)
                    urlref = ds.Tables(1).Rows(0).Item("urlref").ToString
                ElseIf Not Request.QueryString("code_ref") Is Nothing And Request.QueryString("code_ref_dtl") Is Nothing Then
                    Session("status_clearadvance") = "New"
                    codeRef.Text = Request.QueryString("code_ref").ToString
                    ds = objjob.setNonPODtl_by_coderef(Request.QueryString("f").ToString, Request.QueryString("code_ref").ToString, "", Session("usercode").ToString)
                    detailtable = ds.Tables(0)

                    'urlref = ds.Tables(1).Rows(0).Item("urlref").ToString

                End If
                total = String.Format("{0:n}", Convert.ToInt32(If(IsDBNull(detailtable.Compute("SUM(amount)", String.Empty)), 0, detailtable.Compute("SUM(amount)", String.Empty))))

            Else

            End If
        Else

            detailtable = Session("detailtable_payment")
            maintable = Session("maintable_payment")
            CommentTable = Session("comment_payment")
            AttachTable = Session("attatch_payment")

        End If


    End Sub

    Private Sub setHead()

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

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("row", GetType(Integer))
        dt.Columns.Add("listname", GetType(String))
        dt.Columns.Add("accountcode", GetType(Integer))
        dt.Columns.Add("dep", GetType(String))
        dt.Columns.Add("cc", GetType(String))
        dt.Columns.Add("pp", GetType(String))
        dt.Columns.Add("amount", GetType(String))
        dt.Columns.Add("vat", GetType(String))

        Return dt
    End Function

    Private Function createmaintable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("advid", GetType(Double)) 'new =0
        dt.Columns.Add("advno", GetType(String))
        dt.Columns.Add("statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("statusname", GetType(String)) 'new=new

        dt.Columns.Add("branchid", GetType(Double))
        dt.Columns.Add("depid", GetType(Double))
        dt.Columns.Add("secid", GetType(Double))

        dt.Columns.Add("Withdraw_by", GetType(String))
        dt.Columns.Add("Withdraw_date", GetType(String))
        dt.Columns.Add("Service_by", GetType(String))
        dt.Columns.Add("Service_date", GetType(String))
        dt.Columns.Add("Verify_by", GetType(String))
        dt.Columns.Add("Verify_date", GetType(String))
        dt.Columns.Add("Approval_by", GetType(String))
        dt.Columns.Add("Approval_date", GetType(String))
        '---------------------------------------
        dt.Columns.Add("updateby", GetType(String))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))

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
    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection(cboSection, depid)
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
        Response.Redirect("../approval/approval.aspx?approvalcode=" & Request.QueryString("paymentcode"))
endprocess:
    End Sub

End Class