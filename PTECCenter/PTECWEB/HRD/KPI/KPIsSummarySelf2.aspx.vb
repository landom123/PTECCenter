Imports System.Web.DynamicData
Imports System.Web.Script.Serialization

Public Class KPIsSummarySelf2
    Inherits System.Web.UI.Page

    Public menutable As DataTable


    Public groupFormsDtl As DataTable '= createmaintable()
    Public groupCbo As DataTable '= createmaintable()
    Public subGroupCbo As DataTable '= createmaintable()
    Public formsTable As DataTable '= createmaintable()
    Public formsDtlTable As DataTable '= createmaintable()
    Public formsDtlCboTable As DataTable '= createmaintable()
    Public formsDtlCareerTable As DataTable '= createmaintable()
    Public kpiTable As DataTable '= createmaintable()
    Public weightKPITable As DataTable '= createmaintable()
    Public weightCareerTable As DataTable '= createmaintable()
    Dim ownerid As Integer

    Public ansRes_idTable As DataTable '= createmaintable()
    Public ansCareersTable As DataTable '= createmaintable()
    Public ansCboTable As DataTable '= createmaintable()
    Public ansKpisTable As DataTable '= createmaintable()
    Public ansTxtareaTable As DataTable '= createmaintable()
    Public ansProjectTable As DataTable '= createmaintable()

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



        If Not IsPostBack() Then

            If Request.QueryString("ow") Is Nothing Or Request.QueryString("f") Is Nothing Then
                Response.Redirect("~/403.aspx")
            End If


            If Request.QueryString("f") IsNot Nothing Then
                If Request.QueryString("ow") IsNot Nothing Then
                    ownerid = Request.QueryString("ow")

                    FindFormsDtl(Request.QueryString("f"), ownerid)

                    FindAnsDtl(Request.QueryString("f"), ownerid)

                    If ansRes_idTable IsNot Nothing Then
                        If ansRes_idTable.Rows.Count > 0 Then
                            ' ข้อมูลทั่วไป 
                            setInformation(ansRes_idTable)

                            ' คำนวน table kpi
                            If kpiTable IsNot Nothing And ansKpisTable IsNot Nothing Then
                                setDetail_KPIs(kpiTable, ansKpisTable)
                            End If

                            ' คำนวน txtProject
                            If ansProjectTable IsNot Nothing Then
                                setDetail_Project(ansProjectTable)
                            End If

                            ' คำนวน Training
                            setDetail_Training()

                            ' คำนวน table kpi
                            'getCareerWeight(ansRes_idTable.Rows(0).Item("res_code"))
                            If ansCareersTable IsNot Nothing Then
                                setDetail_Career(ansCareersTable, ansTxtareaTable)
                            End If

                            ' คำนวน Talent
                            'setDetail_Talent()

                            ' คำนวน Feedback
                            setDetail_Feedback("owner")
                            'setDetail_Feedback("approver")
                        End If
                    End If

                End If
                'Else
                '    'New
                '    txtOwnername.Text = Session("username")
                '    txtPosition.Text = Session("positionname")
                '    txtDep.Text = Session("depname")
                '    txtSec.Text = Session("secname")


                '    txtFormTitle.Text = "-"

            End If
        Else


        End If
    End Sub

    Private Sub getKPIWeight(res_code As String)
        Dim objKpi As New Kpi
        Try
            weightKPITable = objKpi.Kpi_GetRatio_kpiscore_by_res_code(res_code)

            ViewState("weightKPITable") = weightKPITable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('getKPIWeight fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:

    End Sub

    Private Sub getCareerWeight(res_code As String)
        Dim objKpi As New Kpi
        Try
            weightCareerTable = objKpi.Kpi_GetRatio_careerscore_by_res_code(res_code)

            ViewState("weightCareerTable") = weightCareerTable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('getCareerWeight fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:

    End Sub

    Private Sub setInformation(dt As DataTable)
        With dt

            txtFormTitle.Text = .Rows(0).Item("form_title").ToString

            ' set Information
            txtOwnername.Text = .Rows(0).Item("owner_name").ToString
            txtPosition.Text = .Rows(0).Item("positionname").ToString
            txtDep.Text = .Rows(0).Item("depname").ToString
            txtSec.Text = .Rows(0).Item("secname").ToString

        End With
    End Sub
    Private Sub setDetail_Project(dt As DataTable)
        ' กำหนด CSS Class ให้กับ Table
        tbProject.CssClass = "table table-bordered shadow-sm bg-light"

        ' สร้างหัวตาราง
        Dim headerRow As New TableRow()
        headerRow.Cells.Add(New TableCell() With {.Text = "หัวข้อ/ชื่อโครงการ", .CssClass = "text-center align-middle font-weight-bold"})
        headerRow.Cells.Add(New TableCell() With {.Text = "วัตถุประสงค์", .CssClass = "text-center align-middle font-weight-bold"})
        headerRow.Cells.Add(New TableCell() With {.Text = "วิธีการ", .CssClass = "text-center align-middle font-weight-bold"})
        headerRow.Cells.Add(New TableCell() With {.Text = "ผลลัพธ์", .CssClass = "text-center align-middle font-weight-bold"})
        headerRow.Cells.Add(New TableCell() With {.Text = "ผู้ที่เกี่ยวข้อง", .CssClass = "text-center align-middle font-weight-bold"})
        tbProject.Rows.Add(headerRow)

        ' อ่านข้อมูลจาก DataTable
        For Each row As DataRow In dt.Rows
            If row("object_type").ToString().ToLower() = "project" And row("UserType").ToString().ToLower() = "owner" Then
                Dim tableRow As New TableRow()

                ' หัวข้อ/ชื่อโครงการ
                Dim topicCell As New TableCell()
                Dim topicSpan As New LiteralControl($"<span class='mx-sm-3 text-primary'>{HttpUtility.HtmlEncode(row("txtTopic").ToString()).Replace(vbCrLf, "<br>").Replace("\n", "<br>")}</span>")
                topicCell.Controls.Add(topicSpan)
                tableRow.Cells.Add(topicCell)

                ' วัตถุประสงค์
                Dim objectiveCell As New TableCell()
                Dim objectiveSpan As New LiteralControl($"<span class='mx-sm-3'>{HttpUtility.HtmlEncode(row("txtObjective").ToString()).Replace(vbCrLf, "<br>").Replace("\n", "<br>")}</span>")
                objectiveCell.Controls.Add(objectiveSpan)
                tableRow.Cells.Add(objectiveCell)

                ' วิธีการ
                Dim methodCell As New TableCell()
                Dim methodSpan As New LiteralControl($"<span class='mx-sm-3'>{HttpUtility.HtmlEncode(row("txtMethod").ToString()).Replace(vbCrLf, "<br>").Replace("\n", "<br>")}</span>")
                methodCell.Controls.Add(methodSpan)
                tableRow.Cells.Add(methodCell)

                ' ผลลัพธ์
                Dim resultCell As New TableCell()
                Dim resultSpan As New LiteralControl($"<span class='mx-sm-3'>{HttpUtility.HtmlEncode(row("txtResult").ToString()).Replace(vbCrLf, "<br>").Replace("\n", "<br>")}</span>")
                resultCell.Controls.Add(resultSpan)
                tableRow.Cells.Add(resultCell)

                ' ผู้ที่เกี่ยวข้อง
                Dim stakeholdersCell As New TableCell()
                Dim stakeholdersSpan As New LiteralControl($"<span class='mx-sm-3'>{HttpUtility.HtmlEncode(row("txtStakeholders").ToString()).Replace(vbCrLf, "<br>").Replace("\n", "<br>")}</span>")
                stakeholdersCell.Controls.Add(stakeholdersSpan)
                tableRow.Cells.Add(stakeholdersCell)

                ' เพิ่มแถวในตาราง
                tbProject.Rows.Add(tableRow)
            End If
        Next

        ' ถ้าไม่มีข้อมูล แสดงข้อความ "-"
        If tbProject.Rows.Count = 1 Then ' ถ้ามีแค่ Header แถวเดียว
            Dim noDataRow As New TableRow()
            Dim noDataCell As New TableCell() With {
            .Text = "-",
            .CssClass = "text-center align-middle",
            .ColumnSpan = 5
        }
            noDataRow.Cells.Add(noDataCell)
            tbProject.Rows.Add(noDataRow)
        End If
    End Sub

    Private Sub setDetail_Training()
        Dim hasTraining As Boolean = False
        Dim htmlContent As New StringBuilder()

        htmlContent.AppendLine("<ol>")

        ' ansCboTable
        For Each row As DataRow In ansCboTable.Rows
            If row("object_type").ToString().ToLower = "training" And row("UserType").ToString().ToLower = "owner" Then
                hasTraining = True
                ' แปลง \n เป็น <br> และเข้ารหัส HTML
                Dim detailText As String = HttpUtility.HtmlEncode(row("cbo_text").ToString()).Replace("\n", "<br>")
                htmlContent.AppendLine($"<li>{detailText}</li>")
            End If
        Next
        ' ansTxtareaTable (other owner)
        For Each row As DataRow In ansTxtareaTable.Rows
            If row("object_type").ToString().ToLower = "training" And row("UserType").ToString().ToLower = "owner" Then
                hasTraining = True
                ' แปลง \n เป็น <br> และเข้ารหัส HTML
                Dim detailText As String = HttpUtility.HtmlEncode(row("txt_detail").ToString()).Replace("\n", "<br>")
                htmlContent.AppendLine($"<li>{detailText & "(อื่นๆ)"}</li>")
            End If
        Next

        htmlContent.AppendLine("</ol>")


        txtTraining.Text = If(hasTraining, htmlContent.ToString(), "-")
    End Sub

    Private Sub setDetail_Career(dt_career As DataTable, dt_txtarea As DataTable)
        ' class="table table-bordered shadow-sm bg-light"
        tbCareer.CssClass = "table table-bordered shadow-sm bg-light"

        If dt_career.Rows.Count > 0 Then
            ' สร้างหัวตาราง
            Dim headerRow As New TableRow()
            headerRow.Cells.Add(New TableCell() With {.Text = "-", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-50"})
            headerRow.Cells.Add(New TableCell() With {.Text = "Score (1-5)", .CssClass = "text-center align-middle font-weight-bold mx-sm-3"})
            headerRow.Cells.Add(New TableCell() With {.Text = "Reason", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-25"})

            tbCareer.Rows.Add(headerRow)

            Dim totalScore As Integer = 0

            ' อ่านข้อมูลจาก DataTable
            For Each row As DataRow In dt_career.Rows

                If row("object_type").ToString = "career" And row("UserType").ToString().ToLower = "owner" Then
                    Dim tableRow As New TableRow()

                    ' Company Ratio
                    Dim companyCell As New TableCell()
                    Dim companySpan As New LiteralControl($"<span class='mx-sm-3 text-primary'>{row("label").ToString()}</span>")
                    companyCell.Controls.Add(companySpan)
                    tableRow.Cells.Add(companyCell)


                    ' Score
                    Dim scoreCell As New TableCell()
                    scoreCell.CssClass = "text-center align-middle"
                    Dim scoreSpan As New LiteralControl($"<span class='mx-sm-3 text-success'>{If(row("txtRate").ToString = "-", "-", Convert.ToInt64(row("txtRate")))}</span>")
                    scoreCell.Controls.Add(scoreSpan)
                    tableRow.Cells.Add(scoreCell)


                    ' ค้นหาข้อมูลใน dt_txtarea
                    Dim matchingRows = dt_txtarea.Select($"parent_id = '{row("KPIformDtl_ID").ToString()}' AND usertype = 'owner'")
                    Dim reasonValue As String = If(matchingRows.Length > 0 AndAlso Not IsDBNull(matchingRows(0)("txt_detail")), matchingRows(0)("txt_detail").ToString.Replace(vbCrLf, "<br>").Replace(vbCr, "<br>").Replace(vbLf, "<br>").Replace("\n", "<br>"), "")

                    ' Reason
                    Dim reasonCell As New TableCell()
                    reasonCell.CssClass = "text-left align-middle"
                    Dim reasonSpan As New LiteralControl($"<span class='mx-sm-3'>{reasonValue}</span>")
                    reasonCell.Controls.Add(reasonSpan)
                    tableRow.Cells.Add(reasonCell)

                    ' เพิ่มแถวในตาราง
                    tbCareer.Rows.Add(tableRow)

                    ' สะสมค่ารวม
                    'totalPercent += Convert.ToInt32(row("Weight"))
                    totalScore += If(row("txtRate").ToString = "-", 0.00, Convert.ToInt64(row("txtRate")))
                End If
            Next

            ' เพิ่มแถว Total
            Dim totalRow As New TableRow()

            ' Total Label
            Dim totalLabelCell As New TableCell()
            totalLabelCell.CssClass = "font-weight-bold"
            Dim totalLabelSpan As New LiteralControl("<span class='mx-sm-3'>Total</span>")
            totalLabelCell.Controls.Add(totalLabelSpan)
            totalRow.Cells.Add(totalLabelCell)


            ' Total Score
            Dim totalScoreCell As New TableCell()
            totalScoreCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalScoreSpan As New LiteralControl($"<span class='mx-sm-3'></span>")
            totalScoreCell.Controls.Add(totalScoreSpan)
            totalRow.Cells.Add(totalScoreCell)

            ' Reason Score
            Dim totalReasonCell As New TableCell()
            totalReasonCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalReasonSpan As New LiteralControl($"<span class='mx-sm-3'></span>")
            totalReasonCell.Controls.Add(totalReasonSpan)
            totalRow.Cells.Add(totalReasonCell)
        Else
            ' แสดงข้อความว่าไม่มีข้อมูล
            Dim noDataRow As New TableRow()
            Dim noDataCell As New TableCell()
            noDataCell.ColumnSpan = 5
            noDataCell.CssClass = "text-center text-muted font-italic"
            noDataCell.Text = "ไม่มีข้อมูล"
            noDataRow.Cells.Add(noDataCell)
            tbCareer.Rows.Add(noDataRow)
            cardCareer.Visible = False
        End If
        'tbCareer.Rows.Add(totalRow)
    End Sub
    'Private Sub setDetail_Talent()
    '    Dim hasTexttalent As Boolean = False
    '    Dim htmlContent As New StringBuilder()

    '    ' ansCboTable
    '    For Each row As DataRow In ansCboTable.Rows
    '        If row("object_type").ToString().ToLower = "talent" And row("UserType").ToString().ToLower = "approver" Then
    '            hasTexttalent = True
    '            ' แปลง \n เป็น <br> และเข้ารหัส HTML
    '            Dim detailText As String = HttpUtility.HtmlEncode(row("cbo_text").ToString()).Replace("\n", "<br>")
    '            htmlContent.AppendLine($"<span class='form-control-span'>{detailText}</span>")
    '            Exit For
    '        End If
    '    Next

    '    txtTalent.Text = If(hasTexttalent, htmlContent.ToString(), "-")
    'End Sub
    'Private Sub setDetail_Feedback(assessor)
    '    Dim hasFeedback As Boolean = False
    '    Dim htmlContent As New StringBuilder()

    '    ' ansTxtareaTable
    '    For Each row As DataRow In ansTxtareaTable.Rows
    '        If row("object_type").ToString().ToLower = "feedback" And row("UserType").ToString().ToLower = assessor Then
    '            hasFeedback = True
    '            ' แปลง \n เป็น <br> และเข้ารหัส HTML
    '            Dim detailText As String = HttpUtility.HtmlEncode(row("txt_detail").ToString()).Replace("\n", "<br>")
    '            htmlContent.AppendLine($"<span class='form-control-span'>{detailText}</span>")
    '            If assessor = "approver" Then
    '                Exit For
    '            End If
    '        End If
    '    Next

    '    If assessor = "approver" Then
    '        'txtFeedbackApprover.Text = If(hasFeedback, htmlContent.ToString(), "-")
    '    ElseIf assessor = "owner" Then
    '        txtFeedback_experienceOwner.Text = If(hasFeedback, htmlContent.ToString(), "-")
    '    End If
    'End Sub

    Private Sub setDetail_Feedback(assessor As String)
        Dim htmlContent_pride As New StringBuilder()
        Dim htmlContent_approver As New StringBuilder()
        Dim htmlContent_experience As New StringBuilder()
        Dim htmlContent_migrate As New StringBuilder()

        ' ansTxtareaTable
        For Each row As DataRow In ansTxtareaTable.Rows
            Dim objectType As String = row("object_type").ToString().ToLower()
            Dim userType As String = row("UserType").ToString().ToLower()

            If userType = assessor Then
                ' แปลง \n เป็น <br> และเข้ารหัส HTML
                Dim detailText As String = HttpUtility.HtmlEncode(row("txt_detail").ToString()).Replace("\n", "<br>")

                Select Case objectType
                    Case "feedback_pride"
                        htmlContent_pride.AppendLine($"<span class='form-control-span'>{detailText}</span>")
                    Case "feedback_experience"
                        htmlContent_experience.AppendLine($"<span class='form-control-span'>{detailText}</span>")
                    Case "feedback_migrate"
                        htmlContent_migrate.AppendLine($"<span class='form-control-span'>{detailText}</span>")
                        'Case "feedback_approver"
                        '    htmlContent_approver.AppendLine($"<span class='form-control-span'>{detailText}</span>")
                        '    If assessor = "approver" Then Exit For
                End Select
            End If
        Next

        ' ตั้งค่าให้กับ ASP Controls
        If assessor = "approver" Then
            'txtFeedbackApprover.Text = If(htmlContent_approver.Length > 0, htmlContent_approver.ToString(), "-")
        ElseIf assessor = "owner" Then
            txtFeedbackExperience.Text = If(htmlContent_experience.Length > 0, htmlContent_experience.ToString(), "-")
            txtFeedbackPride.Text = If(htmlContent_pride.Length > 0, htmlContent_pride.ToString(), "-")
            txtFeedbackMigrate.Text = If(htmlContent_migrate.Length > 0, htmlContent_migrate.ToString(), "-")
        End If

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
            'formsDtlCboTable = ds.Tables(2)
            kpiTable = ds.Tables(3)
            formsDtlCareerTable = ds.Tables(4)

            'getDistinctCboGroup()
            'getDistinctCard()

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


            'setAssessor()
            'convertDataSetToJSON(ds)


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

    Private Sub setDetail_KPIs(dt_kpi As DataTable, dt_ans_kpi As DataTable)
        ' class="table table-bordered shadow-sm bg-light"
        tbRatio.CssClass = "table table-bordered shadow-sm bg-light"


        If dt_kpi.Rows.Count > 0 Then



            ' สร้างหัวตาราง
            Dim headerRow As New TableRow()
            headerRow.Cells.Add(New TableCell() With {.Text = "Type", .CssClass = "text-center align-middle font-weight-bold mx-sm-3"})
            headerRow.Cells.Add(New TableCell() With {.Text = "KPIs", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-50"})
            headerRow.Cells.Add(New TableCell() With {.Text = "%", .CssClass = "text-center align-middle font-weight-bold mx-sm-3"})
            headerRow.Cells.Add(New TableCell() With {.Text = "Score (1-5)", .CssClass = "text-center align-middle font-weight-bold mx-sm-3"})
            headerRow.Cells.Add(New TableCell() With {.Text = "Actual", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-25"})
            tbRatio.Rows.Add(headerRow)

            Dim totalPercent As Integer = 0
            Dim totalScore As Decimal = 0.00

            ' อ่านข้อมูลจาก DataTable
            For Each row As DataRow In dt_kpi.Rows
                Dim tableRow As New TableRow()

                ' Type
                Dim typeCell As New TableCell()
                Dim typeSpan As New LiteralControl($"<span class='mx-sm-3 text-primary'>{row("CategoryName").ToString()}</span>")
                typeCell.Controls.Add(typeSpan)
                tableRow.Cells.Add(typeCell)


                ' KPIs
                Dim kpisCell As New TableCell()
                Dim kpisSpan As New LiteralControl($"<span class='mx-sm-3'>{row("title").ToString().Replace(vbCrLf, "<br>").Replace(vbCr, "<br>").Replace(vbLf, "<br>")}</span>")
                kpisCell.Controls.Add(kpisSpan)
                tableRow.Cells.Add(kpisCell)

                ' Percent
                Dim percentCell As New TableCell()
                percentCell.CssClass = "text-center align-middle"
                Dim percentSpan As New LiteralControl($"<span class='mx-sm-3 text-info'>{Convert.ToDecimal(row("Weight"))}</span>")
                percentCell.Controls.Add(percentSpan)
                tableRow.Cells.Add(percentCell)

                ' Score (ค้นหาจาก dt_ans_kpi)
                Dim scoreCell As New TableCell()
                scoreCell.CssClass = "text-center align-middle"

                ' ค้นหาข้อมูลใน dt_ans_kpi
                Dim matchingRows = dt_ans_kpi.Select($"kpi_code = '{row("kpi_code").ToString()}' AND usertype = 'owner'")
                Dim scoreValue As Decimal = If(matchingRows.Length > 0 AndAlso Not IsDBNull(matchingRows(0)("txtRate")), Convert.ToDecimal(matchingRows(0)("txtRate")), 0.00)
                Dim actualValue As String = If(matchingRows.Length > 0 AndAlso Not IsDBNull(matchingRows(0)("txtReason")), matchingRows(0)("txtReason").ToString(), "")
                actualValue = HttpUtility.HtmlEncode(actualValue).Replace("\n", "<br>")

                Dim scoreSpan As New LiteralControl($"<span class='mx-sm-3 text-success'>{scoreValue}</span>")
                scoreCell.Controls.Add(scoreSpan)
                tableRow.Cells.Add(scoreCell)

                ' Actual
                Dim actualCell As New TableCell()
                actualCell.CssClass = "text-left align-middle"
                Dim actualSpan As New LiteralControl($"<span class=''>{actualValue}</span>")
                actualCell.Controls.Add(actualSpan)
                tableRow.Cells.Add(actualCell)

                ' เพิ่มแถวในตาราง
                tbRatio.Rows.Add(tableRow)

                ' สะสมค่ารวม
                totalPercent += Convert.ToInt32(row("Weight"))
                totalScore += scoreValue
            Next



            ' เพิ่มแถว Total
            Dim totalRow As New TableRow()

            ' Total Label
            Dim totalLabelCell As New TableCell()
            totalLabelCell.CssClass = "font-weight-bold"
            Dim totalLabelSpan As New LiteralControl("<span class='mx-sm-3'>Total</span>")
            totalLabelCell.Controls.Add(totalLabelSpan)
            totalRow.Cells.Add(totalLabelCell)

            ' Total KPIs
            Dim totalKPIsCell As New TableCell()
            totalKPIsCell.CssClass = "font-weight-bold"
            Dim totalKPIsSpan As New LiteralControl("<span class='mx-sm-3'></span>")
            totalKPIsCell.Controls.Add(totalKPIsSpan)
            totalRow.Cells.Add(totalKPIsCell)

            ' Total Percent
            Dim totalPercentCell As New TableCell()
            totalPercentCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalPercentSpan As New LiteralControl($"<span class='mx-sm-3'></span>")
            totalPercentCell.Controls.Add(totalPercentSpan)
            totalRow.Cells.Add(totalPercentCell)

            ' Total Score
            Dim totalScoreCell As New TableCell()
            totalScoreCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalScoreSpan As New LiteralControl($"<span class='mx-sm-3'></span>")
            totalScoreCell.Controls.Add(totalScoreSpan)
            totalRow.Cells.Add(totalScoreCell)

            ' Total Actual
            Dim totalActualCell As New TableCell()
            totalActualCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalActualSpan As New LiteralControl($"<span class='mx-sm-3'></span>")
            totalActualCell.Controls.Add(totalActualSpan)
            totalRow.Cells.Add(totalActualCell)

            'tbRatio.Rows.Add(totalRow)
        Else
            ' แสดงข้อความว่าไม่มีข้อมูล
            Dim noDataRow As New TableRow()
            Dim noDataCell As New TableCell()
            noDataCell.ColumnSpan = 5
            noDataCell.CssClass = "text-center text-muted font-italic"
            noDataCell.Text = "No data available."
            noDataRow.Cells.Add(noDataCell)
            tbRatio.Rows.Add(noDataRow)
        End If
    End Sub
End Class