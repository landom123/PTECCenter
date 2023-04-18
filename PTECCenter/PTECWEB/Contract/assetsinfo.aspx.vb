

Imports System.Windows

Public Class assetsinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public usercode, username, assetsno, contractno, projectno As String
    Public clientid As Double = 0

    Public Shared iBuconType As Integer
    Public Shared BuconTypeName As String

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
        Else
            contractno = Request.QueryString("contractno")
            assetsno = Request.QueryString("assetsno")


            SetCboAssetType(cboAssetType)

            txtContractType.Text = assetsinfo.BuconTypeName

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
                txtContractNo.Text = contractno
                SetButton("NEW")
            End If

        End If


        'If iBuconType = 2 Then
        '    rdoplanBlank.Visible = True
        'Else
        '    rdoplanBlank.Visible = False
        'End If


    End Sub

    Private Sub SetCboAssetType(obj As Object)
        Dim asset As New ContractAssets

        obj.DataSource = asset.AssetsType_Cbo
        obj.DataValueField = "assetstypeid"
        obj.DataTextField = "assetstype"
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
            If .Item("fullarea") = True Then
                rdoFull.Checked = True
            Else
                rdoPart.Checked = True
            End If
            txtContractNo.Text = contractno
            txtAssetsNo.Text = .Item("assetsno")
            txtLandno.Text = .Item("landno")
            txtSurveyNo.Text = .Item("surveyno")
            txtSubDistrict.Text = .Item("subdistrict")
            txtDistrict.Text = .Item("district")
            txtProvince.Text = .Item("province")
            txtRai.Text = .Item("area_rai")
            txtNgan.Text = .Item("area_ngan")
            txtWa.Text = .Item("area_wa")
            txtGPS.Text = .Item("gps")

            txtbuNo.Text = .Item("Address")
            txtbuSubDistrict.Text = .Item("Subdistrict1")
            txtbuDistrict.Text = .Item("District1")
            txtbuProvince.Text = .Item("Province1")

            cboAssetType.SelectedIndex = cboAssetType.Items.IndexOf(cboAssetType.Items.FindByText(.Item("assetstype")))
            SetButton(.Item("status"))

        End With
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub
    Private Sub Clear()
        cboAssetType.SelectedIndex = -1
        txtAssetsNo.Text = ""
        txtLandno.Text = ""
        txtSurveyNo.Text = ""
        txtSubDistrict.Text = ""
        txtDistrict.Text = ""
        txtProvince.Text = ""
        txtRai.Text = ""
        txtNgan.Text = ""
        txtWa.Text = ""
        txtGPS.Text = ""
        rdoFull.Checked = True
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
        Dim result As String
        Dim objassets As New ContractAssets
        Dim assetstype As Integer = cboAssetType.SelectedItem.Value
        Dim landno, surveyno, subdistrict, district, province, gps As String
        Dim rai, ngan, fullarea As Integer
        Dim wa As Double

        Dim RentType As Integer
        Dim addrerss, subdistrict1, district1, province1 As String

        landno = txtLandno.Text
        surveyno = txtSurveyNo.Text
        subdistrict = txtSubDistrict.Text
        district = txtDistrict.Text
        province = txtProvince.Text
        gps = txtGPS.Text
        rai = Integer.Parse(txtRai.Text)
        ngan = Integer.Parse(txtNgan.Text)
        wa = Double.Parse(txtWa.Text)


        If rdoFull.Checked = True Then
            RentType = 0
        ElseIf rdoPart.Checked = True Then
            RentType = 1
        End If

        addrerss = txtbuNo.Text
        subdistrict1 = txtbuSubDistrict.Text
        district1 = txtbuDistrict.Text
        province1 = txtbuProvince.Text

        If rdoFull.Checked = True Then
            fullarea = 1
        Else
            fullarea = 0
        End If

        result = objassets.Save(contractno, assetsno, assetstype, landno, surveyno, subdistrict,
                                district, province, rai, ngan, wa, gps, fullarea, usercode, RentType, addrerss, subdistrict1, district1, province1)

        Return result
    End Function

    Private Sub BtnContract_Click(sender As Object, e As EventArgs) Handles BtnContract.Click
        projectno = Session("projectno")
        Response.Redirect("contractinfo.aspx?agreeno=" & contractno & "&projectno=" & projectno)
    End Sub


End Class