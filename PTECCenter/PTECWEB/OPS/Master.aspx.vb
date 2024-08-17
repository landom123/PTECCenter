Imports System.Web.Services

Public Class frmMasters
    Inherits System.Web.UI.Page

    Public Shared objStatus As String
    Public Shared newUpdateItem As Boolean
    Public menutable As DataTable
    Public Shared detailtable As DataTable = createdetailtable()
    Public Shared maintable As DataTable = createmaintable()
    'Public itemtable As DataTable = createdetailtable()
    Public usercode As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")

        'chkuser(usercode)
        Session("detailtable") = detailtable
        Session("status") = objStatus
        Session("maintable") = maintable


        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        '######## START Check Permission page  ########
        Dim total As Integer = menutable.Rows.Count - 1
        Dim is_allowThisPage As Boolean = False
        Dim urlCurrent As String = Request.Url.ToString().ToLower()
        For i = 0 To total
            Dim frmMenuUrl As String = menutable.Rows(i).Item("menu_url").ToString.Replace("\", "/").Replace("~", "").ToLower()
            If Not String.IsNullOrEmpty(frmMenuUrl) Then
                If (urlCurrent.IndexOf(frmMenuUrl) > -1) Then
                    is_allowThisPage = True
                    Exit For
                End If
            End If
        Next
        If Not is_allowThisPage Then
            Response.Redirect("~/403.aspx")
        End If
        '######## END Check Permission page  ########

        Dim objjob As New jobs

        If Not IsPostBack() Then
            detailtable = createdetailtable()
            maintable = createmaintable()
            Session("detailtable") = detailtable
            Session("maintable") = maintable
            ClearText()
            SearchGroup(txtGroupID.Text, txtJobsGroupName.Text, txtGroupNumber.Text)

            SetMenu()
        Else
            objStatus = Session("status")

            detailtable = Session("detailtable")
            maintable = Session("maintable")
            If String.IsNullOrEmpty(Session("groupid")) Or newUpdateItem Then
                If maintable.Rows.Count > 0 Then
                    Session("groupid") = maintable.Rows(0).Item("GroupID")
                    showdata(maintable)
                    SetMenu()
                    newUpdateItem = False
                End If
            End If

        End If

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        objStatus = "new"
        Session("status") = "new"
        'detailtable.Clear()
        txtGroupID.Text = ""
        txtJobsGroupName.Text = ""
        txtGroupNumber.Text = ""
        maintable.Rows.Clear()
        Session("maintable") = maintable
        'ClearText()
        'BindData()
        SetMenu()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchGroup(txtGroupID.Text, txtJobsGroupName.Text, txtGroupNumber.Text)
        Session("detailtable") = detailtable
    End Sub

    Private Sub BindData()
        gvJobsGroup.DataSource = detailtable
        gvJobsGroup.DataBind()
    End Sub

    Private Sub chkuser(usercode As String)
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String
        Dim result As Boolean = False


        Dim objmenu As New menu

        Try
            result = objmenu.chkuser(usercode, "O001")
            If result = False Then
                javaScript = "alert('คุณไม่มีสิทธิ์ใช้งาน กรุณาติดต่อ Admin');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                Response.Redirect("~\default.aspx")
            End If
        Catch ex As Exception
            javaScript = "alert('" + ex.Message + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try


    End Sub
    Private Shared Function createdetailtable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("GroupID", GetType(String))
        dt.Columns.Add("GroupDescription", GetType(String))
        dt.Columns.Add("GroupNumber", GetType(String))

        Return dt
    End Function

    Private Shared Function createmaintable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("GroupID", GetType(String))
        dt.Columns.Add("GroupDescription", GetType(String))
        dt.Columns.Add("GroupNumber", GetType(String))
        dt.Columns.Add("UpdateUser", GetType(String))
        dt.Columns.Add("UpdateDate", GetType(DateTime))
        dt.Columns.Add("CreateUser", GetType(String))
        dt.Columns.Add("CreateDate", GetType(DateTime))

        Return dt
    End Function

    Private Sub showdata(mytable As DataTable)
        With mytable.Rows(0)
            txtGroupID.Text = .Item("GroupID").ToString
            txtJobsGroupName.Text = .Item("GroupDescription").ToString
            txtGroupNumber.Text = .Item("GroupNumber").ToString
            txtUpdateUser.Text = .Item("UpdateUser").ToString
            txtUpdateDate.Text = .Item("UpdateDate").ToString
            txtCreateUser.Text = .Item("CreateUser").ToString
            txtCreateDate.Text = .Item("CreateDate").ToString
        End With
    End Sub
    Private Sub gvJobsGroup_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvJobsGroup.PageIndexChanging
        gvJobsGroup.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub ClearText()
        txtGroupID.Text = ""
        txtJobsGroupName.Text = ""
        txtGroupNumber.Text = ""
        maintable.Rows.Clear()
        detailtable.Rows.Clear()
        Session("detailtable") = detailtable
        Session("maintable") = maintable
        Session("status") = "new"
        Session("groupid") = Nothing
    End Sub

    Private Sub SetMenu()
        Select Case objStatus
            Case = "new"
                txtGroupID.Enabled = True
                txtJobsGroupName.Enabled = True
                txtGroupNumber.Enabled = True
                btnSave.Enabled = True
                btnSearch.Enabled = True
            Case = "update"
                btnSave.Enabled = True
                btnSearch.Enabled = False
                txtGroupID.Enabled = False
        End Select


    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim confirmValue As String = Request.Form("confirm_value")
        If validatedata() Then
            If confirmValue = "Yes" Then
                If objStatus = "update" Then
                    If (Not Equals(maintable.Rows(0).Item("GroupDescription").ToString, Me.txtJobsGroupName.Text)) Or
                        (Not Equals(maintable.Rows(0).Item("GroupNumber").ToString, Me.txtGroupNumber.Text)) Then
                        updateGroup(txtGroupID.Text, txtJobsGroupName.Text, txtGroupNumber.Text)
                    End If

                Else
                    Save(txtGroupID.Text, txtJobsGroupName.Text, txtGroupNumber.Text)
                End If
                confirmValue = "No"
            Else
                'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alertSuccess()", True)
            End If
        End If
    End Sub
    Private Function validatedata() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        If String.IsNullOrEmpty(txtGroupID.Text) Then
            result = False
            msg = "กรุณาระบุรหัสกลุ่มหมวดงาน"
            GoTo endprocess
        End If

        If String.IsNullOrEmpty(txtJobsGroupName.Text) Then
            result = False
            msg = "กรุณาระบุชื่อหมวดงาน"
            GoTo endprocess
        End If

        If String.IsNullOrEmpty(txtGroupNumber.Text) Then
            result = False
            msg = "กรุณาระบุเลขหมวดงาน"
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
    Private Sub Save(groupid As String, groupdescription As String, groupnumber As String)
        Dim job As New jobs
        groupid = groupid.Trim()
        groupdescription = groupdescription.Trim()
        groupnumber = groupnumber.Trim()

        Try
            job.AddGroup(groupid, groupdescription, groupnumber, Session("usercode"))
            ClearText()
            detailtable.Clear()
            detailtable = job.FindGroup(txtGroupID.Text.Trim(), txtJobsGroupName.Text.Trim(), txtGroupNumber.Text.Trim())
            Session("detailtable") = detailtable
            BindData()
            ClientScript.RegisterStartupScript(Me.[GetType](), "success", "alertSuccess()", True)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alertWarning('" + ex.StackTrace + "');"
            Dim javaScript As String = "alertWarning('Fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub updateGroup(groupid As String, groupdescription As String, groupnumber As String)
        Dim job As New jobs
        groupid = groupid.Trim()
        groupdescription = groupdescription.Trim()
        groupnumber = groupnumber.Trim()

        Try
            job.updateGroup(groupid, groupdescription, groupnumber, Session("usercode"))
            maintable.Clear()
            maintable = job.FindGroup(groupid, groupdescription, groupnumber)
            Session("maintable") = maintable
            showdata(maintable)
            Dim rows() As DataRow = detailtable.Select("GroupID = '" + groupid + "'")
            Dim searchedValue As Integer
            If rows.Count > 0 Then
                searchedValue = detailtable.Rows.IndexOf(rows(0))
                detailtable.Rows(searchedValue).Item("GroupDescription") = groupdescription
                detailtable.Rows(searchedValue).Item("GroupNumber") = groupnumber
                Session("detailtable") = detailtable
                BindData()
            End If

            ClientScript.RegisterStartupScript(Me.[GetType](), "success", "alertSuccess()", True)
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alertWarning('" + ex.StackTrace + "');"
            Dim javaScript As String = "alertWarning('Fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub SearchGroup(groupid As String, groupdescription As String, groupnumber As String)
        Dim job As New jobs
        groupid = groupid.Trim()
        groupdescription = groupdescription.Trim()
        groupnumber = groupnumber.Trim()
        Try
            detailtable = job.FindGroup(groupid, groupdescription, groupnumber)
            Session("detailtable") = detailtable
            BindData()

        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "alert('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
            ClearText()
        End Try

    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Function updateGroupById(ByVal groupId As String)

        Dim job As New jobs
        Try
            maintable = job.FindGroup(groupId, "", "")

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In maintable.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In maintable.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
            Next
            newUpdateItem = True
            objStatus = "update"
            Return serializer.Serialize(row)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function deleteGroupById(ByVal groupId As String)

        Dim job As New jobs
        Try
            job.deleteGroupByGroupId(groupId)
        Catch ex As Exception
            Return "fail"
        End Try
        Return "success"

    End Function


End Class