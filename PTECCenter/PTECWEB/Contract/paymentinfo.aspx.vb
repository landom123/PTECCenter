

Imports DocumentFormat.OpenXml.Wordprocessing

Public Class paymentinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, contractno, projectno As String
    Public paymentid As Double = 0

    Dim objPay As New clsPayment
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New TitleName

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
            projectno = Session("projectno")
            paymentid = Session("paymentid")

            'If loadCustContract(Session("paymentid")) = False Then
            '    Exit Sub
            'End If

        Else
            contractno = Request.QueryString("contractno")
            projectno = Request.QueryString("projectno")
            paymentid = Request.QueryString("paymentid")

            txtContractNo.Text = contractno

            Session("contractno") = contractno
            Session("paymentid") = paymentid

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If loadBank() = False Then
                Exit Sub
            End If

            'If loadCustContract(Session("paymentid")) = False Then
            '    Exit Sub
            'End If

            If loadCustContract(paymentid) = False Then
                Exit Sub
            End If


            If Not String.IsNullOrEmpty(paymentid) Then
                FindData(paymentid)
            Else
                SetButton("NEW")
            End If

        End If

    End Sub

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
    Private Sub FindData(paymentid As Double)
        Dim mytable As DataTable
        Dim objpayment As New PaymentContract
        Try
            mytable = objpayment.Find(paymentid)
            ShowData(mytable)
        Catch ex As Exception
            'Dim err As String = ex.Message.ToString.Replace("'", "")
            'Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            txtContractNo.Text = contractno
            txtid.Text = .Item("paymentid")
            'txtBankCode.Text = .Item("bankcode")
            cboBank.Text = .Item("bankcode")
            txtBankbranchcode.Text = .Item("bankbranchcode")
            txtBankbranchname.Text = .Item("bankbranchname")
            txtAccountNo.Text = .Item("accountno")
            txtAccountName.Text = .Item("accountname")
            txtACCode.Text = .Item("ACCode")
            cboPayCust.SelectedValue = IIf(IsDBNull(.Item("PayCust")), "", .Item("PayCust"))
            cboTaxCust.SelectedValue = IIf(IsDBNull(.Item("TaxCust")), "", .Item("TaxCust"))

            paymentid = .Item("paymentid")
            Session("paymentid") = .Item("paymentid")

            If .Item("Active") = "Y" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If

            If .Item("paidtype") = "T" Then
                rdoTrans.Checked = True
            Else
                rdoCheque.Checked = True
            End If
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
        paymentid = 0
        Session("paymentid") = 0
        'txtBankCode.Text = ""
        cboBank.SelectedIndex = 0
        txtBankbranchcode.Text = ""
        txtBankbranchname.Text = ""
        txtAccountNo.Text = ""
        txtAccountName.Text = ""
        rdoTrans.Checked = True
        SetButton("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim err, scriptKey, javaScript As String
        If validateData() Then
            Try
                paymentid = Save()
                txtid.Text = paymentid
                Session("paymentid") = paymentid

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
    Private Function validateData() As Boolean
        Dim result As Boolean = True
        Dim err, scriptKey, javaScript As String

        'If String.IsNullOrEmpty(txtName.Text) Then
        '    result = False
        '    err = "กรุณาระบุชื่อ นามสกุล คู่สัญญา"
        'End If

        If result = False Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Function Save() As Double
        Dim result As Double
        Dim objpayment As New PaymentContract
        Dim paidtype As String
        Dim bankcode, bankbranchcode, bankbranchname, accountno, accountname, sActive, sPayCust, sTaxCust, sACCode As String

        'bankcode = txtBankCode.Text
        bankcode = cboBank.Text
        bankbranchcode = txtBankbranchcode.Text
        bankbranchname = txtBankbranchname.Text
        accountno = txtAccountNo.Text
        accountname = txtAccountName.Text
        sPayCust = cboPayCust.SelectedValue
        sTaxCust = cboTaxCust.SelectedValue
        sACCode = txtACCode.Text

        If rdoTrans.Checked = True Then
            paidtype = "T"
        Else
            paidtype = "C"
        End If

        If chkActive.Checked = True Then
            sActive = "Y"
        Else
            sActive = "N"
        End If

        result = objpayment.Save(contractno, paymentid, paidtype, bankcode, bankbranchcode,
                                 bankbranchname, accountno, accountname, usercode, sActive, sPayCust, sTaxCust, sACCode)

        txtid.Text = result.ToString
        Session("paymentid") = result
        Return result
    End Function

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub

    Private Function loadBank() As Boolean
        Try
            Dim objReq As New clsRequestContract
            Dim dt As New DataTable

            dt = objPay.loadBank

            cboBank.DataSource = dt
            cboBank.DataValueField = "ID"
            cboBank.DataTextField = "BankName"
            cboBank.DataBind()

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err '"alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function

    Private Function loadCustContract(iPayID As Integer) As Boolean
        Try
            Dim objReq As New clsRequestContract
            Dim dt, dt2 As New DataTable

            dt = objPay.loadCustContract(iPayID)

            cboPayCust.DataSource = dt
            cboPayCust.DataValueField = "Name"
            cboPayCust.DataTextField = "Name"
            cboPayCust.DataBind()



            dt2 = objPay.loadCustContract(iPayID)

            cboTaxCust.DataSource = dt2
            cboTaxCust.DataValueField = "Name"
            cboTaxCust.DataTextField = "Name"
            cboTaxCust.DataBind()


            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err '"alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try
    End Function


    Private Sub cboBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedIndexChanged
        Try


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err '"alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class