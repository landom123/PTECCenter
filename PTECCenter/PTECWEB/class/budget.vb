Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Budgets

    Public Function save(usercode As String, year As Integer, begindate As DateTime, enddate As DateTime) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_budget").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Budget_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmd.Parameters.Add("@begindate", SqlDbType.DateTime).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.DateTime).Value = enddate
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
End Class
