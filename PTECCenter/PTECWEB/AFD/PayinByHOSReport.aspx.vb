Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PayinByHOSReport
    Inherits System.Web.UI.Page
    Dim mytable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

        End If
    End Sub



    'Private Sub exporttopdf(rptname As String, comcode As String, monthly As String, taxtype As Integer, doctype As Integer)
    '    Dim rpt As New ReportDocument()
    '    Dim login As New GlobalClass
    '    Dim oStream As New MemoryStream


    '    rpt.Load(Server.MapPath(rptname))

    '    rpt.SetDatabaseLogon(login.dbuser, login.dbpass, login.dbserver, login.dbcatalog)
    '    'rpt.Refresh()

    '    For Each TAB As Table In rpt.Database.Tables
    '        Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
    '        logoninfo.ConnectionInfo.UserID = login.dbuser
    '        logoninfo.ConnectionInfo.Password = login.dbpass
    '        logoninfo.ConnectionInfo.ServerName = login.dbserver
    '        logoninfo.ConnectionInfo.DatabaseName = login.dbcatalog
    '        TAB.ApplyLogOnInfo(logoninfo)
    '    Next

    '    rpt.SetParameterValue("@comcode", comcode)
    '    rpt.SetParameterValue("@monthly", monthly)
    '    rpt.SetParameterValue("@taxtype", taxtype)
    '    rpt.SetParameterValue("@doctype", doctype)


    '    Dim formatType As ExportFormatType = ExportFormatType.NoFormat
    '    'Select Case rbFormat.SelectedItem.Value
    '    '    Case "Word"
    '    '        formatType = ExportFormatType.WordForWindows
    '    '        Exit Select
    '    '    Case "PDF"
    '    formatType = ExportFormatType.PortableDocFormat
    '    '        Exit Select
    '    '    Case "Excel"
    '    '        formatType = ExportFormatType.Excel
    '    '        Exit Select
    '    '    Case "CSV"
    '    '        formatType = ExportFormatType.CharacterSeparatedValues
    '    '        Exit Select
    '    'End Select
    '    rpt.ExportToHttpResponse(formatType, Response, True, "Crystal")

    '    Response.End()
    'End Sub


    Private Sub WHTax353_4_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not (Session("hospayin") Is Nothing) Then
            'Session("rpt353") = rpt
            Me.CrystalReportViewer1.ReportSource = Session("hospayin")
            Me.CrystalReportViewer1.DataBind()
        End If

    End Sub

    'Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
    '    Dim pnd As New pnd353
    '    Dim taxtype As Integer
    '    Dim doctype As Integer
    '    Dim docno As String
    '    Dim comcode As String
    '    Dim loginname As String
    '    loginname = Session("username")
    '    docno = Session("monthly")
    '    comcode = Session("comcode")
    '    If Session("taxtype") = "3" Then
    '        taxtype = 1
    '    Else
    '        taxtype = 2
    '    End If

    '    If Session("doctype") = 1 Then
    '        doctype = 1
    '    Else
    '        doctype = 2
    '    End If

    '    mytable = pnd.GetDataForExport(Session("comcode"), Session("monthly"), taxtype, doctype)
    '    pnd.UpdateStatus(docno, comcode, docno, taxtype, doctype, loginname, 3)

    '    'Build the Text file data.
    '    Dim txt As String = pnd.export_to_text(mytable, taxtype)

    '    Dim fl As String
    '    fl = Session("comcode") & "_" & Session("taxtype") & "_" & Session("monthly") & "_" & Now.ToString("yyyyMMdd_HHmmss") & ".xls"

    '    'Download the Text file.
    '    Response.Clear()

    '    Response.Buffer = True
    '    '"attachment;filename=GridViewExport.txt"
    '    HttpContext.Current.Response.Clear()
    '    HttpContext.Current.Response.ClearContent()
    '    HttpContext.Current.Response.ClearHeaders()
    '    HttpContext.Current.Response.Buffer = True
    '    HttpContext.Current.Response.ContentType = "application/ms-excel"
    '    HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">")
    '    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls")

    '    HttpContext.Current.Response.Charset = "utf-8"
    '    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8")
    '    'sets font
    '    HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>")
    '    HttpContext.Current.Response.Write("<BR><BR><BR>")
    '    'sets the table border, cell spacing, border color, font of the text, background, foreground, font height
    '    HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " + "borderColor='#000000' cellSpacing='0' cellPadding='0' " + "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>")
    '    'am getting my grid's column headers
    '    'int columnscount = GridView_Result.Columns.Count;

    '    For j As Integer = 0 To mytable.Columns.Count - 1
    '        'write in new column
    '        HttpContext.Current.Response.Write("<Td>")
    '        'Get column headers  and make it as bold in excel columns
    '        HttpContext.Current.Response.Write("<B>")
    '        HttpContext.Current.Response.Write(mytable.Columns(j).ColumnName.ToString())
    '        HttpContext.Current.Response.Write("</B>")
    '        HttpContext.Current.Response.Write("</Td>")
    '    Next
    '    HttpContext.Current.Response.Write("</TR>")
    '    For Each row As DataRow In mytable.Rows
    '        'write in new row
    '        HttpContext.Current.Response.Write("<TR>")
    '        For i As Integer = 0 To mytable.Columns.Count - 1
    '            HttpContext.Current.Response.Write("<Td>")
    '            HttpContext.Current.Response.Write(row(i).ToString())
    '            HttpContext.Current.Response.Write("</Td>")
    '        Next

    '        HttpContext.Current.Response.Write("</TR>")
    '    Next

    '    HttpContext.Current.Response.Write("</Table>")
    '    HttpContext.Current.Response.Write("</font>")
    '    HttpContext.Current.Response.Flush()
    '    HttpContext.Current.Response.[End]()
    'End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim rpt As New ReportDocument()
        Dim login As New GlobalClass
        Dim rptname = "~\afd\rpt\HOS_Payin.rpt"
        'Dim begindate, enddate As String
        'Dim monthly As String '= txtmonthly.Text
        Dim type As Integer
        Try
            type = Integer.Parse(txttype.Text)
        Catch ex As Exception
            type = 1000
        End Try
        ' monthly = Strings.Right(txtmonthly.Text, 4) & Strings.Left(txtmonthly.Text, 2)
        rpt.Load(Server.MapPath(rptname))

        rpt.SetDatabaseLogon(login.dbuser, login.dbpass, login.dbserver, login.dbcatalog)
        'rpt.Refresh()

        For Each TAB As Table In rpt.Database.Tables
            Dim logoninfo As TableLogOnInfo = TAB.LogOnInfo
            logoninfo.ConnectionInfo.UserID = login.hquser
            logoninfo.ConnectionInfo.Password = login.hqpass
            logoninfo.ConnectionInfo.ServerName = login.hqserver
            logoninfo.ConnectionInfo.DatabaseName = login.hqcatalog
            TAB.ApplyLogOnInfo(logoninfo)
        Next

        rpt.SetParameterValue("@begindate", txtbegindate.Text)
        rpt.SetParameterValue("@enddate", txtenddate.Text)
        rpt.SetParameterValue("@type", type)
        rpt.SetParameterValue("@monthly", "")
        rpt.SetParameterValue("@cus_code", txtcuscode.Text)

        Session("hospayin") = rpt
        Me.CrystalReportViewer1.ReportSource = Session("hospayin")
        Me.CrystalReportViewer1.DataBind()
    End Sub
End Class