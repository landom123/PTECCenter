Imports System.IO
Imports ClosedXML.Excel

Public Class rptPCCO
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public username As String
    Public usercode As String

    Public mydatatable As DataTable '= createmydatatable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objjob As New jobs
        Dim objsupplier As New Supplier
        Dim objetax As New BPETAX

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

        If Not IsPostBack() Then

            txtduedate.Text = Today()

        Else
            'mydatatable = Session("mydatatable_edi")
        End If

    End Sub

    'Private Function createmydatatable() As DataTable
    '    Dim dt As New DataTable

    '    dt.Columns.Add("billing_type", GetType(String))
    '    dt.Columns.Add("invoice_no", GetType(String))
    '    dt.Columns.Add("shipment", GetType(String))
    '    dt.Columns.Add("invoice_date", GetType(String))
    '    dt.Columns.Add("loading_date", GetType(String))
    '    dt.Columns.Add("due_date", GetType(String))
    '    dt.Columns.Add("net_value", GetType(String))
    '    dt.Columns.Add("vat", GetType(String))
    '    dt.Columns.Add("total_value", GetType(String))
    '    dt.Columns.Add("sh_ship_to_party", GetType(String))
    '    dt.Columns.Add("po_no", GetType(String))
    '    dt.Columns.Add("po_no1", GetType(String))
    '    dt.Columns.Add("so_no", GetType(String))
    '    dt.Columns.Add("delivery_no", GetType(String))
    '    dt.Columns.Add("terminal", GetType(String))
    '    dt.Columns.Add("mat_code", GetType(String))
    '    dt.Columns.Add("product", GetType(String))
    '    dt.Columns.Add("do_volume", GetType(String))
    '    dt.Columns.Add("uom", GetType(String))
    '    dt.Columns.Add("unit_price", GetType(String))
    '    dt.Columns.Add("item_net_value", GetType(String))
    '    dt.Columns.Add("vehicle", GetType(String))

    '    Return dt
    'End Function
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        'If validatedata() Then
        Dim objNonpo As New NonPO
        Try

            mydatatable = objNonpo.Nonpo_Export_PCCO_All_Branch_By_Due(txtduedate.Text.ToString.Trim, chkGroupVAT.Checked, chkGroupVendor.Checked)
            ExportToExcel(mydatatable, Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        'End If

    End Sub


    '    Private Function validatedata() As Boolean
    '        Dim result As Boolean = True
    '        Dim msg As String = ""

    '        If txtbegindate.Text.Trim() = "" Then
    '            result = False
    '            msg = "กรุณาใส่วันเริ่ม"
    '            GoTo endprocess
    '        End If
    '        If txtenddate.Text.Trim() = "" Then
    '            result = False
    '            msg = "กรุณาใส่ถึงวันที่"
    '            GoTo endprocess
    '        End If


    '        'If amountdedusctsell < 0 Then
    '        '    result = False
    '        '    msg = "กรุณาใส่จำนวนเต็ม"
    '        '    GoTo endprocess
    '        'End If
    'endprocess:
    '        If result = False Then
    '            Dim scriptKey As String = "alert"
    '            Dim javaScript As String = "alertWarning('" + msg + "');"
    '            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '            'MsgBox(msg)
    '        End If


    '        Return result
    '    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtduedate.Text = ""

        'BindData()
    End Sub

    'Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
    '    search()
    'End Sub
    'Private Sub search()
    '    If validatedata() Then

    '        Dim objedi As New EDI
    '        Dim detailtable As New DataTable
    '        Try

    '            Dim stdt As DateTime = Convert.ToDateTime(txtbegindate.Text)
    '            Dim edt As DateTime = Convert.ToDateTime(txtenddate.Text)
    '            Dim format As String = "yyyyMMdd"
    '            Dim std As String = stdt.ToString(format)
    '            Dim ed As String = edt.ToString(format)
    '            mydatatable = objedi.EDI_RPT_Invoice_byDate(std, ed)
    '            BindData()

    '            Session("mydatatable_edi") = mydatatable
    '        Catch ex As Exception
    '            Dim scriptKey As String = "alert"
    '            Dim javaScript As String = "alertWarning('search fail');"
    '            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '        End Try
    '    End If
    'End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & Date.Now.ToString
            Dim encode As String
            'If (maintable.Rows(0).Item("statusid") = 7) Then
            '    encode = "(preview)"
            'Else
            '    encode = "(final)"
            'End If
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(filename)
            encode = Convert.ToBase64String(byt)

            'Dim decode As String
            'Dim b As Byte() = Convert.FromBase64String(encode)
            'decode = System.Text.Encoding.UTF8.GetString(b)

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & encode & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub

    'Private Sub BindData()
    '    'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
    '    'setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
    '    'End If
    '    gvRemind.Caption = "ทั้งหมด " & mydatatable.Rows.Count & " รายการ"
    '    gvRemind.DataSource = mydatatable
    '    gvRemind.DataBind()
    'End Sub
    'Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
    '    gvRemind.PageIndex = e.NewPageIndex
    '    BindData()
    'End Sub
End Class