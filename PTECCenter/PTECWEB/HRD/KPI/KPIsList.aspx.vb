Imports System.Drawing
Imports System.Web.Script.Serialization

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

    Public cntdt As Integer
    Public cntkpi As Integer

    Public operator_code As String = ""
    Public adm_code As String = ""

    Public criteria As DataTable = createCriteria()
    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objposition As New Position
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

        logo.Src = Page.ResolveUrl("~/icon/Logo_pure.png") 'แสดง card PURE
        company_th.InnerText = "บริษัท เพียวพลังงานไทย จำกัด"
        company_en.InnerText = "PURE THAI ENERGY COMPANY LIMITED"



        'If Session("positionid") = "10" Or Session("positionid") = "15" Then '10 PM , 15 AA
        'Response.Redirect("KPIsListForBranch.aspx")
        'End If


        operator_code = objKpi.KPIPermisstion("KPI", "OP")
        adm_code = objKpi.KPIPermisstion("KPI", "A")

        If Not IsPostBack() Then


            objKpi.SetCboPeriod(cboPeriod)
            objposition.SetCboPositionCode(cboPosition)
            objKpi.SetCboRatioType(cboRatio)
            objcompany.SetCboCompany(cboCompany, 0)
            objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranchByBranchGroupID(cboBranch, cboBranchGroup.SelectedItem.Value)
            objdep.SetCboDepartmentByMode(cboDepartment, 0, "actived")
            'cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
            objsec.SetCboSectionCodeNameByMode(cboSection, cboDepartment.SelectedItem.Value, "actived")
            'objbranch.SetCboBranchManager(cboBranchManager)



            cboCompany.SelectedIndex = 1
            If Session("positionid") = "10" Or Session("positionid") = "15" Then '10 PM , 15 AA
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If
            SetCboUsers(cboCreateby)
            SetCboUsersCO(cboBranchManager)

            cboCreateby.SelectedIndex = cboCreateby.Items.IndexOf(cboCreateby.Items.FindByValue(Session("userid")))
            cboBranchManager.SelectedIndex = cboBranchManager.Items.IndexOf(cboBranchManager.Items.FindByValue(Session("userid")))
            '------------------------------------
            find()


            Session("kpilist_allkpi") = AllKpi
        Else
            Dim target = Request.Form("__EVENTTARGET")
            If target = "deletehead" Then
                Dim argument As String = Request("__EVENTARGUMENT")
                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(argument)
                deleteHead(json("kpicode"), json("user"))
            ElseIf target = "overview" Then

                Dim argument As String = Request("__EVENTARGUMENT")

                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(argument)

                Dim typeuser As String = ""
                If chkCO.Checked Then
                    typeuser = "CO"
                ElseIf chkHO.Checked Then
                    typeuser = "HO"
                End If
                Response.Redirect("KPIsOverview.aspx?p=" & json("periodid") & "&t=" & typeuser & "&uc=" & json("user"))
            End If


            AllKpi = Session("kpilist_allkpi")

        End If

    End Sub
    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("cboCompany", GetType(String))
        dt.Columns.Add("cboPeriod", GetType(String))
        dt.Columns.Add("cboCreateby", GetType(String))
        dt.Columns.Add("cboRatio", GetType(String))
        dt.Columns.Add("cboPosition", GetType(String))
        dt.Columns.Add("cboDepartment", GetType(String))
        dt.Columns.Add("cboSection", GetType(String))
        dt.Columns.Add("cboBranchGroup", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))
        dt.Columns.Add("chkCO", GetType(Boolean))
        dt.Columns.Add("chkHO", GetType(Boolean))

        Return dt
    End Function

    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then

            Dim objsec As New Section

            cboCompany.SelectedValue = criteria.Rows(0).Item("cboCompany")
            cboPeriod.SelectedValue = criteria.Rows(0).Item("cboPeriod")
            cboCreateby.SelectedValue = criteria.Rows(0).Item("cboCreateby")
            cboRatio.SelectedValue = criteria.Rows(0).Item("cboRatio")
            cboPosition.SelectedValue = criteria.Rows(0).Item("cboPosition")
            cboDepartment.SelectedValue = criteria.Rows(0).Item("cboDepartment")
            objsec.SetCboSectionCodeNameByMode(cboSection, cboDepartment.SelectedItem.Value, "actived")
            cboSection.SelectedValue = criteria.Rows(0).Item("cboSection")
            cboBranchGroup.SelectedValue = criteria.Rows(0).Item("cboBranchGroup")
            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")

            chkCO.Checked = criteria.Rows(0).Item("chkCO")
            chkHO.Checked = criteria.Rows(0).Item("chkHO")
        End If
    End Sub
    Private Sub searchKpilist()

        Dim objKpi As New Kpi
        Try
            If chkCO.Checked Then
                AllKpi = objKpi.Kpi_List_For_Operator("",
                                                      "",
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        "",
                                                        "",
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        "",
                                                        Session("userid").ToString,
                                                        "CO",
                                                        cboPeriod.SelectedValue.ToString,
                                                        cboBranchManager.SelectedItem.Value.ToString)
            ElseIf chkHO.Checked Then
                AllKpi = objKpi.Kpi_List_For_Operator(cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboRatio.SelectedItem.Value.ToString,
                                                        cboPosition.SelectedItem.Value.ToString,
                                                      "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "HO",
                                                        cboPeriod.SelectedValue.ToString,
                                                        "")
            End If

            setCriteria()
            Session("kpilist_allkpi") = AllKpi
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub


    Private Sub searchKpilist_owner()
        Dim objKpi As New Kpi
        Try
            If chkCO.Checked Then
                AllKpi = objKpi.Kpi_List_For_Owner("",
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        cboBranchGroup.SelectedItem.Value.ToString,
                                                        cboBranch.SelectedItem.Value.ToString,
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                        "CO",
                                                        cboPeriod.SelectedValue.ToString,
                                                        cboBranchManager.SelectedItem.Value.ToString)
            ElseIf chkHO.Checked Then
                AllKpi = objKpi.Kpi_List_For_Owner(cboDepartment.SelectedItem.Value.ToString,
                                                        cboSection.SelectedItem.Value.ToString,
                                                        cboCompany.SelectedItem.Value.ToString,
                                                        cboRatio.SelectedItem.Value.ToString,
                                                        cboPosition.SelectedItem.Value.ToString,
                                                        "",
                                                      "",
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        Session("userid").ToString,
                                                    "HO",
                                                        cboPeriod.SelectedValue.ToString,
                                                        "")
            End If

            setCriteria()
            Session("kpilist_allkpi") = AllKpi
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            searchKpilist()
        Else
            searchKpilist_owner()
        End If
    End Sub

    Private Sub setCriteria()
        'criteria = createCriteria()
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        criteria.Rows.Add(
                          (cboCompany.SelectedItem.Value),
                          (cboPeriod.SelectedItem.Value),
                          cboCreateby.SelectedItem.Value.ToString,
                        cboRatio.SelectedItem.Value.ToString,
                        cboPosition.SelectedItem.Value.ToString,
                          (cboDepartment.SelectedItem.Value),
                          (cboSection.SelectedItem.Value),
                          (cboBranchGroup.SelectedItem.Value),
                          (cboBranch.SelectedItem.Value),
                            chkCO.Checked,
                          chkHO.Checked)

        cntdt = AllKpi.Tables(0).Rows.Count
        cntkpi = AllKpi.Tables(1).Rows.Count
        Session("criteria_kpi") = criteria
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch

        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboCompany.SelectedIndex = 1
        cboPeriod.SelectedIndex = -1
        cboRatio.SelectedIndex = -1
        cboPosition.SelectedIndex = -1
        cboBranchGroup.SelectedIndex = -1
        cboBranch.SelectedIndex = -1
        cboBranchManager.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        If AllKpi IsNot Nothing Then
            AllKpi.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("kpilist_allkpi") = AllKpi
        Session("criteria_kpi") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSectionCodeNameByMode(cboSection, depid, "actived")
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
        objsection.SetCboSectionCodeNameByMode(cboSection, depid, "actived")
    End Sub
    Private Sub find()
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            If Not Session("criteria_kpi") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                criteria = Session("criteria_kpi")
                BindCriteria(criteria)
                searchKpilist()
            Else

                searchKpilist()
            End If
        Else
            If Not Session("criteria_kpi") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                criteria = Session("criteria_kpi")
                BindCriteria(criteria)
                searchKpilist_owner()
            Else
                searchKpilist_owner()
            End If

        End If
    End Sub

    Private Sub deleteHead(kpicode As String, user As String)
        Dim objKpi As New Kpi

        Try
            objKpi.deleteHeadbyKPICode(kpicode, user)
            find()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class