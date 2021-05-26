Public Class gsmapprovededuct
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public gsmtable As DataTable = creategsm()
    Public usercode, username As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        usercode = Session("usercode")
        username = Session("username")
        txtbegindate.Attributes.Add("readonly", "readonly")
        txtenddate.Attributes.Add("readonly", "readonly")

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

            setCboStatus()
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
    Private Sub setCboStatus()
        cbostatus.Items.Add("All")
        cbostatus.Items.Add("Pending")
        cbostatus.Items.Add("Confirm")
    End Sub

    Private Function creategsm() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("id", GetType(Double))
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
        gvData.Columns(0).Visible = False
        'MergeRow()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        FindData()
    End Sub
    Private Sub FindData()
        Dim begindate, enddate As String
        Dim approvestatus As String = cbostatus.Text
        Dim objgsm As New gsm
        begindate = txtbegindate.Text.Substring(6, 4) & txtbegindate.Text.Substring(3, 2) & txtbegindate.Text.Substring(0, 2)
        enddate = txtenddate.Text.Substring(6, 4) & txtenddate.Text.Substring(3, 2) & txtenddate.Text.Substring(0, 2)
        gsmtable = objgsm.Deduct_Sell_List(begindate, enddate, "Confirm", approvestatus)
        Session("gsmtable") = gsmtable
        BindData()
    End Sub
    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()

    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim objgsm As New gsm
        If e.CommandName = "Approve" Then
            ''Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            ''Reference the GridView Row.
            Dim row As GridViewRow = gvData.Rows(rowIndex)

            'Fetch value of Name.
            Dim id As Double = Double.Parse(TryCast(row.FindControl("lblid"), Label).Text)
            Try
                objgsm.Deduct_Sell_Approve_or_reject(id, "Confirm", "", usercode)
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Save Complete');", True)
                FindData()
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between Confirm');", True)
            End Try


            ''Fetch value of Country.
            'Dim country As String = row.Cells(1).Text


        End If
        If e.CommandName = "Reject" Then
            '    'Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            '    'Reference the GridView Row.
            Dim row As GridViewRow = gvData.Rows(rowIndex)

            '    'Fetch value of Name.
            Dim reason As String = TryCast(row.FindControl("txtreason"), TextBox).Text

            If String.IsNullOrEmpty(reason) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('error: กรุณาใส่เหตุผล');", True)
            Else
                Dim id As Double = Double.Parse(TryCast(row.FindControl("lblid"), Label).Text)
                Try
                    objgsm.Deduct_Sell_Approve_or_reject(id, "Reject", reason, usercode)
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Reject Complete');", True)
                    FindData()
                Catch ex As Exception
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between Confirm');", True)
                End Try
            End If
            '    'Fetch value of Country.
            '    Dim country As String = row.Cells(1).Text
        End If
    End Sub
    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound
        '*** chk ***'
        'Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
        'If Not IsNothing(chk) Then
        '    chk.Checked = e.Row.DataItem("chk")
        'End If
        '*** vbs_whtaxid ***'
        'Dim lblbranch As Label = CType(e.Row.FindControl("lblbranch"), Label)
        'If Not IsNothing(lblbranch) Then
        '    lblbranch.Text = e.Row.DataItem("branchid")
        'End If

    End Sub
End Class