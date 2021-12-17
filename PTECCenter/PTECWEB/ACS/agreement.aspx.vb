

Imports System.IO

Imports ClosedXML.Excel
Public Class agreement
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public mainClient As DataTable = createClient()
    Public mainAssets As DataTable = createClient()
    Public mainOneTime As DataTable = createOnetime()
    Public mainFix As DataTable = createFix()
    Public mainFlexible As DataTable = createClient()
    Public editable As DataTable = create()
    Public usercode, username, agreeno, projectno As String
    Public agreeid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        txtProjectDate.Attributes.Add("readonly", "readonly")
        txtAgreeDate.Attributes.Add("readonly", "readonly")

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier
        If Session("mainclient") IsNot Nothing Then
            mainClient = Session("mainclient")
        End If
        If Session("mainassets") IsNot Nothing Then
            mainAssets = Session("mainassets")
        End If
        If Session("onetime") IsNot Nothing Then
            mainOneTime = Session("onetime")
        End If
        If Session("fix") IsNot Nothing Then
            mainFix = Session("fix")
        End If
        If Session("flexible") IsNot Nothing Then
            mainFlexible = Session("flexible")
        End If
        gvClientBindData()
        gvOneTimeBindData
        gvFixBindData
        gvFlexibleBindData


        If IsPostBack() Then

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If
            agreeid = Session("agreeid")
            'gsmtable = Session("gsmtable")
            'BindData()
        Else
            Session("pages") = "agree"
            'SetCboUsers(cboPrjManager)
            'SetCboProjectStatus(cboPrjStatus)
            SetCboAgreeType(cboAgreeType)
            SetCboClient(cboClient)
            SetCboOneTimePayment(cboOneTimePaymentType)
            SetCboOneTimePayment(cboFixPaymentType)

            cboFixRecurring.Items.Add("เดือน")
            cboFixRecurring.Items.Add("ปี")

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            If Not (Session("ediinv") Is Nothing) Then
                editable = Session("ediinv")
                BindData()
            End If
        End If

    End Sub
    Private Sub SetCboClient(obj As Object)
        Dim ag As New AgreeClient

        obj.DataSource = ag.Client_Cbo_List(agreeid)
        obj.DataValueField = "clientid"
        obj.DataTextField = "client"
        obj.DataBind()
    End Sub

    Private Sub SetCboAgreeType(obj As Object)
        Dim ag As New Agree

        obj.DataSource = ag.Ag_Cbo_Type()
        obj.DataValueField = "agtypeid"
        obj.DataTextField = "agtype"
        obj.DataBind()
    End Sub
    Private Sub SetCboOneTimePayment(obj As Object)
        Dim payment As New Payment

        obj.DataSource = payment.PaymentType_Cbo_List()
        obj.DataValueField = "paymenttypeid"
        obj.DataTextField = "paymenttype"
        obj.DataBind()
    End Sub
    Private Function createClient() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("No", GetType(Integer))
        dt.Columns.Add("clientid", GetType(Double))
        dt.Columns.Add("clientno", GetType(String))
        dt.Columns.Add("client", GetType(String))
        dt.Columns.Add("clientaddress", GetType(String))

        Return dt
    End Function
    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("billing_type", GetType(String))
        dt.Columns.Add("invoice_no", GetType(String))
        dt.Columns.Add("invoice_date", GetType(Date))
        dt.Columns.Add("due_date", GetType(Date))
        dt.Columns.Add("link", GetType(String))
        dt.Columns.Add("chk", GetType(Boolean))

        Return dt
    End Function

    Private Function createFix() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("fixid", GetType(Double))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("recurring", GetType(String))
        dt.Columns.Add("frequency", GetType(String))
        dt.Columns.Add("begindate", GetType(DateTime))
        dt.Columns.Add("enddate", GetType(DateTime))
        dt.Columns.Add("duedate", GetType(DateTime))
        dt.Columns.Add("amount", GetType(Double))

        Return dt
    End Function
    Private Function createOnetime() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("onetimeid", GetType(Double))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("duedate", GetType(String))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("whopaid", GetType(String))
        dt.Columns.Add("remark", GetType(String))


        'dt.Columns("onetimeid").AutoIncrement = True

        ''Set the Starting or Seed value.
        'dt.Columns("onetimeid").AutoIncrementSeed = 1

        ''Set the Increment value.
        'dt.Columns("onetimeid").AutoIncrementStep = 1

        Return dt
    End Function

    Public Sub btnAddClient()
        Dim clientid As Double
        Dim clienttable As DataTable
        clientid = cboClient.SelectedItem.Value
        clienttable = FindClientInfo(clientid)
        If clienttable IsNot Nothing Then
            AddClient(clienttable)
            gvClientBindData()
        End If
        Session("pages") = "agree"
    End Sub


    Public Sub AddNewAssets()
        Dim clientid As Double
        Dim clienttable As DataTable
        clientid = cboClient.SelectedItem.Value
        clienttable = FindClientInfo(clientid)
        If clienttable IsNot Nothing Then
            AddAssets()
            'gvAssetsBindData()
        End If
        Session("pages") = "assets"
    End Sub

    Public Sub AddOneTime()
        Dim newrow As DataRow = mainOneTime.NewRow
        Dim whopaid As String
        If chkWhopaid.Checked = True Then
            whopaid = "บริษัท"
        Else
            whopaid = "คู่สัญญา"
        End If
        Try
            newrow("onetimeid") = 0
            newrow("paymenttype") = cboOneTimePaymentType.SelectedItem.Text
            newrow("duedate") = DateTime.Parse(txtOneTimeDueDate.Text)
            newrow("amount") = Double.Parse(txtOneTimeAmount.Text)
            newrow("whopaid") = whopaid
            newrow("remark") = txtremark.Text
            mainOneTime.Rows.Add(newrow)

            Session("onetime") = mainOneTime
            Session("pages") = "payment"
            gvOneTimeBindData()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Public Sub AddFix()
        Dim newrow As DataRow = mainFix.NewRow
        Try
            newrow("fixid") = 0
            newrow("paymenttype") = cboFixPaymentType.SelectedItem.Text
            newrow("recurring") = cboFixRecurring.SelectedItem.Text
            newrow("frequency") = txtFrequency.Text
            newrow("begindate") = DateTime.Parse(txtFixBeginDate.Text)
            newrow("enddate") = DateTime.Parse(txtFixEndDate.Text)
            newrow("duedate") = DateTime.Parse(txtFixDueDate.Text)
            newrow("amount") = Double.Parse(txtFixAmount.Text)
            mainFix.Rows.Add(newrow)

            Session("fix") = mainFix
            Session("pages") = "payment"
            gvFixBindData()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub AddAssets()
        Dim newrow As DataRow = mainassets.NewRow
        newrow("clientid") = 0
        newrow("clientno") = ""
        newrow("client") = ""
        newrow("clientaddress") = ""
        mainAssets.Rows.Add(newrow)

        Session("mainassets") = mainAssets
    End Sub

    Public Sub DelAssets()
        'Dim newrow As DataRow = mainAssets.NewRow
        'newrow("clientid") = 0
        'newrow("clientno") = ""
        'newrow("client") = ""
        'newrow("clientaddress") = ""
        'mainAssets.Rows.Add(newrow)

        'Session("mainassets") = mainAssets
    End Sub

    Private Sub AddClient(clienttable As DataTable)
        Dim newrow As DataRow = mainClient.NewRow
        Try
            newrow("clientid") = clienttable.Rows(0).Item("clientid")
            newrow("clientno") = clienttable.Rows(0).Item("clientno")
            newrow("client") = clienttable.Rows(0).Item("client")
            newrow("clientaddress") = clienttable.Rows(0).Item("address")
            mainClient.Rows.Add(newrow)

            Session("mainclient") = mainClient
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Function FindClientInfo(clientid As Double) As DataTable
        Dim result As DataTable
        Dim client As New AgreeClient

        Try
            result = client.Client_Get_Info(clientid)
        Catch ex As Exception

        End Try

        Return result
    End Function
    Public Sub gvClientBindData()
        gvClient.datasource = mainClient
        gvClient.databind
    End Sub

    Public Sub gvOneTimeBindData()
        gvOneTime.DataSource = mainOneTime
        gvOneTime.DataBind()
    End Sub
    Public Sub gvFixBindData()
        gvFix.DataSource = mainFix
        gvFix.DataBind()
    End Sub
    Public Sub gvFlexibleBindData()
        gvFlexible.DataSource = mainFlexible
        gvFlexible.DataBind()
    End Sub
    Public Sub BindData()
        'gvData.DataSource = editable
        'gvData.DataBind()
    End Sub

End Class