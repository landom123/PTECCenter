Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class ReportViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Session("rpt") Is Nothing) Then
            'Session("rpt353") = rpt
            Me.CrystalReportViewer1.ReportSource = Session("rpt")
            Me.CrystalReportViewer1.DataBind()
        End If
    End Sub

    Private Sub ReportViewer_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not (Session("rpt") Is Nothing) Then
            'Session("rpt353") = rpt
            Me.CrystalReportViewer1.ReportSource = Session("rpt")
            Me.CrystalReportViewer1.DataBind()
        End If
    End Sub
End Class