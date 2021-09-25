Imports ClosedXML.Excel
Imports ExcelDataReader
Imports System.IO

Public Class RVtoHQ

    Inherits System.Web.UI.Page
    Public menutable, rvtable As DataTable
    Public mydataset As DataSet
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
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


        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

        End If

    End Sub

    Private Function creatervtable() As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("voucher", GetType(String))
        dt.Columns.Add("transdate", GetType(String))
        dt.Columns.Add("account", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("amount", GetType(Double))

        Return dt
    End Function
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        rvtable = creatervtable()
        Using stream As FileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read)
            Dim excelReader As IExcelDataReader

            If Extension = ".xlsx" Then
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
            Else
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
            End If

            Dim ds As DataSet = excelReader.AsDataSet()

            'GetDataToTable(pono, ds.Tables(0)) 'send data to data table
            'Dim dtview As DataView
            'Dim rvtb As DataTable

            'dtview = New DataView(ds.Tables("ImportD365-Payment"))

            'rvtb = dtview.ToTable(True, "Column25", "Column23", "Column2", "Column14", "Column5")

            Dim dr As DataRow = rvtable.NewRow

            For Each r As DataRow In ds.Tables("ImportD365-Payment").Rows
                If r.Item("column25") <> "VOUCHER" Then
                    dr("voucher") = r.Item("column25")
                    dr("transdate") = r.Item("column223")
                    dr("account") = r.Item("column2")
                    dr("branch") = r.Item("column14")
                    dr("amount") = r.Item("column5")
                    rvtable.Rows.Add(dr)
                End If
            Next


            Session("rv") = rvtable
            gvdata.DataSource = rvtable
            gvdata.DataBind()

        End Using
        'getexcelinterop(FilePath)

    End Sub




    Private Sub SaveData(branch As String, closedate As String)

        'Dim objgsm As New gsm
        'Dim mydataset As DataSet
        ''clear gsm_sale_other,gsm_sale_oil,gsm_paid
        'Dim result As Boolean = objgsm.GSM_delete_gsc(branch, closedate)
        'mydataset = objgsm.getGSCbyBranchDate(branch, closedate)
        ''insert ลง

        '' gsm_sale_oil--table(0)
        'For Each row As DataRow In mydataset.Tables(0).Rows
        '    objgsm.GSM_save_gsm_sale_oil(row("br_code"), row("closedate"), row("grade_title"), row("volume"), row("new_price"))
        'Next row
        '' gsm_sale_other--table(1)
        'For Each row As DataRow In mydataset.Tables(1).Rows
        '    objgsm.GSM_save_gsm_sale_other(row("br_code"), row("closedate"), row("doccode"), row("ar_number"), row("customer_name"),
        '            row("product"), row("volume"), row("amount"), row("duedate"))
        'Next row
        '' gsm_paid--table(2)
        'For Each row As DataRow In mydataset.Tables(2).Rows
        '    objgsm.GSM_save_gsm_paid(row("br_code"), row("closedate"), row("td_description"), row("amount"))
        'Next row

        ''นำข้อมูลลง ivzs GSM_Prepair_GSMtoD365
        'objgsm.GSM_Prepair_GSMtoD365(branch, closedate)


        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Save Complete');", True)
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
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

    Private Sub gvdata2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.DataSource = Session("rv")
        gvData.PageIndex = e.NewPageIndex
        gvData.DataBind()
    End Sub
End Class