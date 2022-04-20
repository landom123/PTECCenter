
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyImportWSPrice
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'gsmtable = Session("gsmtable")
            'BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("whprice") Is Nothing) Then
                editable = Session("whprice")
                BindData()
            End If
        End If

    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("billing_type", GetType(String))
        dt.Columns.Add("invoice_no", GetType(String))
        dt.Columns.Add("invoice_date", GetType(Date))
        dt.Columns.Add("due_date", GetType(Date))
        dt.Columns.Add("link", GetType(String))
        dt.Columns.Add("chk", GetType(Boolean))

        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = editable
        gvData.DataBind()
    End Sub


    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim objedi As New EDI

        Dim chkerror As Boolean = False
        Dim mydataset As DataSet
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String


        Dim saledate, pricedate As DateTime
        Dim supply As String
        Dim g91price, g95price, e20price, e85price, b5price, b7price, b10price As Double
        Dim g91gap, g95gap, e20gap, e85gap, b5gap, b7gap, b10gap As Double

        Dim lblsaledate, lblpricedate, lblsupply As Label
        Dim lblg91price, lblg95price, lble20price, lble85price, lblb5price, lblb7price, lblb10price As Label
        Dim lblg91gap, lblg95gap, lble20gap, lble85gap, lblb5gap, lblb7gap, lblb10gap As Label
        Dim wsobj As New wholesales
        For i = 0 To gvData.Rows.Count - 1
            Dim row As GridViewRow = gvData.Rows(i)
            'Dim id As String = row.Cells(0).Text
            'Dim chkSelect As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
            lblsaledate = CType(row.FindControl("lblsaledate"), Label)
            lblpricedate = CType(row.FindControl("lblpricedate"), Label)
            lblsupply = CType(row.FindControl("lblsupply"), Label)
            lblg91price = CType(row.FindControl("lblg91price"), Label)
            lblg95price = CType(row.FindControl("lblg95price"), Label)
            lble20price = CType(row.FindControl("lble20price"), Label)
            lble85price = CType(row.FindControl("lble85price"), Label)
            lblb5price = CType(row.FindControl("lblb5price"), Label)
            lblb7price = CType(row.FindControl("lblb7price"), Label)
            lblb10price = CType(row.FindControl("lblb10price"), Label)

            lblg91gap = CType(row.FindControl("lblg91gap"), Label)
            lblg95gap = CType(row.FindControl("lblg95gap"), Label)
            lble20gap = CType(row.FindControl("lble20gap"), Label)
            lble85gap = CType(row.FindControl("lble85gap"), Label)
            lblb5gap = CType(row.FindControl("lblb5gap"), Label)
            lblb7gap = CType(row.FindControl("lblb7gap"), Label)
            lblb10gap = CType(row.FindControl("lblb10gap"), Label)


            Try
                'insert into database
                wsobj.Wholesales_Import_Price(lblsaledate.Text, lblpricedate.Text, lblsupply.Text,
                                              check_empy_return_0(lblg91price.Text), check_empy_return_0(lblg95price.Text),
                                              check_empy_return_0(lble20price.Text), check_empy_return_0(lble85price.Text),
                                              check_empy_return_0(lblb5price.Text), check_empy_return_0(lblb7price.Text),
                                              check_empy_return_0(lblb10price.Text), check_empy_return_0(lblg91gap.Text),
                                              check_empy_return_0(lblg95gap.Text), check_empy_return_0(lble20gap.Text),
                                              check_empy_return_0(lble85gap.Text), check_empy_return_0(lblb5gap.Text),
                                              check_empy_return_0(lblb7gap.Text), check_empy_return_0(lblb10gap.Text), usercode)

            Catch ex As Exception
                chkerror = True
                Dim err As String = ex.Message.Replace("'", "")
                javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

            End Try

        Next

        If chkerror = False Then
            javaScript = "<script type='text/javascript'>msgalert('บันทึกเรียบร้อย');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub
    Public Function check_empy_return_0(chkstring As String) As String
        'check empy string and return "0"
        Dim result As String

        If String.IsNullOrEmpty(chkstring) Then
            result = "0"
        Else
            result = chkstring
        End If

        Return result
    End Function
    'Private Sub ExportToExcel(mydataset As DataSet, branch As String, closedate As String)

    '    Using wb As New XLWorkbook()
    '        wb.Worksheets.Add(mydataset.Tables(0), "AP")
    '        wb.Worksheets.Add(mydataset.Tables(1), "Cost")

    '        Response.Clear()
    '        Response.Buffer = True
    '        Response.Charset = ""
    '        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '        Response.AddHeader("content-disposition", "attachment;filename=" & branch & "_" & closedate & ".xlsx")
    '        Using MyMemoryStream As New MemoryStream()
    '            wb.SaveAs(MyMemoryStream)
    '            MyMemoryStream.WriteTo(Response.OutputStream)
    '            Response.Flush()
    '            Response.End()
    '        End Using

    '    End Using
    'End Sub
    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        If Not IsNothing(chk) Then
            chk.Checked = e.Row.DataItem("chk")
        End If

    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

        '1. GNDITM
        filename = Server.MapPath("~\temp\" & FileUploadPrice.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Guid.NewGuid().ToString & Path.GetExtension(filename))
        If Me.FileUploadPrice.HasFile Then
            If Path.GetExtension(filename) = "xls" Or Path.GetExtension(filename) = ".xlsx" Then
                FileUploadPrice.SaveAs(tempname) 'save file to web server temp
                'preview
                Import_To_Grid(tempname, Path.GetExtension(filename))
            Else
                'error
            End If
        End If

        'filename = Path.GetFileName(FileUpload1.FileName)
        'filename = FileUpload1.FileName
        'filename = Path.GetDirectoryName(FileUpload1.FileName)
        'filename = Path.GetFullPath(FileUpload1.FileName)
        'filename = FileUpload1.PostedFile.FileName

    End Sub
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        Using stream As FileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read)
            Dim excelReader As IExcelDataReader

            If Extension = ".xlsx" Then
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
            Else
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
            End If

            Dim ds As DataSet = excelReader.AsDataSet(New ExcelDataSetConfiguration() With {
                    .UseColumnDataType = True,
                    .FilterSheet = Function(tableReader, sheetIndex) True,
                    .ConfigureDataTable = Function(tableReader) New ExcelDataTableConfiguration() With {
                        .UseHeaderRow = True}
                })


            ' gen file id = yyyyMMddhhmmss
            ' file name = yyyyMMddhhmmss.xml
            ' pono=yyyMMddhhmmss + no of po
            'getdatatotest(ds.Tables(0))
            Now.ToString("yyyyMMddhhmmss", _cultureEnInfo)
            'Dim pono As String
            'pono = Now.ToString("yyyyMMddhhmmss", _cultureEnInfo)
            'poFileName = pono
            Session("whprice") = ds.Tables(0)
            gvData.DataSource = ds.Tables(0)
            gvData.DataBind()
            'GetDataToTable(pono, ds.Tables(0)) 'send data to data table



            'GridView1.DataSource = ds.Tables(0)
            'GridView1.DataBind()
            'Response.Write(ds.Tables(0).Columns.Count)
        End Using
        'getexcelinterop(FilePath)

    End Sub

    'Private Sub GetDataToTable(mytable As DataTable)
    '    Dim i As Integer
    '    Dim porunno As Integer = 0
    '    Dim strdlvrdate As String
    '    Dim shipto, cusno As String
    '    Dim itemno As Integer
    '    potable.Rows.Clear()
    '    podetailtable.Rows.Clear()
    '    Dim a As Integer
    '    With mytable
    '        a = .Rows.Count()
    '        For i = 0 To .Rows.Count - 1
    '            ' insert head

    '            If Not IsDBNull(.Rows(i).Item(3)) Then
    '                cusno = .Rows(i).Item(3).ToString
    '                If Not cusno = "Sold To No." Then
    '                    If cusno = "211100" Then ' head po

    '                        itemno = 1
    '                        porunno += 1
    '                        strdlvrdate = .Rows(i).Item(6)
    '                        shipto = .Rows(i).Item(5)
    '                        potable.Rows.Add(strdlvrdate,
    '                                    .Rows(i).Item(3),
    '                                    shipto,
    '                                    pono & porunno.ToString("00"),
    '                                    .Rows(i).Item(9),
    '                                    "",
    '                                    "",
    '                                    .Rows(i).Item(8)
    '                                    )
    '                    Else
    '                        'detail po

    '                        podetailtable.Rows.Add(pono & porunno.ToString("00"),
    '                                               itemno.ToString("0000") & "0",
    '                                               .Rows(i).Item(2),
    '                                               .Rows(i).Item(3),
    '                                               .Rows(i).Item(4),
    '                                               .Rows(i).Item(11),
    '                                               .Rows(i).Item(12),
    '                                               .Rows(i).Item(13),
    '                                               .Rows(i).Item(14),
    '                                               .Rows(i).Item(15),
    '                                               .Rows(i).Item(16),
    '                                               .Rows(i).Item(17),
    '                                               .Rows(i).Item(18),
    '                                               .Rows(i).Item(19),
    '                                               .Rows(i).Item(20))

    '                        itemno += 1

    '                    End If


    '                    'insert detail
    '                End If

    '            End If
    '        Next
    '        Session("po") = potable
    '        Session("podetail") = podetailtable

    '    End With

    '    GridView1.DataSource = potable
    '    GridView1.DataBind()
    '    GridView2.DataSource = podetailtable
    '    GridView2.DataBind()
    'End Sub
End Class