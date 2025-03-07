﻿Imports System.Drawing
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq


Public Class KPIsEdit
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
    Public now_date As String
    Public OPT As String = ""

    Dim md_code As String
    Dim fm_code As String
    Dim dm_code As String
    Dim sm_code As String
    Dim am_code As String


    Public managername As String


    Public operator_code As String = ""
    Public operatordt As DataTable

    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objbranch As New Branch
        Dim objUser As New Users
        Dim objKpi As New Kpi

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

        lbchkKpiComplete.Text = "กรุณาใส่เครื่องหมาย ✓ กรณีที่ KPIs ของท่านดำเนินการเสร็จเรียบร้อยแล้ว" + "<br />" + "เพื่อปิดการแจ้งเตือน KPIs ในเดือนถัดไป"

        Try
            managername = Session("managername")
        Catch ex As Exception
            managername = Nothing
        End Try

        If Not IsPostBack() Then
            objUser.SetcboUserName(cboUserName)
            objbranch.SetComboBranch(cboBranch, "")
            objKpi.SetCboRatioType(cboRatio)
            SetCboUsers(cboOwnerKPI, "", "ALL")
            If Not Request.QueryString("Kpi_Code") Is Nothing Then
                findKpi()
            End If

            ViewState("AllKpi") = AllKpi
        Else

            AllKpi = ViewState("AllKpi")

        End If

        SetBtn(AllKpi)
    End Sub
    Private Sub SetBtn(ds As DataSet)
        If AllKpi IsNot Nothing Then
            If AllKpi.Tables(1).Rows.Count > 0 Then
                chkuser(AllKpi.Tables(1).Rows(0).Item("actionownerid"))
                now_action = AllKpi.Tables(1).Rows(0).Item("actionempuppercode").ToString

            End If

            If AllKpi.Tables(0).Rows.Count > 0 Then
                now_date = AllKpi.Tables(0).Rows(0).Item("datenow").ToString

                If AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then
                    btnUpdate.Visible = False
                    btnUpdateOP.Visible = True

                    OPT = "(Admin)"

                ElseIf AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "OP" Then
                    OPT = "(Operator)"
                Else
                    Dim username = Session("username")
                    Dim usercode = Session("usercode")
                    Dim ownername = AllKpi.Tables(1).Rows(1).Item("nameowner").ToString
                    Dim managernameBranch As String = "-"
                    If managername IsNot Nothing Then
                        managernameBranch = managername
                    End If
                    If (ownername.IndexOf(managernameBranch) > -1 Or ownername.IndexOf(username) > -1 Or ownername.IndexOf(usercode) > -1 Or now_action.IndexOf(usercode) > -1) Then
                        If (String.IsNullOrEmpty(AllKpi.Tables(0).Rows(0).Item("operatortype").ToString)) Then
                            btnUpdate.Visible = True
                            btnUpdateOP.Visible = False
                            chkKpiComplete.Visible = True
                            lbchkKpiComplete.Visible = True
                            If (now_action.IndexOf(usercode) > -1) Then
                                chkKpiComplete.Disabled = True
                            End If
                        End If
                    Else
                        btnUpdate.Visible = False
                        btnUpdateOP.Visible = False
                        chkKpiComplete.Visible = False
                        lbchkKpiComplete.Visible = False
                    End If
                End If
            End If
        Else
            btnUpdate.Visible = False
            btnUpdateOP.Visible = False
        End If

    End Sub
    Private Sub findKpi()
        Dim objkpi As New Kpi
        Try


            AllKpi = objkpi.Kpi_Find_by_code(Request.QueryString("Kpi_Code").ToString, Session("usercode"))

            cboRatio.SelectedIndex = If(AllKpi.Tables(0).Rows(0).Item("category_id"), AllKpi.Tables(0).Rows(0).Item("category_id"), -1)
            cboOwnerKPI.SelectedIndex = cboOwnerKPI.Items.IndexOf(cboOwnerKPI.Items.FindByValue(AllKpi.Tables(0).Rows(0).Item("ownerid").ToString))
            txtKpititle.InnerText = AllKpi.Tables(0).Rows(0).Item("Title").ToString
            txtWeight.Text = AllKpi.Tables(0).Rows(0).Item("Weight").ToString
            txtUnit.Text = AllKpi.Tables(0).Rows(0).Item("Unit").ToString
            txtlv5.Text = AllKpi.Tables(0).Rows(0).Item("Lv5").ToString
            txtlv4.Text = AllKpi.Tables(0).Rows(0).Item("Lv4").ToString
            txtlv3.Text = AllKpi.Tables(0).Rows(0).Item("Lv3").ToString
            txtlv2.Text = AllKpi.Tables(0).Rows(0).Item("Lv2").ToString
            txtlv1.Text = AllKpi.Tables(0).Rows(0).Item("Lv1").ToString
            chkKpiComplete.Checked = AllKpi.Tables(0).Rows(0).Item("complete")
            '-- table 0 = Head
            '-- table 1 = main
            '-- table 2 = detail
            '-- table 3 = attach
            '-- table 4 = comment
            ViewState("AllKpi") = AllKpi
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub update()
        Dim objkpi As New Kpi
        Dim res As String = Request.Form("confirm_value")
        res = res.Replace("[", "")
        res = res.Replace("]", "")
        res = res.Replace("},", "}|")
        Dim strarr() As String
        strarr = res.Split("|")
        Try
            For index = 1 To strarr.Length

                res = strarr(index - 1)

                res = res.Replace(",{", "{")

                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)


                Dim actionid As String = json("actionid").Trim
                Dim actionmonthlyid As String = json("actionmonthlyid").Trim
                Dim actionmonthly As String = json("actionmonthly").Trim
                Dim actiontitleresultid As String = json("actiontitleresultid").Trim
                Dim actiontitleresult As String = json("actiontitleresult").Trim
                Dim actionrateownerid As String = json("actionrateownerid").Trim
                Dim actionrateowner As String = json("actionrateowner").Trim
                Dim actionrateheadid As String = json("actionrateheadid").Trim
                Dim actionratehead As String = json("actionratehead").Trim
                Dim actionfeedbackid As String = json("actionfeedbackid").Trim
                Dim actionfeedback As String = json("actionfeedback").Trim

                Dim id As String
                If (String.IsNullOrEmpty(AllKpi.Tables(0).Rows(0).Item("operatortype").ToString)) Then
                    If (AllKpi.Tables(1).Rows(1).Item("actionempuppercode").ToString.IndexOf(Session("usercode")) > -1) Then
                        id = objkpi.SaverateHeadToActionplan(actionid, actionratehead, actionfeedback, Session("usercode"))
                    Else
                        id = objkpi.SaveDetailToActionplan(actionid, actionmonthly, actiontitleresult, actionrateowner, Session("usercode"))


                    End If
                ElseIf AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then

                    Dim actiontitle As String = json("actiontitle").Trim
                    id = objkpi.SaveDetailToActionplanOP(actionid, actiontitle, actionmonthly, actiontitleresult, actionrateowner, actionratehead, actionfeedback, Session("usercode"))

                End If
            Next
            If chkKpiComplete.Checked <> AllKpi.Tables(0).Rows(0).Item("complete") Then
                objkpi.Kpi_Complete_Save(Request.QueryString("Kpi_Code").ToString, chkKpiComplete.Checked, Session("usercode"))
            End If

            findKpi()


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('adddetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.Click
        update()
    End Sub

    Private Sub btnUpdateOP_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateOP.Click
        update()
    End Sub

    Private Sub chkuser(userid As Integer)
        Dim objuser As New Users
        Dim NonPOPermissionTable As New DataTable
        Try
            NonPOPermissionTable = objuser.NonPOPermissionRead(userid)
            md_code = NonPOPermissionTable.Rows(0).Item("md_code")
            fm_code = NonPOPermissionTable.Rows(0).Item("fm_code")
            dm_code = NonPOPermissionTable.Rows(0).Item("dm_code")
            sm_code = NonPOPermissionTable.Rows(0).Item("sm_code")
            am_code = NonPOPermissionTable.Rows(0).Item("am_code")
            allOwner = "[ md : " + md_code + "],[ fm : " + fm_code + "],[ dm : " + dm_code + "],[ sm : " + sm_code + "],[ am : " + am_code + "]"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub


    Private Sub btnUpdateKPI_Click(sender As Object, e As EventArgs) Handles btnUpdateKPI.Click
        Dim objKpi As New Kpi

        Try
            objKpi.Kpi_HeadOP_Save(Request.QueryString("Kpi_Code"), cboOwnerKPI.SelectedItem.Value, cboRatio.SelectedItem.Value, txtKpititle.InnerText, txtWeight.Text, txtUnit.Text, txtlv5.Text, txtlv4.Text, txtlv3.Text, txtlv2.Text, txtlv1.Text, Session("usercode"))
            'findKpi()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsEdit.aspx?Kpi_Code=" & Request.QueryString("Kpi_Code"))
endprocess:
    End Sub

    Private Sub btnUpdateOwnerAP_Click(sender As Object, e As EventArgs) Handles btnUpdateOwnerAP.Click
        Dim objKpi As New Kpi

        Try
            objKpi.UpdateOwnerActionPlan(hiddenAdvancedetailid.Value.ToString, cboUserName.SelectedItem.Value, Session("usercode"))
            hiddenAdvancedetailid.Value = 0
            'findKpi()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsEdit.aspx?Kpi_Code=" & Request.QueryString("Kpi_Code"))
endprocess:
    End Sub
    Private Sub btnUpdateTitle_Click(sender As Object, e As EventArgs) Handles btnUpdateTitle.Click
        Dim objKpi As New Kpi

        Try
            objKpi.Kpi_ActionPlanTitle_Save(hiddenActionplanid.Value.ToString, txtPlan.InnerText.ToString, Session("usercode"))
            hiddenActionplanid.Value = 0
            'findKpi()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsEdit.aspx?Kpi_Code=" & Request.QueryString("Kpi_Code"))
endprocess:
    End Sub

    Private Sub btnUpdateOwnerBranch_Click(sender As Object, e As EventArgs) Handles btnUpdateOwnerBranch.Click
        Dim objKpi As New Kpi

        Try
            objKpi.UpdateOwnerBranch(hiddenbranchid.Value.ToString, cboBranch.SelectedItem.Value, Session("usercode"))
            hiddenbranchid.Value = 0
            'findKpi()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('UpdateOwnerBranch fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsEdit.aspx?Kpi_Code=" & Request.QueryString("Kpi_Code"))
endprocess:
    End Sub
End Class