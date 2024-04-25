Imports System.Drawing
Imports System.Web.Script.Serialization

Public Class KPIsRequestList
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

    Public criteria As DataTable '= createCriteria()
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


        operator_code = objKpi.KPIPermisstion("NKPI", "OP")
        If Not IsPostBack() Then


            objKpi.SetCboPeriod(cboPeriod)
            objposition.SetCboPositionCode(cboPosition)
            objKpi.SetCboRatioType(cboRatio)
            objcompany.SetCboCompany(cboCompany, 0)
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
            objdep.SetCboDepartmentByMode(cboDepartment, 0, "actived")
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
            objsec.SetCboSectionCodeNameByMode(cboSection, cboDepartment.SelectedItem.Value, "actived")



            cboCompany.SelectedIndex = 1
            If Session("positionid") = "10" Then
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If
            SetCboUsers(cboCreateby)
            SetCboUsersCO(cboCreatebyCO)

            cboCreateby.SelectedIndex = cboCreateby.Items.IndexOf(cboCreateby.Items.FindByValue(Session("userid")))
            cboCreatebyCO.SelectedIndex = cboCreatebyCO.Items.IndexOf(cboCreatebyCO.Items.FindByValue(Session("userid")))

            If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                searchjobslist()
            Else
                searchNewKpilist_owner()
            End If

        Else

            criteria = Session("criteria_newkpi")
            itemtable = Session("newkpiitem")
        End If

    End Sub
    Private Sub searchjobslist()
        Dim objKpi As New Kpi
        Dim objMemo As New Memo

        Dim detailtable As New DataTable
        Try
            If chkCO.Checked Then
                itemtable = objKpi.newkpi_For_Operator("",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        "",
                                                        "",
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCreatebyCO.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "CO",
                                                        cboPeriod.SelectedValue.ToString)
            ElseIf chkHO.Checked Then
                itemtable = objKpi.newkpi_For_Operator(cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboRatio.SelectedItem.Value.ToString,
                                                        cboPosition.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "HO",
                                                        cboPeriod.SelectedValue.ToString)
            End If

            Session("newkpiitem") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindData()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        'setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        gvRemind.Caption = "ทั้งหมด " & itemtable.Rows.Count & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            searchjobslist()
        Else
            searchNewKpilist_owner()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch

        cboCompany.SelectedIndex = 1
        cboPeriod.SelectedIndex = -1
        cboCreatebyCO.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        cboRatio.SelectedIndex = -1
        cboPosition.SelectedIndex = -1
        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1


        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("newkpiitem") = itemtable
        Session("criteria_newkpi") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSectionCodeNameByMode(cboSection, depid, "actived")
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)

        BindData()
    End Sub
    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 6
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("StatusName") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("StatusName") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("StatusName") = "ได้รับการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Green
            ElseIf Data.Item("StatusName") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            End If

        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("KPIsRequest.aspx")
    End Sub


    Private Sub searchNewKpilist_owner()
        Dim objKpi As New Kpi
        Dim objMemo As New Memo

        Dim detailtable As New DataTable
        Try
            If chkCO.Checked Then
                itemtable = objKpi.newkpi_For_Owner("",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        "",
                                                        "",
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCreatebyCO.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "CO",
                                                        cboPeriod.SelectedValue.ToString)
            ElseIf chkHO.Checked Then
                itemtable = objKpi.newkpi_For_Owner(cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboRatio.SelectedItem.Value.ToString,
                                                        cboPosition.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "HO",
                                                        cboPeriod.SelectedValue.ToString)
            End If

            Session("newkpiitem") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class