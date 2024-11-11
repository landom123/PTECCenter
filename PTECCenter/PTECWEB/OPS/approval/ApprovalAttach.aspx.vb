Imports System.Drawing
Public Class approvalattach
    Inherits System.Web.UI.Page

    Public flag As Boolean = True
    Public menutable As DataTable

    'Public cost As Double
    Public usercode, username

    Public Approval_Doc As String = ""
    Public approval_account As Boolean = False
    Public PermissionAccount As DataSet '= createtable()
    Public detailtable As DataTable '= createdetailtable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        Dim statusid As Integer
        Dim objsupplier As New Supplier

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        'txtinvoicedate.Attributes.Add("readonly", "readonly")

        'Double.TryParse(txtCost.Text, cost)

        If Not IsPostBack() Then
            If Not Request.QueryString("approvalcode") Is Nothing Then

                objsupplier.SetCboVendorByName(cboVendor, "")
                Try
                    approvaldataset = objapproval.Approval_Find(Request.QueryString("approvalcode"))
                    detailtable = approvaldataset.Tables(0)
                    statusid = approvaldataset.Tables(0).Rows(0).Item("statusid")
                    If Not statusid = 11 And Not statusid = 13 Then '11	รอแสกนเอกสาร , 13	รอบัญชีตรวจสอบ
                        flag = False
                    End If
                    ViewState("detailtable") = detailtable

                    If approvaldataset.Tables(5).Rows.Count > 0 Then
                        Approval_Doc = convertToJSON(approvaldataset.Tables(5))
                    End If
                    PermissionAccount = chkPermissionAccount(Request.QueryString("approvalcode"))
                    If (statusid = 13) Then '13 รอบัญชีตรวจสอบ


                        PermissionAccount = chkPermissionAccount(Request.QueryString("approvalcode"))
                        Dim all_Account_Code = PermissionAccount.Tables(0).Rows(0).Item("acc").ToString
                        Dim words As String() = all_Account_Code.Split(New Char() {","c})

                        words = words.Select(Function(v) v.Trim()).ToArray()

                        If words.Contains(Session("usercode").ToString()) Then
                            approval_account = True
                            GoTo endprocess
                        End If

                    End If

