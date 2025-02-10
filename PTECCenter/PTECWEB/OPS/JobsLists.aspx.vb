Imports System.Drawing
Imports System.IO
Imports ClosedXML.Excel

Public Class JobsList_test
    Inherits System.Web.UI.Page
    Public itemtable As DataTable = createdetailtable()
    Public criteria As DataTable '= createCriteria()
    Public menutable As DataTable

    Public cntdt As Integer
    Public operator_code As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objbranch As New Branch
        Dim objNonpo As New NonPO
        Dim objdep As New Department
        Dim objjob As New jobs
        Dim objsup As New Supplier
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


        operator_code = objNonpo.NonPOPermisstionOperator("JOB")
        If Not IsPostBack() Then

            If Session("SortExpression_Jobslist") Is Nothing Then
                ' ถ้าไม่มีการตั้งค่าการจัดเรียงใน ViewState, กำหนดให้ไม่จัดเรียง (ใช้ข้อมูลตามลำดับเดิม)
                Session("SortExpression_Jobslist") = String.Empty
                Session("SortDirection_Jobslist") = SortDirection.Ascending
            End If

            If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                txtallOperator.Text = operator_code
                'txtStartDate.Attributes.Add("readonly", "readonly")
                'txtEndDate.Attributes.Add("readonly", "readonly")
                'txtStartDate.Text = Date.Now
                'txtEndDate.Text = Date.Now

                objsup.SetCboSupplier(cboSuppiler)
                objbranch.SetComboBranchGroup(cboBranchGroup)
                objbranch.SetComboBranch(cboBranch, "")
                objjob.SetCboJobStatusListForReport(cboStatusFollow)
                objdep.SetCboDepartmentforjobtype(cboDep)
                cboDep.SelectedIndex = cboDep.Items.IndexOf(cboDep.Items.FindByValue(Session("depid").ToString))
                SetCboJobTypeByDepID(cboJobType, cboDep.SelectedValue)


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
                    itemtable = objjob.JobList(usercode, cboWorking.SelectedValue, cboMaxRows.SelectedValue)
                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    Dim javaScript As String = "alertWarning('search fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
                ViewState("joblist") = itemtable
                BindData()
            End If

        Else

            criteria = Session("criteria_Job")
            itemtable = ViewState("joblist")
        End If
    End Sub
    Private Sub BindData()
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        End If

        ' เรียงข้อมูลตามที่บันทึกไว้ใน ViewState
        If Not String.IsNullOrEmpty(Session("SortExpression_Jobslist").ToString()) Then
            Dim dv As DataView = itemtable.DefaultView
            dv.Sort = Session("SortExpression_Jobslist").ToString() & " " & Session("SortDirection_Jobslist").ToString()
        End If

        cntdt = itemtable.Rows.Count
        gvRemind.Caption = "ทั้งหมด " & cntdt & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub
    Private Sub setCriteria()
        criteria = createCriteria()
        criteria.Rows.Clear()
        criteria.Rows.Add(txtjobcode.Text.Trim(),
                            cboDep.SelectedValue,
                            cboJobType.SelectedValue,
                            cboStatusFollow.SelectedValue,
                            cboBranchGroup.SelectedValue,
                            cboBranch.SelectedValue,
                            txtStartDate.Text.Trim(),
                            txtEndDate.Text.Trim(),
                            cboSuppiler.SelectedValue,
                            gvRemind.PageIndex,
                            cboMaxRows.SelectedValue)
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
        dt.Columns.Add("closedate", GetType(Date))
        dt.Columns.Add("detailFollow", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("lockcost", GetType(Boolean))
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
        dt.Columns.Add("cboSuppiler", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))
        dt.Columns.Add("maxrows", GetType(Integer))

        Return dt
    End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtjobcode.Text = criteria.Rows(0).Item("txtjobcode")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")
            cboMaxRows.SelectedValue = criteria.Rows(0).Item("maxrows")

            cboDep.SelectedValue = criteria.Rows(0).Item("cboDep")
            SetCboJobTypeByDepID(cboJobType, cboDep.SelectedValue)
            cboJobType.SelectedValue = criteria.Rows(0).Item("cboJobType")
            cboStatusFollow.SelectedValue = criteria.Rows(0).Item("cboStatusFollow")
            cboBranchGroup.SelectedValue = criteria.Rows(0).Item("cboBranchGroup")
            cboSuppiler.SelectedValue = criteria.Rows(0).Item("cboSuppiler")
            Dim objbranch As New Branch
            cboBranch.SelectedIndex = -1
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedValue)

            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")

        End If
    End Sub

    Private Sub searchjobslist(Optional getReport As Boolean = False)

        Dim objjob As New jobs
        Dim detailtable As New DataTable
        Try

            If getReport Then
                itemtable = objjob.JobList_For_Operator_Report(txtjobcode.Text.Trim(),
                                                      cboDep.SelectedValue,
                                                      cboJobType.SelectedValue,
                                                      cboStatusFollow.SelectedValue,
                                                        cboBranchGroup.SelectedValue,
                                                        cboBranch.SelectedValue,
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                        cboSuppiler.SelectedValue,
                                                        cboMaxRows.SelectedValue)

            Else
                itemtable = objjob.JobList_For_Operator(txtjobcode.Text.Trim(),
                                                          cboDep.SelectedValue,
                                                          cboJobType.SelectedValue,
                                                          cboStatusFollow.SelectedValue,
                                                            cboBranchGroup.SelectedValue,
                                                            cboBranch.SelectedValue,
                                                            txtStartDate.Text.Trim(),
                                                            txtEndDate.Text.Trim(),
                                                            cboSuppiler.SelectedValue,
                                                            cboMaxRows.SelectedValue)
            End If

            ViewState("joblist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 10
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
            ElseIf Data.Item("status") = "ตรวจรับไม่สำเร็จ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            Else
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
        SetCboJobTypeByDepID(cboJobType, cboDep.SelectedValue)
        'searchjobslist()
    End Sub

    Private Sub cboBranchGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranchGroup.SelectedIndexChanged
        Dim objbranch As New Branch

        cboBranch.SelectedIndex = -1
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedValue)
        'searchjobslist()
    End Sub

    Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged
        Session("cboWorking_job") = cboWorking.SelectedValue
        Dim objjob As New jobs
        itemtable = objjob.JobList(Session("usercode"), cboWorking.SelectedValue, cboMaxRows.SelectedValue)
        ViewState("joblist") = itemtable
        BindData()

    End Sub

    'Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
    '    searchjobslist()
    'End Sub

    'Private Sub cboJobType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobType.SelectedIndexChanged
    '    searchjobslist()
    'End Sub

    'Private Sub cboStatusFollow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatusFollow.SelectedIndexChanged
    '    searchjobslist()
    'End Sub

    'Private Sub txtjobcode_TextChanged(sender As Object, e As EventArgs) Handles txtjobcode.TextChanged
    '    searchjobslist()
    'End Sub

    'Private Sub txtStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtStartDate.TextChanged
    '    searchjobslist()
    'End Sub

    'Private Sub txtEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtEndDate.TextChanged
    '    searchjobslist()
    'End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtjobcode.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        cboDep.SelectedIndex = -1
        cboJobType.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboSuppiler.SelectedIndex = -1
        itemtable.Rows.Clear()
        criteria.Rows.Clear()
        ViewState("joblist") = itemtable
        Session("criteria_Job") = criteria

        SetCboJobTypeByDepID(cboJobType, cboDep.SelectedValue)

        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedValue)

        BindData()
        'searchjobslist()
    End Sub

    Private Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        Dim createdate As String
        createdate = Date.Now
        Dim objnonpo As New NonPO
        Try
            searchjobslist(True)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        If itemtable IsNot Nothing Then
            If itemtable.Rows.Count() > 0 Then
                ExportToExcel(itemtable, Session("usercode"), createdate, Request.QueryString("NonpoCode"))
            End If
        End If
    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String, nonpocode As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & nonpocode & "_" & Date.Now.ToString
            Dim encode As String
            'If (maintable.Rows(0).Item("statusid") = 7) Then
            '    encode = "(preview)"
            'Else
            '    encode = "(final)"
            'End If
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(filename)
            encode = Convert.ToBase64String(byt)

            'Dim decode As String
            'Dim b As Byte() = Convert.FromBase64String(encode)
            'decode = System.Text.Encoding.UTF8.GetString(b)

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & encode & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchjobslist()
    End Sub
    Private Sub gvRemind_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvRemind.Sorting

        Dim dt As DataTable = TryCast(itemtable, DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
            BindData()
        End If
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String
        Dim sortDirection As String = "ASC"
        Dim sortExpression As String = TryCast(Session("SortExpression_Jobslist"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(Session("SortDirection_Jobslist"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        Session("SortDirection_Jobslist") = sortDirection
        Session("SortExpression_Jobslist") = column
        Return sortDirection
    End Function
End Class