Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Approval
    Public Sub SetCboApproval(obj As Object)
        obj.DataSource = Me.Approval_List()
        obj.DataValueField = "ApprovalListID"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboApprovalCategory(obj As Object)
        obj.DataSource = Me.ApprovalCategory_List()
        obj.DataValueField = "ApprovalCategoryID"
        obj.DataTextField = "CategoryName"
        obj.DataBind()
    End Sub
    Public Sub SetCboApprovalByCategoryID(obj As Object, categoryid As Integer)
        obj.DataSource = Me.Approval_List_By_Category(categoryid)
        obj.DataValueField = "ApprovalListID"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboApprovalStatus(obj As Object)
        obj.DataSource = Me.ApprovalStatus_List()
        obj.DataValueField = "statusid"
        obj.DataTextField = "statusname"
        obj.DataBind()
    End Sub
    Public Function Approval_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_List"
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
    Public Function ApprovalCategory_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ApprovalCategory_List"
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
    Public Function Approval_List_By_Category(categoryid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_List_By_Category"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = categoryid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function ApprovalStatus_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ApprovalStatus_List"
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
    Public Function Approval_Save(approvallistid As Integer, name As String, detail As String, price As Double,
                                   day As Integer, branchid As Integer, username As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvallistid", SqlDbType.Int).Value = approvallistid
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@price", SqlDbType.Money).Value = price
        cmd.Parameters.Add("@approvalday", SqlDbType.Int).Value = day
        cmd.Parameters.Add("@branchid", SqlDbType.Int).Value = branchid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Approval_Save_Comment(approvalcode As String, message As String, userid As Integer)
        'Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Save_Comment"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = message
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function
    Public Function Approval_Edit(approvalcode As String, approvallistid As Integer, name As String, detail As String, price As Double,
                                   branchid As Integer, day As Integer, userid As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Edit"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@approvallistid", SqlDbType.Int).Value = approvallistid
        cmd.Parameters.Add("@name_request", SqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@approvaldetail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@price", SqlDbType.Money).Value = price
        cmd.Parameters.Add("@branchid", SqlDbType.Int).Value = branchid
        cmd.Parameters.Add("@day", SqlDbType.Int).Value = day
        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid


        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function Approval_Close(approvalcode As String, approvalclosemessage As String, closedate As Object, username As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Close"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@approvalclosemessage", SqlDbType.VarChar).Value = approvalclosemessage
        cmd.Parameters.Add("@closedate", SqlDbType.DateTime).Value = closedate
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function Approval_Cancel(approvalcode As String, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Public Function Approval_Allow(approvalcode As String, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Allow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Public Function Approval_NotAllow(approvalcode As String, message As String, username As String)
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_NotAllow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = message
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function

    Public Function Approval_Find(approvalcode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function FindApprovalMenuList(approvalcode As String, branchid As String, categoryid As String, approvallistid As String, statusid As String,
                                         areaid As String, userid As String, startdate As String, enddate As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "rpt_ApprovalMenuList_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@categoryid", SqlDbType.VarChar).Value = categoryid
        cmd.Parameters.Add("@approvallistid", SqlDbType.VarChar).Value = approvallistid
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@areaid", SqlDbType.VarChar).Value = areaid
        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function ApprovalMenuList(userid As Integer, working As Integer) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ApprovalMenuList_Find"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@working", SqlDbType.Int).Value = working


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        'cmd.ExecuteNonQuery()

        conn.Close()

        Return result
    End Function
    Public Function GetImageName() As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Get_ImageName"
        cmd.CommandType = CommandType.StoredProcedure

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("imgname")
        conn.Close()
        Return result
    End Function

    Public Function Image_Save(filename As String, imagetype As String, approvalid As Integer, username As String)
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Image_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename
        cmd.Parameters.Add("@imagetype", SqlDbType.VarChar).Value = imagetype
        cmd.Parameters.Add("@approvalid", SqlDbType.BigInt).Value = approvalid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype
        cmd.ExecuteNonQuery()

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("email")
        conn.Close()

    End Function
    Public Function Confirm(approvalcode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Confirm"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Function

    Public Function ApprovalPermission(approvalcode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_PermisstionOwner"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function Approval_Support_Allow(approvalcode As String, username As String)
        'Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Support_Allow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function

    Public Function Approval_Support_Completed(approvalcode As String, username As String)
        'Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Support_Completed"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function

    Public Function Approval_Support_Foraward(approvalcode As String, username As String)
        'Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Support_Foraward"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@approvalcode", SqlDbType.VarChar).Value = approvalcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function

End Class
