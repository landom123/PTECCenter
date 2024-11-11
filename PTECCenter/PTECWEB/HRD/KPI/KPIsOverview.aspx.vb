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

    Public totalRatio As Double = 0

    Public codeowner As String = ""
    Public nameowner As String = ""
    Public account_code As String = ""
    Public operator_code As String = ""
    Public adm_code As String = ""

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


        'Try
        '    Dim usernamebycode As String = ""
        '    Dim usercodebyname As String = ""
        '    Dim objuser As New Users
        '    Dim dt As DataTable

        '    dt = objuser.Find("", "", Request.QueryString("uc"))
        '    usernamebycode = dt.Rows(0).Item("name").ToString

        '    nameowner = If(Session("managername") IsNot Nothing, Session("managername"), usernamebycode)
        'Catch ex As Exception
        '    nameowner = ""
        'End Try


        operator_code = objKpi.KPIPermisstion("KPI", "OP")
        adm_code = objKpi.KPIPermisstion("KPI", "A")
        If Not IsPostBack() Then
            If Not Request.QueryString("p") Is Nothing Then 'preriod
                If Not Request.QueryString("t") Is Nothing Then 'type user
                    If Not Request.QueryString("uc") Is Nothing Then
                        Try
                            Dim usernamebycode As String = ""
                            Dim usercodebyname As String = ""
                            Dim objuser As New Users
                            Dim dt As DataTable

                            If Request.QueryString("t").ToString.ToUpper = "HO" Then

                                dt = objuser.Find("", "", Request.QueryString("uc"))
                                nameowner = dt.Rows(0).Item("name").ToString
                                codeowner = Request.QueryString("uc")
                            ElseIf Request.QueryString("t").ToString.ToUpper = "CO" Then

                                dt = objuser.Find("", Request.QueryString("uc"), "")
                                nameowner = Request.QueryString("uc")
                                codeowner = dt.Rows(0).Item("UserCode").ToString

                            End If
                        Catch ex As Exception
                            nameowner = ""
                            codeowner = ""
                        End Try
                        AllKpi = objKpi.Kpi_Find_Overview(Request.QueryString("p").ToString, nameowner, codeowner)
                        'If Request.QueryString("t").ToString.ToUpper = "HO" Then

                        'ElseIf Request.QueryString("t").ToString.ToUpper = "CO" Then
                        '    'AllKpi = objKpi.Kpi_Find_Overview_For_Branch(Request.QueryString("p").ToString, Request.QueryString("uc").ToString, Session("usercode"))
                        'End If
                    End If
                End If
            End If

            Session("Overview_AllKpi") = AllKpi
        Else

            AllKpi = Session("Overview_AllKpi")
        End If


    End Sub

    Private Sub KPIsOverview_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Try
            Dim fruitGroups = AllKpi.Tables(0).AsEnumerable().GroupBy(Function(row) row.Field(Of String)("CategoryName"))

            Dim tableResult As New DataTable

            tableResult.Columns.Add("key", GetType(String))
            tableResult.Columns.Add("overviewweight", GetType(Double))
            For Each grp In fruitGroups
                tableResult.Rows.Add(grp.Key, grp.First().Field(Of Double)("OverviewWeight"))
            Next

            totalRatio = Convert.ToDouble(tableResult.Compute("SUM(overviewweight)", String.Empty))
        Catch ex As Exception
            totalRatio = 0
        End Try
    End Sub
End Class