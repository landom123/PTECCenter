Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Globalization

Public Class Jobs_Report_byRange
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    'Public followuptable As DataTable = CreateFollowup()
    Dim usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objjob As New jobs
        username = Session("username")
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


        If Not IsPostBack Then
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranch(cboBranch, "")
            objjob.SetCboJobStatusList(cboStatusFollow)
            objdep.SetCboDepartmentforjobtype(cboDep)
            SetCboJobTypeByDepID(cboJobType, cboDep.SelectedItem.Value)
        End If


        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Sub ViewReport(begindate As String, enddate As String, branch As Double, status As Integer,
                           jobowner As String, jobaction As String, jobtypeid As Integer)
        Dim rpt As New ReportDocument()
        Dim rptname As String = "\ops\reports\rpt_ops_report_byJobType.rpt"
        Try
            'Dim test As String = Server.MapPath(rptname)
            rpt.Load(Server.MapPath(rptname))

            rpt.SetDatabaseLogon(dbLogin, dbPassword, dbServer, dbOPS)
            'rpt.Refresh()

            For Each TAB As Table In rpt.Database.Tables
                Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
                logoninfo.ConnectionInfo.UserID = dbLogin
                logoninfo.ConnectionInfo.Password = dbPassword
                logoninfo.ConnectionInfo.ServerName = dbServer
                logoninfo.ConnectionInfo.DatabaseName = dbOPS
                TAB.ApplyLogOnInfo(logoninfo)
            Next

            'rpt.SetParameterValue("@jobcode", jobcode)
            rpt.SetParameterValue("@begindate", begindate)
            rpt.SetParameterValue("@enddate", enddate)
            rpt.SetParameterValue("@status", status)
            rpt.SetParameterValue("@jobowner", jobowner)
            rpt.SetParameterValue("@jobaction", jobaction)
            rpt.SetParameterValue("@jobtypeid", jobtypeid)
            rpt.SetParameterValue("@branch", branch)

            Session("rpt") = rpt
            Response.Write("<script>")
            Response.Write("window.open('../ReportViewer.aspx','_blank')")
            Response.Write("</script>")


            'Me.CrystalReportViewer1.ReportSource = Session("rpt")
            'Me.CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try



    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim s As String = "window.open('../OPS/Jobs_Report_JobReport.aspx?depid=" & cboDep.SelectedItem.Value &
            "&jobtypeid=" & cboJobType.SelectedItem.Value &
            "&statusFollow=" & cboStatusFollow.SelectedItem.Value &
            "&branchgroupid=" & cboBranchGroup.SelectedItem.Value &
            "&branchid=" & cboBranch.SelectedItem.Value &
            "&startdate=" & txtBeginDate.Text &
            "&enddate=" & txtEndDate.Text &
            " ', '_blank');"

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
    End Sub

    Private Sub cboDep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDep.SelectedIndexChanged
        cboJobType.SelectedIndex = -1
        SetCboJobTypeByDepID(cboJobType, cboDep.SelectedItem.Value)
    End Sub

    Private Sub cboBranchGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranchGroup.SelectedIndexChanged
        Dim objbranch As New Branch

        cboBranch.SelectedIndex = -1
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
    End Sub
End Class