Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.Net
Imports System.Reflection
Imports System.Diagnostics.Contracts
Imports System.Web.Http.Results
Imports QRCoder.PayloadGenerator
Imports DocumentFormat.OpenXml.Bibliography
Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Net.NetworkInformation
Imports CrystalDecisions.Web

Public Class Client
    Public Function Save(contractno As String, clientno As String, usercode As String,
                                     clientname As String, cardid As String, sex As String,
                                     companyname As String, birthday As DateTime, mobile As String,
                                     tel As String, email As String, line As String,
                                     address As String, subdistrict As String, district As String,
                                     province As String, postcode As String, address1 As String, subdistrict1 As String, district1 As String,
                                     province1 As String, postcode1 As String) As String
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
        cmd.Parameters.Add("@Address1", SqlDbType.VarChar).Value = address1
        cmd.Parameters.Add("@SubDistrict1", SqlDbType.VarChar).Value = subdistrict1
        cmd.Parameters.Add("@District1", SqlDbType.VarChar).Value = district1
        cmd.Parameters.Add("@Province1", SqlDbType.VarChar).Value = province1
        cmd.Parameters.Add("@PostCode1", SqlDbType.VarChar).Value = postcode1


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
                         accountno As String, accountname As String, usercode As String, sActive As String, sPayCust As String, sTaxCust As String, sACCode As String) As Double
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
        cmd.Parameters.Add("@Active", SqlDbType.VarChar).Value = sActive
        cmd.Parameters.Add("@PayCust", SqlDbType.VarChar).Value = sPayCust
        cmd.Parameters.Add("@TaxCust", SqlDbType.VarChar).Value = sTaxCust
        cmd.Parameters.Add("@ACCode", SqlDbType.VarChar).Value = sACCode

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
                          fullarea As Integer, usercode As String, RentType As Integer, addrerss As String, subdistrict1 As String, district1 As String, province1 As String) As String
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

        cmd.Parameters.Add("@RentType", SqlDbType.Int).Value = RentType
        cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = addrerss
        cmd.Parameters.Add("@subdistrict1", SqlDbType.VarChar).Value = subdistrict1
        cmd.Parameters.Add("@district1", SqlDbType.VarChar).Value = district1
        cmd.Parameters.Add("@province1", SqlDbType.VarChar).Value = province1

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


    Public Sub SetCboloadBranch(obj As Object)
        obj.DataSource = Me.loadBranch()
        obj.DataValueField = "brcode"
        obj.DataTextField = "brname"
        obj.DataBind()

    End Sub
    Public Sub SetCboloadFilterPay(obj As Object)
        obj.DataSource = Me.loadFilterPay()
        obj.DataValueField = "ID"
        obj.DataTextField = "FilterPay"
        obj.DataBind()

    End Sub
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

    Public Function ListFind(Branch As String) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Project_List_find"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = Branch
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function


    Public Function loadBranch() As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_Branch"
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

    Public Function loadFilterPay() As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_Payfind"
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

    Public Function loadStatus(StatusType As Integer) As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_Status"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@StatusType", SqlDbType.Int).Value = StatusType
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
    Public Function MonthlyPayment(monthly As String) As DataTable
        Dim result As DataTable


        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_MonthlyPayment"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function MonthlyPayment2(monthly As String, sBranch As String, iDueDate As Integer, sDueDate As String) As DataTable
        Dim result As DataTable


        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_MonthlyPayment2"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        cmd.Parameters.Add("@sBranch", SqlDbType.VarChar).Value = sBranch
        cmd.Parameters.Add("@iDueDate", SqlDbType.Int).Value = iDueDate
        cmd.Parameters.Add("@sDueDate", SqlDbType.VarChar).Value = sDueDate


        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function MonthlyPaymentFix(sBeginDate As String, sEndDate As String) As DataTable
        Dim result As DataTable


        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Ag_Payment_Fixrate_Table_Calcuate2"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@sBeginDate", SqlDbType.VarChar).Value = sBeginDate
        cmd.Parameters.Add("@sEndDate", SqlDbType.VarChar).Value = sEndDate



        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = cmd.ExecuteNonQuery
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

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
                              agreeno As String, lawcontractno As String, agreedate As DateTime, agreeactivedate As DateTime, usercode As String _
                              , BeginDate As Date, EndDate As Date, ConperiodY As Integer, ConperiodM As Integer, ConperiodD As Integer, ConProvince As String _
                              , Branch As String, DayDueDate As Integer, AreaPlot As Decimal, RentPer As Decimal, ContractLandID As Integer, ContractBuID As Integer, ContractDrID As Integer) As String
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

        cmd.Parameters.Add("@BeginDate", SqlDbType.Date).Value = BeginDate
        cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate
        cmd.Parameters.Add("@ConperiodY", SqlDbType.Int).Value = ConperiodY
        cmd.Parameters.Add("@ConperiodM", SqlDbType.Int).Value = ConperiodM
        cmd.Parameters.Add("@ConperiodD", SqlDbType.Int).Value = ConperiodD
        cmd.Parameters.Add("@ConProvince", SqlDbType.VarChar).Value = ConProvince
        cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = Branch
        cmd.Parameters.Add("@DayDueDate", SqlDbType.Int).Value = DayDueDate
        cmd.Parameters.Add("@AreaPlot", SqlDbType.Decimal).Value = AreaPlot
        cmd.Parameters.Add("@RentPer", SqlDbType.Decimal).Value = RentPer
        cmd.Parameters.Add("@ContractLandID", SqlDbType.Int).Value = ContractLandID
        cmd.Parameters.Add("@ContractBuID", SqlDbType.Int).Value = ContractBuID
        cmd.Parameters.Add("@ContractDrID", SqlDbType.Int).Value = ContractDrID

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
        Dim result As String = ""

        Return result
    End Function
    Public Function saveOnetime() As String
        Dim result As String = ""

        Return result
    End Function
    Public Function saveRecurringFix() As String
        Dim result As String = ""

        Return result
    End Function
    Public Function saveRecurringFlexible() As String
        Dim result As String = ""

        Return result
    End Function
    Public Function saveFinance() As String
        Dim result As String = ""

        Return result
    End Function
    Public Function saveOther() As String
        Dim result As String = ""

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
    Public Function Ag_Payment_Table_Calcuate(agreeid As Double, volume As Integer, agreeno As String) As DataSet
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
        cmd.Parameters.Add("@agno", SqlDbType.VarChar).Value = agreeno

        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function    '
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

    Public Function loadContractLand(iConID As Integer) As DataTable
        Dim result As New DataTable
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_ContractLand"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ConID", SqlDbType.Int).Value = iConID

        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function

    Public Function loadContractBuild(iConID As Integer, iLand As Integer) As DataTable
        Dim result As New DataTable
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_ContractBuild"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ConID", SqlDbType.Int).Value = iConID
        cmd.Parameters.Add("@LanID", SqlDbType.Int).Value = iLand

        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function

    Public Function loadContractDayRent(iConID As Integer, iLand As Integer, iBuID As Integer) As DataTable
        Dim result As New DataTable
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_ContractDayRent"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ConID", SqlDbType.Int).Value = iConID
        cmd.Parameters.Add("@LanID", SqlDbType.Int).Value = iLand
        cmd.Parameters.Add("@BuID", SqlDbType.Int).Value = iBuID

        adp.SelectCommand = cmd
        adp.Fill(result)
        'result = cmd.ExecuteNonQuery
        conn.Close()

        Return result
    End Function

