Imports System.Web.Script.Serialization

Public Class JobsFollowup
    Inherits System.Web.UI.Page
    Dim objStatus As String
    Dim jobno As String
    Dim jobdetailid As Integer
    Public menutable As DataTable
    Public maintable As DataTable
    Public stepsuppilertable As DataTable
    Public assessmenttable As DataTable
    Public ratetable As DataTable
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()

    Public followuptable As DataTable = CreateFollowup()
    Dim usercode, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim objjob As New jobs
        Dim objsupplier As New Supplier
        Dim attatch As New Attatch

        username = Session("username")
        usercode = Session("usercode")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If
        If Session("menulist") Is Nothing Then
            menutable = LoadMenu(usercode)
            Session("menulist") = menutable
        Else
            menutable = Session("menulist")
        End If

        objStatus = "edit"
        Session("jobno") = Request.QueryString("jobno")
        Session("jobdetailid") = Request.QueryString("jobdetailid")
        jobno = Session("jobno")
        jobdetailid = Session("jobdetailid")
        txtJobno.Text = jobno


        If Not IsPostBack Then
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()
            stepsuppilertable = createtablestepsuppiler()
            assessmenttable = createtableassessment()
            Clear()

            objjob.SetCboCloseType(cboCloseType)
            objjob.SetCboCloseCategory(cboCloseCategory)
            objsupplier.SetCboSupplier(cboSupplier)
            'objsupplier.SetCboSupplierByComid(cboVendor, "1") '1 PURE
            objjob.SetCboJobCenterList(cboJobCenter)
            SetCboJobType(cboJobType, usercode)
            attatch.SetCboMyfile(cboMyfile, Session("userid"))
            FindJob(jobno, jobdetailid)
            If maintable.Rows(0).Item("owner") > 0 Then
                objjob.SetCboJobStatusList(cboStatus)
            Else
                Dim dt As New DataTable

                dt.Columns.Add("statusid", GetType(Integer))
                dt.Columns.Add("name", GetType(String))
                dt.Rows.Add(11, "confirm")
                cboStatus.DataSource = dt
                cboStatus.DataValueField = ("statusid")
                cboStatus.DataTextField = ("name")
                cboStatus.DataBind()
            End If

            Session("maintable") = maintable
            Session("followuptable") = followuptable
            Session("stepsuppilertable") = stepsuppilertable
            Session("assessmenttable") = assessmenttable
            Session("comment_jobdetail") = CommentTable
            Session("attatch_jobdetail") = AttachTable

        Else
            followuptable = Session("followuptable")
            maintable = Session("maintable")
            stepsuppilertable = Session("stepsuppilertable")
            assessmenttable = Session("assessmenttable")
            CommentTable = Session("comment_jobdetail")
            AttachTable = Session("attatch_jobdetail")

        End If

        setBtn()

        'txtCreateBy.Text = Session("jobtypeid")
    End Sub

    Private Sub setBtn()
        If maintable IsNot Nothing Then
            If maintable.Rows.Count > 0 Then
                If maintable.Rows(0).Item("owner") > 0 Then

                    'Detail
                    btnSave.Visible = True
                    btnConfirm.Visible = False
                    If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then
                        btnSave.Enabled = True
                        btnEditDetail.Visible = True

                        cardfour.Attributes.Add("style", "display:none;")
                    Else '= ปิดงาน
                        btnSave.Enabled = False
                        btnEditDetail.Visible = False

                        cardfour.Attributes.Remove("readonly")
                    End If

                    'Follow UP
                    fromUpdateFollowup.Visible = True

                    'Supplier
                    btnSentSupplier.Visible = True
                    btnPrint.Visible = True
                    If maintable.Rows(0).Item("supplierid") = 0 Then
                        btnSentSupplier.Enabled = False
                        btnPrint.Enabled = False
                    Else
                        btnSentSupplier.Enabled = True
                        btnPrint.Enabled = True
                        Dim rows() As DataRow = stepsuppilertable.Select("stepdate is not null")
                        If rows.Count > 0 Then
                            btnSentSupplier.Enabled = False
                        End If

                    End If


                    'Analy
                    btnDataAnalyCategory.Visible = True
                    btnDataAnalyGroupType.Visible = True

                    'Rating
                    btnSubmitRate.Visible = False

                ElseIf String.Equals(Session("username"), maintable.Rows(0).Item("createby")) Then
                    'Detail
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnEditDetail.Visible = False
                    If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then
                        btnConfirm.Enabled = True

                        'Rating
                        btnSubmitRate.Visible = True
                    Else
                        btnConfirm.Enabled = False

                        'Rating
                        btnSubmitRate.Visible = False
                    End If

                    'Follow UP
                    fromUpdateFollowup.Visible = False


                    'Analy
                    btnDataAnalyCategory.Visible = False
                    btnDataAnalyGroupType.Visible = False

                    'Supplier
                    btnSentSupplier.Visible = False
                    btnPrint.Visible = False
                Else
                    'Detail
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnEditDetail.Visible = False
                    btnConfirm.Enabled = False

                    'Follow UP
                    fromUpdateFollowup.Visible = False

                    'Supplier
                    btnSentSupplier.Visible = False
                    btnPrint.Visible = False


                    'Analy
                    btnDataAnalyCategory.Visible = False
                    btnDataAnalyGroupType.Visible = False


                    'Rating
                    btnSubmitRate.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub Clear()
        followuptable.Rows.Clear()
        cboStatus.ClearSelection()
        txtDetailFollow.Text = ""
        lblCreateBy.Text = username
        lblCreateDate.Text = Now.ToString
    End Sub
    Private Function CreateFollowup() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("statusid", GetType(Integer))
        dt.Columns.Add("statusname", GetType(String))
        dt.Columns.Add("userid", GetType(Double))
        dt.Columns.Add("details", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(DateTime))
        dt.Columns.Add("username", GetType(String))

        Return dt
    End Function
    Private Function createtablestepsuppiler() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("stepid", GetType(Integer))
        dt.Columns.Add("steptitle", GetType(String))
        dt.Columns.Add("actived", GetType(Integer))
        dt.Columns.Add("stepdate", GetType(String))


        Return dt
    End Function
    Private Function createtableassessment() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("rowid", GetType(Integer))
        dt.Columns.Add("topic_id", GetType(String))
        dt.Columns.Add("topic_detail", GetType(String))
        dt.Columns.Add("rate", GetType(Integer))
        dt.Columns.Add("message", GetType(String))
        dt.Columns.Add("topicgroup_id", GetType(Integer))
        dt.Columns.Add("type", GetType(String))


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
    Private Sub FindJob(jobno As String, jobdetailid As Double)
        Dim job As New jobs
        Dim mydataset As DataSet
        ' Dim jobtable As DataTable
        Session("jobno") = jobno

        'itemtable
        Try
            mydataset = job.FindFollowup(jobno, jobdetailid, usercode)
            maintable = mydataset.Tables(0)
            showjobdata(mydataset.Tables(0))
            followuptable = mydataset.Tables(1)
            stepsuppilertable = mydataset.Tables(2)
            assessmenttable = mydataset.Tables(3)
            AttachTable = mydataset.Tables(4)
            CommentTable = mydataset.Tables(5)

            Session("maintable") = maintable
            Session("followuptable") = followuptable
            Session("stepsuppilertable") = stepsuppilertable
            Session("assessmenttable") = assessmenttable
            Session("comment_jobdetail") = CommentTable
            Session("attatch_jobdetail") = AttachTable


        Catch ex As Exception

            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub showjobdata(mytable As DataTable)

        Dim objjob As New jobs

        With mytable.Rows(0)
            txtDocDate.Text = .Item("jobdate")
            txtOwner.Text = .Item("jobowner")
            txtBranch.Text = .Item("branch")
            txtDepartment.Text = .Item("department")
            txtSection.Text = .Item("section")
            txtJobType.Text = .Item("jobtype")
            cboJobType.SelectedIndex = cboJobType.Items.IndexOf(cboJobType.Items.FindByValue(.Item("jobtypeid")))
            txtAssetCode.Text = .Item("assetcode")
            txtAsset.Text = .Item("assetcode")
            txtAssetName.Text = .Item("assetname")
            txtQuantity.Text = .Item("quantity")
            txtUnit.Text = .Item("unit")
            txtCost.Text = .Item("cost")
            txtcosts.Text = .Item("cost")
            cboJobCenter.SelectedIndex = cboJobCenter.Items.IndexOf(cboJobCenter.Items.FindByValue(.Item("jobcenterid")))
            cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(.Item("supplierid")))
            txtSupplier.Text = .Item("supplier")
            txtDetail.Text = .Item("details")

            txtCloseType.Text = .Item("JobCloseType_name")
            txtCloseCategory.Text = .Item("CategoryName")


            cboCloseType.SelectedIndex = cboCloseType.Items.IndexOf(cboCloseType.Items.FindByValue(.Item("jobclosetypeid")))
            cboCloseCategory.SelectedIndex = cboCloseCategory.Items.IndexOf(cboCloseCategory.Items.FindByValue(.Item("jobclosecategoryid")))

            lbjobscode.Text = .Item("jobno")
            badgeStatus.InnerText = .Item("followup_status")


            objjob.SetJobCateList(cboJobCate, .Item("depid_Jobtype"))

            cboJobCate.SelectedIndex = cboJobCate.Items.IndexOf(cboJobCate.Items.FindByValue(.Item("catecode")))
            txtCateName.Text = .Item("catename")

            objjob.SetJobItemList(cboJobItems, cboJobCate.SelectedItem.Value.ToString, "")


            cboJobItems.SelectedIndex = -1

            txtItems.Text = .Item("itemsname")
        End With
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateUpdate() Then
            saveFollowup()
        End If
    End Sub
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If ValidateUpdate() Then
            saveFollowup()
        End If
    End Sub
    Private Function ValidateUpdate() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        If cboStatus.SelectedItem.Value = 0 Then
            result = False
            msg = "กรุณาเลือกสถานะ"
            GoTo endprocess
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function

    Private Function ValidateEditDetail() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""

        Dim ass As New Assets
        Dim mydataset As DataSet
        Dim assCode As String = txtAsset.Text
        If Not String.IsNullOrEmpty(assCode) Then

            Try
                mydataset = ass.Find(txtAsset.Text)
                If mydataset.Tables(0).Rows.Count > 0 Then
                    txtAssetName.Text = mydataset.Tables(0).Rows(0).Item("name")
                Else
                    result = False
                    msg = "ไม่มีรหัสทรัพสินนี้"
                    GoTo endprocess
                End If
            Catch ex As Exception
                result = False
                msg = "Find fail"
                GoTo endprocess
            End Try
        End If
