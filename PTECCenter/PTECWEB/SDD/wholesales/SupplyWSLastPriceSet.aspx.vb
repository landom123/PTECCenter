
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Imports System.Globalization
Imports System.Windows
Imports System.Diagnostics.Contracts

Public Class SupplyWSLastPriceSet
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public pricetable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public scriptKey As String = "UniqueKeyForThisScript"
    Public javaScript As String
    Public beginpricedate, endpricedate As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        'txtBeginPriceDate.Attributes.Add("readonly", "false")
        'txtEndPriceDate.Attributes.Add("readonly", "false")

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            pricetable = Session("pricetable")
            'BindData()
        Else

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'สร้างตารางมี row ว่าง เพื่อให้ grid แสดง head, footer
            'pricetable.Rows.Add("ESSR", "b5", "b7", "b10", "g91", "g95", "e20", "e85")
            Dim emptytable As DataTable = create()
            Dim R As DataRow = emptytable.NewRow
            emptytable.Rows.Add(R)
            Session("pricetable") = pricetable
            BindData(emptytable)



            'beginpricedate = CDate(txtBeginPriceDate.Text).ToString("yyyyMMdd")
            'endpricedate = CDate(txtEndPriceDate.Text).ToString("yyyyMMdd")


            'FindData(beginpricedate, endpricedate)

        End If
        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "")
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
    Public Sub FindData(beginpricedate As String, endpricedate As String)
        Dim wsobj As New wholesales
        Dim mytable As DataTable

        mytable = wsobj.Wholesales_LastPrice_Find(beginpricedate, endpricedate)
        showdata(mytable)

    End Sub
    Private Sub showdata(mytable As DataTable)
        BindData(mytable)
        'txtremark.Text = mytable.Rows(0).Item("remark")
        'txtPriceDate.Text = Date.Parse(pricedate).ToString("dd/MM/yyyy hh:mm")
        'lblstatus.Text = mytable.Rows(0).Item("status")

        'setbutton(lblstatus.Text)

    End Sub
    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        Dim wsobj As New wholesales
        Dim mytable As New DataTable
        Try
            mytable = wsobj.Wholesales_LastPrice_Find("", "")
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub
    Private Sub setbutton(status As String)
        Select Case status
            Case = "Approve"
                'btnCancel.Enabled = True
            Case = "Pendding"
                'btnCancel.Enabled = True
            Case = "Cancel"
                'btnCancel.Enabled = False
        End Select
    End Sub
    Public Sub PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub
    Private Sub BindData(mytable As DataTable)
        gvPrice.DataSource = mytable
        gvPrice.DataBind()
    End Sub
    Private Function create() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("pricedate", GetType(Date))
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("Status", GetType(String))
        dt.Columns.Add("g91price", GetType(Double))
        dt.Columns.Add("g95price", GetType(Double))
        dt.Columns.Add("e20price", GetType(Double))
        dt.Columns.Add("e85price", GetType(Double))
        dt.Columns.Add("b5price", GetType(Double))
        dt.Columns.Add("b7price", GetType(Double))
        dt.Columns.Add("b10price", GetType(Double))
        dt.Columns.Add("remark", GetType(String))

        Return dt
    End Function

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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("SupplyWSPriceSet.aspx")
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            FindData(CDate(txtBeginPriceDate.Text).ToString("yyyyMMdd"), CDate(txtEndPriceDate.Text).ToString("yyyyMMdd"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class