
Imports System.IO

Imports ClosedXML.Excel
Public Class EssoCNDNOiltoD365
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

            If Not (Session("edioil") Is Nothing) Then
                editable = Session("edioil")
                BindData()
            End If
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
    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("billing_type", GetType(String))
        dt.Columns.Add("invoice_no", GetType(String))
        dt.Columns.Add("invoice_date", GetType(Date))
        dt.Columns.Add("due_date", GetType(Date))
        dt.Columns.Add("link", GetType(String))
        dt.Columns.Add("chk", GetType(Boolean))

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
        editable = objedi.ListInvoice(InvoiceDate, "OIL")
        Session("edioil") = editable
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


        Dim InvoiceDate As String = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        Dim invoiceno As Label
        Dim jsonstr As String
        For i = 0 To gvData.Rows.Count - 1
            Dim row As GridViewRow = gvData.Rows(i)
            'Dim id As String = row.Cells(0).Text
            'Dim chkSelect As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
            invoiceno = CType(row.FindControl("lblinvoiceno"), Label)

            Try
                '2. save data and get json
                'result = objedi.SaveTTForD365AndGetJson(invoiceno.Text, usercode) 
                jsonstr = objedi.SaveInvoiceForD365AndGetJson(invoiceno.Text, usercode) '36317898

                jsonstr = objedi.SaveCnDnAdjustCostForD365AndGetJson(invoiceno.Text, usercode)

            Catch ex As Exception
                javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

            End Try

        Next

        If chkerror = False Then
            mydataset = objedi.EDI_CnDnAdjust_forExcel(InvoiceDate, InvoiceDate)
            ExportToExcel(mydataset, "CNDNOIL", InvoiceDate)
        End If
    End Sub
    Private Sub ExportToExcel(mydataset As DataSet, branch As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydataset.Tables(0), "AP")
            wb.Worksheets.Add(mydataset.Tables(1), "Cost")

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
        Dim terminal As Label = CType(e.Row.FindControl("lblTerminal"), Label)

        If Not IsNothing(chk) Then
            chk.Checked = e.Row.DataItem("chk")
            If chk.Checked = True Then
                terminal.Text = "xx"
            End If
        End If


    End Sub

    Private Sub btnFind_Command(sender As Object, e As CommandEventArgs) Handles btnFind.Command

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