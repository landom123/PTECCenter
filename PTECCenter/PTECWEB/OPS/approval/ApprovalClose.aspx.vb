Imports System.Drawing
Imports System.IO

Public Class WebForm4
    Inherits System.Web.UI.Page
    Public flag As Boolean = True

    Public usercode, username
    Public menutable As DataTable
    Public detailtable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        Dim statusid As Integer

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        'Dim usercode As String
        'usercode = Session("usercode")

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If


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
                    Session("detailtable") = detailtable
                    statusid = approvaldataset.Tables(0).Rows(0).Item("statusid")
                    If statusid = 4 Then
                        txtDetail.ReadOnly = True
                        lbDetailMandatory.Visible = False
                        If approvaldataset.Tables(2).Rows.Count > 0 Then
                            Dim fileImgAfterName As String = My.Settings.fullurl
                            fileImgAfterName += approvaldataset.Tables(2).Rows(0).Item("imagename")
                            'bt_img1.Attributes.Add("href", fileImgAfterName)
                            'img1.Attributes.Add("src", fileImgAfterName)
                        End If
                        If approvaldataset.Tables(3).Rows.Count > 0 Then
                            Dim fileImgBillName As String = My.Settings.fullurl
                            fileImgBillName += approvaldataset.Tables(3).Rows(0).Item("imagename")
                            'img2.Attributes.Add("src", fileImgBillName)
                        End If
                    End If
                    Session("status") = "read"
                    chkuser(detailtable.Rows(0).Item("createby").ToString, detailtable.Rows(0).Item("supportid").ToString)
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
    Private Sub chkuser(userid As String, supportid As String)
        If Not userid = Session("userid") And Not Session("secid").ToString = "2" Then
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
        Dim approval As New Approval
        Dim Files As HttpFileCollection = Request.Files

        If chkAfter.Checked Then
            fullfilenameimageafter = fileupload("approval_af", Files)
            Dim filenameArr_af() As String
            If Not String.IsNullOrEmpty(fullfilenameimageafter) Then
                filenameArr_af = fullfilenameimageafter.Split(",")
            End If
            Try
                For i = 0 To filenameArr_af.Length - 1
                    If Not String.IsNullOrEmpty(filenameArr_af(i)) Then 'สำหรับแก้บัค
                        approval.Image_Save(filenameArr_af(i), "Approval_AT", detailtable.Rows(0).Item("approvalid"), Session("usercode"))
                    End If
                Next i
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('upload file after fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
        End If
        If chkBill.Checked Then
            fullfilenameimagebill = fileupload("approval_bill", Files)
            Dim filenameArr_bill() As String
            If Not String.IsNullOrEmpty(fullfilenameimagebill) Then
                filenameArr_bill = fullfilenameimagebill.Split(",")
            End If
            Try
                For i = 0 To filenameArr_bill.Length - 1
                    If Not String.IsNullOrEmpty(filenameArr_bill(i)) Then 'สำหรับแก้บัค
                        approval.Image_Save(filenameArr_bill(i), "Approval_Bill", detailtable.Rows(0).Item("approvalid"), Session("usercode"))
                    End If
                Next i
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('upload file bill fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
        End If
        Approval_Close(fullfilenameimageafter, fullfilenameimagebill)
endprocess:
    End Sub
    Private Function fileupload(key As String, Files As HttpFileCollection) As String
        Dim imgname As New Approval
        Dim fullfilename As String = String.Empty

        Dim Keys() As String
        Keys = Files.AllKeys
        For i = 0 To Keys.GetUpperBound(0) - 1
            If Not String.IsNullOrEmpty(Files(i).FileName) And key = Keys(i) Then
                Dim Extension As String = System.IO.Path.GetExtension(Files(i).FileName)
                Dim savePath As String = "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
                Dim di As String = System.IO.Path.GetDirectoryName(Files(i).FileName)
                'Dim oldpath As String = di + FileUpload1.FileName

                Try
                    Dim fileName As String
                    fileName = imgname.GetImageName()
                    savePath += fileName
                    savePath += Extension
                    Files(i).SaveAs(savePath)
                    fullfilename += fileName
                    fullfilename += Extension
                    If Not i = Keys.GetUpperBound(0) - 1 Then
                        fullfilename += ","
                    End If
                    'img1.Attributes.Add("src", a)
                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    'Dim javaScript As String = "alert('" & ex.Message & "');"
                    Dim javaScript As String = "alertWarning('upload file fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

                End Try
            End If
            'End Try
        Next i

        Return fullfilename
    End Function
    Private Sub Approval_Close(fullfilenameimageafter As String, fullfilenameimagebill As String)
        Dim approval As New Approval
        Dim approvaltable As DataTable
        Dim approvalid As Integer
        Dim scriptKey As String
        Dim javaScript As String
        Try
            approvaltable = approval.Approval_Close(txtApprovalcode.Text.Trim(), txtDetail.Text.Trim(), "", txtCloseDate.Text.Trim(), Session("usercode"))
            approvalid = approvaltable.Rows(0).Item("approvalid")
            'If Not String.IsNullOrEmpty(fullfilenameimageafter) Then
            '    approval.Image_Save(fullfilenameimageafter, "Approval_AT", approvalid, Session("usercode"))
            'End If
            'If Not String.IsNullOrEmpty(fullfilenameimagebill) Then
            'End If

        Catch ex As Exception
            scriptKey = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            javaScript = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        scriptKey = "alert"
        javaScript = "alertSuccessUpload()"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
endprocess:
    End Sub
End Class