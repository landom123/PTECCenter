Imports System.Drawing

Public Class KPIsOverview
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

        'AllKpi = objKpi.Kpi_Find_Overview(1, Session("usercode"))
        If Not IsPostBack() Then
            If Not Request.QueryString("uc") Is Nothing Then
                AllKpi = objKpi.Kpi_Find_Overview(1, Request.QueryString("uc").ToString)

                'If AllKpi IsNot Nothing Then
                '    If AllKpi.Tables(1).Rows.Count > 0 Then
                '        chkuser(AllKpi.Tables(1).Rows(0).Item("actionownerid"))
                '        now_action = AllKpi.Tables(1).Rows(0).Item("actionempuppercode").ToString
                '    End If
                '    If AllKpi.Tables(1).Rows.Count > 0 Then
                '        now_date = AllKpi.Tables(0).Rows(0).Item("datenow").ToString
                '    End If
                'End If

            End If

            Session("Overview_AllKpi") = AllKpi
        Else

            AllKpi = Session("Overview_AllKpi")
        End If


    End Sub

End Class