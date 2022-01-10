Public Class JobsManageStatus
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public maintable As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usercode, username, dep As String
        usercode = Session("usercode")
        username = Session("username")
        dep = Session("dep")

        Dim objjob As New jobs


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        If Session("menulist") Is Nothing Then
            If Not String.IsNullOrEmpty(usercode) Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            End If
        Else
            menutable = Session("menulist")
        End If

        If Not IsPostBack() Then

            objjob.SetCboJobStatusmainList(cboStatus)
            'objdepart.SetCboDepartment(cboDepart, 0)
            'objsection.SetCboSection(cboSection, 0)
            'objposition.SetCboPosition(cboPosition)
            'If Session("maintable_usermanage") IsNot Nothing Then
            '    showdata(Session("maintable_usermanage"))
            'End If

            If Not Request.QueryString("jobno") Is Nothing Then
                Session("managestatus") = "edit"
                FindJobNo(Request.QueryString("jobno"))


            Else
                Session("managestatus") = "new"
            End If
        Else

            maintable = Session("maintable_usermanage")
        End If
        SetMenu()

    End Sub


    Private Sub SetMenu()
        Select Case Session("managestatus")
            Case = "new"
                btnNew.Enabled = True
                btnSave.Enabled = False
            Case = "edit"
                btnNew.Enabled = True
                btnSave.Enabled = True
        End Select
    End Sub

    Private Sub FindJobNo(jobno As String)
        Dim job As New jobs
        Dim mydataset As DataSet
        'Dim maintable As DataTable
        Dim usercode As String = Session("usercode")
        Session("jobno") = jobno
        'itemtable
        Try
            mydataset = job.Find(usercode, jobno)
            maintable = mydataset.Tables(0)
            'itemtable = mydataset.Tables(1)
            'Session("itemtable") = itemtable

            If maintable.Rows.Count > 0 Then
                showdata(maintable)
                Session("maintable") = mydataset.Tables(0)
            End If
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alertWarning('Find Fail')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub showdata(mytable As DataTable)
        With mytable.Rows(0)
            txtJobno.Text = .Item("jobno")
            txtStatus.Text = .Item("statusname")

        End With
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Response.Redirect("../ADMIN/JobsManageStatus.aspx")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validatedata() Then
            Save()
        End If
    End Sub

    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        If String.IsNullOrEmpty(txtJobno.Text) Then
            result = False
            msg = "กรุณาใส่เลขใบงาน"
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
        Dim objjob As New jobs
        Try
            objjob.setStatus(Request.QueryString("jobno"), cboStatus.SelectedItem.Value)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../ADMIN/JobsManageStatus.aspx?jobno=" & Request.QueryString("jobno"))
endprocess:
    End Sub

End Class