

Imports System.IO

Imports ClosedXML.Excel
Public Class agreement
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public mainClient As DataTable = createClient()
    Public mainAssets As DataTable = createAsset()
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
        gvFixBindData()
        gvFlexibleBindData()


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
            Session("pages") = "info"
            Session("pages2") = "ag"
            'SetCboUsers(cboPrjManager)
            'SetCboProjectStatus(cboPrjStatus)
            SetCboAgreeType(cboAgreeType)
            SetCboClient(cboClient)
            SetCboOneTimePayment(cboOneTimePaymentType)
            SetCboOneTimePayment(cboFixPaymentType)
            SetCboAssetType(cboAssetType)

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

    Private Sub SetCboAssetType(obj As Object)
        Dim asset As New AgAssets

        obj.DataSource = asset.AssetsType_Cbo
        obj.DataValueField = "assetstypeid"
        obj.DataTextField = "assetstype"
        obj.DataBind()
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
    Private Function createAsset() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("assetsno", GetType(String))
        dt.Columns.Add("assettype", GetType(String))
        dt.Columns.Add("landno", GetType(String))
        dt.Columns.Add("surveyno", GetType(String))
        dt.Columns.Add("subdistrict", GetType(String))
        dt.Columns.Add("district", GetType(String))
        dt.Columns.Add("province", GetType(String))
        dt.Columns.Add("renttype", GetType(String))
        dt.Columns.Add("rai", GetType(String))
        dt.Columns.Add("ngan", GetType(String))
        dt.Columns.Add("wa", GetType(String))
        dt.Columns.Add("gps", GetType(String))

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
        dt.Columns.Add("assetsno", GetType(String))
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
        Session("pages") = "info"
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
            Session("pages") = "agree"
            Session("pages2") = "payment"

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
            newrow("assetsno") = cboassetsno.SelectedItem.Text
            newrow("paymenttype") = cboFixPaymentType.SelectedItem.Text
            newrow("recurring") = cboFixRecurring.SelectedItem.Text
            newrow("frequency") = txtFrequency.Text
            newrow("begindate") = DateTime.Parse(txtFixBeginDate.Text)
            newrow("enddate") = DateTime.Parse(txtFixEndDate.Text)
            newrow("duedate") = DateTime.Parse(txtFixDueDate.Text)
            newrow("amount") = Double.Parse(txtFixAmount.Text)
            mainFix.Rows.Add(newrow)

            Session("fix") = mainFix
            Session("pages") = "agree"
            Session("pages2") = "payment"
            gvFixBindData()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub AddAssets()
        Dim newrow As DataRow = mainAssets.NewRow
        newrow("assetsno") = txtAssetDocNo.Text
        newrow("assettype") = cboAgreeType.Text
        newrow("landno") = txtLandno.Text
        newrow("surveyno") = txtSurveyNo.Text
        newrow("subdistrict") = txtSubDistrict.Text
        newrow("district") = txtDistrict.Text
        newrow("province") = txtProvince.Text
        newrow("renttype") = ""
        newrow("rai") = txtRai.Text
        newrow("ngan") = txtNgan.Text
        newrow("wa") = txtWa.Text
        newrow("gps") = txtGPS.Text
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
        Dim err As String
        Dim scriptKey As String
        Dim javaScript As String
        Dim dr() As System.Data.DataRow

        dr = mainClient.Select("clientno='" & clienttable.Rows(0).Item("clientno") & "'")
        If dr.Length > 0 Then
            err = "ข้อมูลซ้ำ"
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        Else
            Dim newrow As DataRow = mainClient.NewRow
            Try
                newrow("no") = mainClient.Rows.Count + 1
                newrow("clientid") = clienttable.Rows(0).Item("clientid")
                newrow("clientno") = clienttable.Rows(0).Item("clientno")
                newrow("client") = clienttable.Rows(0).Item("client")
                newrow("clientaddress") = clienttable.Rows(0).Item("address")
                mainClient.Rows.Add(newrow)

                Session("mainclient") = mainClient
            Catch ex As Exception
                err = ex.Message.ToString.Replace("'", "")
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        End If

    End Sub
    Private Function FindClientInfo(clientid As Double) As DataTable
        Dim result As DataTable
        Dim client As New AgreeClient

        Try
            result = client.Client_Get_Info(clientid)
        Catch ex As Exception
            Dim err As String = ex.Message.ToString.Replace("'", "")
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('" & err & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        Return result
    End Function
    Public Sub gvClientBindData()
        gvClient.datasource = mainClient
        gvClient.databind
    End Sub
    Public Sub gvAssetsBindData()
        'gvassets.DataSource = mainFix
        'gvFix.DataBind()
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
    Protected Sub gvClient_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim ag As New Agree

        Dim rowIndex As Integer
        Dim row As GridViewRow
        Dim projectno As String = txtProjectNo.Text
        Dim clientno As String

        If e.CommandName = "RemoveAgClient" Then
            ''Determine the RowIndex of the Row whose Button was clicked.
            rowIndex = Convert.ToInt32(e.CommandArgument)
            row = gvClient.Rows(rowIndex)

            clientno = Double.Parse(TryCast(row.FindControl("lblClientNo"), Label).Text)

            Try
                ag.removeClient(projectno, clientno, usercode)
            Catch ex As Exception
                Dim err As String
                Dim scriptKey As String
                Dim javaScript As String
                err = ex.Message.ToString.Replace("'", "")
                ScriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
            End Try

        End If

    End Sub
    Public Sub removeAgreeClient()

    End Sub
    Public Sub BindData()
        'gvData.DataSource = editable
        'gvData.DataBind()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
    End Sub
    Private Sub Clear()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim projectno, branch, agreetype As String
        Dim result As String = ""
        Dim ag As New Agree
        Dim projectdate, agreedate, agreeactivedate As DateTime

        Dim err As String
        Dim scriptKey As String
        Dim javaScript As String

        projectno = txtProjectNo.Text
        If validateData() Then
            branch = txtBranchCode.Text
            agreetype = cboAgreeType.SelectedItem.Text
            agreeno = txtAgreeNo.Text
            Try
                projectdate = Date.Parse(txtProjectDate.Text)
            Catch ex As Exception
                projectdate = Date.Now
                txtProjectDate.Text = Date.Now.ToString
            End Try
            Try
                agreedate = Date.Parse(txtAgreeDate.Text)
                txtAgreeDate.Text = Date.Now.ToString
            Catch ex As Exception
                agreedate = Date.Now
            End Try
            Try
                agreeactivedate = Date.Parse(txtAgreeAcitveDate.Text)
            Catch ex As Exception
                agreeactivedate = Date.Now
                txtAgreeAcitveDate.Text = Date.Now.ToString
            End Try

            Try
                result = ag.saveAgree(projectno, projectdate, branch, agreetype, agreeno, agreedate, agreeactivedate, usercode)
                If result <> "error" Then
                    txtProjectNo.Text = result
                    ag.saveClient(txtProjectNo.Text, mainClient, usercode)
                    ag.saveAssets()
                    ag.saveOnetime()
                    ag.saveRecurringFix()
                    ag.saveRecurringFlexible()
                    ag.saveFinance()
                    ag.saveOther()
                    err = "บันทึกเรียบร้อย"
                    scriptKey = "UniqueKeyForThisScript"
                    javaScript = "alertSuccess('" & err & "')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End If
            Catch ex As Exception
                err = ex.Message.ToString.Replace("'", "")
                scriptKey = "UniqueKeyForThisScript"
                javaScript = "alertWarning('" & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try

        End If
    End Sub
    Private Function validateData() As Boolean
        Dim result As Boolean = True


        Return result
    End Function

    'Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
    '    Dim mydataset As DataSet
    '    Dim err As String
    '    Dim scriptKey As String
    '    Dim javaScript As String

    '    Dim projectno As String = txtprojectnoFind.Text
    '    'Dim agreeno As String = txtagreenoFind.Text

    '    If String.IsNullOrEmpty(projectno) = True And String.IsNullOrEmpty(agreeno) = True Then
    '        err = "กรุณาระบุเลขที่โครงการ หรือเลขที่สัญญา"
    '        scriptKey = "UniqueKeyForThisScript"
    '        javaScript = "alertWarning('" & err & "')"
    '        ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript, True)
    '    Else
    '        Dim ag As New Agree
    '        mydataset = ag.FindProject(projectno, agreeno)
    '        If mydataset IsNot Nothing Then
    '            ShowData(mydataset)
    '        End If
    '    End If
    'End Sub
    Private Sub ShowData(mydataset As DataSet)
        With mydataset.Tables(0).Rows(0)
            txtProjectNo.Text = .Item("projectno")
            txtProjectDate.Text = .Item("projectdate")
            txtBranchCode.Text = .Item("branch")
            txtAgreeNo.Text = .Item("agno")
            cboAgreeType.SelectedIndex = cboAgreeType.Items.IndexOf(cboAgreeType.Items.FindByText(.Item("agtype")))
            txtAgreeDate.Text = .Item("agdate")
            txtAgreeAcitveDate.Text = .Item("agactivedate")
            'approveby
            'approvedate
            'createby
            'createdate
            'updateby
            'updatedate
        End With

        mainClient = mydataset.Tables(1)
        Session("mainclient") = mainClient
        gvClientBindData()

    End Sub

End Class