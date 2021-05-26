Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration
Public Class gsm

    Public Function Deduct_Sell_Approve_or_reject(id As Double, status As String, reason As String, user As String) As Boolean
        Dim result As Boolean = True

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_gsm").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "Deduct_Sell_Approve_or_reject"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@reason", SqlDbType.VarChar).Value = reason
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
    Public Function Deduct_Sell_List(begindate As String, enddate As String, docstatus As String, approvestatus As String) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_gsm").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "Deduct_Sell_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = docstatus
        cmd.Parameters.Add("@approvestatus", SqlDbType.VarChar).Value = approvestatus


        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function


    Public Function GSM_Prepair_GSMtoD365(branch As String, closedate As String) As Boolean
        Dim result As Boolean

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_Prepair_GSMtoD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = closedate

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds
        conn.Close()
        Return result
    End Function
    Public Function GSM_save_gsm_paid(br_code As String, closedate As String, td_description As String,
                                      amount As Double) As Boolean
        Dim result As Boolean

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_save_gsm_paid"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@closedate", SqlDbType.VarChar).Value = closedate
        cmd.Parameters.Add("@br_code", SqlDbType.VarChar).Value = br_code
        cmd.Parameters.Add("@td_description", SqlDbType.VarChar).Value = td_description
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = amount

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds
        conn.Close()
        Return result
    End Function

    Public Function GSM_save_gsm_sale_oil(br_code As String, closedate As String, grade_title As String,
                                            volume As Double, new_price As Double) As Boolean
        Dim result As Boolean

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_save_gsm_sale_oil"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@closedate", SqlDbType.VarChar).Value = closedate
        cmd.Parameters.Add("@br_code", SqlDbType.VarChar).Value = br_code
        cmd.Parameters.Add("@grade_title", SqlDbType.VarChar).Value = grade_title
        cmd.Parameters.Add("@volume", SqlDbType.Float).Value = volume
        cmd.Parameters.Add("@new_price", SqlDbType.Float).Value = new_price

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds
        conn.Close()
        Return result
    End Function

    Public Function GSM_save_gsm_sale_other(br_code As String, closedate As String, doccode As String,
                                            ar_number As String, customer_name As String, product As String,
                                            volume As Double, amount As Double, duedate As String) As Boolean
        Dim result As Boolean

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_save_gsm_sale_other"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@closedate", SqlDbType.VarChar).Value = closedate
        cmd.Parameters.Add("@br_code", SqlDbType.VarChar).Value = br_code
        cmd.Parameters.Add("@doccode", SqlDbType.VarChar).Value = doccode
        cmd.Parameters.Add("@ar_number", SqlDbType.VarChar).Value = ar_number
        cmd.Parameters.Add("@customer_name", SqlDbType.VarChar).Value = customer_name
        cmd.Parameters.Add("@product", SqlDbType.VarChar).Value = product
        cmd.Parameters.Add("@volume", SqlDbType.Float).Value = volume
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = amount
        cmd.Parameters.Add("@duedate", SqlDbType.VarChar).Value = duedate

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds
        conn.Close()
        Return result
    End Function

    Public Function GSM_delete_gsc(br_code As String, closedate As String) As Boolean
        'ลบข้อมูลเก่า เวลากด create and download อีกครั้ง
        Dim result As Boolean

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_delete_gsc"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@closedate", SqlDbType.VarChar).Value = closedate
        cmd.Parameters.Add("@br_code", SqlDbType.VarChar).Value = br_code


        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds
        conn.Close()
        Return result
    End Function


    Public Function getGSCbyBranchDate(branch As String, closedate As String) As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_human").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_to_D365_Detail_forD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = closedate


        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function Calc_summary_GSM_to_D365_byDate(branch As String, closedate As String) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_human").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "Calc_summary_GSM_to_D365_byDate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = closedate


        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function GSM_Data_for_D365(branch As String, closedate As String) As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_Data_for_D365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@closedate", SqlDbType.VarChar).Value = closedate
        'GSM_Data_for_D365

        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function GSM_to_D365_Detail_byBranchDate(branch As String, closedate As String) As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_human").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "GSM_to_D365_Detail_byBranchDate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = closedate
        'GSM_Data_for_D365

        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Sub Send_GSM_to_Batch(branch As String, closedate As String, status As Integer, userid As Double)
        'Dim result As Boolean

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_human").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = 600
        cmd.CommandText = "Send_GSM_to_Batch"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@closedate", SqlDbType.VarChar).Value = closedate
        cmd.Parameters.Add("@status", SqlDbType.Int).Value = status
        cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = userid


        'Try
        cmd.ExecuteNonQuery()
        'Catch ex As Exception
        'Throw ex
        'End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0)
        conn.Close()
        'Return result
    End Sub
End Class
