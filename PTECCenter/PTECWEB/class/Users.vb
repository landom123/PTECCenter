Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Users
    Public Sub SetcboUserName(cboUserName As DropDownList)

        Dim dt As DataTable = UserName_List()
        cboUserName.Items.Clear()
        Dim cnt_dt = dt.Rows.Count
        For i As Integer = 0 To cnt_dt - 1
            Dim name As String
            Dim branchname As String
            With dt.Rows(i)
                name = .Item("name").ToString
                branchname = .Item("branchname").ToString
            End With
            Dim item As ListItem = New ListItem(name, name)
            'If Not String.IsNullOrEmpty(dt.Rows(i).Item("Vendor_Code").ToString) Then
            'End If
            item.Attributes("data-subtext") = branchname

            'If Not String.IsNullOrEmpty(dt.Rows(i).Item("taxidno").ToString) Then
            'End If

            cboUserName.Items.Add(item)

        Next

    End Sub

    Public Function Login(usercode As String, password As String) As DataTable
        Dim result As DataTable
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_Login"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function Find(userid As String, username As String, usercode As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_Details"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = username
        cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function

    Public Function AddUser(name As String, usercode As String, branchid As String, depid As String, secid As String, positionid As String, empupper As String, email As String, password As String) As String
        Dim result As String

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branchid
        cmd.Parameters.Add("@department", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        cmd.Parameters.Add("@positionid", SqlDbType.VarChar).Value = positionid
        cmd.Parameters.Add("@empupper", SqlDbType.VarChar).Value = empupper
        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("usercode")
        conn.Close()
        Return result
    End Function

    Public Function resetPassword(usercode As String, pass As String)
        Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_ResetPassword"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@newpassword", SqlDbType.VarChar).Value = pass

        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Function
    Public Function dashboard(usercode As String, monthly As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_Dashboard"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Sub SetCboUsers(obj As Object)
        obj.DataSource = Me.List()
        obj.DataValueField = "userid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Function List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_List"
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
    Public Function EmailList(usercode As String) As String
        Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_email_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("email")
        conn.Close()
        Return result
    End Function
    Public Function ChangePassword(usercode As String, newpassword As String)
        Dim result As String
        'Credit_Balance_List_Createdate
        'Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        'Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_ChangePassword"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@newpassword", SqlDbType.VarChar).Value = newpassword
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype
        cmd.ExecuteNonQuery()

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("email")
        conn.Close()

    End Function
    Public Function ApprovalPermissionRead(userid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Permisstion"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function ApprovalPermissionRead(userid As Integer, approvallistid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Approval_Permisstion"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@approvallistid", SqlDbType.Int).Value = approvallistid
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function NonPOPermissionRead(userid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Permisstion"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function
    Public Function NonPOPermissionRead(userid As Integer, nonpocode As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter


        Dim connstr As String = WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString
        Dim strarr() As String
        Dim strarr_final() As String
        Dim from As String
        strarr = connstr.Split("=")
        strarr_final = strarr(2).Split(";")
        from = If((strarr_final(0).Trim.ToLower = "test_ops"), "uat", "")

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "NonPO_Permisstion"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid
        cmd.Parameters.Add("@nonpocode", SqlDbType.VarChar).Value = nonpocode
        cmd.Parameters.Add("@form", SqlDbType.VarChar).Value = from


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

    Public Function D365Code(userid As Integer) As String
        Dim result As String

        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "D365Code_By_Userid"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("D365Code")
        conn.Close()
        Return result
    End Function

    Public Function UserName_List() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "UserName_List"
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

    Public Sub forgotPassword(usercode As String, randomstr As String, newpass As String, email As String)
        'Dim result As String
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_usersright").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "User_ForgotPassword"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@coderef", SqlDbType.VarChar).Value = randomstr
        cmd.Parameters.Add("@newpass", SqlDbType.VarChar).Value = newpass
        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email

        adp.SelectCommand = cmd
        adp.Fill(ds)
        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()
        'Return result
    End Sub

End Class
