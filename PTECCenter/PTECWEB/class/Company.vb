Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Company
    Public Sub SetCboCompany(obj As Object, df As Integer)
        obj.DataSource = Me.Company_List(df)
        obj.DataValueField = "comid"
        obj.DataTextField = "comcode"
        obj.DataBind()

    End Sub
    Public Function Company_List(Optional df As Integer = 0) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Company_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@default", SqlDbType.Int).Value = df
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
