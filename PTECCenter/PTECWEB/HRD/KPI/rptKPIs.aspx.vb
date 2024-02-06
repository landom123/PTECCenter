Imports System.IO
Imports ClosedXML.Excel

Public Class rptKPIs
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public username As String
    Public usercode As String

    'Public mydatatable As DataTable '= createmydatatable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objKpi As New Kpi
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


        If Not IsPostBack() Then

            objKpi.SetCboPeriod(cboPeriod)

            'objetax.SetCboZone(cboZone)

            'Dim mode As String
            'Select Case cboZone.SelectedValue
            '    Case ""
            '        mode = "ALL"
            '    Case Else
            '        mode = "Zone"
            'End Select

            'objetax.SetCboBuilding(cboBranch, mode, cboZone.SelectedValue, "", "", "")



            'Dim newListItem As ListItem
            'newListItem = New ListItem("-", "")
            'cboZone.Items.Insert(0, newListItem)
            'cboBranch.Items.Insert(0, newListItem)

            'cboZone.SelectedIndex = -1
            'cboBranch.SelectedIndex = -1

        Else
            'mydatatable = Session("mydatatable_kpi")
        End If

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        'If validatedata() Then
        Dim ds As DataSet
        Dim objkpi As New Kpi
        Try
            ds = objkpi.Kpi_Report(cboPeriod.SelectedItem.Value)
            ExportToExcel(ds, Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        'End If

    End Sub

    Private Sub ExportToExcel(ds As DataSet, usercode As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(ds.Tables(0), "KPI_Main")
            wb.Worksheets.Add(ds.Tables(1), "KPI_Ap")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & Date.Now.ToString
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

End Class