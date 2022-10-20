
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyWSPriceSet
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public pricetable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public scriptKey As String = "UniqueKeyForThisScript"
    Public javaScript As String
    Public pricedate As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        'txtSaleDate.Attributes.Add("readonly", "true")
        'txtPriceDate.Attributes.Add("readonly", "true")

        txtPriceDate.Attributes.Add("autocomplete", "off")
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

            pricedate = Request.QueryString("pricedate")
            If Not String.IsNullOrEmpty(pricedate) Then
                'find data
                FindData(pricedate)
            End If
        End If

    End Sub
    Public Sub FindData(pricedate As String)
        Dim wsobj As New wholesales
        Dim mytable As DataTable

        mytable = wsobj.Wholesales_Price_Find(pricedate)
        showdata(mytable)

    End Sub
    Private Sub showdata(mytable As DataTable)
        BindData(mytable)
        txtremark.Text = mytable.Rows(0).Item("remark")
        txtPriceDate.Text = Date.Parse(pricedate).ToString("dd/MM/yyyy hh:mm")
        lblstatus.Text = mytable.Rows(0).Item("status")

        setbutton(lblstatus.Text)


    End Sub

    Private Sub setbutton(status As String)
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
    Public Sub PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Private Sub BindData(mytable As DataTable)
        gvPrice.DataSource = mytable
        gvPrice.DataBind()
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        Dim wsobj As New wholesales
        Dim mytable As New DataTable
        Try
            mytable = wsobj.Wholesales_Terminal_List()
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Dim cbo As DropDownList = TryCast(gvPrice.FooterRow.FindControl("cboTerminal"), DropDownList)
        cbo.DataSource = mytable
        cbo.DataTextField = "name"
        cbo.DataValueField = "wsvendid"
        cbo.DataBind()

    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("terminal", GetType(String))
        dt.Columns.Add("b5", GetType(Double))
        dt.Columns.Add("b7", GetType(Double))
        dt.Columns.Add("b10", GetType(Double))
        dt.Columns.Add("g91", GetType(Double))
        dt.Columns.Add("g95", GetType(Double))
        dt.Columns.Add("e20", GetType(Double))
        dt.Columns.Add("e85", GetType(Double))
        'dt.Columns.Add("remark", GetType(String))

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

    End Sub

    Protected Sub gvPrice_PageIndexChanged(sender As Object, e As EventArgs)

    End Sub


    Protected Sub BtnDelRow(sender As Object, e As EventArgs)

        'javaScript = "<script type='text/javascript'>msgalert('ยังไม่เสร็จ');</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

        'javaScript = "alertWarning('ยังไม่เสร็จ');"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        'javaScript = "<script type='text/javascript'>msgalert('ยังไม่เสร็จ');</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

        Dim terminal As Label = TryCast(sender.FindControl("lblterminal"), Label)

        For Each row As DataRow In pricetable.Rows
            If terminal.Text = row.Item("terminal") Then
                row.Delete()
            End If
        Next row
        Session("pricetable") = pricetable
        BindData(pricetable)
    End Sub

    Protected Sub BtnAddRow(sender As Object, e As EventArgs)
        'pricetable.Rows.Add("ESSR2", "b5", "b7", "b10", "g91", "g95", "e20", "e85")
        'pricetable.Rows.Add("ESSR3", "b5", "b7", "b10", "g91", "g95", "e20", "e85")
        Dim cbo As DropDownList = TryCast(gvPrice.FooterRow.FindControl("cboTerminal"), DropDownList)
        Dim b5 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtB5"), TextBox)
        Dim b7 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtb7"), TextBox)
        Dim b10 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtb10"), TextBox)
        Dim g91 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtg91"), TextBox)
        Dim g95 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtg95"), TextBox)
        Dim e20 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txte20"), TextBox)
        Dim e85 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txte85"), TextBox)

        Try
            b5.Text = Double.Parse(b5.Text)
        Catch ex As Exception
            b5.Text = 0
        End Try
        Try
            b7.Text = Double.Parse(b7.Text)
        Catch ex As Exception
            b7.Text = 0
        End Try
        Try
            b10.Text = Double.Parse(b10.Text)
        Catch ex As Exception
            b10.Text = 0
        End Try
        Try
            g91.Text = Double.Parse(g91.Text)
        Catch ex As Exception
            g91.Text = 0
        End Try
        Try
            g95.Text = Double.Parse(g95.Text)
        Catch ex As Exception
            g95.Text = 0
        End Try
        Try
            e20.Text = Double.Parse(e20.Text)
        Catch ex As Exception
            e20.Text = 0
        End Try
        Try
            e85.Text = Double.Parse(e85.Text)
        Catch ex As Exception
            e85.Text = 0
        End Try

        'เช็คห้ามมีคลังซ้ำ
        If chkdup(cbo.SelectedItem.Text) Then
            'ซ้ำ
            javaScript = "<script type='text/javascript'>msgalert('คลังน้ำมันซ้ำกับที่มีอยู่แล้ว');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        Else
            If b5.Text = "0" And b7.Text = "0" And b10.Text = "0" _
            And g91.Text = "0" And g95.Text = "0" And e20.Text = "0" _
            And e85.Text = "0" Then
                'ไม่ได้คีย์อะไร
                javaScript = "<script type='text/javascript'>msgalert('กรุณากรอกราคาอย่างน้อย 1 ผลิตภัณฑ์');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            Else
                add_row(cbo.SelectedItem.Text, b5.Text, b7.Text, b10.Text, g91.Text, g95.Text, e20.Text, e85.Text)
            End If
        End If
        'เช็คห้ามว่างทุกราคา


    End Sub
    Private Function chkdup(terminal As String) As Boolean
        Dim result As Boolean = False
        Dim dr() As DataRow
        dr = pricetable.Select("terminal='" & terminal & "'")

        If dr.Length > 0 Then
            result = True 'ซ้ำ
        Else
            result = False 'ไม่ซ้ำ
        End If

        Return result
    End Function
    Private Sub add_row(terminal As String, b5 As String, b7 As String, b10 As String, g91 As String, g95 As String,
                        e20 As String, e85 As String)


        Try
            Dim R As DataRow = pricetable.NewRow
            R("terminal") = terminal
            R("b5") = b5
            R("b7") = b7
            R("b10") = b10
            R("g91") = g91
            R("g95") = g95
            R("e20") = e20
            R("e85") = e85

            pricetable.Rows.Add(R)

            Session("pricetable") = pricetable
            BindData(pricetable)
        Catch ex As Exception
            javaScript = "<script type='text/javascript'>msgalert('กรุณาตรวจสอบราคา');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

    End Sub
    Private Function SaveData() As Boolean
        Dim result As Boolean = True
        Dim pricedate As DateTime
        Dim wsobj As New wholesales
        Dim supply As String
        Dim g91, g95, e20, e85, b5, b7, b10 As Double
        Dim remark As String


        Try
            pricedate = DateTime.Parse(txtPriceDate.Text)
            remark = txtremark.Text
            For Each row As DataRow In pricetable.Rows
                If Not row.RowState = DataRowState.Deleted Then
                    supply = row.Item("terminal")
                    g91 = row.Item("g91")
                    g95 = row.Item("g95")
                    e20 = row.Item("e20")
                    e85 = row.Item("e85")
                    b5 = row.Item("b5")
                    b7 = row.Item("b7")
                    b10 = row.Item("b10")
                    result = wsobj.Wholesales_Price_Save(pricedate, supply, g91, g95, e20, e85, b5, b7, b10, remark, usercode)
                End If
            Next row

        Catch ex As Exception
            result = False
            Dim err As String = ex.Message
            err = err.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Return result
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validatesave() Then
            If SaveData() Then
                javaScript = "<script type='text/javascript'>msgalert('บันทึกเรียบร้อย ** ราคาจะมีผลเมื่อ Confirm แล้วเท่านั้น');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                lblstatus.Text = "Pendding"
                setbutton("Pendding")
            End If
        End If

    End Sub
    Private Function validatesave() As Boolean
        Dim result As Boolean = True

        If pricetable.Rows.Count = 0 Then
            result = False
            javaScript = "<script type='text/javascript'>msgalert('กรุณาเพิ่มข้อมูลราคา');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            GoTo endsub
        End If
        If String.IsNullOrEmpty(txtPriceDate.Text) Then
            result = False
            javaScript = "<script type='text/javascript'>msgalert('กรุณาระบุ วัน เวลา ที่ราคามีผล');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            GoTo endsub
        End If

