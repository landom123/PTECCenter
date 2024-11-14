Imports ClosedXML.Excel
Imports ExcelDataReader
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Controls

Public Class RVtoHQ

    Inherits System.Web.UI.Page
    Public menutable, rvtable As DataTable
    Public mydataset As DataSet
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########

        'Dim objsupplier As New Supplier
        filename = Session("filename")
        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            rvtable = Session("rv")

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
        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("voucher", GetType(String))
        dt.Columns.Add("transdate", GetType(String))
        dt.Columns.Add("account", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns("id").AutoIncrement = True
        dt.Columns("Id").AutoIncrementSeed = 1
        Return dt
    End Function

    Private Function creatervtable2() As DataTable

        Dim dt As New DataTable
        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("col1", GetType(String))
        dt.Columns.Add("col2", GetType(String))
        dt.Columns.Add("col3", GetType(String))
        dt.Columns.Add("col4", GetType(Double))
        dt.Columns.Add("col5", GetType(String))
        dt.Columns.Add("col6", GetType(String))
        dt.Columns.Add("col7", GetType(String))
        dt.Columns.Add("col8", GetType(String))
        dt.Columns("id").AutoIncrement = True
        dt.Columns("Id").AutoIncrementSeed = 1
        Return dt
    End Function

    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        filename = FilePath
        Session(filename) = filename
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

            Dim dr As DataRow
            Dim branch() As String

            For Each r As DataRow In ds.Tables("ImportD365-Payment").Rows
                If r.Item("column25") <> "VOUCHER" Then
                    dr = rvtable.NewRow()
                    dr("voucher") = r.Item("column25")
                    dr("transdate") = r.Item("column23")
                    dr("account") = r.Item("column2")
                    branch = Strings.Split(r.Item("column14"), "-")
                    dr("branch") = branch(3)
                    dr("amount") = r.Item("column5")
                    rvtable.Rows.Add(dr)
                End If
            Next


            Session("rv") = rvtable
            gvData.DataSource = rvtable
            gvData.DataBind()

        End Using
        'getexcelinterop(FilePath)

    End Sub


    Private Function SaveData(iType As Integer) As Boolean

        Dim result As Boolean = True

        Dim scriptKey As String
        Dim javaScript As String


        Dim objrv As New rv
        Try
            For Each row As DataRow In rvtable.Rows
                'objrv.rvtohq(row("voucher"), row("transdate"), row("account"), row("branch"), row("amount"), usercode)
                ' by Pison 20220222 ปรับให้ส่ง voucher แทนที่สาขา เนื่องจาก hq นำสาขาไปไว้ที่ description
                ' ในรายงานให้ดูที่ description ว่ามี RVP ให้นำมาออกรายงานการรับชำระ
                objrv.rvtohq(row("voucher"), row("transdate"), row("account"), row("branch"), row("amount"), usercode, iType)
            Next row
        Catch ex As Exception
            result = False
            scriptKey = "alert"
            javaScript = "alertWarning('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        Return result

    End Function

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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If SaveData(1) = True Then

            Dim ScriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('บันทึกเรียบร้อย');"
            ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
        End If

    End Sub

    Private Sub btnSaveGSM_Click(sender As Object, e As EventArgs) Handles btnSaveGSM.Click
        If SaveData(2) = True Then

            Dim ScriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('บันทึกเรียบร้อย');"
            ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
        End If
    End Sub

    'Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
    '    Try

    '        Dim csvPath As String = Server.MapPath("~/temp/") + Path.GetFileName(FileUpload2.PostedFile.FileName)
    '        FileUpload2.SaveAs(csvPath)

    '        rvtable = creatervtable()

    '        Dim csvData As String = File.ReadAllText(csvPath)
    '        Dim dr As DataRow

    '        For Each row As String In csvData.Split(ControlChars.Lf)
    '            If Not String.IsNullOrEmpty(row) Then
    '                dr = rvtable.NewRow()
    '                Dim i As Integer = 1
    '                For Each cell As String In row.Split(New Char() {vbTab})
    '                    'dt.Rows(dt.Rows.Count - 1)(i) = cell
    '                    Select Case i
    '                        Case 1
    '                            dr("voucher") = cell
    '                        Case 2
    '                            dr("account") = cell
    '                        Case 3
    '                        Case 4
    '                            dr("amount") = cell
    '                        Case 5
    '                        Case 6
    '                        Case 7
    '                            dr("branch") = cell
    '                        Case 8
    '                            dr("transdate") = CDate(cell)
    '                    End Select
    '                    i += 1
    '                Next
    '                rvtable.Rows.Add(dr)
    '            End If
    '        Next

    '        Session("rv") = rvtable
    '        gvData.DataSource = rvtable
    '        gvData.DataBind()

    '    Catch ex As Exception
    '        Dim err, scriptKey, javaScript As String
    '        err = ex.Message
    '        scriptKey = "UniqueKeyForThisScript"
    '        javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '    End Try
    'End Sub
End Class