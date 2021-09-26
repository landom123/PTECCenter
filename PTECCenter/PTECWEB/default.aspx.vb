Public Class index
    Inherits System.Web.UI.Page
    Dim penddingpastmonth, pendding, jobnotconfirm, jobnew As Integer
    Public menutable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("usercode") = "PAB"
        'Session("username") = "Pison Anekboonyapirom"

        Dim usercode, username, dep As String
        usercode = Session("usercode")
        username = Session("username")
        dep = Session("dep")

        If Session("menulist") Is Nothing Then
            If Not String.IsNullOrEmpty(usercode) Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            End If
        Else
                menutable = Session("menulist")
        End If

        If Not IsPostBack() Then
            If Session("usercode") Is Nothing Then
                Response.Redirect("~/login.aspx")
            End If
            'menutable = LoadMenu(usercode)
            'Session("menulist") = LoadMenu(usercode)
            GetDataForDashboard(usercode)

        End If
    End Sub
    Private Sub GetDataForDashboard(usercode As String)
        Dim user As New Users
        Dim mytable As DataTable
        Dim year As String = Now.Year.ToString("0000")
        Try
            mytable = user.dashboard(usercode, year)
            pendding = mytable.Rows(0).Item("pendding")
            penddingpastmonth = mytable.Rows(0).Item("penddingpastmonth")
            jobnotconfirm = mytable.Rows(0).Item("jobnotconfirm")
            jobnew = mytable.Rows(0).Item("jobnew")

            Session("pendding") = pendding
            Session("penddingpastmonth") = penddingpastmonth
            Session("jobnotconfirm") = jobnotconfirm
            Session("jobnew") = jobnew
        Catch ex As Exception

        End Try
    End Sub

End Class