endprocess:

        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Sub saveFollowup()
        Dim statusid As Integer = cboStatus.SelectedItem.Value
        Dim details As String = txtDetailFollow.Text

        Dim objjob As New jobs

        Try
            objjob.Followup_Save(jobno, jobdetailid, statusid, details, usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'FindJob(jobno, jobdetailid)

        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("jobs.aspx?jobno=" & jobno)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Response.Redirect("jobs_Close.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'update cost and supplier

        If ValidateEditDetail() Then

            Dim supplierid As Double = cboSupplier.SelectedItem.Value
            Dim jobType As Integer = cboJobType.SelectedItem.Value
            Dim assCode As String = txtAsset.Text
            'Dim jobCenter As Integer = cboJobCenter.SelectedItem.Value
            Dim closetypeid As Double = cboCloseType.SelectedItem.Value
            Dim closecategory As Integer = cboCloseCategory.SelectedItem.Value

            Dim cost As Double

            Try
                cost = Double.Parse(txtCost.Text)
            Catch
                cost = 0
            End Try

            Dim objjob As New jobs

            Try
                'objjob.UpdateSupplierandCost(jobno, jobdetailid, jobCenter, supplierid, cost, usercode)
                objjob.UpdateDetail(jobno, jobdetailid, jobType, assCode, supplierid, cost, usercode, closetypeid, closecategory)
                flashData()
            Catch ex As Exception
                Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess

            End Try

            'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
        End If

endprocess:
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim s As String = "window.open('../OPS/Jobs_Report_JobForm.aspx?jobcode=" & Request.QueryString("jobno").ToString() &
    "&supplierid=" & cboSupplier.SelectedItem.Value &
    " ', '_blank');"

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", s, True)
    End Sub

    Private Sub btnSubmitRate_Click(sender As Object, e As EventArgs) Handles btnSubmitRate.Click

        Dim objjob As New jobs
        Dim res As String = Request.Form("confirm_value")
        res = res.Replace("[", "")
        res = res.Replace("]", "")
        Dim strarr() As String
        strarr = res.Split("},")
        Dim lengthArr = strarr.Length
        Dim rateid As Integer
        Try
            For i = 0 To lengthArr - 1
                If Not String.IsNullOrEmpty(strarr(i).ToString) Then
                    If strarr(i).Substring(0, 1) = "," Then
                        res = strarr(i).Substring(1, strarr(i).Length - 1) + "}"
                    Else
                        res = strarr(i) + "}"
                    End If
                    Dim jss As New JavaScriptSerializer
                    Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)

                    Dim topic_id As String = json("topic_id").Trim
                    Dim value As String = json("value").Trim
                    Dim type As String = json("type").Trim



                    rateid = objjob.SaveRate(topic_id, value, type, jobdetailid, usercode)
                End If
            Next

            objjob.Followup_Save(jobno, jobdetailid, 10, "ลงคะแนนประเมินเสร็จสิ้น", usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('json fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    '    Private Sub btnEditClose_Click(sender As Object, e As EventArgs) Handles btnEditClose.Click


    '        Dim closetypeid As Double = cboCloseType.SelectedItem.Value
    '        Dim closecategory As Integer = cboCloseCategory.SelectedItem.Value

    '        Dim objjob As New jobs
    '        Try
    '            'objjob.UpdateSupplierandCost(jobno, jobdetailid, jobCenter, supplierid, cost, usercode)
    '            objjob.UpdateCloseTypeCategory(jobno, jobdetailid, closetypeid, closecategory, usercode)
    '            flashData()
    '        Catch ex As Exception
    '            Dim scriptKey As String = "UniqueKeyForThisScript"
    '            'Dim javaScript As String = "alert('" & ex.Message & "');"
    '            Dim javaScript As String = "alertWarning('EditClose fail');"
    '            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    '            GoTo endprocess

    '        End Try
    '        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)

    'endprocess:
    '    End Sub

    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click

        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("jobdetailid"), txtComment.Text.Trim(), Session("userid"))
            txtComment.Text = ""
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub

    Private Sub flashData()
        Clear()
        FindJob(jobno, jobdetailid)
        setBtn()
    End Sub

    Private Sub btnSentSupplier_Click(sender As Object, e As EventArgs) Handles btnSentSupplier.Click

        Dim objjob As New jobs

        Try
            objjob.Jobs_Send_Supplier(jobno, jobdetailid, usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub

    Private Sub btnUpdateJobCateCode_Click(sender As Object, e As EventArgs) Handles btnUpdateJobCateCode.Click
        Dim objjob As New jobs

        Try
            objjob.UpdateDetailCateCode(jobno, jobdetailid, cboJobCate.SelectedItem.Value, usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveCateCode fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub

    Private Sub btnUpdateJobitems_Click(sender As Object, e As EventArgs) Handles btnUpdateJobitems.Click

        Dim objjob As New jobs

        Try
            objjob.JobItem_Save(jobdetailid, emails.Value.ToString, usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Save Jobitems fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub
End Class