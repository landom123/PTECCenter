Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Kpi
    Public Sub SetCboPeriod(obj As Object)
        obj.DataSource = Me.Kpi_Period_List()
        obj.DataValueField = "KpiPeriod_ID"
        obj.DataTextField = "Description"
        obj.DataBind()

    End Sub
    Public Sub SetCboRatioType(obj As Object)
        obj.DataSource = Me.RatioType_List()
        obj.DataValueField = "category_id"
        obj.DataTextField = "categoryname"
        obj.DataBind()

    End Sub
    Public Sub SetCboRatioTypeByUser(obj As Object, usersid As Integer)
        obj.DataSource = Me.RatioTypeByUser_List(usersid)
        obj.DataValueField = "category_id"
        obj.DataTextField = "categoryname"
        obj.DataBind()

    End Sub
    Public Function Kpi_Period_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Period_List"
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

    Public Function Kpi_Find_Overview(preriodid As Integer, nameowner As String, user As String) As DataSet
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
        cmd.Parameters.Add("@nameowner", SqlDbType.VarChar).Value = nameowner
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = user



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function Kpi_Find_Overview_For_Branch(preriodid As Integer, nameowner As String, user As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Find_Overview_For_Branch"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@preriodid", SqlDbType.Int).Value = preriodid
        cmd.Parameters.Add("@nameowner", SqlDbType.VarChar).Value = nameowner
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = user



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function Kpi_List_For_Operator(depid As String, secid As String, comid As String, ratioid As String, positionid As String, branchgroupid As String, branchid As String, createbyid As String, userid As String, category As String, preriodid As String, managerid As String) As DataSet
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
        cmd.Parameters.Add("@preriodid", SqlDbType.VarChar).Value = preriodid
        cmd.Parameters.Add("@managerid", SqlDbType.VarChar).Value = managerid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function Kpi_List_For_Owner(depid As String, secid As String, comid As String, ratioid As String, positionid As String, branchgroupid As String, branchid As String, createbyid As String, userid As String, category As String, preriodid As String) As DataSet
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
        cmd.Parameters.Add("@preriodid", SqlDbType.VarChar).Value = preriodid

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
        cmd.CommandText = "Kpi_OP_ActionPlan_Save"
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
    Public Sub Kpi_HeadOP_Save(kpicode As String, ownerid As Integer, categoryid As Integer,
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
        cmd.CommandText = "Kpi_OP_Head_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = kpicode
        cmd.Parameters.Add("@ownerid", SqlDbType.Int).Value = ownerid
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
        cmd.CommandText = "Kpi_OP_Head_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = kpicode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function UpdateOwnerActionPlan(actionid As String, ownername As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_OP_OwnerActionPlan_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@actionid", SqlDbType.VarChar).Value = actionid
        cmd.Parameters.Add("@ownername", SqlDbType.VarChar).Value = ownername
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_ActionPlanTitle_Save(actionid As String, title As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_ActionPlanTitle_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@actionid", SqlDbType.VarChar).Value = actionid
        cmd.Parameters.Add("@actiontitle", SqlDbType.VarChar).Value = title
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Sub Kpi_Complete_Save(code As String, chk As Boolean, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Complete_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = code
        cmd.Parameters.Add("@chk", SqlDbType.Bit).Value = chk
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Function UpdateOwnerBranch(actionid As String, branchid As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_ActionPlanOwnerBranch_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@actionid", SqlDbType.VarChar).Value = actionid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_Get_NewKPIsOfPeriod_by_Usercode(periodid As Integer, user As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIs_GetRatio"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@periodid", SqlDbType.Int).Value = periodid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function Kpi_NewKPIs_Find(newkpicode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIs_Find"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@newkpicode", SqlDbType.VarChar).Value = newkpicode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function deleteDetailbynewkpicode(newkpicode As String, user As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIsDtl_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpicode", SqlDbType.VarChar).Value = newkpicode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function
    Public Function SaveNewKPIs(newkpino As String, headtable As DataTable, detailtable As DataTable, usercode As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        newkpino = SaveHeadNewKpi(headtable, usercode)
        result = newkpino
        SaveDetailNewKpi(newkpino, detailtable, usercode)


        Return result
    End Function
    Private Function SaveHeadNewKpi(mytable As DataTable, usercode As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "KPI_NewKPIs_Save"
        cmd.CommandType = CommandType.StoredProcedure
        With mytable.Rows(0)
            cmd.Parameters.Add("@newkpicode", SqlDbType.VarChar).Value = .Item("newkpicode")
            cmd.Parameters.Add("@preriodid", SqlDbType.VarChar).Value = .Item("preriodid")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        End With


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function
    Private Sub SaveDetailNewKpi(newkpino As String, mytable As DataTable, usercode As String)
        Dim nonpocode As String
        Dim cnt As Integer = mytable.Rows.Count - 1
        With mytable
            For i = 0 To cnt
                If .Rows(i).Item("status") = "new" Or .Rows(i).Item("status") = "edit" Then
                    nonpocode = SaveDetailToTable(newkpino,
                                      .Rows(i).Item("kpi_code").ToString,
                                      .Rows(i).Item("title").ToString,
                                      .Rows(i).Item("categoryid"),
                                      .Rows(i).Item("weight"),
                                      .Rows(i).Item("unit").ToString,
                                      .Rows(i).Item("lv1").ToString,
                                      .Rows(i).Item("lv2").ToString,
                                      .Rows(i).Item("lv3").ToString,
                                      .Rows(i).Item("lv4").ToString,
                                      .Rows(i).Item("lv5").ToString,
                                     usercode)
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub
    Private Function SaveDetailToTable(newkpino As String, kpino As String, title As String, categoryid As Integer, weight As Integer, unit As String,
                                       lv1 As String, lv2 As String, lv3 As String, lv4 As String, lv5 As String,
                                    user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIsDtl_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@newkpino", SqlDbType.VarChar).Value = newkpino
        cmd.Parameters.Add("@kpino", SqlDbType.VarChar).Value = kpino
        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title
        cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = categoryid
        cmd.Parameters.Add("@weight", SqlDbType.Int).Value = weight
        cmd.Parameters.Add("@unit", SqlDbType.VarChar).Value = unit
        cmd.Parameters.Add("@lv1", SqlDbType.VarChar).Value = lv1
        cmd.Parameters.Add("@lv2", SqlDbType.VarChar).Value = lv2
        cmd.Parameters.Add("@lv3", SqlDbType.VarChar).Value = lv3
        cmd.Parameters.Add("@lv4", SqlDbType.VarChar).Value = lv4
        cmd.Parameters.Add("@lv5", SqlDbType.VarChar).Value = lv5
        'cmd.Parameters.Add("@preriodid", SqlDbType.Int).Value = preriodid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function
    Public Function newkpi_For_Operator(depid As String, secid As String, comid As String,
                                        ratioid As String, positionid As String, branchgroupid As String,
                                        branchid As String, createbyid As String, userid As String,
                                        category As String, preriodid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIs_List_For_Operator"
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
        cmd.Parameters.Add("@preriodid", SqlDbType.VarChar).Value = preriodid



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Sub Confirm(newkpino As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIs_Confirm"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@newkpino", SqlDbType.VarChar).Value = newkpino
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub Kpi_NewKPIs_Cancel(newkpino As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIs_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@newkpino", SqlDbType.VarChar).Value = newkpino
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub Kpi_NewKPIs_Allow(newkpino As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_NewKPIs_Allow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@newkpino", SqlDbType.VarChar).Value = newkpino
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub KPI_NewKPIs_Send_To_Edit(newkpino As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "KPI_NewKPIs_Send_To_Edit"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@newkpino", SqlDbType.VarChar).Value = newkpino
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Function Kpi_Report(preriodid As Integer) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Rpt_All"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@preriodid", SqlDbType.Int).Value = preriodid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function RatioTypeByUser_List(userid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_RatioType_By_User_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
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
