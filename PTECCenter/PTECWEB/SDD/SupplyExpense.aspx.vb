Public Class SupplyExpense
    Inherits System.Web.UI.Page
    Dim objStatus As String
    Dim jobno As String
    Dim jobdetailid As Integer
    Public menutable As DataTable
    Public maintable As DataTable
    Public details As DataTable
    Dim usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim objedi As New EDI
        Dim objsupplier As New Supplier

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        username = Session("username")
        usercode = Session("usercode")

        'txtDocDate.Text = Now
        txtDocDate.Attributes.Add("readonly", "readonly")

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

        'objStatus = "edit"
        'Session("jobno") = Request.QueryString("jobno")
        'Session("jobdetailid") = Request.QueryString("jobdetailid")
        'jobno = Session("jobno")
        'jobdetailid = Session("jobdetailid")
        'txtJobno.Text = jobno


        If Not IsPostBack Then
            Clear()
            objsupplier.SetCboTransportSupplier(cboSupplier)
            objedi.SetCboExpense(cboExpense)
            objedi.SetCboSaleOrder(cboSaleOrder)
            'objjob.SetCboJobCenterList(cboJobCenter)
            details = CreateDetails()
            Session("supplyexpense") = details
            'FindJob(jobno, jobdetailid)

            'Dim dt As New DataTable

            '    dt.Columns.Add("statusid", GetType(Integer))
            '    dt.Columns.Add("name", GetType(String))
            '    dt.Rows.Add(11, "confirm")
            '    cboStatus.DataSource = dt
            '    cboStatus.DataValueField = ("statusid")
            '    cboStatus.DataTextField = ("name")
            '    cboStatus.DataBind()

            setBtn(1)
        Else
            details = Session("supplyexpense")
            maintable = Session("maintable")
        End If


        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Sub setBtn(ByRef status As Integer)
        Select Case status
            Case = 1
                'save
                btnSave.Enabled = True
                btnConfirm.Enabled = True
                btnCancel.Enabled = True
            Case = 2
                'confirm
                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
            Case = 3
                'cancel
                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False
        End Select

    End Sub

    Private Sub Clear()
        If Not (details Is Nothing) Then
            details.Rows.Clear()
        End If

        Session("supplyexpense") = details
        txtDocNo.Text = ""
        txtDocDate.Text = Now.ToString
        cboExpense.SelectedIndex = -1
        cboSaleOrder.SelectedIndex = -1
        setBtn(1)
    End Sub
    Private Function CreateDetails() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("saleorder", GetType(String))
        dt.Columns.Add("expense", GetType(String))
        dt.Columns.Add("amount", GetType(Double))

        Return dt
    End Function

    Private Sub FindData(docno As String)
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String

        Dim objedi As New EDI
        Dim mydataset As DataSet
        ' Dim jobtable As DataTable

        'itemtable
        Try
            mydataset = objedi.Supply_Expense_Find(docno)
            maintable = mydataset.Tables(0)
            If maintable.Rows.Count > 0 Then
                showdata(mydataset.Tables(0))
                details = mydataset.Tables(1)

                Session("maintable") = maintable
                Session("supplyexpense") = details
            Else
                javaScript = "alertWarning('Find Data :  ไม่พบข้อมูล ');"
                ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
            End If

        Catch ex As Exception

            javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript)
        End Try
    End Sub
    Private Sub showdata(mytable As DataTable)
        Dim status As Integer
        With mytable.Rows(0)
            txtCreateBy.Text = .Item("createby")
            txtDocDate.Text = .Item("docdate")
            cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(.Item("supplierid")))
            status = .Item("status")
            setBtn(status)
            Select Case status
                Case = 1
                    lblstatus.Text = "บันทึก"
                Case = 2
                    lblstatus.Text = "รับรอง"
                Case = 3
                    lblstatus.Text = "ยกเลิก"
            End Select
        End With
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateUpdate() Then
            Save()
        End If
    End Sub
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If ValidateUpdate() Then
            Confirm()
        End If
    End Sub
    Private Sub Save()
        Dim scriptKey As String = "alert"
        Dim javaScript As String

        Dim docno As String = txtDocNo.Text
        Dim supplierid As Integer
        supplierid = cboSupplier.SelectedItem.Value

        Dim docdate As DateTime
        docdate = DateTime.Parse(txtDocDate.Text)
        If docno = "" Then
            'add new
            Try

                docno = SaveHead("", supplierid, 1, usercode, docdate)
                SaveDetail(docno, details)
            Catch ex As Exception
                javaScript = "alertWarning('Add new : " + ex.Message + "');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

            txtDocNo.Text = docno
            finddata(docno)
        Else
            'update
            Try
                SaveHead(docno, supplierid, 1, usercode, docdate)
                ClearDetail(docno)
                SaveDetail(docno, details)
            Catch ex As Exception
                javaScript = "alertWarning('Update : " + ex.Message + "');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

        End If

    End Sub


    Private Sub Confirm()
        Dim scriptKey As String = "alert"
        Dim javaScript As String

        Dim docno As String = txtDocNo.Text
        Dim supplierid As Integer = cboSupplier.SelectedItem.Value
        Dim docdate As DateTime
        docdate = DateTime.Parse(txtDocDate.Text)
        Try
            SaveHead(docno, supplierid, 2, usercode, docdate)
            setBtn(2)
        Catch ex As Exception
            javaScript = "alertWarning('Confirm : " + ex.Message + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub Cancel()
        Dim scriptKey As String = "alert"
        Dim javaScript As String

        Dim docno As String = txtDocNo.Text
        Dim supplierid As Integer = cboSupplier.SelectedItem.Value
        Dim docdate As DateTime
        docdate = DateTime.Parse(txtDocDate.Text)
        Try
            SaveHead(docno, supplierid, 3, usercode, docdate)
            setBtn(3)
        Catch ex As Exception
            javaScript = "alertWarning('Cancel : " + ex.Message + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
        End Try
    End Sub
    Private Function ValidateUpdate() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If details.Rows.Count = 0 Then
            result = False
            msg = "กรุณาใส่รายละเอียดค่าใช้จ่ายอย่างน้อย 1 รายการ"
            GoTo endprocess
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Sub saveFollowup()
        'Dim statusid As Integer = cboStatus.SelectedItem.Value
        'Dim details As String = txtDetailFollow.Text

        'Dim objjob As New jobs

        'Try
        '    objjob.Followup_Save(jobno, jobdetailid, statusid, details, usercode)
        '    FindJob(jobno, jobdetailid)
        'Catch ex As Exception
        '    Dim scriptKey As String = "UniqueKeyForThisScript"
        '    Dim javaScript As String = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
        '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        'End Try
    End Sub

    Public Function ChkDup(ByRef saleorder As String, ByRef expense As String) As Boolean
        Dim result As Boolean = True
        Dim query As String
        query = "saleorder = '" & saleorder & "' AND expense = '" & expense & "'"
        Dim dupdata() As DataRow = details.Select(query)
        If dupdata.Length > 0 Then
            result = False
        End If
        Return result
    End Function
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim saleorder As String = cboSaleOrder.SelectedItem.Text
        Dim expense As String = cboExpense.SelectedItem.Text
        If ChkDup(saleorder, expense) = True Then
            AddDetail()
        Else
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('ข้อมูลซ้ำ');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

    End Sub
    Private Sub AddDetail()
        Dim row As DataRow
        Dim msg As String = ""
        Dim amount As Double
        Try
            amount = Double.Parse(txtAmount.Text)
        Catch ex As Exception
            msg = "กรุณาระบุจำนวนเงินเป็นตัวเลข"
        End Try
        If msg = "" Then
            If txtAmount.Text = "" Or cboExpense.SelectedValue = "" Or cboSaleOrder.SelectedValue = "" Then
                msg = "กรุณากรอกข้อมูลให้ครบถ้วน"
            Else
                row = details.NewRow()
                row("saleorder") = cboSaleOrder.SelectedItem.Value
                row("expense") = cboExpense.SelectedItem.Text
                row("amount") = txtAmount.Text

                details.Rows.Add(row)

                Session("supplyexpense") = details
            End If
        End If
        If msg <> "" Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim saleorder As String = cboSaleOrder.SelectedItem.Text
        Dim expense As String = cboExpense.SelectedItem.Text

        Dim query As String
        query = "saleorder = '" & saleorder & "' AND expense = '" & expense & "'"
        Dim deldata() As DataRow = details.Select(query)
        If deldata.Length > 0 Then
            For Each r As DataRow In deldata
                details.Rows.Remove(r)
            Next
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub
    'Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    Dim s As String = "window.open('../OPS/Jobs_Report_JobForm.aspx?jobcode=" & Request.QueryString("jobno").ToString() &
    '"&supplierid=" & cboSupplier.SelectedItem.Value &
    '" ', '_blank');"

    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
    'End Sub
    Public Function SaveHead(ByRef docno As String, ByRef supplierid As Integer,
                             ByRef status As Integer, ByRef usercode As String, docdate As DateTime) As String
        Dim objedi As New EDI
        docno = objedi.Supply_SaveHead(docno, supplierid, usercode, status, docdate)
        Return docno
    End Function

    Public Sub SaveDetail(ByRef docno As String, ByRef details As DataTable)
        Dim objedi As New EDI

        For Each r As DataRow In details.Rows
            objedi.Supply_SaveDetail(docno, r("saleorder"), r("expense"), r("amount"))
        Next

    End Sub

    Public Sub ClearDetail(ByRef docno As String)
        Dim objedi As New EDI
        objedi.Supply_Expense_Clear_Detail_beforeSave(docno)
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        FindData(txtDocNo.Text)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Cancel()
    End Sub
End Class