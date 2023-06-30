

Imports System.IO

Imports ClosedXML.Excel
Public Class request2
    Inherits System.Web.UI.Page

    Public menutable As DataTable
    'Public titlenametable As DataTable = create()
    Public usercode, username

    Dim objCo As New clsRequestContract
    Dim objNote As New clsRequestNoteContract
#Region "Function"


    Private Function loadData() As Boolean

        Try

            Dim dt As New DataTable

            dt = objCo.loadContractRequestEdit(txtContractno.Text)

            For Each dr As DataRow In dt.Rows

                lblVendor.Text = dr("CustName")
                lblBeginDate.Text = dr("Begindate")
                lblEndDate.Text = dr("EndDate")
                lblConTractType.Text = dr("MainConName")
                lblContractGroup.Text = dr("SubConName")
            Next

            Return True

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Private Function loadDataEdit() As Boolean

        Try
            Dim dtEd As New DataTable
            dtEd = objNote.loadContractRequestNote
            gvData.DataSource = dtEd
            gvData.DataBind()

            Return True
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            Return False
        End Try

    End Function

    Protected Sub OnRowDataBound_AttFoot(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvData, "Select$" & e.Row.RowIndex)
                e.Row.Attributes("style") = "cursor:pointer"
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChanged_AttFoot(sender As Object, e As EventArgs)
        Try
            Dim index As Integer = gvData.SelectedRow.RowIndex
            Dim iDocID As Integer = CInt(gvData.SelectedRow.Cells(0).Text)
            Dim sContractNo As String = gvData.SelectedRow.Cells(1).Text
            Dim dtNote As New DataTable

            txtdocuno.Text = gvData.SelectedRow.Cells(1).Text
            txtContractno.Text = gvData.SelectedRow.Cells(2).Text

            txtDetail.Text = gvData.SelectedRow.Cells(3).Text

            'dtNote = objCo.loadContractRequestEdit(txtContractno.Text)

            'For Each dr As DataRow In dtNote.Rows
            '    lblVendor.Text = dr("CustName")
            '    lblBeginDate.Text = DateAdd(DateInterval.Year, 543, Date.Parse(dr("Begindate")))
            '    lblEndDate.Text = DateAdd(DateInterval.Year, 543, Date.Parse(dr("EndDate")))
            '    lblConTractType.Text = dr("MainConName")
            '    lblContractGroup.Text = dr("SubConName")
            'Next


        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('บันทึกข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub


#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim objTitleName As New TitleName

            usercode = Session("usercode")
            username = Session("username")

            'Dim objsupplier As New Supplier

            If IsPostBack() Then
                If Session("menulist") Is Nothing Then
                    menutable = LoadMenu(usercode)
                    Session("menulist") = menutable
                Else
                    menutable = Session("menulist")
                End If

            Else

                If Session("menulist") Is Nothing Then
                    menutable = LoadMenu(usercode)
                    Session("menulist") = menutable
                Else
                    menutable = Session("menulist")
                End If

                'If Not (Session("titlename") Is Nothing) Then
                '    titlenametable = Session("titlename")
                '    BindData()
                'Else
                '    titlenametable = objTitleName.List
                '    BindData()
                'End If
                txtdocuno.Text = objCo.GetDocRun(2, 0)

                If loadDataEdit() = False Then
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("requestcontract.aspx")
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            If loadData() = False Then
                Exit Sub
            End If
        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

    End Sub

    Private Sub btnReEdit_Click(sender As Object, e As EventArgs) Handles btnReEdit.Click
        Try

            Dim DocNo, ContractNo, Detail, CreateBy As String

            DocNo = txtdocuno.Text
            ContractNo = txtContractno.Text
            Detail = txtDetail.Text
            CreateBy = usercode

            If objNote.AddContractRequestNote(DocNo, ContractNo, Detail, CreateBy) = False Then
                Exit Sub
            End If

            If loadDataEdit() = False Then
                Exit Sub
            End If

        Catch ex As Exception
            Dim err, scriptKey, javaScript As String
            err = ex.Message
            scriptKey = "UniqueKeyForThisScript"
            javaScript = err ' "alertSuccess('โหลดข้อมูลเรียบร้อย')"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        End Try
    End Sub
End Class