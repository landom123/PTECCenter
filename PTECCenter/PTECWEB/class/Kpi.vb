Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Kpi
    Public Sub SetCboRatioType(obj As Object)
        obj.DataSource = Me.RatioType_List()
        obj.DataValueField = "category_id"
        obj.DataTextField = "categoryname"
        obj.DataBind()

    End Sub
    Public Function RatioType_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_RatioType_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@grpid", SqlDbType.VarChar).Value = grpid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

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
    Public Function Kpi_List_For_Operator(depid As String, secid As String, comid As String, ratioid As String, positionid As String, branchgroupid As String, branchid As String, createbyid As String, userid As String, category As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_List_For_Operator"
        cmd.CommandType = CommandType.StoredProcedure



        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@ratioid", SqlDbType.VarChar).Value = ratioid
        cmd.Parameters.Add("@positionid", SqlDbType.VarChar).Value = positionid
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

    Public Function Kpi_List_For_Owner(depid As String, secid As String, comid As String, ratioid As String, positionid As String, branchgroupid As String, branchid As String, createbyid As String, userid As String, category As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_List_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@ratioid", SqlDbType.VarChar).Value = ratioid
        cmd.Parameters.Add("@positionid", SqlDbType.VarChar).Value = positionid
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

    Public Function Kpi_List_For_Branch(managerid As String, cardid As String, branchid As String, userid As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_List_For_Branch"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@managerid", SqlDbType.VarChar).Value = managerid
        cmd.Parameters.Add("@cardid", SqlDbType.VarChar).Value = cardid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function Kpi_Find_by_code(kpicode As String, user As String) As DataSet
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
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

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
        cmd.Parameters.Add("@actionrateowner", SqlDbType.VarChar).Value = If(String.IsNullOrEmpty(actionrateowner), DBNull.Value, actionrateowner)
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
        cmd.Parameters.Add("@actionratehead", SqlDbType.VarChar).Value = If(String.IsNullOrEmpty(actionratehead), DBNull.Value, actionratehead)
        cmd.Parameters.Add("@actionfeedback", SqlDbType.VarChar).Value = actionfeedback
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Function SaveDetailToActionplanOP(actionid As String, actiontitle As String, actionmonthlyid As String,
                                       actiontitleresult As String, actionrateowner As String, actionratehead As String, actionfeedback As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_ActionPlanOP_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@actionid", SqlDbType.VarChar).Value = actionid
        cmd.Parameters.Add("@actiontitle", SqlDbType.VarChar).Value = actiontitle
        cmd.Parameters.Add("@actionmonthlyid", SqlDbType.VarChar).Value = actionmonthlyid
        cmd.Parameters.Add("@actiontitleresult", SqlDbType.VarChar).Value = actiontitleresult
        cmd.Parameters.Add("@actionrateowner", SqlDbType.VarChar).Value = If(String.IsNullOrEmpty(actionrateowner), DBNull.Value, actionrateowner)
        cmd.Parameters.Add("@actionratehead", SqlDbType.VarChar).Value = If(String.IsNullOrEmpty(actionratehead), DBNull.Value, actionratehead)
        cmd.Parameters.Add("@actionfeedback", SqlDbType.VarChar).Value = actionfeedback
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Function KPIPermisstion(cat As String, type As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "KPI_Permisstion"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = cat
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("acc")
        conn.Close()
        Return result

    End Function
    Public Sub Kpi_HeadOP_Save(kpicode As String, categoryid As Integer,
                               title As String, weight As Double, unit As String,
                               lv5 As String, lv4 As String, lv3 As String, lv2 As String, lv1 As String,
                               usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_HeadOP_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = kpicode
        cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = categoryid
        cmd.Parameters.Add("@kpititle", SqlDbType.VarChar).Value = title
        cmd.Parameters.Add("@unit", SqlDbType.VarChar).Value = unit
        cmd.Parameters.Add("@weight", SqlDbType.Money).Value = weight
        cmd.Parameters.Add("@lv5", SqlDbType.VarChar).Value = lv5
        cmd.Parameters.Add("@lv4", SqlDbType.VarChar).Value = lv4
        cmd.Parameters.Add("@lv3", SqlDbType.VarChar).Value = lv3
        cmd.Parameters.Add("@lv2", SqlDbType.VarChar).Value = lv2
        cmd.Parameters.Add("@lv1", SqlDbType.VarChar).Value = lv1
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub


    Public Function deleteHeadbyKPICode(kpicode As String, user As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_HeadOP_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = kpicode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function
End Class
