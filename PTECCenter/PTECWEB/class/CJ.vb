Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration
Imports System.Xml


Public Class CJ

    Public Function InsertCAT(branch As String, id As Double, name As String,
                              desc As String, usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_cj").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Insert_CAT"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 0

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc
        cmd.Parameters.Add("@createby", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@duedate", SqlDbType.VarChar).Value = DueDate


        'cmd.ExecuteNonQuery()
        Try
            'adp.SelectCommand = cmd
            'adp.Fill(ds)
            'dt = ds.Tables(0)
            'result = dt.Rows(0).Item("price")
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function

    Public Function InsertGNDITEM(entryid As Double, branch As String, check As Double, item As Double,
                                  parent As Double, category As Double, mode As Integer,
                                  hour As Integer, minute As Integer, price As Double,
                                  dob As DateTime, sysdate As DateTime, quantity As Integer,
                                  discpric As Double, usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_cj").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Insert_GNDITEM"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@entryid", SqlDbType.BigInt).Value = entryid
        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@check", SqlDbType.BigInt).Value = check
        cmd.Parameters.Add("@item", SqlDbType.BigInt).Value = item
        cmd.Parameters.Add("@parent", SqlDbType.BigInt).Value = parent
        cmd.Parameters.Add("@category", SqlDbType.BigInt).Value = category
        cmd.Parameters.Add("@mode", SqlDbType.Int).Value = mode
        cmd.Parameters.Add("@hour", SqlDbType.TinyInt).Value = hour
        cmd.Parameters.Add("@minute", SqlDbType.TinyInt).Value = minute
        cmd.Parameters.Add("@price", SqlDbType.Float).Value = price
        cmd.Parameters.Add("@dob", SqlDbType.DateTime).Value = dob
        cmd.Parameters.Add("@sysdate", SqlDbType.DateTime).Value = sysdate
        cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity
        cmd.Parameters.Add("@discpric", SqlDbType.Float).Value = discpric
        cmd.Parameters.Add("@createby", SqlDbType.VarChar).Value = usercode


        'cmd.ExecuteNonQuery()
        Try
            'adp.SelectCommand = cmd
            'adp.Fill(ds)
            'dt = ds.Tables(0)
            'result = dt.Rows(0).Item("price")
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function

    Public Function InsertGNDTndr(branch As String, check As Double, datadate As DateTime, sysdate As DateTime,
                                  type As Integer, typeid As Integer, amount As Double, id As Double, usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_cj").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Insert_GNDTndr"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@check", SqlDbType.BigInt).Value = check
        cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = datadate
        cmd.Parameters.Add("@sysdate", SqlDbType.DateTime).Value = sysdate
        cmd.Parameters.Add("@type", SqlDbType.Int).Value = type
        cmd.Parameters.Add("@typeid", SqlDbType.Int).Value = typeid
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = amount
        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmd.Parameters.Add("@createby", SqlDbType.VarChar).Value = usercode


        'cmd.ExecuteNonQuery()
        Try
            'adp.SelectCommand = cmd
            'adp.Fill(ds)
            'dt = ds.Tables(0)
            'result = dt.Rows(0).Item("price")
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function

    Public Function InsertITM(branch As String, id As Double, shortname As String,
                              chitname As String, longname As String, price As Double,
                              usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_cj").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Insert_ITM"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 0

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmd.Parameters.Add("@shortname", SqlDbType.VarChar).Value = shortname
        cmd.Parameters.Add("@chitname", SqlDbType.VarChar).Value = chitname
        cmd.Parameters.Add("@longname", SqlDbType.VarChar).Value = longname
        cmd.Parameters.Add("@price", SqlDbType.Float).Value = price
        cmd.Parameters.Add("@createby", SqlDbType.VarChar).Value = usercode

        'cmd.ExecuteNonQuery()
        Try
            'adp.SelectCommand = cmd
            'adp.Fill(ds)
            'dt = ds.Tables(0)
            'result = dt.Rows(0).Item("price")
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function

    Public Function InsertTDR(branch As String, id As Double, name As String, cash As String, usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_cj").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Insert_TDR"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 0

        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@cash", SqlDbType.VarChar).Value = cash
        cmd.Parameters.Add("@createby", SqlDbType.VarChar).Value = usercode


        'cmd.ExecuteNonQuery()
        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            dt = ds.Tables(0)
            result = dt.Rows(0).Item("price")
        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function
End Class
