Imports System.Web.Script.Serialization

Public Class JobsClose
    Inherits System.Web.UI.Page
    Dim objStatus As String
    Dim jobno As String
    Dim jobdetailid As Integer
    Dim jobcenterid As Integer
    Dim followup_status As String = String.Empty
    Public chkPAYMENT As Boolean = False
    Public total As String
    Public menutable As DataTable
    Public maintable As DataTable
    Public costtable As DataTable = CreateCostDetail()
    Dim usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim objjob As New jobs

        txtBeginWarr.Attributes.Add("readonly", "readonly")
        txtEndWarr.Attributes.Add("readonly", "readonly")
        txtInvDate.Attributes.Add("readonly", "readonly")
        txtCloseDate.Attributes.Add("readonly", "readonly")


        username = Session("username")
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        objStatus = "edit"
        Session("jobno") = Request.QueryString("jobno")
        Session("jobdetailid") = Request.QueryString("jobdetailid")
        jobno = Session("jobno")
        jobdetailid = Session("jobdetailid")
        txtJobno.Text = jobno

        If Not IsPostBack Then
            Clear()
            Try
                objjob.SetCboCloseType(cboCloseType)
                objjob.SetCboCloseCategory(cboCloseCategory)
                FindJob(jobno, jobdetailid)
                objjob.SetCboJobCenterDtlListByJobCenterID_GroupByJobCenterid(cboCost, jobcenterid)

                If Not followup_status = "ปิดงาน" Then
                    txtBeginWarr.Text = Now.ToString
                    txtEndWarr.Text = Now.ToString
                    txtInvDate.Text = Now.ToString
                    txtCloseDate.Text = Now.ToString
                End If
            Catch ex As Exception
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('fail')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        Else
            'followuptable = Session("followuptable")
            FindJob(jobno, jobdetailid)
        End If


        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Sub Clear()
        'followuptable.Rows.Clear()
        cboCost.ClearSelection()
        txtCostPrice.Text = 0
        txtCostUnit.Text = 1

    End Sub

    Private Sub FindJob(jobno As String, jobdetailid As Double)
        Dim job As New jobs
        Dim mydataset As DataSet
        ' Dim jobtable As DataTable
        Session("jobno") = jobno
        'itemtable
        Try
            mydataset = job.FindClose(jobno, jobdetailid, usercode)
            maintable = mydataset.Tables(0)
            costtable = mydataset.Tables(2)
            total = String.Format("{0:n}", mydataset.Tables(3).Rows(0).Item("total"))

            'chkPAYMENT = mydataset.Tables(4).Rows(0).Item("chkPayment")
            showjobdata(mydataset)

            followup_status = mydataset.Tables(0).Rows(0).Item("followup_status")
            jobcenterid = mydataset.Tables(0).Rows(0).Item("jobcenterid")


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

                cboCloseType.Attributes.Add("disabled", "True")
                cboCloseCategory.Attributes.Add("disabled", "True")
                txtBeginWarr.ReadOnly = True
                txtEndWarr.ReadOnly = True
                txtInvoiceNo.ReadOnly = True
                txtInvDate.ReadOnly = True
                txtCloseDate.ReadOnly = True
                txtRemark.ReadOnly = True
            End With
        End If
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("jobs_Followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
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
            Response.Redirect("jobs_Close.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)

        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try


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
                objjob.Cost_Save(jobscenterdtlid, jobdetailid, price, unit, usercode)
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
        dt.Columns.Add("jobdetailid", GetType(String))
        dt.Columns.Add("jobscenterdtlid", GetType(Integer))
        dt.Columns.Add("jobscenterdtlname", GetType(String))
        dt.Columns.Add("jobcostamount", GetType(String))
        dt.Columns.Add("jobcostunit", GetType(String))

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


End Class