Public Class OilPriceFormulaUpdate
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public formulatable As DataTable = createformulatable()
    Public rowid As Double
    Public usercode, username


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        usercode = Session("usercode")
        username = Session("username")
        Dim branch As Integer = Integer.Parse(Request.QueryString("branch"))

        lblBranch.Text = branch

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            formulatable = Session("formula")

            BindData()
        Else


            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            GetData(branch)
            BindData()
        End If

    End Sub
    Private Sub GetData(ByRef branch As String)

        Dim objprice As New Price
        Dim mydatatable As DataTable
        mydatatable = objprice.Oil_Day_Price_Branch_Formula_View(branch)


        Session("formula") = mydatatable
        With mydatatable.Rows(0)
            lblBranch.Text = .Item("branchid")
            txtdiffbkk.Text = .Item("diffbkk")
            txttax.Text = .Item("tax")
            txtbegindate.Text = .Item("begindate")
            If IsDBNull(.Item("enddate")) Then
                lblenddate.Text = ""
            Else
                lblenddate.Text = .Item("enddate")
            End If

            lblcreateby.Text = .Item("createby")
            lblcreatedate.Text = .Item("createdate")
            rowid = .Item("rowid")
            Session("rowid") = rowid
            txtb7.Text = .Item("b7add")
            txtb10.Text = .Item("b10add")
            txtg91.Text = .Item("g91add")
            txtg95.Text = .Item("g95add")
            txte20.Text = .Item("e20add")
            txtpado.Text = .Item("padoadd")
            txtpg95.Text = .Item("pg95add")



        End With


    End Sub

    Private Function createformulatable() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("grade_title", GetType(String))
        dt.Columns.Add("volume", GetType(Double))
        dt.Columns.Add("price", GetType(Double))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("total", GetType(Double))

        Return dt
    End Function



    Private Sub BindData()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim begindate As DateTime = DateTime.Parse(txtbegindate.Text)
        If begindate.Date >= Now.Date Then
            Save()
        Else
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String
            javaScript = "<script type='text/javascript'>msgalert('ไม่สามารถบันทึกข้อมูลย้อนหลังได้ กรุณาตรวจสอบวันที่');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End If
    End Sub

    Private Sub Save()
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String

        Dim objprice As New Price
        Dim branch As Double
        Dim diffbkk, tax, b7, b10, g91, g95, e20, pado, pg95 As Double
        Dim begindate As DateTime = DateTime.Parse(txtbegindate.Text)
        Dim errmsg As String = ""

        branch = Double.Parse(lblBranch.Text)
        Try
            diffbkk = Double.Parse(txtdiffbkk.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล ส่วนต่าง สนพ ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            tax = Double.Parse(txttax.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล ภาษี ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            b7 = Double.Parse(txtb7.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล B7 ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            b10 = Double.Parse(txtb10.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล B10 ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            g91 = Double.Parse(txtg91.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล Gasohol 91 ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            g95 = Double.Parse(txtg95.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล Gasohol 95 ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            e20 = Double.Parse(txte20.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล Gasohol E20 ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            pado = Double.Parse(txtpado.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล PADO ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            pg95 = Double.Parse(txtpg95.Text)
        Catch ex As Exception
            errmsg = "ข้อมูล PG 95 ไม่ใช่ตัวเลข"
            GoTo error_handler
        End Try
        Try
            objprice.Oil_Day_Price_Branch_Formula_Save(Session("rowid"), branch, diffbkk, tax, b7, b10, g91, g95, e20, pado, pg95, begindate, usercode)
        Catch ex As Exception
            javaScript = "<script type='text/javascript'>msgalert('" & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
        End Try
        GoTo EndProcess
error_handler:

        javaScript = "<script type='text/javascript'>msgalert('" & errmsg & "');</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
EndProcess:
    End Sub


    'Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDataAR.RowDataBound
    '    '*** chk ***'
    '    Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
    '    If Not IsNothing(chk) Then
    '        chk.Checked = e.Row.DataItem("chk")
    '    End If
    '    '*** vbs_whtaxid ***'
    '    Dim lblbranch As Label = CType(e.Row.FindControl("lblbranch"), Label)
    '    If Not IsNothing(lblbranch) Then
    '        lblbranch.Text = e.Row.DataItem("br_code")
    '    End If

    'End Sub
End Class