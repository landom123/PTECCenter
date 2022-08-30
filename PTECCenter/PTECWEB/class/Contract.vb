Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Client
    Public Function Save(contractno As String, clientno As String, usercode As String,
                                     clientname As String, cardid As String, sex As String,
                                     companyname As String, birthday As DateTime, mobile As String,
                                     tel As String, email As String, line As String,
                                     address As String, subdistrict As String, district As String,
                                     province As String, postcode As String) As String
        Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
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
    Public Function Find(clientno As String) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Client_Find"
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
    Public Function List(clientno As String, clientname As String) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
    Public Function Cbo_List(agreeid As Double) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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

Public Class OneTimePayment
    Public Function Save(contractno As String, onetimeid As Double, paymenttype As String, duedate As DateTime,
                         amount As Double, clientpaid As Integer, remark As String, usercode As String) As Double
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As Double
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "OneTime_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
        cmd.Parameters.Add("@onetimeid", SqlDbType.BigInt).Value = onetimeid
        cmd.Parameters.Add("@paymenttype", SqlDbType.VarChar).Value = paymenttype
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = duedate
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = amount
        cmd.Parameters.Add("@clientpaid", SqlDbType.Int).Value = clientpaid
        cmd.Parameters.Add("@remark", SqlDbType.VarChar).Value = remark
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("onetimeid")
        conn.Close()

        Return result
    End Function

    Public Function Find(onetimeid As Double) As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "OneTime_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@onetimeid", SqlDbType.VarChar).Value = onetimeid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
End Class

Public Class PaymentContract

    Public Function Find(paymentid As Double) As DataTable

        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Payment_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@paymentid", SqlDbType.VarChar).Value = paymentid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Save(contractno As String, paymentid As Double, paidtype As String,
                         bankcode As String, bankbranchcode As String, bankbranchname As String,
                         accountno As String, accountname As String, usercode As String) As Double
        Dim result As Double = 0

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Payment_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
        cmd.Parameters.Add("@paymentid", SqlDbType.BigInt).Value = paymentid
        cmd.Parameters.Add("@paidtype", SqlDbType.VarChar).Value = paidtype
        cmd.Parameters.Add("@bankcode", SqlDbType.VarChar).Value = bankcode
        cmd.Parameters.Add("@bankbranchcode", SqlDbType.VarChar).Value = bankbranchcode
        cmd.Parameters.Add("@bankbranchname", SqlDbType.VarChar).Value = bankbranchname
        cmd.Parameters.Add("@accountno", SqlDbType.VarChar).Value = accountno
        cmd.Parameters.Add("@accountname", SqlDbType.VarChar).Value = accountname
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("paymentid")
        conn.Close()

        Return result
    End Function
End Class

Public Class Fixible

    Public Function Find(fixid As Double) As DataTable

        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Fixible_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@fixid", SqlDbType.VarChar).Value = fixid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Save(contractno As String, fixid As Double, paymenttype As String,
                         recuringtype As String, frequency As Integer, begindate As DateTime,
                         enddate As DateTime, duedate As DateTime, amount As Double, usercode As String) As Double
        Dim result As Double = 0

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Fixible_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
        cmd.Parameters.Add("@fixid", SqlDbType.BigInt).Value = fixid
        cmd.Parameters.Add("@paymenttype", SqlDbType.VarChar).Value = paymenttype
        cmd.Parameters.Add("@recuringtype", SqlDbType.VarChar).Value = recuringtype
        cmd.Parameters.Add("@frequency", SqlDbType.Int).Value = frequency
        cmd.Parameters.Add("@begindate", SqlDbType.DateTime).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.DateTime).Value = enddate
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = duedate
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = amount
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("fixid")
        conn.Close()

        Return result
    End Function
End Class

Public Class Flexible
    Public Function Find(flexid As Double) As DataSet

        Dim result As New DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Flexible_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@flexid", SqlDbType.VarChar).Value = flexid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()

        Return result
    End Function

    Public Function Save(contractno As String, flexid As Double, paymenttype As String,
                         recuringtype As String, frequency As Integer, begindate As DateTime,
                         enddate As DateTime, duedate As DateTime, calcmaxorrank As Integer, usercode As String) As Double
        Dim result As Double = 0

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Flexible_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
        cmd.Parameters.Add("@flexid", SqlDbType.BigInt).Value = flexid
        cmd.Parameters.Add("@paymenttype", SqlDbType.VarChar).Value = paymenttype
        cmd.Parameters.Add("@recuringtype", SqlDbType.VarChar).Value = recuringtype
        cmd.Parameters.Add("@frequency", SqlDbType.Int).Value = frequency
        cmd.Parameters.Add("@begindate", SqlDbType.DateTime).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.DateTime).Value = enddate
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = duedate
        cmd.Parameters.Add("@calcmaxorrank", SqlDbType.Float).Value = calcmaxorrank
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("flexid")
        conn.Close()

        Return result
    End Function

    Public Sub DeleteDetail(flexdetailid As Double)
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "FlexibleDetail_Delete"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@flexdetailid", SqlDbType.BigInt).Value = flexdetailid

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("flexid")
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub
    Public Sub SaveDetail(flexid As Double, flexdetailid As Double, beginvolume As Double, endvolume As Double,
                               warrantyamount As Double, rate As Double, usercode As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "FlexibleDetail_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@flexid", SqlDbType.BigInt).Value = flexid
        cmd.Parameters.Add("@flexdetailid", SqlDbType.BigInt).Value = flexdetailid
        cmd.Parameters.Add("@beginvolume", SqlDbType.Float).Value = beginvolume
        cmd.Parameters.Add("@endvolume", SqlDbType.Float).Value = endvolume
        cmd.Parameters.Add("@warrantyamount", SqlDbType.Float).Value = warrantyamount
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("flexid")
        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub
