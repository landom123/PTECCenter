

Public Class requestcontract
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, assetsno, contractno, projectno As String
    Public clientid As Double = 0

    Dim dtBranch As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objTitleName As New TitleName

        usercode = Session("usercode")
        username = Session("username")

        'txtBirthday.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            contractno = Session("contractno")
            assetsno = Session("assetsno")

            'Dim objprj As New Project
            'dtBranch = objprj.loadBranch
            'cboBranch.DataSource = dtBranch
            'cboBranch.DataValueField = "branch_code"
            'cboBranch.DataTextField = "branch_name"
            'cboBranch.DataBind()


        Else
            contractno = Request.QueryString("contractno")
            assetsno = Request.QueryString("assetsno")


            Session("contractno") = contractno
            Session("assetsno") = assetsno

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not String.IsNullOrEmpty(assetsno) Then
                FindData(assetsno)
            Else
                txtdocuno.Text = contractno
                SetButton("NEW")
            End If

            Dim objprj As New Project
            dtBranch = objprj.loadBranch
            cboBranch.DataSource = dtBranch
            cboBranch.DataValueField = "branch_code"
            cboBranch.DataTextField = "branch_name"
            cboBranch.DataBind()

            SetCboContractType(cboContractType)

            cboSex.Items.Add("Male")
            cboSex.Items.Add("Female")

            txtdocuno.Text = Request.QueryString("agreeno")

        End If


    End Sub

    Private Sub SetCboContractType(obj As Object)
        Dim ag As New Contract

        obj.DataSource = ag.ContractTypeCbo()
        obj.DataValueField = "agtypeid"
        obj.DataTextField = "agtype"
        obj.DataBind()
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
    Private Sub FindData(assetsno As String)
        Dim mytable As DataTable
        Dim objassets As New ContractAssets
        Try
            mytable = objassets.Find(assetsno)
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
            'If .Item("fullarea") = True Then
            '    rdoFull.Checked = True
            'Else
            '    rdoPart.Checked = True
            'End If
            txtdocuno.Text = contractno

            'txtLandno.Text = .Item("landno")
            'txtSurveyNo.Text = .Item("surveyno")
            'txtSubDistrict.Text = .Item("subdistrict")
            'txtDistrict.Text = .Item("district")
            'txtProvince.Text = .Item("province")
            'txtRai.Text = .Item("area_rai")
            'txtNgan.Text = .Item("area_ngan")
            'txtWa.Text = .Item("area_wa")
            'txtGPS.Text = .Item("gps")

            SetButton(.Item("status"))

        End With
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub
    Private Sub Clear()

        txtName.Text = ""
        txtCardID.Text = ""
        cboSex.SelectedIndex = 0
        txtCompany.Text = ""
        txtMobile.Text = ""
        txtTel.Text = ""
        txtEmail.Text = ""
        txtLine.Text = ""
        txtAddress.Text = ""
        txtSubDistrict.Text = ""
        txtDistrict.Text = ""
        txtProvince.Text = ""
        txtPostcode.Text = ""

        SetButton("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Dim assetsno As String
        Dim err, scriptKey, javaScript As String
        err = ""
        If validateData() Then
            Try
                assetsno = Save()
                'txtClientNo.Text = clientno
                Session("assetsno") = assetsno

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
    Private Function Save() As String
        Dim result As String = Nothing
        Dim objassets As New ContractAssets

        Dim subdistrict, district, province As String

        subdistrict = txtSubDistrict.Text
        district = txtDistrict.Text
        province = txtProvince.Text

        Return result
    End Function

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        projectno = Session("projectno")
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub
End Class