Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Remind

    Public Function Remind_Info_Save(remindid As Double, branchid As Double,
                                     reminddetail As String, emailmessage As String,
                                     email As String, warningday As Integer, warningdayofweek As Integer,
                                     reminddate As DateTime, periodno As Integer, periodtype As Integer,
                                     usercode As String, active As Integer) As String
        Dim result As String

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Remind_Info_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@remindid", SqlDbType.BigInt).Value = remindid
        cmd.Parameters.Add("@branchid", SqlDbType.BigInt).Value = branchid
        cmd.Parameters.Add("@reminddetail", SqlDbType.VarChar).Value = reminddetail
        cmd.Parameters.Add("@emailmessage", SqlDbType.VarChar).Value = emailmessage
        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email
        cmd.Parameters.Add("@warningday", SqlDbType.Int).Value = warningday
        cmd.Parameters.Add("@warningdayofweek", SqlDbType.Int).Value = warningdayofweek
        cmd.Parameters.Add("@reminddate", SqlDbType.DateTime).Value = reminddate
        cmd.Parameters.Add("@periodno", SqlDbType.Int).Value = periodno
        cmd.Parameters.Add("@periodtype", SqlDbType.Int).Value = periodtype
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@active", SqlDbType.Int).Value = active


        adp.SelectCommand = cmd

        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("remindid")
        conn.Close()

        Return result
    End Function
    Public Function List(usercode As String, Branchid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Remind_ListByBranch"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@branchid", SqlDbType.BigInt).Value = Branchid 'เผื่อไว้ใช้กรณี เลือกแสดง active nonactive
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function List(usercode As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Remind_List_by_User"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@active", SqlDbType.Int).Value = 0 'เผื่อไว้ใช้กรณี เลือกแสดง active nonactive
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function Info(remindid As Double) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Remind_Info"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@remindid", SqlDbType.BigInt).Value = remindid

        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function UpdateRemindDate(usercode As String, remindid As Double, reminddate As DateTime) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Remind_UpdateHistory"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@remindid", SqlDbType.BigInt).Value = remindid
        cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = reminddate 'เผื่อไว้ใช้กรณี เลือกแสดง active nonactive
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
End Class
