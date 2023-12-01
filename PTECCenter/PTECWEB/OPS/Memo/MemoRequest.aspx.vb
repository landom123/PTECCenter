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

        Dim objMemo As New Memo
        objMemo.SetCboMemoType(cboMemoType)
        If Not IsPostBack() Then


            SetCboUsersOnly(cboTo)
            SetCboUsersOnly(cboCC)
        Else
            detailtable = Session("detailtable")
        End If
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        If validatedata() Then
            Dim a As String
            a = txtMemoDetail.Text
            txtTest.Text = a

            If cboTo.GetSelectedIndices.Count > 0 Then
                For Each i As ListItem In cboTo.Items
                    If i.Selected = True Then
                        Dim idjtn As String = i.Value
                    End If
                Next
            End If


        End If

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
End Class