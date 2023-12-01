Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Memo


    Public Sub SetCboMemoType(cbo As DropDownList)

        Dim dtcost As DataTable = MemoType_List()
        cbo.Items.Clear()
        For i As Integer = 0 To dtcost.Rows.Count - 1
            Dim item As ListItem = New ListItem(dtcost.Rows(i).Item("type_name"), dtcost.Rows(i).Item("memo_typeid"))
            Dim category_name As String = dtcost.Rows(i).Item("category_name").ToString
            If Not String.IsNullOrEmpty(category_name) Then
                item.Attributes("data-tokens") = category_name
                item.Attributes("data-category") = category_name
            End If
            'If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("groupat").ToString) Then
            '    item.Attributes("data-subtext") = "- (" + dtcost.Rows(i).Item("groupat").ToString + ")"
            'End If
            cbo.Items.Add(item)

        Next

    End Sub

    Public Function MemoType_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "MemoType_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function rvtohq(voucher As String, transdate As DateTime, account As String, branch As String, amount As Double, user As String) As Boolean
        Dim result As Boolean = True

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "RV_to_HQ"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@voucher", SqlDbType.VarChar).Value = voucher
        cmd.Parameters.Add("@transdate", SqlDbType.DateTime).Value = transdate
        cmd.Parameters.Add("@account", SqlDbType.VarChar).Value = account
        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = amount
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
            result = False
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
End Class
