Imports System.Drawing

Public Class PettyCashCOMenulist
    Inherits System.Web.UI.Page

    Public itemtable As DataTable

    Public criteria As DataTable = createCriteria()
    Public menutable As DataTable
    Public cntdt As Integer

    Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objNonpo As New NonPO
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier
        'Dim objjob As New jobs
        Dim usercode As String
        usercode = Session("usercode")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


        txtStartDate.Attributes.Add("readonly", "readonly")
        txtEndDate.Attributes.Add("readonly", "readonly")



        txtStartDueDate.Attributes.Add("readonly", "readonly")
        txtEndDueDate.Attributes.Add("readonly", "readonly")
        operator_code = objNonpo.NonPOPermisstionOperator("PCCO")

        If Not IsPostBack() Then
            If operator_code.IndexOf(Session("usercode").ToString) > -1 Then

                objNonpo.SetCboStatusbyNonpocategory(cboStatusFollow, "PCCO")
                objbranch.SetComboBranchGroup(cboBranchGroup)
                objbranch.SetComboBranch(cboBranch, "")
                'objsupplier.SetCboVendorByName(cboVendor, "")
                objdep.SetCboDepartmentBybranch(cboDepartment, 0)
                objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
                'chkHO.Checked = True

                '------------------------------------
                If Not Session("criteria_joblist") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_joblist")
                    BindCriteria(criteria)
                    searchjobslist()
                Else
                    searchjobslist()
                End If
            Else
                approval.SetCboApprovalStatusForOwner(cboWorking)
                'If Not Session("cboWorking_clearadv") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                '    cboWorking.SelectedValue = Session("cboWorking_clearadv")
                'End If

                Try
                    'itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedItem.Value)
                    itemtable = objNonpo.PettyCashCO_For_Owner(Session("userid"), cboWorking.SelectedItem.Value)
                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    Dim javaScript As String = "alertWarning('search fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
                Session("joblist") = itemtable
                BindData()
            End If
        Else

            criteria = Session("criteria_joblist")
            itemtable = Session("joblist")
        End If
    End Sub

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("txtclearadv", GetType(String))
        dt.Columns.Add("txtcoderef", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))
        dt.Columns.Add("cboStatusFollow", GetType(String))
        dt.Columns.Add("txtStartDueDate", GetType(String))
        dt.Columns.Add("txtEndDueDate", GetType(String))
        dt.Columns.Add("cboDep", GetType(String))
        dt.Columns.Add("cboSec", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))

        Return dt
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("PettyCashCO2.aspx")
    End Sub

    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtclearadv.Text = criteria.Rows(0).Item("txtclearadv")
            'txtcoderef.Text = criteria.Rows(0).Item("txtcoderef")

            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")

            txtStartDueDate.Text = criteria.Rows(0).Item("txtStartDueDate")
            txtEndDueDate.Text = criteria.Rows(0).Item("txtEndDueDate")

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
            itemtable = objNonPO.PettyCashCO_For_Operator(txtclearadv.Text.Trim(),
                                                    txtStartDate.Text.Trim(),
                                                    txtEndDate.Text.Trim(),
                                                  cboStatusFollow.SelectedItem.Value.ToString,
                                                    txtStartDueDate.Text.Trim(),
                                                    txtEndDueDate.Text.Trim(),
                                                  "",
                                                  "",
                                                    cboBranchGroup.SelectedItem.Value.ToString,
                                                    cboBranch.SelectedItem.Value.ToString,
                                                    "")
            Session("joblist") = itemtable
            BindData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtclearadv.Text = ""
        'txtcoderef.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        txtStartDueDate.Text = ""
        txtEndDueDate.Text = ""
        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboVendor.SelectedIndex = -1
        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("joblist") = itemtable
        Session("criteria_joblist") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)


        BindData()
    End Sub
    Private Sub BindData()
        cntdt = itemtable.Rows.Count
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchjobslist()
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function dup(ByVal nonpocode As String, ByVal usercode As String, ByVal note As String)
        Dim objnonpo As New NonPO
        Try
            objnonpo.NonPO_Payment_Duplicate(nonpocode, usercode, note)

        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 7
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("statusnonpo") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("statusnonpo") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("statusnonpo") = "รอตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGoldenrodYellow
            ElseIf Data.Item("statusnonpo") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("statusnonpo") = "ชำระเงินเสร็จสิ้น" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("statusnonpo") = "ไม่ผ่านการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("statusnonpo") = "รอการเงินตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightCoral
            ElseIf Data.Item("statusnonpo") = "รอบัญชีตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightSalmon
            ElseIf Data.Item("statusnonpo") = "รอเคลียร์ค้างชำระ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Brown
                e.Row.Cells.Item(statusAt).ForeColor = Color.White
            ElseIf Data.Item("statusnonpo") = "ขอเอกสารเพิ่มเติม" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.MediumPurple
            ElseIf Data.Item("statusnonpo") = "ได้รับเอกสารตัวจริง" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Gray
            ElseIf Data.Item("statusnonpo") = "รอเอกสารตัวจริง" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Yellow
            ElseIf Data.Item("statusnonpo") = "อนุมัติหักยอดขาย" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Gray

            End If
        End If
    End Sub

    Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged

        Dim objNonpo As New NonPO

        Try
            'itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedItem.Value)
            itemtable = objNonpo.PaymentList_For_Owner(Session("userid"), cboWorking.SelectedItem.Value)
            Session("joblist") = itemtable

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        BindData()
    End Sub
    Private Sub cboBranchGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranchGroup.SelectedIndexChanged
        Dim objbranch As New Branch

        cboBranch.SelectedIndex = -1
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
        'searchjobslist()
    End Sub
    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        'searchjobslist()

    End Sub
End Class