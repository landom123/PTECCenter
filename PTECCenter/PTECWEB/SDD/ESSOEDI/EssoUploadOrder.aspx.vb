
Imports System.IO
Imports ExcelDataReader
Imports System.Xml
Imports System.Data
Imports System.Web.Configuration

'Imports Microsoft.Office.Interop
'Imports ExcelDataReader

Public Class EssoUploadOrder
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public filesize As Long
    Public menutable As DataTable
    Public usercode As String
    Public tempname As String
    'Public podataset As DataSet
    Public potable As DataTable = createPO()
    Public podetailtable As DataTable = createPoDetail()

    Public poFileName As String
    Public poPath As String = ConfigurationManager.AppSettings("PoPath").ToString()
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    'Public testtable As DataTable = createtest()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'Session("usercode") = "PAB"
        'Session("username") = "Pison Anekboonyapirom"
        usercode = Session("usercode")


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        If Not IsPostBack() Then
            'testtable.Rows.Clear()
        Else
            If Session("po") Is Nothing Then
                '
            Else
                poFileName = Session("poFileName")
                potable = Session("po")
                podetailtable = Session("podetail")

            End If

        End If
    End Sub


    Private Function createPO() As DataTable
        Dim mytable As New DataTable
        mytable.Columns.Add(New DataColumn("SH_REQUESTED_DELIVERY_DATE", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_CUSTOMER_NO", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_SHIP_TO_PARTY", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_PO_NO", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_VEHICLE_ID", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_COMPARTMENT_INFO", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_COMBINE_LOAD", GetType(String)))
        mytable.Columns.Add(New DataColumn("SH_PLANT", GetType(String)))

        Return mytable
    End Function
    Private Function createPoDetail() As DataTable
        Dim mytable As New DataTable
        mytable.Columns.Add(New DataColumn("SH_PO_NO", GetType(String)))
        mytable.Columns.Add(New DataColumn("SD_ITEM_NO", GetType(String)))
        mytable.Columns.Add(New DataColumn("SD_MATERIAL_CODE", GetType(String)))
        mytable.Columns.Add(New DataColumn("SD_QUANTITY", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("SD_SALES_UNIT", GetType(String)))
        mytable.Columns.Add(New DataColumn("TANK01", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK02", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK03", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK04", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK05", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK06", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK07", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK08", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK09", GetType(Integer)))
        mytable.Columns.Add(New DataColumn("TANK10", GetType(Integer)))
        'mytable.Columns.Add(New DataColumn("TANK11", GetType(Integer)))
        'mytable.Columns.Add(New DataColumn("TANK12", GetType(Integer)))
        'mytable.Columns.Add(New DataColumn("TANK13", GetType(Integer)))
        'mytable.Columns.Add(New DataColumn("TANK14", GetType(Integer)))
        'mytable.Columns.Add(New DataColumn("TANK15", GetType(Integer)))
        Return mytable
    End Function

    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        Using stream As FileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read)
            Dim excelReader As IExcelDataReader

            If Extension = ".xlsx" Then
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
            Else
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
            End If

            Dim ds As DataSet = excelReader.AsDataSet()

            ' gen file id = yyyyMMddhhmmss
            ' file name = yyyyMMddhhmmss.xml
            ' pono=yyyMMddhhmmss + no of po
            'getdatatotest(ds.Tables(0))
            Now.ToString("yyyyMMddhhmmss", _cultureEnInfo)
            Dim pono As String
            pono = Now.ToString("yyyyMMddhhmmss", _cultureEnInfo)
            poFileName = pono
            Session("poFileName") = pono
            GetDataToTable(pono, ds.Tables(0)) 'send data to data table



            'GridView1.DataSource = ds.Tables(0)
            'GridView1.DataBind()
            'Response.Write(ds.Tables(0).Columns.Count)
        End Using
        'getexcelinterop(FilePath)

    End Sub

    Private Sub GetDataToTable(pono As String, mytable As DataTable)
        Dim i As Integer
        Dim porunno As Integer = 0
        Dim strdlvrdate As String
        Dim shipto, cusno As String
        Dim itemno As Integer
        potable.Rows.Clear()
        podetailtable.Rows.Clear()
        Dim a As Integer
        With mytable
            a = .Rows.Count()
            For i = 0 To .Rows.Count - 1
                ' insert head

                If Not IsDBNull(.Rows(i).Item(3)) Then
                    cusno = .Rows(i).Item(3).ToString
                    If Not cusno = "Sold To No." Then
                        If cusno = "211100" Then ' head po

                            itemno = 1
                            porunno += 1
                            strdlvrdate = .Rows(i).Item(6)
                            shipto = .Rows(i).Item(5)
                            potable.Rows.Add(strdlvrdate,
                                        .Rows(i).Item(3),
                                        shipto,
                                        pono & porunno.ToString("00"),
                                        .Rows(i).Item(9),
                                        "",
                                        "",
                                        .Rows(i).Item(8)
                                        )
                        Else
                            'detail po

                            podetailtable.Rows.Add(pono & porunno.ToString("00"),
                                                   itemno.ToString("0000") & "0",
                                                   .Rows(i).Item(2),
                                                   .Rows(i).Item(3),
                                                   .Rows(i).Item(4),
                                                   .Rows(i).Item(11),
                                                   .Rows(i).Item(12),
                                                   .Rows(i).Item(13),
                                                   .Rows(i).Item(14),
                                                   .Rows(i).Item(15),
                                                   .Rows(i).Item(16),
                                                   .Rows(i).Item(17),
                                                   .Rows(i).Item(18),
                                                   .Rows(i).Item(19),
                                                   .Rows(i).Item(20))

                            itemno += 1

                        End If


                        'insert detail
                    End If

                End If
            Next
            Session("po") = potable
            Session("podetail") = podetailtable

        End With

        GridView1.DataSource = potable
        GridView1.DataBind()
        GridView2.DataSource = podetailtable
        GridView2.DataBind()
    End Sub
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim filename As String

        'Dim dateEng As DateTime = Convert.ToDateTime(myDateTime, _cultureEnInfo)
        'Me.lblEng.Text = dateEng.ToString("dd MMM yyyy", _cultureEnInfo)

        filename = Server.MapPath("~\temp\" & FileUpload1.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Now.ToString("yyyyMMddhhmmss", _cultureEnInfo) & Path.GetExtension(filename))
        'filename = Path.GetFileName(FileUpload1.FileName)
        If Me.FileUpload1.HasFile Then
            'Import_To_Grid(filename, ".xlsx", "No")
            FileUpload1.SaveAs(tempname)
            Import_To_Grid(tempname, Path.GetExtension(filename))
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim edi As New EDI
        Dim result As String = ""
        If SaveToSQL() = True Then
            System.IO.File.Delete(poFileName)
            result = edi.POCreateXML(potable, podetailtable, poFileName)
            If result = "" Then
                Response.Redirect("EssoUploadOrder.aspx")
            Else
                lblstatus.Text = result
            End If
        Else
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('เกิดข้อผิดพลาดไม่สามารถสร้าง PO ได้ อาจเกิดจากข้อมูลซ้ำ หรือวันที่ลงน้ำมันไม่ถูกต้อง กรุณาตรวจสอบข้อมูล');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub

    Private Function SaveToSQL() As Boolean
        Dim result As Boolean = True
        Dim po As New EDI
        Dim vehicleid As String
        Dim tank01, tank02, tank03, tank04, tank05, tank06, tank07, tank08, tank09, tank10 As Integer

        'save po and detail to database
        For Each row As DataRow In potable.Rows
            If row.Item("SH_VEHICLE_ID") Is DBNull.Value Then
                vehicleid = ""
            Else
                vehicleid = row.Item("SH_VEHICLE_ID")
            End If
            If po.POSave(row.Item("SH_REQUESTED_DELIVERY_DATE"), row.Item("SH_CUSTOMER_NO"), row.Item("SH_SHIP_TO_PARTY"),
                  row.Item("SH_PO_NO"), vehicleid, row.Item("SH_PLANT"), usercode, poFileName) = False Then
                result = False
            End If
        Next
        If result = True Then
            For Each row As DataRow In podetailtable.Rows
                If row.Item("TANK01") Is DBNull.Value Then
                    tank01 = 0
                Else
                    tank01 = row.Item("TANK01")
                End If
                If row.Item("TANK02") Is DBNull.Value Then
                    tank02 = 0
                Else
                    tank02 = row.Item("TANK02")
                End If
                If row.Item("TANK03") Is DBNull.Value Then
                    tank03 = 0
                Else
                    tank03 = row.Item("TANK03")
                End If
                If row.Item("TANK04") Is DBNull.Value Then
                    tank04 = 0
                Else
                    tank04 = row.Item("TANK04")
                End If
                If row.Item("TANK05") Is DBNull.Value Then
                    tank05 = 0
                Else
                    tank05 = row.Item("TANK05")
                End If
                If row.Item("TANK06") Is DBNull.Value Then
                    tank06 = 0
                Else
                    tank06 = row.Item("TANK06")
                End If
                If row.Item("TANK07") Is DBNull.Value Then
                    tank07 = 0
                Else
                    tank07 = row.Item("TANK07")
                End If
                If row.Item("TANK08") Is DBNull.Value Then
                    tank08 = 0
                Else
                    tank08 = row.Item("TANK08")
                End If
                If row.Item("TANK09") Is DBNull.Value Then
                    tank09 = 0
                Else
                    tank09 = row.Item("TANK09")
                End If
                If row.Item("TANK10") Is DBNull.Value Then
                    tank10 = 0
                Else
                    tank10 = row.Item("TANK10")
                End If

                po.PODetailSave(row.Item("SH_PO_NO"), row.Item("SD_ITEM_NO"),
                                row.Item("SD_MATERIAL_CODE"), row.Item("SD_QUANTITY"),
                                row.Item("SD_SALES_UNIT"), tank01, tank02, tank03, tank04, tank05,
                                tank06, tank07, tank08, tank09, tank10)
            Next
        End If
        Return result
    End Function

    'Private Sub btnorder_Click(sender As Object, e As EventArgs) Handles btnorder.Click
    '    Dim edi As New EDI
    '    edi.OrderGetXML("Order_", usercode)
    'End Sub

    'Private Sub btnship_Click(sender As Object, e As EventArgs) Handles btnship.Click
    '    Dim edi As New EDI
    '    edi.ShipGetXML("Ship_", usercode)
    'End Sub

    'Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
    '    Dim edi As New EDI
    '    edi.LoadconfirmGetXML("Loadconfirm_", usercode)
    'End Sub

    'Protected Sub btnInvoice_Click(sender As Object, e As EventArgs) Handles btnInvoice.Click

    '    Dim edi As New EDI
    '    Dim filepath As String
    '    'get invoice oil
    '    filepath = WebConfigurationManager.AppSettings("InvoiceOilPath")
    '    edi.InvoiceGetXML("Invoice_", filepath, "Invoice_*.xml", usercode)

    '    'get invoice non oil
    '    filepath = WebConfigurationManager.AppSettings("InvoiceNonOilPath")
    '    edi.InvoiceGetXML("Invoice_", filepath, "Invoice_*.xml", usercode)

    '    'get cn oil
    '    filepath = WebConfigurationManager.AppSettings("CnOilPath")
    '    edi.InvoiceGetXML("CN_", filepath, "CN_*.xml", usercode)

    '    'get cn non oil
    '    filepath = WebConfigurationManager.AppSettings("CnNonOilPath")
    '    edi.InvoiceGetXML("CN_", filepath, "CN_*.xml", usercode)

    '    'get dn oil
    '    filepath = WebConfigurationManager.AppSettings("DnOilPath")
    '    edi.InvoiceGetXML("DN_", filepath, "DN_*.xml", usercode)

    '    'get dn non oil
    '    filepath = WebConfigurationManager.AppSettings("DnNonOilPath")
    '    edi.InvoiceGetXML("DN_", filepath, "DN_*.xml", usercode)

    'End Sub
End Class