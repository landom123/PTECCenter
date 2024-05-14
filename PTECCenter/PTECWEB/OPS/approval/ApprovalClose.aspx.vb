Imports System.Drawing
Imports System.IO
Imports System.Web.Script.Serialization

Public Class WebForm4
    Inherits System.Web.UI.Page
    Public flag As Boolean = True

    Public usercode, username
    Public menutable As DataTable
    Public detailtable As DataTable '= createtable()
    Public nozzletable As DataTable '= createtable()
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
                    nozzletable = approvaldataset.Tables(6)
                    Session("detailtable") = detailtable
                    Session("nozzletable") = nozzletable
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
            nozzletable = Session("nozzletable")

            Dim target = Request.Form("__EVENTTARGET")
            If target = "deletedetail" Then
                Dim argument As String = Request("__EVENTARGUMENT")
                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(argument)
                deleteDetail(json("approvalnozzle_id"), json("row"))
            End If
        End If
    End Sub
    Private Sub chkuser(userid As String, supportid As String)
        If Not userid = Session("userid") And Not (Session("secid").ToString = "2" Or Session("secid").ToString = "35") Then
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
        'If detailtable.Rows(0).Item("approvallistid") = 23 Then
        '    Try

        '        Dim res As String = Request.Form("confirm_value")

        '        res = res.Replace("[", "")
        '        res = res.Replace("]", "")
        '        res = res.Replace("},", "}|")
        '        Dim strarr() As String
        '        strarr = res.Split("|")


        '        For index = 1 To strarr.Length

        '            res = strarr(index - 1)

        '            res = res.Replace(",{", "{")

        '            Dim jss As New JavaScriptSerializer
        '            Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)

        '            Dim rownumber As Integer = json("rownumber")
        '            Dim brand As String = json("brand").ToString().Trim()
        '            Dim producttype As String = json("producttype").ToString().Trim()
        '            Dim nozzle_no As String = json("nozzle_no").ToString().Trim()
        '            Dim positiononassest As String = json("positiononassest").ToString().Trim()
        '            Dim round1 As Integer = json("round1")
        '            Dim round2 As Integer = json("round2")
        '            Dim round3 As Integer = json("round3")
        '            Dim url1 As String = json("url1").ToString().Trim()
        '            Dim url2 As String = json("url2").ToString().Trim()
        '            Dim url3 As String = json("url3").ToString().Trim()
        '            Dim remark As String = json("remark").ToString().Trim()

        '            Dim id As String
        '            id = approval.SaveTEST5L(txtApprovalcode.Text.Trim(), rownumber, brand,
        '                                     producttype, nozzle_no, positiononassest,
        '                                     round1, round2, round3,
        '                                     url1, url2, url3,
        '                                     remark, Session("usercode"))
        '        Next
        '    Catch ex As Exception
        '        Dim scriptKey As String = "alert"
        '        'Dim javaScript As String = "alert('" & ex.Message & "');"
        '        Dim javaScript As String = "alertWarning('5L fail');"
        '        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        '        GoTo endprocess
        '    End Try
        'End If
        Approval_Close(fullfilenameimageafter, fullfilenameimagebill)
endprocess:
    End Sub
    Private Function fileupload(key As String, Files As HttpFileCollection) As String
        Dim imgname As New Approval
        Dim fullfilename As String = String.Empty

        Dim Keys() As String
        Keys = Files.AllKeys
        For i = 0 To Keys.GetUpperBound(0)
            If Not String.IsNullOrEmpty(Files(i).FileName) And key = Keys(i) Then
                Dim Extension As String = System.IO.Path.GetExtension(Files(i).FileName)
                Dim rootPath As String = "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
                Dim savePath As String '= "D:\\PTECAttatch\\IMG\\OPS_ขออนุมัติ\\"
                Dim di As String = System.IO.Path.GetDirectoryName(Files(i).FileName)
                'Dim oldpath As String = di + FileUpload1.FileName

                Try
                    Dim fileName As String
                    fileName = imgname.GetImageName()
                    savePath = rootPath
                    savePath += fileName
                    savePath += Extension
                    Do While System.IO.File.Exists(savePath)
                        fileName = imgname.GetImageName()
                        savePath = rootPath
                        savePath += fileName
                        savePath += Extension
                    Loop
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

    Private Sub btnAddDetails_Click(sender As Object, e As EventArgs) Handles btnAddDetails.Click

        Dim objApp As New Approval
        Dim res As String = Request.Form("addDetailJSON")

        Dim strarr() As String
        strarr = res.Split("}")
        res = strarr(0) + "}"
        res = res.Split("[")(1)
        Dim jss As New JavaScriptSerializer
        Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)

        Try
            Dim row As Integer = json("row").Trim
            Dim approvalNozzle_ID As String = json("approvalnozzle_id").Trim
            Dim round1 As Double = json("round1").Trim
            Dim round2 As Double = json("round2").Trim
            Dim round3 As Double = json("round3").Trim
            Dim url1 As String = getfilenozzle("nozzle_rond1").ToString()
            Dim url2 As String = getfilenozzle("nozzle_rond2").ToString()
            Dim url3 As String = getfilenozzle("nozzle_rond3").ToString()
            Dim Remark As String = json("remark").Trim

            objApp.UpdateTEST5L(txtApprovalcode.Text.Trim(), approvalNozzle_ID,
                                             round1, round2, round3,
                                             url1, url2, url3,
                                             Remark, Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('adddetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect(Request.RawUrl)
endprocess:
    End Sub
    Private Function getfilenozzle(key As String) As String
        Dim res As String = String.Empty
        Dim fullfilenameimageafter As String = String.Empty
        Dim approval As New Approval
        Dim Files As HttpFileCollection = Request.Files

        fullfilenameimageafter = fileupload(key, Files)
        Dim filenameArr() As String
        If Not String.IsNullOrEmpty(fullfilenameimageafter) Then
            filenameArr = fullfilenameimageafter.Split(",")
        End If
        Try
            If filenameArr IsNot Nothing Then
                For i = 0 To filenameArr.Length - 1
                    If Not String.IsNullOrEmpty(filenameArr(i)) Then
                        approval.Image_Save(filenameArr(i), key, detailtable.Rows(0).Item("approvalid"), Session("usercode"))
                        Dim urlIMG As String = My.Settings.fullurl
                        urlIMG += filenameArr(i)
                        res = urlIMG
                    End If
                Next i
            End If
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('upload file " & key & " fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return res
    End Function

    Private Sub deleteDetail(approvalnozzle_id As Integer, rows As Integer)
        Dim objApp As New Approval

        Try
            objApp.deleteDetail(txtApprovalcode.Text.Trim(), approvalnozzle_id, rows, Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect(Request.RawUrl)
endprocess:
    End Sub

End Class