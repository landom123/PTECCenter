

Imports System.IO

Imports ClosedXML.Excel
Public Class client_list
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public clienttable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim client As New AgreeClient

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

            If Not (Session("clienttable") Is Nothing) Then
                clienttable = Session("clienttable")
                BindData()
            Else
                clienttable = client.Client_List("", "")
                BindData()
            End If
        End If

    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("clientid", GetType(Double))
        dt.Columns.Add("clientno", GetType(String))
        dt.Columns.Add("client", GetType(String))

        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = clienttable
        gvData.DataBind()
    End Sub

    Public Sub Save()

    End Sub

    Private Sub gvData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        BindData()
    End Sub


    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim objgsm As New gsm
        Dim rowIndex As Integer
        Dim row As GridViewRow
        Dim clientid As Double
        Dim clientno As String

        If e.CommandName = "View" Then
            ''Determine the RowIndex of the Row whose Button was clicked.
            rowIndex = Convert.ToInt32(e.CommandArgument)
            row = gvData.Rows(rowIndex)

            clientid = Double.Parse(TryCast(row.FindControl("lblid"), Label).Text)
            clientno = TryCast(row.FindControl("lblclientno"), Label).Text
            Try
                Response.Redirect("clientinfo.aspx?clientno=" & clientno)
            Catch ex As Exception
                Dim msg As String = Strings.Replace(ex.Message, "'", " ")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between find data : " & msg & "');", True)
            End Try


        End If

    End Sub

    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound

    End Sub
End Class