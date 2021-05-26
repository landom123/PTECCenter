Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Account
    Public Function Credit_List_DateOfData() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hq").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 60
        cmd.CommandText = "Credit_Balance_List_Createdate"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@comcode", SqlDbType.VarChar).Value = comcode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
End Class
