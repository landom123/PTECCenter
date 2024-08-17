Imports System.Diagnostics.Contracts
Imports System.IO

Imports ClosedXML.Excel

Public Class monthly_payment
    Inherits System.Web.UI.Page
    Public menutable, paymenttable As DataTable
    Public usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New TitleName

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
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

        'txtCalcDate.Attributes.Add("readonly", "readonly")



        If IsPostBack() Then

        Else


        End If

    End Sub

    Private Sub FindData()
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
            mytable = payment.MonthlyPayment(calcdate)
            gvPayment.DataSource = mytable
            paymenttable = mytable
            Session("paymenttable") = mytable

            gvPayment.DataBind()

        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub ShowData(mytable As DataTable)

    End Sub


    Private Sub SetCboPaymentType(obj As Object)
        Dim payment As New Payment

        obj.DataSource = payment.PaymentType_List()
        obj.DataValueField = "paymenttypeid"
        obj.DataTextField = "paymenttype"
        obj.DataBind()
    End Sub

    Private Sub Clear()

    End Sub

    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click
        FindData()
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

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim calcdate As String = Session("calcdate")
        paymenttable = Session("paymenttable")
        ExportTableToExcel(paymenttable, "pay_", calcdate & DateTime.Now.ToString("yyyyMMddhhmmss"))
    End Sub

    'Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
    '    Response.Redirect("frmReportMontly.aspx")
    'End Sub
End Class