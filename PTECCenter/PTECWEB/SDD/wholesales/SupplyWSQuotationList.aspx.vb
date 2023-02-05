
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Imports System.Windows

Public Class SupplyWSQuotationList
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public quotationtable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public scriptKey As String = "UniqueKeyForThisScript"
    Public javaScript As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        'txtSaleDate.Attributes.Add("readonly", "true")

        txtEnddate.Attributes.Add("autocomplete", "off")
        txtEnddate.Attributes.Add("readonly", "true")
        txtBegindate.Attributes.Add("autocomplete", "off")
        txtBegindate.Attributes.Add("readonly", "true")
        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            quotationtable = Session("quotationtable")
            'BindData()
        Else

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'สร้างตารางมี row ว่าง เพื่อให้ grid แสดง head, footer
            'pricetable.Rows.Add("ESSR", "b5", "b7", "b10", "g91", "g95", "e20", "e85")
            Dim emptytable As DataTable = create()
            Dim R As DataRow = emptytable.NewRow
            emptytable.Rows.Add(R)
            Session("quotationtable") = quotationtable
            BindData(emptytable)

        End If

    End Sub
    Public Sub FindData(begindate As String, enddate As String)
        Dim wsobj As New wholesales
        Dim mytable As DataTable

        mytable = wsobj.Wholesales_Quotation_List(begindate, enddate)
        Session("quotationtable") = mytable
        BindData(mytable)

    End Sub
    Private Sub showdata(mytable As DataTable)

    End Sub

    Public Sub PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Private Sub BindData(mytable As DataTable)
        gvQuotation.DataSource = mytable
        gvQuotation.DataBind()
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        'Dim wsobj As New wholesales
        'Dim mytable As New DataTable
        'Try
        '    mytable = wsobj.Wholesales_Terminal_List()
        'Catch ex As Exception
        '    Dim err As String = ex.Message.Replace("'", "")
        '    javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
        '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        'End Try

        'Dim cbo As DropDownList = TryCast(gvPrice.FooterRow.FindControl("cboTerminal"), DropDownList)
        'cbo.DataSource = mytable
        'cbo.DataTextField = "name"
        'cbo.DataValueField = "wsvendid"
        'cbo.DataBind()

    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("docno", GetType(String))
        dt.Columns.Add("saledate", GetType(String))
        dt.Columns.Add("terminal", GetType(String))
        dt.Columns.Add("volume", GetType(Double))
        dt.Columns.Add("customer", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("link", GetType(String))


        Return dt
    End Function

    Public Sub clear()

    End Sub

    Protected Sub gvPrice_PageIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            Dim begindate, enddate As String
            If txtBegindate.Text = "" Or IsDBNull(txtBegindate.Text) = True Then MessageBox.Show("กรุณาระบุวันที่เริ่มต้น") : Exit Sub
            If txtEnddate.Text = "" Or IsDBNull(txtEnddate.Text) = True Then MessageBox.Show("กรุณาระบุวันที่สิ้นสุด") : Exit Sub
            begindate = Date.Parse(txtBegindate.Text).Year.ToString("0000") & Date.Parse(txtBegindate.Text).Month.ToString("00") & Date.Parse(txtBegindate.Text).Day.ToString("00")
            enddate = Date.Parse(txtEnddate.Text).Year.ToString("0000") & Date.Parse(txtEnddate.Text).Month.ToString("00") & Date.Parse(txtEnddate.Text).Day.ToString("00")
            FindData(begindate, enddate)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class