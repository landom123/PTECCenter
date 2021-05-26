Public Class UpdateSmileSheetID
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public googlesheettable As DataTable = creategooglesheettable()
    Public usercode, username

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

            googlesheettable = Session("googlesheet")
            BindData()
        Else

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            'If Not (Session("googlesheet") Is Nothing) Then
            googlesheettable = getData()
            Session("googlesheet") = googlesheettable
            BindData()
            'End If
        End If

    End Sub

    Private Function getData() As DataTable
        Dim result As DataTable = Nothing
        Dim objsmile As New Smile
        Try
            result = objsmile.GoogleSheetList
        Catch ex As Exception

        End Try


        Return result
    End Function
    Private Function creategooglesheettable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("monthly", GetType(String))
        dt.Columns.Add("sheetid", GetType(String))
        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = googlesheettable
        gvData.DataBind()
    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) 'Handles btnsave.Click

        'Dim begindate As DateTime = DateTime.Parse(txtCloseDate.Text)
        'If begindate.Date >= Now.Date Then
        Save()
        'Else
        '    Dim scriptKey As String = "UniqueKeyForThisScript"
        '    Dim javaScript As String
        '    javaScript = "<script type='text/javascript'>msgalert('ไม่สามารถบันทึกข้อมูลย้อนหลังได้ กรุณาตรวจสอบวันที่');</script>"
        '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        'End If


    End Sub

    Private Sub Save()

    End Sub

    Protected Sub txtadjust_TextChanged(sender As Object, e As EventArgs)

        'Dim adjust As String = CType(sender, TextBox).Text.ToString()

        'Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)

        'Dim price As String = CType(gr.FindControl("lblprice"), Label).Text.ToString()

        'Dim actual As Label = CType(gr.FindControl("lblactual"), Label)
        'actual.Text = Double.Parse(price) + Double.Parse(adjust)

    End Sub

    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        'Dim txtadjust As TextBox = CType(e.Row.FindControl("txtadjust"), TextBox)
        'If Not IsNothing(txtadjust) Then
        '    txtadjust.Text = e.Row.DataItem("adjust")
        'End If
        '''*** vbs_whtaxid ***'
        'Dim txtactual As TextBox = CType(e.Row.FindControl("txtactual"), TextBox)
        'If Not IsNothing(txtactual) Then
        '    txtactual.Text = e.Row.DataItem("actual")
        'End If

    End Sub

    Public Sub gvData_OnRowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "UpdateData" Then
            'googlesheettable.Rows.Add("test", "test")

            'BindData()
        End If
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim objsmile As New Smile
        Try
            objsmile.GoogleSheetSave(txtaddmonthly.Text, txtaddsheetid.Text)
            googlesheettable.Rows.Add(txtaddmonthly.Text, txtaddsheetid.Text)
            Session("googlesheet") = googlesheettable
            BindData()
        Catch ex As Exception
            Dim err As String = ex.Message
            err = Strings.Replace(err, "'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String
            javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

    End Sub
End Class