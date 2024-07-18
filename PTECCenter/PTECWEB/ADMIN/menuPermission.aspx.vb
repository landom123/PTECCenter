Public Class menuPermission
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public menuUser As DataTable
    Public maintable As DataTable
    Public menuAlltable As DataSet
    Public username, usercode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objBranch As New Branch


        username = Session("username")
        usercode = Session("usercode")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

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
        menuAlltable = LoadMenuAll()
        If Not IsPostBack() Then
            SetCboUsers(cboUser, "", "ALL")

        End If
    End Sub

    Private Sub cboUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUser.SelectedIndexChanged
        Dim users As New Users
        If Not cboUser.SelectedItem.Value.ToString = "0" Then
            maintable = users.Find(cboUser.SelectedItem.Value.ToString, "", "")
            menuUser = LoadMenu(maintable.Rows(0).Item("UserCode"))
            For i = 0 To menuUser.Rows.Count - 1
                If String.IsNullOrEmpty(menuUser.Rows(i).Item("menu_url").ToString) Then
                    menuUser.Rows(i).Delete()
                End If
            Next i
            menuUser.AcceptChanges()
            Session("cbouser") = cboUser.SelectedItem.Text
        End If

    End Sub


    <System.Web.Services.WebMethod>
    Public Shared Function addMenuPermission(ByVal userid As String, ByVal parentid As String, ByVal menuid As String)

        Dim mn As New menu
        Dim dt As DataTable
        Try
            dt = mn.menuPermissionAdd(userid, parentid, menuid)

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
    Public Shared Function removeMenuPermission(ByVal userid As String, ByVal parentid As String, ByVal menuid As String)

        Dim mn As New menu
        Dim dt As DataTable
        Try
            dt = mn.menuPermissionRemove(userid, parentid, menuid)

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