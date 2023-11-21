Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class rv
    Public Function rvtohq(voucher As String, transdate As DateTime, account As String, branch As String, amount As Double, user As String, iType As Integer) As Boolean
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
        cmd.Parameters.Add("@iType", SqlDbType.Int).Value = iType

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
