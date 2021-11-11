Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Supplier
    Public Sub SetCboVendor(obj As Object, username As String)
        obj.DataSource = Me.vendor_list(username)
        obj.DataValueField = "name"
        obj.DataTextField = "Vendor_name_code"
        obj.DataBind()
    End Sub
    Public Sub SetCboVendorByName(cboVendor As DropDownList, username As String)

        Dim dt As DataTable = vendor_list(username)
        cboVendor.Items.Clear()

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim item As ListItem = New ListItem(dt.Rows(i).Item("name").ToString, dt.Rows(i).Item("name").ToString)
            If Not String.IsNullOrEmpty(dt.Rows(i).Item("Vendor_Code").ToString) Then
                item.Attributes("data-subtext") = dt.Rows(i).Item("Vendor_Code").ToString
            End If

            cboVendor.Items.Add(item)

        Next

    End Sub
    Public Function list() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "supplier_list"
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

    Public Function vendor_list(username As String) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Vendor_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Sub SetCboSupplier(obj As Object)
        Dim sup As New Supplier

        obj.DataSource = sup.list()
        obj.DataValueField = "supplierid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub

    Public Sub SetCboTransportSupplier(obj As Object)
        Dim sup As New Supplier

        obj.DataSource = sup.Transportlist()
        obj.DataValueField = "supplierid"
        obj.DataTextField = "accountname"
        obj.DataBind()

    End Sub

    Public Function Transportlist() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_edi").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Supply_Transport_Supplier_List"
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

End Class
