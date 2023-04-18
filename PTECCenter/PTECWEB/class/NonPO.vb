Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class NonPO

    Public Sub SetCboPurpose(obj As Object)
        obj.DataSource = Me.Purpose_List()
        obj.DataValueField = "ppid"
        obj.DataTextField = "ppcode"
        obj.DataBind()

    End Sub

    Public Sub SetCboPurpose(obj As Object, show As String)
        obj.DataSource = Me.Purpose_List(show)
        obj.DataValueField = "ppid"
        obj.DataTextField = "ppcode"
        obj.DataBind()

    End Sub

    Public Sub SetCboBu(obj As Object)
        obj.DataSource = Me.Bu_List()
        obj.DataValueField = "buid"
        obj.DataTextField = "bucode"
        obj.DataBind()

    End Sub
    Public Sub SetCboPj(obj As Object)
        obj.DataSource = Me.Pj_List()
        obj.DataValueField = "pjid"
        obj.DataTextField = "pjcode"
        obj.DataBind()

    End Sub

    Public Sub SetCboStatusbyNonpocategory(obj As Object, category As String)
        obj.DataSource = Me.NonPO_Status_List(category)
        obj.DataValueField = "statusid"
        obj.DataTextField = "statusname"
        obj.DataBind()

    End Sub
    Public Sub SetCboPayby(obj As Object)
        obj.DataSource = Me.NonPO_Payby_List()
        obj.DataValueField = "name"
        obj.DataTextField = "code"
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
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("accountcode").ToString) Then
                item.Attributes("data-code") = dtcost.Rows(i).Item("accountcode").ToString
            End If
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("groupat").ToString) Then
                item.Attributes("data-subtext") = "- (" + dtcost.Rows(i).Item("groupat").ToString + ")"
            End If
            cboAccountCode.Items.Add(item)

        Next

    End Sub

    Public Sub SetCboAccountCode_List_for_PettyCashCO(obj As Object, userid As Integer)
        obj.DataSource = Me.AccountCode_List_for_PettyCashCO(userid)
        obj.DataValueField = "accountid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Sub SetCboAccountCode_List_for_PettyCashCO_Sub(cboAccountCode As DropDownList, userid As Integer)

        Dim dtcost As DataTable = AccountCode_List_for_PettyCashCO(userid)
        cboAccountCode.Items.Clear()
        For i As Integer = 0 To dtcost.Rows.Count - 1
            Dim item As ListItem = New ListItem(dtcost.Rows(i).Item("accountname"), dtcost.Rows(i).Item("accountid"))
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("accountgroup").ToString) Then
                item.Attributes("data-tokens") = dtcost.Rows(i).Item("accountgroup").ToString
                item.Attributes("data-category") = dtcost.Rows(i).Item("accountgroup").ToString
            End If
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("accountcode").ToString) Then
                item.Attributes("data-code") = dtcost.Rows(i).Item("accountcode").ToString
            End If
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("groupat").ToString) Then
                item.Attributes("data-subtext") = "- (" + dtcost.Rows(i).Item("groupat").ToString + ")"
            End If
            cboAccountCode.Items.Add(item)

        Next

    End Sub

    Public Function SaveAdvance(advno As String, headtable As DataTable, detailtable As DataTable, username As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        advno = SaveHeadAdv(headtable, username)
        result = advno
        SaveDetailAdv(advno, detailtable, username)


        Return result
    End Function
    Public Function SavePayment(payno As String, headtable As DataTable, detailtable As DataTable, username As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        payno = SaveHeadPay(headtable, username)
        result = payno
        SaveDetailPay(payno, detailtable, username)


        Return result
    End Function

    Public Function SavePettyCashHO(pchono As String, headtable As DataTable, detailtable As DataTable, username As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        pchono = SaveHeadPCHO(headtable, username)
        result = pchono
        SaveDetailPCHO(pchono, detailtable, username)


        Return result
    End Function

    Public Function SavePettyCashCO(pccono As String, headtable As DataTable, detailtable As DataTable, username As String) As String
        Dim result As String

        'Credit_Balance_List_Createdate

        pccono = SaveHeadPCCO(headtable, username)
        result = pccono
        SaveDetailPCCO(pccono, detailtable, username)


        Return result
    End Function

    Private Function SaveHeadPCCO(mytable As DataTable, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PettyCashCO_Save"
        cmd.CommandType = CommandType.StoredProcedure

        With mytable.Rows(0)
            cmd.Parameters.Add("@pccono", SqlDbType.VarChar).Value = .Item("nonpocode")
            cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = .Item("branchid")
            cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(.Item("duedate")), DBNull.Value, DateTime.Parse(.Item("duedate")))
            cmd.Parameters.Add("@vat_wait", SqlDbType.Bit).Value = .Item("vat_wait")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
        End With


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Private Sub SaveDetailPCCO(pccono As String, mytable As DataTable, username As String)
        Dim nonpocode As String
        With mytable
            For i = 0 To mytable.Rows.Count - 1
                If .Rows(i).Item("status") = "new" Or .Rows(i).Item("status") = "edit" Then
                    nonpocode = SaveDetailToTable(pccono,
                                      .Rows(i).Item("row"),
                                      .Rows(i).Item("nonpodtl_id"),
                                      .Rows(i).Item("detail").ToString,
                                      .Rows(i).Item("accountcodeid").ToString,
                                      .Rows(i).Item("depid"),
                                      .Rows(i).Item("buid"),
                                      .Rows(i).Item("ppid"),
                                      .Rows(i).Item("pjid"),
                                      .Rows(i).Item("docdate"),
                                    .Rows(i).Item("branchseller"),
                                    .Rows(i).Item("cost"),
                                    .Rows(i).Item("vat_per"),
                                    .Rows(i).Item("tax_per"),
                                    .Rows(i).Item("vendorcode").ToString,
                                    .Rows(i).Item("invoice").ToString,
                                    .Rows(i).Item("taxid").ToString,
                                    .Rows(i).Item("invoicedate"),
                                    .Rows(i).Item("nobill"),
                                    .Rows(i).Item("incompletebill"),
                                    username)
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub
    Private Function SaveHeadPCHO(mytable As DataTable, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PettyCashHO_Save"
        cmd.CommandType = CommandType.StoredProcedure

        With mytable.Rows(0)
            cmd.Parameters.Add("@pchono", SqlDbType.VarChar).Value = .Item("nonpocode")
            cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = .Item("detail")
            cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = .Item("branchid")
            cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = .Item("depid")
            cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = .Item("secid")
            cmd.Parameters.Add("@vat_wait", SqlDbType.Bit).Value = .Item("vat_wait")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
        End With


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Private Sub SaveDetailPCHO(pchono As String, mytable As DataTable, username As String)
        Dim nonpocode As String
        With mytable
            For i = 0 To mytable.Rows.Count - 1
                If .Rows(i).Item("status") = "new" Or .Rows(i).Item("status") = "edit" Then
                    nonpocode = SaveDetailToTable(pchono,
                                      .Rows(i).Item("row"),
                                      .Rows(i).Item("nonpodtl_id"),
                                      .Rows(i).Item("detail").ToString,
                                      .Rows(i).Item("accountcodeid").ToString,
                                      .Rows(i).Item("depid"),
                                      .Rows(i).Item("buid"),
                                      .Rows(i).Item("ppid"),
                                      .Rows(i).Item("pjid"),
                                      "",
                                      "",
                                    .Rows(i).Item("cost"),
                                    .Rows(i).Item("vat_per"),
                                    .Rows(i).Item("tax_per"),
                                    .Rows(i).Item("vendorcode").ToString,
                                    .Rows(i).Item("invoice").ToString,
                                    .Rows(i).Item("taxid").ToString,
                                    .Rows(i).Item("invoicedate"),
                                    .Rows(i).Item("nobill"),
                                    .Rows(i).Item("incompletebill"),
                                    username)
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub
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
            cmd.Parameters.Add("@vat_wait", SqlDbType.Bit).Value = .Item("vat_wait")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
            cmd.Parameters.Add("@ownerid", SqlDbType.Int).Value = .Item("ownerid")
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
                If .Rows(i).Item("status") = "new" Or .Rows(i).Item("status") = "edit" Then
                    nonpocode = SaveDetailToTable(advno,
                                      .Rows(i).Item("row"),
                                      .Rows(i).Item("nonpodtl_id"),
                                      .Rows(i).Item("detail").ToString,
                                      .Rows(i).Item("accountcodeid").ToString,
                                      .Rows(i).Item("depid"),
                                      .Rows(i).Item("buid"),
                                      .Rows(i).Item("ppid"),
                                      .Rows(i).Item("pjid"),
                                      "",
                                      "",
                                    .Rows(i).Item("cost"),
                                    .Rows(i).Item("vat_per"),
                                    .Rows(i).Item("tax_per"),
                                    .Rows(i).Item("vendorcode").ToString,
                                    .Rows(i).Item("invoice").ToString,
                                    .Rows(i).Item("taxid").ToString,
                                    .Rows(i).Item("invoicedate"),
                                    .Rows(i).Item("nobill"),
                                    .Rows(i).Item("incompletebill"),
                                    username)
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub
    Private Function SaveHeadPay(mytable As DataTable, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Payment_Save"
        cmd.CommandType = CommandType.StoredProcedure
        With mytable.Rows(0)
            cmd.Parameters.Add("@payno", SqlDbType.VarChar).Value = .Item("nonpocode")
            cmd.Parameters.Add("@payby", SqlDbType.VarChar).Value = .Item("payby")
            cmd.Parameters.Add("@coderef", SqlDbType.VarChar).Value = .Item("coderef")
            cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = .Item("branchid")
            cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = .Item("depid")
            cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = .Item("secid")
            cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = .Item("comid")
            cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = .Item("detail")
            cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = .Item("vendorcode")
            cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(.Item("duedate")), DBNull.Value, DateTime.Parse(.Item("duedate")))
            cmd.Parameters.Add("@vatwait", SqlDbType.Bit).Value = .Item("vat_wait")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
        End With


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function
    Private Sub SaveDetailPay(advno As String, mytable As DataTable, username As String)
        Dim nonpocode As String
        With mytable
            For i = 0 To mytable.Rows.Count - 1
                If .Rows(i).Item("status") = "new" Or .Rows(i).Item("status") = "edit" Then
                    nonpocode = SaveDetailToTable(advno,
                                      .Rows(i).Item("row"),
                                      .Rows(i).Item("nonpodtl_id"),
                                      .Rows(i).Item("detail").ToString,
                                      .Rows(i).Item("accountcodeid").ToString,
                                      .Rows(i).Item("depid"),
                                      .Rows(i).Item("buid"),
                                      .Rows(i).Item("ppid"),
                                      .Rows(i).Item("pjid"),
                                      "",
                                      "",
                                    .Rows(i).Item("cost"),
                                    .Rows(i).Item("vat_per"),
                                    .Rows(i).Item("tax_per"),
                                    .Rows(i).Item("vendorcode").ToString,
                                    .Rows(i).Item("invoice").ToString,
                                    .Rows(i).Item("taxid").ToString,
                                    .Rows(i).Item("invoicedate"),
                                    .Rows(i).Item("nobill"),
                                    .Rows(i).Item("incompletebill"),
                                    username)
                End If
            Next
        End With
        'Result = ds.Tables(0).Rows(0).Item("code")

    End Sub
    Private Function SaveDetailToTable(nonpocode As String, Row As Integer, nonpodtl_id As Integer, detail As String, accountcode As String,
                                       dep As Integer, bu As Integer, pp As Integer, pj As Integer,
                                  docdate As String, branchseller As String, amount As Double, vat As Integer, tax As Integer, vendor As String,
                                       invoice As String, taxid As String, invoicedate As String, nobill As Boolean, incompletebill As Boolean, user As String) As String
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
        cmd.Parameters.Add("@nonpodtl_id", SqlDbType.BigInt).Value = nonpodtl_id
        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        cmd.Parameters.Add("@accountcode", SqlDbType.VarChar).Value = accountcode
        cmd.Parameters.Add("@branchseller", SqlDbType.VarChar).Value = branchseller
        cmd.Parameters.Add("@dep", SqlDbType.Int).Value = dep
        cmd.Parameters.Add("@bu", SqlDbType.Int).Value = bu
        cmd.Parameters.Add("@pp", SqlDbType.Int).Value = pp
        cmd.Parameters.Add("@pj", SqlDbType.Int).Value = pj
        'cmd.Parameters.Add("@docdate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(docdate), DBNull.Value, DateTime.Parse(docdate))
        cmd.Parameters.Add("@docdate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(invoicedate), DBNull.Value, DateTime.Parse(invoicedate))
        cmd.Parameters.Add("@amount", SqlDbType.Money).Value = amount
        cmd.Parameters.Add("@vat_per", SqlDbType.Money).Value = vat
        cmd.Parameters.Add("@tax_per", SqlDbType.Money).Value = tax
        cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = vendor
        cmd.Parameters.Add("@invoice", SqlDbType.VarChar).Value = invoice
        cmd.Parameters.Add("@taxid", SqlDbType.VarChar).Value = taxid
        cmd.Parameters.Add("@invoicedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(invoicedate), DBNull.Value, DateTime.Parse(invoicedate))
        cmd.Parameters.Add("@nobill", SqlDbType.Bit).Value = nobill
        cmd.Parameters.Add("@incompletebill", SqlDbType.Bit).Value = incompletebill
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

    Public Function NonPO_AdvanceRequest_Save(amount As Double, detail As String, duedate As String, username As String, advownerid As Integer, comid As String) As DataTable
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
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(duedate), DBNull.Value, DateTime.Parse(duedate))
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username
        cmd.Parameters.Add("@advownerid", SqlDbType.Int).Value = advownerid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function NonPO_AdvanceRequest_Edit(nonpocode As String, amount As Double, detail As String, duedate As String, userid As Integer, advownerid As Integer, comid As String) As Boolean
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
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(duedate), DBNull.Value, DateTime.Parse(duedate))
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@advownerid", SqlDbType.Int).Value = advownerid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid


        cmd.ExecuteNonQuery()

        conn.Close()
        Return result
    End Function

    Public Sub NonPO_AdvanceRequest_Confirm(nonpocode As String, usercode As String)
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
    End Sub

    Public Sub NonPO_AdvanceRequest_More(nonpocode As String, more As Double, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_More"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@more", SqlDbType.Money).Value = more
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_Payment_Duplicate(nonpocode As String, usercode As String, note As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Payment_Duplicate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = note


        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_AdvanceRequest_Cancel(nonpocode As String, usercode As String)
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
    End Sub
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

    Public Function AdvanceRQList_For_Operator(nonpocode As String, startdate As String, enddate As String, statusid As String, startduedate As String, endduedate As String,
                                          depid As String, secid As String, comid As String, branchgroupid As String, branchid As String, createbyid As String, ownerid As String) As DataTable
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


        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@startduedate", SqlDbType.VarChar).Value = startduedate
        cmd.Parameters.Add("@endduedate", SqlDbType.VarChar).Value = endduedate
        cmd.Parameters.Add("@createbyid", SqlDbType.VarChar).Value = createbyid
        cmd.Parameters.Add("@ownerbyid", SqlDbType.VarChar).Value = ownerid



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
        cmd.CommandText = "NonPO_AdvanceRQList_For_Owner_Remain"
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

    Public Function AdvanceRQList_For_Owner(nonpocode As String, startdate As String, enddate As String, statusid As String, startduedate As String, endduedate As String, comid As String, branchgroupid As String, branchid As String, userid As Integer, category As String) As DataTable
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


        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@startduedate", SqlDbType.VarChar).Value = startduedate
        cmd.Parameters.Add("@endduedate", SqlDbType.VarChar).Value = endduedate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function ClearAdvanceList_For_Operator(nonpocode As String, coderef As String, startdate As String, enddate As String, statusid As String,
                                          depid As String, secid As String, comid As String, branchgroupid As String, branchid As String, createbyid As String, ownerid As String, category As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_ClearAdvanceList_For_Operator"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@coderef", SqlDbType.VarChar).Value = coderef
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@createbyid", SqlDbType.VarChar).Value = createbyid
        cmd.Parameters.Add("@ownerbyid", SqlDbType.VarChar).Value = ownerid
        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category




        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    'Public Function ClearAdvanceList_For_Owner(userid As Integer, working As Integer) As DataTable
    '    Dim result As DataTable
    '    'Credit_Balance_List_Createdate
    '    Dim ds As New DataSet
    '    Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim adp As New SqlDataAdapter

    '    conn.Open()
    '    cmd.Connection = conn
    '    cmd.CommandText = "NonPO_ClearAdvanceList_For_Owner"
    '    cmd.CommandType = CommandType.StoredProcedure


    '    cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
    '    cmd.Parameters.Add("@working", SqlDbType.Int).Value = working


    '    adp.SelectCommand = cmd
    '    adp.Fill(ds)
    '    result = ds.Tables(0)
    '    conn.Close()
    '    Return result
    'End Function

    Public Function ClearAdvanceList_For_Owner(nonpocode As String, coderef As String, startdate As String, enddate As String, statusid As String,
                                           branchgroupid As String, branchid As String, comid As String, userid As Integer, category As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_ClearAdvanceList_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@coderef", SqlDbType.VarChar).Value = coderef
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category




        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function PaymentList_For_Owner(userid As Integer, working As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PaymentList_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@working", SqlDbType.Int).Value = working


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function PaymentList_For_Owner(nonpocode As String, startdate As String, enddate As String, statusid As Integer, startduedate As String, endduedate As String,
                                           comid As String, vendor As String, payby As String, userid As Integer, category As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PaymentList_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@startduedate", SqlDbType.VarChar).Value = startduedate
        cmd.Parameters.Add("@endduedate", SqlDbType.VarChar).Value = endduedate
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = vendor
        cmd.Parameters.Add("@payby", SqlDbType.VarChar).Value = payby
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function PaymentList_For_Operator(nonpocode As String, startdate As String, enddate As String, statusid As String, startduedate As String, endduedate As String,
                                          depid As String, secid As String, comid As String, branchgroupid As String, branchid As String, vendor As String, payby As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PaymentList_For_Operator"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@startduedate", SqlDbType.VarChar).Value = startduedate
        cmd.Parameters.Add("@endduedate", SqlDbType.VarChar).Value = endduedate
        cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = vendor
        cmd.Parameters.Add("@payby", SqlDbType.VarChar).Value = payby


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function PettyCashCO_For_Operator(nonpocode As String, startdate As String, enddate As String, statusid As String, startduedate As String, endduedate As String,
                                          depid As String, secid As String, branchgroupid As String, branchid As String, vendor As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PettyCashCO_For_Operator"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        'cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        'cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        'cmd.Parameters.Add("@comid", SqlDbType.VarChar).Value = comid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@branchgroupid", SqlDbType.VarChar).Value = branchgroupid
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@startduedate", SqlDbType.VarChar).Value = startduedate
        cmd.Parameters.Add("@endduedate", SqlDbType.VarChar).Value = endduedate
        cmd.Parameters.Add("@vendor", SqlDbType.VarChar).Value = vendor




        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function PettyCashCO_For_Owner(userid As Integer, working As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PettyCashCO_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@working", SqlDbType.Int).Value = working


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
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Pp_List"
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
    Public Function Purpose_List(Optional show As String = "all") As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Pp_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@show", SqlDbType.VarChar).Value = show
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Bu_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Bu_List"
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

    Public Function Pj_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Pj_List"
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

    Public Function AccountCode_List_for_PettyCashCO(userid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "AccountCode_List_for_PettyCashCO"
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


    Public Sub Delete_Attach_By_attachid(attachid As Integer, userid As Integer)
        'Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Delete_Attach_By_attachid"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@attachid", SqlDbType.Int).Value = attachid
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Sub

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

    Public Sub Confirm(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Confirm"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub Confirm_ft(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Confirm_for_PettyCashCO"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Function NonPOPermission(nonpocode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PermisstionOwner"
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
    Public Function NonPOPermisstionOperator(cat As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PermisstionOperator"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@category_nonpo", SqlDbType.VarChar).Value = cat


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("acc")
        conn.Close()
        Return result

    End Function
    Public Function NonPOPermisstionAccount(nonpocode As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_PermisstionAccount"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("acc")
        conn.Close()
        Return result

    End Function
    Public Sub NonPO_AdvanceRequest_Allow(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Allow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_AdvanceRequest_NotAllow(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_NotAllow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_AdvanceRequest_SetDueDate(nonpocode As String, duedate As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_SetDueDate"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@duedate", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(duedate), DBNull.Value, DateTime.Parse(duedate))
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_AdvanceRequest_SetDueDateMore(nonpocode As String, duedateMore As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_SetDueDateMore"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@duedatemore", SqlDbType.DateTime).Value = If(String.IsNullOrEmpty(duedateMore), DBNull.Value, DateTime.Parse(duedateMore))
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_AdvanceRequest_Account_Verify(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Account_Verify"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_AdvanceRequest_Verify(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_Verify"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_Allow(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Allow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_Allow_ft(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Allow_for_PettyCashCO"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_NotAllow(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_NotAllow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_Verify(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Verify"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_Pass(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Pass"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_Pass_ft(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Pass_for_PettyCashCO"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_Cancel(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_Complete(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Complete"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_FNS_ReceiveDoc(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_FNS_ReceiveDoc"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_AccountEdit(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AccountRequestDocument"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Sub NonPO_Reject(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Reject"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Function checkAttatch(attid As Integer, chked As Boolean, userid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_CheckAttatch"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@attid", SqlDbType.Int).Value = attid
        cmd.Parameters.Add("@checked", SqlDbType.Bit).Value = chked
        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Nonpo_Export(nonpo As String, groupvat As Boolean, groupvendor As Boolean) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_ExportToD365"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpo
        cmd.Parameters.Add("@groupvat", SqlDbType.Bit).Value = groupvat
        cmd.Parameters.Add("@groupvendor", SqlDbType.Bit).Value = groupvendor


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Nonpo_Export_ADV(nonpo As String, groupvat As Boolean, groupvendor As Boolean) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_ExportToD365_ADV"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpo
        cmd.Parameters.Add("@groupvat", SqlDbType.Bit).Value = groupvat
        cmd.Parameters.Add("@groupvendor", SqlDbType.Bit).Value = groupvendor


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function Nonpo_Export_PCCO(nonpo As String, groupvat As Boolean, groupvendor As Boolean) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_ExportToD365_PCCO"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpo
        cmd.Parameters.Add("@groupvat", SqlDbType.Bit).Value = groupvat
        cmd.Parameters.Add("@groupvendor", SqlDbType.Bit).Value = groupvendor


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function NonPO_Status_List(nonpocategory As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Status_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocategory", SqlDbType.VarChar).Value = nonpocategory
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function NonPO_Payby_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Payby_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@nonpocategory", SqlDbType.VarChar).Value = nonpocategory
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function NonPO_GetBudget_PettyCash(username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_GetBudget_PettyCash"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("Budget")
        conn.Close()
        Return result
    End Function

    Public Sub ManageNonPO(nonpocode As String, statusid As Integer, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_ManageStatus"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub NonPO_AdvanceRequest_VerifyApproval(nonpocode As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_AdvanceRequest_VerifyApproval"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
End Class
