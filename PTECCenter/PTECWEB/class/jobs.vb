Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class jobs
    Public Sub SetCboCloseType(obj As Object)
        obj.DataSource = Me.JobCloseType_List()
        obj.DataValueField = "jobclosetypeid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboCloseCategory(obj As Object)
        obj.DataSource = Me.JobCloseCategory_List()
        obj.DataValueField = "jobclosecategoryid"
        obj.DataTextField = "categoryname"
        obj.DataBind()
    End Sub
    Public Sub SetPositionInPumpList(obj As Object, branchid As Integer)
        obj.DataSource = Me.PositionInPump_List(branchid)
        obj.DataValueField = "AssetID"
        obj.DataTextField = "Position"
        obj.DataBind()

    End Sub
    Public Sub SetCboPolicy(obj As Object)
        obj.DataSource = Me.Policy_List()
        obj.DataValueField = "policyid"
        obj.DataTextField = "policy"
        obj.DataBind()

    End Sub

    Public Sub SetCboJobStatusmainList(obj As Object)
        obj.DataSource = Me.JobStatusmain_List()
        obj.DataValueField = "statusid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Sub SetCboJobStatusList(obj As Object)
        obj.DataSource = Me.JobStatus_List()
        obj.DataValueField = "statusid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub

    Public Sub SetCboJobStatusListForReport(obj As Object)
        obj.DataSource = Me.JobStatus_List_For_Report()
        obj.DataValueField = "statusid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Sub SetCboJobCloseTypeList(obj As Object)
        obj.DataSource = Me.JobCloseType_List()
        obj.DataValueField = "jobclosetypeid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub


    Public Sub SetCboJobDtlList(obj As Object, jobno As String)
        obj.DataSource = Me.JobdtlList(jobno)
        obj.DataValueField = "jobdetailid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Sub SetCboJobCenterList(obj As Object)
        obj.DataSource = Me.JobCenter_List()
        obj.DataValueField = "JobCenterID"
        obj.DataTextField = "JobCenterName"
        obj.DataBind()

    End Sub
    Public Sub SetCboJobCenterDtlListByJobCenterID(obj As Object, jobcenterid As String)
        obj.DataSource = Me.JobCenterDtl_List(jobcenterid)
        obj.DataValueField = "JobsCenterDtlID"
        obj.DataTextField = "JobsCenterDtlName"
        obj.DataBind()

    End Sub

    Public Sub SetJobCateList(obj As Object, depid As Integer)
        obj.DataSource = Me.JobCate_List(depid)
        obj.DataValueField = "CateCode"
        obj.DataTextField = "CateName"
        obj.DataBind()

    End Sub

    Public Sub SetJobItemList(obj As Object, catecode As String, groupcode As String)
        obj.DataSource = Me.JobItem_List(catecode, groupcode)
        obj.DataValueField = "itemcode"
        obj.DataTextField = "text"
        obj.DataBind()

    End Sub
    Public Sub SetCboJobCenterDtlListByJobCenterID_GroupByJobCenterid(cboCost As DropDownList, jobcenterid As String)

        Dim dtcost As DataTable = JobCenterDtl_List(jobcenterid)
        cboCost.Items.Clear()
        Dim cnt As Integer = dtcost.Rows.Count - 1
        For i As Integer = 0 To cnt
            Dim item As ListItem = New ListItem(dtcost.Rows(i).Item("JobsCenterDtlName"), dtcost.Rows(i).Item("JobsCenterDtlID"))
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("CostName").ToString) Then
                item.Attributes("data-tokens") = dtcost.Rows(i).Item("CostName").ToString
                item.Attributes("data-category") = dtcost.Rows(i).Item("CostName").ToString
            End If

            cboCost.Items.Add(item)

        Next

    End Sub

    Public Function JobList(usercode As String, status As Integer, Optional maxrows As Integer = 1000) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_List_byUser_Branch"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@status", SqlDbType.BigInt).Value = status
        cmd.Parameters.Add("@maxrows", SqlDbType.Int).Value = maxrows

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        'cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function JobdtlList(jobcode As String) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Jobdtl_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        'cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function



    Public Function JobList_For_Operator(jobcode As String, depid As String, jobtypeid As String, statusfollowid As String,
                                         branchgroupid As String, branchid As String, startdate As String, enddate As String, suppilerid As String, Optional maxrows As Integer = 1000) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_List_byUser_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@jobtypeid", SqlDbType.VarChar).Value = jobtypeid
        cmd.Parameters.Add("@statusfollowid", SqlDbType.VarChar).Value = statusfollowid
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@suppilerid", SqlDbType.VarChar).Value = suppilerid
        cmd.Parameters.Add("@maxrows", SqlDbType.Int).Value = maxrows




        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function JobCloseSave(jobno As String, jobdetailid As Double, jobclosetypeid As Integer,
                                 beginwarr As String, endwarr As String, partamt As Double, laboramt As Double, travelamt As Double,
                                 InvoiceNo As String,
                                 Invoicedate As String, detail As String, closedate As String, usercode As String, jobclosecategoryid As Double) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Close_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobno", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@jobclosetypeid", SqlDbType.Int).Value = jobclosetypeid
        cmd.Parameters.Add("@beginwarr", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(beginwarr), DBNull.Value, Date.Parse(beginwarr))
        cmd.Parameters.Add("@endwarr", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(endwarr), DBNull.Value, Date.Parse(endwarr))
        'cmd.Parameters.Add("@partamt", SqlDbType.Money).Value = partamt
        'cmd.Parameters.Add("@laboramt", SqlDbType.Money).Value = laboramt
        'cmd.Parameters.Add("@travelamt", SqlDbType.Money).Value = travelamt
        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = InvoiceNo
        cmd.Parameters.Add("@invoicedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(Invoicedate), DBNull.Value, Date.Parse(Invoicedate))
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@closedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(closedate), Now.ToString, Date.Parse(closedate))
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@jobclosecategoryid", SqlDbType.BigInt).Value = jobclosecategoryid

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function JobCloseEdit(jobno As String, jobdetailid As Double, jobclosetypeid As Integer,
                                 beginwarr As String, endwarr As String, partamt As Double, laboramt As Double, travelamt As Double,
                                 InvoiceNo As String,
                                 Invoicedate As String, detail As String, closedate As String, usercode As String, jobclosecategoryid As Double) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Close_Edit"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobno", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@jobclosetypeid", SqlDbType.Int).Value = jobclosetypeid
        cmd.Parameters.Add("@beginwarr", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(beginwarr), DBNull.Value, Date.Parse(beginwarr))
        cmd.Parameters.Add("@endwarr", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(endwarr), DBNull.Value, Date.Parse(endwarr))
        'cmd.Parameters.Add("@partamt", SqlDbType.Money).Value = partamt
        'cmd.Parameters.Add("@laboramt", SqlDbType.Money).Value = laboramt
        'cmd.Parameters.Add("@travelamt", SqlDbType.Money).Value = travelamt
        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = InvoiceNo
        cmd.Parameters.Add("@invoicedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(Invoicedate), DBNull.Value, Date.Parse(Invoicedate))
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@closedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(closedate), DBNull.Value, Date.Parse(closedate))
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@jobclosecategoryid", SqlDbType.BigInt).Value = jobclosecategoryid

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function UpdateSupplierandCost(jobno As String, jobdetailid As Double, jobCenterid As Double,
                                          supplierid As Double, cost As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_update_supplier_and_cost"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@jobcenterid", SqlDbType.BigInt).Value = jobCenterid
        cmd.Parameters.Add("@supplierid", SqlDbType.BigInt).Value = supplierid
        cmd.Parameters.Add("@cost", SqlDbType.Money).Value = cost
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function Updatepolicyid(jobno As String, jobdetailid As Double, jobCenterid As Double,
                                          policyid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_update_policyid"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@jobcenterid", SqlDbType.BigInt).Value = jobCenterid
        cmd.Parameters.Add("@policyid", SqlDbType.BigInt).Value = policyid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function Updateduedate(jobno As String, jobdetailid As Double, jobCenterid As Double,
                                          duedate As String, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_update_duedate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@jobcenterid", SqlDbType.BigInt).Value = jobCenterid
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(duedate), DBNull.Value, DateTime.Parse(duedate))
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function JobCloseType_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobclosetype_List"
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
    Public Function JobCloseCategory_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobclosecategory_List"
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

    Public Function JobStatusmain_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Status_List"
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
    Public Function JobStatus_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_Status_List"
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

    Public Function JobStatus_List_For_Report() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_Status_List_For_Report"
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
    Public Function JobCenter_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Center_List"
        cmd.CommandType = CommandType.StoredProcedure

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function JobCenterDtl_List(jobscenterid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_CenterDtl_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobscenterid", SqlDbType.VarChar).Value = jobscenterid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function PositionInPump_List(branchid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_PositionInPump_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@jobtypeid", SqlDbType.VarChar).Value = jobtypeid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function JobGroup_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Group"
        cmd.CommandType = CommandType.StoredProcedure

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function Policy_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "JobtypePolicy_List"
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
    Public Function JobType_List(usercode As String, Optional mode As String = "all") As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "jobtype_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function JobCate_List(DepID As Integer) As DataTable

        Try
            Dim result As DataTable
            'Credit_Balance_List_Createdate
            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "JobCate_List"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@DepID", SqlDbType.VarChar).Value = DepID
            'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
            'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
            'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
            conn.Close()
            Return result

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function JobGroup_List(CateID As Integer) As DataTable

        Try
            Dim result As DataTable
            'Credit_Balance_List_Createdate
            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "JobGroup_List"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@CateID", SqlDbType.VarChar).Value = CateID
            'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
            'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
            'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
            conn.Close()
            Return result

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function JobDescription_List(GroupID As Integer) As DataTable

        Try
            Dim result As DataTable
            'Credit_Balance_List_Createdate
            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "JobDescription_List"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = GroupID
            'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
            'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
            'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
            conn.Close()
            Return result

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function JobItem_List(catecode As String, groupcode As String) As DataTable

        Try
            Dim result As DataTable
            'Credit_Balance_List_Createdate
            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "JobItem_List"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@catecode", SqlDbType.VarChar).Value = catecode
            cmd.Parameters.Add("@groupcode", SqlDbType.VarChar).Value = groupcode
            'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
            'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
            'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
            conn.Close()
            Return result

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function JobType_List_By_Depid(depid As String, Optional mode As String = "all") As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobtype_List_By_DepID"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function SaveSupplierandUserAction(jobcode As String,
                         supplierid As Double, useractionid As Double, policy As Integer, usercode As String) As Boolean
        Dim result As Boolean = True
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        ' Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_SupplierandAction_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@supplierid", SqlDbType.BigInt).Value = supplierid
        cmd.Parameters.Add("@useractionid", SqlDbType.BigInt).Value = useractionid
        cmd.Parameters.Add("@policy", SqlDbType.Int).Value = policy
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            result = False
        End Try
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Public Sub setStatus(jobno As String, statusid As Integer)
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Set_Status"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobno", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid


        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub
    Public Function Save(jobno As String, headtable As DataTable, detailtable As DataTable, username As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        jobno = SaveHead(headtable, username)
        result = jobno
        SaveDetail(jobno, detailtable, username)


        Return result
    End Function
    Private Function SaveHead(mytable As DataTable, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Save"
        cmd.CommandType = CommandType.StoredProcedure
        With mytable.Rows(0)
            cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = .Item("jobno")
            cmd.Parameters.Add("@jobdate", SqlDbType.DateTime).Value = DateTime.Parse(.Item("jobdate"))
            cmd.Parameters.Add("@jobowner", SqlDbType.Int).Value = .Item("jobowner")
            cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = .Item("branchid")
            cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = .Item("depid")
            cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = .Item("secid")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
        End With
        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function
    Private Sub SaveDetail(jobno As String, mytable As DataTable, username As String)
        Dim jobdetailid As Integer
        Dim cnt As Integer = mytable.Rows.Count - 1
        With mytable
            For i = 0 To cnt
                If .Rows(i).Item("jobdetailid") = 0 Then
                    jobdetailid = SaveDetailToTable(jobno,
                                      .Rows(i).Item("jobtypeid"),
                                      .Rows(i).Item("assetid"),
                                      .Rows(i).Item("assetcode"),
                                      .Rows(i).Item("brand"),
                                      .Rows(i).Item("model"),
                                      .Rows(i).Item("quantity"),
                                    .Rows(i).Item("unitid"),
                                    .Rows(i).Item("cost"),
                                    .Rows(i).Item("supplierid"),
                                    .Rows(i).Item("policyid"),
                                    If(String.IsNullOrEmpty(.Rows(i).Item("requestdate")), DBNull.Value, .Rows(i).Item("requestdate")),
                                    .Rows(i).Item("details"),
                                    username)
                    If Not .Rows(i).Item("attatch") Is Nothing Then
                        SaveDetail_Attatch(jobdetailid, .Rows(i).Item("attatch"))
                    End If
                    If Not String.IsNullOrEmpty(.Rows(i).Item("nozzle")) Then
                        Dim arrayNozzle As String() = .Rows(i).Item("nozzle").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        For Each code As String In arrayNozzle
                            Try
                                SaveDetail_Nozzle(jobdetailid, code)
                            Catch ex As Exception
                                Dim scriptKey As String = "alert"
                                'Dim javaScript As String = "alert('" & ex.Message & "');"
                                Dim javaScript As String = "alertWarning('save fail');"
                            End Try
                        Next
                    End If
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub
    Private Function SaveDetailToTable(jobno As String, jobtypeid As Integer, assetid As Object, assetcode As String,
                                       brand As String, model As String, quantity As Integer,
                                  unitid As Integer, cost As Double, supplierid As Double, policyid As Double,
                                  duedate As Object, details As String, updateby As String) As Integer
        Dim result As Integer
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "JobsDetails_save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobtypeid", SqlDbType.Int).Value = jobtypeid
        cmd.Parameters.Add("@assetid", SqlDbType.Int).Value = assetid
        cmd.Parameters.Add("@assetcode", SqlDbType.VarChar).Value = assetcode
        cmd.Parameters.Add("@brand", SqlDbType.VarChar).Value = brand
        cmd.Parameters.Add("@model", SqlDbType.VarChar).Value = model
        cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity
        cmd.Parameters.Add("@unitid", SqlDbType.Int).Value = unitid
        cmd.Parameters.Add("@cost", SqlDbType.Money).Value = cost
        cmd.Parameters.Add("@supplierid", SqlDbType.BigInt).Value = supplierid
        cmd.Parameters.Add("@policyid", SqlDbType.Int).Value = policyid
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = duedate
        cmd.Parameters.Add("@details", SqlDbType.VarChar).Value = details
        'cmd.Parameters.Add("@vendor_code", SqlDbType.VarChar).Value = vendor_code
        cmd.Parameters.Add("@updateby", SqlDbType.VarChar).Value = updateby


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("JobDetailID")
        conn.Close()
        Return result

    End Function

    Private Sub SaveDetail_Attatch(jobdetailid As Integer, filename As String)
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "JobsDetails_Attatch_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobdetailid", SqlDbType.Int).Value = jobdetailid
        cmd.Parameters.Add("@fileName", SqlDbType.VarChar).Value = filename


        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub
    Public Sub SaveDetail_Nozzle(jobdetailid As Integer, nozzle As String)
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Nozzle_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobdetailid", SqlDbType.Int).Value = jobdetailid
        cmd.Parameters.Add("@codelist", SqlDbType.VarChar).Value = nozzle


        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub
    Public Function EmailList(jobcode As String, jobtypeid As Integer) As String
        Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Email_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@jobtypeid", SqlDbType.Int).Value = jobtypeid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("email")
        conn.Close()
        Return result
    End Function
    Public Function Cancel(jobcode As String, usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        Return result
    End Function
    Public Function Cancel(jobcode As String, msg As String, usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@msg", SqlDbType.VarChar).Value = msg

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        Return result
    End Function
    Public Function Confirm(jobcode As String, usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Confirm"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        Return result
    End Function
    Public Function Item_Save(jobcode As String, jobtypeid As Double, supplierid As Double,
                         assetcode As String, quantity As Double, unitid As Double, cost As Double,
                         username As String, details As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "JobsItem_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@updateby", SqlDbType.VarChar).Value = username
        cmd.Parameters.Add("@assetcode", SqlDbType.VarChar).Value = assetcode
        cmd.Parameters.Add("@jobtypeid", SqlDbType.Int).Value = jobtypeid
        cmd.Parameters.Add("@supplierid", SqlDbType.BigInt).Value = supplierid
        cmd.Parameters.Add("@details", SqlDbType.VarChar).Value = details
        cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity
        cmd.Parameters.Add("@unitid", SqlDbType.Int).Value = unitid
        cmd.Parameters.Add("@cost", SqlDbType.Money).Value = cost
        'cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = 1

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)

        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        Return result
    End Function
    Public Function Followup_Save(jobcode As String, jobdetailid As Double, statusid As Integer, detail As String,
                                  usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@details", SqlDbType.VarChar).Value = details
        'cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity
        'cmd.Parameters.Add("@unitid", SqlDbType.Int).Value = unitid
        'cmd.Parameters.Add("@cost", SqlDbType.Money).Value = cost
        'cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = 1

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)

        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        Return result
    End Function

    Public Function Cost_Save(jobscenterdtlid As Integer, jobdetailid As Integer, price As Double, unit As Integer, usercode As String) As Boolean
        Dim result As Boolean
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Cost_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobscenterdtlid", SqlDbType.Int).Value = jobscenterdtlid
        cmd.Parameters.Add("@jobdetailid", SqlDbType.Int).Value = jobdetailid
        cmd.Parameters.Add("@price", SqlDbType.Money).Value = price
        cmd.Parameters.Add("@unit", SqlDbType.Int).Value = unit
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function Cost_Save(jobscenterdtlid As Integer, jobdetailid As Integer, price As Double, unit As Integer,
                              vat As Integer, tax As Integer, invoice As String, invoicedate As String, nobill As Boolean, incompletebill As Boolean,
                              bu As Integer, pp As Integer, pj As Integer, usercode As String, costid As Integer) As Boolean
        Dim result As Boolean
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Cost_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@costid", SqlDbType.Int).Value = costid
        cmd.Parameters.Add("@jobscenterdtlid", SqlDbType.Int).Value = jobscenterdtlid
        cmd.Parameters.Add("@jobdetailid", SqlDbType.Int).Value = jobdetailid
        cmd.Parameters.Add("@price", SqlDbType.Money).Value = price
        cmd.Parameters.Add("@unit", SqlDbType.Int).Value = unit
        cmd.Parameters.Add("@bu", SqlDbType.Int).Value = bu
        cmd.Parameters.Add("@pp", SqlDbType.Int).Value = pp
        cmd.Parameters.Add("@pj", SqlDbType.Int).Value = pj
        cmd.Parameters.Add("@vat_per", SqlDbType.Money).Value = vat
        cmd.Parameters.Add("@tax_per", SqlDbType.Money).Value = tax
        cmd.Parameters.Add("@invoice", SqlDbType.VarChar).Value = invoice
        cmd.Parameters.Add("@invoicedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(invoicedate), DBNull.Value, DateTime.Parse(invoicedate))
        cmd.Parameters.Add("@nobill", SqlDbType.Bit).Value = nobill
        cmd.Parameters.Add("@incompletebill", SqlDbType.Bit).Value = incompletebill
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function
    Public Function detail_Del(jobno As String, jobdetailid As Double, username As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "JobsDetails_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobno", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.Int).Value = jobdetailid
        cmd.Parameters.Add("@updateby", SqlDbType.VarChar).Value = username


        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)

        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        Return result
    End Function
    Public Function Find(usercode As String, jobcode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function FindFollowup(jobcode As String, jobdetailid As Double, usercode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Find_Follow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@jobdetailid", SqlDbType.VarChar).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function FindClose(jobcode As String, jobdetailid As Double, usercode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Find_Close"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@jobdetailid", SqlDbType.VarChar).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function FindCost(jobclosecostid As String, jobcloseid As String, jobscenterdtlid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        'cmd.CommandText = "Jobs_Find_Cost"
        'ยังไม่ได้ทำ Jobs_Find_Cost
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobclosecostid", SqlDbType.VarChar).Value = jobclosecostid
        cmd.Parameters.Add("@jobcloseid", SqlDbType.VarChar).Value = jobcloseid
        cmd.Parameters.Add("@jobscenterdtlid", SqlDbType.VarChar).Value = jobscenterdtlid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function FindJobCenterDtl(jobscenterdtlid As String, groupcodedtl As String, jobscenterid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        'cmd.CommandText = "Jobs_Find_CenterDtl"
        'ยังไม่ได้ทำ Jobs_Find_CenterDtl
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobscenterdtlid", SqlDbType.VarChar).Value = jobscenterdtlid
        cmd.Parameters.Add("@groupcodedtl", SqlDbType.VarChar).Value = groupcodedtl
        cmd.Parameters.Add("@jobscenterid", SqlDbType.VarChar).Value = jobscenterid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function FindGroup(groupid As String, groupdescription As String, groupnumber As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Find_Group"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@groupid", SqlDbType.VarChar).Value = groupid
        cmd.Parameters.Add("@groupdescription", SqlDbType.VarChar).Value = groupdescription
        cmd.Parameters.Add("@groupnumber", SqlDbType.VarChar).Value = groupnumber

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function AddGroup(groupid As String, groupdescription As String, groupnumber As String, usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Add_Group"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@groupid", SqlDbType.VarChar).Value = groupid
        cmd.Parameters.Add("@groupdescription", SqlDbType.VarChar).Value = groupdescription
        cmd.Parameters.Add("@groupnumber", SqlDbType.VarChar).Value = groupnumber
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function updateGroup(groupid As String, groupdescription As String, groupnumber As String, usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Update_Group"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@groupid", SqlDbType.VarChar).Value = groupid
        cmd.Parameters.Add("@groupdescription", SqlDbType.VarChar).Value = groupdescription
        cmd.Parameters.Add("@groupnumber", SqlDbType.VarChar).Value = groupnumber
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode


        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function deleteGroupByGroupId(groupid As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Delete_Group"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@groupid", SqlDbType.VarChar).Value = groupid

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function deleteCostById(jobcostid As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Delete_Cost"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcostid", SqlDbType.VarChar).Value = jobcostid

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function setDueDateByPolicyID(policyid As String, Optional reqdate As String = Nothing) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "DateAdd_By_PolicyID"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@policyid", SqlDbType.VarChar).Value = policyid
        cmd.Parameters.Add("@reqdate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(reqdate), DBNull.Value, Date.Parse(reqdate))


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function GetAttatchName() As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Get_AttatchName"
        cmd.CommandType = CommandType.StoredProcedure

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("attatchname")
        conn.Close()
        Return result
    End Function

    Public Function setNonPODtl_by_coderef(from As String, coderef As String, coderefdtl As String, user As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "set_NonPODtl_by_coderef"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@from", SqlDbType.VarChar).Value = from
        cmd.Parameters.Add("@coderef", SqlDbType.VarChar).Value = coderef
        cmd.Parameters.Add("@coderefdtl", SqlDbType.VarChar).Value = coderefdtl
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function SaveRate(topic_id As String, value As String, type As String, jobsdetailid As Integer, user As String) As Integer
        Dim result As Integer
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Rate_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@topic_id", SqlDbType.VarChar).Value = topic_id
        cmd.Parameters.Add("@value", SqlDbType.VarChar).Value = value
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type
        cmd.Parameters.Add("@jobsdetailid", SqlDbType.Int).Value = jobsdetailid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("id")
        conn.Close()
        Return result

    End Function

    Public Function UpdateDetail(jobno As String, jobdetailid As Double, jobTypeid As Integer, assCode As String,
                                          supplierid As Integer, cost As Double, usercode As String, policyid As Double,
                                  duedate As Object, Optional closeTypeid As Integer = 1, Optional categoryid As Integer = 1) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_update_detail_for_operator"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@jobTypeid", SqlDbType.BigInt).Value = jobTypeid
        cmd.Parameters.Add("@assetcode", SqlDbType.VarChar).Value = assCode
        cmd.Parameters.Add("@supplierid", SqlDbType.BigInt).Value = supplierid
        cmd.Parameters.Add("@cost", SqlDbType.Money).Value = cost
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@policyid", SqlDbType.Int).Value = policyid
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(duedate), DBNull.Value, duedate)
        cmd.Parameters.Add("@closeTypeid", SqlDbType.BigInt).Value = closeTypeid
        cmd.Parameters.Add("@categoryid", SqlDbType.BigInt).Value = categoryid

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function UpdateCloseTypeCategory(jobno As String, jobdetailid As Double, closeTypeid As Integer,
                                          categoryid As Integer, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_update_ClosetypeCategory_for_operator"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@closeTypeid", SqlDbType.BigInt).Value = closeTypeid
        cmd.Parameters.Add("@categoryid", SqlDbType.BigInt).Value = categoryid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Sub Jobs_Send_Supplier(jobno As String, jobdetailid As Double, usercode As String)
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Supplier_Send"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.Int).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode


        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub
    Public Function UpdateDetailCateCode(jobno As String, jobdetailid As Double, CateCode As String, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Followup_update_CateCode_for_operator"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@catecode", SqlDbType.VarChar).Value = CateCode
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function


    Public Function JobItem_Save(jobdetailid As Double, listitemcode As String, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "JobItem_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@listitemcode", SqlDbType.VarChar).Value = listitemcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function Jobs_CostCommited_list(supplierid As Integer, visibleall As Boolean, jobcode As String, branchid As String) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_CostCommited_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@supplierid", SqlDbType.VarChar).Value = supplierid
        cmd.Parameters.Add("@visibleall", SqlDbType.Bit).Value = visibleall
        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        'cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function Jobs_Get_RunningNO_JTN() As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Get_RunningNO_JTN"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = .Item("jobno")
        'cmd.Parameters.Add("@jobdate", SqlDbType.DateTime).Value = DateTime.Parse(.Item("jobdate"))
        '    cmd.Parameters.Add("@jobowner", SqlDbType.Int).Value = .Item("jobowner")
        '    cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = .Item("branchid")
        '    cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = .Item("depid")
        '    cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = .Item("secid")
        'cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Public Function Jobs_MapToNonPO_Save(jtncode As String, jobdetailid As Integer, usercode As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_MapToNonPO_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jtncode", SqlDbType.VarChar).Value = jtncode
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Public Function UpdateUnlockCost(jobno As String, jobdetailid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Unlock_Cost"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function Jobs_DisAccept(jobno As String, dtlid As String, message As String, user As String)
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_DisAccept"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@dtlid", SqlDbType.VarChar).Value = dtlid
        cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = message
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function

    Public Function Jobs_Supplier_Cancel(jobno As String, jobdetailid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Supplier_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function


    Public Function Jobs_Supplier_BackStep(jobno As String, jobdetailid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Supplier_BackStep"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function Jobs_Supplier_NextStep(jobno As String, jobdetailid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Supplier_NextStep"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function Jobs_Supplier_addDetails(jobno As String, jobdetailid As Double, begindate As String, enddate As String, detail As String, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Supplier_addDetails"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@begindate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(begindate), DBNull.Value, Date.Parse(begindate))
        cmd.Parameters.Add("@enddate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(enddate), DBNull.Value, Date.Parse(enddate))
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function Jobs_Supplier_FinishStep(jobno As String, jobdetailid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Supplier_FinishStep"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function Costvisible(jobno As String, jobdetailid As Double, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Visible_Cost"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobno
        cmd.Parameters.Add("@jobdetailid", SqlDbType.BigInt).Value = jobdetailid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function

    Public Function UpdateDetailnozzle(jobdtlid As Integer, nozzleid As Integer, brand As String, producttype As String, nozzle_no As String,
                                          positiononassest As String, expirydate As String, url As String, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Nozzle_Update"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobdtlid", SqlDbType.Int).Value = jobdtlid
        cmd.Parameters.Add("@nozzleid", SqlDbType.Int).Value = nozzleid
        cmd.Parameters.Add("@nozzle_no", SqlDbType.VarChar).Value = nozzle_no
        cmd.Parameters.Add("@brand", SqlDbType.VarChar).Value = brand
        cmd.Parameters.Add("@producttype", SqlDbType.VarChar).Value = producttype
        cmd.Parameters.Add("@positiononassest", SqlDbType.VarChar).Value = positiononassest
        cmd.Parameters.Add("@expirydate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(expirydate), DBNull.Value, DateTime.Parse(expirydate))
        cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = url
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function DelDetail_Nozzle(jobdtlid As Integer, nozzleid As Integer, usercode As String) As Boolean
        Dim result As Boolean

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Jobs_Nozzle_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobdtlid", SqlDbType.Int).Value = jobdtlid
        cmd.Parameters.Add("@nozzleid", SqlDbType.Int).Value = nozzleid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
End Class
