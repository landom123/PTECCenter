

Imports System.IO

Imports ClosedXML.Excel
Public Class titlename
    Inherits System.Web.UI.Page
    Public menutable As DataTable
    Public titlenametable As DataTable = create()
    Public usercode, username
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If Not (Session("titlename") Is Nothing) Then
                titlenametable = Session("titlename")
                BindData()
            Else
                titlenametable = objTitleName.List
                BindData()
            End If
        End If

    End Sub

    Private Function create() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("titleid", GetType(Double))
        dt.Columns.Add("titlename", GetType(String))

        Return dt
    End Function


    Private Sub BindData()
        gvData.DataSource = titlenametable
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
        Dim branch As String

        If e.CommandName = "Create" Then
            ''Determine the RowIndex of the Row whose Button was clicked.
            rowIndex = Convert.ToInt32(e.CommandArgument)
            row = gvData.Rows(rowIndex)

            branch = Double.Parse(TryCast(row.FindControl("lblbranch"), Label).Text)
            branch = Strings.Right("000" & branch.ToString, 3)

            Try

            Catch ex As Exception
                Dim msg As String = Strings.Replace(ex.Message, "'", " ")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between create data : " & msg & "');", True)
            End Try


        End If

        If e.CommandName = "Download" Then
            rowIndex = Convert.ToInt32(e.CommandArgument)
            row = gvData.Rows(rowIndex)

            branch = Double.Parse(TryCast(row.FindControl("lblbranch"), Label).Text)
            branch = Strings.Right("000" & branch.ToString, 3)

            Try

            Catch ex As Exception
                Dim msg As String = Strings.Replace(ex.Message, "'", " ")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Error between create data : " & msg & "');", True)
            End Try


        End If
    End Sub

    Private Sub gvData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvData.RowDataBound

    End Sub
End Class