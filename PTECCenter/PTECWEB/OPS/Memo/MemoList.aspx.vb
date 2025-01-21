Imports System.Drawing
Imports System.IO
Imports ClosedXML.Excel

Public Class MemoList
    Inherits System.Web.UI.Page
    Public itemtable As DataTable = createdetailtable()
    Public criteria As DataTable '= createCriteria()
    Public menutable As DataTable

    Public cntdt As Integer

    Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objNonpo As New NonPO
        Dim objMemo As New Memo
        Dim objsec As New Section
        Dim objcompany As New Company
        'Dim objjob As New jobs
        Dim usercode As String
        usercode = Session("usercode")


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        txtStartDate.Attributes.Add("readonly", "readonly")
        txtEndDate.Attributes.Add("readonly", "readonly")

        operator_code = objNonpo.NonPOPermisstionOperator("MMR")
        If Not IsPostBack() Then
            criteria = createCriteria()
            objMemo.SetCboMemoType_List(cboMemoType)
            objMemo.SetCboMemoStatus_List(cboStatusFollow)
            SetCboUsers(cboCreateby)
            SetCboUsers(cboTo)
            SetCboUsers(cboCc)
            If Session("positionid") = "10" Or Session("positionid") = "15" Then '10 PM , 15 AA
                chkCO.Checked = True
            Else
                chkHO.Checked = True
            End If
            If operator_code.IndexOf(Session("usercode").ToString) > -1 Then

                'objjob.SetCboJobStatusListForReport(cboStatusFollow)

                '------------------------------------
                If Not Session("criteria_memo") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_memo")
                    BindCriteria(criteria)
                    searchjobslist()
                Else
                    searchjobslist()
                End If
            Else
                'approval.SetCboApprovalStatusForOwner(cboWorking)
                'If Not Session("cboWorking_job") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                '    cboWorking.SelectedValue = Session("cboWorking_job")
                'End If
                If Not Session("criteria_memo") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                    criteria = Session("criteria_memo")
                    BindCriteria(criteria)
                    searchjobslist_owner()
                Else
                    searchjobslist_owner()
                End If
            End If

        Else

            criteria = Session("criteria_memo")
            itemtable = Session("memoitem")
        End If
    End Sub
    Private Sub BindData()
        'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
        setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        'End If
        gvRemind.Caption = "ทั้งหมด " & itemtable.Rows.Count & " รายการ"
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub
    Private Sub setCriteria()
        'criteria = createCriteria()
        If Not criteria Is Nothing Then

            criteria.Rows.Clear()
            criteria.Rows.Add(txtMemoCode.Text.Trim(),
                                txtSubject.Text.Trim(),
                                cboMemoType.SelectedItem.Value.ToString,
                                txtStartDate.Text.Trim(),
                                txtEndDate.Text.Trim(),
                                cboStatusFollow.SelectedItem.Value.ToString,
                                cboCreateby.SelectedItem.Value.ToString,
                                cboTo.SelectedItem.Value.ToString,
                                cboCc.SelectedItem.Value.ToString,
                                gvRemind.PageIndex)

            Session("criteria_memo") = criteria
        End If

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("MemoRequest.aspx")
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("jobcode", GetType(String))
        dt.Columns.Add("jobdate", GetType(Date))
        dt.Columns.Add("jobtype", GetType(String))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("StatusName", GetType(String))
        dt.Columns.Add("closedate", GetType(Date))
        dt.Columns.Add("detailFollow", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("link", GetType(String))

        Return dt
    End Function

    Private Function createCriteria() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("txtMemoCode", GetType(String))
        dt.Columns.Add("txtSubject", GetType(String))
        dt.Columns.Add("cboMemoType", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))
        dt.Columns.Add("cboStatusFollow", GetType(String))
        dt.Columns.Add("cboCreateby", GetType(String))
        dt.Columns.Add("cboTo", GetType(String))
        dt.Columns.Add("cboCc", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))

        Return dt
    End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtMemoCode.Text = criteria.Rows(0).Item("txtMemoCode")
            txtSubject.Text = criteria.Rows(0).Item("txtSubject")
            cboMemoType.SelectedValue = criteria.Rows(0).Item("cboMemoType")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")

            cboStatusFollow.SelectedValue = criteria.Rows(0).Item("cboStatusFollow")
            cboCreateby.SelectedValue = criteria.Rows(0).Item("cboCreateby")
            cboTo.SelectedValue = criteria.Rows(0).Item("cboTo")
            cboCc.SelectedValue = criteria.Rows(0).Item("cboCc")


        End If
    End Sub

    Private Sub searchjobslist()

        Dim objMemo As New Memo

        Dim detailtable As New DataTable
        Try
            'If chkCO.Checked Then
            '    itemtable = objNonPO.AdvanceRQList_For_Operator(txtMemoCode.Text.Trim(),
            '                                            txtStartDate.Text.Trim(),
            '                                            txtEndDate.Text.Trim(),
            '                                          cboStatusFollow.SelectedItem.Value.ToString,
            '                                            txtStartDueDate.Text.Trim(),
            '                                            txtEndDueDate.Text.Trim(),
            '                                          "",
            '                                          "",
            '                                            cboCompany.SelectedItem.Value.ToString,
            '                                            cboBranchGroup.SelectedItem.Value.ToString,
            '                                            cboBranch.SelectedItem.Value.ToString,
            '                                            cboCreateby.SelectedItem.Value.ToString,
            '                                            cboOwner.SelectedItem.Value.ToString)
            'ElseIf chkHO.Checked Then
            '    itemtable = objNonPO.AdvanceRQList_For_Operator(txtMemoCode.Text.Trim(),
            '                                            txtStartDate.Text.Trim(),
            '                                            txtEndDate.Text.Trim(),
            '                                          cboStatusFollow.SelectedItem.Value.ToString,
            '                                            txtStartDueDate.Text.Trim(),
            '                                            txtEndDueDate.Text.Trim(),
            '                                            cboDepartment.SelectedItem.Value.ToString,
            '                                            cboSection.SelectedItem.Value.ToString,
            '                                            cboCompany.SelectedItem.Value.ToString,
            '                                          "",
            '                                          "",
            '                                            cboCreateby.SelectedItem.Value.ToString,
            '                                            cboOwner.SelectedItem.Value.ToString)
            'Else
            '    itemtable = objNonPO.AdvanceRQList_For_Operator(txtMemoCode.Text.Trim(),
            '                                            txtStartDate.Text.Trim(),
            '                                            txtEndDate.Text.Trim(),
            '                                          cboStatusFollow.SelectedItem.Value.ToString,
            '                                            txtStartDueDate.Text.Trim(),
            '                                            txtEndDueDate.Text.Trim(),
            '                                            "",
            '                                          "",
            '                                            cboCompany.SelectedItem.Value.ToString,
            '                                          "",
            '                                          "",
            '                                            cboCreateby.SelectedItem.Value.ToString,
            '                                            cboOwner.SelectedItem.Value.ToString)
            'End If


            itemtable = objMemo.MemoList_For_Operator(txtMemoCode.Text.Trim(),
                                                        txtSubject.Text.Trim(),
                                                      cboMemoType.SelectedItem.Value.ToString,
                                                        txtStartDate.Text.Trim(),
                                                        txtEndDate.Text.Trim(),
                                                      cboStatusFollow.SelectedItem.Value.ToString,
                                                        cboCreateby.SelectedItem.Value.ToString,
                                                        cboTo.SelectedItem.Value.ToString,
                                                        cboCc.SelectedItem.Value.ToString)


            Session("memoitem") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub searchjobslist_owner()

        Dim objNonPO As New NonPO
        Dim detailtable As New DataTable
        Try
            'If chkCO.Checked Then
            '    itemtable = objNonPO.AdvanceRQList_For_Owner(txtMemoCode.Text.Trim(),
            '                                            txtStartDate.Text.Trim(),
            '                                            txtEndDate.Text.Trim(),
            '                                          cboStatusFollow.SelectedItem.Value.ToString,
            '                                            txtStartDueDate.Text.Trim(),
            '                                            txtEndDueDate.Text.Trim(),
            '                                            cboCompany.SelectedItem.Value.ToString,
            '                                            cboBranchGroup.SelectedItem.Value.ToString,
            '                                            cboBranch.SelectedItem.Value.ToString,
            '                                            Session("userid"),
            '                                             "CO")
            'ElseIf chkHO.Checked Then
            '    itemtable = objNonPO.AdvanceRQList_For_Owner(txtMemoCode.Text.Trim(),
            '                                            txtStartDate.Text.Trim(),
            '                                            txtEndDate.Text.Trim(),
            '                                          cboStatusFollow.SelectedItem.Value.ToString,
            '                                            txtStartDueDate.Text.Trim(),
            '                                            txtEndDueDate.Text.Trim(),
            '                                            cboCompany.SelectedItem.Value.ToString,
            '                                          "",
            '                                          "",
            '                                            Session("userid"),
            '                                            "HO")
            'Else
            '    itemtable = objNonPO.AdvanceRQList_For_Owner(txtMemoCode.Text.Trim(),
            '                                            txtStartDate.Text.Trim(),
            '                                            txtEndDate.Text.Trim(),
            '                                          cboStatusFollow.SelectedItem.Value.ToString,
            '                                            txtStartDueDate.Text.Trim(),
            '                                            txtEndDueDate.Text.Trim(),
            '                                            cboCompany.SelectedItem.Value.ToString,
            '                                          "",
            '                                          "",
            '                                            Session("userid"),
            '                                            "")
            'End If



            Session("memoitem") = itemtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 9
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("StatusName") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("StatusName") = "รอลงชื่อตรวจสอบ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Thistle
            ElseIf Data.Item("StatusName") = "ผ่านการตรวจสอบแล้ว" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.Green
            ElseIf Data.Item("StatusName") = "ไม่ผ่านการอนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("StatusName") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            End If

        End If
    End Sub
    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtMemoCode.Text = ""
        txtSubject.Text = ""
        cboMemoType.SelectedIndex = -1
        txtStartDate.Text = ""
        txtEndDate.Text = ""


        cboStatusFollow.SelectedIndex = -1
        cboCreateby.SelectedIndex = -1
        cboTo.SelectedIndex = -1
        cboCc.SelectedIndex = -1
        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("memoitem") = itemtable
        Session("criteria_memo") = criteria

        BindData()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
            searchjobslist()
        Else
            searchjobslist_owner()
        End If
    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & Date.Now.ToString
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

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim createdate As String
        createdate = Date.Now
        Dim objnonpo As New NonPO
        Try
            ExportToExcel(itemtable, Session("usercode"), createdate)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class