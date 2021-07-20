Imports System.Drawing

Public Class WebForm3
    Inherits System.Web.UI.Page
    Public criteria As DataTable = createCriteria()
    Public itemtable As DataTable = createdetailtable()
    Public menutable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim area As New Area
        Dim objbranch As New Branch
        Dim usercode As String
        usercode = Session("usercode")

        txtStartDate.Attributes.Add("readonly", "readonly")
        txtEndDate.Attributes.Add("readonly", "readonly")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        Session("status") = "read"
        If Not IsPostBack() Then

            approval.SetCboApproval(cboApproval)
            approval.SetCboApprovalCategory(cboApprovalCategory)
            approval.SetCboApprovalStatus(cboStatus)
            area.SetCboArea(cboArea)
            objbranch.SetComboBranchByAreaid(cboBranch, cboArea.SelectedItem.Value.ToString, Session("userid"), 0)
            If Not Session("positionid") = "10" Then
                If Session("positionid") = "9" Then
                    area.SetCboArea(cboArea, Session("userid"))
                    objbranch.SetComboBranchByAreaid(cboBranch, cboArea.SelectedItem.Value.ToString, Session("userid"), 0)
                End If
                If Not Session("criteria") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria")
                    BindCriteria(criteria)
                    searchapprovallist()
                Else
                    searchapprovallist()
                End If
            Else

                approval.SetCboApprovalStatusForOwner(cboWorking)

                If Not Session("cboWorking") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    cboWorking.SelectedValue = Session("cboWorking")
                End If
                itemtable = approval.ApprovalMenuList(Session("userid"), cboWorking.SelectedItem.Value)
            End If

            Session("approvallist") = itemtable
            BindData()
        Else
            itemtable = Session("approvallist")
            BindData()
        End If
    End Sub

    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtApprovalCode.Text = criteria.Rows(0).Item("txtApprovalCode")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")

            cboApprovalCategory.SelectedValue = criteria.Rows(0).Item("cboApprovalCategory")
            cboApproval.SelectedValue = criteria.Rows(0).Item("cboApproval")
            cboStatus.SelectedValue = criteria.Rows(0).Item("cboStatus")
            cboArea.SelectedValue = criteria.Rows(0).Item("cboArea")
            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")
        End If
    End Sub

    Private Sub BindData()
        If itemtable.Rows.Count > 0 Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("txtApprovalCode", GetType(String))
        dt.Columns.Add("cboApprovalCategory", GetType(String))
        dt.Columns.Add("cboApproval", GetType(String))
        dt.Columns.Add("cboStatus", GetType(String))
        dt.Columns.Add("cboArea", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))

        Return dt
    End Function

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("approvalcode", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("approvalname", GetType(String))
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 5
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("statusname") = "รอผู้แจ้งยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("statusname") = "ยกเลิกการแจ้ง" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("statusname") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("statusname") = "อนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("statusname") = "ไม่อนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("statusname") = "ปิดงาน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Gray
            ElseIf Data.Item("statusname") = "รอประสานงานรับเรื่อง" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightCoral
            ElseIf Data.Item("statusname") = "ดำเนินการด้านเอกสาร" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.OrangeRed
            ElseIf Data.Item("statusname") = "รอแสกนเอกสาร" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Brown
            ElseIf Data.Item("statusname") = "เอกสารครบถ้วน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.MediumPurple
            End If
        End If



    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchapprovallist()
    End Sub
    Private Sub searchapprovallist()
        Dim approval As New Approval
        Dim detailtable As New DataTable
        Try
            itemtable = approval.FindApprovalMenuList(txtApprovalCode.Text,
                                                      cboBranch.SelectedItem.Value,
                                                      cboApprovalCategory.SelectedItem.Value,
                                                        cboApproval.SelectedItem.Value,
                                                        cboStatus.SelectedItem.Value,
                                                        cboArea.SelectedItem.Value,
                                                        Session("userid"),
                                                        txtStartDate.Text,
                                                        txtEndDate.Text)

            setCriteria()
            Session("approvallist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alert('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub

    Private Sub setCriteria()
        criteria = createCriteria()
        criteria.Rows.Add(txtApprovalCode.Text.ToString.Trim(),
                          cboApprovalCategory.SelectedItem.Value,
                          (cboApproval.SelectedItem.Value),
                          (cboStatus.SelectedItem.Value),
                          (cboArea.SelectedItem.Value),
                          (cboBranch.SelectedItem.Value),
                          txtStartDate.Text.ToString.Trim(),
                          txtEndDate.Text.ToString.Trim())
        Session("criteria") = criteria
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtApprovalCode.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        cboBranch.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        cboArea.SelectedIndex = -1
        cboApprovalCategory.SelectedIndex = -1
        cboApproval.SelectedIndex = -1
        itemtable.Rows.Clear()
        criteria.Rows.Clear()
        Session("approvallist") = itemtable
        Session("criteria") = criteria

        searchapprovallist()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim s As String = "window.open('../approval/WebForm5.aspx?approvalcode=" & txtApprovalCode.Text &
    "&branchid=" & cboBranch.SelectedItem.Value &
    "&categoryid=" & cboApprovalCategory.SelectedItem.Value &
    "&approvallistid=" & cboApproval.SelectedItem.Value &
    "&statusid=" & cboStatus.SelectedItem.Value &
    "&areaid=" & cboArea.SelectedItem.Value &
    "&userid=" & Session("userid") &
    "&startdate=" & txtStartDate.Text &
    "&enddate=" & txtEndDate.Text &
    " ', '_blank');"

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
    End Sub

    Private Sub cboApprovalCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboApprovalCategory.SelectedIndexChanged
        Dim approval As New Approval
        cboApproval.SelectedIndex = -1
        approval.SetCboApprovalByCategoryID(cboApproval, cboApprovalCategory.SelectedItem.Value)
        searchapprovallist()
    End Sub

    Private Sub cboArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboArea.SelectedIndexChanged
        Dim objbranch As New Branch
        cboBranch.SelectedIndex = -1
        objbranch.SetComboBranchByAreaid(cboBranch, cboArea.SelectedItem.Value, Session("userid"), 0)
        searchapprovallist()
    End Sub

    Private Sub cboApproval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboApproval.SelectedIndexChanged
        searchapprovallist()
    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        searchapprovallist()
    End Sub
    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        searchapprovallist()
    End Sub

    Private Sub txtStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtStartDate.TextChanged
        searchapprovallist()
    End Sub

    Private Sub txtEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtEndDate.TextChanged
        searchapprovallist()
    End Sub

    Private Sub txtApprovalCode_TextChanged(sender As Object, e As EventArgs) Handles txtApprovalCode.TextChanged
        searchapprovallist()
    End Sub

    Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged
        Session("cboWorking") = cboWorking.SelectedItem.Value
        Dim approval As New Approval
        itemtable = approval.ApprovalMenuList(Session("userid"), cboWorking.SelectedItem.Value)
        Session("approvallist") = itemtable
        BindData()

    End Sub
End Class