Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class AgreeClient

    Public Function Client_Get_Info(clientid As Double) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Get_Info"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@clientid", SqlDbType.VarChar).Value = clientid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Client_Cbo_List(agreeid As Double) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Cbo_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.VarChar).Value = agreeid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
End Class
Public Class TitleName

    Public Function List() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "TitleName_List"
        cmd.CommandType = CommandType.StoredProcedure

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

Public Class Agree

    Public Function Ag_Payment_Table_Calcuate(agreeid As Double, volume As Integer) As DataSet
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Payment_Table_Calcuate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    '
    Public Function Ag_Monthly_Payment_Calcuate(agreeid As Double, monthly As String) As DataSet
        'แสดงข้อมูลตารางค่าใช้จ่ายที่ต้องจ่ายในเดือน
        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Monthly_Payment_Calcuate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    Public Function Ag_Payment_Condition(agreeid As Double) As DataSet
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Payment_Condition"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function


    Public Function Ag_Status_list() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Status_list"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        'cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
End Class
