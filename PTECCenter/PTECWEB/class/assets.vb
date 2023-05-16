Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Assets
    Public Function Save(assetcode As String,
                         updateby As String,
                         assetname As String,
                         assetgroup As Integer,
                         assettype As Integer,
                         serialno As String,
                         price As Double,
                         assetacccode As String,
                         supplier As Double,
                         warrbegindate As String,
                         warrenddate As String,
                         branch As Integer,
                         depid As Integer,
                         secid As Integer,
                         details As String,
                         invoiceno As String,
                         invoicedate As String) As String

        Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "assets_save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = assetcode
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = assetname
        cmd.Parameters.Add("@assettypeid", SqlDbType.Int).Value = assettype
        cmd.Parameters.Add("@supplierid", SqlDbType.Int).Value = supplier
        cmd.Parameters.Add("@branchid", SqlDbType.Int).Value = branch
        cmd.Parameters.Add("@depid", SqlDbType.Int).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.Int).Value = secid
        cmd.Parameters.Add("@details", SqlDbType.VarChar).Value = details
        cmd.Parameters.Add("@warrbegin", SqlDbType.Date).Value = Date.Parse(warrbegindate)
        cmd.Parameters.Add("@warrend", SqlDbType.Date).Value = Date.Parse(warrenddate)
        cmd.Parameters.Add("@serial", SqlDbType.VarChar).Value = serialno
        cmd.Parameters.Add("@price", SqlDbType.Money).Value = price
        cmd.Parameters.Add("@invoiceno", SqlDbType.VarChar).Value = invoiceno
        cmd.Parameters.Add("@invoicedate", SqlDbType.Date).Value = Date.Parse(invoicedate)
        cmd.Parameters.Add("@accountcode", SqlDbType.VarChar).Value = assetacccode
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = updateby
        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        Return result
    End Function
    Public Function ItemSave(assetcode As String,
                             assetitemtypeid As Double,
                             details As String,
                             UpdateBy As String) As Boolean
        'txtDocDate.Text
        'txtCreateBy.Text
        'txtname.Text
        'cboAssetGroup.Text
        'cboAssetType.Text
        'txtSerialNo.Text
        'txtPrice.Text
        'txtAssetAccCode.Text
        'cboSupplier.Text
        'txtBeginDate.Text
        'txtEndDate.Text
        'cboBranch.Text
        'cboDepartment.Text
        'cboSection.Text
        'txtDetails.Text
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "AssetsItem_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@assetcode", SqlDbType.VarChar).Value = assetcode
        cmd.Parameters.Add("@updateby", SqlDbType.VarChar).Value = UpdateBy
        cmd.Parameters.Add("@assetitemtypeid", SqlDbType.Int).Value = assetitemtypeid
        cmd.Parameters.Add("@details", SqlDbType.VarChar).Value = details
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        ' result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function ItemDel(assetcode As String,
                             assetitemtypeid As Double,
                             UpdateBy As String, detail As String) As Boolean
        'txtDetails.Text
        Dim result As Boolean
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "AssetsItem_Del"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@assetcode", SqlDbType.VarChar).Value = assetcode
        cmd.Parameters.Add("@updateby", SqlDbType.VarChar).Value = UpdateBy
        cmd.Parameters.Add("@assetitemtypeid", SqlDbType.Int).Value = assetitemtypeid
        cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        'adp.SelectCommand = cmd
        'adp.Fill(ds)
        ' result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function ItemTypelist() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "assetsitemtype_list"
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
    Public Function Find(assetcode As String) As DataSet
        Dim result As DataSet
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Assets_Find"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = assetcode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()

        Return result
    End Function
    Public Function Typelist(grpid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "assetstype_list"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@grpid", SqlDbType.VarChar).Value = grpid
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function Grouplist() As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "assetsgroup_list"
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@usercode", SqlDbType.VarChar).Value = usercode
        'cmd.Parameters.Add("@monthly", SqlDbType.VarChar).Value = monthly
        'cmd.Parameters.Add("@taxtype", SqlDbType.VarChar).Value = taxtype
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Function ComboList(branch As Integer, depid As Integer, secid As Integer) As DataTable
        Dim result As DataTable
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_ops").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "AssetsCombo_List"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@branchid", SqlDbType.VarChar).Value = branch
        cmd.Parameters.Add("@depid", SqlDbType.VarChar).Value = depid
        cmd.Parameters.Add("@secid", SqlDbType.VarChar).Value = secid
        'cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()

        Return result
    End Function
    Public Sub SetCboAssets(obj As Object, branch As Integer, Optional depid As Integer = 0, Optional secid As Integer = 0)
        obj.DataSource = Me.ComboList(branch, depid, secid)
        obj.DataValueField = "assetid"
        obj.DataTextField = "asset"
        obj.DataBind()

    End Sub
End Class
