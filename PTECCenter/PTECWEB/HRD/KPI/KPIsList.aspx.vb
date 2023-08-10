Imports System.Drawing

Public Class KPIsList
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

    Public account_code As String = ""

    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objbranch As New Branch
        Dim objKpi As New Kpi
        Dim objdep As New Department
        Dim objcompany As New Company
        Dim objsec As New Section

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
        usercode = Session("usercode")


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        logo.Src = Page.ResolveUrl("~/icon/Logo_pure.png") 'แสดง card PURE
        company_th.InnerText = "บริษัท เพียวพลังงานไทย จำกัด"
        company_en.InnerText = "PURE THAI ENERGY COMPANY LIMITED"

        If Not IsPostBack() Then

            objcompany.SetCboCompany(cboCompany, 0)
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)

            cboCompany.SelectedIndex = 1
            If Session("positionid") = "10" Then
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If
            SetCboUsers(cboCreateby)

            'searchKpilist()


            Session("kpilist_allkpi") = AllKpi
        Else

            AllKpi = Session("kpilist_allkpi")
        End If

    End Sub

    Private Sub searchKpilist()

        Dim objKpi As New Kpi
        Try
            If chkCO.Checked Then
                AllKpi = objKpi.Kpi_Find_Follow("",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "CO")
            ElseIf chkHO.Checked Then
                AllKpi = objKpi.Kpi_Find_Follow(cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                    "HO")
            End If


            Session("kpilist_allkpi") = AllKpi
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchKpilist()
    End Sub


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch

        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboCompany.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        If AllKpi IsNot Nothing Then
            AllKpi.Clear()
        End If
        'If criteria IsNot Nothing Then
        '    criteria.Rows.Clear()
        'End If
        Session("kpilist_allkpi") = AllKpi
        'Session("criteria_clearadvlist") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)

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
End Class