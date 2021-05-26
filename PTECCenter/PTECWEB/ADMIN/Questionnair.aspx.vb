Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Questionnair
    Inherits System.Web.UI.Page
    Public usercode, username As String
    Public menutable As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("usercode") Is Nothing Then
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



        If Not IsPostBack() Then

        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim ans As String
        ans = ans01.SelectedValue & "/" & ans02.SelectedValue & "/" & ans03.SelectedValue & "/" & ans04.SelectedValue &
             "/" & ans05.SelectedValue & "/" & ans06.SelectedValue & "/" & ans07.SelectedValue & "/" & ans08.SelectedValue &
             "/" & ans09.SelectedValue & "/" & ans10.SelectedValue & "/" & ans11.SelectedValue & "/" & ans12.SelectedValue &
             "/" & ans13.SelectedValue & "/" & ans14.SelectedValue & "/" & ans15.SelectedValue & "/" & ans16.SelectedValue &
             "/" & ans17.SelectedValue & "/" & ans18.SelectedValue & "/" & ans19.SelectedValue & "/" & ans20.SelectedValue
        save(ans)
    End Sub

    Private Sub save(ans As String)


        Dim result As String
        'Credit_Balance_List_Createdate
        Dim ds As New DataSet
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnnstr_mng").ConnectionString)
        Dim cmd As New SqlCommand
        Dim adp As New SqlDataAdapter

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = "Question_Answer_Save"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@formid", SqlDbType.BigInt).Value = 1
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usercode
        cmd.Parameters.Add("@ans", SqlDbType.VarChar).Value = ans

        Try
            adp.SelectCommand = cmd
            adp.Fill(ds)
            result = ds.Tables(0).Rows(0).Item("score")
            lblstatus.Text = result
        Catch ex As Exception
            Throw ex
        End Try


        'result = ds.Tables(0).Rows(0).Item("code")
        conn.Close()

        'Return result
    End Sub
End Class