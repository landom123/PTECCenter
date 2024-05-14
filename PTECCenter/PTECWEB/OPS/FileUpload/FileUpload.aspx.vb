Imports System.Drawing

Public Class FileUpload1
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
        If Not IsPostBack() Then
            detailtable = createdetailtable()
            txtUserName.Text = Session("username")
            detailtable = attatch.Attatch_Find(Session("userid"))
            Session("detailtable") = detailtable
            BindData()
        Else
            detailtable = Session("detailtable")
            BindData()
        End If

    End Sub
    Private Sub BindData()
        gvRemind.DataSource = detailtable
        gvRemind.DataBind()
    End Sub
    Private Sub gvRemind_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRemind.PageIndexChanging
        gvRemind.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim attatch As New Attatch
        Dim fullfilename As String = String.Empty
        If (FileUpload1.HasFile) Then
            Dim allow_send_pic() As String = {".tiff", ".pjp", ".jfif", ".gif", ".svg", ".bmp", ".png", ".jpeg", ".svgz", ".jpg", ".webp", ".ico", ".xbm", ".dib", ".tif", ".pjpeg", ".avif", ".pdf"}
            Dim Extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
            Dim rootPath As String = "D:\\PTECAttatch\\IMG\\OPS_Fileupload\\"
            Dim savePath As String '= "D:\\PTECAttatch\\IMG\\OPS_Fileupload\\"
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

                    attatch.Attatch_Save(fullfilename, Extension, txtDetail.Text.Trim(), savePath, Session("userid").ToString)

                    txtDetail.Text = ""
                    detailtable = attatch.Attatch_Find(Session("userid"))
                    Session("detailtable") = detailtable
                    BindData()
                    'img1.Attributes.Add("src", a)
                    Dim scriptKey As String = "alert"
                    'Dim javaScript As String = "alert('" & ex.Message & "');"
                    Dim javaScript As String = "alertSuccess();"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End If
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('upload file fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        Else
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('กรุณาแนบไฟล์');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("approvalcode", GetType(Integer))
        dt.Columns.Add("path", GetType(String))
        dt.Columns.Add("attatchdetail", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("attatchextension", GetType(String))
        dt.Columns.Add("createdate", GetType(String))

        Return dt
    End Function
End Class