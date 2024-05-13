Imports System.Globalization
Imports System.IO
Imports ClosedXML.Excel

Public Class AssetsNozzle
    Inherits System.Web.UI.Page
    Public menutable As DataTable

    Public nozzletable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


        If Not IsPostBack Then
            objbranch.SetComboBranch(cboBranch)
            If Session("positionid") = "10" Then
                cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Session("branchid")))

                cboBranch.Attributes.Add("disabled", "True")
                'cboBranch.Visible = False
            End If
            findNozzle(cboBranch.SelectedValue)

        Else
            nozzletable = Session("assetsnozzle")

        End If

        SetBtn(Session("positionid"))
    End Sub

    Private Sub findNozzle(branchid As Integer)
        Dim objassets As New Assets

        Try
            nozzletable = objassets.AssesNozzle_list(branchid)

            Session("assetsnozzle") = nozzletable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        If Not Session("positionid") = "10" Then
            findNozzle(cboBranch.SelectedValue)
        End If
    End Sub

    Private Sub btnAddDetails_Click(sender As Object, e As EventArgs) Handles btnAddDetails.Click
        If Not Session("positionid") = "10" Then
            Dim objassets As New Assets
            Try
                objassets.UpdateDetail(hiddenAdvancedetailid.Value,
                                       txtbrand.Text.ToString,
                                       txtproducttype.Text.ToString,
                                       txtnozzle_no.Text.ToString,
                                       txtpositiononassest.Text.ToString,
                                       txtexpirydate.Text.ToString,
                                       Session("usercode")
                                       )

                findNozzle(cboBranch.SelectedValue)
                Session("assetsnozzle") = nozzletable
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('update fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        End If
    End Sub

    Private Sub btnDowload_Click(sender As Object, e As EventArgs) Handles btnDowload.Click
        Dim createdate As String
        createdate = DateTime.Now()
        Dim objnonpo As New NonPO
        Dim objassets As New Assets
        Try
            If chkNozzle.Checked Then
                'findNozzle(cboBranch.SelectedValue)
                Dim branchid As Integer = cboBranch.SelectedValue
                If branchid = 0 Then
                    branchid = 9999
                End If
                ExportToExcel(objassets.AssesNozzle_Export(branchid), Session("usercode"), createdate)
            ElseIf chkResultTEST.Checked Then
                Dim dt As DataTable
                dt = objassets.Nozzle_Export(cboBranch.SelectedValue)
                ExportToExcel(dt, Session("usercode"), createdate)
            ElseIf chkResultOver.Checked Then
                Dim dt As DataTable
                dt = objassets.NozzleOver_Export(cboBranch.SelectedValue)
                ExportToExcel(dt, Session("usercode"), createdate)
            End If
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & Date.Now.ToString
            Dim encode As String
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(filename)
            encode += Convert.ToBase64String(byt)

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

    Private Sub SetBtn(positionid As Integer)
        If Not positionid = "10" Then

            Dim scriptKey As String = "disable"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "disable();"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

    End Sub
End Class