Public Class KPIsRequest
    Inherits System.Web.UI.Page

    Public menutable As DataTable
    Public detailtable As DataTable '= createdetailtable()
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

        If Not IsPostBack() Then
            detailtable = createdetailtable()

            objKpi.SetCboPeriod(cboPeriod)
            detailtable = objKpi.Get_Weight_by_Period_and_Usercode(cboPeriod.SelectedItem.Value, usercode)


            Session("detailtable_request") = detailtable
        Else

            detailtable = Session("detailtable_request")
        End If
    End Sub

    Private Function createdetailtable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("positionlevel", GetType(Integer))
        dt.Columns.Add("categoryname", GetType(String))
        dt.Columns.Add("category_id", GetType(Integer))
        dt.Columns.Add("weight", GetType(String))
        dt.Columns.Add("kpiperiod_id", GetType(Integer))
        Return dt
    End Function
End Class