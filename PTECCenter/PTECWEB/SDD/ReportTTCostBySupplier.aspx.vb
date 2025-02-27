Imports System.Net
Imports System.IO
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Partial Class ReportTTCostBySupplier
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetReportParameters(Request.QueryString("jobcode").ToString,
                            Request.QueryString("supplierid").ToString)

    End Sub
    Private Sub SetReportParameters(jobcode As String, supplierid As String)
        'Set Processing Mode
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ' Set report server and report path
        ReportViewer1.ServerReport.ReportServerUrl =
           New Uri("http://10.15.100.227:8081/ReportServer")
        ReportViewer1.ServerReport.ReportPath =
           "/OPS/OPSforSupplier"
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim pInfo As ReportParameterInfoCollection
        'pInfo = ReportViewer1.ServerReport.GetParameters()
        'if you have report parameters - add them here
        paramList.Add(New ReportParameter("jobcode", jobcode, True))
        paramList.Add(New ReportParameter("supplierid", supplierid, True))
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
        Response.AddHeader("content-disposition", "attachment; filename=OPSforSupplier." + extension)
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