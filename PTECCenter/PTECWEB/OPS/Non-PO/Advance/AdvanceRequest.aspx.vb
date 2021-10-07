Imports System.Drawing

Public Class AdvanceRequest
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public cntdt As Integer
    Public sumitem As Double


    Public itemtable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objNonPO As New NonPO
        Dim nonpods = New DataSet

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

        If Not IsPostBack() Then
            detailtable = createtableDetail()


            If Not Request.QueryString("ADV") Is Nothing Then
                Try
                    nonpods = objNonPO.NonPO_AdvanceRQ_Find(Request.QueryString("ADV"))
                    detailtable = nonpods.Tables(0)
                    Session("detailtable") = detailtable


                    If Not Session("status") = "edit" Then
                        Session("status") = "read"
                    End If
                    showdata(detailtable)
                    'chkuser(detailtable.Rows(0).Item("createby"))
                    SetBtn(detailtable.Rows(0).Item("statusrqid"))
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            Else
                Session("status") = "new"
            End If
        Else
            detailtable = Session("detailtable")
        End If

        SetMenu()
    End Sub
    Private Function createtableDetail() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("advancerequestcode", GetType(String))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("statusrqid", GetType(Integer))
        dt.Columns.Add("statusrqname", GetType(String))

        dt.Columns.Add("detail", GetType(String))

        dt.Columns.Add("approvalrqby", GetType(String))
        dt.Columns.Add("approvalrqdate", GetType(String))
        dt.Columns.Add("verifyrqby", GetType(String))
        dt.Columns.Add("verifyrqdate", GetType(String))

        dt.Columns.Add("updateby", GetType(Integer))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(Integer))
        dt.Columns.Add("createdate", GetType(String))

        dt.Columns.Add("updateby_name", GetType(String))
        dt.Columns.Add("createby_name", GetType(String))

        Return dt
    End Function

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdate", GetType(Date))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("closedate", GetType(Date))
        dt.Columns.Add("detailFollow", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Sub SetBtn(statusid As String)
        'in advance use statusrqid 1,2,3,6,11,12
        Select Case statusid
            Case = "1" 'รอยืนยัน
                txtStatusRq.BackColor = Color.LightBlue
                btnConfirm.Visible = True
                btnCancel.Visible = True
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = True
                btnClearAdvance.Visible = False
            Case = "2" 'รออนุมัติ
                txtStatusRq.BackColor = Color.LightYellow
                btnConfirm.Visible = False
                btnCancel.Visible = True
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False

            Case = "3" 'รอการเงินตรวจสอบ
                txtStatusRq.BackColor = Color.LightCoral
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False

            Case = "6" 'ไม่ผ่านการอนุมัติ
                txtStatusRq.BackColor = Color.IndianRed
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False

            Case = "11" 'รอเคลียร์ค้างชำระ
                txtStatusRq.BackColor = Color.Brown
                txtStatusRq.ForeColor = Color.White
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = True

            Case = "12" 'ชำระเงินเสร็จสิ้น
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False
                txtStatusRq.BackColor = Color.GreenYellow
            Case = "14" 'ยกเลิก
                btnConfirm.Visible = False
                btnCancel.Visible = False
                btnClose.Visible = False
                btnAddDoc.Visible = False
                btnEdit.Visible = False
                btnClearAdvance.Visible = False
                txtStatusRq.BackColor = Color.LightGray
        End Select
    End Sub

    Private Sub showdata(mytable As DataTable)
        Dim objbranch As New Branch
        With mytable.Rows(0)
            txtNonPOcode.Text = .Item("advancerequestcode").ToString
            txtStatusRq.Text = .Item("statusrqname").ToString



            txtCreateBy.Text = .Item("createby_name").ToString
            txtDocDate.Text = .Item("createdate").ToString

            If Session("status") = "edit" Then
                txtamount.Attributes.Add("type", "number")
                txtamount.Text = .Item("amount")
            Else
                txtamount.Attributes.Remove("type")
                txtamount.Text = String.Format("{0:n4}", .Item("amount"))
            End If

            txtdetail.Text = .Item("detail").ToString

        End With
    End Sub

    Private Sub SetMenu()
        Select Case Session("status")
            Case = "new"
                lbMandatoryamount.Visible = True
                lbMandatorydetail.Visible = True
                txtamount.ReadOnly = False
                txtdetail.ReadOnly = False
            Case = "read"
                lbMandatoryamount.Visible = False
                lbMandatorydetail.Visible = False
                txtamount.ReadOnly = True
                txtdetail.ReadOnly = True
                searchjobslist()
            Case = "write"
                lbMandatoryamount.Visible = False
                lbMandatorydetail.Visible = False
                txtamount.ReadOnly = True
                txtdetail.ReadOnly = True
                searchjobslist()
            Case = "edit"
                lbMandatoryamount.Visible = True
                lbMandatorydetail.Visible = True
                txtamount.ReadOnly = False
                txtdetail.ReadOnly = False
                searchjobslist()

        End Select
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objNonPO As New NonPO
        Dim dt As DataTable
        Try
            dt = objNonPO.NonPO_AdvanceRequest_Save(txtamount.Text.Trim(), txtdetail.Text.Trim(), Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & dt.Rows(0).Item("code"))
endprocess:
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Session("status") = "edit"
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))
    End Sub

    Private Sub btnCancelEdit_Click(sender As Object, e As EventArgs) Handles btnCancelEdit.Click
        Session("status") = "read"
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))
    End Sub

    Private Sub btnSaveEdit_Click(sender As Object, e As EventArgs) Handles btnSaveEdit.Click
        Dim objnonpo As New NonPO
        Try
            objnonpo.NonPO_AdvanceRequest_Edit(Request.QueryString("ADV").ToString, txtamount.Text.Trim(), txtdetail.Text.Trim(), Session("userid"))
            Session("status") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AdvanceRequest_Confirm(Request.QueryString("ADV").Trim, Session("usercode"))
            Session("status") = "read"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim objnonpo As New NonPO

        Try
            objnonpo.NonPO_AdvanceRequest_Cancel(Request.QueryString("ADV").Trim, Session("usercode"))
            Session("status") = "read"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Advance/AdvanceRequest.aspx?ADV=" & Request.QueryString("ADV"))

endprocess:
    End Sub

    Private Sub btnClearAdvance_Click(sender As Object, e As EventArgs) Handles btnClearAdvance.Click
        Response.Redirect("../Advance/ClearAdvance.aspx?f=ADV&code_ref=" & Request.QueryString("ADV"))

    End Sub
    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 5
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("status") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("status") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("status") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("status") = "ชำระเงินเสร็จสิ้น" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.GreenYellow
            ElseIf Data.Item("status") = "ไม่ผ่านการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("status") = "รอการเงินตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightCoral
            ElseIf Data.Item("status") = "รอเคลียร์ค้างชำระ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Brown
            End If
        End If
    End Sub

    Private Sub searchjobslist()

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            itemtable = objNonPO.AdvanceRQList_For_Owner(Session("userid").ToString, 11)

            Session("joblist") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search advlist fail');"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindData()

        cntdt = itemtable.Rows.Count
        sumitem = Convert.ToDouble(itemtable.Compute("SUM(amount)", String.Empty))
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub
End Class