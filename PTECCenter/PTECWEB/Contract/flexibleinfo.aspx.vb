

Public Class flexibleinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, contractno As String
    Public flexdetailtable As DataTable = createflexdetailtable()
    Public flexid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New titlename

        usercode = Session("usercode")
        username = Session("username")


        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            contractno = Session("contractno")
            flexid = Session("flexid")
        Else
            contractno = Request.QueryString("contractno")
            flexid = Request.QueryString("flexid")

            SetCboPaymentType(cboPaymentType)

            Session("contractno") = contractno
            Session("flexid") = flexid
            txtContractNo.Text = contractno
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not String.IsNullOrEmpty(flexid) Then
                FindData(flexid)
            Else
                SetButton("NEW")
            End If

        End If

    End Sub

    Private Function createflexdetailtable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("flexdetailid", GetType(Double))
        dt.Columns.Add("beginvolume", GetType(Double))
        dt.Columns.Add("endvolume", GetType(Double))
        dt.Columns.Add("minwarrantyamount", GetType(Double))
        dt.Columns.Add("rate", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function
    Public Sub SetButton(chkstatus As String)

        Select Case chkstatus
            Case = "บันทึก", "SAVE", "New", "ส่งกลับแก้ไข"
                lblStatus.Text = chkstatus
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkGreen
                btnSave.Enabled = True
                'btnNewAgree.Enabled = True
                btnNew.Enabled = True
                btnDel.Enabled = False

            Case "ยืนยัน(รออนุมัติ)", "ยกเลิก", "อนุมัติ", "Reject"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnSave.Enabled = False
                'btnNewAgree.Enabled = True
                btnNew.Enabled = False
                btnDel.Enabled = False

        End Select
    End Sub
    Private Sub FindData(flexid As Double)
        Dim mydataset As DataSet

        Dim objflex As New Flexible
        Try
            mydataset = objflex.Find(flexid)
            ShowData(mydataset.Tables(0))
            flexdetailtable = mydataset.Tables(1)
            Session("flexdetail") = flexdetailtable
            BindData()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub BindData()
        gvFlexDetail.DataSource = flexdetailtable
        gvFlexDetail.DataBind()
    End Sub
    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            txtContractNo.Text = contractno
            txtid.Text = .Item("flexid")

            'cboPaymentType 
            cboPaymentType.SelectedIndex = cboPaymentType.Items.IndexOf(cboPaymentType.Items.FindByText(.Item("paymenttype")))
            If .Item("recuringtype") = "M" Then
                rdoMonth.Checked = True
            Else
                rdoYear.Checked = True
            End If
            txtFrequency.Text = .Item("frequency")
            txtBegindate.Text = .Item("begindate")
            txtEnddate.Text = .Item("enddate")
            txtDueDate.Text = .Item("duedate")
            If .Item("calcmaxorrank") = True Then
                rdoMax.Checked = True
            Else
                rdoRank.Checked = True
            End If
            flexid = .Item("flexid")
            Session("flexid") = .Item("flexid")


            SetButton(.Item("status"))
        End With
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub

    Private Sub SetCboPaymentType(obj As Object)
        Dim payment As New Payment

        obj.DataSource = payment.PaymentType_List()
        obj.DataValueField = "paymenttypeid"
        obj.DataTextField = "paymenttype"
        obj.DataBind()
    End Sub

    Private Sub Clear()
        txtid.Text = ""
        flexid = 0
        Session("flexid") = 0
        txtBegindate.Text = Date.Now.ToString
        txtEnddate.Text = Date.Now.ToString
        txtDueDate.Text = Date.Now.ToString
        flexdetailtable.Rows.Clear()
        txtFrequency.Text = "0"
        cboPaymentType.SelectedIndex = -1
        rdoMonth.Checked = True

        flexdetailtable.Rows.Clear()
        Session("flexdetail") = flexdetailtable
        BindData()


        SetButton("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim err, scriptKey, javaScript As String
        If validateData() Then
            Try
                flexid = Save()
                txtid.Text = flexid
                Session("flexid") = flexid

            Catch ex As Exception
                err = ex.Message.ToString.Replace("'", "")
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

            If String.IsNullOrEmpty(err) Then
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        End If
    End Sub

    Private Function Save() As Double
        Dim result As Double
        Dim objflex As New Flexible

        Dim paymenttype, recuring As String
        Dim amount As Double
        Dim begindate, enddate, duedate As DateTime
        Dim frequency As Integer
        Dim calcmaxorrank As Integer


        paymenttype = cboPaymentType.SelectedItem.Text

        begindate = DateTime.Parse(txtBegindate.Text)
        enddate = DateTime.Parse(txtEnddate.Text)
        duedate = DateTime.Parse(txtDueDate.Text)
        frequency = Integer.Parse(txtFrequency.Text)
        If rdoMonth.Checked = True Then
            recuring = "M"
        Else
            recuring = "Y"
        End If
        If rdoMax.Checked = True Then
            calcmaxorrank = 1
        Else
            calcmaxorrank = 0
        End If

        result = objflex.Save(contractno, flexid, paymenttype, recuring, frequency, begindate, enddate, duedate, calcmaxorrank, usercode)

        txtid.Text = result.ToString
        Session("flexid") = result
        Return result
    End Function

    Private Function validateDataDetail() As Boolean
        Dim result As Boolean = True
        Dim scriptKey, javaScript As String


        If String.IsNullOrEmpty(txtid.Text) Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('กรุณากดบันทึกก่อนให้ได้ ID')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            result = False
            GoTo end_sub
        End If

        If String.IsNullOrEmpty(txtWarranty.Text) Then
            txtWarranty.Text = "0"
        End If


        If String.IsNullOrEmpty(txtRate.Text) Then
            txtRate.Text = "0"
        End If


        If String.IsNullOrEmpty(txtWarranty.Text) And String.IsNullOrEmpty(txtRate.Text) Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('กรุณาระบุอัตราผลตอบแทน หรือเงินประกัน อย่างน้อยอย่างใดอย่างหนึ่ง')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            result = False
            GoTo end_sub
        End If

        If txtWarranty.Text = "0" And txtRate.Text = "0" Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('กรุณาระบุอัตราผลตอบแทน หรือเงินประกัน อย่างน้อยอย่างใดอย่างหนึ่ง')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            result = False
            GoTo end_sub
        End If

        If String.IsNullOrEmpty(txtRate.Text) Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('กรุณาระบุอัตราผลตอบแทน')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            result = False
            GoTo end_sub
        End If

end_sub:
        Return result
    End Function
    Private Function validateData() As Boolean
        Return True
    End Function
    Private Sub btnAddDetail_Click(sender As Object, e As EventArgs) Handles btnAddDetail.Click
        'add row flexdetail table

        If validateDataDetail() Then
            SaveDetail()
        End If

    End Sub

    Private Sub SaveDetail()
        Dim err, scriptKey, javaScript As String
        Dim objflex As New Flexible
        Dim flexdetailid, beginvolume, endvolume, warranty, rate As Double
        flexdetailid = 0
        beginvolume = Double.Parse(txtBeginvolume.Text)
        endvolume = Double.Parse(txtEndvolume.Text)
        warranty = Double.Parse(txtWarranty.Text)
        rate = Double.Parse(txtRate.Text)
        Try
            objflex.SaveDetail(flexid, flexdetailid, beginvolume, endvolume, warranty, rate, usercode)
            FindData(flexid)
        Catch ex As Exception
            err = ex.Message.ToString.Replace("'", "")
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        Dim projectno As String = Session("projectno")
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub
End Class