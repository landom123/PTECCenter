Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration
Imports System.Xml


Public Class EDI
    Public popath As String = WebConfigurationManager.AppSettings("popath")
    Public orderpath As String = WebConfigurationManager.AppSettings("OrderPath")
    Public ConfirmPath As String = WebConfigurationManager.AppSettings("ConfirmPath")
    Public ShipPath As String = WebConfigurationManager.AppSettings("ShipPath")


    Public Function EDI_Other_TTCost_Save_D365(ByRef invoicedate As String, duedate As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_Other_TTCost_Save_D365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoicedate", SqlDbType.VarChar).Value = invoicedate
        cmd.Parameters.Add("@duedate", SqlDbType.VarChar).Value = duedate


        cmd.ExecuteNonQuery()
        'Try
        '    adp.SelectCommand = cmd
        '    adp.Fill(ds)
        '    dt = ds.Tables(0)
        'Catch ex As Exception
        '    Throw ex
        'End Try


        conn.Close()

        Return result
    End Function

    Public Function SaveTTForD365AndGetJson(invoiceno As String, duedate As String,
                                            cost As Double, branch As String, usercode As String,
                                            vendor As String, carno As String, terminal As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_TTCost_SaveAndReturnForJSON"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = invoiceno
        cmd.Parameters.Add("@duedate", SqlDbType.VarChar).Value = duedate
        cmd.Parameters.Add("@ttcost", SqlDbType.Money).Value = cost
        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = vendor
        cmd.Parameters.Add("@carno", SqlDbType.VarChar).Value = carno
        cmd.Parameters.Add("@terminal", SqlDbType.VarChar).Value = terminal

        cmd.ExecuteNonQuery()
        'Try
        '    adp.SelectCommand = cmd
        '    adp.Fill(ds)
        '    dt = ds.Tables(0)
        'Catch ex As Exception
        '    Throw ex
        'End Try


        conn.Close()

        Return result
    End Function
    Public Function EDI_TTCost_SaveV2(invoicedate As String, duedate As String,
                                            usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_TTCost_SaveV2"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoicedate", SqlDbType.VarChar).Value = invoicedate
        cmd.Parameters.Add("@duedate", SqlDbType.VarChar).Value = duedate
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'Try
        '    adp.SelectCommand = cmd
        '    adp.Fill(ds)
        '    dt = ds.Tables(0)
        'Catch ex As Exception
        '    Throw ex
        'End Try


        conn.Close()

        Return result
    End Function
    Public Function SaveCnDnAdjustCostForD365AndGetJson(invoiceno As String, usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_CNDN_Adjust_Cost_SaveAndReturnForJSON"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoice", SqlDbType.VarChar).Value = invoiceno
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)

        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function
    Public Function SaveInvoiceForD365AndGetJson(invoiceno As String, usercode As String) As String

        Dim result As String = ""
        Dim dt As New DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_Invoice_SaveAndReturnForJSON"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoice", SqlDbType.VarChar).Value = invoiceno
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)

        Catch ex As Exception
            Throw ex
        End Try


        conn.Close()

        Return result
    End Function
    Public Function SaveSplitData(invoiceno As String, usercode As String, matcode As String,
                                  sono As String, shipto As String, volume As Double) As DataTable

        Dim result As DataTable

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter


        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_Save_SplitData"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = invoiceno
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@sono", SqlDbType.VarChar).Value = sono
        cmd.Parameters.Add("@matcode", SqlDbType.VarChar).Value = matcode
        cmd.Parameters.Add("@shipto", SqlDbType.VarChar).Value = shipto
        cmd.Parameters.Add("@volume", SqlDbType.Float).Value = volume



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)


        conn.Close()

        Return Result
    End Function
    Public Function RemoveSplitData(id As Double) As Boolean

        Dim result As Boolean = True

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter


        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_Remove_SplitData"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id

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
    Public Function SaveToBatch(invoiceno As String, send As Int16, usercode As String) As Boolean

        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_SaveInvoicetoD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = invoiceno
        cmd.Parameters.Add("@send", SqlDbType.Int).Value = send
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode


        Try
            cmd.ExecuteNonQuery()
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
    Public Function GetInvoiceInfo(InvoiceNo As String) As DataSet

        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_InvoiceDetailForD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = InvoiceNo


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
    Public Function shiptoList() As DataTable

        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "branch_list"
        cmd.CommandType = CommandType.StoredProcedure



        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function

    Public Function Supply_OtherTT_List_forD365(docdate As String) As DataTable

        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_OtherTT_List_forD365"

        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = docdate

        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function

    Public Function EDI_CnDnAdjust_forExcel(begindate As String, enddate As String) As DataSet
        Dim result As DataSet

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_CnDnAdjust_forExcel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()


        Return result
    End Function
    Public Function EDI_POCnDn_Data_for_D365(begindate As String, enddate As String) As DataSet
        Dim result As DataSet

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_POCnDn_Data_for_D365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ""


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()


        Return result
    End Function
    Public Function EDI_CnDn_nonoil_Data_for_D365(begindate As String, enddate As String) As DataSet
        Dim result As DataSet

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_CnDn_nonoil_Data_for_D365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ""


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()


        Return result
    End Function
    Public Function EDI_TTCOST_Data_for_D365(begindate As String, enddate As String) As DataSet
        Dim result As DataSet

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_TTCOST_Data_for_D365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate



        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()


        Return result
    End Function
    Public Function EDI_Other_TTCOST_Data_for_D365(begindate As String, enddate As String) As DataSet
        Dim result As DataSet

        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "EDI_Other_TTCOST_Data_for_D365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate



        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()


        Return result
    End Function

    Public Function ListInvoice(InvoiceDate As String, billtype As String) As DataTable

        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_InvoiceForD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = InvoiceDate
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = billtype



        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
    Public Function ListInvoiceTTCost(InvoiceDate As String) As DataTable

        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_InvoiceTTCostForD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = InvoiceDate


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
    '----------------------------------------------------------------------------------------
    '       order acknowledge
    '----------------------------------------------------------------------------------------
    Public Sub OrderGetXML(fileprefix As String, usercode As String)
        Dim xmlpath As String = orderpath

        Dim fileEntries As String() = Directory.GetFiles(xmlpath, "Order_*.xml")
        Dim xmlFile As XmlReader
        Dim archivePath As String = xmlpath & "Archive\"
        Dim destFile As String
        Dim fname As String
        ' Process the list of .txt files found in the directory. '
        Dim fileName As String



        For Each fileName In fileEntries
            Try
                If (System.IO.File.Exists(fileName)) Then
                    'Read File 
                    fname = Path.GetFileName(fileName)
                    xmlFile = XmlReader.Create(fileName, New XmlReaderSettings())
                    Dim ds As New DataSet
                    ds.ReadXml(xmlFile)
                    'Save to SQL
                    Dim i As Integer
                    For i = 0 To ds.Tables(i).Rows.Count - 1
                        With ds.Tables(i)
                            OrderSave(.Rows(i).Item("PO_NUMBER"), .Rows(i).Item("OIL_SO"),
                                      .Rows(i).Item("STATUS"), .Rows(i).Item("MESSAGE"),
                                      .Rows(i).Item("CREDIT_STATUS"), .Rows(i).Item("INCOMPLETION_HEADER_DELIVERY"),
                                      .Rows(i).Item("INCOMPLETION_HEADER_PRICING"), .Rows(i).Item("REJECT_STATUS"),
                                      fname, usercode)
                        End With
                    Next
                    'Move file to Archive
                    xmlFile.Close()
                    destFile = System.IO.Path.Combine(archivePath, fname)
                    ds.Dispose()
                    System.IO.File.Move(fileName, destFile)
                End If
            Catch ex As Exception

            End Try
        Next

    End Sub

    Public Sub OrderSave(PO_NUMBER As String, OIL_SO As String, STATUS As String,
                         MESSAGE As String, CREDIT_STATUS As String, INCOMPLETION_HEADER_DELIVERY As String,
                         INCOMPLETION_HEADER_PRICING As String, REJECT_STATUS As String, FILENAME As String,
                         CREATEBY As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_ORDERACK"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PO_NUMBER", SqlDbType.VarChar).Value = PO_NUMBER
        cmd.Parameters.Add("@OIL_SO", SqlDbType.VarChar).Value = OIL_SO
        cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = STATUS
        cmd.Parameters.Add("@MESSAGE", SqlDbType.VarChar).Value = MESSAGE
        cmd.Parameters.Add("@CREDIT_STATUS", SqlDbType.VarChar).Value = CREDIT_STATUS
        cmd.Parameters.Add("@INCOMPLETION_HEADER_DELIVERY", SqlDbType.VarChar).Value = INCOMPLETION_HEADER_DELIVERY
        cmd.Parameters.Add("@INCOMPLETION_HEADER_PRICING", SqlDbType.VarChar).Value = INCOMPLETION_HEADER_PRICING
        cmd.Parameters.Add("@REJECT_STATUS", SqlDbType.VarChar).Value = REJECT_STATUS
        cmd.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = FILENAME
        cmd.Parameters.Add("@CREATEBY", SqlDbType.VarChar).Value = CREATEBY

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub

    '----------------------------------------------------------------------------------------
    '       shipment
    '----------------------------------------------------------------------------------------
    Public Sub ShipGetXML(fileprefix As String, usercode As String)
        Dim xmlpath As String = ShipPath

        Dim fileEntries As String() = Directory.GetFiles(xmlpath, "Ship_*.xml")
        Dim xmlFile As XmlReader
        Dim archivePath As String = xmlpath & "Archive\"
        Dim destFile As String
        Dim fname As String
        ' Process the list of .txt files found in the directory. '
        Dim fileName As String
        Try


            For Each fileName In fileEntries
                If (System.IO.File.Exists(fileName)) Then
                    'Read File 
                    fname = Path.GetFileName(fileName)
                    xmlFile = XmlReader.Create(fileName, New XmlReaderSettings())
                    Dim ds As New DataSet
                    ds.ReadXml(xmlFile)
                    'Save to SQL
                    Dim i As Integer
                    For i = 0 To ds.Tables("T_HEADER").Rows.Count - 1
                        With ds.Tables("T_HEADER")
                            ShipSave(.Rows(i).Item("SHIPMENT"), .Rows(i).Item("VEHICLE"),
                                      .Rows(i).Item("DELETE"),
                                      fname, usercode)
                        End With
                    Next
                    For i = 0 To ds.Tables("T_ITEM").Rows.Count - 1
                        With ds.Tables("T_ITEM")
                            ShipSaveDetail(.Rows(i).Item("SHIPMENT"), .Rows(i).Item("SEQ"),
                                      .Rows(i).Item("ESSO_SO"), .Rows(i).Item("PO_NUMBER"))
                        End With
                    Next
                    'Move file to Archive
                    xmlFile.Close()
                    destFile = System.IO.Path.Combine(archivePath, fname)
                    ds.Dispose()
                    System.IO.File.Move(fileName, destFile)
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ShipSaveDetail(SHIPMENT As String, SEQ As Integer, ORDER As String,
                         PONUMBER As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_SHIPMENT_DETAIL"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@shipment", SqlDbType.VarChar).Value = SHIPMENT
        cmd.Parameters.Add("@seq", SqlDbType.VarChar).Value = SEQ
        cmd.Parameters.Add("@order", SqlDbType.VarChar).Value = ORDER
        cmd.Parameters.Add("@po_number", SqlDbType.VarChar).Value = PONUMBER
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub

    Public Sub ShipSave(SHIPMENT As String, VEHICLE As String,
                              DELETION As String, FILENAME As String,
                              CREATEBY As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_SHIPMENT"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@shipment", SqlDbType.VarChar).Value = SHIPMENT
        cmd.Parameters.Add("@vehicle", SqlDbType.VarChar).Value = VEHICLE
        cmd.Parameters.Add("@deletion", SqlDbType.VarChar).Value = DELETION
        cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = FILENAME
        cmd.Parameters.Add("@CREATEBY", SqlDbType.VarChar).Value = CREATEBY

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub

    '----------------------------------------------------------------------------------------
    '       order confirmation
    '----------------------------------------------------------------------------------------
    Public Sub LoadconfirmGetXML(fileprefix As String, usercode As String)
        Dim xmlpath As String = ConfirmPath

        Dim fileEntries As String() = Directory.GetFiles(xmlpath, "Loadconfirm_*.xml")
        Dim xmlFile As XmlReader
        Dim archivePath As String = xmlpath & "Archive\"
        Dim destFile As String
        Dim PONUMBER As String = ""
        Dim fname As String
        ' Process the list of .txt files found in the directory. '
        Dim fileName As String
        Try


            For Each fileName In fileEntries
                If (System.IO.File.Exists(fileName)) Then
                    'Read File 
                    fname = Path.GetFileName(fileName)
                    xmlFile = XmlReader.Create(fileName, New XmlReaderSettings())
                    Dim ds As New DataSet
                    ds.ReadXml(xmlFile)
                    'Save to SQL
                    Dim i As Integer
                    For i = 0 To ds.Tables("T_HEADER").Rows.Count - 1
                        With ds.Tables("T_HEADER")
                            PONUMBER = .Rows(i).Item("PO_NUMBER")
                            LoadconfirmSave(.Rows(i).Item("PO_NUMBER"), .Rows(i).Item("SH_SHIP_TO_PARTY"),
                                      .Rows(i).Item("STATUS"), .Rows(i).Item("TOP_LOAD_DATE"), .Rows(i).Item("TOP_LOAD_TIME"),
                                      fname, usercode)
                        End With
                    Next
                    For i = 0 To ds.Tables("T_ITEM").Rows.Count - 1
                        With ds.Tables("T_ITEM")
                            LoadconfirmSaveDetail(PONUMBER, .Rows(i).Item("ESSO_SO"),
                                      .Rows(i).Item("PO_ITEM_NO"), .Rows(i).Item("SH_PLANT"),
                                      .Rows(i).Item("MAT_NO"), .Rows(i).Item("MAT_NAME"),
                                      .Rows(i).Item("LC_QTY_L"), .Rows(i).Item("MAT_TEMP"),
                                      .Rows(i).Item("API_GRAVITY"))
                        End With
                    Next
                    'Move file to Archive
                    xmlFile.Close()
                    destFile = System.IO.Path.Combine(archivePath, fname)
                    ds.Dispose()
                    System.IO.File.Move(fileName, destFile)
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub LoadconfirmSaveDetail(PO_NUMBER As String, ESSO_SO As String, PO_ITEM_NO As Integer,
                                     SH_PLANT As String, MAT_NO As String, MAT_NAME As String,
                                     LC_QTY_L As Integer,
                                     MAT_TEMP As Double, API_GRAVITY As Double)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter


        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_LOADCONFIRM_DETAIL"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PO_NUMBER", SqlDbType.VarChar).Value = PO_NUMBER
        cmd.Parameters.Add("@ESSO_SO", SqlDbType.VarChar).Value = ESSO_SO
        cmd.Parameters.Add("@PO_ITEM_NO", SqlDbType.TinyInt).Value = PO_ITEM_NO
        cmd.Parameters.Add("@SH_PLANT", SqlDbType.VarChar).Value = SH_PLANT

        cmd.Parameters.Add("@MAT_NO", SqlDbType.VarChar).Value = MAT_NO
        cmd.Parameters.Add("@MAT_NAME", SqlDbType.VarChar).Value = MAT_NAME
        cmd.Parameters.Add("@LC_QTY_L", SqlDbType.Decimal).Value = LC_QTY_L
        cmd.Parameters.Add("@MAT_TEMP", SqlDbType.Decimal).Value = MAT_TEMP
        cmd.Parameters.Add("@API_GRAVITY", SqlDbType.Decimal).Value = API_GRAVITY

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub

    Public Sub LoadconfirmSave(PO_NUMBER As String, SH_SHIP_TO_PARTY As String,
                              STATUS As String, TOP_LOAD_DATE As String, TOP_LOAD_TIME As String,
                              FILENAME As String, CREATEBY As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_LOADCONFIRM"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PO_NUMBER", SqlDbType.VarChar).Value = PO_NUMBER
        cmd.Parameters.Add("@SH_SHIP_TO_PARTY", SqlDbType.VarChar).Value = SH_SHIP_TO_PARTY
        cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = STATUS
        cmd.Parameters.Add("@TOP_LOAD_DATE", SqlDbType.VarChar).Value = TOP_LOAD_DATE
        cmd.Parameters.Add("@TOP_LOAD_TIME", SqlDbType.VarChar).Value = TOP_LOAD_TIME
        cmd.Parameters.Add("@CREATEBY", SqlDbType.VarChar).Value = CREATEBY
        cmd.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = FILENAME

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub

    '----------------------------------------------------------------------------------------
    '       Invoice CN DN
    '----------------------------------------------------------------------------------------
    Public Sub InvoiceGetXML(fileprefix As String, fullpath As String, filetype As String, usercode As String)
        Dim xmlpath As String = fullpath

        Dim fileEntries As String() = Directory.GetFiles(xmlpath, filetype)
        Dim xmlFile As XmlReader
        Dim archivePath As String = xmlpath & "Archive\"
        Dim destFile As String
        Dim fname As String
        ' Process the list of .txt files found in the directory. '
        Dim fileName As String



        For Each fileName In fileEntries
            Try
                If (System.IO.File.Exists(fileName)) Then
                    'Read File 
                    fname = Path.GetFileName(fileName)
                    xmlFile = XmlReader.Create(fileName, New XmlReaderSettings())
                    Dim ds As New DataSet
                    ds.ReadXml(xmlFile)
                    'Save to SQL
                    Dim i As Integer
                    Dim SHIP As String
                    Dim INVOICE As String = ""
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        With ds.Tables("T_HEADER")
                            If Strings.Left(.Rows(i).Item("BILLING_TYPE"), 2) = "ZF" Then
                                SHIP = .Rows(i).Item("SHIPMENT")
                            Else
                                SHIP = ""
                            End If
                            INVOICE = .Rows(i).Item("INVOICE_NO")
                            InvoiceSave(.Rows(i).Item("BILLING_TYPE"), .Rows(i).Item("INVOICE_NO"),
                                      SHIP, .Rows(i).Item("INVOICE_DATE"),
                                      .Rows(i).Item("LOADING_DATE"), .Rows(i).Item("DUE_DATE"),
                                      .Rows(i).Item("NET_VALUE"), .Rows(i).Item("VAT"), .Rows(i).Item("TOTAL_VALUE"),
                                      fname, usercode)
                        End With
                    Next
                    For i = 0 To ds.Tables("T_ITEM").Rows.Count - 1
                        With ds.Tables("T_ITEM")
                            InvoiceSaveDetail(INVOICE, .Rows(i).Item("sh_ship_to_party"), .Rows(i).Item("po_no"),
                                      .Rows(i).Item("so_no"), .Rows(i).Item("delivery_no"), .Rows(i).Item("terminal"),
                                     .Rows(i).Item("mat_code"), .Rows(i).Item("product"), .Rows(i).Item("do_volume"),
                                     .Rows(i).Item("uom"), .Rows(i).Item("unit_price"), .Rows(i).Item("item_net_value"), .Rows(i).Item("ref_invoice_no"),
                                     .Rows(i).Item("ref_order_no"))
                        End With
                    Next
                    'Move file to Archive
                    xmlFile.Close()
                    destFile = System.IO.Path.Combine(archivePath, fname)
                    ds.Dispose()
                    System.IO.File.Move(fileName, destFile)
                End If
            Catch ex As Exception
                'Throw ex
            End Try
        Next

    End Sub

    Public Sub InvoiceSave(BILLING_TYPE As String, INVOICE_NO As String, SHIP As String,
                         INVOICE_DATE As String, LOADING_DATE As String, DUE_DATE As String,
                         NET_VALUE As Double, VAT As Double, TOTAL_VALUE As Double, FILENAME As String,
                         CREATEBY As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_INVOICE"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@BILLING_TYPE", SqlDbType.VarChar).Value = BILLING_TYPE
        cmd.Parameters.Add("@INVOICE_NO", SqlDbType.VarChar).Value = INVOICE_NO
        cmd.Parameters.Add("@SHIPMENT", SqlDbType.VarChar).Value = SHIP
        cmd.Parameters.Add("@INVOICE_DATE", SqlDbType.VarChar).Value = INVOICE_DATE
        cmd.Parameters.Add("@LOADING_DATE", SqlDbType.VarChar).Value = LOADING_DATE
        cmd.Parameters.Add("@DUE_DATE", SqlDbType.VarChar).Value = DUE_DATE
        cmd.Parameters.Add("@NET_VALUE", SqlDbType.Money).Value = NET_VALUE
        cmd.Parameters.Add("@VAT", SqlDbType.Money).Value = VAT
        cmd.Parameters.Add("@TOTAL_VALUE", SqlDbType.Money).Value = TOTAL_VALUE
        cmd.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = FILENAME
        cmd.Parameters.Add("@CREATEBY", SqlDbType.VarChar).Value = CREATEBY

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            'Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub

    Public Sub InvoiceSaveDetail(INVOICE As String, sh_ship_to_party As String, po_no As String,
                         so_no As String, delivery_no As String, terminal As String,
                         mat_code As String, product As String, do_volume As String,
                         uom As String, unitprice As Double, item_net_value As Double, ref_invoice_no As String, ref_order_no As String)

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_INVOICE_DETAIL"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@INVOICE_NO", SqlDbType.VarChar).Value = INVOICE
        cmd.Parameters.Add("@sh_ship_to_party", SqlDbType.VarChar).Value = sh_ship_to_party
        cmd.Parameters.Add("@po_no", SqlDbType.VarChar).Value = po_no
        cmd.Parameters.Add("@so_no", SqlDbType.VarChar).Value = so_no
        cmd.Parameters.Add("@delivery_no", SqlDbType.VarChar).Value = delivery_no
        cmd.Parameters.Add("@terminal", SqlDbType.VarChar).Value = terminal
        cmd.Parameters.Add("@mat_code", SqlDbType.VarChar).Value = mat_code
        cmd.Parameters.Add("@product", SqlDbType.VarChar).Value = product
        cmd.Parameters.Add("@do_volume", SqlDbType.VarChar).Value = do_volume
        cmd.Parameters.Add("@uom", SqlDbType.VarChar).Value = uom
        cmd.Parameters.Add("@unitprice", SqlDbType.VarChar).Value = unitprice
        cmd.Parameters.Add("@item_net_value", SqlDbType.Money).Value = item_net_value
        cmd.Parameters.Add("@ref_invoice_no", SqlDbType.VarChar).Value = ref_invoice_no
        cmd.Parameters.Add("@ref_order_no", SqlDbType.VarChar).Value = ref_order_no
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

    End Sub
    '----------------------------------------------------------------------------------------
    '       po
    '----------------------------------------------------------------------------------------
    Public Function POSave(SH_REQUESTED_DELIVERY_DATE As String,
                         SH_CUSTOMER_NO As String,
                         SH_SHIP_TO_PARTY As String,
                         SH_PO_NO As String,
                         SH_VEHICLE_ID As String,
                         SH_PLANT As String,
                         USERCODE As String,
                         CREATEDATE As String) As Boolean


        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_PO"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@SH_REQUESTED_DELIVERY_DATE", SqlDbType.VarChar).Value = SH_REQUESTED_DELIVERY_DATE
        cmd.Parameters.Add("@SH_CUSTOMER_NO", SqlDbType.VarChar).Value = SH_CUSTOMER_NO
        cmd.Parameters.Add("@SH_SHIP_TO_PARTY", SqlDbType.VarChar).Value = SH_SHIP_TO_PARTY
        cmd.Parameters.Add("@SH_PO_NO", SqlDbType.VarChar).Value = SH_PO_NO
        cmd.Parameters.Add("@SH_VEHICLE_ID", SqlDbType.VarChar).Value = SH_VEHICLE_ID
        cmd.Parameters.Add("@SH_PLANT", SqlDbType.VarChar).Value = SH_PLANT
        cmd.Parameters.Add("@USERCODE", SqlDbType.VarChar).Value = USERCODE
        'cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = CREATEDATE

        Try
            cmd.ExecuteNonQuery()
            result = True
        Catch ex As Exception
            result = False
            'Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function

    Public Function PODetailSave(SH_PO_NO As String,
                         SD_ITEM_NO As String,
                         SD_MATERIAL_CODE As String,
                         SD_QUANTITY As Integer,
                         SD_SALES_UNIT As String,
                         TANK01 As Integer, TANK02 As Integer, TANK03 As Integer,
                         TANK04 As Integer, TANK05 As Integer, TANK06 As Integer,
                         TANK07 As Integer, TANK08 As Integer, TANK09 As Integer,
                         TANK10 As Integer) As Boolean

        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "ESSO_Save_PO_DETAIL"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@SH_PO_NO", SqlDbType.VarChar).Value = SH_PO_NO
        cmd.Parameters.Add("@SD_ITEM_NO", SqlDbType.VarChar).Value = SD_ITEM_NO
        cmd.Parameters.Add("@SD_MATERIAL_CODE", SqlDbType.VarChar).Value = SD_MATERIAL_CODE
        cmd.Parameters.Add("@SD_QUANTITY", SqlDbType.Int).Value = SD_QUANTITY
        cmd.Parameters.Add("@SD_SALES_UNIT", SqlDbType.VarChar).Value = SD_SALES_UNIT
        cmd.Parameters.Add("@TANK01", SqlDbType.Int).Value = TANK01
        cmd.Parameters.Add("@TANK02", SqlDbType.Int).Value = TANK02
        cmd.Parameters.Add("@TANK03", SqlDbType.Int).Value = TANK03
        cmd.Parameters.Add("@TANK04", SqlDbType.Int).Value = TANK04
        cmd.Parameters.Add("@TANK05", SqlDbType.Int).Value = TANK05
        cmd.Parameters.Add("@TANK06", SqlDbType.Int).Value = TANK06
        cmd.Parameters.Add("@TANK07", SqlDbType.Int).Value = TANK07
        cmd.Parameters.Add("@TANK08", SqlDbType.Int).Value = TANK08
        cmd.Parameters.Add("@TANK09", SqlDbType.Int).Value = TANK09
        cmd.Parameters.Add("@TANK10", SqlDbType.Int).Value = TANK10

        Try
            cmd.ExecuteNonQuery()
            result = True
        Catch ex As Exception
            result = False
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function

    Public Function POCreateXML(potable As DataTable, podetailtable As DataTable,
                                poFileName As String) As String
        Dim result As String = ""
        Dim xmlFileName As String = popath & "PO_" & poFileName & ".xml"
        'Dim xmlSW As System.IO.StreamWriter = New System.IO.StreamWriter("d:\test.xml")
        'mydataset.WriteXml(xmlSW, XmlWriteMode.IgnoreSchema)
        'xmlSW.Close()
        Dim writer As New XmlTextWriter(xmlFileName, System.Text.Encoding.GetEncoding(874))
        writer.WriteStartDocument(True)
        writer.Formatting = System.Xml.Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("Order")
        createXmlTransferInfo(writer, poFileName)
        writer.WriteStartElement("ESSO_ORDER") '--begin esso_order
        '------------------------------ loop po
        'podataset = New DataSet
        'Dim table1 As New DataTable
        'Dim table2 As New DataTable

        'podataset.Tables.Add(table1)
        'podataset.Tables.Add(table2)

        result = POCreateXMLDetail(writer, potable, podetailtable)
        If result <> "" Then
            writer.Flush()
            writer.Close()
            System.IO.File.Delete(xmlFileName)
        Else
            '------------------------------
            writer.WriteEndElement() '--end esso_order
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
        End If

        Return result
    End Function

    Private Function POCreateXMLDetail(ByVal writer As XmlTextWriter, mytable As DataTable, podetailtable As DataTable) As String
        'Dim a As Integer = mytable.Rows.Count
        Dim result As String = ""
        Dim vehicleid As String = ""
        With mytable
            For Each row As DataRow In mytable.Rows
                'strDetail = row.Item("Detail")


                writer.WriteStartElement("ESSO_SALEORHD_TMP") '--begin ESSO_SALEORHD_TMP po head
                writer.WriteStartElement("item") 'item

                '----------------------------------------
                writer.WriteStartElement("SH_REQUESTED_DELIVERY_DATE")
                writer.WriteString(row.Item("SH_REQUESTED_DELIVERY_DATE"))
                writer.WriteEndElement()
                '----------------------------------------
                writer.WriteStartElement("SH_CUSTOMER_NO")
                writer.WriteString(row.Item("SH_CUSTOMER_NO"))
                writer.WriteEndElement()
                '----------------------------------------
                writer.WriteStartElement("SH_SHIP_TO_PARTY")
                writer.WriteString(row.Item("SH_SHIP_TO_PARTY"))
                writer.WriteEndElement()
                '----------------------------------------
                writer.WriteStartElement("SH_PO_NO")
                writer.WriteString(row.Item("SH_PO_NO"))
                writer.WriteEndElement()
                '----------------------------------------
                writer.WriteStartElement("SH_VEHICLE_ID")
                If Not row.Item("SH_VEHICLE_ID") Is DBNull.Value Then
                    vehicleid = row.Item("SH_VEHICLE_ID")
                    writer.WriteString(row.Item("SH_VEHICLE_ID"))
                Else
                    vehicleid = ""
                    writer.WriteString("")
                End If
                writer.WriteEndElement()
                '----------------------------------------
                'writer.WriteStartElement("SH_COMBINE_LOAD")
                'writer.WriteString(row.Item("SH_COMBINE_LOAD"))
                'writer.WriteEndElement()
                '----------------------------------------
                writer.WriteStartElement("SH_PLANT")
                writer.WriteString(row.Item("SH_PLANT"))
                writer.WriteEndElement()
                '----------------------------------------
                ' po item
                writer.WriteStartElement("ESSO_SALEORDTL_TMP")
                '-  po item

                result = POcreateXmlItem(writer, podetailtable, row.Item("SH_PO_NO"), vehicleid)

                writer.WriteEndElement()
                '----------------------------------------

                writer.WriteEndElement() 'item
                writer.WriteEndElement() '--end ESSO_SALEORHD_TMP
            Next row
        End With
        Return result
    End Function

    Private Function POcreateXmlItem(ByVal writer As XmlTextWriter,
                                podetailtable As DataTable, pono As String,
                                vehicleid As String) As String

        Dim result As String = ""
        Dim chkCompartment As Boolean = False ' เช็คว่ามีการระบุ compartment หรือไม่ ถ้ามี = true และนำไปตรวจถ้าไม่มี compartment ต้องไม่มีเลขทะเบียนรถ vehicleid
        Dim dr2 As DataRow() = podetailtable.Select("SH_PO_NO = '" & pono & "'")

        For Each dr2row As DataRow In dr2
            writer.WriteStartElement("item")
            '----------------------------------------
            writer.WriteStartElement("SD_PO_NO")
            writer.WriteString(dr2row.Item("SH_PO_NO"))
            writer.WriteEndElement()
            '----------------------------------------
            writer.WriteStartElement("SD_ITEM_NO")
            writer.WriteString(dr2row.Item("SD_ITEM_NO"))
            writer.WriteEndElement()
            '----------------------------------------
            writer.WriteStartElement("SD_MATERIAL_CODE")
            writer.WriteString(dr2row.Item("SD_MATERIAL_CODE"))
            writer.WriteEndElement()
            '----------------------------------------
            writer.WriteStartElement("SD_QUANTITY")
            writer.WriteString(dr2row.Item("SD_QUANTITY"))
            writer.WriteEndElement()
            '----------------------------------------
            writer.WriteStartElement("SD_SALES_UNIT")
            writer.WriteString(dr2row.Item("SD_SALES_UNIT"))
            writer.WriteEndElement()
            '----------COMPARTMENT------------------------------
            If Not (dr2row.Item("TANK01") Is DBNull.Value) Then
                If dr2row.Item("TANK01") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(1)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK01"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK02") Is DBNull.Value) Then
                If dr2row.Item("TANK02") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(2)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK02"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK03") Is DBNull.Value) Then
                If dr2row.Item("TANK03") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(3)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK03"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK04") Is DBNull.Value) Then
                If dr2row.Item("TANK04") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(4)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK04"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK05") Is DBNull.Value) Then
                If dr2row.Item("TANK05") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(5)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK05"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK06") Is DBNull.Value) Then
                If dr2row.Item("TANK06") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(6)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK06"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK07") Is DBNull.Value) Then
                If dr2row.Item("TANK07") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(7)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK07"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK08") Is DBNull.Value) Then
                If dr2row.Item("TANK08") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(8)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK08"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK09") Is DBNull.Value) Then
                If dr2row.Item("TANK09") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(9)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK09"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If
            If Not (dr2row.Item("TANK10") Is DBNull.Value) Then
                If dr2row.Item("TANK10") > 0 Then
                    chkCompartment = True
                    writer.WriteStartElement("SD_COMPARTMENT_INFO")
                    writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
                    writer.WriteString(10)
                    writer.WriteEndElement()
                    writer.WriteStartElement("SD_LOAD_VOLUME")
                    writer.WriteString(dr2row.Item("TANK10"))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
            End If

            If chkCompartment = True Then
                If String.IsNullOrEmpty(vehicleid) Then
                    'error มีทะเบียน แต่ไม่มี compartment
                    result = "error บางรายการ มี compartment แต่ไม่มีทะเบียนรถ"
                End If
            Else
                If Not String.IsNullOrEmpty(vehicleid) Then
                    'error ไม่มี compartment แต่มีทะเบียน
                    result = "error บางรายการ มีทะเบียนรถ แต่ไม่มี compartment"
                End If
            End If
            'If Not (dr2row.Item("TANK11") Is DBNull.Value) Then
            '    If dr2row.Item("TANK11") > 0 Then
            '        writer.WriteStartElement("SD_COMPARTMENT_INFO")
            '        writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
            '        writer.WriteString(11)
            '        writer.WriteEndElement()
            '        writer.WriteStartElement("SD_LOAD_VOLUME")
            '        writer.WriteString(dr2row.Item("TANK11"))
            '        writer.WriteEndElement()
            '        writer.WriteEndElement()
            '    End If
            'End If
            'If Not (dr2row.Item("TANK12") Is DBNull.Value) Then
            '    If dr2row.Item("TANK12") > 0 Then
            '        writer.WriteStartElement("SD_COMPARTMENT_INFO")
            '        writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
            '        writer.WriteString(12)
            '        writer.WriteEndElement()
            '        writer.WriteStartElement("SD_LOAD_VOLUME")
            '        writer.WriteString(dr2row.Item("TANK12"))
            '        writer.WriteEndElement()
            '        writer.WriteEndElement()
            '    End If
            'End If
            'If Not (dr2row.Item("TANK13") Is DBNull.Value) Then
            '    If dr2row.Item("TANK13") > 0 Then
            '        writer.WriteStartElement("SD_COMPARTMENT_INFO")
            '        writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
            '        writer.WriteString(13)
            '        writer.WriteEndElement()
            '        writer.WriteStartElement("SD_LOAD_VOLUME")
            '        writer.WriteString(dr2row.Item("TANK13"))
            '        writer.WriteEndElement()
            '        writer.WriteEndElement()
            '    End If
            'End If
            'If Not (dr2row.Item("TANK14") Is DBNull.Value) Then
            '    If dr2row.Item("TANK14") > 0 Then
            '        writer.WriteStartElement("SD_COMPARTMENT_INFO")
            '        writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
            '        writer.WriteString(14)
            '        writer.WriteEndElement()
            '        writer.WriteStartElement("SD_LOAD_VOLUME")
            '        writer.WriteString(dr2row.Item("TANK14"))
            '        writer.WriteEndElement()
            '        writer.WriteEndElement()
            '    End If
            'End If
            'If Not (dr2row.Item("TANK15") Is DBNull.Value) Then
            '    If dr2row.Item("TANK15") > 0 Then
            '        writer.WriteStartElement("SD_COMPARTMENT_INFO")
            '        writer.WriteStartElement("SD_COMPARTMENT_NUMBER")
            '        writer.WriteString(15)
            '        writer.WriteEndElement()
            '        writer.WriteStartElement("SD_LOAD_VOLUME")
            '        writer.WriteString(dr2row.Item("TANK15"))
            '        writer.WriteEndElement()
            '        writer.WriteEndElement()
            '    End If
            'End If
            '----------COMPARTMENT------------------------------
            writer.WriteEndElement()

        Next dr2row
        Return result
    End Function
    Private Sub createXmlTransferInfo(ByVal writer As XmlTextWriter, pofilename As String)
        writer.WriteStartElement("TransferInfo")
        '----------------------------------------
        writer.WriteStartElement("TransactionID")
        writer.WriteString(pofilename)
        writer.WriteEndElement()
        '----------------------------------------
        writer.WriteStartElement("Sender")
        writer.WriteString("PureThai")
        writer.WriteEndElement()
        '----------------------------------------
        writer.WriteStartElement("Receiver")
        writer.WriteString("SEA-STRIPES")
        writer.WriteEndElement()
        '----------------------------------------
        writer.WriteEndElement()
    End Sub


    Public Sub SetCboSaleOrder(obj As Object)
        Dim edi As New EDI

        obj.DataSource = edi.Supply_SaleOrder_List()
        obj.DataValueField = "saleorder"
        obj.DataTextField = "saleorder"
        obj.DataBind()

    End Sub
    Public Function Supply_SaleOrder_List() As DataTable

        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_SaleOrder_List"
        cmd.CommandType = CommandType.StoredProcedure

        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
    Public Sub SetCboExpense(obj As Object)
        Dim edi As New EDI

        obj.DataSource = edi.Supply_Expense_List()
        obj.DataValueField = "exid"
        obj.DataTextField = "ExpenseName"
        obj.DataBind()

    End Sub

    Public Function Supply_Expense_List() As DataTable

        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_Expense_List"
        cmd.CommandType = CommandType.StoredProcedure

        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function


    Public Function Supply_SaveHead(docno As String, supplierid As Integer, usercode As String, status As Integer) As String

        ' Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_Expense_Save_Head"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@docno", SqlDbType.VarChar).Value = docno
        cmd.Parameters.Add("@supplierid", SqlDbType.Int).Value = supplierid
        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@status", SqlDbType.Int).Value = status


        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            docno = ds.Tables(0).Rows(0).Item("docno")
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return docno
    End Function

    Public Function Supply_SaveDetail(docno As String, saleorder As String, expense As String, amount As Double) As Boolean

        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_Expense_Save_Detail"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@docno", SqlDbType.VarChar).Value = docno
        cmd.Parameters.Add("@saleorder", SqlDbType.Int).Value = saleorder
        cmd.Parameters.Add("@expense", SqlDbType.VarChar).Value = expense
        cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount


        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function

    Public Function Supply_Expense_Clear_Detail_beforeSave(docno As String) As Boolean

        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_Expense_Clear_Detail_beforeSave"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@docno", SqlDbType.VarChar).Value = docno

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function


    Public Function Supply_Expense_Find(docno As String) As DataSet

        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_Expense_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@docno", SqlDbType.VarChar).Value = docno

        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
End Class
