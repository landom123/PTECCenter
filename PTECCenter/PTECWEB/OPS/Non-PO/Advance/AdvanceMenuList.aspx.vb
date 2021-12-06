Imports System.Drawing

Public Class AdvanceMenuList
    Inherits System.Web.UI.Page
    Public itemtable As DataTable = createdetailtable()
    Public criteria As DataTable = createCriteria()
    Public menutable As DataTable

    Public cntdt As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
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

                objjob.SetCboJobStatusListForReport(cboStatusFollow)
                objbranch.SetComboBranchGroup(cboBranchGroup)
                objbranch.SetComboBranch(cboBranch, "")
                objdep.SetCboDepartmentBybranch(cboDepartment, 0)
                objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
                chkHO.Checked = True

                '------------------------------------
                If Not Session("criteria_advlist") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_advlist")
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

                Dim objNonPO As New NonPO
                Try
                    itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedItem.Value)
                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    Dim javaScript As String = "alertWarning('search fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
                Session("joblist") = itemtable
                BindData()
            End If

        Else

            criteria = Session("criteria_advlist")
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
        criteria.Rows.Add(txtAdvRQ.Text.Trim(),
                            txtStartDate.Text.Trim(),
                            txtEndDate.Text.Trim(),
                            cboStatusFollow.SelectedItem.Value.ToString,
                            cboDepartment.SelectedItem.Value.ToString,
                            cboSection.SelectedItem.Value.ToString,
                            cboBranchGroup.SelectedItem.Value.ToString,
                            cboBranch.SelectedItem.Value.ToString,
                            gvRemind.PageIndex)

        Session("criteria_advlist") = criteria
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("AdvanceRequest.aspx")
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdate", GetType(Date))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("closedate", GetType(Date))
        dt.Columns.Add("detailFollow", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("txtadvrq", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))
        dt.Columns.Add("cboStatusFollow", GetType(String))
        dt.Columns.Add("cboDep", GetType(String))
        dt.Columns.Add("cboSec", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))

        Return dt
    End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtAdvRQ.Text = criteria.Rows(0).Item("txtadvrq")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")

            cboStatusFollow.SelectedValue = criteria.Rows(0).Item("cboStatusFollow")
            cboBranchGroup.SelectedValue = criteria.Rows(0).Item("cboBranchGroup")
            Dim objbranch As New Branch
            cboBranch.SelectedIndex = -1
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")

            cboDepartment.SelectedValue = criteria.Rows(0).Item("cboDep")
            cboSection.SelectedIndex = -1
            Dim depid As Integer
            Dim objsection As New Section

            depid = cboDepartment.SelectedItem.Value
            objsection.SetCboSection_seccode(cboSection, depid)
            cboSection.SelectedValue = criteria.Rows(0).Item("cboSec")


        End If
    End Sub

    Private Sub searchjobslist()

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            If chkCO.Checked Then
                itemtable = objNonPO.AdvanceRQList_For_Operator(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString)
            ElseIf chkHO.Checked Then
                itemtable = objNonPO.AdvanceRQList_For_Operator(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                        cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                      "",
                                                      "")
            Else
                itemtable = objNonPO.AdvanceRQList_For_Operator(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                        "",
                                                      "",
                                                      "",
                                                      "")
            End If



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
            If Data.Item("status") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("status") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("status") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("status") = "ชำระเงินเสร็จสิ้น" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("status") = "ไม่ผ่านการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("status") = "รอการเงินตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightCoral
            ElseIf Data.Item("status") = "รอเคลียร์ค้างชำระ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Brown
            End If
        End If
    End Sub
    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
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
        'itemtable = objjob.JobList(Session("usercode"), cboWorking.SelectedItem.Value)
        Session("joblist") = itemtable
        BindData()

    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        searchjobslist()
    End Sub

    Private Sub cboStatusFollow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatusFollow.SelectedIndexChanged
        searchjobslist()
    End Sub

    Private Sub txtAdvRQ_TextChanged(sender As Object, e As EventArgs) Handles txtAdvRQ.TextChanged
        searchjobslist()
    End Sub

    Private Sub txtStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtStartDate.TextChanged
        searchjobslist()
    End Sub

    Private Sub txtEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtEndDate.TextChanged
        searchjobslist()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtAdvRQ.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("joblist") = itemtable
        Session("criteria_advlist") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
        searchjobslist()
    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        searchjobslist()

    End Sub

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        searchjobslist()
    End Sub
End Class