Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Policy

    Public Sub setComboPolicy(obj As Object)
        obj.datasource = Me.List("")
        obj.DataValueField = "policyid"
        obj.DataTextField = "policyallname"
        obj.DataBind()
    End Sub

    Public Sub setComboPolicyByJobTypeID(obj As Object, jobtypeid As String)
        obj.datasource = Me.List(jobtypeid)
        obj.DataValueField = "policyid"
        obj.DataTextField = "policyallname"
        obj.DataBind()
    End Sub
    Public Sub setComboPolicyByJobTypeID_VA(obj As Object, jobtypeid As String)
        obj.datasource = Me.List(jobtypeid)
        obj.DataValueField = "policyid"
        obj.DataTextField = "policyallname"
        obj.DataBind()
    End Sub
    Public Function List(jobtypeid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Policy_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@jobtypeid", SqlDbType.VarChar).Value = jobtypeid
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
