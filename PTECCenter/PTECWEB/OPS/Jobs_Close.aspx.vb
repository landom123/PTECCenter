Imports System.Web.Script.Serialization

Public Class JobsClose
    Inherits System.Web.UI.Page
    Dim objStatus As String
    Dim jobno As String
    Dim jobdetailid As Integer
    Dim jobcenterid As Integer
    Dim followup_status As String = String.Empty
    Public chkPAYMENT As Boolean = False
    Public vat As String
    Public cost As String
    Public total As String
    Public menutable As DataTable
    Public maintable As DataTable
    Public costtable As DataTable = CreateCostDetail()
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Dim usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim attatch As New Attatch
        Dim objjob As New jobs
        Dim objNonpo As New NonPO

        txtBeginWarr.Attributes.Add("readonly", "readonly")
        txtEndWarr.Attributes.Add("readonly", "readonly")
        txtInvDate.Attributes.Add("readonly", "readonly")
        txtCloseDate.Attributes.Add("readonly", "readonly")


        username = Session("username")
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

        objStatus = "edit"
        ViewState("jobno") = Request.QueryString("jobno")
        ViewState("jobdetailid") = Request.QueryString("jobdetailid")
        jobno = ViewState("jobno")
        jobdetailid = ViewState("jobdetailid")
        txtJobno.Text = jobno

        If Not IsPostBack Then
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()
            Clear()
            Try

                objNonpo.SetCboBu(cboBU)
                objNonpo.SetCboPj(cboPJ)
                objNonpo.SetCboPurpose(cboPP, "active")
                objjob.SetCboCloseType(cboCloseType)
                objjob.SetCboCloseCategory(cboCloseCategory)
                attatch.SetCboMyfile(cboMyfile, Session("userid"))
                FindJob(jobno, jobdetailid)
                objjob.SetCboJobCenterDtlListByJobCenterID_GroupByJobCenterid(cboCost, jobcenterid)

                'If Not followup_status = "ปิดงาน" Then
                '    txtBeginWarr.Text = Now.ToString
                '    txtEndWarr.Text = Now.ToString
                '    txtInvDate.Text = Now.ToString
                '    txtCloseDate.Text = Now.ToString
                'End If

            Catch ex As Exception
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('fail')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        Else
            'followuptable = ViewState("followuptable")
            'FindJob(jobno, jobdetailid)

            maintable = ViewState("maintable")
            costtable = ViewState("costtable")
            AttachTable = ViewState("attach_jobclose")
            CommentTable = ViewState("comment_jobclose")
        End If
        setBtn()

        'txtCreateBy.Text = ViewState("jobtypeid")
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
    Private Sub setBtn()

        If maintable IsNot Nothing Then
            If maintable.Rows.Count > 0 Then
                If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then
                    FromAddDetail.Visible = True

                    btnClose.Visible = False
                    btnAddAttatch.Visible = True

                    lockcost.Visible = False

                    txtBeginWarr.ReadOnly = True
                    txtEndWarr.ReadOnly = True

                    'txtCloseDate.ReadOnly = True
                    txtRemark.ReadOnly = True
                Else '= ปิดงาน

                    If maintable.Rows(0).Item("lockcost") = False Then
                        FromAddDetail.Visible = True
                        btnClose.Visible = True

                        lockcost.Visible = False

                        txtBeginWarr.ReadOnly = False
                        txtEndWarr.ReadOnly = False

                        'txtCloseDate.ReadOnly = False
                        txtRemark.ReadOnly = False
                    Else
                        FromAddDetail.Visible = False
                        btnClose.Visible = False

                        lockcost.Visible = True
                        lockcost.InnerHtml = "ล็อกค่าใช้จ่ายเรียบร้อยแล้ว"


                        txtBeginWarr.ReadOnly = True
                        txtEndWarr.ReadOnly = True

                        'txtCloseDate.ReadOnly = True
                        txtRemark.ReadOnly = True

                        Dim scriptKey As String = "UniqueKeyForThisScript"
                        Dim javaScript As String = "disbtndelete();"
                        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    End If

                    btnAddAttatch.Visible = False

                End If

            End If
        End If

    End Sub
    Private Sub Clear()
        'followuptable.Rows.Clear()
        cboCost.ClearSelection()
        txtPrice.Text = 0
        txtCostUnit.Text = 1
        txtVat.Text = 0
        txtTax.Text = 0



    End Sub

    Private Sub FindJob(jobno As String, jobdetailid As Double)
        Dim job As New jobs
        Dim mydataset As DataSet
        ' Dim jobtable As DataTable
        ViewState("jobno") = jobno
        'itemtable
        Try
            mydataset = job.FindClose(jobno, jobdetailid, usercode)
            maintable = mydataset.Tables(0)
            costtable = mydataset.Tables(2)
            vat = String.Format("{0:n}", mydataset.Tables(3).Rows(0).Item("vat"))
            cost = String.Format("{0:n}", mydataset.Tables(3).Rows(0).Item("cost"))
            total = String.Format("{0:n}", mydataset.Tables(3).Rows(0).Item("total"))
            AttachTable = mydataset.Tables(4)
            CommentTable = mydataset.Tables(5)

            'chkPAYMENT = mydataset.Tables(4).Rows(0).Item("chkPayment")
            showjobdata(mydataset)

            followup_status = mydataset.Tables(0).Rows(0).Item("followup_status")
            jobcenterid = mydataset.Tables(0).Rows(0).Item("jobcenterid")


            ViewState("maintable") = maintable
            ViewState("costtable") = costtable
            ViewState("attach_jobclose") = AttachTable
            ViewState("comment_jobclose") = CommentTable

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub showjobdata(mydataset As DataSet)
        With mydataset.Tables(0).Rows(0)
            txtDocDate.Text = .Item("jobdate")
            txtOwner.Text = .Item("jobowner")
            txtBranch.Text = .Item("branch")
            txtDepartment.Text = .Item("department")
            txtSection.Text = .Item("section")
            txtJobType.Text = .Item("jobtype")
            txtAssetCode.Text = .Item("assetcode")
            txtAssetName.Text = .Item("assetname")
            txtQuantity.Text = .Item("quantity")
            txtUnit.Text = .Item("unit")
            txtCost.Text = .Item("cost")
            'txtJobCenter.Text = .Item("jobcentername")
            txtSupplier.Text = .Item("supplier")
            txtDetail.Text = .Item("details")

            txtCloseType.Text = .Item("JobCloseType_name")
            txtCloseCategory.Text = .Item("CategoryName")

            lbjobscode.Text = .Item("jobno")
            badgeStatus.InnerText = .Item("followup_status")
        End With
        If mydataset.Tables(1).Rows.Count > 0 Then

            With mydataset.Tables(1).Rows(0)
                'cboCloseType.SelectedIndex = cboCloseType.Items.IndexOf(cboCloseType.Items.FindByValue(.Item("jobclosetypeid")))
                txtBeginWarr.Text = .Item("beginwarr").ToString
                txtEndWarr.Text = .Item("endwarr").ToString
                txtCloseDate.Text = .Item("closedate").ToString
                txtRemark.Text = .Item("details")
                'cboCloseCategory.SelectedIndex = cboCloseCategory.Items.IndexOf(cboCloseCategory.Items.FindByValue(.Item("jobclosecategoryid")))
                'txtLaborAmt.Text = .Item("laboramt")
                'txtPartAmt.Text = .Item("partamt")
                'txtTravelAmt.Text = .Item("travelamt")
                'txtInvoiceNo.Text = .Item("invoiceno")
                'txtInvDate.Text = .Item("invoicedate").ToString

                'cboCloseType.Attributes.Add("disabled", "True")
                'cboCloseCategory.Attributes.Add("disabled", "True")
                txtBeginWarr.ReadOnly = True
                txtEndWarr.ReadOnly = True
                'txtInvoiceNo.ReadOnly = True
                'txtInvDate.ReadOnly = True
                txtCloseDate.ReadOnly = True
                txtRemark.ReadOnly = True
            End With
        End If
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("jobs_Followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
    End Sub

    Private Function ValidateClose() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If String.IsNullOrEmpty(txtRemark.Text.ToString()) Then
            result = False
            msg = "กรุณาใส่รายละเอียดสำหรับ Payment"
            GoTo endprocess
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If ValidateClose() Then
            Dim objjob As New jobs
            Dim jobclosetypeid As Integer
            Dim jobclosecategoryid As Integer
            Dim beginwarr, endwarr, invoicedate, closedate As String
            Dim labor, part, travel As Double
            Dim invoiceno, detail As String

            jobclosetypeid = cboCloseType.SelectedItem.Value
            jobclosecategoryid = cboCloseCategory.SelectedItem.Value
            beginwarr = (txtBeginWarr.Text)
            endwarr = (txtEndWarr.Text)
            labor = 0.0
            part = 0.0
            travel = 0.0
            invoiceno = txtInvoiceNo.Text
            invoicedate = (txtInvDate.Text)
            detail = txtRemark.Text
            closedate = (txtCloseDate.Text)

            Try
                If objjob.JobCloseSave(jobno, jobdetailid, jobclosetypeid, beginwarr, endwarr,
                                       part, labor, travel, invoiceno, invoicedate, detail, closedate, usercode, jobclosecategoryid) Then

                    'Response.Redirect("jobs_Close.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)

                End If
                flashData()
            Catch ex As Exception
                Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            'Response.Redirect("jobs_Close.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objjob As New jobs
        Dim costid As Integer = hiddenAdvancedetailid.Value
        Dim jobscenterdtlid As Integer = cboCost.SelectedItem.Value
        Dim price As Double
        Dim unit As Integer
        Dim vat As Integer
        Dim tax As Integer
        Dim invoice As String = txtInvoiceNo.Text
        Dim invoicedate As String = txtInvDate.Text
        Dim nobill As Boolean = chkNoBill.Checked
        Dim incompletebill As Boolean = chkIncompleteBill.Checked
        Dim buid As Integer = cboBU.SelectedItem.Value
        Dim ppid As Integer = cboPP.SelectedItem.Value
        Dim pjid As Integer = cboPJ.SelectedItem.Value

        Try
            price = (txtPrice.Text)
        Catch ex As Exception
            price = 0
        End Try
        Try
            vat = txtVat.Text
        Catch ex As Exception
            vat = 0
        End Try
        Try
            tax = txtTax.Text
        Catch ex As Exception
            tax = 0
        End Try
        Try
            unit = txtCostUnit.Text
        Catch ex As Exception
            unit = 1
        End Try

        If Not jobscenterdtlid = 0 Then
            Try
                objjob.Cost_Save(jobscenterdtlid, jobdetailid, price, unit, vat, tax, invoice, invoicedate, nobill, incompletebill, buid, ppid, pjid, usercode, costid)
                FindJob(jobno, jobdetailid)
                Clear()
            Catch ex As Exception
                Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        Else
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('กรุณาเลือกรายการค่าใช้จ่าย');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If


    End Sub

    Private Function CreateCostDetail() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcostid", GetType(Integer))
        dt.Columns.Add("bu", GetType(Integer))
        dt.Columns.Add("pp", GetType(Integer))
        dt.Columns.Add("pj", GetType(Integer))
        dt.Columns.Add("jobdetailid", GetType(String))
        dt.Columns.Add("jobscenterdtlid", GetType(Integer))
        dt.Columns.Add("jobscenterdtlname", GetType(String))
        dt.Columns.Add("jobcostunit", GetType(String))
        dt.Columns.Add("unitprice", GetType(String))
        dt.Columns.Add("amount", GetType(String))
        dt.Columns.Add("vat_per", GetType(String))
        dt.Columns.Add("tax_per", GetType(String))
        dt.Columns.Add("invoiceno", GetType(String))
        dt.Columns.Add("invoicedate", GetType(String))
        dt.Columns.Add("nobill", GetType(Boolean))
        dt.Columns.Add("incompletebill", GetType(Boolean))
        'dt.Columns.Add("bill", GetType(String))

        '<%= costtable.rows(i).item("jobscenterdtlid").tostring() %>'
        '<%= costtable.rows(i).item("bu").tostring() %>'
        '<%= costtable.rows(i).item("pp").tostring() %>'
        '<%= costtable.rows(i).item("pj").tostring() %>'
        '<%= costtable.rows(i).item("jobcostunit").tostring() %>'
        '<%= costtable.rows(i).item("unitprice").tostring() %>'
        '<%= costtable.rows(i).item("vat_per").tostring() %>'
        '<%= costtable.rows(i).item("tax_per").tostring() %>'
        '<%= costtable.rows(i).item("invoice").tostring() %>'
        '<%= costtable.rows(i).item("invoicedate").tostring() %>'
        '<%= costtable.rows(i).item("nobill").tostring() %>'
        '<%= costtable.rows(i).item("incompletebill").tostring() %>'



        Return dt

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function deleteCostById(ByVal jobcostid As String)

        Dim job As New jobs
        Try
            job.deleteCostById(jobcostid)
        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function
    Private Sub flashData()
        Clear()
        FindJob(jobno, jobdetailid)
        setBtn()
    End Sub
    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click

        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("jobdetailid"), txtComment.Text.Trim(), Session("userid"))
            txtComment.Text = ""
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub
End Class