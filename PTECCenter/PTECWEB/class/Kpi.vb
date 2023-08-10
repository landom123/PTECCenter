Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Kpi

    Public Function Kpi_Find_Overview(preriodid As Integer, user As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Find_Overview"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@preriodid", SqlDbType.Int).Value = preriodid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function Kpi_Find_Follow(depid As String, secid As String, comid As String, branchgroupid As String, branchid As String, createbyid As String, userid As String, category As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Find_Follow"
        cmd.CommandType = CommandType.StoredProcedure



        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@createbyid", SqlDbType.VarChar).Value = createbyid
        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function Kpi_Find_by_code(kpicode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Find_by_code"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = kpicode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function


    Public Function SaveDetailToActionplan(actionid As String, actionmonthlyid As String,
                                       actiontitleresult As String, actionrateowner As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_ActionPlan_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@actionid", SqlDbType.VarChar).Value = actionid
        cmd.Parameters.Add("@actionmonthlyid", SqlDbType.VarChar).Value = actionmonthlyid
        cmd.Parameters.Add("@actiontitleresult", SqlDbType.VarChar).Value = actiontitleresult
        cmd.Parameters.Add("@actionrateowner", SqlDbType.VarChar).Value = actionrateowner
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Function SaverateHeadToActionplan(actionid As String,
                                       actionratehead As String, actionfeedback As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_SubmitRate_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@actionid", SqlDbType.VarChar).Value = actionid
        cmd.Parameters.Add("@actionratehead", SqlDbType.VarChar).Value = actionratehead
        cmd.Parameters.Add("@actionfeedback", SqlDbType.VarChar).Value = actionfeedback
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function
End Class
