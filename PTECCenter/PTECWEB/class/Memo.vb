Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Memo


    Public Sub SetCboMemoType(cbo As DropDownList)

        Dim dtcost As DataTable = MemoType_List()
        cbo.Items.Clear()
        For i As Integer = 0 To dtcost.Rows.Count - 1
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

    Public Function MemoType_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Memo_TypeList"
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
    Public Function Memo_Save(mmrno As String, typeid As Integer, subject As String,
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
End Class
