Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class FrmRemindInfo
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public itemtable As DataTable = createdetailtable()
    Public menutable As DataTable
    Public usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim usercode As String
        usercode = Session("usercode")
        Dim remindid As String = Request.QueryString("remindid")
        'txtRemindDate.ReadOnly = True
        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        If Not IsPostBack() Then
            setCboRepeat()
            setCboPeriodType()
            SetCboActived()
            SetCboBranch()

            If remindid = 0 Then
                txtRemindID.Text = 0
                objStatus = "เพิ่มการแจ้งเตือน"
                btnSaveAs.Enabled = False
            Else
                objStatus = "แสดงข้อมูล"
                txtRemindID.Text = remindid
                btnSaveAs.Enabled = True
                itemtable = LoadRemind(remindid)
                Session("itemtable") = itemtable
                ShowData()
            End If

        Else
            itemtable = Session("itemtable")
        End If
    End Sub
    Private Sub SetCboBranch()
        Dim branch As New Branch
        cboBranch.DataSource = branch.List(usercode)
        cboBranch.DataValueField = "branchid"
        cboBranch.DataTextField = "name"
        cboBranch.DataBind()
    End Sub
    Private Sub SetCboRepeat()
        cboRepeat.Items.Add("อาทิตย์")
        cboRepeat.Items.Add("จันทร์")
        cboRepeat.Items.Add("อังคาร")
        cboRepeat.Items.Add("พุธ")
        cboRepeat.Items.Add("พฤหัส")
        cboRepeat.Items.Add("ศุกร์")
        cboRepeat.Items.Add("เสาร์")
    End Sub
    Private Sub SetCboPeriodType()
        cboPeriodType.Items.Add("วัน")
        cboPeriodType.Items.Add("เดือน")
        cboPeriodType.Items.Add("ปี")
    End Sub
    Private Sub SetCboActived()
        cboActived.Items.Add("เปิดใช้งาน")
        cboActived.Items.Add("ปิดใช้งาน")
    End Sub
    Private Sub ShowData()
        txtCreateDate.Text = itemtable.Rows(0).Item("createdate")
        txtUpdateDate.Text = itemtable.Rows(0).Item("updatedate")
        cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByText(itemtable.Rows(0).Item("branchname")))
        txtEmail.Text = itemtable.Rows(0).Item("email")
        txtRemindDetail.Text = itemtable.Rows(0).Item("reminddetail")
        txtEmailMessage.Text = itemtable.Rows(0).Item("emailmessage")
        txtRemindDate.Text = itemtable.Rows(0).Item("reminddate")
        txtNextDate.Text = itemtable.Rows(0).Item("nextremind")
        txtWarningDay.Text = itemtable.Rows(0).Item("warningday")
        cboRepeat.SelectedIndex = itemtable.Rows(0).Item("warningdayofweek") - 1
        txtPeriod.Text = itemtable.Rows(0).Item("periodno")
        cboPeriodType.SelectedIndex = itemtable.Rows(0).Item("periodtype") - 1
        If itemtable.Rows(0).Item("actived") = True Then
            cboActived.SelectedIndex = 0
        Else
            cboActived.SelectedIndex = 1
        End If
        'chkActive.Checked = itemtable.Rows(0).Item("actived")
    End Sub
    Private Function LoadRemind(remindid As Double) As DataTable
        Dim result As DataTable
        Dim remindobj As New Remind
        Try
            result = remindobj.Info(remindid)

        Catch ex As Exception

        End Try
        Return result
    End Function
    Private Function Createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("remindid", GetType(Double))
        dt.Columns.Add("branchname", GetType(String))
        dt.Columns.Add("reminddetail", GetType(String))
        dt.Columns.Add("email", GetType(String))
        dt.Columns.Add("emailmessage", GetType(String))
        dt.Columns.Add("reminddate", GetType(Date))

        dt.Columns.Add("periodno", GetType(Integer))
        dt.Columns.Add("periodtype", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(DateTime))
        dt.Columns.Add("updatedate", GetType(DateTime))
        dt.Columns.Add("actived", GetType(Integer))



        Return dt
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Dim a As String = txtRemindDate.Text
        Dim remindid As Double, branchid As Double,
            reminddetail As String, emailmessage As String,
            email As String, warningday As Integer, warningdayofweek As Integer,
            reminddate As DateTime, periodno As Integer, periodtype As Integer,
            usercode As String, active As Integer

        Dim remind As New Remind
        remindid = txtRemindID.Text
        branchid = cboBranch.SelectedItem.Value
        reminddetail = txtRemindDetail.Text
        emailmessage = txtEmailMessage.Text
        email = txtEmail.Text
        Try
            warningday = Integer.Parse(txtWarningDay.Text)
        Catch ex As Exception
            warningday = 90 'ถ้า error default 90 วัน
        End Try
        warningdayofweek = cboRepeat.SelectedIndex + 1
        reminddate = DateTime.Parse(txtRemindDate.Text)
        Try
            periodno = Integer.Parse(txtPeriod.Text)
        Catch ex As Exception
            periodno = 1 'ถ้า error default 1
        End Try
        periodtype = cboPeriodType.SelectedIndex + 1
        usercode = Session("usercode")
        active = cboActived.SelectedIndex + 1
        remindid = remind.Remind_Info_Save(remindid, branchid, reminddetail, emailmessage,
                    email, warningday, warningdayofweek, reminddate, periodno, periodtype, usercode, active)

        txtRemindID.Text = remindid

    End Sub

    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        'Dim a As String = txtRemindDate.Text
        Dim remindid As Double, branchid As Double,
            reminddetail As String, emailmessage As String,
            email As String, warningday As Integer, warningdayofweek As Integer,
            reminddate As DateTime, periodno As Integer, periodtype As Integer,
            usercode As String, active As Integer

        Dim remind As New Remind
        remindid = 0
        branchid = cboBranch.SelectedItem.Value
        reminddetail = txtRemindDetail.Text & " copy "
        emailmessage = txtEmailMessage.Text & " copy "

        txtRemindDetail.Text = reminddetail
        txtEmailMessage.Text = emailmessage
        email = txtEmail.Text
        Try
            warningday = Integer.Parse(txtWarningDay.Text)
        Catch ex As Exception
            warningday = 90 'ถ้า error default 90 วัน
        End Try
        warningdayofweek = cboRepeat.SelectedIndex + 1
        reminddate = DateTime.Parse(txtRemindDate.Text)
        Try
            periodno = Integer.Parse(txtPeriod.Text)
        Catch ex As Exception
            periodno = 1 'ถ้า error default 1
        End Try
        periodtype = cboPeriodType.SelectedIndex + 1
        usercode = Session("usercode")
        active = cboActived.SelectedIndex + 1
        remindid = remind.Remind_Info_Save(remindid, branchid, reminddetail, emailmessage,
                    email, warningday, warningdayofweek, reminddate, periodno, periodtype, usercode, active)

        txtRemindID.Text = remindid
    End Sub
End Class