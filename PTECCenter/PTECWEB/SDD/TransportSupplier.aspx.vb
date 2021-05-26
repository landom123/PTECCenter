
Public Class TransportSupplier
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public suppliertable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'gsmtable = Session("gsmtable")
            'BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("suppliertable") Is Nothing) Then
                suppliertable = Session("suppliertable")
                BindData()
            Else
                SupplierList()
            End If
        End If

    End Sub
    Private Sub SupplierList()
        Dim objsup As New Supplier
        suppliertable = objsup.TransportSupplierList
        gvData.DataSource = suppliertable
        Session("suppliertable") = suppliertable
        BindData()
    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("accountnum", GetType(String))
        dt.Columns.Add("accountname", GetType(String))
        dt.Columns.Add("suppliercode", GetType(Date))
        dt.Columns.Add("suppliername", GetType(Date))

        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = suppliertable
        gvData.DataBind()
    End Sub


    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        'Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        'If Not IsNothing(chk) Then
        '    chk.Checked = e.Row.DataItem("chk")
        'End If

    End Sub

    'Private Sub btnselectall_Click(sender As Object, e As EventArgs) Handles btnselectall.Click
    '    Dim chk As CheckBox
    '    For Each row As GridViewRow In gvData.Rows

    '        chk = CType(row.FindControl("chk"), CheckBox)
    '        chk.Checked = True
    '    Next

    'End Sub

    'Private Sub btnunselect_Click(sender As Object, e As EventArgs) Handles btnunselect.Click
    '    Dim chk As CheckBox
    '    For Each row As GridViewRow In gvData.Rows

    '        chk = CType(row.FindControl("chk"), CheckBox)
    '        chk.Checked = False
    '    Next
    'End Sub
End Class