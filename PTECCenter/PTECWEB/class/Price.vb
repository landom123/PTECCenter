Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Price
    Public Function Oil_Day_Price_Branch_Formula_View(branch As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Oil_Day_Price_Branch_Formula_View"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.Int).Value = branch
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Oil_Day_Price_Select(selectdate As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Oil_Day_Price_Select"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = selectdate
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Oil_Day_Price_Branch_Formula_List(selectdate As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Oil_Day_Price_Branch_Formula_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = selectdate
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Oil_Day_Price_Branch_Formula_Save(rowid As Double, branch As Integer, diffbkk As Double, tax As Double,
                                                      b7 As Double, b10 As Double, g91 As Double, g95 As Double,
                                                      e20 As Double, pado As Double, pg95 As Double,
                                                      begindate As DateTime, usercode As String) As Boolean
        'Dim result As Boolean = True
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Oil_Day_Price_Branch_Formula_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@rowid", SqlDbType.Int).Value = rowid
        cmd.Parameters.Add("@branch", SqlDbType.Int).Value = branch
        cmd.Parameters.Add("@diffbkk", SqlDbType.Float).Value = diffbkk
        cmd.Parameters.Add("@tax", SqlDbType.Float).Value = tax
        cmd.Parameters.Add("@b7", SqlDbType.Decimal).Value = b7
        cmd.Parameters.Add("@b10", SqlDbType.Decimal).Value = b10
        cmd.Parameters.Add("@g91", SqlDbType.Decimal).Value = g91
        cmd.Parameters.Add("@g95", SqlDbType.Decimal).Value = g95
        cmd.Parameters.Add("@e20", SqlDbType.Decimal).Value = e20
        cmd.Parameters.Add("@pado", SqlDbType.Decimal).Value = pado
        cmd.Parameters.Add("@pg95", SqlDbType.Decimal).Value = pg95
        cmd.Parameters.Add("@begindate", SqlDbType.DateTime).Value = begindate
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode


        cmd.ExecuteNonQuery()


        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0)
        conn.Close()
        Return True
        'Return result
    End Function
    Public Function Oil_Day_Price_Save(name As String, lastprice As Double, adjust As Double, price As Double,
                                       usercode As String, effectivedate As String, effectivetime As String) As Boolean
        Dim result As Boolean = True
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Oil_Day_Price_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@productname", SqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@lastprice", SqlDbType.VarChar).Value = lastprice
        cmd.Parameters.Add("@movevalue", SqlDbType.VarChar).Value = adjust
        cmd.Parameters.Add("@price", SqlDbType.VarChar).Value = price
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@effectivedate", SqlDbType.VarChar).Value = effectivedate
        cmd.Parameters.Add("@effectivetime", SqlDbType.VarChar).Value = effectivetime

        '       	@productcode varchar(20),
        '@lastprice real,
        '@movevalue real,
        '@price real,
        '@user varchar(20),
        '@effectivedate varchar(8),
        '@effectivetime varchar(8)

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            result = False
        End Try

        conn.Close()

        Return result
    End Function
End Class
