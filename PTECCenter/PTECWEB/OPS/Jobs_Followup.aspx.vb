Public Class JobsFollowup
    Inherits System.Web.UI.Page
    Dim objStatus As String
    Dim jobno As String
    Dim jobdetailid As Integer
    Public menutable As DataTable
    Public maintable As DataTable
    Public followuptable As DataTable = CreateFollowup()
    Dim usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim objjob As New jobs
        Dim objsupplier As New Supplier

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
            objsupplier.SetCboSupplier(cboSupplier)
            objjob.SetCboJobCenterList(cboJobCenter)

            FindJob(jobno, jobdetailid)
            If maintable.Rows(0).Item("owner") > 0 Then
                objjob.SetCboJobStatusList(cboStatus)
            Else
                Dim dt As New DataTable

                dt.Columns.Add("statusid", GetType(Integer))
                dt.Columns.Add("name", GetType(String))
                dt.Rows.Add(11, "confirm")
                cboStatus.DataSource = dt
                cboStatus.DataValueField = ("statusid")
                cboStatus.DataTextField = ("name")
                cboStatus.DataBind()
            End If
            setBtn()
        Else
            followuptable = Session("followuptable")
            maintable = Session("maintable")
        End If


        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Sub setBtn()
        If Not maintable.Rows(0).Item("supplierid") = 0 Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If

    End Sub

    Private Sub Clear()
        followuptable.Rows.Clear()
        lblCreateBy.Text = username
        lblCreateDate.Text = Now.ToString
    End Sub
    Private Function CreateFollowup() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("statusid", GetType(Integer))
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("userid", GetType(Double))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(DateTime))
        dt.Columns.Add("username", GetType(String))

        Return dt
    End Function

    Private Sub FindJob(jobno As String, jobdetailid As Double)
        Dim job As New jobs
        Dim mydataset As DataSet
        ' Dim jobtable As DataTable
        Session("jobno") = jobno

        'itemtable
        Try
            mydataset = job.FindFollowup(jobno, jobdetailid, usercode)
            maintable = mydataset.Tables(0)
            showjobdata(mydataset.Tables(0))
            followuptable = mydataset.Tables(1)

            Session("maintable") = maintable
            Session("followuptable") = followuptable

        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub
    Private Sub showjobdata(mytable As DataTable)
        With mytable.Rows(0)
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
            cboJobCenter.SelectedIndex = cboJobCenter.Items.IndexOf(cboJobCenter.Items.FindByValue(.Item("jobcenterid")))
            cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(.Item("supplierid")))
            'txtSupplier.Text = .Item("supplier")
            txtDetail.Text = .Item("details")
        End With
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        saveFollowup()
    End Sub
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        saveFollowup()
    End Sub
    Private Sub saveFollowup()
        Dim statusid As Integer = cboStatus.SelectedItem.Value
        Dim details As String = txtDetailFollow.Text

        Dim objjob As New jobs

        Try
            objjob.Followup_Save(jobno, jobdetailid, statusid, details, usercode)
            FindJob(jobno, jobdetailid)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("jobs.aspx?jobno=" & jobno)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Response.Redirect("jobs_Close.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'update cost and supplier

        Dim supplierid As Double = cboSupplier.SelectedItem.Value
        Dim jobCenter As Integer = cboJobCenter.SelectedItem.Value
        Dim cost As Double

        Try
            cost = Double.Parse(txtCost.Text)
        Catch
            cost = 0
        End Try

        Dim objjob As New jobs

        Try
            objjob.UpdateSupplierandCost(jobno, jobdetailid, jobCenter, supplierid, cost, usercode)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim s As String = "window.open('../OPS/Jobs_Report_JobForm.aspx?jobcode=" & Request.QueryString("jobno").ToString() &
    "&supplierid=" & cboSupplier.SelectedItem.Value &
    " ', '_blank');"

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
    End Sub

End Class