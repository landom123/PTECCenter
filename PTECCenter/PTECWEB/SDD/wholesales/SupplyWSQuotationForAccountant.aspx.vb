
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyWSQuotationForAccountant
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
            'BindData(saleitemtable)
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

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
    Private Function createimagetable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("imagepath", GetType(String))
        dt.Columns.Add("url", GetType(String))

        Return dt
    End Function

    Private Sub FindData(docno As String)
        Dim wsobj As New wholesales
        Dim mydataset As DataSet
        Dim findataset As DataSet
        Try
            mydataset = wsobj.Wholesales_Quotation_Find(docno)
            findataset = wsobj.Wholesales_Quotation_Finance_Find(docno)
            'showdata
            If mydataset.Tables(0).Rows.Count > 0 Then
                ShowData(mydataset.Tables(0))
            End If
            If findataset.Tables(0).Rows.Count > 0 Then
                ShowFinData(findataset.Tables(0))
            End If
            saleitemtable = mydataset.Tables(1)
            imagetable = mydataset.Tables(2)
            Session("saleitemtable") = saleitemtable
            BindData(saleitemtable)

        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('FindData : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub ShowFinData(mytable As DataTable)
        With mytable.Rows(0)
            lblCreateBy.Text = ""
            lblFinanceDate.Text = .Item("createdate")
            txtPaymentdate.Text = .Item("paiddate")
            txtPaymentAmount.Text = .Item("paidamount")
            txtCreditAmount.Text = .Item("creditamount")
            txtRemark.Text = .Item("remark")
            setbutton(.Item("status"))
        End With
    End Sub
    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            lblDocNo.Text = .Item("docno")
            lblRemark.Text = .Item("remark")
            lblCommission.Text = .Item("comm_rate")
            lblTTCost.Text = .Item("ttcost_rate")
            lblNetCommission.Text = .Item("comm_amount")
            lblDistanct.Text = .Item("distance")
            lblNetTTCost.Text = .Item("ttcost")
            txtSaledate.Text = .Item("saledate")
            txtDocDate.Text = .Item("createdate")
            lblstatus.Text = .Item("status")
            lblcustomer.Text = .Item("customer")
            lblTerminal.Text = .Item("vendor")
        End With

    End Sub

    Private Sub BindData(mytable As DataTable)
        gvSaleitem.DataSource = mytable
        gvSaleitem.DataBind()
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim wsobj As New wholesales
        Dim paiddate As DateTime
        Dim creditamt, paidamt As Double
        Dim remark As String
        docno = lblDocNo.Text
        Try
            paiddate = DateTime.Parse(txtPaymentdate.Text)
            creditamt = Double.Parse(txtCreditAmount.Text)
            paidamt = Double.Parse(txtPaymentAmount.Text)
            remark = txtRemark.Text
            If wsobj.Wholesales_Quotation_Finance_Save(docno, paiddate, remark, creditamt, paidamt, usercode) Then
                setbutton("Pendding")
                javaScript = "alertSuccess('save : Complete')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('save : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim wsobj As New wholesales

        docno = lblDocNo.Text
        Try

            If wsobj.Wholesales_Quotation_Finance_Confirm(docno, usercode) Then
                setbutton("Confirm")
                javaScript = "alertSuccess('Confirm : Complete')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "alertWarning('Confirm : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub setbutton(status As String)
        'lblstatus.Text = "สถานะ : " & status
        Select Case status
            Case = "Confirm"
                'btnNew.Enabled = True
                btnSave.Enabled = False
                btnConfirm.Enabled = False
            Case = "Pendding"
                btnSave.Enabled = True
                btnConfirm.Enabled = True
        End Select
    End Sub
End Class