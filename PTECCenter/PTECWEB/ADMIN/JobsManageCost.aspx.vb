Public Class JobsManageCost
    Inherits System.Web.UI.Page
    Dim jobno As String
    Dim jobdetailid As Integer
    Public menutable As DataTable
    Public maintable As DataTable
    Public total As String
    Public costtable As DataTable = CreateCostDetail()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usercode, username, dep As String
        usercode = Session("usercode")
        username = Session("username")
        dep = Session("dep")

        Dim objjob As New jobs


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        If Session("menulist") Is Nothing Then
            If Not String.IsNullOrEmpty(usercode) Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            End If
        Else
            menutable = Session("menulist")
        End If

        txtBeginWarr.Attributes.Add("readonly", "readonly")
        txtEndWarr.Attributes.Add("readonly", "readonly")
        txtInvDate.Attributes.Add("readonly", "readonly")
        txtCloseDate.Attributes.Add("readonly", "readonly")
        'txtInvDateNew.Attributes.Add("readonly", "readonly")

        If Not IsPostBack() Then

            objjob.SetCboCloseType(cboCloseType)
            objjob.SetCboCloseCategory(cboCloseCategory)
            objjob.SetCboJobCenterDtlListByJobCenterID_GroupByJobCenterid(cboCost, "")
            Clear()
            If Not Request.QueryString("jobno") Is Nothing Then
                objjob.SetCboJobDtlList(cboJobdtlId, Request.QueryString("jobno"))
                txtJobnofind.Text = Request.QueryString("jobno")
                If Not Request.QueryString("jobdetailid") Is Nothing Then
                    cboJobdtlId.SelectedIndex = cboJobdtlId.Items.IndexOf(cboJobdtlId.Items.FindByValue(Request.QueryString("jobdetailid")))

                    jobno = Request.QueryString("jobno")
                    jobdetailid = Request.QueryString("jobdetailid")
                    FindJob(jobno, jobdetailid)
                End If
            Else
                Session("managecost") = "new"
            End If
        End If
        'SetMenu()

    End Sub
    Private Function CreateCostDetail() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcostid", GetType(Integer))
        dt.Columns.Add("jobdetailid", GetType(String))
        dt.Columns.Add("jobscenterdtlid", GetType(Integer))
        dt.Columns.Add("jobscenterdtlname", GetType(String))
        dt.Columns.Add("jobcostamount", GetType(String))
        dt.Columns.Add("jobcostunit", GetType(String))

        Return dt

    End Function
    Private Sub FindJob(jobno As String, jobdetailid As Double)
        Dim job As New jobs
        Dim mydataset As DataSet
        ' Dim jobtable As DataTable
        Session("jobno") = jobno
        'itemtable
        Try
            mydataset = job.FindClose(jobno, jobdetailid, Session("usercode"))
            maintable = mydataset.Tables(0)
            costtable = mydataset.Tables(2)
            total = String.Format("{0:n}", mydataset.Tables(3).Rows(0).Item("total"))

            'chkPAYMENT = mydataset.Tables(4).Rows(0).Item("chkPayment")

            showjobdata(mydataset)
            txtJobno.Text = jobno

        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('fail')"
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
            txtJobCenter.Text = .Item("jobcentername")
            txtSupplier.Text = .Item("supplier")
            txtDetail.Text = .Item("details")
        End With
        If mydataset.Tables(1).Rows.Count > 0 Then

            With mydataset.Tables(1).Rows(0)
                cboCloseType.SelectedIndex = cboCloseType.Items.IndexOf(cboCloseType.Items.FindByValue(.Item("jobclosetypeid")))
                txtBeginWarr.Text = .Item("beginwarr")
                txtEndWarr.Text = .Item("endwarr")
                cboCloseCategory.SelectedIndex = cboCloseCategory.Items.IndexOf(cboCloseCategory.Items.FindByValue(.Item("jobclosecategoryid")))
                'txtLaborAmt.Text = .Item("laboramt")
                'txtPartAmt.Text = .Item("partamt")
                'txtTravelAmt.Text = .Item("travelamt")
                txtInvoiceNo.Text = .Item("invoiceno")
                txtInvDate.Text = .Item("invoicedate")
                txtCloseDate.Text = .Item("closedate")
                txtRemark.Text = .Item("details")

            End With
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Response.Redirect("../ADMIN/JobsManageCost.aspx")
    End Sub

    Private Sub cboJobdtlId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobdtlId.SelectedIndexChanged
        If Not Request.QueryString("jobno") Is Nothing Then
            jobno = Request.QueryString("jobno")
            jobdetailid = cboJobdtlId.SelectedItem.Value
            Response.Redirect("JobsManageCost.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objjob As New jobs
        Dim jobscenterdtlid As Integer
        Dim price As Double
        Dim unit As Integer

        jobscenterdtlid = cboCost.SelectedItem.Value
        price = (txtCostPrice.Text)
        unit = txtCostUnit.Text
        If Not jobscenterdtlid = 0 Then
            Try

                jobno = Request.QueryString("jobno")
                jobdetailid = cboJobdtlId.SelectedItem.Value
                objjob.Cost_Save(jobscenterdtlid, jobdetailid, price, unit, Session("usercode"))
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
    Private Sub Clear()
        'followuptable.Rows.Clear()
        cboCost.ClearSelection()
        txtCostPrice.Text = 0
        txtCostUnit.Text = 1

    End Sub
    Private Sub btnUpdateinvoice_Click(sender As Object, e As EventArgs) Handles btnUpdateinvoice.Click
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

            jobno = Request.QueryString("jobno")
            jobdetailid = cboJobdtlId.SelectedItem.Value
            If objjob.JobCloseEdit(jobno, jobdetailid, jobclosetypeid, beginwarr, endwarr,
                                   part, labor, travel, invoiceno, invoicedate, detail, closedate, Session("usercode"), jobclosecategoryid) Then

                'Response.Redirect("jobs_Close.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)

            End If

        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try

        Response.Redirect("JobsManageCost.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)

endprocess:
    End Sub
End Class