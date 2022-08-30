

Public Class onetimeinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, contractno, projectno As String
    Public onetimeid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New titlename

        usercode = Session("usercode")
        username = Session("username")

        txtDueDate.Attributes.Add("readonly", "readonly")



        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            contractno = Session("contractno")
            projectno = Session("projectno")
            onetimeid = Session("onetimeid")
        Else
            contractno = Request.QueryString("contractno")
            Try
                onetimeid = Request.QueryString("onetimeid")
            Catch ex As Exception
                onetimeid = 0
            End Try
            txtContractNo.Text = contractno

            Session("contractno") = contractno
            Session("onetimeid") = onetimeid

            SetCboPaymentType(cboPayment)

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not String.IsNullOrEmpty(onetimeid) Then
                FindData(onetimeid)
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
    Private Sub FindData(onetimeid As Double)
        Dim mytable As DataTable
        Dim objonetime As New OneTimePayment
        Try
            mytable = objonetime.Find(onetimeid)
            ShowData(mytable)
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub ShowData(mytable As DataTable)
        With mytable.Rows(0)
            txtContractNo.Text = contractno
            txtid.Text = .Item("onetimeid")
            txtAmount.Text = .Item("amount")
            txtDueDate.Text = .Item("duedate")
            txtRemark.Text = .Item("remark")
            cboPayment.SelectedIndex = cboPayment.Items.IndexOf(cboPayment.Items.FindByText(.Item("paymenttype")))
            If .Item("clientpaid") = True Then
                rdoClient.Checked = True
            Else
                rdoCompany.Checked = True
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
        txtAmount.Text = "0"
        cboPayment.SelectedIndex = -1
        rdoClient.Checked = True
        txtDueDate.Text = Date.Now.ToString
        SetButton("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim err, scriptKey, javaScript As String
        If validateData() Then
            Try
                onetimeid = Save()
                txtid.Text = onetimeid
                Session("onetimeid") = onetimeid

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
        Dim objonetime As New OneTimePayment
        Dim paymenttype As String
        Dim duedate As DateTime
        Dim amount As Double = Double.Parse(txtAmount.Text)
        Dim clientpaid As Integer
        If rdoClient.Checked = True Then
            clientpaid = 1
        Else
            clientpaid = 0
        End If
        duedate = DateTime.Parse(txtDueDate.Text)

        paymenttype = cboPayment.SelectedItem.Text

        result = objonetime.Save(contractno, onetimeid, paymenttype, duedate, amount, clientpaid, txtRemark.Text, usercode)

        txtid.Text = result.ToString
        Session("onetimeid") = result
        Return result
    End Function

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub
End Class