﻿Imports System.Drawing

Public Class JobsList_test
    Inherits System.Web.UI.Page
    Public itemtable As DataTable = createdetailtable()
    Public criteria As DataTable = createCriteria()
    Public menutable As DataTable

    Public cntdt As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objjob As New jobs
        Dim usercode As String
        usercode = Session("usercode")


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


        If Not IsPostBack() Then

            If Not Session("positionid") = "10" Then

                txtStartDate.Attributes.Add("readonly", "readonly")
                txtEndDate.Attributes.Add("readonly", "readonly")

                objbranch.SetComboBranchGroup(cboBranchGroup)
                objbranch.SetComboBranch(cboBranch, "")
                objjob.SetCboJobStatusListForReport(cboStatusFollow)
                objdep.SetCboDepartmentforjobtype(cboDep)
                cboDep.SelectedIndex = cboDep.Items.IndexOf(cboDep.Items.FindByValue(Session("depid").ToString))
                SetCboJobTypeByDepID(cboJobType, cboDep.SelectedItem.Value)


                '------------------------------------
                If Not Session("criteria_Job") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_Job")
                    BindCriteria(criteria)
                    searchjobslist()
                Else
                    searchjobslist()
                End If
            Else
                'กรณีถ้าเป็น ผจก. สาขา
                approval.SetCboApprovalStatusForOwner(cboWorking)
                If Not Session("cboWorking_job") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    cboWorking.SelectedValue = Session("cboWorking_job")
                End If
                Try
                    itemtable = objjob.JobList(usercode, cboWorking.SelectedItem.Value)
                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    Dim javaScript As String = "alertWarning('search fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
                Session("joblist") = itemtable
                BindData()
            End If

        Else

            criteria = Session("criteria_Job")
            itemtable = Session("joblist")
        End If
    End Sub
    Private Sub BindData()
        If Not Session("positionid") = "10" Then
            setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        End If
        cntdt = itemtable.Rows.Count
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub
    Private Sub setCriteria()
        'criteria = createCriteria()
        criteria.Rows.Clear()
        criteria.Rows.Add(txtjobcode.Text.Trim(),
                            cboDep.SelectedItem.Value,
                            cboJobType.SelectedItem.Value,
                            cboStatusFollow.SelectedItem.Value,
                            cboBranchGroup.SelectedItem.Value,
                            cboBranch.SelectedItem.Value,
                            txtStartDate.Text.Trim(),
                            txtEndDate.Text.Trim(),
                            gvRemind.PageIndex)
        Session("criteria_Job") = criteria
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("jobs.aspx")
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdate", GetType(Date))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("txtjobcode", GetType(String))
        dt.Columns.Add("cboDep", GetType(String))
        dt.Columns.Add("cboJobType", GetType(String))
        dt.Columns.Add("cboStatusFollow", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))

        Return dt
    End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtjobcode.Text = criteria.Rows(0).Item("txtjobcode")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")

            cboDep.SelectedValue = criteria.Rows(0).Item("cboDep")
            SetCboJobTypeByDepID(cboJobType, cboDep.SelectedItem.Value)
            cboJobType.SelectedValue = criteria.Rows(0).Item("cboJobType")
            cboStatusFollow.SelectedValue = criteria.Rows(0).Item("cboStatusFollow")
            cboBranchGroup.SelectedValue = criteria.Rows(0).Item("cboBranchGroup")
            Dim objbranch As New Branch
            cboBranch.SelectedIndex = -1
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)

            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")

        End If
    End Sub

    Private Sub searchjobslist()

        Dim objjob As New jobs
        Dim detailtable As New DataTable
        Try


            itemtable = objjob.JobList_For_Operator(txtjobcode.Text.Trim(),
                                                      cboDep.SelectedItem.Value,
                                                      cboJobType.SelectedItem.Value,
                                                      cboStatusFollow.SelectedItem.Value,
                                                        cboBranchGroup.SelectedItem.Value,
                                                        cboBranch.SelectedItem.Value,
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim())

            Session("joblist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 6
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("status") = "แจ้งงาน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("status") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("status") = "ยืนยันแจ้งงาน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("status") = "ปิดงาน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Gray

            ElseIf Data.Item("status") = "ผู้แจ้ง Confirm" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("status") = "กำลังดำเนินการ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Pink
            ElseIf Data.Item("status") = "แจ้ง Supplier" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Pink
            ElseIf Data.Item("status") = "รออะไหล่/อุปกรณ์" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Pink
            ElseIf Data.Item("status") = "รออนุมัติค่าใช้จ่าย" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Pink
            End If
        End If
    End Sub
    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub cboDep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDep.SelectedIndexChanged
        cboJobType.SelectedIndex = -1
        SetCboJobTypeByDepID(cboJobType, cboDep.SelectedItem.Value)
        searchjobslist()
    End Sub

    Private Sub cboBranchGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranchGroup.SelectedIndexChanged
        Dim objbranch As New Branch

        cboBranch.SelectedIndex = -1
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
        searchjobslist()
    End Sub

    Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged
        Session("cboWorking_job") = cboWorking.SelectedItem.Value
        Dim objjob As New jobs
        itemtable = objjob.JobList(Session("usercode"), cboWorking.SelectedItem.Value)
        Session("joblist") = itemtable
        BindData()

    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        searchjobslist()
    End Sub

    Private Sub cboJobType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobType.SelectedIndexChanged
        searchjobslist()
    End Sub

    Private Sub cboStatusFollow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatusFollow.SelectedIndexChanged
        searchjobslist()
    End Sub

    Private Sub txtjobcode_TextChanged(sender As Object, e As EventArgs) Handles txtjobcode.TextChanged
        searchjobslist()
    End Sub

    Private Sub txtStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtStartDate.TextChanged
        searchjobslist()
    End Sub

    Private Sub txtEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtEndDate.TextChanged
        searchjobslist()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtjobcode.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        cboDep.SelectedIndex = -1
        cboJobType.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        itemtable.Rows.Clear()
        criteria.Rows.Clear()
        Session("joblist") = itemtable
        Session("criteria_Job") = criteria

        searchjobslist()
    End Sub
End Class