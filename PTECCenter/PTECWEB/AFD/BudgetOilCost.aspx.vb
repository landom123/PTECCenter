Public Class BudgetOilCost
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public username As String
    Public usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objjob As New jobs
        Dim objsupplier As New Supplier

        'txtbegindate.Attributes.Add("readonly", "readonly")
        txtenddate.Attributes.Add("readonly", "readonly")
        username = Session("username")
        usercode = Session("usercode")
        setCboYear()

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

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

    End Sub
End Class