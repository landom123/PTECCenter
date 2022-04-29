Imports System.Drawing
Public Class approvalattach
    Inherits System.Web.UI.Page

    Public flag As Boolean = True

    Public detailtable As DataTable '= createdetailtable()
    Public menutable As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        Dim statusid As Integer
        Dim objsupplier As New Supplier

        'txtinvoicedate.Attributes.Add("readonly", "readonly")
        If Not IsPostBack() Then
            If Not Request.QueryString("approvalcode") Is Nothing Then

                objsupplier.SetCboVendorByName(cboVendor, "")
                Try
                    approvaldataset = objapproval.Approval_Find(Request.QueryString("approvalcode"))
                    detailtable = approvaldataset.Tables(0)
                    statusid = approvaldataset.Tables(0).Rows(0).Item("statusid")
                    If Not statusid = 11 Then
                        flag = False
                    End If
                    Session("detailtable") = detailtable
                    chkuser(detailtable.Rows(0).Item("createby"))
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
            detailtable = Session("detailtable")
        End If
    End Sub

    Private Sub chkuser(userid As Integer)
        If Not userid = Session("userid") Then
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
            End Select

            'cboApproval.Attributes.Add("disabled", "True")
            'cboApproval.SelectedIndex = cboApproval.Items.IndexOf(cboApproval.Items.FindByValue(.Item("approvallistid")))

            txtApproval.Text = .Item("ApprovalName").ToString
            txtCreateBy.Text = .Item("name_request").ToString
            'txtCloseDate.Text = .Item("approvalclosedate").ToString
            txtDocDate.Text = .Item("createdate").ToString
            txtOwnerApprovalName.Text = .Item("ownerapprovalname").ToString
            txtOwnerApprovalDate.Text = .Item("OwnerApprovalDate").ToString
        End With
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

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
                    Dim savePath As String = "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
                    Dim di As String = System.IO.Path.GetDirectoryName(Files(i).FileName)
                    'Dim oldpath As String = di + FileUpload1.FileName

                    Try
                        Dim fileName As String
                        fileName = approval.GetImageName()
                        savePath += fileName
                        savePath += Extension
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
                code = approval.Approval_Support_Allow(Request.QueryString("approvalcode"), String.Format("{0:n4}", txtCost.Text), txtVat.Text, txtTax.Text, txtVendor.Text, txtinvoiceno.Text, txttaxid.Text, txtinvoicedate.Text, Session("usercode"))

                Dim ds As DataSet
                Dim dt As DataTable
                Dim statusid As Integer
                ds = approval.Approval_Find(Request.QueryString("approvalcode"))
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
    End Sub
    Private Sub saveTodba(fullfilename As String)
        Dim approval As New Approval
        Try
            Approval.Image_Save(fullfilename, "Approval_Doc", detailtable.Rows(0).Item("approvalid"), Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('upload file bill fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class