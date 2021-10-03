Public Class JobsList
    Inherits System.Web.UI.Page
    Public itemtable As DataTable = createdetailtable()
    Public menutable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objjob As New jobs
        Dim usercode As String
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


        If Not IsPostBack() Then
            itemtable = objjob.JobList(usercode, 0)
            Session("joblist") = itemtable
            BindData()
        Else
            itemtable = Session("joblist")
            BindData()
        End If
    End Sub
    Private Sub BindData()
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("jobs.aspx")
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdate", GetType(Date))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class