﻿Imports System.Net
Imports System.IO
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Partial Class nac_sale
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetReportParameters(Request.QueryString("nac_code").ToString)

    End Sub
    Private Sub SetReportParameters(naccode As String)
        'Set Processing Mode
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ' Set report server and report path
        ReportViewer1.ServerReport.ReportServerUrl =
           New Uri("http://10.15.100.227:8081/ReportServer")
        ReportViewer1.ServerReport.ReportPath =
           "/FA/NAC_SALE"
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim pInfo As ReportParameterInfoCollection
        'pInfo = ReportViewer1.ServerReport.GetParameters()
        'if you have report parameters - add them here
        paramList.Add(New ReportParameter("nac_code", naccode, True))
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
        Response.AddHeader("content-disposition", "attachment; filename=rpt_Approval." + extension)
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
'<Serializable()>
'Public NotInheritable Class MyReportServerCredentials
'    Implements IReportServerCredentials
'    Public userName As String = ConfigurationManager.AppSettings("rvUser")
'    Public password As String = ConfigurationManager.AppSettings("rvPassword")
'    Public domain As String = ConfigurationManager.AppSettings("rvDomain")
'    Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
'            Implements IReportServerCredentials.ImpersonationUser
'        Get
'            'Use the default windows user.  Credentials will be
'            'provided by the NetworkCredentials property.
'            Return Nothing
'        End Get
'    End Property
'    Public ReadOnly Property NetworkCredentials() As ICredentials _
'            Implements IReportServerCredentials.NetworkCredentials
'        Get
'            'Read the user information from the web.config file. 
'            'By reading the information on demand instead of storing
'            'it, the credentials will not be stored in session,
'            'reducing the vulnerable surface area to the web.config
'            'file, which can be secured with an ACL.
'            If (String.IsNullOrEmpty(userName)) Then
'                Throw New Exception("Missing user name from web.config file")
'            End If
'            If (String.IsNullOrEmpty(password)) Then
'                Throw New Exception("Missing password from web.config file")
'            End If
'            If (String.IsNullOrEmpty(domain)) Then
'                Throw New Exception("Missing domain from web.config file")
'            End If
'            Return New NetworkCredential(userName, password, domain)
'        End Get
'    End Property
'    Public Function GetFormsCredentials(ByRef authCookie As Cookie,
'                                        ByRef userName As String,
'                                        ByRef password As String,
'                                        ByRef authority As String) _
'                                        As Boolean _
'            Implements IReportServerCredentials.GetFormsCredentials
'        authCookie = Nothing
'        userName = Nothing
'        password = Nothing
'        authority = Nothing
'        'Not using form credentials
'        Return False
'    End Function
'End Class