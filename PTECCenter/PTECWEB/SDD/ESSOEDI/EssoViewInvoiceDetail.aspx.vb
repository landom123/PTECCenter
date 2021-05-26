Public Class EssoViewInvoiceDetail
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public splittable As DataTable = createsplittable()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm
        Dim invoiceno As String
        Dim mydataset As DataSet
        usercode = Session("usercode")
        username = Session("username")


        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            If Session("splittable") Is Nothing Then
                splittable = createsplittable()
            Else
                splittable = Session("splittable")
            End If
            editable = Session("editable")
            BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If


            Dim objedi As New EDI

            invoiceno = Request.QueryString("invoiceno")
            mydataset = objedi.GetInvoiceInfo(invoiceno)

            editable = mydataset.Tables(0)
            splittable = mydataset.Tables(1)
            Session("editable") = editable
            Session("splittable") = splittable
            BindData()
            BindDataDGV2()
            ShowData(editable)



        End If

    End Sub
    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            lblinvoiceno.Text = .Item("invoice_no")
            lblbillingtype.Text = .Item("billing_type")
            lblinvoicedate.Text = .Item("invoice_date")

            lblduedate.Text = .Item("due_date")
            lblsono.Text = .Item("so_no")
            lbldeliveryno.Text = .Item("delivery_no")

            lblshipfrom.Text = .Item("shipfrom")
            lblinvoiceamount.Text = Double.Parse(.Item("total_value")).ToString("#,###,##0.00") 'String.Format("#,###,##0.00", .Item("total_value"))

            cbomatcode.Items.Clear()
            cboSono.Items.Clear()
            For i = 0 To mytable.Rows.Count - 1
                cbomatcode.Items.Add(mytable.Rows(i).Item("mat_code"))
                cboSono.Items.Add(mytable.Rows(i).Item("so_no"))
            Next
            Dim objedi As New EDI
            Dim temptable As DataTable = objedi.shiptoList
            cboshipto.DataSource = temptable
            cboshipto.DataTextField = "essoname"
            cboshipto.DataValueField = "code"
            cboshipto.DataBind()
        End With

    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("billing_type", GetType(String))
        dt.Columns.Add("invoice_no", GetType(String))
        dt.Columns.Add("invoice_date", GetType(Date))
        dt.Columns.Add("due_date", GetType(Date))
        dt.Columns.Add("loading_date", GetType(Date))
        dt.Columns.Add("net_value", GetType(Double))
        dt.Columns.Add("vat", GetType(Double))
        dt.Columns.Add("total_value", GetType(Double))
        dt.Columns.Add("shipfrom", GetType(String))
        dt.Columns.Add("shipto", GetType(String))
        dt.Columns.Add("so_no", GetType(String))
        dt.Columns.Add("delivery_no", GetType(String))
        dt.Columns.Add("mat_code", GetType(String))
        dt.Columns.Add("product", GetType(String))
        dt.Columns.Add("do_volume", GetType(Double))
        dt.Columns.Add("unit_price", GetType(Double))
        dt.Columns.Add("item_net_value", GetType(Double))
        dt.Columns.Add("ref_invoice_no", GetType(String))

        Return dt
    End Function

    Private Function createsplittable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ID", GetType(Double))
        dt.Columns.Add("sono", GetType(String))
        dt.Columns.Add("matcode", GetType(String))
        dt.Columns.Add("shipto", GetType(String))
        dt.Columns.Add("shipname", GetType(String))
        dt.Columns.Add("volume", GetType(Double))

        Return dt
    End Function
    Private Sub BindData()
        gvData.DataSource = editable
        gvData.DataBind()
    End Sub
    Private Sub BindDataDGV2()
        dgv2.DataSource = splittable
        dgv2.DataBind()
    End Sub


    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgv2.RowDataBound
        '*** chk ***'
        'Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        'If Not IsNothing(chk) Then
        '    chk.Checked = e.Row.DataItem("chk")
        'End If

    End Sub

    'Public Sub gvData_OnRowCommand(sender As Object, e As GridViewCommandEventArgs)
    '    If e.CommandName = "UpdateData" Then
    '        googlesheettable.Rows.Add("test", "test")

    '        BindData()
    '    End If
    'End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        '1. บันทึกข้อมูล เช็คข้อมูลใน store
        Dim invoiceno As String = lblinvoiceno.Text
        Dim saleorder As String = cboSono.SelectedItem.Text
        Dim matcode As String = cbomatcode.SelectedItem.Text
        Dim shipto As String = cboshipto.SelectedItem.Value
        Dim shipname As String = cboshipto.SelectedItem.Text
        Dim volume As Double
        Try
            volume = Double.Parse(txtvolume.Text)
        Catch ex As Exception
            volume = 0
        End Try
        If volume > 0 Then
            'save
            Dim objedi As New EDI
            Try
                splittable = objedi.SaveSplitData(invoiceno, usercode, matcode, saleorder, shipto, volume)
                '2. ถ้า save ได้ ส่ง table กลับมาที่ splittable 
                'splittable.Rows.Add(cbomatcode.SelectedItem.Text, cboshipto.SelectedItem.Value, cboshipto.SelectedItem.Text, txtvolume.Text)
                Session("splittable") = splittable
                BindDataDGV2()
            Catch ex As Exception
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String
                javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            End Try
        Else
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String
            javaScript = "<script type='text/javascript'>msgalert('Volume ต้องมากกว่า 0');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If

    End Sub

    Private Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click

        Dim chk As CheckBox
        Dim objedi As New EDI
        Dim id As Label
        Dim mydataset As DataSet


        Try
            For i = 0 To dgv2.Rows.Count - 1
                Dim row As GridViewRow = dgv2.Rows(i)
                'Dim id As String = row.Cells(0).Text
                'Dim chkSelect As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
                chk = DirectCast(row.FindControl("chk"), CheckBox)
                id = DirectCast(row.FindControl("lblid"), Label)
                If chk.Checked = True Then
                    'delete
                    If objedi.RemoveSplitData(Double.Parse(id.Text)) Then


                        mydataset = objedi.GetInvoiceInfo(lblinvoiceno.Text)

                        editable = mydataset.Tables(0)
                        splittable = mydataset.Tables(1)
                        Session("editable") = editable
                        Session("splittable") = splittable
                        BindData()
                        BindDataDGV2()
                        ShowData(editable)
                    End If
                End If

            Next i
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String
            javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try




    End Sub
End Class