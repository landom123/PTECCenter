Imports ClosedXML.Excel

Imports System.IO

Public Class newgsmtod365

    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public gsmtable As DataTable = creategsm()
    Public mydataset As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm
        Dim usercode, username
        usercode = Session("usercode")
        username = Session("username")
        txtCloseDate.Attributes.Add("readonly", "readonly")

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

            setCboBranch()

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("newgsmtable") Is Nothing) Then
                gsmtable = Session("newgsmtable")
                BindData()
            End If
        End If

    End Sub
    Private Sub setCboBranch()
        Dim objbranch As New Branch
        objbranch.SetComboBranch(cbobranch)

    End Sub


    Private Function creategsm() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("br_code", GetType(String))
        dt.Columns.Add("br_name", GetType(String))
        dt.Columns.Add("income", GetType(Double))
        dt.Columns.Add("receive", GetType(Double))
        dt.Columns.Add("diff", GetType(Double))
        dt.Columns.Add("link", GetType(String))
        dt.Columns.Add("send", GetType(String))
        dt.Columns.Add("chk", GetType(Boolean))

        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = gsmtable
        gvData.DataBind()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim CloseDate As String
        Dim objgsm As New gsm
        CloseDate = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
        gsmtable = objgsm.NEWGSM_to_D365_byDate(cbobranch.SelectedItem.Value, CloseDate)
        Session("newgsmtable") = gsmtable
        BindData()
    End Sub

    'Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
    '    gvData.PageIndex = e.NewPageIndex
    '    BindData()
    'End Sub

    'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    '    Dim CloseDate As String = txtCloseDate.Text.Substring(6, 4) & txtCloseDate.Text.Substring(3, 2) & txtCloseDate.Text.Substring(0, 2)
    '    For i = 0 To gvData.Rows.Count - 1
    '        Dim row As GridViewRow = gvData.Rows(i)
    '        'Dim id As String = row.Cells(0).Text
    '        Dim chkSelect As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
    '        Dim brcode As Label = CType(row.FindControl("lblbranch"), Label)

    '        If chkSelect.Checked = "True" Then
    '            Dim objgsm As New gsm
    '            Try
    '                objgsm.Send_GSM_to_Batch(brcode.Text, CloseDate, 1, Session("userid"))
    '            Catch ex As Exception

    '            End Try
    '        Else
    '            Dim objgsm As New gsm
    '            Try
    '                objgsm.Send_GSM_to_Batch(brcode.Text, CloseDate, 0, Session("userid"))
    '            Catch ex As Exception

    '            End Try
    '        End If
    '    Next
    'End Sub

    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        'Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        'If Not IsNothing(chk) Then
        '    chk.Checked = e.Row.DataItem("chk")
        'End If
        '*** vbs_whtaxid ***'
        'Dim lblbranch As Label = CType(e.Row.FindControl("lblbranch"), Label)
        'If Not IsNothing(lblbranch) Then
        '    lblbranch.Text = e.Row.DataItem("br_code")
        'End If

    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim objgsm As New gsm
        Dim rowIndex As Integer
        Dim row As GridViewRow
        Dim branch As String
        Dim closedate As String = Strings.Right(txtCloseDate.Text, 4) & Strings.Mid(txtCloseDate.Text, 4, 2) & Strings.Left(txtCloseDate.Text, 2)
        If e.CommandName = "Create" Then
            ''Determine the RowIndex of the Row whose Button was clicked.
            rowIndex = Convert.ToInt32(e.CommandArgument)
            row = gvData.Rows(rowIndex)

            branch = Double.Parse(TryCast(row.FindControl("lblbranch"), Label).Text)
            branch = Strings.Right("000" & branch.ToString, 3)

            Try
                SaveData(branch, closedate)
            Catch ex As Exception
                Dim msg As String = Strings.Replace(ex.Message, "'", " ")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between create data : " & msg & "');", True)
            End Try


        End If

        If e.CommandName = "Download" Then
            rowIndex = Convert.ToInt32(e.CommandArgument)
            row = gvData.Rows(rowIndex)

            branch = Double.Parse(TryCast(row.FindControl("lblbranch"), Label).Text)
            branch = Strings.Right("000" & branch.ToString, 3)

            Try
                SaveData(branch, closedate)

                mydataset = objgsm.GSM_Data_for_D365(branch, closedate)
                ExportToExcel(mydataset, branch, closedate)
            Catch ex As Exception
                Dim msg As String = Strings.Replace(ex.Message, "'", " ")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between create data : " & msg & "');", True)
            End Try


        End If
    End Sub
    Private Sub ExportToExcel(mydataset As DataSet, branch As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydataset.Tables(0), "Saleorder")
            wb.Worksheets.Add(mydataset.Tables(1), "Payment")

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

    Private Sub SaveData(branch As String, closedate As String)
        Dim result As Boolean = True
        Dim objgsm As New gsm
        Dim errstr As String
        'insert ลง

        Try
            objgsm.NEWGSM_Sale_forD365(branch, closedate)
            objgsm.NEWGSM_Payment_forD365(branch, closedate)
        Catch ex As Exception
            errstr = Strings.Replace(ex.Message, "'", "")
            result = False
            ClientScript.RegisterStartupScript(Me.GetType(), errstr, "alert('Error');", True)
        End Try

        If result = True Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Save Complete');", True)
        End If
    End Sub
End Class