Imports System.Drawing
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

    Dim md_code As String
    Dim fm_code As String
    Dim dm_code As String
    Dim sm_code As String
    Dim am_code As String


    Public account_code As String = ""

    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim objKpi As New Kpi

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
            If Not Request.QueryString("Kpi_Code") Is Nothing Then
                findKpi()

                If AllKpi IsNot Nothing Then
                    If AllKpi.Tables(1).Rows.Count > 0 Then
                        chkuser(AllKpi.Tables(1).Rows(0).Item("actionownerid"))
                        now_action = AllKpi.Tables(1).Rows(0).Item("actionempuppercode").ToString
                    End If
                    If AllKpi.Tables(1).Rows.Count > 0 Then
                        now_date = AllKpi.Tables(0).Rows(0).Item("datenow").ToString
                    End If
                End If

            End If

            Session("AllKpi") = AllKpi
        Else

            AllKpi = Session("AllKpi")
        End If

        SetBtn(AllKpi)
    End Sub
    Private Sub SetBtn(ds As DataSet)
        If AllKpi.Tables(1).Rows.Count > 0 Then
            btnUpdate.Visible = True

        Else

            btnUpdate.Visible = False
        End If
    End Sub
    Private Sub findKpi()
        Dim objkpi As New Kpi
        Try


            AllKpi = objKpi.Kpi_Find_by_code(Request.QueryString("Kpi_Code").ToString)
            '-- table 0 = Head
            '-- table 1 = main
            '-- table 2 = detail
            '-- table 3 = attach
            '-- table 4 = comment
            Session("AllKpi") = AllKpi
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

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

                Dim objkpi As New Kpi
                Dim id As String
                If (AllKpi.Tables(1).Rows(1).Item("actionempuppercode").ToString.IndexOf(Session("usercode")) > -1) Then
                    id = objkpi.SaverateHeadToActionplan(actionid, actionratehead, actionfeedback, Session("usercode"))
                Else
                    id = objkpi.SaveDetailToActionplan(actionid, actionmonthly, actiontitleresult, actionrateowner, Session("usercode"))
                End If
                findKpi()
            Next


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('adddetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
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
End Class