endprocess:

                    chkuser(detailtable.Rows(0).Item("createby"), approval_account)
                    showdata(detailtable)
                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    'Dim javaScript As String = "alert('" & ex.Message & "');"
                    Dim javaScript As String = "alertWarning('find fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            Else
                flag = False
            End If
        Else
            detailtable = ViewState("detailtable")

            Dim target = Request.Form("__EVENTTARGET")
            'If target = "btnUpload_Click" Then

            '    Dim argument As String = Request("__EVENTARGUMENT")
            '    If argument = "isConfirmed" Then
            '        upload()
            '    ElseIf argument = "isDenied" Then
            '        cacel()
            '    End If

            'End If
            If target = "btnUpload_Click_oldCost_Other" Then
                Dim arg_oldCoast As Double = Request("__EVENTARGUMENT")
                upload(arg_oldCoast)
            ElseIf target = "btnUpload_Click_isDenied_Other" Then
                cacel()

            ElseIf target = "btnUpload_Click_newCost_ROD" Then
                Dim arg_newCoast As Double = Request("__EVENTARGUMENT")
                upload(arg_newCoast)

            ElseIf target = "btnUpload_Click_oldCost_ROD" Then
                Dim arg_oldCoast As Double = Request("__EVENTARGUMENT")
                upload(arg_oldCoast)
            ElseIf target = "btnUpload_Click_newCost" Then
                Dim arg_Coast As Double = Request("__EVENTARGUMENT")
                upload(arg_Coast)

            End If
        End If
    End Sub
    Private Function chkPermissionAccount(approvalcode As String) As DataSet
        Dim permissionApproval As New Approval
        Dim appPermission As New DataSet
        Try
            appPermission = permissionApproval.ApprovalAccountPermission(approvalcode)
        Catch ex As Exception
            'Dim scriptKey As String = "alert"
            ''Dim javaScript As String = "alert('" & ex.Message & "');"
            'Dim javaScript As String = "alertWarning('approval_permission fail');"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Throw New ApplicationException("Something happened :( chkPermissionApprovalOther ", ex)
        End Try
        Return appPermission
    End Function

    Private Sub chkuser(userid As Integer, isAccounting As Boolean)
        If Not userid = Session("userid") And isAccounting = False Then
            flag = False
        End If
    End Sub
    Private Sub showdata(mytable As DataTable)
        With mytable.Rows(0)
            txtApprovalcode.Text = .Item("approvalcode").ToString
            txtStatus.Text = .Item("statusname").ToString
            Select Case .Item("statusid")
                Case = "1"
                    txtStatus.BackColor = Color.LightYellow
                Case = "2"
                    txtStatus.BackColor = Color.GreenYellow
                Case = "3"
                    txtStatus.BackColor = Color.IndianRed
                Case = "4"
                    txtStatus.BackColor = Color.Gray
                Case = "6"
                    txtStatus.BackColor = Color.LightGray
                Case = "7"
                    txtStatus.BackColor = Color.LightBlue
                Case = "8"
                    txtStatus.BackColor = Color.LightCoral
                Case = "9"
                    txtStatus.BackColor = Color.OrangeRed
                Case = "10"
                    txtStatus.BackColor = Color.MediumPurple
                Case = "11"
                    txtStatus.BackColor = Color.Brown
                    txtStatus.ForeColor = Color.White
                Case = "12"
                    txtStatus.BackColor = Color.LightYellow
                Case = "13"
                    txtStatus.BackColor = Color.LightSalmon
                Case = "14"
                    txtStatus.BackColor = Color.IndianRed
            End Select

            'cboApproval.Attributes.Add("disabled", "True")
            'cboApproval.SelectedIndex = cboApproval.Items.IndexOf(cboApproval.Items.FindByValue(.Item("approvallistid")))

            txtApproval.Text = .Item("ApprovalName").ToString
            txtCreateBy.Text = .Item("name_request").ToString
            'txtCloseDate.Text = .Item("approvalclosedate").ToString
            txtDocDate.Text = .Item("createdate").ToString
            txtOwnerApprovalName.Text = .Item("ownerapprovalname").ToString
            txtOwnerApprovalDate.Text = .Item("OwnerApprovalDate").ToString


            txtCost.Text = .Item("price")
            txtDocDate.Text = .Item("attatchdate").ToString
            txtVat.Text = .Item("vat_per").ToString
            txtTax.Text = .Item("tax_per").ToString

            txtVendor.Text = .Item("Vendor_Code").ToString
            txtinvoiceno.Text = .Item("InvoiceNo").ToString
            txttaxid.Text = .Item("TaxIDNo").ToString
            txtinvoicedate.Text = .Item("InvoiceDate").ToString
            txtDistance.Text = .Item("distance").ToString



        End With
    End Sub
    Private Sub cacel()
        Dim approval As New Approval
        Dim approvalcode As String

        Try
            approvalcode = approval.Approval_Cancel(Request.QueryString("approvalcode"), Session("usercode"))
            ViewState("status") = "read"
            Response.Redirect("../approval/approval.aspx?approvalcode=" & approvalcode)


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub upload(cost As Double)
        If validatedata() Then
            If InStr(Request.ContentType, "multipart/form-data") Then
                Dim approval As New Approval
                Dim scriptKey As String
                Dim javaScript As String
                Dim fullfilename As String
                Dim Files As HttpFileCollection = Request.Files
                Dim Keys() As String
                Keys = Files.AllKeys
                'If Keys.Length <= 1 Then
                'scriptKey = "UniqueKeyForThisScript"
                'javaScript = "alertWarning('File is Nothing')"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                'GoTo endprocess
                'End If
                For i = 0 To Keys.GetUpperBound(0) - 1

                    If Not String.IsNullOrEmpty(Files(i).FileName) Then
                        fullfilename = String.Empty
                        Dim Extension As String = System.IO.Path.GetExtension(Files(i).FileName)
                        Dim rootPath As String = "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
                        Dim savePath As String '= "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
                        Dim di As String = System.IO.Path.GetDirectoryName(Files(i).FileName)
                        'Dim oldpath As String = di + FileUpload1.FileName

                        Try
                            Dim fileName As String
                            fileName = approval.GetImageName()
                            savePath = rootPath
                            savePath += fileName
                            savePath += Extension
                            Do While System.IO.File.Exists(savePath)
                                fileName = approval.GetImageName()
                                savePath = rootPath
                                savePath += fileName
                                savePath += Extension
                            Loop
                            Files(i).SaveAs(savePath)
                            fullfilename += fileName
                            fullfilename += Extension
                            If Not String.IsNullOrEmpty(fullfilename) Then
                                saveTodba(fullfilename)

                            End If
                            'img1.Attributes.Add("src", a)
                        Catch ex As Exception
                            scriptKey = "alert"
                            'Dim javaScript As String = "alert('" & ex.Message & "');"
                            javaScript = "alertWarning('upload file fail');"
                            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                            GoTo endprocess
                        End Try
                        'End Try
                    End If
                Next i
                Dim code As String
                Dim urldes As String
                Try
                    'String.Format("{0:n4}", txtCost.Text)
                    code = approval.Approval_Support_Allow(Request.QueryString("approvalcode"), cost, txtVat.Text, txtTax.Text, txtVendor.Text, txtinvoiceno.Text, txttaxid.Text, txtinvoicedate.Text, Session("usercode"), If(String.IsNullOrEmpty(txtDistance.Text), 0.00, txtDistance.Text))

                    Dim ds As DataSet
                    Dim dt As DataTable
                    Dim statusid As Integer
                    ds = approval.Approval_Find(code)
                    dt = ds.Tables(0)
                    statusid = dt.Rows(0).Item("statusid") '9 = ดำเนินการด้านเอกสาร
                    If detailtable.Rows(0).Item("category").ToString = "หักยอดขาย" And statusid = 9 Then
                        urldes = "../Non-PO/Advance/ClearAdvance.aspx?f=APP&code_ref=" & code
                    Else
                        urldes = "../approval/approval.aspx?approvalcode=" & code
                    End If

                Catch ex As Exception
                    scriptKey = "alert"
                    'Dim javaScript As String = "alert('" & ex.Message & "');"
                    javaScript = "alertWarning('Approval Allow fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    GoTo endprocess
                End Try
                scriptKey = "alert"
                javaScript = "alertSuccessUpload('" & code & "','" & urldes & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