End Class

Public Class clsRequestContract
    Public Function loadBranch() As DataTable
        Dim result As DataTable

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "sp_Get_Branch"
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

    Public Function Addbranch(BrCode As String, BrName As String, Addr As String, SubDistrict As String, District As String, Province As String _
            , PostCode As String, Tel As String, Contract As String, Remark As String, Createby As String) As Boolean

        Try

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Branch_Add"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@BrCode", SqlDbType.VarChar).Value = BrCode
            cmd.Parameters.Add("@BrName", SqlDbType.VarChar).Value = BrName
            cmd.Parameters.Add("@Addr", SqlDbType.VarChar).Value = Addr
            cmd.Parameters.Add("@SubDistrict", SqlDbType.VarChar).Value = SubDistrict
            cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = District
            cmd.Parameters.Add("@Province", SqlDbType.VarChar).Value = Province
            cmd.Parameters.Add("@PostCode", SqlDbType.VarChar).Value = PostCode
            cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Tel
            cmd.Parameters.Add("@Contract", SqlDbType.VarChar).Value = Contract
            cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value = Remark
            cmd.Parameters.Add("@Createby", SqlDbType.VarChar).Value = Createby

            adp.SelectCommand = cmd
            adp.Fill(ds)
            conn.Close()

            Return True
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function
    Public Function AddRequest(DocuNo As String, Branch As String, ContractID As Integer, Begindate As Date, EndDate As Date, CustName As String, CardID As String, Gender As String _
                                , Company As String, Mobile As String, Tel As String, Email As String, Line As String, StatusID As Integer, Address As String, SubDistrict As String _
                                , District As String, Province As String, PostCode As String, CreateDate As DateTime, CreateBy As String, RegistryTo As String, ContractNo As String) As String

        Try
            Dim result As String

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Add_Request"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@DocuNo", SqlDbType.VarChar).Value = DocuNo
            cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = Branch
            cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = ContractID
            cmd.Parameters.Add("@Begindate", SqlDbType.DateTime).Value = Begindate
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate
            cmd.Parameters.Add("@CustName", SqlDbType.VarChar).Value = CustName
            cmd.Parameters.Add("@CardID", SqlDbType.VarChar).Value = CardID
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender
            cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = Company
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = Mobile
            cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Tel
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email
            cmd.Parameters.Add("@Line", SqlDbType.VarChar).Value = Line
            cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = StatusID
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address
            cmd.Parameters.Add("@SubDistrict", SqlDbType.VarChar).Value = SubDistrict
            cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = District
            cmd.Parameters.Add("@Province", SqlDbType.VarChar).Value = Province
            cmd.Parameters.Add("@PostCode", SqlDbType.VarChar).Value = PostCode
            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreateDate
            cmd.Parameters.Add("@CreateBy", SqlDbType.VarChar).Value = CreateBy
            cmd.Parameters.Add("@RegistryTo", SqlDbType.VarChar).Value = RegistryTo
            cmd.Parameters.Add("@ContractNo", SqlDbType.VarChar).Value = ContractNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            'result = ds.Tables(0)
            result = ds.Tables(0).Rows(0).Item("TT_Request")
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try
    End Function

    Public Function AddContractPerson(sDocNo As String, Type As Integer, PersonName As String, IDCard As String, TaxID As String, Gender As String, Addr As String, SubDistrict As String _
                                , District As String, Province As String, PostCode As String, Tel As String, Line As String, Email As String _
                                , CreateBy As String) As Boolean

        Try

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Add_TT_PerContract"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = Type
            cmd.Parameters.Add("@PersonName", SqlDbType.VarChar).Value = PersonName
            cmd.Parameters.Add("@IDCard", SqlDbType.VarChar).Value = IDCard
            cmd.Parameters.Add("@TaxID", SqlDbType.VarChar).Value = TaxID
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender
            cmd.Parameters.Add("@Addr", SqlDbType.VarChar).Value = Addr
            cmd.Parameters.Add("@SubDistrict", SqlDbType.VarChar).Value = SubDistrict
            cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = District
            cmd.Parameters.Add("@Province", SqlDbType.VarChar).Value = Province
            cmd.Parameters.Add("@PostCode", SqlDbType.VarChar).Value = PostCode
            cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Tel
            cmd.Parameters.Add("@Line", SqlDbType.VarChar).Value = Line
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email
            cmd.Parameters.Add("@Createby", SqlDbType.VarChar).Value = CreateBy


            adp.SelectCommand = cmd
            adp.Fill(ds)
            conn.Close()

            Return True
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function
    Public Function UpdateRequest(DocuNo As String, Branch As String, ContractID As Integer, Begindate As Date, EndDate As Date, CustName As String, CardID As String, Gender As String _
                                , Company As String, Mobile As String, Tel As String, Email As String, Line As String, StatusID As Integer, Address As String, SubDistrict As String _
                                , District As String, Province As String, PostCode As String, UpdateDate As DateTime, UpdateBy As String, iDocID As Integer, RegistryTo As String, ContractNo As String) As String

        Try
            Dim result As String

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Update_Request"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@DocuNo", SqlDbType.VarChar).Value = DocuNo
            cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = Branch
            cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = ContractID
            cmd.Parameters.Add("@Begindate", SqlDbType.DateTime).Value = Begindate
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate
            cmd.Parameters.Add("@CustName", SqlDbType.VarChar).Value = CustName
            cmd.Parameters.Add("@CardID", SqlDbType.VarChar).Value = CardID
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender
            cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = Company
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = Mobile
            cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Tel
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email
            cmd.Parameters.Add("@Line", SqlDbType.VarChar).Value = Line
            cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = StatusID
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address
            cmd.Parameters.Add("@SubDistrict", SqlDbType.VarChar).Value = SubDistrict
            cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = District
            cmd.Parameters.Add("@Province", SqlDbType.VarChar).Value = Province
            cmd.Parameters.Add("@PostCode", SqlDbType.VarChar).Value = PostCode
            cmd.Parameters.Add("@UpdateDate", SqlDbType.DateTime).Value = UpdateDate
            cmd.Parameters.Add("@UpdateBy", SqlDbType.VarChar).Value = UpdateBy
            cmd.Parameters.Add("@DocuID", SqlDbType.VarChar).Value = iDocID
            cmd.Parameters.Add("@RegistryTo", SqlDbType.VarChar).Value = RegistryTo
            cmd.Parameters.Add("@ContractNo", SqlDbType.VarChar).Value = ContractNo


            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            'result = ds.Tables(0)
            result = ds.Tables(0).Rows(0).Item("TT_Request")
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try
    End Function

    Public Function UpdateStatusRequest(sStatus As String, iDocID As Integer, sProjectNo As String) As String

        Try
            Dim result As String

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Update_Status_Request"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = sStatus
            cmd.Parameters.Add("@iDocID", SqlDbType.Int).Value = iDocID
            cmd.Parameters.Add("@sProjectNo", SqlDbType.VarChar).Value = sProjectNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            'result = ds.Tables(0)
            result = ds.Tables(0).Rows(0).Item("TT_Request")
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try
    End Function

    Public Function GetDocRun(iDocuType As Integer, iCondition As Integer) As String
        Try
            Dim Result As String = ""

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_get_DocuRun"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@DocType", SqlDbType.Int).Value = iDocuType
            cmd.Parameters.Add("@bBool", SqlDbType.Int).Value = iCondition
            cmd.Parameters.Add("@Format", SqlDbType.VarChar, 20)
            cmd.Parameters("@Format").Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            Result = cmd.Parameters("@Format").Value.ToString()
            conn.Close()

            Return Result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return ""
        End Try

    End Function

    Public Function FindRequest(sBeginDate As String, sEndDate As String, iStatus As Integer) As DataTable

        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_FindData_Request"
            cmd.CommandType = CommandType.StoredProcedure


            cmd.Parameters.Add("@CreateDatefr", SqlDbType.VarChar).Value = sBeginDate
            cmd.Parameters.Add("@CreateDateto", SqlDbType.VarChar).Value = sEndDate
            cmd.Parameters.Add("@iStatus", SqlDbType.Int).Value = iStatus


            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try

    End Function

    Public Function loadRequestProject(ProjectNo As String) As DataTable

        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_RequestByProject"
            cmd.CommandType = CommandType.StoredProcedure


            cmd.Parameters.Add("@ProjectNo", SqlDbType.VarChar).Value = ProjectNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try

    End Function

    Public Function LoadRequest(iDocuID As Integer) As DataTable

        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_GetData_Request"
            cmd.CommandType = CommandType.StoredProcedure


            cmd.Parameters.Add("@DocuID", SqlDbType.VarChar).Value = iDocuID
            'cmd.Parameters.Add("@CreateDateto", SqlDbType.VarChar).Value = sEndDate
            'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try

    End Function

    Public Function loadContractPersaonal(sDocNo As String) As DataTable


        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_ContractPer"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try


    End Function

    Public Function loadRequestContract() As DataTable


        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_Contract_Request"
            cmd.CommandType = CommandType.StoredProcedure


            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try


    End Function

    Public Function AddContractCompany(sDocNo As String, CompanyName As String, AddrCom As String, SubDistrictCom As String, DistrictCom As String, ProvinceCom As String _
                                       , PostCodeCom As String, TelCom As String, CustNamePer As String, CardIDPer As String, TaxIDPer As String, AddrComPer As String _
                                       , SubDistrictComPer As String, DistrictComPer As String, ProvinceComPer As String, PostCodeComPer As String, TelComPer As String _
                                       , LineComPer As String, EmailComPer As String, CreateBy As String) As Boolean

        Try

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Add_TT_ComContract"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo
            cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CompanyName
            cmd.Parameters.Add("@AddrCom", SqlDbType.VarChar).Value = AddrCom
            cmd.Parameters.Add("@SubDistrictCom", SqlDbType.VarChar).Value = SubDistrictCom
            cmd.Parameters.Add("@DistrictCom", SqlDbType.VarChar).Value = DistrictCom
            cmd.Parameters.Add("@ProvinceCom", SqlDbType.VarChar).Value = ProvinceCom
            cmd.Parameters.Add("@PostCodeCom", SqlDbType.VarChar).Value = PostCodeCom
            cmd.Parameters.Add("@TelCom", SqlDbType.VarChar).Value = TelCom
            cmd.Parameters.Add("@CustNamePer", SqlDbType.VarChar).Value = CustNamePer
            cmd.Parameters.Add("@CardIDPer", SqlDbType.VarChar).Value = CardIDPer
            cmd.Parameters.Add("@TaxIDPer", SqlDbType.VarChar).Value = TaxIDPer
            cmd.Parameters.Add("@AddrComPer", SqlDbType.VarChar).Value = AddrComPer
            cmd.Parameters.Add("@SubDistrictComPer", SqlDbType.VarChar).Value = SubDistrictComPer
            cmd.Parameters.Add("@DistrictComPer", SqlDbType.VarChar).Value = DistrictComPer
            cmd.Parameters.Add("@ProvinceComPer", SqlDbType.VarChar).Value = ProvinceComPer
            cmd.Parameters.Add("@PostCodeComPer", SqlDbType.VarChar).Value = PostCodeComPer
            cmd.Parameters.Add("@TelComPer", SqlDbType.VarChar).Value = TelComPer
            cmd.Parameters.Add("@LineComPer", SqlDbType.VarChar).Value = LineComPer
            cmd.Parameters.Add("@EmailComPer", SqlDbType.VarChar).Value = EmailComPer
            cmd.Parameters.Add("@Createby", SqlDbType.VarChar).Value = CreateBy


            adp.SelectCommand = cmd
            adp.Fill(ds)
            conn.Close()

            Return True
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Public Function loadContractCompany(sDocNo As String) As DataTable


        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_Contract_Company"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try


    End Function

    Public Function AddContractFixiblePayment(sDocNo As String, PayType As Integer, DueType As Integer, Frequency As Integer, DueDate As Date, Amount As Decimal _
                                              , BeginDate As Date, EndDate As Date, CreateBy As String) As Boolean

        Try

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Add_TT_FixiblePayment"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo
            cmd.Parameters.Add("@PayType", SqlDbType.Int).Value = PayType
            cmd.Parameters.Add("@DueType", SqlDbType.Int).Value = DueType
            cmd.Parameters.Add("@Frequency", SqlDbType.Int).Value = Frequency
            cmd.Parameters.Add("@DueDate", SqlDbType.Date).Value = DueDate
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount
            cmd.Parameters.Add("@BeginDate", SqlDbType.Date).Value = BeginDate
            cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate
            cmd.Parameters.Add("@Createby", SqlDbType.VarChar).Value = CreateBy


            adp.SelectCommand = cmd
            adp.Fill(ds)
            conn.Close()

            Return True
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Public Function loadContractFixiblePayment(sDocNo As String) As DataTable


        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_FixiblePayment"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try


    End Function

    Public Function AddContractPayment(sDocNo As String, PaymentPay As Integer, BankID As Integer, BranchName As String, AccCode As String, AccName As String _
                                              , PayCust As String, TaxCust As String, ACCode As String, CreateBy As String) As Boolean

        Try

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter


            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Add_TT_Payment"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo
            cmd.Parameters.Add("@PaymentPay", SqlDbType.Int).Value = PaymentPay
            cmd.Parameters.Add("@BankID", SqlDbType.Int).Value = BankID
            cmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = BranchName
            cmd.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = AccCode
            cmd.Parameters.Add("@AccName", SqlDbType.VarChar).Value = AccName
            cmd.Parameters.Add("@PayCust", SqlDbType.VarChar).Value = PayCust
            cmd.Parameters.Add("@TaxCust", SqlDbType.VarChar).Value = TaxCust
            cmd.Parameters.Add("@ACCode", SqlDbType.VarChar).Value = ACCode
            cmd.Parameters.Add("@Createby", SqlDbType.VarChar).Value = CreateBy


            adp.SelectCommand = cmd
            adp.Fill(ds)
            conn.Close()

            Return True
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Public Function loadContractPayment(sDocNo As String) As DataTable


        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_TT_Payment"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo

            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try


    End Function

End Class

Public Class clsPayment
    Public Function loadBank() As DataTable

        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_Bank"
            cmd.CommandType = CommandType.StoredProcedure
            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try

    End Function

    Public Function loadCustContract(iPayID As Integer) As DataTable

        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_Cust_Contract"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@PayID", SqlDbType.Int).Value = iPayID


            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try

    End Function

    Public Function loadCustContract2(sDocNo As String) As DataTable

        Try
            Dim result As DataTable

            Dim ds As New DataSet
            Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_contract").ConnectionString)
            Dim cmd As New SqlCommand
            Dim adp As New SqlDataAdapter

            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "sp_Get_Cust_Contract2"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@sDocNo", SqlDbType.VarChar).Value = sDocNo


            adp.SelectCommand = cmd
            adp.Fill(ds)
            'result = cmd.ExecuteNonQuery
            result = ds.Tables(0)
            conn.Close()

            Return result
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return Nothing
        End Try

    End Function


End Class
