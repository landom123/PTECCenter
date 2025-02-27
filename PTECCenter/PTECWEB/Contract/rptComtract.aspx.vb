Imports System.Net
Imports System.IO
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms

Public Class rptComtract
    Inherits System.Web.UI.Page

    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SetReportParameters(Request.QueryString("rpt_JobOPS").ToString)

        'SetReportParameters("REQ-0823-0026")
        If IsPostBack = False Then
            SetReportParameters(Session("sDocnoReport"))
        End If

    End Sub

    Private Sub SetReportParameters(Docno As String)


        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New Uri("http://10.15.100.227:8081/ReportServer")
        Dim paramList As New Generic.List(Of ReportParameter)
        Select Case Session("ContractType")
            Case 1, 5
                ReportViewer1.ServerReport.ReportPath = "/Report Parts/rptContractRentJoin"
                paramList.Add(New ReportParameter("sDocNo", Docno, True))
            Case 2
                ReportViewer1.ServerReport.ReportPath = "/Report Parts/rptContractNonOil"
                paramList.Add(New ReportParameter("sDocNo", Docno, True))
            Case 3
                ReportViewer1.ServerReport.ReportPath = "/Report Parts/rptContractOther"
                paramList.Add(New ReportParameter("sDocNo", Docno, True))
            Case 4
                ReportViewer1.ServerReport.ReportPath = "/Report Parts/rptContractPowerBook"
                paramList.Add(New ReportParameter("sDocNo", Docno, True))
            Case 6
                ReportViewer1.ServerReport.ReportPath = "/Report Parts/rptNonOil"
                'paramList.Add(New ReportParameter("sDocNo", Docno, True))
        End Select

        ReportViewer1.ServerReport.SetParameters(paramList)

        ReportViewer1.ServerReport.Refresh()


    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials()
    End Sub

End Class