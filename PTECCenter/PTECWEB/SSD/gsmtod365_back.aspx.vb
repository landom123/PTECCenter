﻿Public Class gsmtod365_back
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public gsmtable As DataTable = creategsm()
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

            If Not (Session("gsmtable") Is Nothing) Then
                gsmtable = Session("gsmtable")
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
        gsmtable = objgsm.Calc_summary_GSM_to_D365_byDate(cbobranch.SelectedItem.Value, CloseDate)
        Session("gsmtable") = gsmtable
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
        Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        If Not IsNothing(chk) Then
            chk.Checked = e.Row.DataItem("chk")
        End If
        '*** vbs_whtaxid ***'
        'Dim lblbranch As Label = CType(e.Row.FindControl("lblbranch"), Label)
        'If Not IsNothing(lblbranch) Then
        '    lblbranch.Text = e.Row.DataItem("br_code")
        'End If

    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim objgsm As New gsm
        If e.CommandName = "Create" Then
            ''Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            ''Reference the GridView Row.
            Dim row As GridViewRow = gvData.Rows(rowIndex)

            'Fetch value of Name.
            Dim branch As String = Double.Parse(TryCast(row.FindControl("lblbranch"), Label).Text)
            branch = Strings.Right("000" & branch.ToString, 3)
            Dim closedate As String = Strings.Right(txtCloseDate.Text, 4) & Strings.Mid(txtCloseDate.Text, 4, 2) & Strings.Left(txtCloseDate.Text, 2)
            Try
                'objgsm.Deduct_Sell_Approve_or_reject(id, "Confirm", "", usercode)
                'ClientScript.RegisterStartupScript(Me.GetType(), "alert" & id, "alert('Send');", True)
                'FindData()
                SaveData(branch, closedate)
            Catch ex As Exception
                Dim msg As String = Strings.Replace(ex.Message, "'", " ")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between send data : " & msg & "');", True)
            End Try


            ''Fetch value of Country.
            'Dim country As String = row.Cells(1).Text


        End If
        If e.CommandName = "Send" Then
            'Dim objexcel As New Excel
            'objexcel.ExportExcel()
        End If
        If e.CommandName = "Download" Then

        End If
    End Sub

    Private Sub SaveData(branch As String, closedate As String)

        Dim objgsm As New gsm
        Dim mydataset As DataSet
        mydataset = objgsm.getGSCbyBranchDate(branch, closedate)
        'insert ลง
        ' gsm_sale_oil--table(0)
        For Each row As DataRow In mydataset.Tables(0).Rows
            objgsm.GSM_save_gsm_sale_oil(row("br_code"), row("closedate"), row("grade_title"), row("volume"), row("new_price"))
        Next row
        ' gsm_sale_other--table(1)
        For Each row As DataRow In mydataset.Tables(1).Rows
            objgsm.GSM_save_gsm_sale_other(row("br_code"), row("closedate"), row("doccode"), row("ar_number"), row("customer_name"),
                    row("product"), row("volume"), row("amount"), row("duedate"))
        Next row
        ' gsm_paid--table(2)
        For Each row As DataRow In mydataset.Tables(2).Rows
            objgsm.GSM_save_gsm_paid(row("br_code"), row("closedate"), row("td_description"), row("amount"))
        Next row

        'นำข้อมูลลง ivzs GSM_Prepair_GSMtoD365
        objgsm.GSM_Prepair_GSMtoD365(branch, closedate)

        'GSM_Payment_Data_for_D365 '002','20210301','20210401' ดึงข้อมูลที่เตรียมไว้ เพื่อเตรียมส่ง json
        'GSM_SaleOrder_Data_for_D365 '111','20210301','20210401'
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Save Complete');", True)
    End Sub
End Class