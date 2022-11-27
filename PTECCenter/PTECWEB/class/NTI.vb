Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class NTI

    Public Sub SetCboStatusList(obj As Object)
        obj.DataSource = Me.Status_List()
        obj.DataValueField = "statusid"
        obj.DataTextField = "statusname"
        obj.DataBind()

    End Sub

    Public Sub SetCboOfferType(obj As Object)
        obj.DataSource = Me.NewNTIOfferType_List()
        obj.DataValueField = "Typename"
        obj.DataTextField = "Typename"
        obj.DataBind()
    End Sub
    Public Function NewNTI_Find(nticode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTI_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function


    Public Function NewNTI_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTI_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function NewNTI_Edit(nticode As String, title As String, usercode As String)
        'Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTI_Edit"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode
        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function

    Public Function Status_List(Optional flag As String = "") As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTIStatus_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = flag
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function


    Public Function NewNTIOfferType_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTIOfferType_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = flag
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function Followup_Save(nticode As String, statusid As Integer, usercode As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTIFollowup_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode
        cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
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

    Public Function NewNTIFollowup_Reject(nticode As String, message As String, username As String)
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTIFollowup_Reject"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode
        cmd.Parameters.Add("@msg_reject", SqlDbType.VarChar).Value = message
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function


    Public Function NewNTIPermisstionOperator(Optional nticode As String = "") As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTI_PermisstionOperator"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("acc")
        conn.Close()
        Return result

    End Function


    Public Function NewNTI_Edit_Offer(nticode As String, offer As String, usercode As String)
        'Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NewNTI_Edit_Offer"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nticode", SqlDbType.VarChar).Value = nticode
        cmd.Parameters.Add("@offer", SqlDbType.VarChar).Value = offer
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function
End Class
