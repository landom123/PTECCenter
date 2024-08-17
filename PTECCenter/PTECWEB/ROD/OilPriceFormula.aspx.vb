Public Class OilPriceFormula
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public pricetable As DataTable = createpricetable()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier
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


        If IsPostBack() Then
            Dim a
        Else
            Find()
        End If

    End Sub


    Private Function createpricetable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("product_name", GetType(String))
        dt.Columns.Add("product_code", GetType(String))
        dt.Columns.Add("mainpriceid", GetType(Double))
        dt.Columns.Add("effectivedate", GetType(Date))
        dt.Columns.Add("lastprice", GetType(Double))
        dt.Columns.Add("movevalue", GetType(Double))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(Date))
        dt.Columns.Add("adjust", GetType(Double))
        dt.Columns.Add("actual", GetType(Double))
        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = pricetable
        gvData.DataBind()
    End Sub


    Private Sub Find()
        Dim selectdate As String
        Dim objprice As New Price
        selectdate = "" 'txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        pricetable = objprice.Oil_Day_Price_Branch_Formula_List(selectdate)
        Session("pricetable") = pricetable
        BindData()
    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        'Dim txtadjust As TextBox = CType(e.Row.FindControl("txtadjust"), TextBox)
        'If Not IsNothing(txtadjust) Then
        '    txtadjust.Text = e.Row.DataItem("adjust")
        'End If
        '*** vbs_whtaxid ***'
        'Dim txtactual As TextBox = CType(e.Row.FindControl("txtactual"), TextBox)
        'If Not IsNothing(txtactual) Then
        '    txtactual.Text = e.Row.DataItem("actual")
        'End If

    End Sub
End Class