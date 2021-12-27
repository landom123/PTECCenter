Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class AgreeClient
    Public Function Client_Save_Info(clientno As String, usercode As String,
                                     clientname As String, cardid As String, sex As String,
                                     companyname As String, birthday As DateTime, mobile As String,
                                     tel As String, email As String, line As String,
                                     address As String, subdistrict As String, district As String,
                                     province As String, postcode As String) As String
        Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Save_Info"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@clientno", SqlDbType.VarChar).Value = clientno
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = clientname
        cmd.Parameters.Add("@cardid", SqlDbType.VarChar).Value = cardid
        cmd.Parameters.Add("@sex", SqlDbType.VarChar).Value = sex
        cmd.Parameters.Add("@companyname", SqlDbType.VarChar).Value = companyname
        cmd.Parameters.Add("@birthday", SqlDbType.DateTime).Value = birthday
        cmd.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile
        cmd.Parameters.Add("@tel", SqlDbType.VarChar).Value = tel
        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email
        cmd.Parameters.Add("@line", SqlDbType.VarChar).Value = line
        cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = address
        cmd.Parameters.Add("@subdistrict", SqlDbType.VarChar).Value = subdistrict
        cmd.Parameters.Add("@district", SqlDbType.VarChar).Value = district
        cmd.Parameters.Add("@province", SqlDbType.VarChar).Value = province
        cmd.Parameters.Add("@postcode", SqlDbType.VarChar).Value = postcode

        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("clientno")
        conn.Close()

        Return result
    End Function
    Public Function Client_Info(clientno As String) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Info"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@clientno", SqlDbType.VarChar).Value = clientno

        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Client_List(clientno As String, clientname As String) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@clientno", SqlDbType.VarChar).Value = clientno
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = clientname
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Client_Get_Info(clientid As Double) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Get_Info"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@clientid", SqlDbType.VarChar).Value = clientid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Client_Cbo_List(agreeid As Double) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Cbo_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.VarChar).Value = agreeid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
End Class
Public Class Payment

    Public Function PaymentType_Cbo_List() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "PaymentType_Cbo_List"
        cmd.CommandType = CommandType.StoredProcedure

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
Public Class TitleName

    Public Function List() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "TitleName_List"
        cmd.CommandType = CommandType.StoredProcedure

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

Public Class AgAssets
    Public Function AssetsType_Cbo() As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Assets_Type"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        'cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
End Class
Public Class Agree
    Public Function FindProject(projectno As String, agreeno As String) As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@projectno", SqlDbType.VarChar).Value = projectno
        cmd.Parameters.Add("@agreeno", SqlDbType.VarChar).Value = agreeno

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds
        conn.Close()

        Return result
    End Function
    Public Function saveAgree(projectno As String, projectdate As DateTime, branch As String, agreetype As String,
                              agreeno As String, agreedate As DateTime, agreeactivedate As DateTime,
                              usercode As String) As String
        Dim result As String
        'Ag_AgreeSave
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_AgreeSave"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@projectno", SqlDbType.VarChar).Value = projectno
        cmd.Parameters.Add("@projectdate", SqlDbType.DateTime).Value = projectdate
        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@agreetype", SqlDbType.VarChar).Value = agreetype
        cmd.Parameters.Add("@agreeno", SqlDbType.VarChar).Value = agreeno
        cmd.Parameters.Add("@agreedate", SqlDbType.DateTime).Value = agreedate
        cmd.Parameters.Add("@agreeactivedate", SqlDbType.DateTime).Value = agreeactivedate
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0).Rows(0).Item("projectno")
        conn.Close()

        Return result
    End Function
    Public Sub saveClient(projectno As String, client As DataTable, usercode As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_AgreeSaveClient"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@projectno", SqlDbType.VarChar)
        cmd.Parameters.Add("@no", SqlDbType.TinyInt)
        cmd.Parameters.Add("@clientno", SqlDbType.VarChar)
        cmd.Parameters.Add("@user", SqlDbType.VarChar)

        For Each row As DataRow In client.Rows
            cmd.Parameters("@projectno").Value = projectno
            cmd.Parameters("@no").Value = row.Item("no")
            cmd.Parameters("@clientno").Value = row.Item("clientno")
            cmd.Parameters("@user").Value = usercode
            adp.SelectCommand = cmd
            'adp.Fill(ds)
            cmd.ExecuteNonQuery()
        Next row



        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        'result = ds.Tables(0).Rows(0).Item("projectno")
        conn.Close()

    End Sub
    Public Sub removeClient(projectno As String, clientno As String, usercode As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_AgreeRemoveClient"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@projectno", SqlDbType.VarChar)
        cmd.Parameters.Add("@clientno", SqlDbType.VarChar)
        cmd.Parameters.Add("@user", SqlDbType.VarChar)


        cmd.Parameters("@projectno").Value = projectno

        cmd.Parameters("@clientno").Value = clientno
        cmd.Parameters("@user").Value = usercode
        adp.SelectCommand = cmd
        'adp.Fill(ds)
        cmd.ExecuteNonQuery()




        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        'result = ds.Tables(0).Rows(0).Item("projectno")
        conn.Close()

    End Sub
    Public Function saveAssets() As String
        Dim result As String

        Return result
    End Function
    Public Function saveOnetime() As String
        Dim result As String

        Return result
    End Function
    Public Function saveRecurringFix() As String
        Dim result As String

        Return result
    End Function
    Public Function saveRecurringFlexible() As String
        Dim result As String

        Return result
    End Function
    Public Function saveFinance() As String
        Dim result As String

        Return result
    End Function
    Public Function saveOther() As String
        Dim result As String

        Return result
    End Function
    Public Function Ag_Cbo_Type() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Cbo_Type"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        'cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    Public Function Ag_Payment_Table_Calcuate(agreeid As Double, volume As Integer) As DataSet
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Payment_Table_Calcuate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    '
    Public Function Ag_Monthly_Payment_Calcuate(agreeid As Double, monthly As String) As DataSet
        'แสดงข้อมูลตารางค่าใช้จ่ายที่ต้องจ่ายในเดือน
        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Monthly_Payment_Calcuate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    Public Function Ag_Payment_Condition(agreeid As Double) As DataSet
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Payment_Condition"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function


    Public Function Ag_Status_list() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_acs").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Status_list"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@agid", SqlDbType.BigInt).Value = agreeid
        'cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
End Class
