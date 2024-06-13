Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class JobsFollowup
    Inherits System.Web.UI.Page
    Dim objStatus As String
    Dim jobno As String
    Dim jobdetailid As Integer
    Public cntSupplier As Integer
    Public statusnow As Integer
    Public menutable As DataTable
    Public maintable As DataTable
    Public suppilertable As DataTable
    Public stepsuppilertable As DataTable
    Public assessmenttable As DataTable
    Public ratetable As DataTable
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public nozzletable As DataTable
    Public assetsNozzletable As DataTable

    Public followuptable As DataTable = CreateFollowup()
    Dim usercode, username As String
    Public allOperator As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("username") = "PAB"
        Dim objjob As New jobs
        Dim objsupplier As New Supplier
        Dim attatch As New Attatch
        Dim objpolicy As New Policy

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
        ViewState("jobno") = Request.QueryString("jobno")
        ViewState("jobdetailid") = Request.QueryString("jobdetailid")
        jobno = ViewState("jobno")
        jobdetailid = ViewState("jobdetailid")
        txtJobno.Text = jobno


        If Not IsPostBack Then
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()
            suppilertable = createtablesuppiler()
            stepsuppilertable = createtablestepsuppiler()
            assessmenttable = createtableassessment()
            Clear()

            objjob.SetCboCloseType(cboCloseType)
            objjob.SetCboCloseCategory(cboCloseCategory)
            objsupplier.SetCboSupplier(cboSupplier)

            'objsupplier.SetCboSupplierByComid(cboVendor, "1") '1 PURE
            objjob.SetCboJobCenterList(cboJobCenter)
            SetCboJobType(cboJobType, usercode, "actived")
            attatch.SetCboMyfile(cboMyfile, Session("userid"))
            FindJob(jobno, jobdetailid)
            If maintable.Rows(0).Item("owner") > 0 Then
                objjob.SetCboJobStatusList(cboStatus)
                'Else
                '    Dim dt As New DataTable

                '    dt.Columns.Add("statusid", GetType(Integer))
                '    dt.Columns.Add("name", GetType(String))
                '    dt.Rows.Add(11, "confirm")
                '    cboStatus.DataSource = dt
                '    cboStatus.DataValueField = ("statusid")
                '    cboStatus.DataTextField = ("name")
                '    cboStatus.DataBind()
            End If

            ViewState("maintable") = maintable
            ViewState("followuptable") = followuptable
            ViewState("suppilertable") = suppilertable
            ViewState("stepsuppilertable") = stepsuppilertable
            ViewState("assessmenttable") = assessmenttable
            ViewState("comment_jobdetail") = CommentTable
            ViewState("attatch_jobdetail") = AttachTable
            ViewState("nozzle_jobdetail") = nozzletable

        Else
            followuptable = ViewState("followuptable")
            maintable = ViewState("maintable")
            suppilertable = ViewState("suppilertable")
            stepsuppilertable = ViewState("stepsuppilertable")
            assessmenttable = ViewState("assessmenttable")
            CommentTable = ViewState("comment_jobdetail")
            AttachTable = ViewState("attatch_jobdetail")
            nozzletable = ViewState("nozzle_jobdetail")


            showsuppilerdata(suppilertable)

            Dim target = Request.Form("__EVENTTARGET")
            If target = "flashData" Then
                flashData()
            ElseIf target = "deleteNozzle" Then
                Dim argument As String = Request("__EVENTARGUMENT")
                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(argument)
                deleteNozzle(json("nozzleid"))
            End If
        End If

        setBtn()

        'txtCreateBy.Text = ViewState("jobtypeid")
    End Sub
    Private Sub BindData()
        'cntdt = itemtable.Rows.Count
        gvNozzle.DataSource = nozzletable
        gvNozzle.DataBind()
    End Sub
    Private Sub setBtn()
        If maintable IsNot Nothing Then
            If maintable.Rows.Count > 0 Then
                If maintable.Rows(0).Item("owner") > 0 Then 'กรณีเป็น Operator

                    'Detail
                    btnSave.Visible = True
                    'btnConfirm.Visible = False
                    'Rating
                    btnSubmitRate.Visible = False
                    btndisAccept.Visible = False

                    If maintable.Rows(0).Item("followup_status") = "ปิดงาน" Then
                        btnSave.Enabled = False
                        btnEditDetail.Visible = False
                        btnNozzle.Visible = False


                        'Supplier
                        btnSentSupplier.Visible = False
                        btnPrint.Visible = False
                        btnBackStep.Visible = False
                        btnNextStep.Visible = False
                        btnCancelSupplier.Visible = False
                        If Not maintable.Rows(0).Item("supplierid") = 0 Then
                            Dim rows() As DataRow = stepsuppilertable.Select("stepdate is not null")
                            If rows.Count > 0 Then
                                If statusnow = 7 Then
                                    btnCancelSupplier.Enabled = False
                                    btnBackStep.Enabled = False
                                    btnNextStep.Enabled = False
                                Else
                                    btnCancelSupplier.Visible = True
                                    btnBackStep.Visible = True
                                    btnNextStep.Visible = True
                                End If
                            End If
                        End If



                        cardfour.Attributes.Remove("readonly") 'คะแนนการประเมิน

                        btnAddAttatch.Visible = False

                        Dim scriptKey As String = "UniqueKeyForThisScript"
                        Dim javaScript As String = "disbtndelete();"
                        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)


                    Else
                        btnSave.Enabled = True
                        btnEditDetail.Visible = True
                        If nozzletable.Rows.Count > 0 Then
                            btnNozzle.Visible = True
                        Else
                            btnNozzle.Visible = False
                        End If



                        'Supplier
                        btnSentSupplier.Visible = True
                        btnPrint.Visible = True
                        btnBackStep.Visible = True
                        btnNextStep.Visible = True
                        btnCancelSupplier.Visible = True
                        If maintable.Rows(0).Item("supplierid") = 0 Then
                            btnSentSupplier.Visible = False '
                            btnPrint.Visible = False
                            btnBackStep.Visible = False
                            btnNextStep.Visible = False
                            btnCancelSupplier.Visible = False
                        Else
                            btnSentSupplier.Visible = True '
                            btnPrint.Visible = True
                            btnBackStep.Visible = False
                            btnNextStep.Visible = False
                            btnCancelSupplier.Visible = False
                            Dim rows() As DataRow = stepsuppilertable.Select("stepdate is not null")
                            If rows.Count > 0 Then
                                btnSentSupplier.Visible = False '
                                If statusnow = 7 Then
                                    btnCancelSupplier.Enabled = False
                                    btnBackStep.Enabled = False
                                    btnNextStep.Enabled = False
                                Else
                                    btnCancelSupplier.Visible = True
                                    btnBackStep.Visible = True
                                    btnNextStep.Visible = True
                                End If
                            End If

                        End If



                        cardfour.Attributes.Add("style", "display:none;") 'คะแนนการประเมิน

                        btnAddAttatch.Visible = True

                        If String.Equals(username, maintable.Rows(0).Item("jobowner")) Then
                            'If maintable.Rows(0).Item("followup_status") = "รอลงคะแนนประเมินงาน" Then

                            If statusnow = 4 Or maintable.Rows(0).Item("supplierid") = 0 Then
                                cardfour.Attributes.Remove("readonly") 'คะแนนการประเมิน
                                cardfour.Attributes.Remove("style")
                                btnSubmitRate.Visible = True
                                btndisAccept.Visible = True
                            End If
                            'End If
                        End If


                    End If

                    'Follow UP
                    fromUpdateFollowup.Visible = True

                    'Analy
                    btnDataAnalyCategory.Visible = True
                    btnDataAnalyGroupType.Visible = True



                ElseIf String.Equals(username, maintable.Rows(0).Item("jobowner")) Then 'กรณีเป็น เจ้าของงาน
                    'Detail
                    btnSave.Visible = False
                    'btnConfirm.Visible = False
                    btnEditDetail.Visible = False
                    btnNozzle.Visible = False


                    'Rating
                    btnSubmitRate.Visible = False
                    btndisAccept.Visible = False
                    If maintable.Rows(0).Item("followup_status") = "ปิดงาน" Then
                        'btnConfirm.Enabled = False

                        btnAddAttatch.Visible = False

                        Dim scriptKey As String = "UniqueKeyForThisScript"
                        Dim javaScript As String = "disbtndelete();"
                        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)


                        cardfour.Attributes.Remove("readonly") 'คะแนนการประเมิน

                    Else
                        'btnConfirm.Enabled = True

                        'btnSubmitRate.Visible = True
                        'btndisAccept.Visible = True

                        btnAddAttatch.Visible = True

                        cardfour.Attributes.Add("style", "display:none;") 'คะแนนการประเมิน

                        If statusnow = 4 Or maintable.Rows(0).Item("supplierid") = 0 Then

                            cardfour.Attributes.Remove("readonly") 'คะแนนการประเมิน
                            cardfour.Attributes.Remove("style")
                            btnSubmitRate.Visible = True
                            btndisAccept.Visible = True
                        End If
                    End If

                    'Follow UP
                    fromUpdateFollowup.Visible = False


                    'Analy
                    btnDataAnalyCategory.Visible = False
                    btnDataAnalyGroupType.Visible = False

                    'Supplier
                    btnSentSupplier.Visible = False
                    btnPrint.Visible = False
                    btnBackStep.Visible = False
                    btnNextStep.Visible = False
                    btnCancelSupplier.Visible = False
                Else
                    'Detail
                    btnSave.Visible = False
                    'btnConfirm.Visible = False
                    btnEditDetail.Visible = False
                    btnNozzle.Visible = False
                    'btnConfirm.Enabled = False

                    'Follow UP
                    fromUpdateFollowup.Visible = False

                    'Supplier
                    btnSentSupplier.Visible = False
                    btnPrint.Visible = False
                    btnBackStep.Visible = False
                    btnNextStep.Visible = False
                    btnCancelSupplier.Visible = False


                    'Analy
                    btnDataAnalyCategory.Visible = False
                    btnDataAnalyGroupType.Visible = False


                    'Rating

                    If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then
                        cardfour.Attributes.Add("style", "display:none;") 'คะแนนการประเมิน
                    Else
                        cardfour.Attributes.Remove("readonly") 'คะแนนการประเมิน
                        cardfour.Attributes.Remove("style")
                    End If
                    btnSubmitRate.Visible = False
                    btndisAccept.Visible = False

                    btnAddAttatch.Visible = False

                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "disbtndelete();"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
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

    Private Function createtablesuppiler() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("statusnow", GetType(Integer))
        dt.Columns.Add("End_Comments", GetType(String))
        dt.Columns.Add("BeginDate", GetType(String))
        dt.Columns.Add("EndDate", GetType(String))


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
        ViewState("jobno") = jobno

        'itemtable
        Try
            mydataset = job.FindFollowup(jobno, jobdetailid, usercode)
            maintable = mydataset.Tables(0)
            showjobdata(maintable)
            followuptable = mydataset.Tables(1)
            stepsuppilertable = mydataset.Tables(2)
            assessmenttable = mydataset.Tables(3)
            AttachTable = mydataset.Tables(4)
            CommentTable = mydataset.Tables(5)
            suppilertable = mydataset.Tables(6)
            txtallOperator.Text = mydataset.Tables(7).Rows(0).Item("alloperator")
            nozzletable = mydataset.Tables(8)
            findNozzle(maintable.Rows(0).Item("branchid"))
            showsuppilerdata(suppilertable)
            BindData()


            ViewState("maintable") = maintable
            ViewState("followuptable") = followuptable
            ViewState("suppilertable") = suppilertable
            ViewState("stepsuppilertable") = stepsuppilertable
            ViewState("assessmenttable") = assessmenttable
            ViewState("comment_jobdetail") = CommentTable
            ViewState("attatch_jobdetail") = AttachTable
            ViewState("nozzle_jobdetail") = nozzletable



        Catch ex As Exception

            Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub showjobdata(mytable As DataTable)

        If mytable.Rows.Count > 0 Then
            Dim objjob As New jobs
            Dim objpolicy As New Policy

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

                'policy modal
                objpolicy.setComboPolicyByJobTypeID(cboPolicy, .Item("jobtypeid"))
                cboPolicy.SelectedIndex = cboPolicy.Items.IndexOf(cboPolicy.Items.FindByValue(.Item("policyid")))
                txtPolicyRequestdate.Text = .Item("requestdate")

                'policy ที่โชว์ในรายละเอียดงาน
                txtPolicyName.Text = .Item("policy")
                txtPolicyDate.Text = .Item("requestdate")

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
        End If
    End Sub

    Private Sub showsuppilerdata(mytable As DataTable)
        If mytable.Rows.Count > 0 Then
            With mytable.Rows(0)
                txtstkCode.InnerText = .Item("stk_code")
                txtCntSupplier.InnerText = "( " + .Item("cnt_supplier") + " คน )"
                txtSuppilerCode.InnerText = .Item("vendorcode")
                txtSuppilerName.InnerText = .Item("vendorname")
                txtEndComment.Text = .Item("End_Comments")
                statusnow = .Item("statusnow")
                txtSupplierBeginDate.Text = .Item("BeginDate")
                txtSupplierEndDate.Text = .Item("EndDate")

                cntSupplier = .Item("cnt_supplier")
            End With
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateUpdate() Then

            Dim statusid As Integer = cboStatus.SelectedItem.Value
            Dim details As String = txtDetailFollow.Text

            saveFollowup(statusid, details)
        End If
    End Sub
    'Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
    '    If ValidateUpdate() Then
    '        saveFollowup()
    '    End If
    'End Sub
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
            Dim javaScript As String = "alertWarning('" + msg + "');modalShowID = 'EditDetail';"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function
    Private Sub saveFollowup(statusid As Integer, details As String)

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

            Dim policyid As Integer = cboPolicy.SelectedItem.Value
            Dim policydate As String = txtPolicyRequestdate.Text

            Dim cost As Double

            Try
                cost = Double.Parse(txtCost.Text)
            Catch
                cost = 0
            End Try

            Dim objjob As New jobs

            Try
                'objjob.UpdateSupplierandCost(jobno, jobdetailid, jobCenter, supplierid, cost, usercode)
                objjob.UpdateDetail(jobno, jobdetailid, jobType, assCode, supplierid, cost, usercode, policyid, policydate, closetypeid, closecategory)
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
        Dim supplierid As Integer = maintable.Rows(0).Item("supplierid")
        Dim s As String = "window.open('../OPS/Jobs_Report_JobForm.aspx?jobcode=" & Request.QueryString("jobno").ToString() &
    "&supplierid=" & supplierid &
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
            objjob.Jobs_Supplier_FinishStep(jobno, jobdetailid, usercode)
            objjob.Followup_Save(jobno, jobdetailid, 10, "ลงคะแนนประเมินเสร็จสิ้น", usercode)
            ClientScript.RegisterStartupScript(Me.GetType(), "Finish", "finishStep();", True)
            'If maintable.Rows(0).Item("jobtypeid").ToString = "54" Then
            '    Response.Redirect("../OPS/Assets/AssetsNozzle.aspx")
            'Else
            flashData()
            'End If
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
    'Private Sub btnSentSupplier_Click(sender As Object, e As EventArgs) Handles btnSentSupplier.ServerClick
    '    'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
    '    flashData()
    'End Sub

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
            objjob.JobItem_Save(jobdetailid, jobitems.Value.ToString, usercode)
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
    Private Sub cboJobType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobType.SelectedIndexChanged
        Dim objpolicy As New Policy
        Dim jobtypeid As Integer

        jobtypeid = cboJobType.SelectedItem.Value
        objpolicy.setComboPolicyByJobTypeID(cboPolicy, jobtypeid)

        If maintable IsNot Nothing Then
            If maintable.Rows.Count > 0 Then
                With maintable.Rows(0)
                    cboPolicy.SelectedIndex = cboPolicy.Items.IndexOf(cboPolicy.Items.FindByValue(.Item("policyid")))
                End With
            End If
        End If

        Dim scriptKey As String = "modalShow"
        'Dim javaScript As String = "alert('" & ex.Message & "');"
        Dim javaScript As String = "modalShowID = 'EditDetail';"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

    End Sub
    Private Function setDueDate(policyid As String) As Boolean
        Dim objjobs As New jobs
        Dim mydataset As DataSet
        Try
            If maintable IsNot Nothing Then
                If maintable.Rows.Count > 0 Then
                    With maintable.Rows(0)
                        mydataset = objjobs.setDueDateByPolicyID(policyid, .Item("jobdate").ToString())
                        txtPolicyRequestdate.Text = mydataset.Tables(0).Rows(0).Item("duedate")
                    End With
                End If
            End If
            Return True
        Catch ex As Exception
            txtPolicyRequestdate.Text = ""
            Return False
        End Try
    End Function

    Private Sub cboPolicy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPolicy.SelectedIndexChanged
        setDueDate(cboPolicy.SelectedItem.Value)

        Dim scriptKey As String = "modalShow"
        'Dim javaScript As String = "alert('" & ex.Message & "');"
        Dim javaScript As String = "modalShowID = 'EditDetail';"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
    End Sub

    Private Sub btnCancelSupplier_Click(sender As Object, e As EventArgs) Handles btnCancelSupplier.Click
        Dim objjob As New jobs

        Try
            objjob.Jobs_Supplier_Cancel(jobno, jobdetailid, usercode)
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

    Private Sub btnBackStep_Click(sender As Object, e As EventArgs) Handles btnBackStep.Click
        Dim objjob As New jobs

        Try
            objjob.Jobs_Supplier_BackStep(jobno, jobdetailid, usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('BackStep fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub

    Private Sub btnNextStep_Click(sender As Object, e As EventArgs) Handles btnNextStep.Click
        If ValidateSuppilerStatus() Then
            Dim objjob As New jobs

            Try
                objjob.Jobs_Supplier_NextStep(jobno, jobdetailid, usercode)
                flashData()
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('NextStep fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
        End If
endprocess:
    End Sub

    Private Function ValidateSuppilerStatus() As Boolean
        Dim result As Boolean = True
        'Dim msg As String = ""

        If statusnow = 3 Then '3	กำลังดำเนินการ
            result = False
            GoTo endprocess
        End If

endprocess:
        If result = False Then
            Dim scriptKey As String = "modalShow"
            Dim javaScript As String = "modalShowID = 'suppilerDetail';"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End If

        Return result
    End Function

    Private Sub btnSuppilerSubmit_Click(sender As Object, e As EventArgs) Handles btnSuppilerSubmit.Click
        Dim objjob As New jobs

        Try
            objjob.Jobs_Supplier_addDetails(jobno, jobdetailid, txtbegindate.Text, txtenddate.Text, txtSuppilerDetail.Text, usercode)
            objjob.Followup_Save(jobno, jobdetailid, 15, "มีการกรอบรายละเอียดการปฏิบัติงานเข้ามา", usercode)
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('BackStep fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        'Response.Redirect("../OPS/jobs_followup.aspx?jobno=" & jobno & "&jobdetailid=" & jobdetailid)
endprocess:
    End Sub

    Private Sub btnAddDetails_Click(sender As Object, e As EventArgs) Handles btnAddDetails.Click
        Dim objjob As New jobs
        Try
            'Dim a As String = getfilenozzle("files__nozzle")
            objjob.UpdateDetailnozzle(jobdetailid,
                                hiddenAdvancedetailid.Value,
                                txtbrand.Text.ToString,
                                txtproducttype.Text.ToString,
                                txtnozzle_no.Text.ToString,
                                txtpositiononassest.Text.ToString,
                                txtexpirydate.Text.ToString,
                                getfilenozzle("files__nozzle"),
                                Session("usercode")
                                )

            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('update nozzle fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
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
                        'approval.Image_Save(filenameArr(i), key, jobdetailid, Session("usercode"))
                        Dim urlIMG As String = "http://vpnptec.dyndns.org:10280/OPS_แจ้งซ่อม/"
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

    Private Function fileupload(key As String, Files As HttpFileCollection) As String
        Dim imgname As New Approval
        Dim fullfilename As String = String.Empty

        Dim Keys() As String
        Keys = Files.AllKeys
        For i = 0 To Keys.GetUpperBound(0)
            If Not String.IsNullOrEmpty(Files(i).FileName) And key = Keys(i) Then
                Dim Extension As String = System.IO.Path.GetExtension(Files(i).FileName)
                Dim rootPath As String = "D:\\PTECAttatch\\IMG\\OPS_แจ้งซ่อม\\"
                Dim savePath As String
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
    Private Sub deleteNozzle(nozzleid As Integer)
        Dim objjob As New jobs
        Dim approval As New Approval
        Try
            'Dim a As String = getfilenozzle("files__nozzle")
            objjob.DelDetail_Nozzle(jobdetailid,
                                nozzleid,
                                Session("usercode")
                                )

            approval.Save_Comment_By_Code(Request.QueryString("jobdetailid"), txtComment.Text.Trim(), Session("userid"))
            flashData()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Delete nozzle fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub findNozzle(branchid As Integer)
        Dim objassets As New Assets

        Try
            assetsNozzletable = objassets.AssesNozzle_list(branchid)
            BindDataAssetsNozzle()
            ViewState("assetsNozzletable") = assetsNozzletable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find nozzle fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub BindDataAssetsNozzle()
        Dim cntdt = assetsNozzletable.Rows.Count
        gvAssetsNozzle.Caption = "ทั้งหมด " & cntdt & " รายการ"
        gvAssetsNozzle.DataSource = assetsNozzletable
        gvAssetsNozzle.DataBind()
    End Sub

    Private Sub btnSetNozzle_Click(sender As Object, e As EventArgs) Handles btnSetNozzle.Click
        Dim confirmValue As String = Request.Form("setNozzle")
        Dim objjob As New jobs
        Dim result = JsonConvert.DeserializeObject(Of ArrayList)(confirmValue)
        Dim token As JToken
        Dim code
        If result IsNot Nothing Then

            For Each value As Object In result
                token = JObject.Parse(value.ToString())
                'position = token.SelectToken("position")
                code = token.SelectToken("code")

                If Not String.IsNullOrEmpty(code) Then
                    Try
                        objjob.SaveDetail_Nozzle(jobdetailid, code)
                        flashData()
                    Catch ex As Exception
                        Dim scriptKey As String = "alert"
                        'Dim javaScript As String = "alert('" & ex.Message & "');"
                        Dim javaScript As String = "alertWarning('save fail');"
                    End Try
                End If



            Next value
            'BindData()
            ViewState("nozzletable") = nozzletable
        End If
endprocess:
    End Sub
    Private Sub gvAssetsNozzle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAssetsNozzle.RowDataBound
        Dim Data As DataRowView
        Data = e.Row.DataItem
        If Data Is Nothing Then
            Return
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Not String.IsNullOrEmpty(Data.Item("jobref").ToString) Then
                e.Row.Visible = False
            End If
        End If
    End Sub
End Class