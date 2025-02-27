Imports System.Net
Imports System.IO
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Partial Class Rpt_Customer_bal
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'http://localhost:62517/
        Dim asdate, branch As String

        asdate = Request.QueryString("asdate")
        branch = Request.QueryString("branch")
        'idh = 31
        'asdate = "20211111"
        'branch = "003"
        SetReportParameters(asdate, branch)


    End Sub
    Private Sub SetReportParameters(asdate As String, branch As String)
        'Set Processing Mode
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ' Set report server and report path
        ReportViewer1.ServerReport.ReportServerUrl =
           New Uri("http://10.15.100.227:8081/ReportServer")
        ReportViewer1.ServerReport.ReportPath =
           "/GSM/rpt_customer_bal"
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim pInfo As ReportParameterInfoCollection
        'pInfo = ReportViewer1.ServerReport.GetParameters()
        'if you have report parameters - add them here
        paramList.Add(New ReportParameter("asdate", asdate, True))
        paramList.Add(New ReportParameter("branch", branch, True))
        'paramList.Add(New ReportParameter("supplierid", supplierid, True))
        ReportViewer1.ServerReport.SetParameters(paramList)
        ' Process and render the report
        ReportViewer1.ServerReport.Refresh()
        'output as PDF
        Dim returnValue As Byte()
        Dim format As String = "PDF"
        Dim deviceinfo As String = ""
        Dim mimeType As String = ""
        Dim encoding As String = ""
        Dim extension As String = "pdf"
        Dim streams As String() = Nothing
        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        returnValue = ReportViewer1.ServerReport.Render(format, deviceinfo, mimeType, encoding, extension, streams, warnings)
        Response.Buffer = True
        Response.Clear()
        Response.ContentType = mimeType
        Response.AddHeader("content-disposition", "attachment; filename=ArBal" & "_" & branch & "_" & asdate & "." + extension)
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