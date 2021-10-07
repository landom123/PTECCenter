Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class NonPO

    Public Sub SetCboPurpose(obj As Object)
        obj.DataSource = Me.Purpose_List()
        obj.DataValueField = "branchid"
        obj.DataTextField = "d365code"
        obj.DataBind()

    End Sub

    Public Sub SetCboAccountCode(cboAccountCode As DropDownList, userid As Integer)

        Dim dtcost As DataTable = AccountCode_List(userid)
        cboAccountCode.Items.Clear()
        For i As Integer = 0 To dtcost.Rows.Count - 1
            Dim item As ListItem = New ListItem(dtcost.Rows(i).Item("accountname"), dtcost.Rows(i).Item("accountid"))
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("accountgroup").ToString) Then
                item.Attributes("data-tokens") = dtcost.Rows(i).Item("accountgroup").ToString
                item.Attributes("data-category") = dtcost.Rows(i).Item("accountgroup").ToString
            End If

            cboAccountCode.Items.Add(item)

        Next

    End Sub
    Public Function SaveAdvance(advno As String,  headtable As DataTable, detailtable As DataTable, username As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        advno = SaveHeadAdv(headtable, username)
        result = advno
        SaveDetailAdv(advno, detailtable, username)


        Return result
    End Function
    Private Function SaveHeadAdv(mytable As DataTable, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Advance_Save"
        cmd.CommandType = CommandType.StoredProcedure

        With mytable.Rows(0)
            cmd.Parameters.Add("@advno", SqlDbType.VarChar).Value = .Item("nonpocode")
            cmd.Parameters.Add("@coderef", SqlDbType.VarChar).Value = .Item("coderef")
            cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = .Item("branchid")
            cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = .Item("depid")
            cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = .Item("secid")
            cmd.Parameters.Add("@chkpayback", SqlDbType.Bit).Value = .Item("chkpayback")
            cmd.Parameters.Add("@chkdeductsell", SqlDbType.Bit).Value = .Item("chkdeductsell")
            cmd.Parameters.Add("@payback_amount", SqlDbType.Money).Value = .Item("payback_amount")
            cmd.Parameters.Add("@deductsell_amount", SqlDbType.Money).Value = .Item("deductsell_amount")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
        End With


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function
    Private Sub SaveDetailAdv(advno As String, mytable As DataTable, username As String)
        Dim nonpocode As String
        With mytable
            For i = 0 To mytable.Rows.Count - 1
                If .Rows(i).Item("nonpodtl_id") = 0 Then
                    nonpocode = SaveDetailToTable(advno,
                                      .Rows(i).Item("row"),
                                      .Rows(i).Item("detail").ToString,
                                      .Rows(i).Item("accountcodeid").ToString,
                                      .Rows(i).Item("depid"),
                                      .Rows(i).Item("buid"),
                                      .Rows(i).Item("ppid"),
                                      "",
                                    .Rows(i).Item("cost"),
                                    .Rows(i).Item("vendorcode").ToString,
                                    username)
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub

    Private Function SaveDetailToTable(nonpocode As String, Row As Integer, detail As String, accountcode As String,
                                       dep As Integer, bu As Integer, pp As Integer,
                                  docdate As String, amount As Double, vendor As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPODtl_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@row", SqlDbType.BigInt).Value = Row
        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@accountcode", SqlDbType.VarChar).Value = accountcode
        cmd.Parameters.Add("@dep", SqlDbType.Int).Value = dep
        cmd.Parameters.Add("@bu", SqlDbType.Int).Value = bu
        cmd.Parameters.Add("@pp", SqlDbType.Int).Value = pp
        cmd.Parameters.Add("@docdate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(docdate), DBNull.Value, DateTime.Parse(docdate))
        cmd.Parameters.Add("@amount", SqlDbType.Money).Value = amount
        cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = vendor
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function



    Public Function NonPO_Save(nonpocategory As String, amount As Double, payby As Integer,
                                 statusrq As Integer, statusnpo As Integer,
                                 duedate As Object, detail As String,
                                 vendorcode As String, username As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocategory", SqlDbType.VarChar).Value = nonpocategory.ToUpper
        cmd.Parameters.Add("@limit", SqlDbType.Money).Value = amount
        cmd.Parameters.Add("@payby", SqlDbType.Int).Value = payby
        cmd.Parameters.Add("@statusrequest", SqlDbType.Int).Value = statusrq
        cmd.Parameters.Add("@statusnonpo", SqlDbType.Int).Value = statusnpo
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = duedate
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@vcode", SqlDbType.VarChar).Value = vendorcode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function NonPO_AdvanceRequest_Save(amount As Double, detail As String, username As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@amount", SqlDbType.Money).Value = amount
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function NonPO_AdvanceRequest_Edit(nonpocode As String, amount As Double, detail As String, userid As Integer) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Edit"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@limit", SqlDbType.Money).Value = amount
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Function NonPO_AdvanceRequest_Confirm(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Confirm"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Function

    Public Function NonPO_AdvanceRequest_Cancel(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Function
    Public Function NonPO_Find(nonpocode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function NonPO_AdvanceRQ_Find(advancerequestcode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRQ_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@advancerequestcode", SqlDbType.VarChar).Value = advancerequestcode
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Function AdvanceRQList_For_Operator(advrqcode As String, startdate As String, enddate As String, statusfollowid As String,
                                          depid As String, secid As String, branchgroupid As String, branchid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRQList_For_Operator"
        cmd.CommandType = CommandType.StoredProcedure


        'cmd.Parameters.Add("@jobcode", SqlDbType.VarChar).Value = jobcode
        'cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        'cmd.Parameters.Add("@jobtypeid", SqlDbType.VarChar).Value = jobtypeid
        'cmd.Parameters.Add("@statusfollowid", SqlDbType.VarChar).Value = statusfollowid
        'cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        'cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        'cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        'cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function AdvanceRQList_For_Owner(userid As Integer, statusid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRQList_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid
        'cmd.Parameters.Add("@jobtypeid", SqlDbType.VarChar).Value = jobtypeid
        'cmd.Parameters.Add("@statusfollowid", SqlDbType.VarChar).Value = statusfollowid
        'cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        'cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        'cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        'cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Purpose_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Purpose_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@grpid", SqlDbType.VarChar).Value = grpid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function AccountCode_List(userid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "AccountCode_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function NonPO_Attatch_Save(nonpocode As String, url As String, description As String, user As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Attatch_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = url
        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result

    End Function

    Public Function deleteDetailbyNonpodtlid(nonpodtlid As Integer, user As String) As Boolean
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPODtl_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpodtlid", SqlDbType.BigInt).Value = nonpodtlid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user

        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function
End Class
