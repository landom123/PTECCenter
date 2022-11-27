Imports System.Drawing

Public Class newNTI
    Inherits System.Web.UI.Page

    Public detailtable As DataTable '= createdetailtable()
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()

    Public menutable As DataTable

    Public operator_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objNewNTI As New NTI

        Dim attatch As New Attatch
        Dim statusid As Integer = 0


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



        If Not IsPostBack() Then
            detailtable = createdetailtable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()


            ' objNewNTI.SetcboStatusList('cboStatus)

            If Not Request.QueryString("nticode") Is Nothing Then
                Try
                    attatch.SetCboMyfile(cboMyfile, Session("userid"))
                    objNewNTI.SetCboOfferType(cboOfferType)
                    Dim ds As DataSet
                    ds = objNewNTI.NewNTI_Find(Request.QueryString("nticode"))

                    detailtable = ds.Tables(0)
                    AttachTable = ds.Tables(1)
                    CommentTable = ds.Tables(2)


                    statusid = detailtable.Rows(0).Item("statusid")



                    'sethead(head)
                    setdetailtable(detailtable)
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            Else

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('Find Fail')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End If




            Session("detailtable_newnti") = detailtable
            Session("comment_newnti") = CommentTable
            Session("attatch_newnti") = AttachTable
        Else

            detailtable = Session("detailtable_newnti")
            CommentTable = Session("comment_newnti")
            AttachTable = Session("attatch_newnti")
            Try
                statusid = detailtable.Rows(0).Item("statusid")

            Catch ex As Exception
                statusid = 0
            End Try
        End If

        operator_code = objNewNTI.NewNTIPermisstionOperator()

        SetBtn(statusid)
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("potision_id", GetType(Integer))
        dt.Columns.Add("nticode", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("Tell", GetType(String))
        dt.Columns.Add("owner_Name", GetType(String))
        dt.Columns.Add("owner_Tell", GetType(String))
        dt.Columns.Add("owner_Type", GetType(String))
        dt.Columns.Add("Email", GetType(String))
        dt.Columns.Add("Latitude", GetType(String))
        dt.Columns.Add("Longitude", GetType(String))
        dt.Columns.Add("Remark", GetType(String))
        dt.Columns.Add("Address", GetType(String))
        dt.Columns.Add("Area", GetType(String))
        dt.Columns.Add("NumberArea", GetType(String))
        dt.Columns.Add("Title", GetType(String))
        dt.Columns.Add("Statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("Statusname", GetType(String)) 'new=0
        dt.Columns.Add("updateby", GetType(String))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("offer_Type", GetType(String))

        Return dt
    End Function


    Private Function createtablecomment() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("commentid", GetType(Integer))
        dt.Columns.Add("commentdetail", GetType(String))
        dt.Columns.Add("updateby", GetType(String))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("coderef", GetType(String))

        Return dt
    End Function
    Private Function createtableAttach() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("imagename", GetType(String))
        dt.Columns.Add("url", GetType(String))
        dt.Columns.Add("show", GetType(String))
        dt.Columns.Add("checked", GetType(Integer))

        Return dt
    End Function

    Private Sub SetBtn(statusid As String)
        Select Case statusid
            Case = "0" '0 : new

                btnUpdate.Visible = False

                'cboStatus.Visible = False
                btnConfirm.Visible = False
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = False
                card_attatch.Visible = False
            Case = "1" '1   รอตรวจสอบข้อมูล


                txtstatus.BackColor = Color.LightBlue


                If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                    txttitle.Attributes.Remove("readonly")
                    btnUpdate.Visible = True
                    cboOfferType.Attributes.Remove("disabled")
                    btnUpdateOffer.Visible = True



                    'cboStatus.Visible = True
                    btnConfirm.Visible = True
                    btnDisApproval.Visible = True
                Else
                    txttitle.Attributes.Add("readonly", "readonly")
                    btnUpdate.Visible = False
                    cboOfferType.Attributes.Add("disabled", "True")
                    btnUpdateOffer.Visible = False




                    'cboStatus.Visible = True
                    btnConfirm.Visible = False
                    btnDisApproval.Visible = False
                End If

                ' btnCancel.Enabled = True

                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "2" '2   อยู่ระหว่างการพิจารณาเบื้องต้น

                txtstatus.BackColor = Color.LightYellow


                If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                    txttitle.Attributes.Remove("readonly")
                    btnUpdate.Visible = True
                    cboOfferType.Attributes.Remove("disabled")
                    btnUpdateOffer.Visible = True



                    'cboStatus.Visible = True
                    btnConfirm.Visible = True
                    btnDisApproval.Visible = True
                Else
                    txttitle.Attributes.Add("readonly", "readonly")
                    btnUpdate.Visible = False
                    cboOfferType.Attributes.Add("disabled", "True")
                    btnUpdateOffer.Visible = False




                    'cboStatus.Visible = True
                    btnConfirm.Visible = False
                    btnDisApproval.Visible = False
                End If

                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "3" '3   อยู่ระหว่างเจรจาเจ้าของที่ดิน

                txtstatus.BackColor = Color.LightYellow


                If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                    txttitle.Attributes.Remove("readonly")
                    btnUpdate.Visible = True
                    cboOfferType.Attributes.Remove("disabled")
                    btnUpdateOffer.Visible = True



                    'cboStatus.Visible = True
                    btnConfirm.Visible = True
                    btnDisApproval.Visible = True
                Else
                    txttitle.Attributes.Add("readonly", "readonly")
                    btnUpdate.Visible = False
                    cboOfferType.Attributes.Add("disabled", "True")
                    btnUpdateOffer.Visible = False




                    'cboStatus.Visible = True
                    btnConfirm.Visible = False
                    btnDisApproval.Visible = False
                End If
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "4" '4   อยู่ระหว่างเสนอ EXCOM

                txtstatus.BackColor = Color.LightYellow


                If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                    txttitle.Attributes.Remove("readonly")
                    btnUpdate.Visible = True
                    cboOfferType.Attributes.Remove("disabled")
                    btnUpdateOffer.Visible = True



                    'cboStatus.Visible = True
                    btnConfirm.Visible = True
                    btnDisApproval.Visible = True
                Else
                    txttitle.Attributes.Add("readonly", "readonly")
                    btnUpdate.Visible = False
                    cboOfferType.Attributes.Add("disabled", "True")
                    btnUpdateOffer.Visible = False




                    'cboStatus.Visible = True
                    btnConfirm.Visible = False
                    btnDisApproval.Visible = False
                End If
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "5" '5   อยู่ระหว่างทำสัญญา

                txtstatus.BackColor = Color.LightYellow

                If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                    txttitle.Attributes.Remove("readonly")
                    btnUpdate.Visible = True
                    cboOfferType.Attributes.Remove("disabled")
                    btnUpdateOffer.Visible = True



                    'cboStatus.Visible = True
                    btnConfirm.Visible = True
                    btnDisApproval.Visible = True
                Else
                    txttitle.Attributes.Add("readonly", "readonly")
                    btnUpdate.Visible = False
                    cboOfferType.Attributes.Add("disabled", "True")
                    btnUpdateOffer.Visible = False




                    'cboStatus.Visible = True
                    btnConfirm.Visible = False
                    btnDisApproval.Visible = False
                End If
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "6" '6   อยู่ระหว่างรอจดเช่าที่ดิน

                txtstatus.BackColor = Color.LightYellow

                If operator_code.IndexOf(Session("usercode").ToString) > -1 Then
                    txttitle.Attributes.Remove("readonly")
                    btnUpdate.Visible = True
                    cboOfferType.Attributes.Remove("disabled")
                    btnUpdateOffer.Visible = True



                    'cboStatus.Visible = True
                    btnConfirm.Visible = True
                    btnDisApproval.Visible = True
                Else
                    txttitle.Attributes.Add("readonly", "readonly")
                    btnUpdate.Visible = False
                    cboOfferType.Attributes.Add("disabled", "True")
                    btnUpdateOffer.Visible = False




                    'cboStatus.Visible = True
                    btnConfirm.Visible = False
                    btnDisApproval.Visible = False
                End If
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

            Case = "7" '7   ดำเนินการเสร็จสิ้น

                txtstatus.BackColor = Color.GreenYellow

                txttitle.Attributes.Add("readonly", "readonly")
                btnUpdate.Visible = False
                cboOfferType.Attributes.Add("disabled", "True")
                btnUpdateOffer.Visible = False

                'cboStatus.Visible = False
                btnConfirm.Visible = False
                btnDisApproval.Visible = False
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "8" '8   ไม่ผ่านการประเมิน

                txtstatus.BackColor = Color.IndianRed

                txttitle.Attributes.Add("readonly", "readonly")
                btnUpdate.Visible = False
                cboOfferType.Attributes.Add("disabled", "True")
                btnUpdateOffer.Visible = False

                'cboStatus.Visible = False
                btnConfirm.Visible = False
                btnDisApproval.Visible = False
                'btnCancel.Enabled = False

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

        End Select
    End Sub

    Private Sub saveorupdate()
        If validatedata() Then
            'If Session("status") = "new" Then
            'If maintable.Rows.Count = 0 Then
            'updatehead()
            'End If
            Save()
            'Else
            '    SaveEdit()
            'End If
        End If

    End Sub

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""


        If String.IsNullOrEmpty(txttitle.Text.Trim()) Then
            result = False
            msg = "กรุณาใส่ชื่อสถานที่"
            GoTo endprocess
        End If
endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function


    Private Sub Save()
        Dim objNewNTI As New NTI
        Dim payno As String = ""
        Dim nticode = detailtable.Rows(0).Item("nticode")
        Dim title As String = txttitle.Text.Trim()
        'payno = txtpmno.Text
        Try
            objNewNTI.NewNTI_Edit(nticode, title, Session("usercode"))
            'txtpmno.Text = payno
            'Session("status_payment") = "edit"


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../Station/newNTI.aspx?nticode=" & nticode)
endprocess:
    End Sub

    Private Sub setdetailtable(dt As DataTable)

        Dim objsec As New Section
        With dt

            txtnticode.Text = .Rows(0).Item("nticode").ToString
            txttitle.Text = .Rows(0).Item("Title").ToString
            txtNumberArea.Text = .Rows(0).Item("NumberArea").ToString
            txtArea.Text = .Rows(0).Item("Area").ToString
            txtAreaType.Text = .Rows(0).Item("Area_Type").ToString
            Dim lalong As String = .Rows(0).Item("Latitude").ToString + "," + .Rows(0).Item("Longitude").ToString
            txtlalong.InnerText = lalong
            btnGPS.HRef = "https://maps.google.com/?q=" + lalong
            btnGPS.Target = "_blank"

            txtAreaAddress.Text = .Rows(0).Item("address").ToString

            txtRemark.Text = .Rows(0).Item("remark").ToString

            txtstatus.Text = .Rows(0).Item("statusname").ToString

            txtcreateby.InnerText = .Rows(0).Item("name").ToString
            txttell.InnerText = .Rows(0).Item("tell").ToString
            txtcreatedate.Text = .Rows(0).Item("createdate").ToString
            txtemail.InnerText = .Rows(0).Item("email").ToString

            lbtxtOwnerName.Text = "ชื่อ (" + .Rows(0).Item("owner_Type").ToString + ")"
            txtOwnerName.Text = .Rows(0).Item("owner_name").ToString
            lbtxtOwnerTell.Text = "เบอร์ (" + .Rows(0).Item("owner_Type").ToString + ")"
            txtOwnerTell.Text = .Rows(0).Item("owner_tell").ToString


            cboOfferType.SelectedIndex = cboOfferType.Items.IndexOf(cboOfferType.Items.FindByValue(.Rows(0).Item("offer_Type").ToString))
        End With


    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        saveorupdate()
    End Sub

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click

        Dim approval As New Approval
        Dim nticode As String = Request.QueryString("nticode")
        Try
            approval.Save_Comment_By_Code(nticode, txtComment.Text.Trim(), Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../Station/newNTI.aspx?nticode=" & nticode)

endprocess:
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If ValidateUpdate() Then
            saveFollowup()
        End If
    End Sub
    Private Function ValidateUpdate() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        'If cboStatus.SelectedItem.Value = 0 Then
        '    result = False
        '    msg = "กรุณาเลือกสถานะ"
        '    GoTo endprocess
        'End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function

    Private Sub saveFollowup()
        Dim statusid As Integer = detailtable.Rows(0).Item("statusid")
        Dim nticode As String = txtnticode.Text

        Dim objnti As New NTI
        Select Case statusid
            Case 1
                statusid = 2   'อยู่ระหว่างการพิจารณาเบื้องต้น
            Case 2
                statusid = 3   'อยู่ระหว่างเจรจาเจ้าของที่ดิน
            Case 3
                statusid = 4   'อยู่ระหว่างเสนอ EXCOM
            Case 4
                statusid = 5   'อยู่ระหว่างทำสัญญา
            Case 5
                statusid = 6   'อยู่ระหว่างรอจดเช่าที่ดิน
            Case 6
                statusid = 7   'ดำเนินการเสร็จสิ้น
        End Select

        Try
            objnti.Followup_Save(nticode, statusid, Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('update fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../Station/newNTI.aspx?nticode=" & nticode)
endprocess:
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function disApprovalByCode(ByVal approvalcode As String, ByVal message As String, ByVal updateby As String)
        Dim objnti As New NTI

        Try
            objnti.NewNTIFollowup_Reject(approvalcode, message, updateby)

        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function

    Private Sub btnUpdateOffer_Click(sender As Object, e As EventArgs) Handles btnUpdateOffer.Click
        If cboOfferType.SelectedItem.Value = "" Then

            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('กรุณาเลือกข้อเสนอ');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End If
        Dim nticode As String = txtnticode.Text
        Dim objnti As New NTI

        Try
            objnti.NewNTI_Edit_Offer(nticode, cboOfferType.SelectedItem.Value, Session("usercode"))
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('update fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../Station/newNTI.aspx?nticode=" & nticode)
endprocess:
    End Sub
End Class