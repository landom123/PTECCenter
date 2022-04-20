
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyWSQuatation
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public scriptKey As String = "UniqueKeyForThisScript"
    Public javaScript As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        usercode = Session("usercode")
        username = Session("username")

        txtSaleDate.Attributes.Add("readonly", "true")
        txtPriceDate.Attributes.Add("readonly", "true")
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

            setCboTerminal()
            setCboCustomer()
            setCboProduct()

        End If

    End Sub
    Private Sub setCboTerminal()
        Dim wsobj As New wholesales
        Dim mytable As DataTable
        Try
            mytable = wsobj.Wholesales_Terminal_List()

            cboTerminal.DataSource = mytable
            cboTerminal.DataValueField = "wsvendid"
            cboTerminal.DataTextField = "name"
            cboTerminal.DataBind()
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub
    Private Sub setCboProduct()
        Dim wsobj As New wholesales
        Dim mytable As DataTable
        Try
            mytable = wsobj.Wholesales_Product_List()

            cboProduct.DataSource = mytable
            cboProduct.DataValueField = "product"
            cboProduct.DataTextField = "product"
            cboProduct.DataBind()
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub
    Private Sub setCboCustomer()
        Dim wsobj As New wholesales
        Dim mytable As DataTable
        Try
            mytable = wsobj.Wholesales_Customer_List()

            cboCustomer.DataSource = mytable
            cboCustomer.DataValueField = "supplyid"
            cboCustomer.DataTextField = "essoname"
            cboCustomer.DataBind()
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
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

    Public Function check_empy_return_0(chkstring As String) As String
        'check empy string and return "0"
        Dim result As String

        If String.IsNullOrEmpty(chkstring) Then
            result = "0"
        Else
            result = chkstring
        End If

        Return result
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

    End Sub

    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click
        clear()
        'หาราคาขาย
        'หาค่าขนส่ง
        'หาค่าคอม
        Dim wsobj As New wholesales
        Dim mytable As DataTable
        Dim pricedate As DateTime
        Dim supplyid As Double
        Dim product, customer As String
        If validatedata() Then
            pricedate = DateTime.Parse(txtPriceDate.Text)
            supplyid = cboTerminal.SelectedItem.Value
            product = cboProduct.Text
            customer = cboCustomer.SelectedItem.Text
            Try
                mytable = wsobj.Wholesales_Calc_Detail_for_Sale(pricedate, supplyid, product, customer)
                lblPrice.Text = mytable.Rows(0).Item("price") + mytable.Rows(0).Item("gap")
                lblDistanct.Text = mytable.Rows(0).Item("distance")
                lblTTCost.Text = mytable.Rows(0).Item("ttcost")
                lblCommission.Text = mytable.Rows(0).Item("commission")
            Catch ex As Exception
                Dim err As String = ex.Message.Replace("'", "")
                javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            End Try
        End If
    End Sub
    Public Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim pricedate As DateTime
        Try
            pricedate = DateTime.Parse(txtPriceDate.Text)
        Catch ex As Exception
            result = False
            msg = "วัน เวลา ที่มีผลไม่ถูกต้อง"
            GoTo error_handler
        End Try

error_handler:
        If result = False Then
            javaScript = "<script type='text/javascript'>msgalert('" & msg & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
        Return result
    End Function
    Public Sub clear()
        lblCommission.Text = "0"
        txtAdd.Text = "0"
        txtVolume.Text = "0"
    End Sub

    Private Sub btnCalcPrice_Click(sender As Object, e As EventArgs) Handles btnCalcPrice.Click
        Dim price, oilprice, total As Double
        Dim comm, commnet As Double
        Dim volume As Double
        Dim ttcost, ttcostnet As Double
        Dim added As Double

        price = Double.Parse(lblPrice.Text)
        added = Double.Parse(txtAdd.Text)
        comm = Double.Parse(lblCommission.Text)
        volume = Double.Parse(txtVolume.Text)
        ttcost = Double.Parse(lblTTCost.Text)

        oilprice = volume * (price + added)
        lblOilPrice.Text = oilprice.ToString("N02")
        commnet = comm * volume
        lblNetCommission.Text = commnet.ToString("N02")
        ttcostnet = ttcost * volume
        lblNetTTCost.Text = ttcostnet.ToString("N02")
        lblTotal.Text = (oilprice + commnet + ttcostnet).ToString("N02")
        lblTotalPerLitre.Text = ((oilprice + commnet + ttcostnet) / volume).ToString("N02")


    End Sub
End Class