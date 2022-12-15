
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyWSPriceDailyList
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public pricetable As DataTable = create()
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


        txtPriceDate.Attributes.Add("autocomplete", "off")
        txtPriceDate.Attributes.Add("readonly", "true")
        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            pricetable = Session("pricetable")
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
            Session("pricetable") = pricetable
            BindData(emptytable)

        End If

    End Sub
    Public Sub FindData(pricedate As String, enddate As String)
        Dim wsobj As New wholesales
        Dim mytable As DataTable

        mytable = wsobj.Wholesales_Price_Daily_List(pricedate, enddate)
        Session("pricetable") = mytable
        BindData(mytable)

    End Sub
    Private Sub showdata(mytable As DataTable)

    End Sub

    Public Sub PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Private Sub BindData(mytable As DataTable)
        gvPrice.DataSource = mytable
        gvPrice.DataBind()
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
        dt.Columns.Add("pricedate", GetType(String))
        dt.Columns.Add("terminal", GetType(String))
        dt.Columns.Add("b5", GetType(Double))
        dt.Columns.Add("b7", GetType(Double))
        dt.Columns.Add("b10", GetType(Double))
        dt.Columns.Add("g91", GetType(Double))
        dt.Columns.Add("g95", GetType(Double))
        dt.Columns.Add("e20", GetType(Double))
        dt.Columns.Add("e85", GetType(Double))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Public Sub clear()

    End Sub

    Protected Sub gvPrice_PageIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim pricedate As String
        Dim enddate As String

        pricedate = Date.Parse(txtPriceDate.Text).Year.ToString("0000") & Date.Parse(txtPriceDate.Text).Month.ToString("00") & Date.Parse(txtPriceDate.Text).Day.ToString("00")
        enddate = Date.Parse(txtEnddate.Text).Year.ToString("0000") & Date.Parse(txtEnddate.Text).Month.ToString("00") & Date.Parse(txtEnddate.Text).Day.ToString("00")
        FindData(pricedate, enddate)
    End Sub
End Class