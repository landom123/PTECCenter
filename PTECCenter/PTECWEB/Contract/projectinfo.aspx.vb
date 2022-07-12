

Public Class projectinfo
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Public agreetable As DataTable = createagreetable()

    Public usercode, username, projectno As String
    Public projectid As Double = 0
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
            Find(projectno)

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
            Dim javaScript As String = "alertWarning('" & err & "" ')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

        End Try

    End Sub
    Private Function CreateAgreeTable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("agid", GetType(Double))
        dt.Columns.Add("agno", GetType(String))
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

                SetButton(.Rows(0).Item("status"))
            End If

        End With
    End Sub
    Public Sub SetButton(status As String)
        lblStatus.Text = status

        Select Case status
            Case = "New"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnNewAgree.Enabled = False
                btnNewProject.Enabled = True
                btnApprove.Enabled = False
                btnChange.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
            Case = "บันทึก"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkOrange
                btnNewAgree.Enabled = True
                btnNewProject.Enabled = True
                btnApprove.Enabled = False
                btnChange.Enabled = False
                btnConfirm.Enabled = True
                btnCancel.Enabled = True
            Case = "รับรอง"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkGreen
                btnNewAgree.Enabled = False
                btnNewProject.Enabled = True
                btnApprove.Enabled = True
                btnChange.Enabled = True
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
            Case = "ยกเลิก"
                lblStatus.ForeColor = System.Drawing.Color.White
                lblStatus.BackColor = System.Drawing.Color.DarkRed
                btnNewAgree.Enabled = False
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
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String = "alertSuccess('ยกเลิกข้อมูลเรียบร้อย')"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    End Sub

    Private Sub btnCofirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ConfirmData
    End Sub
    Public Sub ConfirmData()
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String = "alertSuccess('ยืนยันรายการเรียบร้อย')"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    End Sub
End Class