Imports System.Globalization
Public Class frmAssets
    Inherits System.Web.UI.Page
    Public objStatus As String
    Public myrow As DataRow
    Public itemtypeTable As DataTable = createdetailtable()
    Public menutable As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'session
        'status,username,assetcode,
        Dim objbranch As New Branch
        Dim objdepart As New Department
        Dim objsection As New Section
        Dim objsupplier As New Supplier
        Dim scriptKey As String
        Dim javaScript As String

        txtBeginDate.Attributes.Add("readonly", "readonly") 'ใส่ที่หลัง ถ้าใส่ตอน add object จะเก็บค่าไม่ได้
        txtEndDate.Attributes.Add("readonly", "readonly")
        txtInvoiceDate.Attributes.Add("readonly", "readonly")

        'Session("username") = "PAB"

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
        usercode = Session("usercode")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########

        If Not (Session("itemtypetable") Is Nothing) Then
            itemtypeTable = Session("itemtypetable")
        End If
        'Response.Write("<script>alert('" & Session("status") & "');</script>")

        If Not IsPostBack Then
            'not postback
            Session("status") = "new"
            Try
                itemtypeTable.Rows.Clear()

                SetMenu()
                ClearText()
                'SetCboSupplier(cboSupplier)
                objsupplier.SetCboSupplier(cboSupplier)
                SetCboAssetGroup(cboAssetGroup)
                Dim grpid As Integer
                grpid = cboAssetGroup.SelectedItem.Value 'cboAssetGroup.Items(cboAssetGroup.SelectedIndex).Value
                SetCboAssetType(cboAssetType, grpid)
                'SetCboBranch(cboBranch)
                objbranch.SetComboBranch(cboBranch)
                'SetCboDepartment(cboDepartment)
                objdepart.SetCboDepartment(cboDepartment, 0)
                Dim depid As Integer
                depid = cboDepartment.SelectedItem.Value
                'SetCboSection(cboSection, depid)
                objsection.SetCboSection(cboSection, 0)
                SetCboAssetItemType(cboAssetItemType)
                txtCreateBy.Text = Session("username")

                If Not (Request.QueryString("assetcode") Is Nothing) Then
                    Session("assetcode") = Request.QueryString("assetcode")
                    txtAssetCode.Text = Session("assetcode")
                    Session("status") = "edit"
                    objStatus = "edit"
                    FindAsset(txtAssetCode.Text)
                Else
                    Session("status") = "new"
                    objStatus = "new"
                End If
            Catch ex As Exception
                Dim err As String = ex.Message
                ScriptKey = "UniqueKeyForThisScript"
                javaScript = "<script type='text/javascript'>msgalert('" & err & "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), ScriptKey, javaScript)
            End Try
        Else
            If Not (Request.QueryString("status") Is Nothing) Then
                Session("status") = Request.QueryString("status")
                objStatus = Session("status")
            Else
                objStatus = Session("status")
                SetMenu()
                'Response.Write("<script>alert('" & objEdit & "');</script>")
            End If
        End If
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("itemtypeid", GetType(Double))
        dt.Columns.Add("itemtype", GetType(String))
        dt.Columns.Add("itemdetail", GetType(String))

        Return dt
    End Function

    Private Sub ClearText()
        txtAssetCode.Text = ""
        txtCreateDate.Text = ""
        txtCreateBy.Text = ""
        txtname.Text = ""
        cboAssetGroup.SelectedIndex = 0
        cboAssetType.SelectedIndex = 0
        cboBranch.SelectedIndex = 0
        txtSerialNo.Text = ""
        txtPrice.Text = ""
        txtAssetAccCode.Text = ""
        cboSupplier.SelectedIndex = 0
        txtBeginDate.Text = ""
        txtEndDate.Text = ""
        cboBranch.SelectedIndex = 0
        cboDepartment.SelectedIndex = 0
        cboSection.SelectedIndex = 0
        txtDetails.Text = ""
        txtInvoiceno.Text = ""
        txtInvoiceDate.Text = ""
    End Sub
    Private Sub SetMenu()
        If Session("status") = "edit" Then
            btnPrint.Enabled = False
            btnSaveDetail.Enabled = True
        End If
        If Session("status") = "new" Then 'confrim เรื่อง กด new แล้วให้แก้ไขได้
            btnPrint.Enabled = False
            btnSaveDetail.Enabled = False
        End If

    End Sub

    Private Sub btnSaveDetail_Click(sender As Object, e As EventArgs) Handles btnSaveDetail.Click
        Dim itemid As Double
        Dim itemtype As String
        Dim itemdetail As String
        Dim assetcode As String
        assetcode = txtAssetCode.Text
        itemid = cboAssetItemType.SelectedItem.Value
        itemtype = cboAssetItemType.SelectedItem.Text
        itemdetail = txtItemDetail.Text

        If Not String.IsNullOrEmpty(txtItemDetail.Text) Then
            itemtypeTable.Rows.Add(itemid, itemtype, itemdetail)
            SaveItem(assetcode, itemid, itemdetail, Session("username"))
        End If
        Session("itemtypetable") = itemtypeTable
    End Sub
    Private Function validationsave() As Boolean
        Dim scriptKey As String
        Dim javaScript As String
        Dim result As Boolean = True
        If String.IsNullOrEmpty(txtname.Text) Then
            result = False
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "<script type='text/javascript'>msgalert('กรุณาระบุชื่อทรัพย์สิน');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            GoTo endprocess
        End If
        If cboAssetType.SelectedIndex < 0 Then
            result = False
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "<script type='text/javascript'>msgalert('กรุณาระบุประเภททรัพย์สิน');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            GoTo endprocess
        End If
