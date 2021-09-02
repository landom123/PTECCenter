Public Class PaymentPage
    Inherits System.Web.UI.Page
    Public CommentTable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch
        Dim approval As New Approval
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier



        If Not IsPostBack() Then
            approval.SetCboApprovalCategory(cboBrancha)

            'objsupplier.SetCboVendorByName(cboName, Session("username"))

            objbranch.SetComboBranch(cboBranch, Session("usercode"))
            objdep.SetCboDepartment(cboDepartment, 0)
            objsec.SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
            objsupplier.SetCboVendorByName(cboName, "")
            objsupplier.SetCboVendorByName(cboVendor, "")
            setHead()

        End If


        CommentTable = createtablecomment()
    End Sub

    Private Sub setHead()

        Dim objsec As New Section
        cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(Session("secid").ToString))
        cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
        cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid").ToString))
        objsec.SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
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

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection(cboSection, depid)
    End Sub
End Class