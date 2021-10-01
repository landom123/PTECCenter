Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Department

    Public Sub SetCboDepartmentBybranch(cboDepartment As DropDownList, branch As Integer)

        Dim dt As DataTable = List(branch)
        cboDepartment.Items.Clear()

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim item As ListItem = New ListItem(dt.Rows(i).Item("DepCode").ToString, dt.Rows(i).Item("depid").ToString)
            If Not String.IsNullOrEmpty(dt.Rows(i).Item("depname").ToString) Then
                'item.Attributes("data-subtext") = dt.Rows(i).Item("depname").ToString
                item.Attributes("data-token") = dt.Rows(i).Item("depname").ToString
            End If

            cboDepartment.Items.Add(item)

        Next

    End Sub
    Public Sub SetCboDepartment(obj As Object, branch As Integer)
        obj.DataSource = Me.List(branch)
        obj.DataValueField = "depid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Sub SetCboDepartmentforjobtype(obj As Object)
        obj.DataSource = Me.List_forjobtype()
        obj.DataValueField = "depid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub

    Public Function List_forjobtype() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Department_List_ForJobType"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = Branch
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function List(branch As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "department_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branch
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function




End Class
