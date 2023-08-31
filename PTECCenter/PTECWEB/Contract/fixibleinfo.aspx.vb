

Imports DocumentFormat.OpenXml.Presentation

Public Class fixibleinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, contractno As String
    Public fixid As Double = 0
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
            fixid = Session("fixid")
        Else
            contractno = Request.QueryString("contractno")
            fixid = Request.QueryString("fixid")
            txtContractNo.Text = contractno
            SetCboPaymentType(cboPaymentType)

            Session("contractno") = contractno
            Session("fixid") = fixid

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not String.IsNullOrEmpty(fixid) Then
                FindData(fixid)
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
                'btnDel.Enabled = False

            Case "ยืนยัน(รออนุมัติ)", "ยกเลิก", "อนุมัติ", "Reject"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnSave.Enabled = False
                'btnNewAgree.Enabled = True
                btnNew.Enabled = False
                'btnDel.Enabled = False

        End Select

        'btnDel.Enabled = True

    End Sub
    Private Sub FindData(fixid As Double)
        Dim mytable As DataTable
        Dim objfix As New Fixible
        Try
            mytable = objfix.Find(fixid)
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
            txtid.Text = .Item("fixid")

            'cboPaymentType 
            cboPaymentType.SelectedIndex = cboPaymentType.Items.IndexOf(cboPaymentType.Items.FindByText(.Item("paymenttype")))
            If .Item("recuringtype") = "M" Then
                rdoMonth.Checked = True
            Else
                rdoYear.Checked = True
            End If
            txtFrequency.Text = .Item("frequency")
            'txtBegindate.Text = .Item("begindate")
            'txtEnddate.Text = .Item("enddate")
            'txtDueDate.Text = .Item("duedate")

            txtBegindate.Text = DateAdd(DateInterval.Year, 543, CDate(.Item("begindate")))
            txtEnddate.Text = DateAdd(DateInterval.Year, 543, CDate(.Item("enddate")))
            txtDueDate.Text = DateAdd(DateInterval.Year, 543, CDate(.Item("duedate")))

            txtAmount.Text = .Item("amount")

            fixid = .Item("fixid")
            Session("fixid") = .Item("fixid")


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
        fixid = 0
        Session("fixid") = 0
        txtBegindate.Text = Date.Now.ToString
        txtEnddate.Text = Date.Now.ToString
        txtDueDate.Text = Date.Now.ToString
        txtFrequency.Text = "0"
        cboPaymentType.SelectedIndex = -1
        rdoMonth.Checked = True
        SetButton("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim err, scriptKey, javaScript As String
        If validateData() Then
            Try
                fixid = Save()
                txtid.Text = fixid
                Session("fixid") = fixid

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
        Dim objfix As New Fixible

        Dim paymenttype, recuring As String
        Dim amount As Double
        Dim begindate, enddate, duedate As DateTime
        Dim frequency As Integer


        paymenttype = cboPaymentType.SelectedItem.Text
        amount = Double.Parse(txtAmount.Text)
        begindate = DateAdd(DateInterval.Year, -543, CDate(txtBegindate.Text))
        enddate = DateAdd(DateInterval.Year, -543, CDate(txtEnddate.Text))
        duedate = DateAdd(DateInterval.Year, -543, CDate(txtDueDate.Text))

        frequency = Integer.Parse(txtFrequency.Text)
        If rdoMonth.Checked = True Then
            recuring = "M"
        Else
            recuring = "Y"
        End If

        result = objfix.Save(contractno, fixid, paymenttype, recuring, frequency, begindate, enddate, duedate, amount, usercode)

        txtid.Text = result.ToString
        Session("fixid") = result
        Return result
    End Function

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        Dim projectno As String = Session("projectno")
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Dim objfix As New Fixible

            If objfix.fixibleDel(Session("fixid")) = False Then
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class