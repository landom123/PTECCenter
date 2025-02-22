﻿Imports System.Drawing
Imports System.IO
Imports ClosedXML.Excel

Public Class ClearAdvanceMenuList2
    Inherits System.Web.UI.Page

    Public criteria As DataTable = createCriteria()
    Public menutable As DataTable
    Public itemtable As DataTable

    Public cntdt As Integer

    Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objNonpo As New NonPO
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objjob As New jobs
        Dim objcompany As New Company
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

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########


        'txtStartDate.Attributes.Add("readonly", "readonly")
        'txtEndDate.Attributes.Add("readonly", "readonly")

        operator_code = objNonpo.NonPOPermisstionOperator("CLADV")
        If Not IsPostBack() Then

            objNonpo.SetCboStatusbyNonpocategory(cboStatusFollow, "CLADV")
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranch(cboBranch, "")
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)
            objcompany.SetCboCompany(cboCompany, 0)
            SetCboUsers(cboCreateby)
            SetCboUsers(cboOwner)
            approval.SetCboApproval(cboApproval, Session("depid"), "", Session("secid"))
            If Session("positionid") = "10" Or Session("positionid") = "15" Then
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If

            If operator_code.IndexOf(Session("usercode").ToString) > -1 Then


                '------------------------------------
                If Not Session("criteria_clearadvlist") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_clearadvlist")
                    BindCriteria(criteria)
                    searchjobslist()
                Else
                    searchjobslist()
                End If
            Else
                If Not Session("criteria_clearadvlist") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_clearadvlist")
                    BindCriteria(criteria)
                    searchjobslist_owner()
                Else
                    searchjobslist_owner()
                End If

            End If
        Else

            criteria = Session("criteria_clearadvlist")
            itemtable = Session("advlist")
        End If


    End Sub

    Private Sub setCriteria()
        'criteria = createCriteria()
        criteria.Rows.Clear()
        criteria.Rows.Add(chkCO.Checked,
                          chkHO.Checked,
                          txtclearadv.Text.ToString.Trim(),
                          txtcoderef.Text.ToString.Trim(),
                          (cboStatusFollow.SelectedItem.Value),
                          txtStartDate.Text.ToString.Trim(),
                          txtEndDate.Text.ToString.Trim(),
                          (cboDepartment.SelectedItem.Value),
                          (cboSection.SelectedItem.Value),
                          (cboCompany.SelectedItem.Value),
                          (cboBranchGroup.SelectedItem.Value),
                          (cboBranch.SelectedItem.Value),
                            cboCreateby.SelectedItem.Value.ToString,
                            cboOwner.SelectedItem.Value.ToString,
                            cboApproval.SelectedItem.Value.ToString,
                          gvRemind.PageIndex,
                            cboMaxRows.SelectedValue)
        Session("criteria_clearadvlist") = criteria
    End Sub
    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("chkCO", GetType(Boolean))
        dt.Columns.Add("chkHO", GetType(Boolean))
        dt.Columns.Add("txtclearadv", GetType(String))
        dt.Columns.Add("txtcoderef", GetType(String))
        dt.Columns.Add("cboStatusFollow", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))
        dt.Columns.Add("cboDep", GetType(String))
        dt.Columns.Add("cboSec", GetType(String))
        dt.Columns.Add("cboCom", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("cboCreateby", GetType(String))
        dt.Columns.Add("cboOwner", GetType(String))
        dt.Columns.Add("cboApproval", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))
        dt.Columns.Add("maxrows", GetType(Integer))

        Return dt
    End Function

    'Private Function createdetailtable() As DataTable
    '    Dim dt As New DataTable

    '    dt.Columns.Add("jobcode", GetType(String))
    '    dt.Columns.Add("jobdate", GetType(Date))
    '    dt.Columns.Add("jobtype", GetType(String))
    '    dt.Columns.Add("details", GetType(String))
    '    dt.Columns.Add("status", GetType(String))
    '    dt.Columns.Add("closedate", GetType(Date))
    '    dt.Columns.Add("detailFollow", GetType(String))
    '    dt.Columns.Add("cost", GetType(Double))
    '    dt.Columns.Add("link", GetType(String))

    '    Return dt
    'End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtclearadv.Text = criteria.Rows(0).Item("txtclearadv")
            txtcoderef.Text = criteria.Rows(0).Item("txtcoderef")

            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")
            cboMaxRows.SelectedValue = criteria.Rows(0).Item("maxrows")

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
            cboCompany.SelectedValue = criteria.Rows(0).Item("cboCom")
            cboCreateby.SelectedValue = criteria.Rows(0).Item("cboCreateby")
            cboOwner.SelectedValue = criteria.Rows(0).Item("cboOwner")

            cboApproval.SelectedValue = criteria.Rows(0).Item("cboApproval")

            chkCO.Checked = criteria.Rows(0).Item("chkCO")
            chkHO.Checked = criteria.Rows(0).Item("chkHO")
        End If
    End Sub
    Private Sub searchjobslist()

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            If chkCO.Checked Then
                itemtable = objNonPO.ClearAdvanceList_For_Operator(txtclearadv.Text.Trim(),
                                                                txtcoderef.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        cboOwner.SelectedItem.Value.ToString,
                                                        "CO",
                                                        cboApproval.SelectedItem.Value.ToString,
                                                        cboMaxRows.SelectedValue)
            ElseIf chkHO.Checked Then
                itemtable = objNonPO.ClearAdvanceList_For_Operator(txtclearadv.Text.Trim(),
                                                                txtcoderef.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                        cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        cboOwner.SelectedItem.Value.ToString,
                                                    "HO",
                                                        cboApproval.SelectedItem.Value.ToString,
                                                        cboMaxRows.SelectedValue)
            Else
                itemtable = objNonPO.ClearAdvanceList_For_Operator(txtclearadv.Text.Trim(),
                                                                txtcoderef.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                        "",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        cboOwner.SelectedItem.Value.ToString,
                                                        "",
                                                        cboApproval.SelectedItem.Value.ToString,
                                                        cboMaxRows.SelectedValue)
            End If



            Session("advlist") = itemtable
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
                itemtable = objNonPO.ClearAdvanceList_For_Owner(txtclearadv.Text.Trim(),
                                                                txtcoderef.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        Session("userid"),
                                                        "CO",
                                                        cboMaxRows.SelectedValue)
            ElseIf chkHO.Checked Then
                itemtable = objNonPO.ClearAdvanceList_For_Owner(txtclearadv.Text.Trim(),
                                                                txtcoderef.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        Session("userid"),
                                                    "HO",
                                                        cboMaxRows.SelectedValue)
            Else
                itemtable = objNonPO.ClearAdvanceList_For_Owner(txtclearadv.Text.Trim(),
                                                                txtcoderef.Text.Trim(),
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        Session("userid"),
                                                        "",
                                                        cboMaxRows.SelectedValue)
            End If



            Session("advlist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindData()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        cntdt = itemtable.Rows.Count
        gvRemind.Caption = "ทั้งหมด " & cntdt & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()


        'hide/show Columns GridView
        Dim colDuedate As Integer = 6
        Dim colOwnerApprover As Integer = 9
        Dim colApprovalname As Integer = 7
        gvRemind.Columns(colApprovalname).Visible = chkCO.Checked
        gvRemind.Columns(colOwnerApprover).Visible = chkHO.Checked
        gvRemind.Columns(colDuedate).Visible = chkCO.Checked
    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            searchjobslist()
        Else
            searchjobslist_owner()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtclearadv.Text = ""
        txtcoderef.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboCompany.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        cboOwner.SelectedIndex = -1
        cboApproval.SelectedIndex = -1
        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("advlist") = itemtable
        Session("criteria_clearadvlist") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)

        BindData()
    End Sub

    Private Sub cboBranchGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranchGroup.SelectedIndexChanged
        Dim objbranch As New Branch

        cboBranch.SelectedIndex = -1
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
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
            End If




            If Data.Item("comcode") = "PURE" Then
                e.Row.Cells.Item(companyAt).ForeColor = Color.FromArgb(1, 237, 1, 128)
            ElseIf Data.Item("comcode") = "SAP" Then
                e.Row.Cells.Item(companyAt).ForeColor = Color.FromArgb(1, 0, 166, 81)
            End If
        End If

    End Sub

    'Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged

    '    Dim objNonpo As New NonPO

    '    Try
    '        'itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedItem.Value)
    '        itemtable = objNonpo.ClearAdvanceList_For_Owner(Session("userid"), cboWorking.SelectedItem.Value)
    '        Session("advlist") = itemtable

    '    Catch ex As Exception
    '        Dim scriptKey As String = "alert"
    '        Dim javaScript As String = "alertWarning('search fail');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '    End Try
    '    BindData()
    'End Sub


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