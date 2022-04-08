


'Imports Microsoft.Office.Interop
'Imports ExcelDataReader

Public Class EssoInvCNDNReport

    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode As String

    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    'Public testtable As DataTable = createtest()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Session("usercode") = "PAB"
        Session("username") = "Pison Anekboonyapirom"
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        If IsPostBack() Then

        End If
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        'Dim rpt As New ReportDocument()
        Dim login As New GlobalClass
        Dim rptname = "~\sdd\essoedi\edi_po_status.rpt"
        'Dim begindate, enddate As String
        'Dim monthly As String '= txtmonthly.Text
        Dim begindate As String = Strings.Right(txtBeginDate.Text, 4) & Strings.Mid(txtBeginDate.Text, 4, 2) & Strings.Left(txtBeginDate.Text, 2)
        Dim enddate As String = Strings.Right(txtEndDate.Text, 4) & Strings.Mid(txtEndDate.Text, 4, 2) & Strings.Left(txtEndDate.Text, 2)
        ' monthly = Strings.Right(txtmonthly.Text, 4) & Strings.Left(txtmonthly.Text, 2)
        '''''rpt.Load(Server.MapPath(rptname))

        '''''rpt.SetDatabaseLogon(login.dbuser, login.dbpass, login.dbserver, login.dbcatalog)
        'rpt.Refresh()

        'For Each TAB As Table In rpt.Database.Tables
        '    Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
        '    logoninfo.ConnectionInfo.UserID = login.hquser
        '    logoninfo.ConnectionInfo.Password = login.hqpass
        '    logoninfo.ConnectionInfo.ServerName = login.hqserver
        '    logoninfo.ConnectionInfo.DatabaseName = login.hqcatalog
        '    TAB.ApplyLogOnInfo(logoninfo)
        'Next

        'rpt.SetParameterValue("@begindate", begindate)
        'rpt.SetParameterValue("@enddate", enddate)
        'rpt.SetParameterValue("@type", type)
        'rpt.SetParameterValue("@monthly", "")
        'rpt.SetParameterValue("@cus_code", txtcuscode.Text)

        'Session("edistatus") = rpt
        'CrystalReportViewer1.ReportSource = Session("edistatus")
        'CrystalReportViewer1.DataBind()
    End Sub

    Private Sub EssoReport_Init(sender As Object, e As EventArgs) Handles Me.Init
        'If Not (Session("edistatus") Is Nothing) Then
        '    'Session("rpt353") = rpt
        '    Me.CrystalReportViewer1.ReportSource = Session("edistatus")
        '    Me.CrystalReportViewer1.DataBind()
        'End If
    End Sub
End Class