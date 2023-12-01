Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Text

Public Class BPETAX
    Public cnnstr As String = "Server=10.15.100.228\ptecdba;Database=ptec_etax;User Id=ptec;Password=ptec@pure;"
    Public Function ExInvCSV(companytaxid As String, taxbranch As String,
                             doctype As String, begindate As Date, enddate As Date) As String
        Dim mydataset As DataSet
        Dim detaildataset As DataSet
        Dim strBegindate, strEnddate As String
        strBegindate = begindate.Year.ToString("0000") & begindate.Month.ToString("00") & begindate.Day.ToString("00")
        strEnddate = enddate.Year.ToString("0000") & enddate.Month.ToString("00") & enddate.Day.ToString("00")
        mydataset = GetInvoiceDataset(companytaxid, taxbranch, doctype, strBegindate, strEnddate)
        Dim sCSV = New StringBuilder()
        'sCSV.Append(Environment.NewLine)

        'Dim view As New DataView(mydataset.Tables(0))
        'section C
        For Each rowC As DataRow In mydataset.Tables(0).Rows
            '-- Handle comma
            sCSV.Append(String.Join(",", (From rw In rowC.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
            sCSV.Append(Environment.NewLine)
        Next

        'section H
        For Each rowH As DataRow In mydataset.Tables(1).Rows
            '-- Handle comma
            sCSV.Append(String.Join(",", (From rw In rowH.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
            sCSV.Append(Environment.NewLine)
            'section b,l,f
            detaildataset = GetInvoiceDetailDataset(rowH.Item("H4"))
            For Each rowB As DataRow In detaildataset.Tables(0).Rows
                '-- Handle comma
                sCSV.Append(String.Join(",", (From rw In rowB.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
                sCSV.Append(Environment.NewLine)
            Next
            For Each rowL As DataRow In detaildataset.Tables(1).Rows
                '-- Handle comma
                sCSV.Append(String.Join(",", (From rw In rowL.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
                sCSV.Append(Environment.NewLine)
            Next
            For Each rowF As DataRow In detaildataset.Tables(2).Rows
                '-- Handle comma
                sCSV.Append(String.Join(",", (From rw In rowF.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
                sCSV.Append(Environment.NewLine)
            Next
        Next

        'section T
        For Each rowT As DataRow In mydataset.Tables(2).Rows
            '-- Handle comma
            sCSV.Append(String.Join(",", (From rw In rowT.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
            sCSV.Append(Environment.NewLine)
        Next

        Return sCSV.ToString

        'GenDataC
        'GenDataH
        'GenDataB
        'GenDataL
        'GenDataF
        'GenDataT

    End Function

    Private Function GetInvoiceDetailDataset(invoiceno As String) As DataSet

        Dim result As New DataSet
        Dim ds As New DataSet
        Dim conn As New SqlConnection(cnnstr)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "BP_Invoice_for_ETAX_Detail"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invno", SqlDbType.VarChar).Value = invoiceno

        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result

    End Function
    Private Function GetInvoiceDataset(companytaxid As String, taxbranch As String,
                             doctype As String, begindate As String, enddate As String) As DataSet
        Dim result As New DataSet
        Dim ds As New DataSet
        Dim conn As New SqlConnection(cnnstr)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "BP_Invoice_for_ETAX"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@companytaxid", SqlDbType.VarChar).Value = companytaxid
        cmd.Parameters.Add("@taxbranch", SqlDbType.VarChar).Value = taxbranch
        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
    Public Function ExBillingCSV(companytaxid As String, taxbranch As String,
                                 doctype As String, begindate As Date, enddate As Date) As String
        'Dim result As String
        Dim mydataset As DataSet
        Dim detaildataset As DataSet
        Dim strBegindate, strEnddate As String
        strBegindate = begindate.Year.ToString("0000") & begindate.Month.ToString("00") & begindate.Day.ToString("00")
        strEnddate = enddate.Year.ToString("0000") & enddate.Month.ToString("00") & enddate.Day.ToString("00")
        mydataset = GetBillingDataset(companytaxid, taxbranch, doctype, strBegindate, strEnddate)
        Dim sCSV = New StringBuilder()
        'sCSV.Append(Environment.NewLine)

        'Dim view As New DataView(mydataset.Tables(0))
        'section C
        For Each rowC As DataRow In mydataset.Tables(0).Rows
            '-- Handle comma
            sCSV.Append(String.Join(",", (From rw In rowC.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
            sCSV.Append(Environment.NewLine)
        Next

        'section H
        For Each rowH As DataRow In mydataset.Tables(1).Rows
            '-- Handle comma
            sCSV.Append(String.Join(",", (From rw In rowH.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
            sCSV.Append(Environment.NewLine)
            'section b,l,f
            detaildataset = GetBillingDetailDataset(rowH.Item("H4"))
            For Each rowB As DataRow In detaildataset.Tables(0).Rows
                '-- Handle comma
                sCSV.Append(String.Join(",", (From rw In rowB.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
                sCSV.Append(Environment.NewLine)
            Next
            For Each rowL As DataRow In detaildataset.Tables(1).Rows
                '-- Handle comma
                sCSV.Append(String.Join(",", (From rw In rowL.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
                sCSV.Append(Environment.NewLine)
            Next
            For Each rowF As DataRow In detaildataset.Tables(2).Rows
                '-- Handle comma
                sCSV.Append(String.Join(",", (From rw In rowF.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
                sCSV.Append(Environment.NewLine)
            Next
        Next

        'section T
        For Each rowT As DataRow In mydataset.Tables(2).Rows
            '-- Handle comma
            sCSV.Append(String.Join(",", (From rw In rowT.ItemArray Select If(rw.ToString.Trim.Contains(","), String.Format("""{0}""", rw.ToString.Trim), rw.ToString.Trim))))
            sCSV.Append(Environment.NewLine)
        Next

        Return sCSV.ToString

        'GenDataC
        'GenDataH
        'GenDataB
        'GenDataL
        'GenDataF
        'GenDataT


    End Function
    Private Function GetBillingDetailDataset(invoiceno As String) As DataSet

        Dim result As New DataSet
        Dim ds As New DataSet
        Dim conn As New SqlConnection(cnnstr)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "BP_Billing_for_ETAX_Detail"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@invno", SqlDbType.VarChar).Value = invoiceno


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result

    End Function
    Private Function GetBillingDataset(companytaxid As String, taxbranch As String,
                             doctype As String, begindate As String, enddate As String) As DataSet
        Dim result As New DataSet
        Dim ds As New DataSet
        Dim conn As New SqlConnection(cnnstr)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "BP_Billing_for_ETAX"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@companytaxid", SqlDbType.VarChar).Value = companytaxid
        cmd.Parameters.Add("@taxbranch", SqlDbType.VarChar).Value = taxbranch
        cmd.Parameters.Add("@begindate", SqlDbType.VarChar).Value = begindate
        cmd.Parameters.Add("@enddate", SqlDbType.VarChar).Value = enddate
        cmd.Parameters.Add("@doctype", SqlDbType.VarChar).Value = doctype


        adp.SelectCommand = cmd
        adp.Fill(ds)
        result = ds
        conn.Close()
        Return result
    End Function
End Class
