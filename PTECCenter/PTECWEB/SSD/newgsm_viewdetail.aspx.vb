Public Class newgsm_viewdetail
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public metertable As DataTable = createmetertable()
    Public testtable As DataTable = createmetertable()
    Public topuptable As DataTable = createmetertable()
    Public artable As DataTable = createartable()
    Public othertable As DataTable = createothertable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim usercode, username
        usercode = Session("usercode")
        username = Session("username")
        Dim branch As String = Request.QueryString("branch")
        Dim closedate As String = Request.QueryString("closedate")

        lblBranch.Text = branch
        lblCloseDate.Text = Strings.Right(closedate, 2) & "/" & Strings.Mid(closedate, 5, 2) & "/" & Strings.Left(closedate, 4)
        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            metertable = Session("metertable")
            testtable = Session("testtable")
            topuptable = Session("topuptable")
            artable = Session("artable")
            othertable = Session("othertable")
            BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            GetData(branch, closedate)
            BindData()
        End If

    End Sub
    Private Sub GetData(ByRef branch As String, ByRef closedate As String)

        Dim objgsm As New gsm
        Dim mydataset As DataSet
        mydataset = objgsm.NEWGSM_to_D365_Detail_byBranchDate(branch, closedate)
        metertable = mydataset.Tables(0)

        testtable = mydataset.Tables(1)
        topuptable = mydataset.Tables(2)
        artable = mydataset.Tables(3)
        othertable = mydataset.Tables(4)
        Session("metertable") = metertable
        Session("testable") = testtable
        Session("topuptable") = topuptable
        Session("artable") = artable
        Session("othertable") = othertable

        If metertable.Rows.Count > 0 Then
            lblmetertotal.Text = Double.Parse(metertable.Rows(0).Item("total")).ToString("#,###,##0.00")
        End If
        If testtable.Rows.Count > 0 Then
            lbltestmeter.Text = Double.Parse(testtable.Rows(0).Item("total")).ToString("#,###,##0.00")
        End If
        If topuptable.Rows.Count > 0 Then
            lbltopup.Text = Double.Parse(topuptable.Rows(0).Item("total")).ToString("#,###,##0.00")
        End If
        If artable.Rows.Count > 0 Then
            lblAr.Text = Double.Parse(artable.Rows(0).Item("total")).ToString("#,###,##0.00")
        End If

    End Sub

    Private Function createmetertable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("grade_title", GetType(String))
        dt.Columns.Add("volume", GetType(Double))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("total", GetType(Double))

        Return dt
    End Function



    Private Function createartable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("ar_number", GetType(String))
        dt.Columns.Add("customer_name", GetType(String))
        dt.Columns.Add("product", GetType(String))
        dt.Columns.Add("volume", GetType(Double))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("total", GetType(Double))


        Return dt
    End Function


    Private Function createothertable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("td_description", GetType(String))
        dt.Columns.Add("amount", GetType(Double))

        Return dt
    End Function

    Private Sub BindData()
        gvDataSaleMeter.DataSource = metertable
        gvDataSaleMeter.DataBind()

        gvDataTest.DataSource = testtable
        gvDataTest.DataBind()

        gvDataTopup.DataSource = topuptable
        gvDataTopup.DataBind()

        gvDataAR.DataSource = artable
        gvDataAR.DataBind()

        gvDataOther.DataSource = othertable
        gvDataOther.DataBind()
    End Sub


    Private Sub gvDataAr_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDataAR.PageIndexChanging
        gvDataAR.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    'Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDataAR.RowDataBound
    '    '*** chk ***'
    '    Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
    '    If Not IsNothing(chk) Then
    '        chk.Checked = e.Row.DataItem("chk")
    '    End If
    '    '*** vbs_whtaxid ***'
    '    Dim lblbranch As Label = CType(e.Row.FindControl("lblbranch"), Label)
    '    If Not IsNothing(lblbranch) Then
    '        lblbranch.Text = e.Row.DataItem("br_code")
    '    End If

    'End Sub
End Class