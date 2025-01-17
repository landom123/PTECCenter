Imports System.Web.DynamicData
Imports System.Web.Script.Serialization
Imports System.Windows.Interop
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports QRCoder.PayloadGenerator

Public Class KPIsForm
    Inherits System.Web.UI.Page

    Public menutable As DataTable

    Public assessor As String = Nothing

    Public groupFormsDtl As DataTable '= createmaintable()
    Public groupCbo As DataTable '= createmaintable()
    Public subGroupCbo As DataTable '= createmaintable()
    Public formsTable As DataTable '= createmaintable()
    Public formsDtlTable As DataTable '= createmaintable()
    Public formsDtlCboTable As DataTable '= createmaintable()
    Public formsDtlCareerTable As DataTable '= createmaintable()
    Public kpiTable As DataTable '= createmaintable()
    Dim ownerid As Integer

    Public ansRes_idTable As DataTable '= createmaintable()
    Public ansCareersTable As DataTable '= createmaintable()
    Public ansCboTable As DataTable '= createmaintable()
    Public ansKpisTable As DataTable '= createmaintable()
    Public ansTxtareaTable As DataTable '= createmaintable()
    Public ansProjectTable As DataTable '= createmaintable()

    Public adm_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objKpi As New Kpi
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
        'Dim total As Integer = menutable.Rows.Count - 1
        'Dim is_allowThisPage As Boolean = False
        'Dim urlCurrent As String = Request.Url.ToString().ToLower()
        'For i = 0 To total
        '    Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
        '    If Not String.IsNullOrEmpty(frmMenuUrl) Then
        '        If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
        '            is_allowThisPage = True
        '            Exit For
        '        End If
        '    End If
        'Next
        'If Not is_allowThisPage Then
        '    Response.Redirect("~/403.aspx")
        'End If
        '######## END Check Permission page  ########


        adm_code = objKpi.KPIPermisstion("SKPI", "A")

        If Request.QueryString("ow") IsNot Nothing Then
            ownerid = Request.QueryString("ow")

            'ต้องเข็คมีสิทธิเข้าถึง res ของ ow คนนี้ไหม
            'verify , approver , admin
            If Request.QueryString("f") IsNot Nothing Then
                If Request.QueryString("ow") = Session("userid") Then
                    Response.Redirect("../KPI/KPIsSummarySelf2.aspx?f=" & Request.QueryString("f") & "&ow=" & Request.QueryString("ow"))
                End If
            Else
                Response.Redirect("~/403.aspx")
            End If

        Else
            ownerid = Session("userid")
        End If

        If Not IsPostBack() Then



            If Request.QueryString("f") IsNot Nothing Then
                FindFormsDtl(Request.QueryString("f"), ownerid)

                FindAnsDtl(Request.QueryString("f"), ownerid)
            End If

            If assessor = "owner" Then
                flexRadioDefault1.Checked = True
                flexRadioDefault2.Checked = False
            End If
            If assessor = "approver" Then
                flexRadioDefault1.Checked = False
                flexRadioDefault2.Checked = True
            End If

            ViewState("assessor") = assessor

        Else
            assessor = ViewState("assessor")

            formsTable = ViewState("forms_FKPI")
            formsDtlTable = ViewState("formsDtl_FKPI")
            formsDtlCboTable = ViewState("formsDtlCbo_FKPI")
            kpiTable = ViewState("kpi_FKPI")
            formsDtlCareerTable = ViewState("formsDtlCareer_FKPI")
            groupFormsDtl = ViewState("groupFormsDtl")
            groupCbo = ViewState("groupCbo")
            subGroupCbo = ViewState("subGroupCbo")


            ansRes_idTable = ViewState("ansRes_idTable")
            ansCareersTable = ViewState("ansCareersTable")
            ansCboTable = ViewState("ansCboTable")
            ansKpisTable = ViewState("ansKpisTable")
            ansTxtareaTable = ViewState("ansTxtareaTable")
            ansProjectTable = ViewState("ansProjectTable")

            Dim target = Request.Form("__EVENTTARGET")
            Dim argument As String = Request("__EVENTARGUMENT")
            If target = "save" Then
                Dim jsonArray As JArray = JArray.Parse(argument)
                Dim cntArr As Integer = jsonArray.Count()

                If (cntArr > 0) Then
                    save(jsonArray)
                End If
            ElseIf (target = "submit") Then
                Dim jsonArray As JArray = JArray.Parse(argument)
                Dim cntArr As Integer = jsonArray.Count()

                If (cntArr > 0) Then
                    save(jsonArray)
                    ownerSubmit(Session("userid"), Request.QueryString("f"))
                End If
            ElseIf (target = "accept") Then
                Dim jsonArray As JArray = JArray.Parse(argument)
                Dim cntArr As Integer = jsonArray.Count()

                If (cntArr > 0) Then
                    save(jsonArray)
                    approverSubmit(ownerid, Session("userid"), Request.QueryString("f"))
                End If
            End If
        End If

        SetBtn(assessor)
    End Sub

    Private Sub SetBtn(now_assessor As String)
        If now_assessor = "owner" Then
            chkKpiComplete.Visible = True
            lbchkKpiComplete.Text = "ข้าพเจ้าขอยืนยันว่าได้กรอกข้อมูลการประเมินผลงานประจำปี ถูกต้องครบถ้วนตามความเป็นจริง"
            lbchkKpiComplete.Visible = True

            btnUpload.Visible = True
            btnApproval.Visible = False
            'btnReject.Visible = False
        ElseIf now_assessor = "approver" Then
            chkKpiComplete.Visible = False
            lbchkKpiComplete.Text = "ข้าพเจ้าได้ตรวจสอบและประเมินผลพนักงาน ถูกต้องครบถ้วนตามผลการปฏิบัติงานจริง"
            lbchkKpiComplete.Visible = False

            btnUpload.Visible = False
            btnApproval.Visible = True
            'btnReject.Visible = True
        Else
            chkKpiComplete.Visible = False
            lbchkKpiComplete.Text = "-"
            lbchkKpiComplete.Visible = False

            btnUpload.Visible = False
            btnApproval.Visible = False
        End If

        If ansRes_idTable IsNot Nothing Then
            If ansRes_idTable.Rows.Count > 0 Then
                boxAlert_hasResId.Visible = Not String.IsNullOrEmpty(ansRes_idTable.Rows(0).Item("res_id").ToString)
                txtAlert_hasResId.InnerText = ansRes_idTable.Rows(0).Item("status").ToString
            End If

            'If ansRes_idTable.Rows.Count = 0 Or ansRes_idTable.Rows(0).Item("status").ToString = "draft" Then
            'setDefaultRateOwner(kpiTable)

            'End If
        Else
            boxAlert_hasResId.Visible = False
        End If



        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "updateTablesForApprover(`" & now_assessor & "`);replaceCell('" & now_assessor & "');"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    End Sub

    Private Sub FindAnsDtl(formid As Integer, ownerid As Integer)
        Dim objKpi As New Kpi
        Dim ds As New DataSet
        Try
            ds = objKpi.Kpi_Find_FormsRes_by_userid(formid, ownerid)
            '-- table 0 = res_id
            '-- table 1 = careers
            '-- table 2 = cbo
            '-- table 3 = kpis
            '-- table 4 = txtarea
            '-- table 5 = project

            ansRes_idTable = ds.Tables(0)
            ansCareersTable = ds.Tables(1)
            ansCboTable = ds.Tables(2)
            ansKpisTable = ds.Tables(3)
            ansTxtareaTable = ds.Tables(4)
            ansProjectTable = ds.Tables(5)


            setAssessor()
            convertDataSetToJSON(ds)


            ViewState("ansRes_idTable") = ansRes_idTable
            ViewState("ansCareersTable") = ansCareersTable
            ViewState("ansCboTable") = ansCboTable
            ViewState("ansKpisTable") = ansKpisTable
            ViewState("ansTxtareaTable") = ansTxtareaTable
            ViewState("ansProjectTable") = ansProjectTable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Kpi_Find_FormsRes_by_userid fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:

    End Sub

    Private Sub ownerSubmit(ownerid As Integer, formid As Integer)
        Dim objKpi As New Kpi
        Try
            objKpi.Kpi_FormsRes_Submit(ownerid, formid, Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('ownerSubmit fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsSummary.aspx")
endprocess:

    End Sub
    Private Sub approverSubmit(ownerid As Integer, approval_by As Integer, formid As Integer)
        Dim objKpi As New Kpi
        Try
            objKpi.Kpi_FormsRes_Approval(ownerid, approval_by, formid, Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('ownerSubmit fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsSummary.aspx")
endprocess:

    End Sub

    Private Sub setAssessor()
        If ansRes_idTable.Rows.Count > 0 Then
            If String.IsNullOrEmpty(ansRes_idTable.Rows(0).Item("res_id").ToString) Then
                ' new
                assessor = "owner"
            ElseIf Not String.IsNullOrEmpty(ansRes_idTable.Rows(0).Item("res_id").ToString) And ansRes_idTable.Rows(0).Item("owner_id") = Session("userid") And ansRes_idTable.Rows(0).Item("status").ToString = "draft" Then
                ' continue owner
                assessor = "owner"
            ElseIf Not String.IsNullOrEmpty(ansRes_idTable.Rows(0).Item("res_id").ToString) Then
                If ansRes_idTable.Rows(0).Item("Approver_id") = Session("userid") And ansRes_idTable.Rows(0).Item("status").ToString = "submitted" Then
                    ' continue approver
                    assessor = "approver"
                ElseIf Not ansRes_idTable.Rows(0).Item("owner_id") = Session("userid") And Not ansRes_idTable.Rows(0).Item("Approver_id") = Session("userid") And Not adm_code.IndexOf(Session("usercode").ToString) > -1 Then
                    ' other person
                    Response.Redirect("~/403.aspx")
                End If
            End If
        Else
            assessor = "owner"
        End If
    End Sub

    'Private Sub setDefaultRateOwner(dt As DataTable)
    '    ' แปลง DataTable เป็น JSON
    '    Dim ansJson As String = JsonConvert.SerializeObject(dt, Formatting.Indented)

    '    ' แปลง JSON ให้เหมาะสมสำหรับใช้งานใน JavaScript
    '    Dim encodedJson As String = HttpUtility.JavaScriptStringEncode(ansJson)

    '    ' เตรียม JavaScript
    '    Dim scriptKey As String = "UniqueKeyForThisScript"
    '    Dim javaScript As String = "setDefaultRateOwner(`" & encodedJson & "`);"

    '    ' ลงทะเบียน JavaScript
    '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    'End Sub

    Private Sub convertDataSetToJSON(ds As DataSet)
        For Each dt As DataTable In ds.Tables
            For Each row As DataRow In dt.Rows
                For Each col As DataColumn In dt.Columns
                    If row(col) IsNot DBNull.Value Then
                        row(col) = Regex.Replace(row(col).ToString(), "[\x00-\x1F\x7F]", "")
                    End If
                Next
            Next
        Next

        Dim ansJson = JsonConvert.SerializeObject(ds, Formatting.Indented)

        ansJson = ansJson.Replace("\", "\\").Replace("`", "\`")

        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String = "fetching(`" & ansJson & "`);"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

    End Sub

    Private Sub FindFormsDtl(formid As Integer, ownerid As Integer)
        Dim objKpi As New Kpi
        Dim ds As New DataSet
        Try
            ds = objKpi.Kpi_FormsDtl_by_formid_and_ownerid(formid, ownerid)
            '-- table 0 = Head
            '-- table 1 = detail
            '-- table 2 = detail_cbo
            '-- table 3 = kpi
            '-- table 4 = detail_career

            formsTable = ds.Tables(0)
            formsDtlTable = ds.Tables(1)
            formsDtlCboTable = ds.Tables(2)
            kpiTable = ds.Tables(3)
            formsDtlCareerTable = ds.Tables(4)

            getDistinctCboGroup()
            getDistinctCard()

            ViewState("forms_FKPI") = formsTable
            ViewState("formsDtl_FKPI") = formsDtlTable
            ViewState("formsDtlCbo_FKPI") = formsDtlCboTable
            ViewState("kpi_FKPI") = kpiTable
            ViewState("formsDtlCareer_FKPI") = formsDtlCareerTable
            ViewState("groupFormsDtl") = groupFormsDtl
            ViewState("groupCbo") = groupCbo
            ViewState("subGroupCbo") = subGroupCbo
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('FindForms_by_period fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:
    End Sub

    Private Sub getDistinctCard()
        If formsDtlTable IsNot Nothing Then
            If formsDtlTable.Rows.Count > 0 Then

                Dim cardGroup = formsDtlTable.AsEnumerable().
            GroupBy(Function(row) New With {
            Key .Name = row.Field(Of Integer)("card_id")
                                                })

                Dim dt As New DataTable
                dt.Columns.Add("card_id", GetType(Integer))

                Dim newrows As DataRow
                For Each grp In cardGroup
                    newrows = dt.NewRow()
                    newrows("card_id") = grp.Key.Name
                    dt.Rows.Add(newrows)
                Next
                groupFormsDtl = dt
            End If
        End If
    End Sub

    Private Sub getDistinctCboGroup()

        If formsDtlCboTable IsNot Nothing Then
            If formsDtlCboTable.Rows.Count > 0 Then
                Dim groupedData = formsDtlCboTable.AsEnumerable().GroupBy(Function(row) New With {
                                    Key .Group = row.Field(Of String)("cbo_Group"),
                                    Key .KPIFormDtlID = row.Field(Of Integer)("KPIFormDtl_ID")
                                }).Select(Function(g) New With {
                                    .Group = g.Key.Group,
                                    .KPIFormDtlID = g.Key.KPIFormDtlID,
                                    .Count = g.Count() ' นับจำนวนรายการในแต่ละกลุ่ม (ถ้าต้องการ)
                                }).ToList()

                Dim subGroupedData = formsDtlCboTable.AsEnumerable().GroupBy(Function(row) New With {
                                    Key .Group = row.Field(Of String)("cbo_Group"),
                                    Key .KPIFormDtlID = row.Field(Of Integer)("KPIFormDtl_ID"),
                                    Key .subText = row.Field(Of String)("cbo_subText")
                                }).Select(Function(g) New With {
                                    .Group = g.Key.Group,
                                    .KPIFormDtlID = g.Key.KPIFormDtlID,
                                    .subText = g.Key.subText,
                                    .Count = g.Count() ' นับจำนวนรายการในแต่ละกลุ่ม (ถ้าต้องการ)
                                }).ToList()

                ' สร้าง DataTable จากผลลัพธ์ที่ Group By
                Dim dt As New DataTable
                dt.Columns.Add("cbo_Group", GetType(String))
                dt.Columns.Add("KPIFormDtl_ID", GetType(Integer))
                dt.Columns.Add("Count", GetType(Integer)) ' คอลัมน์นี้ไม่จำเป็นถ้าคุณไม่สนใจจำนวน


                ' สร้าง DataTable จากผลลัพธ์ที่ Group By
                Dim dt2 As New DataTable
                dt2.Columns.Add("cbo_Group", GetType(String))
                dt2.Columns.Add("KPIFormDtl_ID", GetType(Integer))
                dt2.Columns.Add("cbo_subText", GetType(String))
                dt2.Columns.Add("Count", GetType(Integer)) ' คอลัมน์นี้ไม่จำเป็นถ้าคุณไม่สนใจจำนวน

                For Each item In groupedData
                    Dim newRow = dt.NewRow()
                    newRow("cbo_Group") = item.Group
                    newRow("KPIFormDtl_ID") = item.KPIFormDtlID
                    newRow("Count") = item.Count ' ไม่จำเป็นถ้าไม่ต้องการ
                    dt.Rows.Add(newRow)
                Next


                For Each item In subGroupedData
                    Dim newRow = dt2.NewRow()
                    newRow("cbo_Group") = item.Group
                    newRow("KPIFormDtl_ID") = item.KPIFormDtlID
                    newRow("cbo_subText") = item.subText
                    newRow("Count") = item.Count ' ไม่จำเป็นถ้าไม่ต้องการ
                    dt2.Rows.Add(newRow)
                Next

                groupCbo = dt

                subGroupCbo = dt2
            End If
        End If
    End Sub

    Private Sub flexRadioDefault1_CheckedChanged(sender As Object, e As EventArgs) Handles flexRadioDefault1.CheckedChanged
        If flexRadioDefault1.Checked Then
            assessor = "owner"
            flexRadioDefault2.Checked = False

            SetBtn(assessor)
        End If
    End Sub

    Private Sub flexRadioDefault2_CheckedChanged(sender As Object, e As EventArgs) Handles flexRadioDefault2.CheckedChanged
        If flexRadioDefault2.Checked Then
            assessor = "approver"
            flexRadioDefault1.Checked = False

            SetBtn(assessor)
        End If
    End Sub

    Private Sub save(jsonArray As JArray)

        Dim objKpi As New Kpi
        Dim res_id As Integer

        If assessor = "owner" Then
            res_id = objKpi.Kpi_FormsRes_Save(ownerid, Request.QueryString("f"), Session("userid"))
        End If
        If assessor = "approver" Then
            res_id = ansRes_idTable.Rows(0).Item("res_id").ToString
        End If
        ' วนลูปผ่านแต่ละออบเจกต์ใน JSON Array
        For Each item As JObject In jsonArray

            ' ตรวจสอบว่า cardid, dtl_id หรือ type เป็นค่าว่างหรือเป็น Nothing หรือไม่
            If (String.IsNullOrEmpty(item("cardid")?.ToString()) OrElse
                String.IsNullOrEmpty(item("dtl_id")?.ToString()) OrElse
                String.IsNullOrEmpty(item("type")?.ToString())) Then
                ' ข้ามไปยังรอบถัดไปถ้า cardid, dtl_id หรือ type ไม่มีค่า
                Continue For
            End If

            ' ดึงข้อมูล cardid, dtl_id และ type ที่มีในทุกออบเจกต์
            Dim res_cardid As Integer? = If(item("cardid") IsNot Nothing, item("cardid").ToObject(Of Integer)(), CType(Nothing, Integer?))
            Dim res_dtl_id As Integer? = If(item("dtl_id") IsNot Nothing, item("dtl_id").ToObject(Of Integer)(), CType(Nothing, Integer?))
            Dim res_type As String = If(Not String.IsNullOrEmpty(item("type")?.ToString()), item("type").ToString(), Nothing)

            ' ประกาศตัวแปรเพิ่มเติมที่อาจใช้ในแต่ละประเภทไว้ข้างนอก Select Case
            Dim res_txtAssessor As String = Nothing
            Dim res_txtKpicode As String = Nothing
            Dim res_value As String = Nothing
            Dim res_txtReason As String = Nothing
            Dim res_txtTopic As String = Nothing
            Dim res_txtObjective As String = Nothing
            Dim res_txtMethod As String = Nothing
            Dim res_txtResult As String = Nothing
            Dim res_txtStakeholders As String = Nothing
            Dim res_project_ans_id As Integer? = CType(Nothing, Integer?)
            Dim res_careerid As Integer? = CType(Nothing, Integer?)
            Dim res_cboid As Integer? = CType(Nothing, Integer?)

            ' เช็ค type และกำหนดค่าตัวแปรเพิ่มเติมตามประเภทที่พบ
            Select Case res_type
                Case "kpi_holding", "kpi_competency", "kpi_holding_reason", "kpi_competency_reason"
                    res_txtAssessor = If(Not String.IsNullOrEmpty(item("assessor")?.ToString()), item("assessor").ToString().ToLower(), Nothing)
                    res_txtKpicode = If(Not String.IsNullOrEmpty(item("kpicode")?.ToString()), item("kpicode").ToString(), Nothing)
                    res_value = If(Not String.IsNullOrEmpty(item("value")?.ToString()), item("value").ToString(), Nothing)
                    res_txtReason = If(Not String.IsNullOrEmpty(item("reason")?.ToString()), item("reason").ToString(), Nothing)

                Case "project"

                    res_txtAssessor = If(Not String.IsNullOrEmpty(item("assessor")?.ToString()), item("assessor").ToString().ToLower(), Nothing)
                    res_txtTopic = If(Not String.IsNullOrEmpty(item("topic")?.ToString()), item("topic").ToString(), Nothing)
                    res_txtObjective = If(Not String.IsNullOrEmpty(item("objective")?.ToString()), item("objective").ToString(), Nothing)
                    res_txtMethod = If(Not String.IsNullOrEmpty(item("method")?.ToString()), item("method").ToString(), Nothing)
                    res_txtResult = If(Not String.IsNullOrEmpty(item("result")?.ToString()), item("result").ToString(), Nothing)
                    res_txtStakeholders = If(Not String.IsNullOrEmpty(item("stakeholders")?.ToString()), item("stakeholders").ToString(), Nothing)
                    res_project_ans_id = If(item("project_ans_id") IsNot Nothing, item("project_ans_id").ToObject(Of Integer)(), CType(Nothing, Integer?))
                    res_value = If(Not String.IsNullOrEmpty(item("value")?.ToString()), item("value").ToString(), Nothing)

                Case "textarea"
                    res_txtAssessor = If(Not String.IsNullOrEmpty(item("assessor")?.ToString()), item("assessor").ToString().ToLower(), Nothing)
                    res_value = If(Not String.IsNullOrEmpty(item("value")?.ToString()), item("value").ToString(), Nothing)

                Case "career"
                    res_txtAssessor = If(Not String.IsNullOrEmpty(item("assessor")?.ToString()), item("assessor").ToString().ToLower(), Nothing)
                    res_careerid = If(item("careerid") IsNot Nothing, item("careerid").ToObject(Of Integer)(), CType(Nothing, Integer?))
                    res_value = If(Not String.IsNullOrEmpty(item("value")?.ToString()), item("value").ToString(), Nothing)

                Case "cbo", "cboGroup"
                    res_cboid = If(item("cboid") IsNot Nothing, item("cboid").ToObject(Of Integer)(), CType(Nothing, Integer?))
                    res_txtAssessor = If(Not String.IsNullOrEmpty(item("assessor")?.ToString()), item("assessor").ToString().ToLower(), Nothing)

            End Select


            If res_id > 0 Then
                Dim ans_id = objKpi.Kpi_FormsAns_Save(res_id, res_dtl_id, assessor, Session("userid"))
                If ans_id > 0 Then
                    Dim res As Integer
                    Select Case res_type
                        Case "kpi_holding", "kpi_competency", "kpi_holding_reason", "kpi_competency_reason"
                            If res_txtAssessor = assessor Then
                                res = objKpi.Kpi_FormsAns_kpis_Save(ans_id, res_txtKpicode, res_value, res_txtReason, Session("userid"))
                            End If

                        Case "project"
                            If res_txtAssessor = "owner" And res_txtAssessor = assessor Then    'aseessor = owner
                                If res_txtTopic IsNot Nothing Or
                                    res_txtObjective IsNot Nothing Or
                                    res_txtMethod IsNot Nothing Or
                                    res_txtResult IsNot Nothing Or
                                    res_txtStakeholders IsNot Nothing Then
                                    res = objKpi.Kpi_FormsAns_projects_Save(ans_id, res_txtTopic, res_txtObjective, res_txtMethod, res_txtResult, res_txtStakeholders, Session("userid"))
                                End If
                            ElseIf res_txtAssessor = "approver" And res_txtAssessor = assessor And res_project_ans_id > 0 Then 'aseessor = approver
                                'If res_value = "accept" Then
                                res = objKpi.Kpi_FormsAns_projects_Accept(res_project_ans_id, Session("userid"), res_value, Session("userid"))
                                'ElseIf res_value = "reject" Then
                                'res = objKpi.Kpi_FormsAns_projects_Reject(res_project_ans_id, Session("userid"), res_value, Session("userid"))
                                'End If

                            End If


                        Case "textarea"
                            If res_txtAssessor = assessor Then
                                If res_value IsNot Nothing Then
                                    res = objKpi.Kpi_FormsAns_txt_Save(ans_id, res_value, Session("userid"))
                                End If
                            End If



                        Case "career"
                            If res_txtAssessor = assessor Then
                                res = objKpi.Kpi_FormsAns_career_Save(ans_id, res_careerid, res_value, Session("userid"))
                            End If

                        Case "cbo", "cboGroup"
                            If res_txtAssessor = assessor Then
                                If res_cboid > 0 Then
                                    res = objKpi.Kpi_FormsAns_cbo_Save(ans_id, res_cboid, Session("userid"))
                                End If
                            End If

                    End Select


                End If
            End If

        Next

        If (res_id > 0) Then
            FindAnsDtl(Request.QueryString("f"), ownerid)
        End If
    End Sub

End Class