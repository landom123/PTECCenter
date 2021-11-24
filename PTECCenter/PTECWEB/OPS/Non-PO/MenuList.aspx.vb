Public Class MenuList
    Inherits System.Web.UI.Page

    Public itemtable As DataTable

    Public cntdt As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

            searchjobslist()
        Else

            itemtable = Session("joblist")
        End If
    End Sub
    'Private Function createdetailtable() As DataTable
    '    Dim dt As New DataTable

    '    dt.Columns.Add("jobcode", GetType(String))
    '    dt.Columns.Add("jobdate", GetType(Date))
    '    dt.Columns.Add("jobtype", GetType(String))
    '    dt.Columns.Add("details", GetType(String))
    '    dt.Columns.Add("status", GetType(String))
    '    dt.Columns.Add("closedate", GetType(Date))
    '    dt.Columns.Add("detailFollow", GetType(String))
    '    dt.Columns.Add("cost", GetType(Double))
    '    dt.Columns.Add("link", GetType(String))

    '    Return dt
    'End Function

    Private Sub searchjobslist()

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            itemtable = objNonPO.PaymentList_For_Operator()

            Session("joblist") = itemtable
            BindData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindData()
        cntdt = itemtable.Rows.Count
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

End Class