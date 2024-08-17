Public Class BudgetControl
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public username As String
    Public usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objjob As New jobs
        Dim objsupplier As New Supplier

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        username = Session("username")
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
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
        txtbegindate.Attributes.Add("readonly", "readonly")
        txtenddate.Attributes.Add("readonly", "readonly")

        setCboYear()
    End Sub
    Private Sub setCboYear()
        cboYear.Items.Clear()
        cboYear.Items.Add(2021)
        cboYear.Items.Add(2022)
        cboYear.Items.Add(2023)
        cboYear.Items.Add(2024)
        cboYear.Items.Add(2025)
        cboYear.Items.Add(2026)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objbudget As New Budgets
        Dim result As Boolean = True
        Dim year As Integer = Integer.Parse(cboYear.SelectedItem.Text)
        Dim begindate, enddate As DateTime
        begindate = DateTime.Parse(txtbegindate.Text)
        enddate = DateTime.Parse(txtenddate.Text)
        Try
            result = objbudget.save(usercode, year, begindate, enddate)
        Catch ex As Exception

        End Try
    End Sub
End Class