endsub:
        Return result
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        pricetable.Rows.Clear()
        BindData(pricetable)

        txtPriceDate.Text = ""
        txtremark.Text = ""
        lblstatus.Text = ""
        setbutton("Pendding")

    End Sub
    Private Function ConfirmData() As Boolean
        Dim result As Boolean = True
        Dim pricedate As DateTime
        Dim wsobj As New wholesales
        Try
            pricedate = DateTime.Parse(txtPriceDate.Text)
            wsobj.Wholesales_Price_Confirm(pricedate, usercode)
        Catch ex As Exception
            result = False
            Dim err As String = ex.Message
            err = err.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Return result
    End Function
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If ConfirmData() Then
            lblstatus.Text = "Approve"
            setbutton("Approve")
            javaScript = "<script type='text/javascript'>msgalert('Confirm รายการนี้เรียบร้อย');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub
    Private Function CancelData() As Boolean
        Dim result As Boolean = True
        Dim pricedate As DateTime
        Dim wsobj As New wholesales
        Try
            pricedate = DateTime.Parse(txtPriceDate.Text)
            wsobj.Wholesales_Price_Cancel(pricedate, usercode)
        Catch ex As Exception
            result = False
            Dim err As String = ex.Message
            err = err.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Return result
    End Function
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If CancelData() Then
            lblstatus.Text = "Cancel"
            setbutton("Cancel")
            javaScript = "<script type='text/javascript'>msgalert('Cancel รายการนี้เรียบร้อย');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub
End Class