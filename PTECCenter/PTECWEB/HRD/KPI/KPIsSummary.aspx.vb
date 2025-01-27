Imports System.Web.DynamicData
Imports System.Web.Script.Serialization

Public Class KPIsSummary
    Inherits System.Web.UI.Page

    Public menutable As DataTable
    Public periodTable As DataTable '= createmaintable()
    Public formsTable As DataTable '= createmaintable()

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
        If Not IsPostBack() Then
            objKpi.SetCboPeriod(cboPeriod)
            'periodTable = objKpi.Kpi_Period_List()
            FindForms_by_period(cboPeriod.SelectedValue)


            ViewState("formsTable") = formsTable
        Else
            formsTable = ViewState("formsTable")

            Dim target = Request.Form("__EVENTTARGET")
            If target = "delRes" Then
                Dim argument As Integer = Request("__EVENTARGUMENT")
                delRes(Session("userid"), argument)
                FindForms_by_period(cboPeriod.SelectedValue)
            End If

        End If
    End Sub

    Private Sub delRes(owner_id As Integer, kpiform_id As Integer)
        Dim objKpi As New Kpi
        Try
            objKpi.Kpi_FormsRes_Del(owner_id, kpiform_id, Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('FindForms_by_period fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:
    End Sub
    Private Sub FindForms_by_period(period_id As Integer)
        Dim objKpi As New Kpi
        Try
            formsTable = objKpi.Kpi_Forms_List_by_userid(period_id, Session("userid"))


            ViewState("formsTable") = formsTable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('FindForms_by_period fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
endprocess:
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function addForms(ByVal message As String, ByVal updateby As Integer)
        Dim objKpi As New Kpi

        Dim result As Boolean
        Try
            result = objKpi.Kpi_Forms_Add(message, updateby)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function


    <System.Web.Services.WebMethod>
    Public Shared Function dupForms(ByVal formid As Integer, ByVal message As String, ByVal updateby As Integer)
        Dim objKpi As New Kpi

        Dim result As Boolean
        Try
            'result = objKpi.Kpi_Forms_Dup(formid, message, updateby)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function CancelByCode(ByVal formid As Integer, ByVal message As String, ByVal updateby As Integer)
        Dim objKpi As New Kpi

        Dim result As Boolean
        Try
            result = objKpi.Kpi_Forms_Del(formid, message, updateby)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function
    Private Sub cboPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPeriod.SelectedIndexChanged
        FindForms_by_period(cboPeriod.SelectedValue)
    End Sub
End Class