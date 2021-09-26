Imports ClosedXML.Excel
Imports ExcelDataReader
Imports System.IO

Public Class DeducttoNEWGSM

    Inherits System.Web.UI.Page
    Public menutable, rvtable As DataTable
    Public mydataset As DataSet
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")

        'Dim objsupplier As New Supplier
        filename = Session("filename")
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
        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("detail", GetType(String))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("date", GetType(DateTime))
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

            For Each r As DataRow In ds.Tables(0).Rows

                dr = rvtable.NewRow()
                    dr("branch") = r.Item("column0")
                    dr("detail") = r.Item("column1")
                    dr("amount") = r.Item("column2")
                    dr("date") = r.Item("column3")
                    rvtable.Rows.Add(dr)

            Next


            Session("rv") = rvtable
            gvData.DataSource = rvtable
            gvData.DataBind()

        End Using
        'getexcelinterop(FilePath)

    End Sub


    Private Function SaveData() As Boolean

        Dim result As Boolean = True

        Dim scriptKey As String
        Dim javaScript As String


        Dim objgsm As New gsm
        Try
            For Each row As DataRow In rvtable.Rows
                objgsm.Deduct_Save(row("branch"), row("detail"), row("amount"), row("date"), usercode)
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
        If SaveData() = True Then

            Dim ScriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('บันทึกเรียบร้อย');"
            ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
        End If

    End Sub
End Class