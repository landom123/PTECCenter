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

    Public Sub SetCboForms_by_periodid(obj As Object, periodid As Integer)
        obj.DataSource = Me.Kpi_Forms_List_by_periodid(periodid)
        obj.DataValueField = "KPIForm_ID"
        obj.DataTextField = "title"
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

    Public Function Kpi_List_For_Owner(depid As String, secid As String, comid As String, ratioid As String, positionid As String, branchgroupid As String, branchid As String, createbyid As String, userid As String, category As String, preriodid As String, managerid As String) As DataSet
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
        cmd.Parameters.Add("@managerid", SqlDbType.VarChar).Value = managerid

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

    Public Function Kpi_Get_NewKPIsOfPeriod_by_Usercode(periodid As Integer, user As String, Optional type As String = "kpi") As DataSet
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

    Public Function newkpi_For_Owner(depid As String, secid As String, comid As String,
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
        cmd.CommandText = "Kpi_NewKPIs_List_For_Owner"
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
    Public Function Kpi_Forms_List_by_periodid(period_id As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Forms_List_by_periodid"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpiperiod_id", SqlDbType.Int).Value = period_id
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Kpi_Forms_List_by_userid(period_id As Integer, Optional userid As Integer = 0) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Forms_List_by_userid"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@kpiperiod_id", SqlDbType.Int).Value = period_id
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

    Public Function Kpi_FormsDtl_by_formid_and_ownerid(formid As Integer, ownerid As Integer) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsDtl_by_formid_and_ownerid"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@kpiform_id", SqlDbType.Int).Value = formid
        cmd.Parameters.Add("@ownerid", SqlDbType.Int).Value = ownerid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function Kpi_FormsRes_Save(ownerid As Integer, kpiforms_id As Integer, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsRes_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@owner_id", SqlDbType.Int).Value = ownerid
        cmd.Parameters.Add("@KPIForm_ID", SqlDbType.Int).Value = kpiforms_id
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsAns_Save(resid As Integer, kpiforms_id As Integer, usertype As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@res_id", SqlDbType.Int).Value = resid
        cmd.Parameters.Add("@KPIFormDtl_ID", SqlDbType.Int).Value = kpiforms_id
        cmd.Parameters.Add("@usertype", SqlDbType.VarChar).Value = usertype
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsAns_kpis_Save(ans_id As Integer, kpi_code As String, txtRate As String, txtReason As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_kpis_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ans_id", SqlDbType.Int).Value = ans_id
        cmd.Parameters.Add("@KPI_code", SqlDbType.VarChar).Value = kpi_code
        cmd.Parameters.Add("@txtRate", SqlDbType.VarChar).Value = If(txtRate Is Nothing, DBNull.Value, txtRate)
        cmd.Parameters.Add("@txtReason", SqlDbType.VarChar).Value = If(txtReason Is Nothing, DBNull.Value, txtReason)
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function
    Public Function Kpi_FormsAns_projects_Save(ans_id As Integer,
                                               txtTopic As String, txtObjective As String,
                                               txtMethod As String, txtResult As String, txtStakeholders As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_projects_Save"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@ans_id", SqlDbType.Int).Value = ans_id
        cmd.Parameters.Add("@txtTopic", SqlDbType.VarChar).Value = If(txtTopic Is Nothing, DBNull.Value, txtTopic)
        cmd.Parameters.Add("@txtObjective", SqlDbType.VarChar).Value = If(txtObjective Is Nothing, DBNull.Value, txtObjective)
        cmd.Parameters.Add("@txtMethod", SqlDbType.VarChar).Value = If(txtMethod Is Nothing, DBNull.Value, txtMethod)
        cmd.Parameters.Add("@txtResult", SqlDbType.VarChar).Value = If(txtResult Is Nothing, DBNull.Value, txtResult)
        cmd.Parameters.Add("@txtStakeholders", SqlDbType.VarChar).Value = If(txtStakeholders Is Nothing, DBNull.Value, txtStakeholders)
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsAns_txt_Save(ans_id As Integer, txtDetail As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_txt_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ans_id", SqlDbType.Int).Value = ans_id
        cmd.Parameters.Add("@txt_detail", SqlDbType.VarChar).Value = txtDetail
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsAns_cbo_Save(ans_id As Integer, cbo_id As Integer, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_cbo_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ans_id", SqlDbType.Int).Value = ans_id
        cmd.Parameters.Add("@cbo_id ", SqlDbType.Int).Value = cbo_id
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_Find_FormsRes_by_userid(formid As Integer, ownerid As Integer) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_Find_FormsRes_by_userid"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@kpiform_id", SqlDbType.Int).Value = formid
        cmd.Parameters.Add("@ownerid", SqlDbType.Int).Value = ownerid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function Kpi_FormsAns_career_Save(ans_id As Integer, career_id As Integer, txtRate As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_career_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ans_id", SqlDbType.Int).Value = ans_id
        cmd.Parameters.Add("@career_id", SqlDbType.Int).Value = career_id
        cmd.Parameters.Add("@txtRate", SqlDbType.VarChar).Value = txtRate
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsRes_Del(owner_id As Integer, kpiform_id As Integer, userid As Integer) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsRes_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@owner_id", SqlDbType.Int).Value = owner_id
        cmd.Parameters.Add("@KPIForm_ID", SqlDbType.Int).Value = kpiform_id
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Sub Kpi_FormsRes_Submit(owner_id As Integer, kpiform_id As Integer, userid As Integer)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsRes_Submit"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@owner_id", SqlDbType.Int).Value = owner_id
        cmd.Parameters.Add("@KPIForm_ID", SqlDbType.Int).Value = kpiform_id
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub Kpi_FormsRes_Approval(owner_id As Integer, approval_by As Integer, kpiform_id As Integer, userid As Integer)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsRes_Approval"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@owner_id", SqlDbType.Int).Value = owner_id
        cmd.Parameters.Add("@approval_by", SqlDbType.Int).Value = approval_by
        cmd.Parameters.Add("@KPIForm_ID", SqlDbType.Int).Value = kpiform_id
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Function Kpi_FormsAns_projects_Accept(project_ans_id As Integer, approval_by As Integer, status As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_projects_Accept"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@project_ans_id ", SqlDbType.Int).Value = project_ans_id
        cmd.Parameters.Add("@approval_by ", SqlDbType.Int).Value = approval_by
        cmd.Parameters.Add("@status ", SqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsAns_projects_Reject(project_ans_id As Integer, reject_by As Integer, status As String, userid As Integer) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsAns_projects_Reject"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@project_ans_id ", SqlDbType.Int).Value = project_ans_id
        cmd.Parameters.Add("@reject_by ", SqlDbType.Int).Value = reject_by
        cmd.Parameters.Add("@status ", SqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function Kpi_FormsRes_List_For_Owner(depid As String, secid As String, comid As String,
                                        branchgroupid As String,
                                        branchid As String, ownerid As String, userid As Integer,
                                        category As String, preriodid As Integer, formid As Integer, Optional maxrows As Integer = 1000) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsRes_List_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@preriodid", SqlDbType.Int).Value = preriodid
        cmd.Parameters.Add("@formid", SqlDbType.Int).Value = formid
        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@ownerid", SqlDbType.VarChar).Value = ownerid
        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@maxrows", SqlDbType.Int).Value = maxrows


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Kpi_GetRatio_by_positionid(periodid As Integer, positionid As Integer, type_kpi As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_GetRatio_by_positionid"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@periodid", SqlDbType.Int).Value = periodid
        cmd.Parameters.Add("@positionid", SqlDbType.Int).Value = positionid
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type_kpi



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Kpi_GetRatio_kpiscore_by_res_code(res_code As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_GetRatio_kpiscore_by_res_code"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@res_code", SqlDbType.VarChar).Value = res_code



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Kpi_GetRatio_careerscore_by_res_code(res_code As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_GetRatio_careerscore_by_res_code"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@res_code", SqlDbType.VarChar).Value = res_code



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Sub Kpi_FormsRes_Del_by_Code(res_code As String, note As String, userid As Integer)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_hrd").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Kpi_FormsRes_Del_by_Code"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@res_code", SqlDbType.VarChar).Value = res_code
        cmd.Parameters.Add("@cancel_msg", SqlDbType.VarChar).Value = note
        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid


        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
End Class
