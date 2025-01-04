Imports System.Web.DynamicData
Imports System.Web.Script.Serialization

Public Class KPIsSummaryView
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


        alNotHasDataApprover.Visible = False

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
                            getKPIWeight(ansRes_idTable.Rows(0).Item("res_code"))
                            If weightKPITable IsNot Nothing Then
                                setDetail_Ratio(weightKPITable)
                            End If


                            ' คำนวน txtProject
                            setDetail_Project()

                            ' คำนวน Training
                            setDetail_Training()

                            ' คำนวน table kpi
                            getCareerWeight(ansRes_idTable.Rows(0).Item("res_code"))
                            If weightCareerTable IsNot Nothing Then
                                setDetail_Career(weightCareerTable)
                            End If

                            ' คำนวน Talent
                            setDetail_Talent()

                            ' คำนวน Feedback
                            setDetail_Feedback("owner")
                            setDetail_Feedback("approver")
                        End If
                    End If

                End If
            End If
        Else


        End If
    End Sub

    Private Sub getKPIWeight(res_code As string)
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

            ' Check if Approval_date has a value
            If Not IsDBNull(.Rows(0).Item("Approval_date")) AndAlso Not String.IsNullOrEmpty(.Rows(0).Item("Approval_date").ToString()) Then
                alNotHasDataApprover.Visible = False
                txtApprovalBy.Text = "ผลประเมินจาก : (" & .Rows(0).Item("Approval_code").ToString & ")"
            Else
                alNotHasDataApprover.Visible = True
            End If
        End With
    End Sub
    Private Sub setDetail_Ratio(dt As DataTable)
        ' class="table table-sm table-bordered shadow-sm bg-light"
        tbRatio.CssClass = "table table-sm table-bordered shadow-sm bg-light"


        If dt.Rows.Count > 0 Then
            ' สร้างหัวตาราง
            Dim headerRow As New TableRow()
            headerRow.Cells.Add(New TableCell() With {.Text = "Company Ratio", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-50"})
            headerRow.Cells.Add(New TableCell() With {.Text = "%", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-25"})
            headerRow.Cells.Add(New TableCell() With {.Text = "Score", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-25"})
            tbRatio.Rows.Add(headerRow)

            Dim totalPercent As Integer = 0
            Dim totalScore As Decimal = 0.00

            ' อ่านข้อมูลจาก DataTable
            For Each row As DataRow In dt.Rows
                If row("type").ToString = "kpi" Then

                    Dim tableRow As New TableRow()

                    ' Company Ratio
                    Dim companyCell As New TableCell()
                    Dim companySpan As New LiteralControl($"<span class='mx-sm-3 text-primary'>{row("CategoryName").ToString()}</span>")
                    companyCell.Controls.Add(companySpan)
                    tableRow.Cells.Add(companyCell)

                    ' Percent
                    Dim percentCell As New TableCell()
                    percentCell.CssClass = "text-center align-middle"
                    Dim percentSpan As New LiteralControl($"<span class='mx-sm-3 text-info'>{If(row("score").ToString = "-", "-", Convert.ToDecimal(row("Weight")))}</span>")
                    percentCell.Controls.Add(percentSpan)
                    tableRow.Cells.Add(percentCell)

                    ' Score
                    Dim scoreCell As New TableCell()
                    scoreCell.CssClass = "text-center align-middle"
                    Dim scoreSpan As New LiteralControl($"<span class='mx-sm-3 text-success'>{If(row("score").ToString = "-", "-", Convert.ToDecimal(row("score")))}</span>")
                    scoreCell.Controls.Add(scoreSpan)
                    tableRow.Cells.Add(scoreCell)

                    ' เพิ่มแถวในตาราง
                    tbRatio.Rows.Add(tableRow)

                    ' สะสมค่ารวม
                    totalPercent += Convert.ToInt32(row("Weight"))
                    totalScore += If(row("score").ToString = "-", 0.00, Convert.ToDecimal(row("score")))
                End If
            Next

            ' เพิ่มแถว Total
            Dim totalRow As New TableRow()

            ' Total Label
            Dim totalLabelCell As New TableCell()
            totalLabelCell.CssClass = "font-weight-bold"
            Dim totalLabelSpan As New LiteralControl("<span class='mx-sm-3'>Total (คะแนน Company Ratio)</span>")
            totalLabelCell.Controls.Add(totalLabelSpan)
            totalRow.Cells.Add(totalLabelCell)

            ' Total Percent
            Dim totalPercentCell As New TableCell()
            totalPercentCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalPercentSpan As New LiteralControl($"<span class='mx-sm-3'>{totalPercent}</span>")
            totalPercentCell.Controls.Add(totalPercentSpan)
            totalRow.Cells.Add(totalPercentCell)

            ' Total Score
            Dim totalScoreCell As New TableCell()
            totalScoreCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalScoreSpan As New LiteralControl($"<span class='mx-sm-3'>{totalScore}</span>")
            totalScoreCell.Controls.Add(totalScoreSpan)
            totalRow.Cells.Add(totalScoreCell)

            tbRatio.Rows.Add(totalRow)
        Else
            ' แสดงข้อความว่าไม่มีข้อมูล
            Dim noDataRow As New TableRow()
            Dim noDataCell As New TableCell()
            noDataCell.ColumnSpan = 5
            noDataCell.CssClass = "text-center text-muted font-italic"
            noDataCell.Text = "ไม่มีข้อมูล"
            noDataRow.Cells.Add(noDataCell)
            tbRatio.Rows.Add(noDataRow)
        End If

    End Sub

    Private Sub setDetail_Project()
        Dim hasProject As Boolean = False
        Dim htmlContent As New StringBuilder()

        htmlContent.AppendLine("<ol>")

        ' ansProjectTable
        For Each row As DataRow In ansProjectTable.Rows
            If row("object_type").ToString().ToLower = "project" And row("UserType").ToString().ToLower = "owner" And row("status").ToString = "accept" Then
                hasProject = True
                ' แปลง \n เป็น <br> และเข้ารหัส HTML
                Dim detailText As String = HttpUtility.HtmlEncode(row("txtTopic").ToString()).Replace("\n", "<br>") & " --(" & row("approval_rate").ToString() & ")"
                htmlContent.AppendLine($"<li>{detailText}</li>")
            End If
        Next

        htmlContent.AppendLine("</ol>")

        txtProject.Text = If(hasProject, htmlContent.ToString(), "-")
    End Sub
    Private Sub setDetail_Training()
        Dim hasTraining As Boolean = False
        Dim htmlContent As New StringBuilder()

        htmlContent.AppendLine("<ol>")

        ' ansCboTable
        For Each row As DataRow In ansCboTable.Rows
            If row("object_type").ToString().ToLower = "training" And row("UserType").ToString().ToLower = "approver" Then
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

    Private Sub setDetail_Career(dt As DataTable)
        ' class="table table-sm table-bordered shadow-sm bg-light"
        tbCareer.CssClass = "table table-sm table-bordered shadow-sm bg-light"

        Dim totalPercent As Integer = 0
        Dim totalScore As Decimal = 0.00

        If dt.Rows.Count > 0 Then


            ' สร้างหัวตาราง
            Dim headerRow As New TableRow()
            headerRow.Cells.Add(New TableCell() With {.Text = "Title", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-50"})
            headerRow.Cells.Add(New TableCell() With {.Text = "%", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-25"})
            headerRow.Cells.Add(New TableCell() With {.Text = "Score", .CssClass = "text-center align-middle font-weight-bold mx-sm-3 w-25"})
            tbCareer.Rows.Add(headerRow)


            ' อ่านข้อมูลจาก DataTable
            For Each row As DataRow In dt.Rows

                If row("type").ToString = "career" Then
                    Dim tableRow As New TableRow()

                    ' Company Ratio
                    Dim companyCell As New TableCell()
                    Dim companySpan As New LiteralControl($"<span class='mx-sm-3 text-primary'>{row("CategoryName").ToString()}</span>")
                    companyCell.Controls.Add(companySpan)
                    tableRow.Cells.Add(companyCell)

                    ' Percent
                    Dim percentCell As New TableCell()
                    percentCell.CssClass = "text-center align-middle"
                    Dim percentSpan As New LiteralControl($"<span class='mx-sm-3 text-info'>{If(row("score").ToString = "-", "-", Convert.ToDecimal(row("Weight")))}</span>")
                    percentCell.Controls.Add(percentSpan)
                    tableRow.Cells.Add(percentCell)

                    ' Score
                    Dim scoreCell As New TableCell()
                    scoreCell.CssClass = "text-center align-middle"
                    Dim scoreSpan As New LiteralControl($"<span class='mx-sm-3 text-success'>{If(row("score").ToString = "-", "-", Convert.ToDecimal(row("score")))}</span>")
                    scoreCell.Controls.Add(scoreSpan)
                    tableRow.Cells.Add(scoreCell)

                    ' เพิ่มแถวในตาราง
                    tbCareer.Rows.Add(tableRow)

                    ' สะสมค่ารวม
                    totalPercent += Convert.ToInt32(row("Weight"))
                    totalScore += If(row("score").ToString = "-", 0.00, Convert.ToDecimal(row("score")))
                End If
            Next

            ' เพิ่มแถว Total
            Dim totalRow As New TableRow()

            ' Total Label
            Dim totalLabelCell As New TableCell()
            totalLabelCell.CssClass = "font-weight-bold"
            Dim totalLabelSpan As New LiteralControl("<span class='mx-sm-3'>Total (คะแนนประเมินศักยภาพ)</span>")
            totalLabelCell.Controls.Add(totalLabelSpan)
            totalRow.Cells.Add(totalLabelCell)

            ' Total Percent
            Dim totalPercentCell As New TableCell()
            totalPercentCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalPercentSpan As New LiteralControl($"<span class='mx-sm-3'>{totalPercent}</span>")
            totalPercentCell.Controls.Add(totalPercentSpan)
            totalRow.Cells.Add(totalPercentCell)

            ' Total Score
            Dim totalScoreCell As New TableCell()
            totalScoreCell.CssClass = "text-center align-middle font-weight-bold"
            Dim totalScoreSpan As New LiteralControl($"<span class='mx-sm-3'>{totalScore}</span>")
            totalScoreCell.Controls.Add(totalScoreSpan)
            totalRow.Cells.Add(totalScoreCell)

            tbCareer.Rows.Add(totalRow)
        End If
        If dt.Rows.Count = 0 And totalPercent = 0 Then

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
    End Sub
    Private Sub setDetail_Talent()
        Dim hasTexttalent As Boolean = False
        Dim htmlContent As New StringBuilder()

        ' ansCboTable
        For Each row As DataRow In ansCboTable.Rows
            If row("object_type").ToString().ToLower = "talent" And row("UserType").ToString().ToLower = "approver" Then
                hasTexttalent = True
                ' แปลง \n เป็น <br> และเข้ารหัส HTML
                Dim detailText As String = HttpUtility.HtmlEncode(row("cbo_text").ToString()).Replace("\n", "<br>")
                htmlContent.AppendLine($"<span class='form-control-span'>{detailText}</span>")
                Exit For
            End If
        Next

        txtTalent.Text = If(hasTexttalent, htmlContent.ToString(), "-")
    End Sub
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
    '        txtFeedbackApprover.Text = If(hasFeedback, htmlContent.ToString(), "-")
    '    ElseIf assessor = "owner" Then
    '        txtFeedbackOwner.Text = If(hasFeedback, htmlContent.ToString(), "-")
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
                    Case "feedback_approver"
                        htmlContent_approver.AppendLine($"<span class='form-control-span'>{detailText}</span>")
                        If assessor = "approver" Then Exit For
                End Select
            End If
        Next

        ' ตั้งค่าให้กับ ASP Controls
        If assessor = "approver" Then
            txtFeedbackApprover.Text = If(htmlContent_approver.Length > 0, htmlContent_approver.ToString(), "-")
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

End Class