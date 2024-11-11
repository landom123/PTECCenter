Imports System.IO

Imports ClosedXML.Excel

Public Class SupplyWSQuotationReport
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public editable As DataTable
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm
        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If
        usercode = Session("usercode")
        username = Session("username")
        txtBegindate.Attributes.Add("readonly", "readonly")
        txtEnddate.Attributes.Add("readonly", "readonly")
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
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim begindate, enddate As String
        Dim objedi As New wholesales

        begindate = txtBegindate.Text.Substring(6, 4) & txtBegindate.Text.Substring(3, 2) & txtBegindate.Text.Substring(0, 2)
        enddate = txtEnddate.Text.Substring(6, 4) & txtEnddate.Text.Substring(3, 2) & txtEnddate.Text.Substring(0, 2)
        editable = objedi.Wholesales_Quotation_Report(begindate, enddate)
        ExportToExcel(editable, begindate, enddate)

    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, begindate As String, enddate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "Quotation")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Quotation_" & begindate & "_" & enddate & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub

End Class