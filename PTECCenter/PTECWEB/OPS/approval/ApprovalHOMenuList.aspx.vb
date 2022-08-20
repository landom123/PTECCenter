Imports System.Drawing
Imports System.IO
Imports ClosedXML.Excel

Public Class ApprovalHOMenuList
    Inherits System.Web.UI.Page

    Public itemtable As DataTable

    Public criteria As DataTable '= createCriteria()
    Public menutable As DataTable
    Public cntdt As Integer

    Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim approval As New Approval
        Dim objNonpo As New NonPO
        Dim objbranch As New Branch
        Dim objdep As New Department
        Dim objsec As New Section
        Dim objsupplier As New Supplier
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

        If Not IsPostBack() Then

            approval.SetCboApprovalHOStatus(cboStatusFollow)
            objdep.SetCboDepartmentBybranch(cboDepartment, 0)
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(Session("depid").ToString))
            objsec.SetCboSection_seccode(cboSection, cboDepartment.SelectedItem.Value)

            '------------------------------------
            If Not Session("criteria_aho") Is Nothing Then 'จำเงื่อนไขที่กดไว้ล่าสุด
                criteria = Session("criteria_aho")
                BindCriteria(criteria)
                searchjobslist()
            Else
                searchjobslist()
                End If

            Else

            criteria = Session("criteria_aho")
            itemtable = Session("joblist_aho")
        End If

    End Sub
    Private Function createCriteria() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("txtaho", GetType(String))
        dt.Columns.Add("txtStartDate", GetType(String))
        dt.Columns.Add("txtEndDate", GetType(String))
        dt.Columns.Add("cboStatusFollow", GetType(String))
        dt.Columns.Add("cboDepartment", GetType(String))
        dt.Columns.Add("cboSection", GetType(String))
        dt.Columns.Add("pageindex", GetType(Integer))

        Return dt
    End Function
    Private Sub BindCriteria(criteria As DataTable)
        If criteria.Rows.Count > 0 Then
            txtaho.Text = criteria.Rows(0).Item("txtaho")
            txtStartDate.Text = criteria.Rows(0).Item("txtStartDate")
            txtEndDate.Text = criteria.Rows(0).Item("txtEndDate")
            gvRemind.PageIndex = criteria.Rows(0).Item("pageindex")
            cboDepartment.SelectedValue = criteria.Rows(0).Item("cboDepartment")
            cboSection.SelectedIndex = -1
            Dim depid As Integer
            Dim objsection As New Section

            depid = cboDepartment.SelectedItem.Value
            objsection.SetCboSection_seccode(cboSection, depid)
            cboSection.SelectedValue = criteria.Rows(0).Item("cboSection")
            cboStatusFollow.SelectedValue = criteria.Rows(0).Item("cboStatusFollow")

        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Response.Redirect("approvalHO.aspx")
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchjobslist()
    End Sub

    Private Sub searchjobslist()

        Dim objapproval As New Approval
        Dim detailtable As New DataTable
        Try
            itemtable = objapproval.ApprovalHOMenuList(txtaho.Text.Trim(),
                                                  cboStatusFollow.SelectedItem.Value.ToString,
                                                    txtStartDate.Text.Trim(),
                                                    txtEndDate.Text.Trim(),
                                                  cboDepartment.SelectedItem.Value.ToString,
                                                  cboSection.SelectedItem.Value.ToString,
                                                    Session("userid"))
            Session("joblist_aho") = itemtable
            BindData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('search fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub BindData()
        setCriteria() 'จำเงื่อนไขที่กดไว้ล่าสุด
        cntdt = itemtable.Rows.Count
        gvRemind.DataSource = itemtable
        gvRemind.DataBind()
    End Sub

    Private Sub setCriteria()
        criteria = createCriteria()
        criteria.Rows.Clear()
        criteria.Rows.Add(txtaho.Text.ToString.Trim(),
                          txtStartDate.Text.ToString.Trim(),
                          txtEndDate.Text.ToString.Trim(),
                          (cboStatusFollow.SelectedItem.Value),
                          (cboDepartment.SelectedItem.Value),
                          (cboSection.SelectedItem.Value),
                          gvRemind.PageIndex)
        Session("criteria_aho") = criteria
    End Sub
    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        cboSection.SelectedIndex = -1
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)
        'searchjobslist()

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


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim objbranch As New Branch
        txtaho.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        cboDepartment.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboStatusFollow.SelectedIndex = -1
        If itemtable IsNot Nothing Then
            itemtable.Rows.Clear()
        End If
        If criteria IsNot Nothing Then
            criteria.Rows.Clear()
        End If
        Session("joblist_aho") = itemtable
        Session("criteria_aho") = criteria


        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection_seccode(cboSection, depid)


        BindData()
    End Sub

    Private Sub gvRemind_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRemind.RowDataBound
        Dim statusAt As Integer = 6
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Data.Item("statusname") = "รอยืนยัน" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightBlue
            ElseIf Data.Item("statusname") = "ยกเลิก" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGray
            ElseIf Data.Item("statusname") = "รออนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightYellow
            ElseIf Data.Item("statusname") = "รอยืนยันหักยอดขาย" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.LightGoldenrodYellow
            ElseIf Data.Item("statusname") = "ไม่อนุมัติ" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.IndianRed
            ElseIf Data.Item("statusname") = "อนุมัติหักยอดขาย" Then
                e.Row.Cells.Item(statusAt).BackColor = Color.FromArgb(40, 167, 69)

            End If
        End If
    End Sub
End Class