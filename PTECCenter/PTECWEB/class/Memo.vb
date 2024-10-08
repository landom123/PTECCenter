﻿Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Memo

    Public Sub SetcboMemoCate_List(obj As Object)
        obj.DataSource = Me.Memo_Category_List()
        obj.DataValueField = "Memo_CategoryID"
        obj.DataTextField = "Category_Name"
        obj.DataBind()

    End Sub
    Public Sub SetCboMemoType_List(obj As Object)
        obj.DataSource = Me.MemoType_List()
        obj.DataValueField = "memo_typeid"
        obj.DataTextField = "type_name"
        obj.DataBind()

    End Sub

    Public Sub SetCboMemoStatus_List(obj As Object)
        obj.DataSource = Me.Memo_Status_List()
        obj.DataValueField = "statusid"
        obj.DataTextField = "statusname"
        obj.DataBind()

    End Sub
    Public Sub SetCboMemoType_List_by_Cateid(obj As Object, cateid As String)
        obj.DataSource = Me.MemoType_List(cateid)
        obj.DataValueField = "memo_typeid"
        obj.DataTextField = "type_name"
        obj.DataBind()

    End Sub

    Public Sub SetCboMemoType(cbo As DropDownList)

        Dim dtcost As DataTable = MemoType_List()
        cbo.Items.Clear()
        Dim cnt As Integer = dtcost.Rows.Count - 1
        For i As Integer = 0 To cnt
            Dim item As ListItem = New ListItem(dtcost.Rows(i).Item("type_name"), dtcost.Rows(i).Item("memo_typeid"))
            Dim category_name As String = dtcost.Rows(i).Item("category_name").ToString
            If Not String.IsNullOrEmpty(category_name) Then
                item.Attributes("data-tokens") = category_name
                item.Attributes("data-category") = category_name
            End If
            'If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("groupat").ToString) Then
            '    item.Attributes("data-subtext") = "- (" + dtcost.Rows(i).Item("groupat").ToString + ")"
            'End If
            cbo.Items.Add(item)

        Next

    End Sub

    Public Function MemoType_List(Optional cateid As String = "") As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Type_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@cateid", SqlDbType.VarChar).Value = cateid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Memo_Category_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Category_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Sub Memo_SentTo_Save(mmrno As String, touserid As Integer, type As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_SentTo_Save"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@mmrno", SqlDbType.VarChar).Value = mmrno
        cmd.Parameters.Add("@touserid", SqlDbType.Int).Value = touserid
        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Function Memo_Save(mmrno As String, cateid As Integer, typeid As Integer, subject As String,
                                 detailOther As String, menodetail As String, dtltype As String, url As String,
                                 amount As Double, username As String) As String
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrno", SqlDbType.VarChar).Value = mmrno
        cmd.Parameters.Add("@cateid", SqlDbType.Int).Value = cateid
        cmd.Parameters.Add("@typeid", SqlDbType.Int).Value = typeid
        cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = subject
        cmd.Parameters.Add("@detailOther", SqlDbType.VarChar).Value = detailOther
        cmd.Parameters.Add("@menodetail", SqlDbType.VarChar).Value = menodetail
        cmd.Parameters.Add("@dtltype", SqlDbType.VarChar).Value = dtltype
        cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = url
        cmd.Parameters.Add("@amount", SqlDbType.Money).Value = amount
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = username


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        Return result
    End Function

    Public Function Memo_Find(code As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrcode", SqlDbType.VarChar).Value = code
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function MemoPermission(code As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_PermisstionOwner"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrcode", SqlDbType.VarChar).Value = code
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function

    Public Sub Memo_Allow(mmrno As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Allow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrcode", SqlDbType.VarChar).Value = mmrno
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub Memo_NotAllow(mmrno As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_NotAllow"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrno", SqlDbType.VarChar).Value = mmrno
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub

    Public Sub Memo_Cancel(mmrno As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Cancel"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrno", SqlDbType.VarChar).Value = mmrno
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
    Public Function MemoList_For_Operator(mmrcode As String, Subject As String, MemoTypeid As String,
                                          startdate As String, enddate As String, statusid As String,
                                          ownerid As String, toid As String, ccid As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_List_For_Operator"
        cmd.CommandType = CommandType.StoredProcedure


        cmd.Parameters.Add("@mmrcode", SqlDbType.VarChar).Value = mmrcode
        cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = Subject
        cmd.Parameters.Add("@memotypeid", SqlDbType.VarChar).Value = MemoTypeid
        cmd.Parameters.Add("@startdate", SqlDbType.VarChar).Value = startdate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@statusid", SqlDbType.VarChar).Value = statusid
        cmd.Parameters.Add("@ownerid", SqlDbType.VarChar).Value = ownerid
        cmd.Parameters.Add("@toid", SqlDbType.VarChar).Value = toid
        cmd.Parameters.Add("@ccid", SqlDbType.VarChar).Value = ccid



        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function MemoList_For_Owner(userid As Integer, statusid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_List_For_Owner"
        cmd.CommandType = CommandType.StoredProcedure


        'cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        'cmd.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid
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

    Public Function Memo_Status_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Status_List"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Sub Memo_Sign_The_Acceptor(mmrno As String, usercode As String)
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_Sign_The_Acceptor"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@mmrcode", SqlDbType.VarChar).Value = mmrno
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode

        cmd.ExecuteNonQuery()
        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("jobcode")
        conn.Close()
        'Return result
    End Sub
End Class
