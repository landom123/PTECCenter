Imports System.Net
Imports System.IO
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Partial Class newgsm_billingnote01_detail
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'http://localhost:62517/
        Dim idh As Double
        idh = Request.QueryString("idh")
        'idh = 31
        SetReportParameters(idh)


    End Sub
    Private Sub SetReportParameters(billingid As Double)
        'Set Processing Mode
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ' Set report server and report path
        ReportViewer1.ServerReport.ReportServerUrl =
           New Uri("http://ptecdba:8081/ReportServer")
        ReportViewer1.ServerReport.ReportPath =
           "/GSM/Billingnote01_detail"
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim pInfo As ReportParameterInfoCollection
        'pInfo = ReportViewer1.ServerReport.GetParameters()
        'if you have report parameters - add them here
        paramList.Add(New ReportParameter("idh", billingid, True))
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
        Response.AddHeader("content-disposition", "attachment; filename=BillingNote_detail." + extension)
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