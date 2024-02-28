Imports System.Drawing

Public Class MemoRequest
    Inherits System.Web.UI.Page

    Public flag As Boolean = True
    Public menutable As DataTable

    Public detailtable As DataTable '= createdetailtable()
    Public PermissionOwner As DataSet '= createtable()
    Public CommentTable As DataTable '= createtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim attatch As New Attatch
        Dim objMemo As New Memo

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

        flag = True

        If Not IsPostBack() Then


            objMemo.SetcboMemoCate_List(cboMemoCate)
            objMemo.SetCboMemoType(cboMemoType)

            SetCboUsersOnly(cboTo)
            SetCboUsersOnly(cboCC)
        Else
            detailtable = Session("detailtable")
        End If
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        If validatedata() Then
            Dim objMemo As New Memo
            Dim attatch As New Attatch
            Dim a As String, mmrno As String = "", dtltype As String
            Dim fullfilename As String = String.Empty, url As String = String.Empty
            a = txtMemoDetail.Text
            txtTest.Text = a


            If chkMemoFile.Checked Then
                dtltype = "FILE"
                If (FileUpload1.HasFile) Then
                    Dim allow_send_pic() As String = {".tiff", ".pjp", ".jfif", ".gif", ".svg", ".bmp", ".png", ".jpeg", ".svgz", ".jpg", ".webp", ".ico", ".xbm", ".dib", ".tif", ".pjpeg", ".avif", ".pdf"}
                    Dim Extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
                    Dim rootPath As String = "D:\\PTECAttatch\\IMG\\OPS_Memo\\"
                    Dim savePath As String '= "D:\\PTECAttatch\\IMG\\OPS_Memo\\"
                    Dim di As String = System.IO.Path.GetDirectoryName(FileUpload1.FileName)
                    Dim oldpath As String = di + FileUpload1.FileName
                    Try
                        If Array.IndexOf(allow_send_pic, Extension.ToLower()) = -1 Then
                            Dim scriptKey As String = "alert"
                            Dim javaScript As String = "alertWarning('Upload ได้เฉพาะ image,pdf');"
                            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                        Else
                            Dim fileName As String
                            fileName = attatch.GetAttatchName()
                            savePath = rootPath
                            savePath += fileName
                            savePath += Extension
                            Do While System.IO.File.Exists(savePath)
                                fileName = attatch.GetAttatchName()
                                savePath = rootPath
                                savePath += fileName
                                savePath += Extension
                            Loop
                            FileUpload1.SaveAs(savePath)
                            fullfilename += fileName
                            fullfilename += Extension

                            'attatch.Attatch_Save(fullfilename, Extension, "File Memo", savePath, Session("userid").ToString)

                            url = "http://vpnptec.dyndns.org:10280/OPS_Memo/" + fullfilename
                        End If
                    Catch ex As Exception
                        Dim scriptKey As String = "alert"
                        'Dim javaScript As String = "alert('" & ex.Message & "');"
                        Dim javaScript As String = "alertWarning('upload file fail');"
                        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                        GoTo endprocess
                    End Try
                End If
            ElseIf chkMemoDetail.Checked Then
                dtltype = "DETAIL"
            End If
            Try
                mmrno = objMemo.Memo_Save(mmrno, cboMemoCate.SelectedItem.Value, cboMemoType.SelectedItem.Value, txtSubject.Text.Trim, txtMemoOther.Text.Trim, txtMemoDetail.Text.Trim, dtltype, url, txtAmount.Text, Session("usercode").ToString)
                Dim arrTo As New ArrayList()
                Dim arrCC As New ArrayList()

                If cboTo.GetSelectedIndices.Count > 0 Then
                    For Each i As ListItem In cboTo.Items
                        If i.Selected = True Then
                            objMemo.Memo_SentTo_Save(mmrno, i.Value, "TO", Session("usercode").ToString)
                        End If
                    Next
                End If
                If cboCC.GetSelectedIndices.Count > 0 Then
                    For Each i As ListItem In cboCC.Items
                        If i.Selected = True Then
                            objMemo.Memo_SentTo_Save(mmrno, i.Value, "CC", Session("usercode").ToString)
                        End If
                    Next
                End If
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('upload file fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            Response.Redirect("../Memo/Memo2.aspx?MemoCode=" & mmrno)
        End If
endprocess:
    End Sub

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        'If txtTo.Text.Trim() = "" Then
        '    result = False
        '    msg = "กรุณาใส่ ปลายทาง"
        '    GoTo endprocess
        'End If
        If cboTo.GetSelectedIndices.Count = 0 Then
            result = False
            msg = "กรุณาใส่ " & lbcboTo.Text
            GoTo endprocess
        End If
        If txtSubject.Text.Trim() = "" Then
            result = False
            msg = "กรุณาใส่ " & lbtxtSubject.Text
            GoTo endprocess
        End If
        If cboMemoCate.SelectedItem.Value = 0 Then
            result = False
            msg = "กรุณาใส่ " & lbcboMemoCate.Text
            GoTo endprocess
        End If
        If cboMemoType.SelectedItem.Value = 0 Then
            result = False
            msg = "กรุณาใส่ " & lbcboMemoType.Text
            GoTo endprocess
        End If

        If chkMemoFile.Checked = False And chkMemoDetail.Checked = False Then
            result = False
            msg = "กรุณาแนบไฟล์ หรือ เขียน Memo"
            GoTo endprocess
        End If

        If chkMemoFile.Checked Then
            If Not (FileUpload1.HasFile) Then
                result = False
                msg = "กรุณาแนบไฟล์"
                GoTo endprocess
            End If
        ElseIf chkMemoDetail.Checked Then
            If txtMemoDetail.Text.Trim() = "" Then
                result = False
                msg = "กรุณาใส่ รายละเอียด Memo"
                GoTo endprocess
            End If
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

    Private Sub cboMemoType_PreRender(sender As Object, e As EventArgs) Handles cboMemoType.PreRender
        Dim objMemo As New Memo
        Dim yy As String = cboMemoType.SelectedValue
        cboMemoType.Items.Clear()

        objMemo.SetCboMemoType(cboMemoType)

        cboMemoType.Text = yy
    End Sub
End Class