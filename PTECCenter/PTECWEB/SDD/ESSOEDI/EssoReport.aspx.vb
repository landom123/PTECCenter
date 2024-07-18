
Imports System.IO
Imports ExcelDataReader
Imports System.Xml
Imports System.Data
Imports System.Web.Configuration
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

'Imports Microsoft.Office.Interop
'Imports ExcelDataReader

Public Class EssoReport

    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode As String

    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    'Public testtable As DataTable = createtest()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If
        'Session("usercode") = "PAB"
        'Session("username") = "Pison Anekboonyapirom"
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "")
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########

        If IsPostBack() Then

        End If
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim rpt As New ReportDocument()
        Dim login As New GlobalClass
        Dim rptname = "~\sdd\essoedi\edi_po_status.rpt"
        'Dim begindate, enddate As String
        'Dim monthly As String '= txtmonthly.Text

        Session.Remove("edistatus")

        Dim begindate As String = Strings.Right(txtBeginDate.Text, 4) & Strings.Mid(txtBeginDate.Text, 4, 2) & Strings.Left(txtBeginDate.Text, 2)
        Dim enddate As String = Strings.Right(txtEndDate.Text, 4) & Strings.Mid(txtEndDate.Text, 4, 2) & Strings.Left(txtEndDate.Text, 2)
        ' monthly = Strings.Right(txtmonthly.Text, 4) & Strings.Left(txtmonthly.Text, 2)
        rpt.Load(Server.MapPath(rptname))

        rpt.SetDatabaseLogon(login.dbuser, login.dbpass, login.dbserver, "ptec_edi")
        'rpt.Refresh()

        For Each TAB As Table In rpt.Database.Tables
            Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
            logoninfo.ConnectionInfo.UserID = login.dbuser
            logoninfo.ConnectionInfo.Password = login.dbpass
            logoninfo.ConnectionInfo.ServerName = login.dbserver
            logoninfo.ConnectionInfo.DatabaseName = "ptec_edi"
            TAB.ApplyLogOnInfo(logoninfo)
        Next

        rpt.SetParameterValue("@begindate", begindate)
        rpt.SetParameterValue("@enddate", enddate)
        'rpt.SetParameterValue("@type", type)
        'rpt.SetParameterValue("@monthly", "")
        'rpt.SetParameterValue("@cus_code", txtcuscode.Text)

        Session("edistatus") = rpt
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.DataBind()
    End Sub

    Private Sub EssoReport_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not (Session("edistatus") Is Nothing) Then
            'Session("rpt353") = rpt
            Me.CrystalReportViewer1.ReportSource = Session("edistatus")
            Me.CrystalReportViewer1.DataBind()
        End If
    End Sub
End Class