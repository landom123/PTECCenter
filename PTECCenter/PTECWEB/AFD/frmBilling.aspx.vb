Imports System.IO
Imports ClosedXML.Excel

Public Class frmBilling
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public username As String
    Public usercode As String

    Public mydatatable As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objjob As New jobs
        Dim objsupplier As New Supplier
        Dim objetax As New BPETAX

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")


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


        If Not IsPostBack() Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable

            objetax.SetCboZone(cboZone)

            Dim mode As String
            Select Case cboZone.SelectedValue
                Case ""
                    mode = "ALL"
                Case Else
                    mode = "Zone"
            End Select

            objetax.SetCboBuilding(cboBranch, mode, cboZone.SelectedValue, "", "", "")



            Dim newListItem As ListItem
            newListItem = New ListItem("-", "")
            cboZone.Items.Insert(0, newListItem)
            cboBranch.Items.Insert(0, newListItem)

            cboZone.SelectedIndex = -1
            cboBranch.SelectedIndex = -1

        Else
            menutable = Session("menulist")
        End If

    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If validatedata() Then

            Dim objetax As New BPETAX
            Dim csvString As String
            Dim filename As String
            Try
                csvString = objetax.ExBillingCSV("0105544048559", "00000", "IV", txtbegindate.Text, txtenddate.Text, cboZone.SelectedValue, cboBranch.SelectedValue, txtEmail.Text.Trim())
                filename = "PTEC_BI_" & Now.ToString("yyMMdd_HHMMss")

                'Dim decode As String
                'Dim b As Byte() = Convert.FromBase64String(encode)
                'decode = System.Text.Encoding.UTF8.GetString(b)

                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".txt")
                Using MyMemoryStream As New MemoryStream()
                    Dim streamWriter = New StreamWriter(MyMemoryStream)
                    streamWriter.Write(csvString)
                    streamWriter.Flush()
                    MyMemoryStream.Position = 0
                    Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(csvString.ToString().Normalize)
                    'MyMemoryStream.WriteTo(Response.OutputStream)
                    Response.Flush()
                    Response.OutputStream.Write(bytes, 0, bytes.Length)
                    Response.End()
                End Using

            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('export fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

        End If

    End Sub

    Private Sub cboZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboZone.SelectedIndexChanged

        Dim objetax As New BPETAX
        Dim mode As String
        Select Case cboZone.SelectedValue
            Case ""
                mode = "ALL"
            Case Else
                mode = "Zone"
        End Select

        objetax.SetCboBuilding(cboBranch, mode, cboZone.SelectedValue, "", "", "")

        Dim newListItem As ListItem
        newListItem = New ListItem("-", "")
        cboBranch.Items.Insert(0, newListItem)

    End Sub

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If txtbegindate.Text.Trim() = "" Then
            result = False
            msg = "กรุณาใส่วันเริ่ม"
            GoTo endprocess
        End If
        If txtenddate.Text.Trim() = "" Then
            result = False
            msg = "กรุณาใส่ถึงวันที่"
            GoTo endprocess
        End If


        'If amountdedusctsell < 0 Then
        '    result = False
        '    msg = "กรุณาใส่จำนวนเต็ม"
        '    GoTo endprocess
        'End If
endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtbegindate.Text = ""
        txtenddate.Text = ""
        txtEmail.Text = ""

        cboZone.SelectedIndex = -1
        cboBranch.SelectedIndex = -1


        Dim objetax As New BPETAX
        Dim mode As String
        Select Case cboZone.SelectedValue
            Case ""
                mode = "ALL"
            Case Else
                mode = "Zone"
        End Select
        objetax.SetCboBuilding(cboBranch, mode, cboZone.SelectedValue, "", "", "")

        Dim newListItem As ListItem
        newListItem = New ListItem("-", "")
        cboBranch.Items.Insert(0, newListItem)

    End Sub
End Class