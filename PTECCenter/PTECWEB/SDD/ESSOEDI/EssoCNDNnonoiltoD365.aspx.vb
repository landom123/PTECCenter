﻿Imports System.IO

Imports ClosedXML.Excel

Public Class EssoCNDNnonoiltoD365
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")
        txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'gsmtable = Session("gsmtable")
            'BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("edinonoil") Is Nothing) Then
                editable = Session("edinonoil")
                BindData()
            End If
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
    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("terminal", GetType(String))
        dt.Columns.Add("sono", GetType(String))
        dt.Columns.Add("shipto", GetType(String))
        dt.Columns.Add("invoice_no", GetType(String))

        dt.Columns.Add("invoice_date", GetType(Date))
        dt.Columns.Add("due_date", GetType(Date))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = editable
        gvData.DataBind()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim InvoiceDate As String
        Dim objedi As New EDI

        InvoiceDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        editable = objedi.ListInvoice(InvoiceDate, "NON")
        Session("edinonoil") = editable
        BindData()
    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objedi As New EDI
        Dim chkerror As Boolean = False
        Dim mydataset As DataSet
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String

        Dim invoiceno As Label
        Dim InvoiceDate As String = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        For i = 0 To gvData.Rows.Count - 1
            Dim row As GridViewRow = gvData.Rows(i)
            'Dim id As String = row.Cells(0).Text
            'Dim chkSelect As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
            invoiceno = CType(row.FindControl("lblinvoiceno"), Label)

            Try
                '2. save data and get json
                'result = objedi.SaveTTForD365AndGetJson(invoiceno.Text, usercode) 
                objedi.SaveInvoiceForD365AndGetJson(invoiceno.Text, usercode) '36317898

            Catch ex As Exception
                chkerror = True
                javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

            End Try

        Next
        If chkerror = False Then
            mydataset = objedi.EDI_CnDn_nonoil_Data_for_D365(InvoiceDate, InvoiceDate)
            ExportToExcel(mydataset, "NonOIL", InvoiceDate)
        End If
    End Sub
    Private Sub ExportToExcel(mydataset As DataSet, branch As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydataset.Tables(0), "NonOil")
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
    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        If Not IsNothing(chk) Then
            chk.Checked = e.Row.DataItem("chk")
        End If

    End Sub

    'Private Sub btnselectall_Click(sender As Object, e As EventArgs) Handles btnselectall.Click
    '    Dim chk As CheckBox
    '    For Each row As GridViewRow In gvData.Rows

    '        chk = CType(row.FindControl("chk"), CheckBox)
    '        chk.Checked = True
    '    Next

    'End Sub

    'Private Sub btnunselect_Click(sender As Object, e As EventArgs) Handles btnunselect.Click
    '    Dim chk As CheckBox
    '    For Each row As GridViewRow In gvData.Rows

    '        chk = CType(row.FindControl("chk"), CheckBox)
    '        chk.Checked = False
    '    Next
    'End Sub
End Class