endprocess:
            End If
        End If
    End Sub
    'Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
    '    upload()
    'End Sub
    Private Sub saveTodba(fullfilename As String)
        Dim approval As New Approval
        Try
            approval.Image_Save(fullfilename, "Approval_Doc", detailtable.Rows(0).Item("approvalid"), Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('upload file bill fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Function convertToJSON(dt As DataTable) As String
        Dim filePath As String = My.Settings.fullurl
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)()
            filePath = My.Settings.fullurl
            For Each col As DataColumn In dt.Columns
                filePath += dr(col)
                Dim strArr() As String
                Dim extension As String
                Dim type As String
                strArr = dr(col).Split(".")
                extension = strArr(strArr.Length - 1).ToLower()
                Select Case extension
                    Case = "pdf"
                        type = "application/" + extension
                    Case Else
                        type = "image/" + extension
                End Select
                row.Add("name", dr(col))
                row.Add("size", "")
                row.Add("type", type)
                row.Add("file", filePath)
            Next
            rows.Add(row)
        Next

        Return serializer.Serialize(rows)
    End Function

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""


        If String.IsNullOrEmpty(txtCost.Text) Then
            result = False
            msg = "กรุณาระบุค่าใช้จ่าย"
            GoTo endprocess
        End If

        'If String.IsNullOrEmpty(txtDistance.Text) Then
        '    result = False
        '    msg = "กรุณาระบุระยะทาง"
        '    GoTo endprocess
        'End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function
End Class