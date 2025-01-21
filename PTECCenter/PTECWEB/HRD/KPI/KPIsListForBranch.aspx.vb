Imports System.Drawing
Imports System.Web.Script.Serialization

Public Class KPIsListForBranch
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

        logo.Src = Page.ResolveUrl("~/icon/Logo_pure.png") 'แสดง card PURE
        company_th.InnerText = "บริษัท เพียวพลังงานไทย จำกัด"
        company_en.InnerText = "PURE THAI ENERGY COMPANY LIMITED"


        If Not Session("positionid") = "10" And Not Session("positionid") = "15" Then '10 PM , 15 AA
            Response.Redirect("KPIsList.aspx")
        End If

        txtCardID.Attributes.Remove("type")
        txtCardID.Attributes.Add("type", "password")

        If Not IsPostBack() Then

            'objposition.SetCboPositionCode(cboPosition)
            objbranch.SetCboBranchManager(cboBranchManager)
            'objcompany.SetCboCompany(cboCompany, 0)
            'objbranch.SetComboBranchGroup(cboBranchGroup)
            objbranch.SetComboBranchByManagerCardid(cboBranch, txtCardID.Text)
            'objdep.SetCboDepartmentByMode(cboDepartment, 0, "actived")
            'cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
            'objsec.SetCboSectionCodeNameByMode(cboSection, cboDepartment.SelectedItem.Value, "actived")


            If Not Session("criteriabranch_kpi") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                criteria = Session("criteriabranch_kpi")
                BindCriteria(criteria)
                searchKpilist_ownerBranch()
            End If


            'cboCompany.SelectedIndex = 1
            'If Session("positionid") = "10" Or Session("positionid") = "15" Then '10 PM , 15 AA
            '    chkCO.Checked = True
            'Else
            '    chkHO.Checked = True
            'End If
            'SetCboUsers(cboCreateby)

            ''------------------------------------
            'find()


            Session("kpilistbranch_allkpi") = AllKpi
        Else
            Dim target = Request.Form("__EVENTTARGET")
            If target = "overview" Then

                Dim argument As String = Request("__EVENTARGUMENT")
                Dim typeuser As String = "CO"
                Response.Redirect("KPIsOverview.aspx?p=1&t=" & typeuser & "&uc=" & argument)
            End If

            AllKpi = Session("kpilistbranch_allkpi")

        End If

    End Sub

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("cboBranchManager", GetType(String))
        dt.Columns.Add("txtCardID", GetType(String))
        dt.Columns.Add("cboBranch", GetType(String))

        Return dt
    End Function

    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then

            Dim objsec As New Section

            cboBranchManager.SelectedValue = criteria.Rows(0).Item("cboBranchManager")
            txtCardID.Text = criteria.Rows(0).Item("txtCardID")
            cboBranch.SelectedValue = criteria.Rows(0).Item("cboBranch")

        End If
    End Sub

    Private Sub searchKpilist_ownerBranch()
        Dim objKpi As New Kpi
        Dim branchid As String
        Try
            branchid = cboBranch.SelectedItem.Value.ToString
        Catch ex As Exception
            branchid = 0
        End Try

        Try
            AllKpi = objKpi.Kpi_List_For_Branch(cboBranchManager.SelectedItem.Value.ToString,
                                                        txtCardID.Text.ToString,
                                                        branchid,
                                                        Session("userid").ToString)

            setCriteria()
            Session("kpilistbranch_allkpi") = AllKpi
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        If validatedata() Then
            searchKpilist_ownerBranch()


        Else
            txtCardID.Focus()
        End If
    End Sub

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        If Not cboBranchManager.SelectedIndex > 0 Then
            result = False
            msg = "กรุณาเลือกชื่อผู้จัดการ (สาขาที่ดูแล)"
            GoTo endprocess
        End If
        If String.IsNullOrEmpty(txtCardID.Text) And Not txtCardID.Text.Length = 4 Then
            result = False
            msg = "กรุณาระบุรหัสผ่านให้ถูกต้อง"
            GoTo endprocess
        Else

            Dim usertable As DataTable

            Dim objbranch As New Branch
            usertable = objbranch.Manager_Login(cboBranchManager.SelectedItem.Value, txtCardID.Text)

            Session("managername") = usertable.Rows(0).Item("name")
            If usertable.Rows(0).Item("password") = 2 Then
                result = False
                msg = "กรุณาระบุรหัสผ่านให้ถูกต้อง"
                GoTo endprocess
            Else
            End If
            If cboBranch.Items.Count <= 1 Then
                objbranch.SetComboBranchByManagerCardid(cboBranch, txtCardID.Text)
            End If
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function

    Private Sub setCriteria()
        'criteria = createCriteria()
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Dim branchid As String
        Try
            branchid = cboBranch.SelectedItem.Value.ToString
        Catch ex As Exception
            branchid = 0
        End Try
        criteria.Rows.Add(
                          (cboBranchManager.SelectedItem.Value),
                          txtCardID.Text,
                          branchid)

        cntdt = AllKpi.Tables(0).Rows.Count
        cntkpi = AllKpi.Tables(1).Rows.Count
        Session("criteriabranch_kpi") = criteria
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch

        cboBranchManager.SelectedIndex = -1
        txtCardID.Text = ""
        cboBranch.SelectedIndex = -1
        If AllKpi IsNot Nothing Then
            AllKpi.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("kpilistbranch_allkpi") = AllKpi
        Session("criteriabranch_kpi") = criteria



    End Sub


End Class