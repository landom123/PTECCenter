Public Class PaymentPage
    Inherits System.Web.UI.Page
    Public CommentTable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim approval As New Approval
        'Approval.SetCboApprovalCategory(cboBranch)

        CommentTable = createtablecomment()
    End Sub
    Private Function createtablecomment() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("commentid", GetType(Integer))
        dt.Columns.Add("commentdetail", GetType(String))
        dt.Columns.Add("updateby", GetType(Integer))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(Integer))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("paymentid", GetType(Integer))

        Return dt
    End Function
End Class