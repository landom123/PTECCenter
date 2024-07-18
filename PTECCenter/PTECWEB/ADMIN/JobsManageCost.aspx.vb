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

        Dim objsupplier As New Supplier
        Dim objjob As New jobs


        'txtDueDate.Attributes.Add("readonly", "readonly")

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

        txtBeginWarr.Attributes.Add("readonly", "readonly")
        txtEndWarr.Attributes.Add("readonly", "readonly")
        txtInvDate.Attributes.Add("readonly", "readonly")
        txtCloseDate.Attributes.Add("readonly", "readonly")
        'txtInvDateNew.Attributes.Add("readonly", "readonly")

        If Not IsPostBack() Then

            objsupplier.SetCboSupplier(cboSupplier)
            objjob.SetCboJobCenterList(cboJobCenter)
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
            Dim javaScript As String = "alertWarning('find fail')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub showjobdata(mydataset As DataSet)
        Dim objpolicy As New Policy

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


            objpolicy.setComboPolicyByJobTypeID(cboPolicy, .Item("jobtypeid"))
            cboPolicy.SelectedIndex = cboPolicy.Items.IndexOf(cboPolicy.Items.FindByValue(.Item("PolicyID").ToString))
            txtDueDate.Text = .Item("requestdate").ToString

            TextBox1.Text = .Item("cost")
            cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByText(.Item("supplier").ToString))
            cboJobCenter.SelectedIndex = cboJobCenter.Items.IndexOf(cboJobCenter.Items.FindByText(.Item("jobcentername").ToString))



        End With
        If mydataset.Tables(1).Rows.Count > 0 Then

            With mydataset.Tables(1).Rows(0)
                cboCloseType.SelectedIndex = cboCloseType.Items.IndexOf(cboCloseType.Items.FindByValue(.Item("jobclosetypeid")))
                txtBeginWarr.Text = .Item("beginwarr").ToString
                txtEndWarr.Text = .Item("endwarr").ToString
                cboCloseCategory.SelectedIndex = cboCloseCategory.Items.IndexOf(cboCloseCategory.Items.FindByValue(.Item("jobclosecategoryid")))
                'txtLaborAmt.Text = .Item("laboramt")
                'txtPartAmt.Text = .Item("partamt")
                'txtTravelAmt.Text = .Item("travelamt")
                txtInvoiceNo.Text = .Item("invoiceno")
                txtInvDate.Text = .Item("invoicedate").ToString
                txtCloseDate.Text = .Item("closedate").ToString
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

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'update cost and supplier

        Dim supplierid As Double = cboSupplier.SelectedItem.Value
        Dim jobCenter As Integer = cboJobCenter.SelectedItem.Value
        Dim cost As Double

        Try
            cost = Double.Parse(TextBox1.Text)
        Catch
            cost = 0
        End Try

        Dim objjob As New jobs

        Try

            jobno = Request.QueryString("jobno")
            jobdetailid = cboJobdtlId.SelectedItem.Value
            objjob.UpdateSupplierandCost(jobno, jobdetailid, jobCenter, supplierid, cost, Session("usercode"))
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

    Private Sub btnUpdatePolicy_Click(sender As Object, e As EventArgs) Handles btnUpdatePolicy.Click
        'update cost and supplier

        Dim objjob As New jobs
        Dim policyid As Double = cboPolicy.SelectedItem.Value
        Dim jobCenter As Integer = cboJobCenter.SelectedItem.Value

        Try

            jobno = Request.QueryString("jobno")
            jobdetailid = cboJobdtlId.SelectedItem.Value
            objjob.Updatepolicyid(jobno, jobdetailid, jobCenter, policyid, Session("usercode"))
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

    Private Sub btnUpdateDuedate_Click(sender As Object, e As EventArgs) Handles btnUpdateDuedate.Click
        'update cost and supplier

        Dim objjob As New jobs
        Dim duedate As String = txtDueDate.Text.ToString
        Dim jobCenter As Integer = cboJobCenter.SelectedItem.Value

        Try

            jobno = Request.QueryString("jobno")
            jobdetailid = cboJobdtlId.SelectedItem.Value
            objjob.Updateduedate(jobno, jobdetailid, jobCenter, duedate, Session("usercode"))
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

    Private Function setDueDate(policyid As String) As Boolean
        Dim objjobs As New jobs
        Dim mydataset As DataSet
        Try
            mydataset = objjobs.setDueDateByPolicyID(policyid)
            txtDueDate.Text = mydataset.Tables(0).Rows(0).Item("duedate")
            Return True
        Catch ex As Exception
            txtDueDate.Text = ""
            Return False
        End Try
    End Function
    'Private Sub cboPolicy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPolicy.SelectedIndexChanged
    '    setDueDate(cboPolicy.SelectedItem.Value)
    'End Sub
End Class