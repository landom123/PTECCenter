

Public Class projectinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Public agreetable As DataTable = createagreetable()

    Public usercode, username, projectno As String
    Public projectid As Double = 0
    Public salevolume As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        'txtProjectDate.Attributes.Add("readonly", "readonly")
        'txtAgreeDate.Attributes.Add("readonly", "readonly")
        txtprojectno.Attributes.Add("readonly", "readonly")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            projectno = Request.QueryString("projectno")
            txtprojectno.Text = projectno
            If Not String.IsNullOrEmpty(projectno) Then
                Find(projectno)
            End If
            If Not (Session("agree") Is Nothing) Then

                    agreetable = Session("agree")
                    BindData()
                End If

            End If


    End Sub
    Private Sub Find(projectno As String)
        Dim objprj As New Project
        Dim mydataset As DataSet
        Dim projecttable As DataTable

        Try
            mydataset = objprj.Find(projectno)
            projecttable = mydataset.Tables(0)
            agreetable = mydataset.Tables(1)
            Session("agree") = agreetable
            BindData()
            ViewProject(projecttable)
        Catch ex As Exception
            'show error
            Dim err As String
            err = Strings.Replace(ex.Message, "'", " ")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('001 : " & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        End Try

    End Sub
    Private Function CreateAgreeTable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("agid", GetType(Double))
        dt.Columns.Add("agno", GetType(String))
        dt.Columns.Add("lawcontractno", GetType(String))
        dt.Columns.Add("agdate", GetType(DateTime))
        dt.Columns.Add("agtype", GetType(String))
        dt.Columns.Add("agstatus", GetType(String))
        dt.Columns.Add("approveby", GetType(String))
        dt.Columns.Add("approvedate", GetType(DateTime))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function
    Public Sub BindData()
        gvData.DataSource = agreetable
        gvData.DataBind()

    End Sub
    Public Sub ViewProject(mydatatable As DataTable)
        With mydatatable
            If .Rows.Count = 0 Then
                'show msg nodata
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('ไม่พบข้อมูล')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Else
                txtprojectno.Text = .Rows(0).Item("projectno")
                txtbranch.Text = .Rows(0).Item("branch")
                txtRemark.Text = .Rows(0).Item("remark")
                txtSaleVolume.Text = Double.Parse(.Rows(0).Item("EstSaleVolume")).ToString("N")
                SetButton(.Rows(0).Item("status"))
            End If

        End With
    End Sub
    Public Sub SetButton(chkstatus As String)


        Select Case chkstatus
            Case = "บันทึก", "SAVE", "New"
                lblStatus.Text = "บันทึก"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnSave.Enabled = True
                btnNewAgree.Enabled = True
                btnNewProject.Enabled = True
                btnApprove.Enabled = False
                btnChange.Enabled = False
                btnConfirm.Enabled = True
                btnCancel.Enabled = True
            Case = "ยืนยัน (รออนุมัติ)", "CONFIRM"
                lblStatus.Text = "ยืนยัน (รออนุมัติ)"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkGreen
                btnSave.Enabled = False
                btnNewAgree.Enabled = False
                btnNewProject.Enabled = True
                btnApprove.Enabled = True
                btnChange.Enabled = True
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
            Case = "ยกเลิก", "CANCEL"
                lblStatus.Text = "ยกเลิก"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkRed
                btnSave.Enabled = False
                btnNewAgree.Enabled = False
                btnNewProject.Enabled = True
                btnApprove.Enabled = False
                btnChange.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
            Case = "อนุมัติ", "APPROVE"
                lblStatus.Text = "อนุมัติ"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkRed
                btnSave.Enabled = False
                btnNewAgree.Enabled = False
                btnNewProject.Enabled = True
                btnApprove.Enabled = False
                btnChange.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
            Case = "ส่งกลับแก้ไข", "CHANGE"
                lblStatus.Text = "ส่งกลับแก้ไข"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkRed
                btnSave.Enabled = True
                btnNewAgree.Enabled = True
                btnNewProject.Enabled = True
                btnApprove.Enabled = False
                btnChange.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
        End Select
    End Sub
    Private Sub btnNewProject_Click(sender As Object, e As EventArgs) Handles btnNewProject.Click
        SetButton("New")
        agreetable.Rows.Clear()
        BindData()
        txtprojectno.Text = ""
        txtRemark.Text = ""
        txtbranch.Text = ""
        projectno = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateData() Then
            SaveData()
        End If
    End Sub
    Private Function ValidateData() As Boolean
        Dim result As Boolean = True
        Dim scriptKey As String
        Dim javaScript As String



        If String.IsNullOrEmpty(txtbranch.Text) Then
            result = False
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('กรุณาระบุข้อมูลสาขา')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Try
            salevolume = Integer.Parse(Double.Parse(txtSaleVolume.Text.Replace(",", "")))
        Catch ex As Exception
            salevolume = 0
        Finally
            If salevolume = 0 Then
                result = False
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('กรุณาระบุ ประมาณการยอดขาย')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If
        End Try


        Return result
    End Function


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub

    Private Sub btnCofirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ConfirmData()
    End Sub
    Private Sub ApproveData()
        Dim scriptKey As String
        Dim javaScript As String
        Dim chkerr As Integer = 0

        'Dim remark As String = txtRemark.Text
        'Dim branch As String = txtbranch.Text
        Dim objprj As New Project
        Dim mytable As DataTable
        Try
            mytable = objprj.Save(txtprojectno.Text, "", "", 0, "APPROVE", usercode)
            SetButton("APPROVE")
        Catch ex As Exception
            Dim err As String = Strings.Replace(ex.Message, "'", "")
            chkerr = 100
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('เกิดข้อผิดพลาด'" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        If chkerr = 0 Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('ยืนยันรายการเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub
    Private Sub RequestChangeData()
        Dim scriptKey As String
        Dim javaScript As String
        Dim chkerr As Integer = 0

        'Dim remark As String = txtRemark.Text
        'Dim branch As String = txtbranch.Text
        Dim objprj As New Project
        Dim mytable As DataTable
        Try
            mytable = objprj.Save(txtprojectno.Text, "", "", 0, "CHANGE", usercode)
            SetButton("CHANGE")
        Catch ex As Exception
            Dim err As String = Strings.Replace(ex.Message, "'", "")
            chkerr = 100
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('เกิดข้อผิดพลาด'" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        If chkerr = 0 Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('ยืนยันรายการเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub
    Private Sub CancelData()
        Dim scriptKey As String
        Dim javaScript As String
        Dim chkerr As Integer = 0

        'Dim remark As String = txtRemark.Text
        'Dim branch As String = txtbranch.Text
        Dim objprj As New Project
        Dim mytable As DataTable
        Try
            mytable = objprj.Save(txtprojectno.Text, "", "", 0, "CANCEL", usercode)
            SetButton("CANCEL")
        Catch ex As Exception
            Dim err As String = Strings.Replace(ex.Message, "'", "")
            chkerr = 100
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('เกิดข้อผิดพลาด'" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        If chkerr = 0 Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('ยืนยันรายการเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub
    Private Sub ConfirmData()
        Dim scriptKey As String
        Dim javaScript As String
        Dim chkerr As Integer = 0

        'Dim remark As String = txtRemark.Text
        'Dim branch As String = txtbranch.Text
        Dim objprj As New Project
        Dim mytable As DataTable
        Try
            mytable = objprj.Save(txtprojectno.Text, "", "", 0, "CONFIRM", usercode)
            SetButton("CONFIRM")
        Catch ex As Exception
            Dim err As String = Strings.Replace(ex.Message, "'", "")
            chkerr = 100
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('เกิดข้อผิดพลาด'" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        If chkerr = 0 Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('ยืนยันรายการเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub

    Private Sub SaveData()
        Dim scriptKey As String
        Dim javaScript As String
        Dim chkerr As Integer = 0

        Dim remark As String = txtRemark.Text
        Dim branch As String = txtbranch.Text
        Dim objprj As New Project
        Dim mytable As DataTable
        Try
            mytable = objprj.Save(txtprojectno.Text, txtbranch.Text, txtRemark.Text, salevolume, "SAVE", usercode)
            txtprojectno.Text = mytable.Rows(0).Item("projectno")
            SetButton("SAVE")
        Catch ex As Exception
            Dim err As String = Strings.Replace(ex.Message, "'", "")
            chkerr = 100
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('เกิดข้อผิดพลาด'" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        If chkerr = 0 Then
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        RequestChangeData()
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        ApproveData()
    End Sub

    Private Sub btnNewAgree_Click(sender As Object, e As EventArgs) Handles btnNewAgree.Click
        projectno = txtprojectno.Text
        'Response.Redirect("contractinfo.aspx?projectno=" & projectno & "&agreeno='' " & "&branch=" & txtbranch.Text & "")

        Response.Redirect("contractinfo.aspx?projectno=" & projectno & "&agreeno='' &branch=" & txtbranch.Text & "")

    End Sub
End Class