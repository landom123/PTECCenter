Imports System.IO

Imports ClosedXML.Excel

Public Class OthersTTCosttoD365
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")
        txtCloseDate.Attributes.Add("readonly", "readonly")
        txtDueDate.Attributes.Add("readonly", "readonly")
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
            'Dim oauth2url, clientid, clientsecret, serverurl As String
            'Dim objedi As New EDI
            'Dim objeditod365 As New EDItoD365.EDItoD365
            'Dim objtoken As New D365Json.Token
            'Dim token, tokenjson As String


            'objeditod365.getD365CnnInfoUAT(WebConfigurationManager.ConnectionStrings("cnnstr_datacenter").ConnectionString)
            'oauth2url = objeditod365.oauth2url
            'clientid = objeditod365.clientid
            'clientsecret = objeditod365.clientsecret
            'serverurl = objeditod365.serverurl

            ''Dim CloseDate As String = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)

            ''1. get token
            'tokenjson = objtoken.getToken(oauth2url, clientid, clientsecret, serverurl)
            'Dim result = JsonConvert.DeserializeObject(tokenjson)
            'token = result("access_token")

            'lbltoken.Text = token
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("OTT") Is Nothing) Then
                editable = Session("OTT")
                BindData()
            End If
        End If

    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable

        'dt.Columns.Add("billing_type", GetType(String))
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

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim InvoiceDate As String
        Dim objedi As New EDI

        InvoiceDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        editable = objedi.Supply_OtherTT_List_forD365(InvoiceDate)
        Session("OTT") = editable
        BindData()
    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Dim oauth2url, clientid, clientsecret, serverurl As String
        Dim chkerror As Boolean = False
        Dim objedi As New EDI
        Dim mydataset As DataSet
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String

        Dim invoicedate, duedate As String
        invoicedate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        Try
            duedate = txtDueDate.Text.Substring(6, 4) & txtDueDate.Text.Substring(3, 2) & txtDueDate.Text.Substring(0, 2)
        Catch ex As Exception
            duedate = ""
        End Try

        If txtDueDate.Text <> "" Then

            'For i = 0 To gvData.Rows.Count - 1
            '    Dim row As GridViewRow = gvData.Rows(i)



            Try
                '2. save data and get json
                objedi.EDI_Other_TTCost_Save_D365(invoicedate, duedate)

                'javaScript = "<script type='text/javascript'>msgalert('บันทึกเรียบร้อย');</script>"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            Catch ex As Exception
                chkerror = True
                javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

            End Try

            'Next
            If chkerror = False Then
                mydataset = objedi.EDI_Other_TTCOST_Data_for_D365(invoicedate, invoicedate)
                ExportToExcel(mydataset, "OTT", invoicedate)
            End If
        Else
            javaScript = "<script type='text/javascript'>msgalert('กรุณาระบุ Duedate');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub
    Private Sub ExportToExcel(mydataset As DataSet, branch As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydataset.Tables(0), "TTCost")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & branch & "_" & closedate & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub
    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        If Not IsNothing(chk) Then
            chk.Checked = e.Row.DataItem("chk")
        End If

    End Sub

    'Private Sub btnselectall_Click(sender As Object, e As EventArgs) Handles btnselectall.Click
    '    Dim chk As CheckBox
    '    For Each row As GridViewRow In gvData.Rows

    '        chk = CType(row.FindControl("chk"), CheckBox)
    '        chk.Checked = True
    '    Next

    'End Sub

    'Private Sub btnunselect_Click(sender As Object, e As EventArgs) Handles btnunselect.Click
    '    Dim chk As CheckBox
    '    For Each row As GridViewRow In gvData.Rows

    '        chk = CType(row.FindControl("chk"), CheckBox)
    '        chk.Checked = False
    '    Next
    'End Sub
End Class