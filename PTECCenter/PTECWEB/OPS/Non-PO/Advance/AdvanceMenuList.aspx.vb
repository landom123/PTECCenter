Imports System.Drawing
Imports System.IO
Imports ClosedXML.Excel

Public Class AdvanceMenuList
    Inherits System.Web.UI.Page
    Public itemtable As DataTable = createdetailtable()
    Public criteria As DataTable '= createCriteria()
    Public menutable As DataTable

    Public cntdt As Integer

    Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objNonpo As New NonPO
        Dim objsec As New Section
        Dim objcompany As New Company
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
        operator_code = objNonpo.NonPOPermisstionOperator("ADV")
        If Not IsPostBack() Then
            criteria = createCriteria()
            objNonpo.SetCboStatusbyNonpocategory(cboStatusFollow, "ADV")
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranch(cboBranch, "")
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedValue)
            objcompany.SetCboCompany(cboCompany, 0)
            SetCboUsers(cboCreateby)
            SetCboUsers(cboOwner)
            If Session("positionid") = "10" Or Session("positionid") = "15" Then '10 PM , 15 AA
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If
            If operator_code.IndexOf(Session("usercode").ToString) > -1 Then

                'objjob.SetCboJobStatusListForReport(cboStatusFollow)

                '------------------------------------
                If Not Session("criteria_advlist") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_advlist")
                    BindCriteria(criteria)
                    searchjobslist()
                Else
                    searchjobslist()
                End If
            Else
                'approval.SetCboApprovalStatusForOwner(cboWorking)
                'If Not Session("cboWorking_job") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                '    cboWorking.SelectedValue = Session("cboWorking_job")
                'End If
                If Not Session("criteria_advlist") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_advlist")
                    BindCriteria(criteria)
                    searchjobslist_owner()
                Else
                    searchjobslist_owner()
                End If
            End If

        Else

            criteria = Session("criteria_advlist")
            itemtable = Session("joblist")
        End If
    End Sub
    Private Sub BindData()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        cntdt = itemtable.Rows.Count
        gvRemind.Caption = "ทั้งหมด " & cntdt & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub
    Private Sub setCriteria()
        'criteria = createCriteria()
        If Not criteria Is Nothing Then

            criteria.Rows.Clear()
            criteria.Rows.Add(txtAdvRQ.Text.Trim(),
                                txtStartDate.Text.Trim(),
                                txtEndDate.Text.Trim(),
                                cboStatusFollow.SelectedValue.ToString,
                                txtStartDueDate.Text.Trim(),
                                txtEndDueDate.Text.Trim(),
                                cboDepartment.SelectedValue.ToString,
                                cboSection.SelectedValue.ToString,
                                cboCompany.SelectedValue.ToString,
                                cboBranchGroup.SelectedValue.ToString,
                                cboBranch.SelectedValue.ToString,
                                cboCreateby.SelectedValue.ToString,
                                cboOwner.SelectedValue.ToString,
                                gvRemind.PageIndex,
                                cboMaxRows.SelectedValue)

            Session("criteria_advlist") = criteria
        End If

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
        dt.Columns.Add("txtStartDueDate", GetType(String))
        dt.Columns.Add("txtEndDueDate", GetType(String))
        dt.Columns.Add("cboDep", GetType(String))
        dt.Columns.Add("cboSec", GetType(String))
        dt.Columns.Add("cboCom", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("cboCreateby", GetType(String))
        dt.Columns.Add("cboOwner", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))
        dt.Columns.Add("maxrows", GetType(Integer))

        Return dt
    End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtAdvRQ.Text = criteria.Rows(0).Item("txtadvrq")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")
            cboMaxRows.SelectedValue = criteria.Rows(0).Item("maxrows")

            txtStartDueDate.Text = criteria.Rows(0).Item("txtStartDueDate")
            txtEndDueDate.Text = criteria.Rows(0).Item("txtEndDueDate")

            cboStatusFollow.SelectedValue = criteria.Rows(0).Item("cboStatusFollow")
            cboBranchGroup.SelectedValue = criteria.Rows(0).Item("cboBranchGroup")
            Dim objbranch As New Branch
            cboBranch.SelectedIndex = -1
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedValue)
            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")

            cboDepartment.SelectedValue = criteria.Rows(0).Item("cboDep")
            cboSection.SelectedIndex = -1
            Dim depid As Integer
            Dim objsection As New Section

            depid = cboDepartment.SelectedValue
            objsection.SetCboSection_seccode(cboSection, depid)
            cboSection.SelectedValue = criteria.Rows(0).Item("cboSec")
            cboCompany.SelectedValue = criteria.Rows(0).Item("cboCom")
            cboCreateby.SelectedValue = criteria.Rows(0).Item("cboCreateby")
            cboOwner.SelectedValue = criteria.Rows(0).Item("cboOwner")


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
                                                      cboStatusFollow.SelectedValue.ToString,
                                                        txtStartDueDate.Text.Trim(),
                                                        txtEndDueDate.Text.Trim(),
                                                      "",
                                                      "",
                                                        cboCompany.SelectedValue.ToString,
                                                        cboBranchGroup.SelectedValue.ToString,
                                                        cboBranch.SelectedValue.ToString,
                                                        cboCreateby.SelectedValue.ToString,
                                                        cboOwner.SelectedValue.ToString,
                                                        cboMaxRows.SelectedValue)
            ElseIf chkHO.Checked Then
                itemtable = objNonPO.AdvanceRQList_For_Operator(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedValue.ToString,
                                                        txtStartDueDate.Text.Trim(),
                                                        txtEndDueDate.Text.Trim(),
                                                        cboDepartment.SelectedValue.ToString,
                                                        cboSection.SelectedValue.ToString,
                                                        cboCompany.SelectedValue.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedValue.ToString,
                                                        cboOwner.SelectedValue.ToString,
                                                        cboMaxRows.SelectedValue)
            Else
                itemtable = objNonPO.AdvanceRQList_For_Operator(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedValue.ToString,
                                                        txtStartDueDate.Text.Trim(),
                                                        txtEndDueDate.Text.Trim(),
                                                        "",
                                                      "",
                                                        cboCompany.SelectedValue.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedValue.ToString,
                                                        cboOwner.SelectedValue.ToString,
                                                        cboMaxRows.SelectedValue)
            End If



            Session("joblist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub searchjobslist_owner()

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            If chkCO.Checked Then
                itemtable = objNonPO.AdvanceRQList_For_Owner(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedValue.ToString,
                                                        txtStartDueDate.Text.Trim(),
                                                        txtEndDueDate.Text.Trim(),
                                                        cboCompany.SelectedValue.ToString,
                                                        cboBranchGroup.SelectedValue.ToString,
                                                        cboBranch.SelectedValue.ToString,
                                                        Session("userid"),
                                                         "CO",
                                                        cboMaxRows.SelectedValue)
            ElseIf chkHO.Checked Then
                itemtable = objNonPO.AdvanceRQList_For_Owner(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedValue.ToString,
                                                        txtStartDueDate.Text.Trim(),
                                                        txtEndDueDate.Text.Trim(),
                                                        cboCompany.SelectedValue.ToString,
                                                      "",
                                                      "",
                                                        Session("userid"),
                                                        "HO",
                                                        cboMaxRows.SelectedValue)
            Else
                itemtable = objNonPO.AdvanceRQList_For_Owner(txtAdvRQ.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedValue.ToString,
                                                        txtStartDueDate.Text.Trim(),
                                                        txtEndDueDate.Text.Trim(),
                                                        cboCompany.SelectedValue.ToString,
                                                      "",
                                                      "",
                                                        Session("userid"),
                                                        "",
                                                        cboMaxRows.SelectedValue)
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
        Dim statusAt As Integer = 10
        Dim companyAt As Integer = 0
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
            ElseIf Data.Item("status") = "รอบัญชีตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightSalmon
            ElseIf Data.Item("status") = "รอเคลียร์ค้างชำระ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Brown
                e.Row.Cells.Item(statusAt).ForeColor = Color.White
            ElseIf Data.Item("status") = "ขอเอกสารเพิ่มเติม" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.MediumPurple
            ElseIf Data.Item("status") = "ได้รับเอกสารตัวจริง" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Gray
            ElseIf Data.Item("statusnonpo") = "รอเอกสารตัวจริง" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Yellow
            End If

            If Data.Item("comcode") = "PURE" Then
                e.Row.Cells.Item(companyAt).ForeColor = Color.FromArgb(1, 237, 1, 128)
            ElseIf Data.Item("comcode") = "SAP" Then
                e.Row.Cells.Item(companyAt).ForeColor = Color.FromArgb(1, 0, 166, 81)
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
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedValue)
        'searchjobslist()
    End Sub

    'Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged
    '    Session("cboWorking_job") = cboWorking.SelectedValue

    '    Dim objNonpo As New NonPO

    '    Try
    '        'itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedValue)
    '        itemtable = objNonpo.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedValue)
    '        Session("joblist") = itemtable

    '    Catch ex As Exception
    '        Dim scriptKey As String = "alert"
    '        Dim javaScript As String = "alertWarning('search fail');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '    End Try
    '    BindData()

    'End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtAdvRQ.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        txtStartDueDate.Text = ""
        txtEndDueDate.Text = ""

        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboCompany.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        cboOwner.SelectedIndex = -1
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

        depid = cboDepartment.SelectedValue
        objsection.SetCboSection_seccode(cboSection, depid)
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedValue)
        'searchjobslist()

        BindData()
    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedValue
        objsection.SetCboSection_seccode(cboSection, depid)
        'searchjobslist()

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            searchjobslist()
        Else
            searchjobslist_owner()
        End If
    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & Date.Now.ToString
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

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim createdate As String
        createdate = Date.Now
        Dim objnonpo As New NonPO
        Try
            ExportToExcel(itemtable, Session("usercode"), createdate)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
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
        Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column
        Return sortDirection
    End Function
End Class