End Class
Public Class Payment

    Public Function PaymentType_List() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "PaymentType_List"
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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

Public Class ContractAssets

    Public Function Save(contractno As String, assetsno As String, assetstype As Integer,
                         landno As String, surveyno As String, subdistrict As String, district As String,
                         province As String, rai As Integer, ngan As Integer, wa As Double, gps As String,
                          fullarea As Integer, usercode As String) As String
        Dim result As String

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Assets_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
        cmd.Parameters.Add("@assetsno", SqlDbType.VarChar).Value = assetsno
        cmd.Parameters.Add("@assetstype", SqlDbType.Int).Value = assetstype
        cmd.Parameters.Add("@fullarea", SqlDbType.Int).Value = fullarea
        cmd.Parameters.Add("@landno", SqlDbType.VarChar).Value = landno
        cmd.Parameters.Add("@surveyno", SqlDbType.VarChar).Value = surveyno
        cmd.Parameters.Add("@subdistrict", SqlDbType.VarChar).Value = subdistrict
        cmd.Parameters.Add("@district", SqlDbType.VarChar).Value = district
        cmd.Parameters.Add("@province", SqlDbType.VarChar).Value = province
        cmd.Parameters.Add("@rai", SqlDbType.VarChar).Value = rai
        cmd.Parameters.Add("@ngan", SqlDbType.VarChar).Value = ngan
        cmd.Parameters.Add("@wa", SqlDbType.VarChar).Value = wa
        cmd.Parameters.Add("@gps", SqlDbType.VarChar).Value = gps
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@volume", SqlDbType.Int).Value = volume

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("assetsno")
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function
    Public Function Find(assetsno As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Assets_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@assetsno", SqlDbType.VarChar).Value = assetsno
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

    Public Function AssetsType_Cbo() As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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

Public Class Project

    Public Function Find(projectno As String) As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Project_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@projectno", SqlDbType.VarChar).Value = projectno
        'cmd.Parameters.Add("@agreeno", SqlDbType.VarChar).Value = agreeno

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

    Public Function List() As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Project_List"
        cmd.CommandType = CommandType.StoredProcedure


        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Save(projectno As String, branch As String, remark As String, salevolume As Integer, status As String, usercode As String) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Project_Save"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@projectno", SqlDbType.VarChar).Value = projectno
        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@remark", SqlDbType.VarChar).Value = remark
        cmd.Parameters.Add("@salevolume", SqlDbType.VarChar).Value = salevolume
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
End Class
Public Class Contract

    Public Function Find(contractno As String) As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Contract_Find"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@contractno", SqlDbType.VarChar).Value = contractno
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds
        conn.Close()

        Return result
    End Function
    Public Function List() As DataSet
        Dim result As DataSet

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Contract_List"
        cmd.CommandType = CommandType.StoredProcedure


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

    Public Function Save(projectno As String, agreetype As String,
                              agreeno As String, lawcontractno As String, agreedate As DateTime, agreeactivedate As DateTime,
                              usercode As String) As String
        Dim result As String
        'Ag_AgreeSave
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Contract_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@projectno", SqlDbType.VarChar).Value = projectno
        cmd.Parameters.Add("@agreetype", SqlDbType.VarChar).Value = agreetype
        cmd.Parameters.Add("@agreeno", SqlDbType.VarChar).Value = agreeno
        cmd.Parameters.Add("@lawcontractno", SqlDbType.VarChar).Value = lawcontractno
        cmd.Parameters.Add("@agreedate", SqlDbType.DateTime).Value = agreedate
        cmd.Parameters.Add("@agreeactivedate", SqlDbType.DateTime).Value = agreeactivedate
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0).Rows(0).Item("Contractno")
        conn.Close()

        Return result
    End Function
    Public Sub saveClient(projectno As String, client As DataTable, usercode As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
    Public Function ContractTypeCbo() As DataTable
        'แสดงข้อมูลตารางค่าใช้จ่ายทั้งหมดของสัญญา จนจบสัญญา
        Dim result As New DataTable
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
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
