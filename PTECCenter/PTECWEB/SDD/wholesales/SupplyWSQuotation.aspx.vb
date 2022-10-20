
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyWSQuotation
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public saleitemtable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public docno As String


    Public scriptKey As String = "alert"
    Public javaScript As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        'txtPriceDate.Attributes.Add("readonly", "true")
        txtSaledate.Attributes.Add("autocomplete", "off")
        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            saleitemtable = Session("saleitemtable")
            'BindData(saleitemtable)
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

            docno = Request.QueryString("docno")
            If String.IsNullOrEmpty(docno) Then
                Dim emptytable As DataTable = create()
                Dim R As DataRow = emptytable.NewRow
                emptytable.Rows.Add(R)
                Session("saleitemtable") = saleitemtable
                BindData(emptytable)
            Else
                FindData(docno)
            End If

        End If

    End Sub
    Private Sub FindData(docno As String)
        Dim wsobj As New wholesales
        Dim mydataset As DataSet
        Try
            mydataset = wsobj.Wholesales_Quotation_Find(docno)
            'showdata
            ShowData(mydataset.Tables(0))
            saleitemtable = mydataset.Tables(1)
            Session("saleitemtable") = saleitemtable
            BindData(saleitemtable)

        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('setCboCustomer : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            lblDocNo.Text = .Item("docno")
            txtremark.Text = .Item("remark")
            lblCommission.Text = .Item("comm_rate")
            lblTTCost.Text = .Item("ttcost_rate")
            lblNetCommission.Text = .Item("comm_amount")
            lblDistanct.Text = .Item("distance")
            lblNetTTCost.Text = .Item("ttcost")
            txtSaledate.Text = .Item("saledate")
            txtDocDate.Text = .Item("createdate")
            setbutton(.Item("status"))
            cboCustomer.SelectedIndex = cboCustomer.Items.IndexOf(cboCustomer.Items.FindByValue(.Item("customerid")))
            cboTerminal.SelectedIndex = cboTerminal.Items.IndexOf(cboTerminal.Items.FindByValue(.Item("vendorid")))
        End With

    End Sub
    Protected Sub BtnDelRow(sender As Object, e As EventArgs)

        'javaScript = "alertWarning('ยังไม่เสร็จ');"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        'javaScript = "<script type='text/javascript'>msgalert('ยังไม่เสร็จ');</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

        Dim product As Label = TryCast(sender.FindControl("lblproduct"), Label)

        For Each row As DataRow In saleitemtable.Rows
            If product.Text = row.Item("product") Then
                row.Delete()
            End If
        Next row
        Session("saleitemtable") = saleitemtable
        BindData(saleitemtable)

    End Sub
    Private Sub BindData(mytable As DataTable)
        gvSaleitem.DataSource = mytable
        gvSaleitem.DataBind()
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
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
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
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub setCboCustomer()
        Dim wsobj As New wholesales
        Dim mytable As DataTable
        Try
            mytable = wsobj.Wholesales_Customer_List()

            cboCustomer.DataSource = mytable
            cboCustomer.DataValueField = "customerid"
            cboCustomer.DataTextField = "essoname"
            cboCustomer.DataBind()
        Catch ex As Exception

            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('setCboCustomer : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("product", GetType(String))
        dt.Columns.Add("volume", GetType(Double))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("terminalmarkup", GetType(Double))
        dt.Columns.Add("markup", GetType(Double))
        dt.Columns.Add("total", GetType(Double))

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


    Private Sub CalcItemPrice()
        'หาราคาขาย
        'หาค่าขนส่ง
        'หาค่าคอม
        Dim wsobj As New wholesales
        Dim mytable As DataTable
        Dim pricedate As DateTime
        Dim supplyid, customerid As Double
        Dim product As String

        If validatedata() Then
            pricedate = DateTime.Parse(txtSaledate.Text)
            supplyid = cboTerminal.SelectedItem.Value
            product = cboProduct.Text
            customerid = cboCustomer.SelectedItem.Value
            Try
                mytable = wsobj.Wholesales_Calc_Detail_for_Sale(pricedate, supplyid, product, customerid)
                'lblPrice.Text = mytable.Rows(0).Item("price") + mytable.Rows(0).Item("gap")
                lblDistanct.Text = mytable.Rows(0).Item("distance")
                lblTTCost.Text = Math.Round(mytable.Rows(0).Item("ttcost"), 2)
                lblCommission.Text = mytable.Rows(0).Item("commission")

                Dim R As DataRow = saleitemtable.NewRow
                R("product") = cboProduct.SelectedItem.Text
                R("volume") = txtVolume.Text
                R("price") = mytable.Rows(0).Item("price")
                R("terminalmarkup") = mytable.Rows(0).Item("terminalmarkup")
                R("markup") = txtAdd.Text
                R("total") = (mytable.Rows(0).Item("price") + Double.Parse(txtAdd.Text) + mytable.Rows(0).Item("terminalmarkup")) * Double.Parse(txtVolume.Text)
                saleitemtable.Rows.Add(R)

                Session("saleitemtable") = saleitemtable
                BindData(saleitemtable)

            Catch ex As Exception
                Dim err As String = ex.Message.Replace("'", "")
                javaScript = "alertWarning('btnCalc_Click : เกิดข้อผิดพลาดในการค้นหาราคาขาย " & err & "')"
                'javaScript = "<script type='text/javascript'>msgalert('btnCalc_Click : " & err & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        End If
    End Sub
    Public Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim pricedate As DateTime
        Try
            pricedate = DateTime.Parse(txtSaledate.Text)
        Catch ex As Exception
            result = False
            msg = "วัน เวลา ที่มีผลไม่ถูกต้อง"
            GoTo error_handler
        End Try

error_handler:
        If result = False Then
            javaScript = "alertWarning('validatedata : " & msg & "')"
            'javaScript = "<script type='text/javascript'>msgalert('validatedata : " & msg & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
        Return result
    End Function
    Public Sub clear()
        lblCommission.Text = "0"
        txtAdd.Text = "0"
        txtVolume.Text = "0"
    End Sub

    Private Sub btnCalcPrice_Click(sender As Object, e As EventArgs)
        Dim price, oilprice As Double
        Dim comm, commnet As Double
        Dim volume As Double
        Dim ttcost, ttcostnet As Double
        Dim added As Double


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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validatedata() Then
            If SaveData() Then
                javaScript = "alertSuccess('บันทึกเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        End If
    End Sub
    Private Function chkdup(product As String) As Boolean
        Dim result As Boolean = False
        Dim dr() As DataRow
        dr = saleitemtable.Select("product='" & product & "'")

        If dr.Length > 0 Then
            result = True 'ซ้ำ
        Else
            result = False 'ไม่ซ้ำ
        End If

        Return result
    End Function
    Private Function SaveData() As Boolean
        Dim result As Boolean = True
        Dim wsobj As New wholesales
        Dim docno As String = lblDocNo.Text
        Dim pricedate As DateTime
        Dim supplyid, customerid, volume, amount As Double
        Dim remark As String = txtremark.Text
        Dim product As String
        supplyid = cboTerminal.SelectedItem.Value
        customerid = cboCustomer.SelectedItem.Value
        product = cboProduct.Text
        volume = Convert.ToInt32(saleitemtable.Compute("SUM(volume)", String.Empty))

        Try
            amount = Double.Parse(lblTotal.Text)
            pricedate = DateTime.Parse(txtSaledate.Text)
            docno = wsobj.Wholesales_Quotation_Save(docno, pricedate, supplyid, customerid, volume, amount, remark, usercode)
            lblDocNo.Text = docno

            setbutton("Pendding")
            SaveDetail(docno)
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('Savedata() : " & err & " ')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        Return result
    End Function
    Private Sub SaveDetail(docno As String)
        Dim wsobj As New wholesales
        Dim product As String
        Dim terminalmarkup, volume, price, markup As Double



        Try
            wsobj.Wholesales_Quotation_Delete_before_Save_Detail(docno)

            For Each row As DataRow In saleitemtable.Rows
                If Not row.RowState = DataRowState.Deleted Then
                    terminalmarkup = row.Item("terminalmarkup")
                    product = row.Item("product")
                    volume = row.Item("volume")
                    price = row.Item("price")
                    markup = row.Item("markup")
                    wsobj.Wholesales_Quotation_SaveDetail(docno, product, terminalmarkup, price, volume, markup)
                End If
            Next
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('SaveDetail() : " & err & " ')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If CancelData() Then

            setbutton("Cancel")
            javaScript = "alertSuccess('Cancel รายการนี้เรียบร้อย')"
            'javaScript = "<script type='text/javascript'>msgalert('Cancel รายการนี้เรียบร้อย');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        End If
    End Sub

    Private Function CancelData() As Boolean
        Dim result As Boolean = True

        Dim wsobj As New wholesales
        Try
            wsobj.Wholesales_Quotation_Cancel(lblDocNo.Text, usercode)
        Catch ex As Exception
            result = False
            Dim err As String = ex.Message
            err = err.Replace("'", "")
            javaScript = "alertWarning('" & err & "')"
            'javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        Return result
    End Function

    Private Function ConfirmData() As Boolean
        Dim result As Boolean = True
        'Dim pricedate As DateTime
        Dim wsobj As New wholesales
        Try
            wsobj.Wholesales_Quotation_Confirm(lblDocNo.Text, usercode)
        Catch ex As Exception
            result = False
            Dim err As String = ex.Message
            err = err.Replace("'", "")
            javaScript = "alertWarning('" & err & "')"
            'javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        Return result
    End Function

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If ConfirmData() Then
            setbutton("Approve")
            'javaScript = "alertWarning('ยังไม่เสร็จ');"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            javaScript = "alertSuccess('Confirm รายการนี้เรียบร้อย')"
            'javaScript = "<script type='text/javascript'>msgalert('');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        End If
    End Sub

    Private Sub setbutton(status As String)
        lblstatus.Text = "สถานะ : " & status
        Select Case status
            Case = "Approve"
                btnNew.Enabled = True
                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = True

            Case = "Pendding"
                btnNew.Enabled = True
                btnSave.Enabled = True
                btnConfirm.Enabled = True
                btnCancel.Enabled = True

            Case = "Cancel"
                btnNew.Enabled = True
                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

        End Select
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

        Dim nettotal As Double
        Try
            nettotal = Convert.ToInt32(saleitemtable.Compute("SUM(total)", String.Empty))
        Catch ex As Exception
            nettotal = 0
        End Try
        Dim volumetotal As Double
        Try
            volumetotal = Convert.ToInt32(saleitemtable.Compute("SUM(volume)", String.Empty))
        Catch ex As Exception
            volumetotal = 0
        End Try

        Dim lbltotalfooter As Label
        Try
            lbltotalfooter = TryCast(gvSaleitem.FooterRow.FindControl("lblnettotal"), Label)
            lbltotalfooter.Text = nettotal
        Catch ex As Exception
            lbltotalfooter.Text = "0"
        End Try

        Dim lblvolumetotal As Label
        Try
            lblvolumetotal = TryCast(gvSaleitem.FooterRow.FindControl("lblvolumetotal"), Label)
            lblvolumetotal.Text = volumetotal
        Catch ex As Exception
            lblvolumetotal.Text = "0"
        End Try


        Dim netcommission, netttcost, commissionrate, ttcostrate As Double
        Try
            commissionrate = Double.Parse(lblCommission.Text)
        Catch ex As Exception
            commissionrate = 0
        End Try
        Try
            ttcostrate = Double.Parse(lblTTCost.Text)
        Catch ex As Exception
            ttcostrate = 0
        End Try
        lblNetCommission.Text = (commissionrate * volumetotal).ToString("N02")
        lblNetTTCost.Text = (ttcostrate * volumetotal).ToString("N02")

        Try
            netcommission = Double.Parse(lblNetCommission.Text)
        Catch ex As Exception
            netcommission = 0
        End Try
        Try
            netttcost = Double.Parse(lblNetTTCost.Text).ToString("N02")
        Catch ex As Exception
            netttcost = 0
        End Try
        lblOilPrice.Text = nettotal.ToString("N02")
        lbltotal.Text = (nettotal + netcommission + netttcost).ToString("N02")
        lblTotalPerLitre.Text = ((nettotal + netcommission + netttcost) / volumetotal).ToString("N02")

        'cbo.DataSource = mytable
        'cbo.DataTextField = "name"
        'cbo.DataValueField = "wsvendid"
        'cbo.DataBind()

    End Sub

    Private Sub btnAddtoTable_Click(sender As Object, e As EventArgs) Handles btnAddtoTable.Click
        Dim product As String = cboProduct.SelectedItem.Text
        If chkdup(product) = False Then
            CalcItemPrice()
        Else
            javaScript = "alertWarning('ผลิตภัณฑ์ซ้ำ ไม่สามารถเพิ่มได้')"
            'javaScript = "<script type='text/javascript'>msgalert('');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        lblDocNo.Text = ""
        txtDocDate.Text = ""
        saleitemtable.Rows.Clear()
        Session("saleitemtable") = saleitemtable
        BindData(saleitemtable)
    End Sub
End Class