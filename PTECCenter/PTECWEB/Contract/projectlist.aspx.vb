

Imports System.IO

Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.Windows

Public Class projectlist
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public projecttable As DataTable = create()
    Public usercode, username

    Dim dtBranch As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "")
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########

        'Dim objsupplier As New Supplier

        If IsPostBack() Then


            'gsmtable = Session("gsmtable")
            'BindData()
        Else


            If Not (Session("project") Is Nothing) Then
                projecttable = Session("project")
                BindData()


                List()

                Dim objprj As New Project
                    dtBranch = objprj.loadBranch
                    cboBranch.DataSource = dtBranch
                cboBranch.DataValueField = "BrCode"
                cboBranch.DataTextField = "Brname"
                cboBranch.DataBind()


            Else
                List()

                Dim objprj As New Project
                dtBranch = objprj.loadBranch
                cboBranch.DataSource = dtBranch
                cboBranch.DataValueField = "BrCode"
                cboBranch.DataTextField = "Brname"
                cboBranch.DataBind()

            End If
        End If



    End Sub

    Private Sub SetCboContractType(obj As Object)
        Dim ag As New Contract

        obj.DataSource = ag.ContractTypeCbo()
        obj.DataValueField = "agtypeid"
        obj.DataTextField = "agtype"
        obj.DataBind()
    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("projectno", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("remark", GetType(String))
        dt.Columns.Add("agstatus", GetType(Date))
        dt.Columns.Add("approveby", GetType(String))
        dt.Columns.Add("approvedate", GetType(String))
        dt.Columns.Add("link", GetType(String))


        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = projecttable
        gvData.DataBind()
    End Sub

    Protected Sub List()
        'Dim InvoiceDate As String
        Dim objprj As New Project

        'InvoiceDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        projecttable = objprj.List()
        Session("project") = projecttable
        BindData()

    End Sub

    Protected Sub ListFind(Branch As String)
        'Dim InvoiceDate As String
        Dim objprj As New Project

        'InvoiceDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        projecttable = objprj.ListFind(Branch)
        Session("project") = projecttable
        BindData()

    End Sub


    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound


    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("projectinfo.aspx")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'List()

        Try

            If cboBranch.SelectedValue.ToUpper = "ALL" Then
                List()
            Else
                ListFind(cboBranch.SelectedValue)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        Try

            If cboBranch.SelectedValue.ToUpper = "ALL" Then
                List()
            Else
                ListFind(cboBranch.SelectedValue)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Class