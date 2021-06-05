Imports System.Drawing
Imports System.IO

Public Class WebForm4
    Inherits System.Web.UI.Page
    Public flag As Boolean = True

    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        Dim statusid As Integer

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        Dim usercode As String
        usercode = Session("usercode")



        If Not IsPostBack() Then
            txtCloseDate.Text = Now
            txtCloseDate.Attributes.Add("readonly", "readonly")

            Session("status") = "new"
            'objapproval.SetCboApproval(cboApproval)
            'objbranch.SetComboBranch(cboBranch, usercode)

            If Not Request.QueryString("approvalcode") Is Nothing Then
                Try
                    approvaldataset = objapproval.Approval_Find(Request.QueryString("approvalcode"))
                    detailtable = approvaldataset.Tables(0)
                    statusid = approvaldataset.Tables(0).Rows(0).Item("statusid")
                    If statusid = 4 Then
                        txtDetail.ReadOnly = True
                        lbDetailMandatory.Visible = False
                        If approvaldataset.Tables(2).Rows.Count > 0 Then
                            Dim fileImgAfterName As String = My.Settings.fullurl
                            fileImgAfterName += approvaldataset.Tables(2).Rows(0).Item("imagename")
                            'bt_img1.Attributes.Add("href", fileImgAfterName)
                            img1.Attributes.Add("src", fileImgAfterName)
                        End If
                        If approvaldataset.Tables(3).Rows.Count > 0 Then
                            Dim fileImgBillName As String = My.Settings.fullurl
                            fileImgBillName += approvaldataset.Tables(3).Rows(0).Item("imagename")
                            img2.Attributes.Add("src", fileImgBillName)
                        End If
                    End If
                    Session("status") = "read"
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
            End Select

            'cboApproval.Attributes.Add("disabled", "True")
            'cboApproval.SelectedIndex = cboApproval.Items.IndexOf(cboApproval.Items.FindByValue(.Item("approvallistid")))
            txtDetail.Text = .Item("message_closeapproval").ToString

            txtApproval.Text = .Item("ApprovalName").ToString
            txtCreateBy.Text = .Item("name_request").ToString
            'txtCloseDate.Text = .Item("approvalclosedate").ToString
            txtDocDate.Text = .Item("createdate").ToString
            txtOwnerApprovalName.Text = .Item("ownerapprovalname").ToString
            txtOwnerApprovalDate.Text = .Item("OwnerApprovalDate").ToString
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim fullfilenameimageafter As String = String.Empty
        Dim fullfilenameimagebill As String = String.Empty
        If chkAfter.Checked Then
            fullfilenameimageafter = fileupload(fileImgAfter)
        End If
        If chkBill.Checked Then
            fullfilenameimagebill = fileupload(fileImgBill)
        End If

        Approval_Close(fullfilenameimageafter, fullfilenameimagebill)
    End Sub
    Private Function fileupload(file As FileUpload) As String
        Dim imgname As New Approval
        Dim fullfilename As String = String.Empty
        If (file.HasFile) Then
            Dim Extension As String = System.IO.Path.GetExtension(file.FileName)
            Dim savePath As String = "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
            Try
                Dim fileName As String
                fileName = imgname.GetImageName()
                savePath += fileName
                savePath += Extension
                file.SaveAs(savePath)
                fullfilename += fileName
                fullfilename += Extension
                'img1.Attributes.Add("src", a)
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('upload file fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
            Return fullfilename
        End If
    End Function
    Private Sub Approval_Close(fullfilenameimageafter As String, fullfilenameimagebill As String)
        Dim approval As New Approval
        Dim approvaltable As DataTable
        Dim approvalid As Integer
        Try
            approvaltable = approval.Approval_Close(txtApprovalcode.Text.Trim(), txtDetail.Text.Trim(), txtCloseDate.Text.Trim(), Session("usercode"))
            approvalid = approvaltable.Rows(0).Item("approvalid")
            If Not String.IsNullOrEmpty(fullfilenameimageafter) Then
                approval.Image_Save(fullfilenameimageafter, "Approval_AT", approvalid, Session("usercode"))
            End If
            If Not String.IsNullOrEmpty(fullfilenameimagebill) Then
                approval.Image_Save(fullfilenameimagebill, "Approval_Bill", approvalid, Session("usercode"))
            End If
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertSuccessUpload()"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class