endprocess:
        Return result
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If validationsave() = True Then
            If objStatus = "new" Then
                'save new ไม่ต้องบันทึก itemdetail เนื่องจากยังไม่มีข้อมูล    
                SaveNew()
            End If
            If objStatus = "edit" Then
                'save edit บันทึก itemdetail ด้วย   
                SaveEdit()
            End If
        Else
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('กรุณาระบุชื่อทรัพย์สิน');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub
    Private Sub SaveNew()
        Dim ass As New Assets
        Dim code As String
        Dim secid, typeid As Integer
        Dim price As Double

        'Dim begindate, enddate, invoicedate As String
        'begindate = DateTime.Parse(txtBeginDate.Text)
        'enddate = DateTime.Parse(txtEndDate.Text)
        'invoicedate = DateTime.Parse(txtInvoiceDate.Text)
        Try
            price = Double.Parse(txtPrice.Text)
        Catch ex As Exception
            price = 0
        End Try
        If cboSection.SelectedIndex < 0 Then
            secid = 0
        Else
            secid = cboSection.SelectedItem.Value
        End If
        If cboAssetType.SelectedIndex < 0 Then
            typeid = 0
        Else
            typeid = cboAssetType.SelectedItem.Value
        End If

        Try
            code = ass.Save("", Session("username"), txtname.Text,
                            cboAssetGroup.SelectedItem.Value, typeid,
                            txtSerialNo.Text, price, txtAssetAccCode.Text,
                            cboSupplier.SelectedItem.Value, txtBeginDate.Text, txtEndDate.Text,
                            cboBranch.SelectedItem.Value, cboDepartment.SelectedItem.Value,
                            secid, txtDetails.Text, txtInvoiceno.Text, txtInvoiceDate.Text)
            txtAssetCode.Text = code
            Session("assetcode") = code
            Session("status") = "edit"
            SetMenu()
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('SaveNew : " & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub
    Private Sub SaveEdit()
        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "<script type='text/javascript'>msgalert('update');</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

        Dim ass As New Assets
        Dim code As String
        Dim secid, typeid As Integer
        Dim price As Double

        Try
            price = Double.Parse(txtPrice.Text)
        Catch ex As Exception
            price = 0
        End Try
        If cboSection.SelectedIndex < 0 Then
            secid = 0
        Else
            secid = cboSection.SelectedItem.Value
        End If
        If cboAssetType.SelectedIndex < 0 Then
            typeid = 0
        Else
            typeid = cboAssetType.SelectedItem.Value
        End If

        Try
            code = ass.Save(txtAssetCode.Text, Session("username"), txtname.Text,
                            cboAssetGroup.SelectedItem.Value, typeid,
                            txtSerialNo.Text, price, txtAssetAccCode.Text,
                            cboSupplier.SelectedItem.Value, txtBeginDate.Text, txtEndDate.Text,
                            cboBranch.SelectedItem.Value, cboDepartment.SelectedItem.Value,
                            secid, txtDetails.Text, txtInvoiceno.Text, txtInvoiceDate.Text)
            txtAssetCode.Text = code
            Session("assetcode") = code
            Session("status") = "edit"
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('SaveEdit : " & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub

    Private Sub SaveItem(assetcode As String, itemtypeid As Integer, itemdetail As String, updateby As String)
        Dim ass As New Assets
        Dim result As Boolean
        Try
            result = ass.ItemSave(assetcode, itemtypeid, itemdetail, updateby)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('SaveItem : " & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
    End Sub
    Private Sub FindAsset(assetcode As String)
        Dim ass As New Assets
        Dim mytable As DataTable
        Dim mydataset As DataSet
        Dim objsection As New Section
        Dim invdate As DateTime
        Dim warbegin, warend, createdate As DateTime

        Try
            mydataset = ass.Find(assetcode)
            itemtypeTable.Rows.Clear()
            mytable = mydataset.Tables(0)
            itemtypeTable = mydataset.Tables(1)
            Session("itemtypeTable") = itemtypeTable
            With mytable.Rows(0)
                txtCreateBy.Text = chkDBNull(.Item("createbyname"))
                'createdate = .Item("createdate")
                'txtCreateDate.Text = createdate.ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("en-US")) '.Item("createdate")
                txtCreateDate.Text = chkDBNull(.Item("createdate"))
                txtname.Text = chkDBNull(.Item("name"))
                txtDetails.Text = chkDBNull(.Item("details"))
                txtBeginDate.Text = chkDBNull(.Item("warrantybegin"))
                'txtBeginDate.Text = warbegin.ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("en-US")) 'chkDBNull(.Item("createdate")'.Item(")warrantybegin")
                txtEndDate.Text = chkDBNull(.Item("warrantyend"))
                'txtEndDate.Text = warend.ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("en-US")) '.Item("createdate")'.Item("warrantyend")
                txtInvoiceno.Text = chkDBNull(.Item("invoiceno"))
                txtInvoiceDate.Text = chkDBNull(.Item("invoicedate"))
                'txtInvoiceDate.Text = invdate.ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("en-US")) '.Item("invoicedate")
                txtSerialNo.Text = chkDBNull(.Item("serialno"))
                txtPrice.Text = chkDBNull(.Item("price"))
                txtAssetAccCode.Text = chkDBNull(.Item("accountcode"))
                cboAssetGroup.SelectedIndex = cboAssetGroup.Items.IndexOf(cboAssetGroup.Items.FindByValue(.Item("assetgroupid")))
                SetCboAssetType(cboAssetType, cboAssetGroup.SelectedItem.Value)
                cboAssetType.SelectedIndex = cboAssetType.Items.IndexOf(cboAssetType.Items.FindByValue(.Item("assettypeid")))
                cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(.Item("supplierid")))
                cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(.Item("branchid")))
                cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(.Item("depid")))
                'SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
                objsection.SetCboSection(cboSection, cboDepartment.SelectedItem.Value)
                cboSection.SelectedIndex = cboSection.Items.IndexOf(cboSection.Items.FindByValue(.Item("secid")))
                SetMenu()
            End With
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>msgalert('FindAsset : " & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try

    End Sub


    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        Dim depid As Integer
        Dim objsection As New Section

        depid = cboDepartment.SelectedItem.Value
        objsection.SetCboSection(cboSection, depid)
    End Sub

    Private Sub cboAssetGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAssetGroup.SelectedIndexChanged
        Dim grpid As Integer
        grpid = cboAssetGroup.SelectedItem.Value
        SetCboAssetType(cboAssetType, grpid)
    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        Dim branchid As Integer = cboBranch.SelectedItem.Value

        Dim objdep As New Department
        objdep.SetCboDepartment(cboDepartment, branchid)

    End Sub

    Function chkDBNull(strString As Object) As String
        If IsDBNull(strString) Then
            Return ""
        Else
            Return strString.ToString()
        End If
    End Function

End Class