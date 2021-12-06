

Imports System.IO

Imports ClosedXML.Excel
Public Class agreement
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public mainClient As DataTable = createClient()
    Public editable As DataTable = create()
    Public usercode, username As String
    Public agreeid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        txtProjectDate.Attributes.Add("readonly", "readonly")
        txtAgreeDate.Attributes.Add("readonly", "readonly")

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

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
            SetCboUsers(cboPrjManager)
            SetCboProjectStatus(cboPrjStatus)
            SetCboClient(cboClient)
            gvClientBindData()

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


    Public Sub btnAddClient()
        Dim clientid As Double
        Dim clienttable As DataTable
        clientid = cboClient.SelectedItem.Value
        clienttable = FindClientInfo(clientid)
        If clienttable IsNot Nothing Then
            AddClient(clienttable)
            gvClientBindData()
        End If
    End Sub

    Private Sub AddClient(clienttable As DataTable)
        Dim newrow As DataRow = mainClient.NewRow
        newrow("clientid") = clienttable.Rows(0).Item("clientid")
        newrow("clientno") = clienttable.Rows(0).Item("clientno")
        newrow("client") = clienttable.Rows(0).Item("client")
        newrow("clientaddress") = clienttable.Rows(0).Item("address")
        mainClient.Rows.Add(newrow)

        Session("mainclient") = mainClient
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
    Public Sub BindData()
        'gvData.DataSource = editable
        'gvData.DataBind()
    End Sub

End Class