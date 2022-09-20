
Imports System.IO
Imports ExcelDataReader
Imports ClosedXML.Excel
Public Class SupplyWSPriceSet
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public pricetable As DataTable = create()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public scriptKey As String = "UniqueKeyForThisScript"
    Public javaScript As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        usercode = Session("usercode")
        username = Session("username")

        'txtSaleDate.Attributes.Add("readonly", "true")
        txtPriceDate.Attributes.Add("readonly", "true")
        'Dim objsupplier As New Supplier

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


        End If

    End Sub

    Public Sub PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Private Sub BindData(mytable As DataTable)
        gvPrice.DataSource = mytable
        gvPrice.DataBind()
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        Dim wsobj As New wholesales
        Dim mytable As New DataTable
        Try
            mytable = wsobj.Wholesales_Terminal_List()
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "")
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

        Dim cbo As DropDownList = TryCast(gvPrice.FooterRow.FindControl("cboTerminal"), DropDownList)
        cbo.DataSource = mytable
        cbo.DataTextField = "name"
        cbo.DataValueField = "wsvendid"
        cbo.DataBind()

    End Sub


    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("terminal", GetType(String))
        dt.Columns.Add("b5", GetType(String))
        dt.Columns.Add("b7", GetType(String))
        dt.Columns.Add("b10", GetType(String))
        dt.Columns.Add("g91", GetType(String))
        dt.Columns.Add("g95", GetType(String))
        dt.Columns.Add("e20", GetType(String))
        dt.Columns.Add("e85", GetType(String))

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


    Public Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim pricedate As DateTime
        Try
            pricedate = DateTime.Parse(txtPriceDate.Text)
        Catch ex As Exception
            result = False
            msg = "วัน เวลา ที่มีผลไม่ถูกต้อง"
            GoTo error_handler
        End Try

error_handler:
        If result = False Then
            javaScript = "<script type='text/javascript'>msgalert('" & msg & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
        Return result
    End Function
    Public Sub clear()

    End Sub

    Protected Sub gvPrice_PageIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub BtnAdd(sender As Object, e As EventArgs)
        'pricetable.Rows.Add("ESSR2", "b5", "b7", "b10", "g91", "g95", "e20", "e85")
        'pricetable.Rows.Add("ESSR3", "b5", "b7", "b10", "g91", "g95", "e20", "e85")
        Dim cbo As DropDownList = TryCast(gvPrice.FooterRow.FindControl("cboTerminal"), DropDownList)
        Dim b5 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtB5"), TextBox)
        Dim b7 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtb7"), TextBox)
        Dim b10 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtb10"), TextBox)
        Dim g91 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtg91"), TextBox)
        Dim g95 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txtg95"), TextBox)
        Dim e20 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txte20"), TextBox)
        Dim e85 As TextBox = TryCast(gvPrice.FooterRow.FindControl("txte85"), TextBox)

        Dim R As DataRow = pricetable.NewRow
        R("terminal") = cbo.SelectedItem.Text
        R("b5") = b5.Text
        R("b7") = b7.Text
        R("b10") = b10.Text
        R("g91") = g91.Text
        R("g95") = g95.Text
        R("e20") = e20.Text
        R("e85") = e85.Text

        pricetable.Rows.Add(R)

        Session("pricetable") = pricetable
        BindData(pricetable)
    End Sub

End Class