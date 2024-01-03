Imports ClosedXML.Excel
Imports ExcelDataReader
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Web.Script.Serialization

Public Class approvalHO

    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public maintable As DataTable '= createdetailtable()
    Public detailtable As DataTable '= createdetailtable()
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public Allapprover As DataTable '= createtable()
    Public mydataset As DataSet
    Public PermissionOwner As DataSet '= createtable()
    Public usercode, username
    Public _cultureEnInfo As New Globalization.CultureInfo("en-US")
    Public tempname As String
    Public filename As String

    Public total As String
    Public allOwner As String
    Public at As String
    Public approver As String
    Public verifier As String
    Public now_action As String

    Dim md_code As String
    Dim fm_code As String
    Dim dm_code As String
    Dim sm_code As String
    Dim am_code As String

    Public verify As Boolean = False
    Public approval As Boolean = False
    Public confirmer As Boolean = False

    Public Approval_BF As String = ""

    Public confirmer_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbranch As New Branch
        Dim objapproval As New Approval
        Dim approvaldataset = New DataSet
        Dim attatch As New Attatch

        Dim statusid As Integer = 0
        Dim createby As Integer = 0


        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")

        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        'filename = Session("filename")
        If Not IsPostBack() Then
            maintable = createmaintable()
            detailtable = createdetailtable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()


            'objapproval.SetCboApproval(cboApproval, "")
            objapproval.SetCboApproval(cboApproval, Session("depid"), "ds")

            objbranch.SetComboBranch(cboBranch, "")
            setmaindefault()

            If Not Request.QueryString("approvalHOcode") Is Nothing Then
                Try

                    'objapproval.SetCboApproval(cboApproval)
                    attatch.SetCboMyfile(cboMyfile, Session("userid"))
                    approvaldataset = objapproval.ApprovalHO_Find(Request.QueryString("approvalHOcode"))
                    maintable = approvaldataset.Tables(0)
                    detailtable = approvaldataset.Tables(1)
                    AttachTable = approvaldataset.Tables(2)
                    CommentTable = approvaldataset.Tables(3)
                    Allapprover = approvaldataset.Tables(4)

                    If Allapprover IsNot Nothing Then
                        For Each row As DataRow In Allapprover.Rows
                            If row("Level_approval") Then
                                approvalby.Attributes.Add("class", "approvalby confirm") 'แสดง buged approval
                                lbApprovalby.Text = "อนุมัติโดย : " & row("ownercode").ToString
                                lbApprovalDate.Text = row("createdate").ToString
                            Else
                                verifyby.Attributes.Add("class", "verifyby confirm") 'แสดง buged verify
                                lbVerifyby.Text = "ตรวจสอบโดย : " & row("ownercode").ToString
                                lbVerifyDate.Text = row("createdate").ToString
                            End If

                        Next row
                    End If

                    If Not (String.IsNullOrEmpty(maintable.Rows(0).Item("confirmby").ToString) And String.IsNullOrEmpty(maintable.Rows(0).Item("confirmdate").ToString)) Then
                        confirmby.Attributes.Add("class", "confirmby confirm") 'แสดง buged verify
                        lbConfirmby.Text = "ยืนยันโดย : " & maintable.Rows(0).Item("confirmby").ToString
                        lbConfirmDate.Text = maintable.Rows(0).Item("confirmdate").ToString
                    End If
                    'CommentTable = approvaldataset.Tables(4)
                    'Session("detailtable_approvalHO") = detailtable


                    If Not Session("status_approvalHO") = "edit" Then
                        Session("status_approvalHO") = "read"
                    End If


                    statusid = maintable.Rows(0).Item("statusid")
                    createby = maintable.Rows(0).Item("ownerid")

                    confirmer_code += If(objapproval.ApprovalHOPermisstionConfirm(Request.QueryString("approvalHOcode")), "")

                    If (confirmer_code.IndexOf(Session("usercode").ToString) > -1) And
                    (maintable.Rows(0).Item("statusid") = 6) Then 'statusid = 6 รอยืนยันหักยอดขาย
                        Session("status_approvalHO") = "confirm"
                        confirmer = True
                    End If

                    chkuser(createby)
                    showdata(maintable)


                    If (maintable.Rows(0).Item("statusid") = 2) Then
                        PermissionOwner = chkPermissionApprovalHO(Request.QueryString("approvalHOcode"))

                        at = "วิ่งเส้น : " + PermissionOwner.Tables(0).Rows(0).Item("at").ToString
                        approver = "ผู้มีสิทธิอนุมัติ : " + PermissionOwner.Tables(0).Rows(0).Item("approver").ToString
                        verifier = "ผู้ตรวจ : " + PermissionOwner.Tables(0).Rows(0).Item("verifier").ToString
                        now_action = "ผู้ที่ต้องปฏิบัติงาน : " + PermissionOwner.Tables(0).Rows(1).Item("approver").ToString + PermissionOwner.Tables(0).Rows(1).Item("verifier").ToString
                    End If

                    If (md_code.ToString.IndexOf(usercode) > -1 Or
                    fm_code.ToString.IndexOf(usercode) > -1 Or
                    dm_code.ToString.IndexOf(usercode) > -1 Or
                    sm_code.ToString.IndexOf(usercode) > -1 Or
                    am_code.ToString.IndexOf(usercode) > -1) And
                    (maintable.Rows(0).Item("statusid") = 2) Then
                        Session("status_approvalHO") = "write"
                        'Dim SearchWithinThis As String = "ABCDEFGHIJKLMNOP"
                        'Dim SearchForThis As String = "DEF"
                        'Dim FirstCharacter As Integer = SearchWithinThis.IndexOf(SearchForThis)

                        For Each row As DataRow In PermissionOwner.Tables(0).Rows
                            If row("status").ToString = "now" Then
                                If row("approver").ToString.IndexOf("MD") > -1 Then
                                    If (md_code.IndexOf(usercode) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("FM") > -1 Then
                                    If (fm_code.IndexOf(usercode) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("DM") > -1 Then
                                    If (dm_code.IndexOf(usercode) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("SM") > -1 Then
                                    If (sm_code.IndexOf(usercode) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If row("approver").ToString.IndexOf("AM") > -1 Then
                                    If (am_code.IndexOf(usercode) > -1) Then
                                        approval = True
                                        GoTo endprocess
                                    End If
                                End If

                                If maintable.Rows(0).Item("statusid") = 2 Then
                                    If row("verifier").ToString.IndexOf("MD") > -1 Then
                                        If (md_code.IndexOf(usercode) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("FM") > -1 Then
                                        If (fm_code.IndexOf(usercode) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("DM") > -1 Then
                                        If (dm_code.IndexOf(usercode) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("SM") > -1 Then
                                        If (sm_code.IndexOf(usercode) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    ElseIf row("verifier").ToString.IndexOf("AM") > -1 Then
                                        If (am_code.IndexOf(usercode) > -1) Then
                                            verify = True
                                            GoTo endprocess
                                        End If
                                    End If
                                End If
                            End If

                        Next row
endprocess:

                    End If
                Catch ex As Exception
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "alertWarning('Find Fail')"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End Try
            End If

            Session("maintable_approvalHO") = maintable
            Session("detailtable_approvalHO") = detailtable
            Session("comment_approvalHO") = CommentTable
            Session("attatch_approvalHO") = AttachTable
        Else
            detailtable = Session("detailtable_approvalHO")
            maintable = Session("maintable_approvalHO")
            CommentTable = Session("comment_approvalHO")
            AttachTable = Session("attatch_approvalHO")

            Try
                statusid = maintable.Rows(0).Item("statusid")
                createby = maintable.Rows(0).Item("ownerid")

            Catch ex As Exception
                statusid = 0
                createby = 0
            End Try
            Dim target = Request.Form("__EVENTTARGET")
            If target = "addDetails" Then
                AddDetailsss()
            End If
        End If

        SetBtn(statusid, createby)
        BindData()
    End Sub

    Private Sub SetBtn(statusid As String, createby As Integer)
        Select Case statusid
            Case = "0" '0 : new

                btnSave.Enabled = True
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnExport.Visible = False

                txtHeadDetail.ReadOnly = False
                'ช่อง ปุ่ม เพิ่มรายการ
                groupFromAddDetail.Visible = True

                'ปุ่ม & status 
                badgestatus.Visible = False

                'กล่อง comment & attatch file
                card_comment.Visible = False
                card_attatch.Visible = False

                btnAddAttatch.Visible = False
            Case = "1" '1 : รอยืนยัน
                If createby = Session("userid") Then
                    btnSave.Enabled = False
                    btnUpdate.Enabled = True
                    btnConfirm.Enabled = True
                    btnCancel.Enabled = True

                    btnExport.Visible = True
                    Session("status_approvalHO") = "edit"

                    'ช่อง ปุ่ม เพิ่มรายการ
                    groupFromAddDetail.Visible = True
                Else
                    btnSave.Enabled = False
                    btnUpdate.Enabled = False
                    btnConfirm.Enabled = False
                    btnCancel.Enabled = False

                    Session("status_approvalHO") = "read"

                    'ช่อง ปุ่ม เพิ่มรายการ
                    groupFromAddDetail.Visible = False


                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "disbtndelete()"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End If


                txtHeadDetail.ReadOnly = False
                'ปุ่ม & status 
                badgestatus.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = True
            Case = "2" '2 : รออนุมัติ
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False

                btnExport.Visible = True
                If createby = Session("userid") Then
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If


                txtHeadDetail.ReadOnly = True
                'If approval And verify Then

                '    'ช่อง ปุ่ม เพิ่มรายการ
                '    groupFromAddDetail.Visible = True
                'Else

                '    groupFromAddDetail.Visible = False
                'End If


                'ช่อง ปุ่ม เพิ่มรายการ
                groupFromAddDetail.Visible = False

                'ปุ่ม & status 
                badgestatus.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "3" '3 : อนุมัติหักยอดขาย
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnExport.Visible = True

                txtHeadDetail.ReadOnly = True
                'ช่อง ปุ่ม เพิ่มรายการ
                groupFromAddDetail.Visible = False

                'ปุ่ม & status 
                badgestatus.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "4" '4 : ไม่อนุมัติ
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnExport.Visible = True

                txtHeadDetail.ReadOnly = True
                'ช่อง ปุ่ม เพิ่มรายการ
                groupFromAddDetail.Visible = False

                'ปุ่ม & status 
                badgestatus.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "5" '5 : ยกเลิก
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnExport.Visible = True

                txtHeadDetail.ReadOnly = True
                'ช่อง ปุ่ม เพิ่มรายการ
                groupFromAddDetail.Visible = False

                'ปุ่ม & status 
                badgestatus.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "6" '6 : รอยืนยันหักยอดขาย
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnExport.Visible = True

                txtHeadDetail.ReadOnly = True
                'If approval And verify Then

                '    'ช่อง ปุ่ม เพิ่มรายการ
                '    groupFromAddDetail.Visible = True
                'Else

                '    groupFromAddDetail.Visible = False
                'End If


                'ช่อง ปุ่ม เพิ่มรายการ
                groupFromAddDetail.Visible = False

                'ปุ่ม & status 
                badgestatus.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                btnAddAttatch.Visible = False

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Select
    End Sub
    Private Sub chkuser(userid As Integer)
        Dim objuser As New Users
        Dim NonPOPermissionTable As New DataTable
        Try
            NonPOPermissionTable = objuser.NonPOPermissionRead(userid)
            md_code = NonPOPermissionTable.Rows(0).Item("md_code")
            fm_code = NonPOPermissionTable.Rows(0).Item("fm_code")
            dm_code = NonPOPermissionTable.Rows(0).Item("dm_code")
            sm_code = NonPOPermissionTable.Rows(0).Item("sm_code")
            am_code = NonPOPermissionTable.Rows(0).Item("am_code")
            'chkuser_sub(md_code, userid)
            'chkuser_sub(fm_code, userid)
            'chkuser_sub(dm_code, userid)
            'chkuser_sub(sm_code, userid)
            'chkuser_sub(am_code, userid)
            allOwner = "[ md : " + md_code + "],[ fm : " + fm_code + "],[ dm : " + dm_code + "],[ sm : " + sm_code + "],[ am : " + am_code + "]"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Function createmaintable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("approvalHO_code", GetType(String))
        dt.Columns.Add("statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("detail", GetType(String))
        '---------------------------------------
        dt.Columns.Add("depid", GetType(String))
        dt.Columns.Add("secid", GetType(String))
        '---------------------------------------
        dt.Columns.Add("confirmby", GetType(String))
        dt.Columns.Add("confirmdate", GetType(String))
        '---------------------------------------
        dt.Columns.Add("ownerid", GetType(Integer))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("ownercode", GetType(String))


        Return dt
    End Function
    Private Function createdetailtable() As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("approvalid", GetType(String))
        dt.Columns.Add("approval", GetType(String))
        dt.Columns.Add("statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("branchid", GetType(String))
        dt.Columns.Add("branch", GetType(String))
        dt.Columns.Add("cost", GetType(Double))
        dt.Columns.Add("detail", GetType(String))
        dt.Columns.Add("approvalcode", GetType(String))
        dt.Columns.Add("cladvcode", GetType(String))
        dt.Columns.Add("cladvstatus", GetType(String))
        dt.Columns("id").AutoIncrement = True
        dt.Columns("Id").AutoIncrementSeed = 1

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
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        filename = FilePath
        Session(filename) = filename
        Try
            'For Each row As DataRow In detailtable.Rows
            'objgsm.Deduct_Save(row("branch"), row("detail"), row("amount"), row("date"), usercode)
            'Next row
            Using stream As FileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read)
                Dim excelReader As IExcelDataReader

                If Extension = ".xlsx" Then
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                Else
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
                End If

                Dim ds As DataSet = excelReader.AsDataSet()

                'GetDataToTable(pono, ds.Tables(0)) 'send data to data table
                'Dim dtview As DataView
                'Dim rvtb As DataTable

                'dtview = New DataView(ds.Tables("ImportD365-Payment"))

                'rvtb = dtview.ToTable(True, "Column25", "Column23", "Column2", "Column14", "Column5")

                ds.Tables(0).Rows.RemoveAt(0) 'remove row name colum

                Dim dr As DataRow
                Dim dt_temp As DataTable = createdetailtable()
                For Each r As DataRow In ds.Tables(0).Rows
                    dr = dt_temp.NewRow()
                    dr("approvalid") = cboApproval.Items.FindByText(r.Item("column0")).Value
                    dr("approval") = r.Item("column0")
                    dr("branchid") = cboBranch.Items.FindByText(r.Item("column1")).Value
                    dr("branch") = r.Item("column1")
                    dr("cost") = r.Item("column2")
                    dr("detail") = r.Item("column3")
                    dt_temp.Rows.Add(dr)

                Next

                Merge(dt_temp)
                'GetData()
                BindData()

            End Using
            'getexcelinterop(FilePath)

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('upload fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub Merge(dt_temp As DataTable)

        Dim dr As DataRow
        For Each r As DataRow In dt_temp.Rows
            Dim cntrow As Integer = detailtable.Rows.Count + 1
            While detailtable.Rows.IndexOf(detailtable.Select("id='" & cntrow & "'").FirstOrDefault()) > -1
                cntrow += 1
            End While

            dr = detailtable.NewRow()
            dr("id") = cntrow
            dr("approvalid") = r.Item("approvalid")
            dr("approval") = r.Item("approval")
            dr("branchid") = r.Item("branchid")
            dr("branch") = r.Item("branch")
            dr("cost") = r.Item("cost")
            dr("detail") = r.Item("detail")
            detailtable.Rows.Add(dr)

        Next
    End Sub

    Private Function SaveData() As Boolean

        Dim result As Boolean = True

        Dim scriptKey As String
        Dim javaScript As String


        Dim objgsm As New gsm
        Try
            'For Each row As DataRow In detailtable.Rows
            'objgsm.Deduct_Save(row("branch"), row("detail"), row("amount"), row("date"), usercode)
            'Next row
        Catch ex As Exception
            result = False
            scriptKey = "alert"
            javaScript = "alertWarning('" & ex.Message & "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

        Return result

    End Function
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.ServerClick
        Dim filename As String

        'Dim dateEng As DateTime = Convert.ToDateTime(myDateTime, _cultureEnInfo)
        'Me.lblEng.Text = dateEng.ToString("dd MMM yyyy", _cultureEnInfo)

        filename = Server.MapPath("~\temp\" & FileUpload1.FileName)
        tempname = Server.MapPath("~\temp\" & usercode & Now.ToString("yyyyMMddhhmmss", _cultureEnInfo) & Path.GetExtension(filename))
        'filename = Path.GetFileName(FileUpload1.FileName)
        If Me.FileUpload1.HasFile Then
            'Import_To_Grid(filename, ".xlsx", "No")
            Try
                FileUpload1.SaveAs(tempname)
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('SaveAs fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
            Import_To_Grid(tempname, Path.GetExtension(filename))
        End If
    End Sub

    Private Sub gvdata2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        'gvData.DataSource = Session("detailtable")
        gvData.PageIndex = e.NewPageIndex
        gvData.DataBind()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim confirmValue As String = Request.Form("delete_value")
        'Dim rawresp As String = "[{""id"":""1""},{""id"":""2""},{""id"":""3""},{""id"":""4""},{""id"":""5""},{""id"":""6""},{""id"":""7""}]"

        Dim result = JsonConvert.DeserializeObject(Of ArrayList)(confirmValue)
        Dim token As JToken
        Dim id
        Dim appcode
        If result IsNot Nothing Then
            For Each value As Object In result
                token = JObject.Parse(value.ToString())
                id = token.SelectToken("id")
                appcode = token.SelectToken("appcode")
                Try
                    If String.IsNullOrEmpty(appcode) Then
                        detailtable.Rows.Remove(detailtable.Select("id='" & id & "'")(0))
                    Else

                        Dim objapproval As New Approval
                        objapproval.deleteDetailbycrossid(id, Session("usercode"))
                        detailtable.Rows.Remove(detailtable.Select("id='" & id & "'")(0))

                    End If

                Catch ex As Exception
                    Dim scriptKey As String = "alert"
                    'Dim javaScript As String = "alert('" & ex.Message & "');"
                    Dim javaScript As String = "alertWarning('Delete fail');"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                    GoTo endprocess
                End Try
            Next value
            BindData()
        End If
endprocess:
    End Sub

    Private Sub AddDetailsss()
        Dim res As String = Request.Form("addDetail")
        Dim strarr() As String
        strarr = res.Split("}")
        res = strarr(0) + "}"
        Dim jss As New JavaScriptSerializer
        Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)


        'Dim rows As Integer = json("rows").Trim
        'Dim status As String = json("status").Trim
        Dim approvalid As String = json("approvalid").Trim
        Dim approval As String = json("approval").Trim
        Dim branchid As String = json("branchid").Trim
        Dim branch As String = json("branch").Trim
        Dim cost As Double = json("cost").Trim
        Dim detail As String = json("detail").Trim


        Dim cntrow As Integer = detailtable.Rows.Count + 1
        While detailtable.Rows.IndexOf(detailtable.Select("id='" & cntrow & "'").FirstOrDefault()) > -1
            cntrow += 1
        End While
        Try
            Dim row As DataRow
            row = detailtable.NewRow()
            row("id") = cntrow
            row("approvalid") = approvalid
            row("approval") = approval
            row("branchid") = branchid
            row("branch") = branch
            row("cost") = cost
            row("detail") = detail

            detailtable.Rows.Add(row)

            BindData()
            'checkunsave()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('adddetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    'Private Sub btnAddDetailsss_Click(sender As Object, e As EventArgs) Handles btnAddDetailsss.Click
    '    AddDetailsss()
    'End Sub

    Private Sub setmaindefault()
        txtHeadDetail.Text = ""
        txtApprovalHOcode.Visible = False
        badgestatus.Visible = False
        lbCreateby.Text = Session("usercode")
        lbCreateDate.Text = Now()
        Session("status_approvalHO") = "new"

    End Sub

    Private Sub BindData()
        Session("detailtable_approvalHO") = detailtable
        gvData.DataSource = detailtable
        gvData.DataBind()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        saveorupdate()
    End Sub

    Private Sub saveorupdate()
        If validatedata() Then
            'If Session("status") = "new" Then
            'If maintable.Rows.Count = 0 Then
            updatehead()
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
        Dim cnt_dt As Integer
        Try
            cnt_dt = detailtable.Rows.Count
        Catch ex As Exception
            cnt_dt = 0
        End Try

        If cnt_dt <= 0 Then
            result = False
            msg = "กรุณาใส่รายการ"
            GoTo endprocess
        End If

        If txtHeadDetail.Text.Trim() = "" Then
            result = False
            msg = "กรุณาใส่วัตถุประสงค์"
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
    Private Sub updatehead()
        Dim userid As Double

        userid = Session("userid")

        If maintable.Rows.Count > 0 Then
            'update
            With maintable.Rows(0)
                .Item("detail") = txtHeadDetail.Text
            End With
        Else
            'Dim amountpayBack As Double
            'Dim amountdedusctsell As Double

            Dim dr As DataRow
            dr = maintable.NewRow()
            dr("approvalHO_code") = txtApprovalHOcode.Text
            dr("detail") = txtHeadDetail.Text
            maintable.Rows.Add(dr)
        End If
        Session("maintable_approvalHO") = maintable
    End Sub
    Private Sub Save()
        Dim objapproval As New Approval
        Dim ahono As String = ""

        ahono = txtApprovalHOcode.Text
        Try
            ahono = objapproval.SaveApprovalHO(ahono, maintable, detailtable, Session("usercode"))
            'txtApprovalHOcode.Text = ahono
            Session("status_approvalHO") = "edit"


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & ahono)
endprocess:
    End Sub

    Private Sub showdata(mytable As DataTable)
        Dim objbranch As New Branch
        With mytable.Rows(0)

            txtApprovalHOcode.Visible = True
            txtApprovalHOcode.Text = .Item("approvalHO_code").ToString
            txtHeadDetail.Text = .Item("detail").ToString

            lbCreateby.Text = .Item("ownercode").ToString
            lbCreateDate.Text = .Item("createdate").ToString

            badgestatus.Visible = True
            Select Case .Item("statusid").ToString
                Case = "1" '1 : รอยืนยัน
                    badgestatus.Attributes.Add("class", "badgestatus p-1 pl-4 pr-4 bg-info")
                Case = "2" '2 : รออนุมัติ
                    badgestatus.Attributes.Add("class", "badgestatus p-1 pl-4 pr-4 bg-warning")
                Case = "3" '3 : อนุมัติหักยอดขาย
                    badgestatus.Attributes.Add("class", "badgestatus p-1 pl-4 pr-4 bg-success")
                Case = "4" '4 : ไม่อนุมัติ
                    badgestatus.Attributes.Add("class", "badgestatus p-1 pl-4 pr-4 bg-danger")
                Case = "5" '5 : ยกเลิก
                    badgestatus.Attributes.Add("class", "badgestatus p-1 pl-4 pr-4 bg-danger")
                Case = "6" '6 : รอยืนยันหักยอดขาย
                    badgestatus.Attributes.Add("class", "badgestatus p-1 pl-4 pr-4 bg-warning")
            End Select
            statusnonpo.Text = .Item("statusname").ToString


        End With
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        saveorupdate()
    End Sub

    Private Sub approvalHO_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim cost As Double
        Try
            cost = Convert.ToDouble(detailtable.Compute("SUM(cost)", String.Empty))
        Catch ex As Exception
            cost = 0
        End Try

        total = String.Format("{0:n2}", cost)

        If detailtable.Rows.Count <= 0 Then
            badgestatus.Visible = False
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue.IndexOf("Yes") > -1 Then
            Confirm(Request.QueryString("approvalHOcode").ToString.Trim, Session("usercode"))
            confirmValue = "No"
        End If
    End Sub
    Private Sub Confirm(approvalhocode As String, usercode As String)
        Dim objapproval As New Approval
        Dim objnonpo As New NonPO

        Try
            objapproval.ApprovalHO_Confirm(approvalhocode, usercode)
            Session("status_approvalHO") = "read"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Confirm fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
endprocess:
    End Sub
    '    Private Sub btnPass_ServerClick(sender As Object, e As EventArgs) Handles btnPass.ServerClick
    '        Dim objapproval As New Approval
    '        Try
    '            objapproval.ApprovalHO_Pass(Request.QueryString("approvalHOcode"), Session("usercode"))
    '            Session("status_approvalHO") = "read"

    '        Catch ex As Exception
    '            Dim scriptKey As String = "alert"
    '            'Dim javaScript As String = "alert('" & ex.Message & "');"
    '            Dim javaScript As String = "alertWarning('Verify fail');"
    '            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '            GoTo endprocess
    '        End Try
    '        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
    'endprocess:
    '    End Sub

    Private Function chkPermissionApprovalHO(approvalhocode As String) As DataSet
        Dim objapproval As New Approval
        Dim ahoPermission As New DataSet
        Try
            ahoPermission = objapproval.ApprovalHOPermission(approvalhocode)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
        Return ahoPermission
    End Function
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Dim objapproval As New Approval

        Try
            objapproval.ApprovalHO_Cancel(Request.QueryString("approvalHOcode"), Session("usercode"))
            Session("status_approvalHO") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
endprocess:
    End Sub

    Private Sub btnDisApproval_Click(sender As Object, e As EventArgs) Handles btnDisApproval.Click
        Dim objapproval As New Approval
        Try
            objapproval.ApprovalHO_NotAllow(Request.QueryString("approvalHOcode"), Session("usercode"))
            Session("status_approvalHO") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('DisApproval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
endprocess:
    End Sub

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim objapproval As New Approval

        Try
            objapproval.ApprovalHO_Allow(Request.QueryString("approvalHOcode"), Session("usercode"))
            Session("status_approvalHO") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
endprocess:
    End Sub

    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim objapproval As New Approval

        Try
            objapproval.ApprovalHO_Verify(Request.QueryString("approvalHOcode"), Session("usercode"))
            Session("status_approvalHO") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Verify fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
endprocess:
    End Sub
    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        For i = 0 To detailtable.Rows.Count - 1
            If String.IsNullOrEmpty(detailtable.Rows(i).Item("approvalcode")) Then
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('กรุณาบันทึกรายการ');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End If
        Next i
        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("approvalHOcode"), txtComment.Text.Trim(), Session("userid"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))

endprocess:
    End Sub

    Private Sub btnConfirmerApproval_Click(sender As Object, e As EventArgs) Handles btnConfirmerApproval.Click
        Dim objapproval As New Approval

        Try
            objapproval.ApprovalHO_AllowDeductSell(Request.QueryString("approvalHOcode"), Session("usercode"))
            Session("status_approvalHO") = "read"

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../approval/approvalHO.aspx?approvalHOcode=" & Request.QueryString("approvalHOcode"))
endprocess:
    End Sub

    Private Sub ExportToExcel(mydatatable As DataTable, usercode As String, closedate As String)

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(mydatatable, "General_journal")
            'wb.Worksheets.Add(mydataset.Tables(1), "Payment")

            Dim filename As String = usercode & "_" & closedate & "_" & Date.Now.ToString
            Dim encode As String
            'If (maintable.Rows(0).Item("statusid") = 7) Then
            '    encode = "(preview)"
            'Else
            '    encode = "(final)"
            'End If
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(filename)
            encode = Convert.ToBase64String(byt)

            'Dim decode As String
            'Dim b As Byte() = Convert.FromBase64String(encode)
            'decode = System.Text.Encoding.UTF8.GetString(b)

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & encode & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

        End Using
    End Sub
    Private Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        Dim createdate As String
        createdate = Date.Now
        Try
            ExportToExcel(detailtable, Session("usercode"), createdate)
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('export fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class