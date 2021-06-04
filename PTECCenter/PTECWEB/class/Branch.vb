Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Branch
    Public Sub SetComboBranch(obj As Object)
        obj.datasource = Me.List("")
        obj.DataValueField = "branchid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetComboBranchByBranchGroupID(obj As Object, branchgroupid As String)
        obj.DataSource = Me.Branch_List_By_BranchGroupID(branchgroupid)
        obj.DataValueField = "branchid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetComboBranchGroup(obj As Object)
        obj.datasource = Me.ListGroup()
        obj.DataValueField = "branchgroupid"
        obj.DataTextField = "branchgroupcode"
        obj.DataBind()
    End Sub
    Public Sub SetComboBranch(obj As Object, usercode As String)
        obj.datasource = Me.List(usercode)
        obj.DataValueField = "branchid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetComboBranchByAreaid(obj As Object, areaid As String, userid As Integer, flag As Integer)
        obj.datasource = Me.ListAllForArea(areaid, userid, flag)
        obj.DataValueField = "branchid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub

    Public Function PermissionRemove(usercode As String, branchid As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Branch_Permission_remove"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function

    Public Function PermissionAdd(usercode As String, branchid As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Branch_Permission_add"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    Public Function List(usercode As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "branch_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function ListGroup() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "BranchGroup_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Branch_List_By_BranchGroupID(branchgroupid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Branch_List_By_BranchGroupID"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function ListAllForSelected(usercode As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Branch_ListAll_ForSelected"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function ListAllForArea(areaid As String, userid As Integer, flag As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Branch_ListAll_ForArea"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@areaid", SqlDbType.VarChar).Value = areaid
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@flag", SqlDbType.Int).Value = flag


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
End Class
