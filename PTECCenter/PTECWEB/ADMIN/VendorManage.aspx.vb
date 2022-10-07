Imports System.IO
Imports ExcelDataReader

Public Class VendorManage
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public usercode, username, dep As String
    Public dtSource As DataTable
    Public dtDestination As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usercode = Session("usercode")
        username = Session("username")
        dep = Session("dep")

        Dim objsupplier As New Supplier

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        If Session("menulist") Is Nothing Then
            If Not String.IsNullOrEmpty(usercode) Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            End If
        Else
            menutable = Session("menulist")
        End If

        If Not IsPostBack() Then
            dtSource = objsupplier.vendor_list("")
            dtSource.Rows.Remove(dtSource.Rows(0))
            BindDataSource()
        End If
    End Sub
    Private Sub BindDataSource()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        'setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        'cntdt = itemtable.Rows.Count
        gvSource.DataSource = dtSource
        gvSource.DataBind()
    End Sub
    Private Sub gvSource_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSource.RowDataBound
        'Dim statusAt As Integer = 9
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    If Data.Item("statusnonpo") = "รอยืนยัน" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
        '    ElseIf Data.Item("statusnonpo") = "ยกเลิก" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
        '    ElseIf Data.Item("statusnonpo") = "รอตรวจสอบ" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.LightGoldenrodYellow
        '    ElseIf Data.Item("statusnonpo") = "รออนุมัติ" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
        '    ElseIf Data.Item("statusnonpo") = "ชำระเงินเสร็จสิ้น" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
        '    ElseIf Data.Item("statusnonpo") = "ไม่ผ่านการอนุมัติ" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
        '    ElseIf Data.Item("statusnonpo") = "รอการเงินตรวจสอบ" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.LightCoral
        '    ElseIf Data.Item("statusnonpo") = "รอบัญชีตรวจสอบ" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.LightSalmon
        '    ElseIf Data.Item("statusnonpo") = "รอเคลียร์ค้างชำระ" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.Brown
        '        e.Row.Cells.Item(statusAt).ForeColor = Color.White
        '    ElseIf Data.Item("statusnonpo") = "ขอเอกสารเพิ่มเติม" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.MediumPurple
        '    ElseIf Data.Item("statusnonpo") = "ได้รับเอกสารตัวจริง" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.Gray
        '    ElseIf Data.Item("statusnonpo") = "รอเอกสารตัวจริง" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.Yellow
        '    ElseIf Data.Item("statusnonpo") = "การเงินได้รับเอกสาร" Then
        '        e.Row.Cells.Item(statusAt).BackColor = Color.Gray
        '
        '    End If
        'End If
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.ServerClick
        Dim filename As String

        'Dim dateEng As DateTime = Convert.ToDateTime(myDateTime, _cultureEnInfo)
        'Me.lblEng.Text = dateEng.ToString("dd MMM yyyy", _cultureEnInfo)

        filename = Server.MapPath("~\temp\" & FileUpload1.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Now.ToString("yyyyMMddhhmmss", _cultureEnInfo) & Path.GetExtension(filename))
        'filename = Path.GetFileName(FileUpload1.FileName)
        If Me.FileUpload1.HasFile Then
            'Import_To_Grid(filename, ".xlsx", "No")
            Try
                FileUpload1.SaveAs(tempname)
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('SaveAs fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
            Import_To_Grid(tempname, Path.GetExtension(filename))
        End If
    End Sub
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        filename = FilePath
        Session(filename) = filename
        Try
            'For Each row As DataRow In detailtable.Rows
            'objgsm.Deduct_Save(row("branch"), row("detail"), row("amount"), row("date"), usercode)
            'Next row
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

                ds.Tables(0).Rows.RemoveAt(0) 'remove row name colum

                'Dim dr As DataRow
                'Dim dt_temp As DataTable = createdetailtable()
                'For Each r As DataRow In ds.Tables(0).Rows
                '    dr = dt_temp.NewRow()
                '    dr("approvalid") = cboApproval.Items.FindByText(r.Item("column0")).Value
                '    dr("approval") = r.Item("column0")
                '    dr("branchid") = cboBranch.Items.FindByText(r.Item("column1")).Value
                '    dr("branch") = r.Item("column1")
                '    dr("cost") = r.Item("column2")
                '    dr("detail") = r.Item("column3")
                '    dt_temp.Rows.Add(dr)

                'Next

                'Merge(dt_temp)
                'GetData()
                'BindData()

            End Using
            'getexcelinterop(FilePath)

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('upload fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    'Private Sub Merge(dt_temp As DataTable)

    '    Dim dr As DataRow
    '    For Each r As DataRow In dt_temp.Rows
    '        Dim cntrow As Integer = detailtable.Rows.Count + 1
    '        While detailtable.Rows.IndexOf(detailtable.Select("id='" & cntrow & "'").FirstOrDefault()) > -1
    '            cntrow += 1
    '        End While

    '        dr = detailtable.NewRow()
    '        dr("id") = cntrow
    '        dr("approvalid") = r.Item("approvalid")
    '        dr("approval") = r.Item("approval")
    '        dr("branchid") = r.Item("branchid")
    '        dr("branch") = r.Item("branch")
    '        dr("cost") = r.Item("cost")
    '        dr("detail") = r.Item("detail")
    '        detailtable.Rows.Add(dr)

    '    Next
    'End Sub
End Class