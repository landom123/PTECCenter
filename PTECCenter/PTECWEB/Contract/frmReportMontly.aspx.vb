

Imports System.IO

Imports ClosedXML.Excel
Public Class frmReportMontly
    Inherits System.Web.UI.Page
    Public menutable, paymenttable As DataTable

    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim objTitleName As New TitleName

        usercode = Session("usercode")
        username = Session("username")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

        Else

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If


        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("monthly_payment.aspx")
    End Sub

    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click
        Dim mytable As DataTable
        Dim payment As New Contract
        Dim calcdate As String

        If IsDate(txtCalcDate.Text) = False Then
            Dim err As String = "กรุณาระบุวันที่"
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Exit Sub
        End If
        calcdate = DateTime.Parse(txtCalcDate.Text).Year & DateTime.Parse(txtCalcDate.Text).Month.ToString("00")
        Session("calcdate") = calcdate
        Try
            mytable = payment.MonthlyPayment2(calcdate)
            'gvPayment.DataSource = mytable
            paymenttable = mytable
            Session("paymenttable") = mytable

            'gvPayment.DataBind()

            Dim numrows As Integer
            Dim numcells As Integer
            Dim i As Integer
            Dim j As Integer
            Dim r As TableRow
            Dim c As TableCell
            ' Generate rows and cells
            numrows = mytable.Rows.Count
            numcells = mytable.Columns.Count
            For j = 0 To numrows - 1
                r = New TableRow()
                For i = 0 To numcells - 1
                    c = New TableCell()
                    'c.Controls.Add(New LiteralControl("row " & j & ", cell " & i))
                    If IsNumeric(mytable.Rows(j).Item(i).ToString) = False Then
                        c.Controls.Add(New LiteralControl(mytable.Rows(j).Item(i).ToString))
                    Else
                        c.Controls.Add(New LiteralControl(FormatNumber(mytable.Rows(j).Item(i).ToString, 2)))
                    End If

                    r.Cells.Add(c)
                Next i
                'MyTable.Rows.Add(r)
                tblData.Rows.Add(r)
            Next j


        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim calcdate As String = Session("calcdate")
        paymenttable = Session("paymenttable")
        ExportTableToExcel(paymenttable, "pay_", calcdate & DateTime.Now.ToString("yyyyMMddhhmmss"))
    End Sub

    Private Sub ExportTableToExcel(mytable As DataTable, branch As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mytable, "TTCost")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & branch & "_" & closedate & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub

End Class