Public Class BranchPermission
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public branchUser As DataTable
    Public maintable As DataTable
    Public branchtable As DataTable
    Public username, usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objBranch As New Branch


        username = Session("username")
        usercode = Session("usercode")


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        branchtable = LoadBranchAll()
        If Not IsPostBack() Then
            SetCboUsers(cboUser)

        End If


    End Sub

    Private Sub cboUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUser.SelectedIndexChanged
        Dim users As New Users
        Dim branch As New Branch
        If Not cboUser.SelectedItem.Value.ToString = "0" Then
            maintable = users.Find(cboUser.SelectedItem.Value.ToString, "", "")
            branchUser = branch.ListAllForSelected(maintable.Rows(0).Item("UserCode").ToString)
            For i = 0 To branchUser.Rows.Count - 1
                If Not branchUser.Rows(i).Item("selected") Then
                    branchUser.Rows(i).Delete()
                End If
            Next i
            branchUser.AcceptChanges()
            Session("cbouser_branch") = cboUser.SelectedItem.Text
        End If

    End Sub


    <System.Web.Services.WebMethod>
    Public Shared Function addBranchPermission(ByVal usercode As String, ByVal branchid As String)
        Dim objbranch As New Branch

        Dim mn As New menu
        Dim dt As DataTable
        Try
            dt = objbranch.PermissionAdd(usercode, branchid)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)
        Catch ex As Exception
            Return False
        End Try

    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function removeBranchPermission(ByVal usercode As String, ByVal branchid As String)
        Dim objbranch As New Branch

        Dim mn As New menu
        Dim dt As DataTable
        Try
            dt = objbranch.PermissionRemove(usercode, branchid)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)
        Catch ex As Exception
            Return False
        End Try

    End Function
End Class