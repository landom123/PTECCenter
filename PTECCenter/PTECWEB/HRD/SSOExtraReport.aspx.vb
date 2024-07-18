
Imports System.IO
Imports ExcelDataReader
Imports System.Xml
Imports System.Data
Imports System.Web.Configuration
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

'Imports Microsoft.Office.Interop
'Imports ExcelDataReader

Public Class SSOExtraReport

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
        Dim rptname = "~\HRD\Reports\SSOExtra.rpt"

        Dim monthly As String = txtMonthly.Text
        Dim branch As String = txtBranch.Text
        rpt.Load(Server.MapPath(rptname))

        rpt.SetDatabaseLogon(login.dbuser, login.dbpass, login.dbserver, login.dbcatalog)
        'rpt.Refresh()

        For Each TAB As Table In rpt.Database.Tables
            Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
            logoninfo.ConnectionInfo.UserID = login.dbuser
            logoninfo.ConnectionInfo.Password = login.dbpass
            logoninfo.ConnectionInfo.ServerName = login.dbserver
            logoninfo.ConnectionInfo.DatabaseName = login.dbcatalog
            TAB.ApplyLogOnInfo(logoninfo)
        Next

        rpt.SetParameterValue("@monthly", monthly)
        rpt.SetParameterValue("@branch", Branch)
        'rpt.SetParameterValue("@type", type)
        'rpt.SetParameterValue("@monthly", "")
        'rpt.SetParameterValue("@cus_code", txtcuscode.Text)

        Session("ssoextra") = rpt
        CrystalReportViewer1.ReportSource = Session("ssoextra")
        CrystalReportViewer1.DataBind()
    End Sub

    Private Sub EssoReport_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not (Session("ssoextra") Is Nothing) Then
            'Session("rpt353") = rpt
            Me.CrystalReportViewer1.ReportSource = Session("ssoextra")
            Me.CrystalReportViewer1.DataBind()
        End If
    End Sub
End Class