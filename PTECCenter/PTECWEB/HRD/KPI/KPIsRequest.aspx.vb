Imports System.Web.Script.Serialization

Public Class KPIsRequest
    Inherits System.Web.UI.Page
    Public chkunsave As Integer = 0

    Public PermissionOwner As DataSet '= createtable()
    Public menutable As DataTable
    Dim statusid As Integer = 0
    Dim createby As Integer = 0

    Public verify As Boolean = False
    Public approval As Boolean = False
    Public flag As Boolean = True

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

    Public maintable As DataTable '= createmaintable()
    Public AttachTable As DataTable '= createtable()
    Public CommentTable As DataTable '= createtable()
    Public weighttable As DataTable '= createdetailtable()
    Public detailtable As DataTable '= createdetailtable()
    Public signedtable As DataTable '= createsignedtable()
    Public groupdetailtable As DataTable '= createdetailtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objKpi As New Kpi

        'Dim objjob As New jobs
        Dim usercode As String
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

        cboPeriod.Attributes.Add("disabled", "True")

        If Not IsPostBack() Then
            maintable = createmaintable()
            detailtable = createdetailtable()
            weighttable = createweighttable()
            signedtable = createsignedtable()
            CommentTable = createtablecomment()
            AttachTable = createtableAttach()

            objKpi.SetCboPeriod(cboPeriod)
            objKpi.SetCboRatioTypeByUser(cboRatio, Session("userid"))
            If Request.QueryString("NewKpiCode") IsNot Nothing Then
                'Has code
                findNewKPI()

                statusid = maintable.Rows(0).Item("statusid")
                createby = maintable.Rows(0).Item("ownerid")
                chkuser(createby)

                If (maintable.Rows(0).Item("statusid") = 2) Then
                    If (maintable.Rows(0).Item("ownerempupper").ToString().IndexOf(usercode) > -1) Then
                        approval = True
                    End If
                End If

                setmain(maintable)
            Else
                'New
                Dim ds As DataSet
                ds = objKpi.Kpi_Get_NewKPIsOfPeriod_by_Usercode(cboPeriod.SelectedItem.Value, usercode, "kpi")
                weighttable = ds.Tables(0)

                txtOwnername.Text = Session("username")
                txtPosition.Text = Session("positionname")
                txtDep.Text = Session("depname")
                txtSec.Text = Session("secname")

            End If

            txtratio.InnerText = ""
            If weighttable.Rows.Count > 0 Then
                For i As Integer = weighttable.Rows.Count - 1 To 0 Step -1
                    txtratio.InnerText += "/ " + weighttable.Rows(i).Item("weight").ToString() + " "
                Next i
                If Not String.IsNullOrEmpty(txtratio.InnerText) Then
                    Dim firstChar As Char = txtratio.InnerText.Substring(0, 1)
                    If firstChar.ToString.Trim = "/" Then
                        txtratio.InnerText = txtratio.InnerText.Substring(1, txtratio.InnerText.Length - 1)
                    End If
                End If
            End If



            ViewState("maintable_request") = maintable
            ViewState("detailtable_request") = detailtable
            ViewState("weighttable_request") = weighttable
            ViewState("signedtable_request") = signedtable
            ViewState("commenttable_request") = CommentTable
        Else

            maintable = ViewState("maintable_request")
            detailtable = ViewState("detailtable_request")
            weighttable = ViewState("weighttable_request")
            signedtable = ViewState("signedtable_request")
            CommentTable = ViewState("commenttable_request")

            Try
                statusid = maintable.Rows(0).Item("statusid")
                createby = maintable.Rows(0).Item("ownerid")

            Catch ex As Exception
                statusid = 0
                createby = 0
            End Try
            Dim target = Request.Form("__EVENTTARGET")
            If target = "deletedetail" Then
                Dim argument As String = Request("__EVENTARGUMENT")
                Dim jss As New JavaScriptSerializer
                Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(argument)
                deleteDetail(json("kpicode"), json("status"), json("user"))
            End If
        End If
        SetBtn(statusid, createby)
    End Sub
    Private Function createmaintable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("newkpicode", GetType(String))
        dt.Columns.Add("preriodid", GetType(Integer))
        dt.Columns.Add("statusid", GetType(Integer)) 'new=0
        dt.Columns.Add("statusname", GetType(String))
        '---------------------------------------
        dt.Columns.Add("branchid", GetType(String))
        dt.Columns.Add("branchname", GetType(String))
        dt.Columns.Add("positionid", GetType(String))
        dt.Columns.Add("positionname", GetType(String))
        dt.Columns.Add("depid", GetType(String))
        dt.Columns.Add("depcode", GetType(String))
        dt.Columns.Add("depname", GetType(String))
        dt.Columns.Add("secid", GetType(String))
        dt.Columns.Add("seccode", GetType(String))
        dt.Columns.Add("secname", GetType(String))
        '---------------------------------------
        dt.Columns.Add("verify_by", GetType(String))
        dt.Columns.Add("verify_date", GetType(String))
        dt.Columns.Add("approval_by", GetType(String))
        dt.Columns.Add("approval_date", GetType(String))
        '---------------------------------------
        dt.Columns.Add("ownerid", GetType(Integer))
        dt.Columns.Add("ownercode", GetType(String))
        dt.Columns.Add("ownername", GetType(String))
        dt.Columns.Add("ownerempupper", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("commitdate", GetType(String))
        '---------------------------------------
        dt.Columns.Add("assignby", GetType(String))
        dt.Columns.Add("assigndate", GetType(String))

        Return dt
    End Function
    Private Function createsignedtable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("approvallevel_id", GetType(Integer))
        dt.Columns.Add("at_code", GetType(String))
        dt.Columns.Add("code_ref", GetType(String))
        dt.Columns.Add("approverid", GetType(Integer))
        dt.Columns.Add("approvercode", GetType(String))
        dt.Columns.Add("approval_date", GetType(String))
        dt.Columns.Add("level_approval", GetType(Integer))
        Return dt
    End Function
    Private Function createweighttable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("positionlevel", GetType(Integer))
        dt.Columns.Add("categoryname", GetType(String))
        dt.Columns.Add("category_id", GetType(Integer))
        dt.Columns.Add("weight", GetType(Integer))
        dt.Columns.Add("kpiperiod_id", GetType(Integer))
        Return dt
    End Function
    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("kpi_code", GetType(String))
        dt.Columns.Add("title", GetType(String))
        ' dt.Columns.Add("statusid", GetType(Integer))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("categoryid", GetType(Integer))
        dt.Columns.Add("categoryname", GetType(String))
        dt.Columns.Add("weight", GetType(Double))
        dt.Columns.Add("unit", GetType(String))
        dt.Columns.Add("lv1", GetType(String))
        dt.Columns.Add("lv2", GetType(String))
        dt.Columns.Add("lv3", GetType(String))
        dt.Columns.Add("lv4", GetType(String))
        dt.Columns.Add("lv5", GetType(String))
        'dt.Columns.Add("commit_date", GetType(String))
        'dt.Columns.Add("preriodid", GetType(Integer))
        dt.Columns.Add("active", GetType(String))
        dt.Columns.Add("updateby", GetType(String))
        dt.Columns.Add("updatedate", GetType(String))
        dt.Columns.Add("createby", GetType(String))
        dt.Columns.Add("createdate", GetType(String))
        dt.Columns.Add("kpinew_code", GetType(String))
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

    Private Sub btnAddDetail_Click(sender As Object, e As EventArgs) Handles btnAddDetail.Click
        Dim res As String = Request.Form("addDetailJSON")
        Dim strarr() As String
        strarr = res.Split("}")
        res = strarr(0) + "}"
        Dim jss As New JavaScriptSerializer
        Dim json As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(res)

        'Dim preriodid As Integer = json("preriodid").Trim
        Dim status As String = json("status").Trim
        Dim kpicode As String = json("kpicode").Trim
        Dim categoryid As Integer = json("categoryid").Trim
        Dim categoryname As String = json("categoryname").Trim
        Dim title As String = json("txtkpititle").Trim
        Dim weight As Integer = json("txtweight").Trim
        Dim unit As String = json("txtunit").Trim
        Dim lv5 As String = json("txtlv5").Trim
        Dim lv4 As String = json("txtlv4").Trim
        Dim lv3 As String = json("txtlv3").Trim
        Dim lv2 As String = json("txtlv2").Trim
        Dim lv1 As String = json("txtlv1").Trim

        Dim cntrow As Integer = detailtable.Rows.Count + 1
        While detailtable.Rows.IndexOf(detailtable.Select("kpi_code='" & cntrow & "'").FirstOrDefault()) > -1
            cntrow += 1
        End While
        Try
            If status = "undefined" Then
                Dim row As DataRow
                row = detailtable.NewRow()
                row("kpi_code") = cntrow
                row("status") = "new"
                'row("preriodid") = preriodid
                row("categoryid") = categoryid
                row("categoryname") = categoryname
                row("title") = title
                row("weight") = weight
                row("unit") = unit
                row("lv5") = lv5
                row("lv4") = lv4
                row("lv3") = lv3
                row("lv2") = lv2
                row("lv1") = lv1


                detailtable.Rows.Add(row)
            Else
                With detailtable.Rows(detailtable.Rows.IndexOf(detailtable.Select("kpi_code='" & kpicode & "'")(0)))
                    .Item("kpi_code") = kpicode
                    .Item("status") = "edit"
                    '.Item("preriodid") = preriodid
                    .Item("categoryid") = categoryid
                    .Item("categoryname") = categoryname
                    .Item("title") = title
                    .Item("weight") = weight
                    .Item("unit") = unit
                    .Item("lv5") = lv5
                    .Item("lv4") = lv4
                    .Item("lv3") = lv3
                    .Item("lv2") = lv2
                    .Item("lv1") = lv1

                End With
            End If

            ViewState("detailtable_request") = detailtable
            checkunsave()
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('adddetail fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub checkunsave()
        Dim cnt As Integer = detailtable.Rows.Count - 1
        For i = 0 To cnt
            If Not detailtable.Rows(i).Item("status") = "used" Then
                chkunsave = 1
                GoTo endprocess
            End If
        Next i
endprocess:
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        saveorupdate()
    End Sub

    Private Sub saveorupdate()
        If validateSave() Then
            'If ViewState("status") = "new" Then
            'If maintable.Rows.Count = 0 Then
            updatehead()
            'End If
            Save()
            'Else
            '    SaveEdit()
            'End If
        End If

    End Sub

    Private Function validateSave() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim cnt_cost As Integer
        'Dim amountpayBack As Double
        'Dim amountdedusctsell As Double

        If cboPeriod.SelectedItem.Value = "" Then
            result = False
            msg = "กรุณาเลือกรอบ KPI"
            GoTo endprocess
        End If

        Dim kpiGroup = detailtable.AsEnumerable().
        GroupBy(Function(row) New With {
            Key .Name = row.Field(Of String)("categoryname")
        })

        Dim tableResult As New DataTable
        tableResult.Columns.Add("categoryname", GetType(String))
        tableResult.Columns.Add("weight", GetType(Integer))

        Dim newrows As DataRow
        For Each grp In kpiGroup
            newrows = tableResult.NewRow()
            newrows("categoryname") = grp.Key.Name
            newrows("weight") = grp.Sum(Function(row) row.Field(Of Double)("weight"))
            If newrows("weight") > 100 Then
                result = False
                msg = "weight รวมย่อยต้องไม่เกิน 100"
                GoTo endprocess
            End If
        Next

endprocess:
        If result = False Then
            Dim scriptKey As String = "alert"
            Dim javaScript As String = "alertWarning('" + msg + "');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            'MsgBox(msg)
        End If


        Return result
    End Function
    Private Function validateConfirm() As Boolean
        Dim result As Boolean = True
        Dim msg As String = ""
        Dim objKpi As New Kpi

        Dim kpiGroup = detailtable.AsEnumerable().
        GroupBy(Function(row) New With {
            Key .Name = row.Field(Of String)("categoryname")
        })

        Dim tableResult As New DataTable
        tableResult.Columns.Add("categoryname", GetType(String))
        tableResult.Columns.Add("weight", GetType(Integer))

        Dim newrows As DataRow
        For Each grp In kpiGroup
            newrows = tableResult.NewRow()
            newrows("categoryname") = grp.Key.Name
            newrows("weight") = grp.Sum(Function(row) row.Field(Of Double)("weight"))
            If Not newrows("weight") = 100 Then
                result = False
                msg = "weight ย่อยของแต่ละกลุ่มต้องรวมให้ได้ 100"
                GoTo endprocess
            End If
        Next


        Dim dtRatioTypeByUser As DataTable
        Try
            dtRatioTypeByUser = objKpi.RatioTypeByUser_List(Session("userid"))
            dtRatioTypeByUser.Rows.Remove(dtRatioTypeByUser.Select("category_id='0'")(0))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning(' category fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try


        Dim listKPIG = kpiGroup.ToList
        Dim cntList As Integer = listKPIG.Count

        If Not cntList = dtRatioTypeByUser.Rows.Count Then
            result = False
            msg = "ต้องมี KPI ให้ครบทุกประเภท"
            GoTo endprocess
        End If



        'For Each grp In kpiGroup
        '    newrows = tableResult.NewRow()
        '    newrows("categoryname") = grp.Key.Name
        '    newrows("weight") = grp.Sum(Function(row) row.Field(Of Double)("weight"))
        '    tableResult.Rows.Add(newrows)
        'Next
        'For Each row As DataRow In tableResult.Rows
        '    If row("weight") > 100 Then
        '        result = False
        '        GoTo endprocess
        '    End If
        'Next row

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
        Dim objkpi As New Kpi
        Dim newkpino As String = ""

        newkpino = txtnewkpi.Text
        Try
            newkpino = objkpi.SaveNewKPIs(newkpino, maintable, detailtable, Session("usercode"))
            txtnewkpi.Text = newkpino


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsRequest.aspx?NewKpiCode=" & newkpino)
endprocess:
    End Sub
    Private Sub SaveBeforeConfirm()
        Dim objkpi As New Kpi
        Dim newkpino As String = ""

        newkpino = txtnewkpi.Text
        Try
            newkpino = objkpi.SaveNewKPIs(newkpino, maintable, detailtable, Session("usercode"))
            txtnewkpi.Text = newkpino


        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('save fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)

            GoTo endprocess
        End Try
endprocess:
    End Sub

    <System.Web.Services.WebMethod>
    Public Function deleteDetail(ByVal kpicode As String, ByVal status As String, user As String)

        Try
            If status.ToString <> "used" Then
                detailtable.Rows.Remove(detailtable.Select("kpi_code='" & kpicode & "'")(0))
            Else

                Dim objKpi As New Kpi
                Dim objNonpo As New NonPO
                objKpi.deleteDetailbynewkpicode(kpicode, user)
                detailtable.Rows.Remove(detailtable.Select("kpi_code='" & kpicode & "'")(0))
            End If

        Catch ex As Exception
            Return "fail"

            GoTo endprocess
        End Try
        Return "success"
endprocess:
    End Function


    Private Sub updatehead()
        Dim userid As Double

        userid = Session("userid")

        If maintable.Rows.Count > 0 Then

            'fix bug share session 2 tab check code
            If Not (maintable.Rows(0).Item("newkpicode").Equals(txtnewkpi.Text)) Then
                findMainNewKPI()
            End If

            'update
            '    With maintable.Rows(0)
            '        .Item("payby") = payby
            '        .Item("vendorcode") = cboVendor.SelectedItem.Value
            '        .Item("comid") = cboCompany.SelectedItem.Value
            '        .Item("DueDate") = txtDuedate.Text.Trim()
            '        .Item("detail") = txtNote.Text.Trim()
            '        .Item("vat_wait") = If(chkVat.Checked, 1, 0)
            '        .Item("purecard_amount") = purecard_amount.ToString
            '        .Item("coderef") = codeRef.Text.Trim()
            '    End With
            'Else
            'Dim amountpayBack As Double
            'Dim amountdedusctsell As Double
        Else

            'insert
            With maintable
                .Rows.Add("", cboPeriod.SelectedItem.Value, 0, "",
                          "", "", "", "",
                          "", "", "", "", "", "",
                          "", "", "", "",
                          userid, "", "", "", Date.Now.ToString, Date.Now.ToString,
                              "", Date.Now.ToString)

            End With

        End If
        ViewState("maintable_request") = maintable

    End Sub
    Private Sub findNewKPI()
        Dim ds_find = New DataSet
        Dim objKpi As New Kpi
        Try
            ds_find = objKpi.Kpi_NewKPIs_Find(Request.QueryString("NewKpiCode").ToString())
            '-- table 0 = main
            '-- table 1 = detail

            maintable = ds_find.Tables(0)
            detailtable = ds_find.Tables(1)
            weighttable = ds_find.Tables(2)
            CommentTable = ds_find.Tables(3)

            ViewState("maintable_request") = maintable
            ViewState("detailtable_request") = detailtable
            ViewState("weighttable_request") = weighttable
            ViewState("commenttable_request") = CommentTable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
    Private Sub findMainNewKPI()
        Dim ds_find = New DataSet
        Dim objKpi As New Kpi
        Try
            ds_find = objKpi.Kpi_NewKPIs_Find(Request.QueryString("NewKpiCode").ToString())
            '-- table 0 = main
            '-- table 1 = detail

            maintable = ds_find.Tables(0)

            ViewState("maintable_request") = maintable
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('find main fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub setmain(dt As DataTable)

        Dim objsec As New Section
        With dt
            Select Case .Rows(0).Item("statusid").ToString
                Case = "1" '1 : รอยืนยัน
                    statusnewkpi.Attributes.Add("class", "btn btn-info")
                Case = "2" '2 : รออนุมัติ
                    statusnewkpi.Attributes.Add("class", "btn btn-warning")
                Case = "3" '3 : ได้รับการอนุมัติ
                    statusnewkpi.Attributes.Add("class", "btn btn-secondary")
                Case = "4" '4 : ยกเลิก
                    statusnewkpi.Attributes.Add("class", "btn btn-danger")
            End Select
            statusnewkpi.Text = .Rows(0).Item("statusname").ToString


            cboPeriod.Attributes.Remove("disabled")


            cboPeriod.SelectedIndex = cboPeriod.Items.IndexOf(cboPeriod.Items.FindByValue(.Rows(0).Item("preriodid").ToString))
            txtOwnername.Text = .Rows(0).Item("ownername").ToString
            txtPosition.Text = .Rows(0).Item("positionname").ToString
            txtDep.Text = .Rows(0).Item("depname").ToString
            txtSec.Text = .Rows(0).Item("secname").ToString
            txtnewkpi.Text = .Rows(0).Item("newkpicode").ToString()


            cboPeriod.Attributes.Add("disabled", "True")

            now_action = "ผู้ที่มีสิทธิอนุมัติ : " + .Rows(0).Item("ownerempupper").ToString()
        End With


    End Sub

    Private Sub SetBtn(statusid As String, createby As Integer)
        Select Case statusid
            Case = "0" '0 : new

                btnSave.Enabled = True
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnAddDetail.Visible = True
                btnAddNewkpi.Visible = True

                'status
                statusnewkpi.Visible = False


                'กล่อง comment & attatch file
                card_comment.Visible = False
                card_attatch.Visible = False
            Case = "1" '1 : รอยืนยัน
                If createby = Session("userid") Then
                    btnSave.Enabled = True
                    btnConfirm.Enabled = True
                    btnCancel.Enabled = True

                    btnAddDetail.Visible = True
                    btnAddNewkpi.Visible = True
                Else
                    btnSave.Enabled = False
                    btnConfirm.Enabled = False
                    btnCancel.Enabled = False

                    btnAddDetail.Visible = False
                    btnAddNewkpi.Visible = False

                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "disbtndelete()"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                End If

                'status
                statusnewkpi.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True
            Case = "2" '2 : รออนุมัติ

                btnSave.Enabled = False
                btnConfirm.Enabled = False
                If createby = Session("userid") Then
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If


                btnAddDetail.Visible = False
                btnAddNewkpi.Visible = False

                'status
                statusnewkpi.Visible = True


                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "3" '3 : ได้รับการอนุมัติ

                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnAddDetail.Visible = False
                btnAddNewkpi.Visible = False

                'status
                statusnewkpi.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Case = "4" '4 : ยกเลิก

                btnSave.Enabled = False
                btnConfirm.Enabled = False
                btnCancel.Enabled = False

                btnAddDetail.Visible = False
                btnAddNewkpi.Visible = False

                'status
                statusnewkpi.Visible = True

                'กล่อง comment & attatch file
                card_comment.Visible = True
                card_attatch.Visible = True

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "disbtndelete()"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Select
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If validateSave() Then
            'If ViewState("status") = "new" Then
            'If maintable.Rows.Count = 0 Then
            updatehead()
            'End If
            SaveBeforeConfirm()
            'Else
            '    SaveEdit()
            'End If
        Else
            GoTo endprocess
        End If
        If validateConfirm() Then
            Dim objKpi As New Kpi
            Try
                objKpi.Confirm(Request.QueryString("NewKpiCode").ToString(), Session("usercode"))
            Catch ex As Exception
                Dim scriptKey As String = "alert"
                'Dim javaScript As String = "alert('" & ex.Message & "');"
                Dim javaScript As String = "alertWarning('Confirm fail');"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
                GoTo endprocess
            End Try
            Response.Redirect("../KPI/KPIsRequest.aspx?NewKpiCode=" & Request.QueryString("NewKpiCode").ToString())
        End If
endprocess:
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Dim objKpi As New Kpi
        Try
            objKpi.Kpi_NewKPIs_Cancel(Request.QueryString("NewKpiCode"), Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('cancel fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsRequest.aspx?NewKpiCode=" & Request.QueryString("NewKpiCode").ToString())
endprocess:
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
            allOwner = "[ md : " + md_code + "],[ fm : " + fm_code + "],[ dm : " + dm_code + "],[ sm : " + sm_code + "],[ am : " + am_code + "]"
        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('approval_permission fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub
    Private Sub chkuser_sub(allusercode As String, userid As Integer)

        Dim strSplit As Array
        Dim i As Integer
        strSplit = allusercode.Split(",")

        Dim cnt As Integer = strSplit.Length - 1
        For i = 0 To cnt
            If Not Session("userid") = userid And
                Not Session("usercode") = strSplit(i) And
                Not Session("secid").ToString = "2" And
                Not Session("secid").ToString = "35" And
                Not Session("depid").ToString = "24" And
                Not Session("depid").ToString = "25" And
                Not Session("depid").ToString = "2" And
                Not Session("depid").ToString = "4" Then
                flag = False
            End If

        Next
    End Sub

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim objKpi As New Kpi

        Try
            objKpi.Kpi_NewKPIs_Allow(Request.QueryString("NewKpiCode"), Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsRequest.aspx?NewKpiCode=" & Request.QueryString("NewKpiCode").ToString())
endprocess:
    End Sub

    Private Sub btnSendback_Click(sender As Object, e As EventArgs) Handles btnSendback.Click
        Dim objKpi As New Kpi

        Try
            objKpi.KPI_NewKPIs_Send_To_Edit(Request.QueryString("NewKpiCode"), Session("usercode"))

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('Approval fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsRequest.aspx?NewKpiCode=" & Request.QueryString("NewKpiCode").ToString())
endprocess:
    End Sub
    Private Sub btnSaveComment_Click(sender As Object, e As EventArgs) Handles btnSaveComment.Click
        Dim approval As New Approval

        Try
            approval.Save_Comment_By_Code(Request.QueryString("NewKpiCode"), txtComment.Text.Trim(), Session("userid"))
            txtComment.Text = ""

        Catch ex As Exception
            Dim scriptKey As String = "alert"
            'Dim javaScript As String = "alert('" & ex.Message & "');"
            Dim javaScript As String = "alertWarning('SaveComment fail');"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            GoTo endprocess
        End Try
        Response.Redirect("../KPI/KPIsRequest.aspx?NewKpiCode=" & Request.QueryString("NewKpiCode").ToString())

endprocess:
    End Sub

    Private Sub KPIsRequest_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim kpiGroup = detailtable.AsEnumerable().
        GroupBy(Function(row) New With {
            Key .Name = row.Field(Of String)("categoryname")
        })

        groupdetailtable = New DataTable
        groupdetailtable.Columns.Add("categoryname", GetType(String))
        groupdetailtable.Columns.Add("weight", GetType(Integer))

        Dim newrows As DataRow
        For Each grp In kpiGroup
            newrows = groupdetailtable.NewRow()
            newrows("categoryname") = grp.Key.Name
            newrows("weight") = grp.Sum(Function(row) row.Field(Of Double)("weight"))
            groupdetailtable.Rows.Add(newrows)
        Next
    End Sub
End Class