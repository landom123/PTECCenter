Public Class OilPriceUpdate
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public pricetable As DataTable = createpricetable()
    Public usercode, username

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm


        usercode = Session("usercode")
        username = Session("username")
        txtCloseDate.Attributes.Add("readonly", "readonly")

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
            cboShift.Items.Add("1")
            cboShift.Items.Add("2")
            cboShift.Items.Add("3")
            cboShift.SelectedIndex = 0


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'If Not (Session("pricetable") Is Nothing) Then
            '    pricetable = Session("pricetable")
            '    BindData()
            'End If
        End If

    End Sub


    Private Function createpricetable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("product_name", GetType(String))
        dt.Columns.Add("product_code", GetType(String))
        dt.Columns.Add("mainpriceid", GetType(Double))
        dt.Columns.Add("effectivedate", GetType(Date))
        dt.Columns.Add("lastprice", GetType(Double))
        dt.Columns.Add("movevalue", GetType(Double))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(Date))
        dt.Columns.Add("adjust", GetType(Double))
        dt.Columns.Add("actual", GetType(Double))
        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = pricetable
        gvData.DataBind()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim selectdate As String
        Dim objprice As New Price
        selectdate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        pricetable = objprice.Oil_Day_Price_Select(selectdate)
        Session("pricetable") = pricetable
        BindData()
    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        'Dim begindate As DateTime = DateTime.Parse(txtCloseDate.Text)
        'If begindate.Date >= Now.Date Then
        Save()
        'Else
        '    Dim scriptKey As String = "UniqueKeyForThisScript"
        '    Dim javaScript As String
        '    javaScript = "<script type='text/javascript'>msgalert('ไม่สามารถบันทึกข้อมูลย้อนหลังได้ กรุณาตรวจสอบวันที่');</script>"
        '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        'End If


    End Sub

    Private Sub Save()

        Dim result As Boolean = True
        'Dim code As String
        Dim price As Double
        Dim adjust As Double
        Dim actual As Double
        Dim effectivedate As String = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        Dim effectivetime As String
        Dim err As Integer
        Dim errmsg As String = ""
        Dim chkerr As Integer = 0
        effectivetime = "05:00:00"
        'For i = 0 To gvData.Rows.Count - 1
        '    test = gvData.Rows(i).FindControl("txtadjust")
        '    lbltest.Text = test.Text
        'Next
        Dim objprice As New Price
        For Each item As GridViewRow In gvData.Rows
            err = 0
            If item.RowType = DataControlRowType.DataRow Then
                Dim lblname As Label = CType(item.FindControl("lblname"), Label)
                Dim lblprice As Label = CType(item.FindControl("lblprice"), Label)
                Dim txtadjust As TextBox = CType(item.FindControl("txtadjust"), TextBox)
                Dim lblactual As Label = CType(item.FindControl("lblactual"), Label)
                'Dim box As TextBox = CType(item.FindControl("txtadjust"), TextBox)
                'Label.Visible = True
                'box.Visible = False
                Try
                    price = Double.Parse(lblprice.Text)
                Catch ex As Exception
                    price = 0
                    err = 1
                    chkerr = 1
                    errmsg = "Product : " & lblname.Text & " ราคาล่าสุด = 0 กรุณาตรวจสอบ"
                End Try
                Try
                    adjust = Double.Parse(txtadjust.Text)
                Catch ex As Exception
                    adjust = 0
                    err = 1
                    chkerr = 1
                    errmsg = "Product : " & lblname.Text & " ราคาที่ปรับปรุง = 0 กรุณาตรวจสอบ"
                End Try
                Try
                    actual = price + adjust
                Catch ex As Exception
                    actual = 0
                    err = 1
                    chkerr = 1
                End Try

                'save data


                result = objprice.Oil_Day_Price_Save(lblname.Text, price, adjust, actual, usercode, effectivedate, effectivetime)

            End If
        Next

        If chkerr > 0 Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('เกิดข้อผิดพลาดขณะบันทึก บางส่วน หรือทั้งหมด กรุณาตรวจสอบ');", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('save complete');", True)
        End If
    End Sub

    Protected Sub txtadjust_TextChanged(sender As Object, e As EventArgs)

        Dim adjust As String = CType(sender, TextBox).Text.ToString()

        Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)

        Dim price As String = CType(gr.FindControl("lblprice"), Label).Text.ToString()

        Dim actual As Label = CType(gr.FindControl("lblactual"), Label)
        actual.Text = Double.Parse(price) + Double.Parse(adjust)

    End Sub

    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        'Dim txtadjust As TextBox = CType(e.Row.FindControl("txtadjust"), TextBox)
        'If Not IsNothing(txtadjust) Then
        '    txtadjust.Text = e.Row.DataItem("adjust")
        'End If
        '*** vbs_whtaxid ***'
        'Dim txtactual As TextBox = CType(e.Row.FindControl("txtactual"), TextBox)
        'If Not IsNothing(txtactual) Then
        '    txtactual.Text = e.Row.DataItem("actual")
        'End If

    End Sub
End Class