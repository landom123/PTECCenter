Public Class ClearAdvance
    Inherits System.Web.UI.Page
    Public Shared AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public Shared detailtable As DataTable '= createdetailtable()
    Public maintable As DataTable '= createmaintable()
    Public head As DataTable
    Public menutable As DataTable
    Public total As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objNonpo As New NonPO
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
            head = createHeadtable()
            detailtable = createdetailtable()
            maintable = createmaintable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()

            Session("detailtable_clearadvance") = detailtable
            Session("maintable_clearadvance") = maintable
            Session("comment_clearadvance") = CommentTable
            Session("attatch_clearadvance") = AttachTable
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


            objbranch.SetComboBranch(cboBranch, Session("usercode"))
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
            'objsupplier.SetCboVendorByName(cboName, "")
            'objsupplier.SetCboVendorByName(cboVendor, "")
            SetCboUsers(cboOwner)
            setmain()

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

                If Not Session("status_clearadvance") = "edit" Then
                    Session("status_clearadvance") = "read"
                End If
                SetBtn(1)

            ElseIf Not Request.QueryString("f") Is Nothing Then
                Dim objjob As New jobs
                Dim ds As New DataSet
                If Not Request.QueryString("code_ref") Is Nothing And Not Request.QueryString("code_ref_dtl") Is Nothing Then
                    Session("status_clearadvance") = "new"
                    Dim pbjjob As New jobs


                ElseIf Not Request.QueryString("code_ref") Is Nothing And Request.QueryString("code_ref_dtl") Is Nothing Then
                    Session("status_clearadvance") = "new"
                    codeRef.Text = Request.QueryString("code_ref").ToString
                    ds = objjob.setNonPODtl_by_coderef(Request.QueryString("f").ToString, Request.QueryString("code_ref").ToString, "", Session("usercode").ToString)
                    head = ds.Tables(0)
                    sethead(head)
                End If
            Else
                Session("status_clearadvance") = "new"

            End If
        Else

            detailtable = Session("detailtable_clearadvance")
            maintable = Session("maintable_clearadvance")
            AttachTable = Session("attatch_clearadvance")
            CommentTable = Session("comment_clearadvance")
            Dim eventTarget As String
            Dim eventArgument As String

            If Me.Request("__EVENTTARGET") Is Nothing Then
                eventTarget = String.Empty
            Else
                eventTarget = Me.Request("__EVENTTARGET")
            End If
            If Me.Request("__EVENTARGUMENT") Is Nothing Then
                eventArgument = String.Empty
            Else
                eventArgument = Me.Request("__EVENTARGUMENT")
            End If
            If eventTarget = "setFromDetail" Then
                Dim row As String = eventArgument
                setdetail_by_row(row)
                ' Call your VB method here...
            End If
        End If
        SetMenu()
    End Sub

    Private Sub sethead(dt As DataTable)
        With dt

            codeRef.Text = .Rows(0).Item("coderef").ToString
            amount.Text = String.Format("{0:n4}", .Rows(0).Item("amount"))
        End With

    End Sub
    Private Sub setdetail_by_row(row As Integer)
        Dim dr() As System.Data.DataRow
        dr = detailtable.Select("row='" & row & "'")
        If dr.Length > 0 Then
            cboAccountCode.SelectedIndex = cboAccountCode.Items.IndexOf(cboAccountCode.Items.FindByValue(dr(0)("accountcodeid").ToString()))
            cboDep.SelectedIndex = cboDep.Items.IndexOf(cboDep.Items.FindByValue(dr(0)("depid").ToString()))
            cboBU.SelectedIndex = cboBU.Items.IndexOf(cboBU.Items.FindByValue(dr(0)("buid").ToString()))
            cboPP.SelectedIndex = cboPP.Items.IndexOf(cboPP.Items.FindByValue(dr(0)("ppid").ToString()))

            txtPrice.Text = dr(0)("cost").ToString()
            txtDetail.Text = dr(0)("detail").ToString()
        End If

    End Sub
    Private Sub setmain()

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
    Private Sub SetMenu()
        Select Case Session("status_clearadvance")
            Case = "new"
                btnFromAddDetail.Visible = True

                btnExport.Visible = True
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "read"
                btnFromAddDetail.Visible = False

                btnExport.Visible = True
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "write"
                btnFromAddDetail.Visible = False

                btnExport.Visible = False
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "edit"
                btnFromAddDetail.Visible = True

                btnExport.Visible = True
                btnPrint.Visible = True

                card_comment.Visible = True
                card_attatch.Visible = True
        End Select


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

        Return dt
    End Function
    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("row", GetType(Integer))
        dt.Columns.Add("advancedetailid", GetType(Double))
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
    Private Sub btnAddDetail_Click(sender As Object, e As EventArgs) Handles btnAddDetail.Click
        Try
            AddDetails()
            cleardetail()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('AddDetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub AddDetails()

        Dim cost As Double
        Try
            cost = Double.Parse(txtPrice.Text)
        Catch ex As Exception
            cost = 0
        End Try

        Dim row As DataRow
        row = detailtable.NewRow()
        row("row") = detailtable.Rows.Count + 1
        row("advancedetailid") = 0
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

    Private Sub cleardetail()
        cboAccountCode.SelectedIndex = -1
        cboDep.SelectedIndex = -1
        cboBU.SelectedIndex = -1
        cboPP.SelectedIndex = -1

        txtPrice.Text = "0"
        txtDetail.Text = ""
    End Sub
    <System.Web.Services.WebMethod>
    Public Shared Function addAttach(ByVal userid As Integer, ByVal url As String, ByVal description As String, ByVal paymentcode As String)

        Try
            'AttachTable.Rows.Add(9, "test", url, description)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function editDetail(ByVal rows As Integer)

        Try
            'AttachTable.Rows.Add(9, "test", url, description)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In detailtable.Select("row='" & rows & "'")
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In detailtable.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
            Next
            Return serializer.Serialize(row)
        Catch ex As Exception
            Return "fail"
        End Try

    End Function

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
End Class