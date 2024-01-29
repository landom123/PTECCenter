Imports System.Drawing
Imports System.Web.Script.Serialization

Public Class KPIsRequestList
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

    Public criteria As DataTable '= createCriteria()
    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objKpi As New Kpi
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objNonpo As New NonPO
        Dim objMemo As New Memo
        Dim objsec As New Section
        Dim objcompany As New Company
        'Dim objjob As New jobs
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


        operator_code = objNonpo.NonPOPermisstionOperator("MMR")
        If Not IsPostBack() Then

            objcompany.SetCboCompany(cboCompany, 0)

            objKpi.SetCboPeriod(cboPeriod)

            searchjobslist()

        Else

            criteria = Session("criteria_memo")
            itemtable = Session("memoitem")
        End If

    End Sub
    Private Sub searchjobslist()
        Dim objKpi As New Kpi
        Dim objMemo As New Memo

        Dim detailtable As New DataTable
        Try
            itemtable = objKpi.newkpi_For_Operator()


            Session("memoitem") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindData()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        'setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        gvRemind.Caption = "ทั้งหมด " & itemtable.Rows.Count & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchjobslist()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("memoitem") = itemtable
        Session("criteria_memo") = criteria

        BindData()
    End Sub
    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 6
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("StatusName") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("StatusName") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("StatusName") = "ได้รับการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Green
            ElseIf Data.Item("StatusName") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            End If

        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("KPIsRequest.aspx")
    End Sub
End Class