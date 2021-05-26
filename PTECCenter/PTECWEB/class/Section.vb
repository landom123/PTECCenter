Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Section
    Public Function List(depid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "section_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Sub SetCboSection(obj As Object, depid As Integer)
        obj.DataSource = Me.List(depid)
        obj.DataValueField = "secid"
        obj.DataTextField = "name"
        obj.DataBind()


    End Sub
End Class
