Imports System.Net
Imports System.IO
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Partial Class Jobs_Report_JobReport
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetReportParameters(Request.QueryString("depid").ToString,
                            Request.QueryString("jobtypeid").ToString,
                            Request.QueryString("statusFollow").ToString,
                            Request.QueryString("branchgroupid").ToString,
                            Request.QueryString("branchid").ToString,
                            Request.QueryString("startdate").ToString,
                            Request.QueryString("enddate").ToString)

    End Sub
    Private Sub SetReportParameters(depid As String, jobtypeid As String, statusFollow As String, branchgroupid As String, branchid As String, startdate As String, enddate As String)
        'Set Processing Mode
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ' Set report server and report path
        ReportViewer1.ServerReport.ReportServerUrl =
           New Uri("http://ptecdba:8081/ReportServer")
        ReportViewer1.ServerReport.ReportPath =
           "/OPS/rpt_JobOPS"
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim pInfo As ReportParameterInfoCollection
        'pInfo = ReportViewer1.ServerReport.GetParameters()
        'if you have report parameters - add them here
        paramList.Add(New ReportParameter("depid", depid, True))
        paramList.Add(New ReportParameter("jobtypeid", jobtypeid, True))
        'paramList.Add(New ReportParameter("status", status, True))
        paramList.Add(New ReportParameter("statusFollow", statusFollow, True))
        paramList.Add(New ReportParameter("branchgroupid", branchgroupid, True))
        paramList.Add(New ReportParameter("branchid", branchid, True))
        paramList.Add(New ReportParameter("startdate", startdate, True))
        paramList.Add(New ReportParameter("enddate", enddate, True))
        ReportViewer1.ServerReport.SetParameters(paramList)
        ' Process and render the report
        ReportViewer1.ServerReport.Refresh()
        'output as PDF
        Dim returnValue As Byte()
        Dim format As String = "EXCEL"
        Dim deviceinfo As String = ""
        Dim mimeType As String = ""
        Dim encoding As String = ""
        Dim extension As String = "xlsx"
        Dim streams As String() = Nothing
        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        returnValue = ReportViewer1.ServerReport.Render(format, deviceinfo, mimeType, encoding, extension, streams, warnings)
        Response.Buffer = True
        Response.Clear()
        Response.ContentType = mimeType
        Response.AddHeader("content-disposition", "attachment; filename=OPS_Job." + extension)
        Response.BinaryWrite(returnValue)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object,
                            ByVal e As System.EventArgs) _
                            Handles Me.Init
        ReportViewer1.ServerReport.ReportServerCredentials =
            New MyReportServerCredentials()
    End Sub
End Class