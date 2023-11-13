

Imports System.IO

Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.Windows
Imports System.Globalization
Imports System.Net.NetworkInformation

Public Class requestcontract
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Public usercode, username

    Dim dtBranch As New DataTable
    Dim dtStatus As New DataTable

    Dim objprj As New Project
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

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

            dtStatus = objprj.loadStatus(1)
            cboStatus.DataSource = dtStatus
            cboStatus.DataValueField = "ID"
            cboStatus.DataTextField = "StatusName"
            cboStatus.DataBind()

        End If

    End Sub


    'Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
    '    gvData.PageIndex = e.NewPageIndex

    'End Sub

    'Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound

    'End Sub

    Protected Sub OnRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvData, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim sDocno As String = gvData.SelectedRow.Cells(0).Text
            'Dim iDocID As String = gvData.SelectedRow.Cells(10).Text
            Dim iDocID As String = gvData.SelectedRow.Cells(11).Text

            Session("DocNo") = sDocno
            Session("iDocID") = iDocID
            Session("smode") = "EDIT"
            Response.Redirect("requestcontract2.aspx")

            'If loadRequest(iDocID) = False Then
            '    Exit Sub
            'End If
            'Dim country As String = gvData.SelectedRow.Cells(1).Text
            'Dim message As String = "Row Index: " & index & "\nName: " & name + "\nCountry: " & country
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" + message + "');", True)
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Session("smode") = "NEW"
        Session("iDocID") = 0
        Response.Redirect("requestcontract2.aspx")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'List()

        Try

            'If cboBranch.SelectedValue.ToUpper = "ALL" Then
            '    List()
            'Else
            '    ListFind(cboBranch.SelectedValue)
            'End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            Dim objReq As New clsRequestContract
            Dim dtinfo As New DateTimeFormatInfo
            Dim dt As New DataTable

            Dim dBegindate, dEnddate, dDueDate As Date

            dDueDate = Date.Now
            'dBegindate = DateAdd(DateInterval.Year, -543, CDate(txtBegindate.Text))
            'dEnddate = DateAdd(DateInterval.Year, -543, CDate(txtEnddate.Text))

            dBegindate = CDate(txtBegindate.Text)
            dEnddate = CDate(txtEnddate.Text)



            dt = objReq.FindRequest(dBegindate.ToString("yyyyMMdd", dtinfo), dEnddate.ToString("yyyyMMdd", dtinfo), 1, txtVendor.Text, txtContractno.Text)

            gvData.DataSource = dt
            gvData.DataBind()


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnReEdit_Click(sender As Object, e As EventArgs) Handles btnReEdit.Click
        Response.Redirect("requesteditcontract.aspx")
    End Sub
    Private Sub btnReNote_Click(sender As Object, e As EventArgs) Handles btnReNote.Click
        Response.Redirect("request2.aspx")
    End Sub

    'Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
    '    Try

    '        If cboBranch.SelectedValue.ToUpper = "ALL" Then
    '            List()
    '        Else
    '            ListFind(cboBranch.SelectedValue)
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub


End Class