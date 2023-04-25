
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Imports System.Windows

Public Class SupplyWSQuotation
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public saleitemtable As DataTable = create()
    Public imagetable As DataTable = createimagetable()
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
            imagetable = Session("imagetable")
            BindDataImage(imagetable)
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
                Dim emptyimagetable As DataTable = createimagetable()
                Dim R As DataRow = emptytable.NewRow
                emptytable.Rows.Add(R)
                Session("saleitemtable") = emptytable
                BindData(emptytable)

                Dim img As DataRow = emptyimagetable.NewRow
                emptyimagetable.Rows.Add(img)
                Session("imagetable") = emptyimagetable
                BindDataImage(emptyimagetable)
            Else
                FindData(docno)
            End If

        End If
        'txtAdd.Text = 0

    End Sub
    Private Sub FindData(docno As String)
        Dim wsobj As New wholesales
        Dim mydataset As DataSet
        Try
            mydataset = wsobj.Wholesales_Quotation_Find(docno)
            'showdata

            ShowData(mydataset.Tables(0))
            saleitemtable = mydataset.Tables(1)
            imagetable = mydataset.Tables(2)
            Session("imagetable") = imagetable
            Session("saleitemtable") = saleitemtable
            BindDataImage(imagetable)
            BindData(saleitemtable)

        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('FindData : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            lblDocNo.Text = .Item("docno")
            txtremark.Text = .Item("remark")
            lblCommission.Text = IIf(IsDBNull(.Item("comm_rate")), "", .Item("comm_rate"))
            lblTTCost.Text = .Item("ttcost_rate")
            lblNetCommission.Text = IIf(IsDBNull(.Item("comm_amount")), 0, .Item("comm_amount"))
            lblDistanct.Text = .Item("distance")
            lblNetTTCost.Text = .Item("ttcost")
            txtSaledate.Text = .Item("saledate")
            txtDocDate.Text = .Item("createdate")
            setbutton(.Item("status"))
            cboCustomer.SelectedIndex = cboCustomer.Items.IndexOf(cboCustomer.Items.FindByValue(.Item("customerid")))
            cboTerminal.SelectedIndex = cboTerminal.Items.IndexOf(cboTerminal.Items.FindByValue(.Item("vendorid")))
        End With

    End Sub
    'Protected Sub btnAddImage(sender As Object, e As EventArgs)

    '    If (FileUpload1.HasFile) Then
    '        Dim allow_send_pic() As String = {".jpg", ".png", ".gif", ".pdf"}
    '        Dim Extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
    '        If Array.IndexOf(allow_send_pic, Extension.ToLower()) = -1 Then
    '            Dim scriptKey As String = "alert"
    '            Dim javaScript As String = "alertWarning('Upload ได้เฉพาะ jpg png gif pdf');"
    '            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '        Else
    '            If System.IO.File.Exists("D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\" & FileUpload1.FileName) Then
    '                Dim scriptKey As String = "alert"
    '                Dim javaScript As String = "alertWarning('ชื่อไฟล์ซ้ำกันไม่ได้');"
    '                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '            Else
    '                Dim attatchName As New jobs
    '                Dim fileName As String
    '                fileName = attatchName.GetAttatchName()
    '                Dim savePath As String = "D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\"
    '                'Dim fileName As String = FileUpload1.FileName
    '                savePath += fileName
    '                savePath += Extension
    '                FileUpload1.SaveAs(savePath)
    '                fileName += Extension
    '                'lblattatch.Text = fileName
    '            End If
    '        End If
    '    End If
    'End Sub
    Protected Sub BtnDelRow(sender As Object, e As EventArgs)

        'javaScript = "alertWarning('ยังไม่เสร็จ');"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        'javaScript = "<script type='text/javascript'>msgalert('ยังไม่เสร็จ');</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        Try
            Dim product As Label = TryCast(sender.FindControl("lblproduct"), Label)

            For Each row As DataRow In saleitemtable.Rows

                If product.Text = row.Item("product").ToString Then
                    row.Delete()
                    'saleitemtable.Rows.Remove(row)
                    saleitemtable.AcceptChanges()

                    Session("saleitemtable") = saleitemtable
                    BindData(saleitemtable)

                End If

            Next row

            Session("saleitemtable") = saleitemtable
            BindData(saleitemtable)

        Catch ex As Exception
            'MessageBox.Show("ไม่พบรายการที่ต้องการลบ")
            'MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub BindData(mytable As DataTable)
        'For j = 0 To mytable.Columns.Count - 1
        '    If j = 1 Or j = 2 Or j = 5 Then
        '        mytable.Columns(j).DataType = System.Type.GetType("System.Double")
        '    End If

        'Next
        gvSaleitem.DataSource = mytable
        gvSaleitem.DataBind()



    End Sub


    Private Sub BindDataImage(mytable As DataTable)
        gvImage.DataSource = mytable
        gvImage.DataBind()
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

    Private Function createimagetable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("imagepath", GetType(String))
        dt.Columns.Add("url", GetType(String))

        Return dt
    End Function
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
                lblDistanct.Text = FormatNumber(lblDistanct.Text, 2)
                lblTTCost.Text = Math.Round(mytable.Rows(0).Item("ttcost"), 2)
                lblTTCost.Text = FormatNumber(lblTTCost.Text, 2)
                lblCommission.Text = mytable.Rows(0).Item("commission")
                lblCommission.Text = FormatNumber(lblCommission.Text, 2)

                If IsNumeric(txtAdd.Text) = False Then
                    txtAdd.Text = 0
                End If

                If IsNumeric(txtVolume.Text) = False Then
                    txtVolume.Text = 0
                    txtVolume.Text = CDbl(txtVolume.Text)
                End If

                Dim R As DataRow = saleitemtable.NewRow
                R("product") = cboProduct.SelectedItem.Text
                R("volume") = FormatNumber(CDbl(txtVolume.Text))
                R("price") = FormatNumber(mytable.Rows(0).Item("price") + Math.Round(mytable.Rows(0).Item("ttcost"), 2) + mytable.Rows(0).Item("commission") + +CDbl(txtAdd.Text), 2)
                R("terminalmarkup") = mytable.Rows(0).Item("terminalmarkup")
                R("markup") = FormatNumber(CDbl(txtAdd.Text), 4)
                'R("total") = FormatNumber((mytable.Rows(0).Item("price") + FormatNumber(CDbl(txtAdd.Text), 2) + mytable.Rows(0).Item("terminalmarkup")) * Double.Parse(txtVolume.Text), 2)

                'R("total") = FormatNumber((mytable.Rows(0).Item("price") + 0 + mytable.Rows(0).Item("terminalmarkup")) * Double.Parse(txtVolume.Text), 2)
                R("total") = FormatNumber((R("price") + 0 + mytable.Rows(0).Item("terminalmarkup")) * Double.Parse(txtVolume.Text), 2)
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
        lblOilPrice.Text = FormatNumber(oilprice, 2)
        commnet = comm * volume
        lblNetCommission.Text = FormatNumber(commnet, 2)
        ttcostnet = ttcost * volume
        lblNetTTCost.Text = FormatNumber(ttcostnet, 2)
        lblTotal.Text = FormatNumber((oilprice + commnet + ttcostnet), 2)
        lblTotalPerLitre.Text = FormatNumber(((oilprice + commnet + ttcostnet) / volume), 2)

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

        wsobj.Wholesales_Quotation_Delete_before_Save_Detail(docno)

        For Each row As DataRow In saleitemtable.Rows
            Try
                If Not row.RowState = DataRowState.Deleted Then
                    'terminalmarkup = row.Item("terminalmarkup")
                    'product = row.Item("product")
                    'volume = row.Item("volume")
                    'price = row.Item("price")
                    'markup = row.Item("markup")
                    terminalmarkup = IIf(IsDBNull(row("terminalmarkup")), 0, row("terminalmarkup"))
                    product = IIf(IsDBNull(row("product")), 0, row("product"))
                    volume = IIf(IsDBNull(row("volume")), 0, row("volume"))
                    price = IIf(IsDBNull(row("price")), 0, row("price"))
                    markup = IIf(IsDBNull(row("markup")), 0, row("markup"))

                    wsobj.Wholesales_Quotation_SaveDetail(docno, product, terminalmarkup, price, volume, markup)
                End If
            Catch ex As Exception
                Dim err As String = ex.Message.Replace("'", "")
                javaScript = "alertWarning('SaveDetail() : " & err & " ')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        Next

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
            Case = "Finance"
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

        Dim lbltotalfooter As Label = Nothing

        Try
            lbltotalfooter = TryCast(gvSaleitem.FooterRow.FindControl("lblnettotal"), Label)
            lbltotalfooter.Text = FormatNumber(nettotal, 2)
        Catch ex As Exception
            lbltotalfooter.Text = "0.00"
        End Try

        Dim lblvolumetotal As Label = Nothing

        Try
            lblvolumetotal = TryCast(gvSaleitem.FooterRow.FindControl("lblvolumetotal"), Label)
            lblvolumetotal.Text = FormatNumber(volumetotal, 2)
        Catch ex As Exception
            lblvolumetotal.Text = "0.00"
        End Try


        Dim netcommission, netttcost, commissionrate, ttcostrate As Double
        Try
            commissionrate = Double.Parse(lblCommission.Text)
        Catch ex As Exception
            commissionrate = 1
        End Try
        Try
            ttcostrate = Double.Parse(lblTTCost.Text)
        Catch ex As Exception
            ttcostrate = 0
        End Try
        lblNetCommission.Text = (commissionrate * volumetotal).ToString("N02")
        lblNetCommission.Text = FormatNumber(lblNetCommission.Text, 2)
        lblNetTTCost.Text = (ttcostrate * volumetotal).ToString("N02")
        lblNetTTCost.Text = FormatNumber(lblNetTTCost.Text, 2)

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
        'lblTotal.Text = (nettotal + netcommission + netttcost).ToString("N02")
        lblTotal.Text = (nettotal).ToString("N02")
        lblTotal.Text = FormatNumber(lblTotal.Text, 2)

        'lblTotalPerLitre.Text = ((nettotal + netcommission + netttcost) / volumetotal).ToString("N02")
        lblTotalPerLitre.Text = (nettotal / volumetotal).ToString("N02")
        lblTotalPerLitre.Text = FormatNumber(lblTotalPerLitre.Text, 2)
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

    Private Sub btnAddImage_Click(sender As Object, e As EventArgs) Handles btnAddImage.Click
        If Not String.IsNullOrEmpty(lblDocNo.Text) Then
            If (FileUpload1.HasFile) Then
                Dim allow_send_pic() As String = {".jpg", ".png", ".gif", ".pdf"}
                Dim Extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
                If Array.IndexOf(allow_send_pic, Extension.ToLower()) = -1 Then
                    Dim scriptKey As String = "alert"
                    Dim javaScript As String = "alertWarning('Upload ได้เฉพาะ jpg png gif pdf');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                Else
                    If System.IO.File.Exists("D:\\PTECAttatch\\IMG\\wholesales\\" & FileUpload1.FileName) Then
                        Dim scriptKey As String = "alert"
                        Dim javaScript As String = "alertWarning('ชื่อไฟล์ซ้ำกันไม่ได้');"
                        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    Else
                        'Dim attatchName As New jobs
                        Dim fileName As String
                        Dim imgno As Integer = 1
                        fileName = lblDocNo.Text & "_" & imgno.ToString & Extension 'attatchName.GetAttatchName()
                        For imgno = 1 To 20
                            If System.IO.File.Exists("D:\\PTECAttatch\\IMG\\wholesales\\" & fileName) Then
                                imgno += 1
                                fileName = lblDocNo.Text & "_" & imgno.ToString & Extension
                            Else
                                imgno = 30
                            End If
                        Next


                        Dim savePath As String = "D:\\PTECAttatch\\IMG\\wholesales\\"
                        'Dim fileName As String = FileUpload1.FileName
                        savePath += fileName
                        'savePath += Extension
                        Try
                            FileUpload1.SaveAs(savePath)
                            SaveImgName(fileName)
                        Catch ex As Exception
                            Dim err As String = ex.Message
                            err = err.Replace("'", "")
                            javaScript = "alertWarning('error ขณะบันทึกรูป '" & err & "')"
                            'javaScript = "<script type='text/javascript'>msgalert('');</script>"
                            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                        End Try

                        'fileName += Extension
                        'lblattatch.Text = fileName
                    End If
                End If
            End If
        Else
            javaScript = "alertWarning('ไม่พบเลขที่เอกสาร ไม่สามารถเพิ่มรูปได้')"
            'javaScript = "<script type='text/javascript'>msgalert('');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub
    Private Sub SaveImgName(filename As String)
        'save file name to database
        Dim docno As String = lblDocNo.Text
        Dim wsobj As New wholesales
        Try
            wsobj.Wholesales_Quotation_Save_Image(docno, filename)
            Dim R As DataRow = imagetable.NewRow
            R("imagepath") = filename
            imagetable.Rows.Add(R)
            BindDataImage(imagetable)
            Session("imagetable") = imagetable
        Catch ex As Exception
            javaScript = "alertWarning('error : SaveImgName')"
            'javaScript = "<script type='text/javascript'>msgalert('');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        'refresh imagetable
    End Sub

    Private Sub txtVolume_TextChanged(sender As Object, e As EventArgs) Handles txtVolume.TextChanged
        If IsNumeric(txtVolume.Text) = True Then
            txtVolume.Text = FormatNumber(txtVolume.Text, 2)
        Else
            txtVolume.Text = 0
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'Response.Redirect("SupplyWSQuotationReport.aspx")
        Dim s As String = "window.open('../OPS/Jobs_Report_JobForm.aspx?jobcode=" & Request.QueryString("jobno").ToString() & "&supplierid=0', '_blank');"

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)

    End Sub
End Class