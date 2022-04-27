Imports NDbfReader
Imports System.IO
'Imports ClosedXML.Excel
'Imports System.Data.OleDb
Public Class CJImprotDBF
    Inherits System.Web.UI.Page
    'Public cnn As OleDbConnection("provide")
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String
    Public branch As String
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

            If Not (Session("edioil") Is Nothing) Then
                editable = Session("edioil")
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        '1. import file to temp
        Dim scriptKey As String
        Dim javaScript As String
        Dim result As String
        result = ImportToTemp()
        If Not String.IsNullOrEmpty(result) Then
            scriptKey = "alert"
            javaScript = "alertWarning('" & result & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
        '3. import to sql temp

error_handler:
    End Sub
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


    Private Function ImportToTemp() As String
        Dim result As String = ""
        '1. GNDITM
        filename = Server.MapPath("~\temp\" & FileUploadGNDITEM.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & "_GNDITEM" & Guid.NewGuid().ToString & Path.GetExtension(filename))

        If Me.FileUploadGNDITEM.HasFile Then
            If Strings.UCase(Path.GetFileName(filename)) = "GNDITEM.DBF" Then
                FileUploadGNDITEM.SaveAs(tempname) 'save file to web server temp
                txtGNDITEM.Text = tempname.ToString

                'import dbf
                Dim gnditemtable As Table
                gnditemtable = Table.Open(tempname)
                Dim gnditem As DataTable
                gnditem = gnditemtable.AsDataTable()
                'insert to sql
                InsertGNDITEM(gnditem)
                'remove file
                'IO.File.Delete(tempname)
            Else
                'error
                result = "ต้องเลือกไฟล์ GNDITM.DBF เสมอ"
                GoTo error_handler
            End If
        Else
            result = "ต้องเลือกไฟล์ GNDITM.DBF เสมอ"
            GoTo error_handler
        End If


        '2. GNDTndr
        filename = Server.MapPath("~\temp\" & FileUploadGNDTndr.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & "_GNDTndr" & Guid.NewGuid().ToString & Path.GetExtension(filename))

        If Me.FileUploadGNDTndr.HasFile Then
            If Strings.UCase(Path.GetFileName(filename)) = "GNDTNDR.DBF" Then
                FileUploadGNDTndr.SaveAs(tempname) 'save file to web server temp
                txtGNDTndr.Text = tempname.ToString

                'import dbf
                Dim gndtndrtable As Table
                gndtndrtable = Table.Open(tempname)
                Dim gndtndr As DataTable
                gndtndr = gndtndrtable.AsDataTable()
                'insert to sql
                InsertGNDTndr(gndtndr)
                'remove file
                'IO.File.Delete(tempname)
            Else
                result = "ต้องเลือกไฟล์ GNDTndr.DBF เสมอ"
                GoTo error_handler
            End If

        Else
            result = "ต้องเลือกไฟล์ GNDTndr.DBF เสมอ"
            GoTo error_handler
        End If
        '3. ITM
        filename = Server.MapPath("~\temp\" & FileUploadITM.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Guid.NewGuid().ToString & Path.GetExtension(filename))

        If Me.FileUploadITM.HasFile Then
            If Path.GetFileName(filename) = "ITM.DBF" Then
                FileUploadITM.SaveAs(tempname) 'save file to web server temp
                txtITM.Text = tempname.ToString
            End If

            'import dbf
            Dim itmtable As Table
            itmtable = Table.Open(tempname)
            Dim itm As DataTable
            itm = itmtable.AsDataTable()
            'insert to sql
            InsertITM(itm)
            'remove file
            'IO.File.Delete(tempname)

        End If
        '4. CAT
        filename = Server.MapPath("~\temp\" & FileUploadCAT.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Guid.NewGuid().ToString & Path.GetExtension(filename))

        If Me.FileUploadCAT.HasFile Then
            If Path.GetFileName(filename) = "CAT.DBF" Then
                FileUploadCAT.SaveAs(tempname) 'save file to web server temp
                txtCAT.Text = tempname.ToString

                'import dbf
                Dim cattable As Table
                cattable = Table.Open(tempname)
                Dim cat As DataTable
                cat = cattable.AsDataTable()
                'insert to sql
                InsertCAT(cat)
                'remove file
                'IO.File.Delete(tempname)
            End If


        End If
        '''''''''''5. MOD
        ''''''''''filename = Server.MapPath("~\temp\" & FileUploadMOD.FileName)
        ''''''''''tempname = Server.MapPath("~\temp\" & usercode & Guid.NewGuid().ToString & Path.GetExtension(filename))

        ''''''''''If Me.FileUploadMOD.HasFile Then
        ''''''''''    If Path.GetFileName(filename) = "MOD.DBF" Then
        ''''''''''        FileUploadMOD.SaveAs(tempname) 'save file to web server temp
        ''''''''''        txtMOD.Text = tempname.ToString

        ''''''''''        'import dbf
        ''''''''''        Dim modtable As Table
        ''''''''''        modtable = Table.Open(tempname)
        ''''''''''        Dim mod_table As DataTable
        ''''''''''        mod_table = modtable.AsDataTable()
        ''''''''''        'insert to sql

        ''''''''''        'remove file
        ''''''''''        IO.File.Delete(tempname)

        ''''''''''    End If


        ''''''''''End If
        '''''''''''6. PRO
        ''''''''''filename = Server.MapPath("~\temp\" & FileUploadPRO.FileName)
        ''''''''''tempname = Server.MapPath("~\temp\" & usercode & Guid.NewGuid().ToString & Path.GetExtension(filename))

        ''''''''''If Me.FileUploadPRO.HasFile Then
        ''''''''''    If Path.GetFileName(filename) = "PRO.DBF" Then
        ''''''''''        FileUploadPRO.SaveAs(tempname) 'save file to web server temp
        ''''''''''        txtPRO.Text = tempname.ToString

        ''''''''''        'import dbf
        ''''''''''        Dim protable As Table
        ''''''''''        protable = Table.Open(tempname)
        ''''''''''        Dim pro As DataTable
        ''''''''''        pro = protable.AsDataTable()
        ''''''''''        'insert to sql

        ''''''''''        'remove file
        ''''''''''        IO.File.Delete(tempname)

        ''''''''''    End If


        ''''''''''End If
        '7. TDR
        filename = Server.MapPath("~\temp\" & FileUploadTDR.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Guid.NewGuid().ToString & Path.GetExtension(filename))

        If Me.FileUploadTDR.HasFile Then
            If Path.GetFileName(filename) = "TDR.DBF" Then
                FileUploadTDR.SaveAs(tempname) 'save file to web server temp
                txtTDR.Text = tempname.ToString

                'import dbf
                Dim tdrtable As Table
                tdrtable = Table.Open(tempname)
                Dim tdr As DataTable
                tdr = tdrtable.AsDataTable()
                'insert to sql
                InsertTDR(tdr)
                'remove file
                'IO.File.Delete(tempname)
            End If
            result = "บันทึกข้อมูลเรียบร้อย"

        End If
error_handler:
        Return result
    End Function

    Private Sub InsertGNDITEM(mytable As DataTable)
        Dim branch As String, check As Double, item As Double,
            parent As Double, category As Double, mode As Integer,
            hour As Integer, minute As Integer, price As Double,
            dob As DateTime, sysdate As DateTime, quantity As Integer,
            discpric, entryid As Double
        Dim cjobj As New CJ
        For Each row As DataRow In mytable.Rows
            entryid = row("entryid")
            branch = usercode
            check = row("check")
            item = row("item")
            parent = row("parent")
            category = row("category")
            mode = row("mode")
            hour = row("hour")
            minute = row("minute")
            price = row("price")
            dob = row("dob")
            sysdate = row("sysdate")
            quantity = row("quantity")
            discpric = row("discpric")
            Try
                cjobj.InsertGNDITEM(entryid, branch, check, item, parent, category, mode, hour, minute,
                                    price, dob, sysdate, quantity, discpric, usercode)
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub InsertGNDTndr(mytable As DataTable)
        Dim branch As String, check As Double, datadate As DateTime, sysdate As DateTime,
            type As Integer, typeid As Integer, amount As Double, id As Double
        Dim cjobj As New CJ
        For Each row As DataRow In mytable.Rows
            branch = usercode
            check = row("check")
            datadate = row("date")
            sysdate = row("sysdate")
            type = row("type")
            typeid = row("typeid")
            amount = row("amount")
            id = row("id")

            Try
                cjobj.InsertGNDTndr(branch, check, datadate, sysdate, type, typeid, amount, id, usercode)
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub InsertCAT(mytable As DataTable)
        Dim branch As String, id As Double, name As String,
            desc As String
        Dim cjobj As New CJ
        For Each row As DataRow In mytable.Rows
            branch = usercode
            id = row("id")
            name = row("name")
            Try
                desc = row("desc")
            Catch ex As Exception
                desc = ""
            End Try

            Try
                cjobj.InsertCAT(branch, id, name, desc, usercode)
            Catch ex As Exception

            End Try

        Next
    End Sub


    Private Sub InsertITM(mytable As DataTable)
        Dim branch As String, ID As Double, shortname As String,
            chitname As String, longname As String, Price As Double
        Dim cjobj As New CJ
        For Each row As DataRow In mytable.Rows
            branch = usercode
            id = row("id")
            shortname = row("shortname")
            chitname = row("chitname")
            longname = row("longname")
            Price = row("price")

            Try
                cjobj.InsertITM(branch, ID, shortname, chitname, longname, Price, usercode)
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub InsertTDR(mytable As DataTable)
        Dim id As Double, name As String, cash As String
        Dim cjobj As New CJ
        For Each row As DataRow In mytable.Rows
            Branch = usercode
            id = row("id")
            name = row("name")
            cash = row("cash")
            Try
                cjobj.InsertTDR(branch, id, name, cash, usercode)
            Catch ex As Exception

            End Try

        Next
    End Sub

End Class