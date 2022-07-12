

Imports System.IO

Imports ClosedXML.Excel
Public Class test02
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

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

            If Not (Session("ediinv") Is Nothing) Then
                editable = Session("ediinv")
                BindData()
            End If
        End If

    End Sub

    Private Sub ExportToExcel(mydataset As DataSet, branch As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydataset.Tables(0), "Invoice")
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
        'gvData.DataSource = editable
        'gvData.DataBind()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

        Dim objagree As New Agree
        Dim mydataset As DataSet
        'InvoiceDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        'editable = objedi.ListInvoice(InvoiceDate, "ZF2")
        'Session("ediinv") = editable
        'BindData()
        mydataset = objagree.Ag_Monthly_Payment_Calcuate(Double.Parse(txtagreeno.Text), txtMonthly.Text)
        GridView1.DataSource = mydataset.Tables(0)
        GridView1.DataBind()
        'GridView2.DataSource = mydataset.Tables(1)
        'GridView2.DataBind()
        'GridView3.DataSource = mydataset.Tables(2)
        'GridView3.DataBind()
    End Sub

    'Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
    'gvData.PageIndex = e.NewPageIndex
    'BindData()
    'End Sub

    'Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
    '    '*** chk ***'
    '    Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
    '    If Not IsNothing(chk) Then
    '        chk.Checked = e.Row.DataItem("chk")
    '    End If

    'End Sub

End Class