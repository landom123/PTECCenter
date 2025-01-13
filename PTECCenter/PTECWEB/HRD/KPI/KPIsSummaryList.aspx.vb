Imports System.Drawing
Imports System.Web.Script.Serialization

Public Class KPIsSummaryList
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Public PermissionOwner As DataSet '= createtable()
    Public Shared AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public AllKpi As DataSet

    Public verify As Boolean = False
    Public approval As Boolean = False
    Public flag As Boolean = True

    Public allOwner As String
    Public at As String
    Public approver As String
    Public verifier As String
    Public now_action As String

    Dim md_code As String
    Dim fm_code As String
    Dim dm_code As String
    Dim sm_code As String
    Dim am_code As String

    Public cntdt As Integer
    Public cntkpi As Integer

    Public operator_code As String = ""
    Public adm_code As String = ""

    Public criteria As DataTable = createCriteria()
    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objposition As New Position
        Dim objKpi As New Kpi
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objNonpo As New NonPO
        Dim objMemo As New Memo
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



        operator_code = objKpi.KPIPermisstion("SKPI", "OP")
        adm_code = objKpi.KPIPermisstion("SKPI", "A")
        If Not IsPostBack() Then


            objKpi.SetCboPeriod(cboPeriod)
            objKpi.SetCboForms_by_periodid(cboForms, cboPeriod.SelectedItem.Value)
            objposition.SetCboPositionCode(cboPosition)

            objcompany.SetCboCompany(cboCompany, 0)
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
            objdep.SetCboDepartmentByMode(cboDepartment, 0, "actived")
            If Not operator_code.IndexOf(Session("usercode").ToString) > -1 Then cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
            objsec.SetCboSectionCodeNameByMode(cboSection, cboDepartment.SelectedItem.Value, "actived")
            If Not operator_code.IndexOf(Session("usercode").ToString) > -1 Then cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(Session("secid").ToString))



            cboCompany.SelectedIndex = 1
            If Session("positionid") = "10" Then
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If
            SetCboUsers(cboCreateby)
            SetCboUsersCO(cboCreatebyCO)

            SetCboUsers(cboApproverby, "", "ALL")
            'If Not operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            '    cboCreateby.SelectedIndex = cboCreateby.Items.IndexOf(cboCreateby.Items.FindByValue(Session("userid")))
            '    cboCreatebyCO.SelectedIndex = cboCreatebyCO.Items.IndexOf(cboCreatebyCO.Items.FindByValue(Session("userid")))
            'End If


            If Not Session("criteria_KpiSummary") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                criteria = Session("criteria_KpiSummary")
                BindCriteria(criteria)
            End If
            searchjobslist()

        Else

            criteria = Session("criteria_KpiSummary")
            itemtable = ViewState("newkpiitem")
        End If

    End Sub
    Private Sub searchjobslist()
        Dim objKpi As New Kpi
        Dim objMemo As New Memo

        Dim detailtable As New DataTable
        Try
            If chkCO.Checked Then
                itemtable = objKpi.Kpi_FormsRes_List_For_Owner("",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        "",
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCreatebyCO.SelectedItem.Value.ToString,
                                                        cboApproverby.SelectedItem.Value.ToString,
                                                        Session("userid"),
                                                        "CO",
                                                        cboPeriod.SelectedValue.ToString,
                                                        cboForms.SelectedValue.ToString,
                                                        cboMaxRows.SelectedValue)
            ElseIf chkHO.Checked Then
                itemtable = objKpi.Kpi_FormsRes_List_For_Owner(cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboPosition.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        cboApproverby.SelectedItem.Value.ToString,
                                                        Session("userid"),
                                                        "HO",
                                                        cboPeriod.SelectedValue.ToString,
                                                        cboForms.SelectedValue.ToString,
                                                        cboMaxRows.SelectedValue)
            End If





            ViewState("newkpiitem") = itemtable
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
        gvRemind.Caption = "ทั้งหมด " & itemtable.Rows.Count & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchjobslist()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch

        cboCompany.SelectedIndex = 1
        cboPeriod.SelectedIndex = -1
        cboForms.SelectedIndex = -1
        cboCreatebyCO.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        cboApproverby.SelectedIndex = -1
        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboPosition.SelectedIndex = -1


        If itemtable IsNot Nothing Then itemtable.Rows.Clear()
        If criteria IsNot Nothing Then criteria.Rows.Clear()

        ViewState("newkpiitem") = itemtable
        Session("criteria_KpiSummary") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSectionCodeNameByMode(cboSection, depid, "actived")
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)

        BindData()
    End Sub
    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 11
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then Return

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("StatusTH") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("StatusTH") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("StatusTH") = "ได้รับการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Green
            ElseIf Data.Item("StatusTH") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            End If



            ' ค้นหา HyperLink1
            Dim btnForm As HyperLink = CType(e.Row.FindControl("btnForm"), HyperLink)
            Dim btnSummarySelf As HyperLink = CType(e.Row.FindControl("btnSummarySelf"), HyperLink)
            Dim btnSummary2Person As HyperLink = CType(e.Row.FindControl("btnSummary2Person"), HyperLink)
            Dim btnSummaryView As HyperLink = CType(e.Row.FindControl("btnSummaryView"), HyperLink)
            'Dim btnReqApproverEdit As LinkButton = CType(e.Row.FindControl("btnReqApproverEdit"), LinkButton)
            Dim btnDel As LinkButton = CType(e.Row.FindControl("btnDel"), LinkButton)

            ' ดึงค่าเงื่อนไขจาก DataItem
            Dim resCode As String = DataBinder.Eval(e.Row.DataItem, "res_code").ToString()
            Dim resStatus As String = DataBinder.Eval(e.Row.DataItem, "status").ToString()
            Dim owner_id As Integer = If(IsDBNull(DataBinder.Eval(e.Row.DataItem, "owner_id")), 0, Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "owner_id")))
            Dim Approver_id As Integer = If(IsDBNull(DataBinder.Eval(e.Row.DataItem, "Approver_id")), 0, Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Approver_id")))
            Dim ownerBeginDate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "OwnerBegin_Date"))
            Dim ownerEndDate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "OwnerEnd_Date"))
            Dim approvalBeginDate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ApprovalBegin_Date"))
            Dim approvalEndDate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ApprovalEnd_Date"))
            Dim targetDate As DateTime = DateTime.Now

            Dim in_OwnerPeriod As Boolean = False
            Dim in_ApproverPeriod As Boolean = False

            ' ไม่อยู่ในช่วงประเมิน
            If targetDate > ownerBeginDate And targetDate < ownerEndDate Then in_OwnerPeriod = True
            If targetDate > approvalBeginDate And targetDate < approvalEndDate Then in_ApproverPeriod = True

            '[Status] = 'submitted' -> 'รออนุมัติ' 
            '[Status] = 'approved' -> 'ได้รับการอนุมัติ' 

            'default
            btnForm.Visible = False
            btnSummarySelf.Visible = False
            btnSummary2Person.Visible = False
            btnSummaryView.Visible = False
            btnDel.Visible = False

            ' เงื่อนไขในการแสดงหรือซ่อน
            If resStatus = "submitted" Then
                If Session("userid") = owner_id Then 'owner

                    btnForm.Visible = False
                    btnSummarySelf.Visible = True
                    btnSummary2Person.Visible = False
                    btnSummaryView.Visible = False
                    btnDel.Visible = False

                ElseIf Session("userid") = Approver_id Then 'approver

                    If in_ApproverPeriod Then
                        btnForm.Visible = True
                    Else
                        btnForm.Visible = False
                    End If
                    btnSummarySelf.Visible = False
                    btnSummary2Person.Visible = True
                    btnSummaryView.Visible = True
                    btnDel.Visible = False

                ElseIf adm_code.IndexOf(Session("usercode").ToString) > -1 Then 'admin

                    btnForm.Visible = False
                    btnSummarySelf.Visible = False
                    btnSummary2Person.Visible = True
                    btnSummaryView.Visible = True
                    btnDel.Visible = True

                Else 'other can view

                    btnForm.Visible = False
                    btnSummarySelf.Visible = False
                    btnSummary2Person.Visible = True
                    btnSummaryView.Visible = True
                    btnDel.Visible = False
                End If
            ElseIf resStatus = "approved" Then
                If Session("userid") = owner_id Then 'owner

                    btnForm.Visible = False
                    btnSummarySelf.Visible = True
                    btnSummary2Person.Visible = False
                    btnSummaryView.Visible = False
                    btnDel.Visible = False

                ElseIf Session("userid") = Approver_id Then 'approver

                    btnForm.Visible = False
                    btnSummarySelf.Visible = False
                    btnSummary2Person.Visible = True
                    btnSummaryView.Visible = True
                    btnDel.Visible = False

                ElseIf adm_code.IndexOf(Session("usercode").ToString) > -1 Then 'admin

                    btnForm.Visible = False
                    btnSummarySelf.Visible = False
                    btnSummary2Person.Visible = True
                    btnSummaryView.Visible = True
                    btnDel.Visible = True

                Else 'other can view

                    btnForm.Visible = False
                    btnSummarySelf.Visible = False
                    btnSummary2Person.Visible = True
                    btnSummaryView.Visible = True
                    btnDel.Visible = False
                End If
            End If

        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("KPIsSummary.aspx")
    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
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

    Private Sub cboPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim objKpi As New Kpi
        objKpi.SetCboForms_by_periodid(cboForms, cboPeriod.SelectedItem.Value)
    End Sub

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("chkCO", GetType(Boolean))
        dt.Columns.Add("chkHO", GetType(Boolean))
        dt.Columns.Add("cboCompany", GetType(String))
        dt.Columns.Add("cboPeriod", GetType(String))
        dt.Columns.Add("cboForms", GetType(String))
        dt.Columns.Add("cboCreateby", GetType(String))
        dt.Columns.Add("cboCreatebyCO", GetType(String))
        dt.Columns.Add("cboApproverby", GetType(String))
        dt.Columns.Add("cboPosition", GetType(String))
        dt.Columns.Add("cboDep", GetType(String))
        dt.Columns.Add("cboSec", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))
        dt.Columns.Add("maxrows", GetType(Integer))

        Return dt
    End Function

    Private Sub setCriteria()
        criteria = createCriteria()
        criteria.Rows.Clear()
        criteria.Rows.Add(chkCO.Checked,
                              chkHO.Checked,
                                cboCompany.SelectedValue,
                                cboPeriod.SelectedValue,
                                cboForms.SelectedValue,
                                cboCreateby.SelectedValue,
                                cboCreatebyCO.SelectedValue,
                                cboApproverby.SelectedValue,
                            cboPosition.SelectedValue,
                              (cboDepartment.SelectedValue),
                              (cboSection.SelectedValue),
                              (cboBranchGroup.SelectedValue),
                              (cboBranch.SelectedValue),
                              gvRemind.PageIndex,
                                cboMaxRows.SelectedValue)
        Session("criteria_KpiSummary") = criteria
    End Sub
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then

            cboCompany.SelectedValue = criteria.Rows(0).Item("cboCompany")
            cboPeriod.SelectedValue = criteria.Rows(0).Item("cboPeriod")
            cboForms.SelectedValue = criteria.Rows(0).Item("cboForms")
            cboCreateby.SelectedValue = criteria.Rows(0).Item("cboCreateby")
            cboCreatebyCO.SelectedValue = criteria.Rows(0).Item("cboCreatebyCO")
            cboApproverby.SelectedValue = criteria.Rows(0).Item("cboApproverby")
            cboPosition.SelectedValue = criteria.Rows(0).Item("cboPosition")

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

            chkCO.Checked = criteria.Rows(0).Item("chkCO")
            chkHO.Checked = criteria.Rows(0).Item("chkHO")

            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")
            cboMaxRows.SelectedValue = criteria.Rows(0).Item("maxrows")
        End If
    End Sub
    Private Sub gvRemind_PreRender(sender As Object, e As EventArgs) Handles gvRemind.PreRender
        Dim headColAdmin As Integer = 12

        If adm_code.IndexOf(Session("usercode").ToString) > -1 Then
            Dim grid As GridView = CType(sender, GridView)
            ' เปลี่ยน HeaderText ของคอลัมน์

            ' เปลี่ยน HeaderText ของคอลัมน์
            CType(grid.Columns(headColAdmin), TemplateField).HeaderText = "(Admin)"
            CType(grid.Columns(headColAdmin), TemplateField).HeaderStyle.CssClass = "text-info"


            ' Bind Grid อีกครั้งเพื่อแสดง Header ที่เปลี่ยนแปลง
            grid.DataBind()
        End If
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function reqEdit(ByVal res_code As String, ByVal note As String, ByVal userid As Integer)
        Dim objKpi As New Kpi
        Try
            objKpi.Kpi_FormsRes_Del_by_Code(res_code, note, userid)

        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function reqDel(ByVal res_code As String, ByVal note As String, ByVal userid As Integer)
        Dim objKpi As New Kpi
        Try
            objKpi.Kpi_FormsRes_Del_by_Code(res_code, note, userid)

        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function


End Class