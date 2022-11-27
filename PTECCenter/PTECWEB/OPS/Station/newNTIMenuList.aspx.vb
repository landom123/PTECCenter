Imports System.Drawing
Imports System.IO
Imports ClosedXML.Excel

Public Class newNTIMenuList
    Inherits System.Web.UI.Page

    Public itemtable As DataTable

    Public criteria As DataTable '= createCriteria()
    Public menutable As DataTable
    Public cntdt As Integer

    'Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objNonpo As New NonPO
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier
        Dim objcompany As New Company
        'Dim objjob As New jobs
        Dim usercode As String
        usercode = Session("usercode")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


        'txtStartDate.Attributes.Add("readonly", "readonly")
        'txtEndDate.Attributes.Add("readonly", "readonly")
        'txtStartDueDate.Attributes.Add("readonly", "readonly")
        'txtEndDueDate.Attributes.Add("readonly", "readonly")


        'operator_code = objNonpo.NonPOPermisstionOperator("PAY")

        If Not IsPostBack() Then
            searchjobslist()


        Else

            criteria = Session("criteria_joblist")
            itemtable = Session("joblist")
        End If
    End Sub
    Private Sub searchjobslist()

        Dim objnewnti As New NTI
        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try

            itemtable = objnewnti.NewNTI_List()

            Session("joblist") = itemtable
            BindData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim res As String = Regex.Replace(ex.ToString, "[^A-Za-z0-9\-/]", "")
            'Dim javaScript As String = "alertWarning('" + res + "');"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub BindData()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        'setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        cntdt = itemtable.Rows.Count
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchjobslist()

    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 7
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("Statusname") = "รอตรวจสอบข้อมูล" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("Statusname") = "ดำเนินการเสร็จสิ้น" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("Statusname") = "ไม่ผ่านการประเมิน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            Else
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            End If

        End If


    End Sub

    'Private Sub cboWorking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorking.SelectedIndexChanged

    '    Dim objNonpo As New NonPO

    '    Try
    '        'itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, cboWorking.SelectedItem.Value)
    '        itemtable = objNonpo.PaymentList_For_Owner(Session("userid"), cboWorking.SelectedItem.Value)
    '        Session("joblist") = itemtable

    '    Catch ex As Exception
    '        Dim scriptKey As String = "alert"
    '        Dim javaScript As String = "alertWarning('search fail');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '    End Try
    '    BindData()
    'End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & Date.Now.ToString
            Dim encode As String
            'If (maintable.Rows(0).Item("statusid") = 7) Then
            '    encode = "(preview)"
            'Else
            '    encode = "(final)"
            'End If
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(filename)
            encode = Convert.ToBase64String(byt)

            'Dim decode As String
            'Dim b As Byte() = Convert.FromBase64String(encode)
            'decode = System.Text.Encoding.UTF8.GetString(b)

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & encode & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim createdate As String
        createdate = Date.Now
        Dim objnonpo As New NonPO
        Try
            ExportToExcel(itemtable, Session("usercode"), createdate)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub


End Class