﻿Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class SmartBill
    Public Sub SetCboSmartBilllist(cbo As ListBox)

        Dim dtcost As DataTable = list()
        cbo.Items.Clear()
        For i As Integer = 0 To dtcost.Rows.Count - 1
            Dim item As ListItem = New ListItem(dtcost.Rows(i).Item("nameforcbo"), dtcost.Rows(i).Item("sbw_code"))
            If Not String.IsNullOrEmpty(dtcost.Rows(i).Item("pure_card").ToString) Then
                item.Attributes("data-subtext") = "PC = (" + dtcost.Rows(i).Item("pure_card").ToString + ")"
            End If
            cbo.Items.Add(item)

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
        cmd.CommandText = "SmartBill_Withdraw_list"
        cmd.CommandType = CommandType.StoredProcedure


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds.Tables(0)
        conn.Close()
        Return result
    End Function

End Class