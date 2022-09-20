

Public Class estimate_payment_byContract
    Inherits System.Web.UI.Page
    Public menutable As DataTable


    Public onetimetable As DataTable = createOnetime()
    Public fixtabletable As DataTable = createFix()
    Public flexibletable As DataTable = createFlex()

    Public usercode, username, projectno, contractno As String
    'Public projectid As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objgsm As New gsm

        'txtContractDate.Attributes.Add("readonly", "readonly")
        'txtContractActiveDate.Attributes.Add("readonly", "readonly")
        'txtprojectno.Attributes.Add("readonly", "readonly")

        If Session("usercode") Is Nothing Then
            Session("pre_page") = Request.Url.ToString()
            Response.Redirect("~/login.aspx")
        End If

        usercode = Session("usercode")
        username = Session("username")
        'txtCloseDate.Attributes.Add("readonly", "readonly")

        'Dim objsupplier As New Supplier

        If IsPostBack() Then
            projectno = Session("projectno")
            contractno = Session("contractno")
            txtContractNo.Text = contractno
            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            onetimetable = Session("onetime")
            BindDataOnetime()

            flexibletable = Session("flex")
            BindDataFlex()

            fixtabletable = Session("fix")
            BindDataFix()


        Else

            '            SetCboContractType(cboContractType)

            If Session("menulist") Is Nothing Then
                menutable = LoadMenu(usercode)
                Session("menulist") = menutable
            Else
                menutable = Session("menulist")
            End If

            projectno = Request.QueryString("projectno")
            contractno = Request.QueryString("agreeno")
            Session("projectno") = projectno
            Session("contractno") = contractno
            txtContractNo.Text = contractno
            'txtprojectno.Text = projectno
            Find(contractno)
            'If Not (Session("client") Is Nothing) Then
            '    clienttable = Session("client")
            '    BindDataClient()
            'End If
        End If
    End Sub

    Private Sub Find(contractno As String)
        Dim objContract As New Contract
        Dim mydataset As DataSet
        Dim volume As Double
        Try
            volume = Double.Parse(txtVolume.Text)
        Catch ex As Exception
            volume = 0
        End Try


        If contractno = "" Then
            'NewData()
        Else
            Try
                mydataset = objContract.Ag_Payment_Table_Calcuate(0, volume, contractno)

                'ViewContract(mydataset.Tables(0))

                onetimetable = mydataset.Tables(0)
                Session("onetime") = onetimetable
                BindDataOnetime()

                flexibletable = mydataset.Tables(2)
                Session("flex") = flexibletable
                BindDataFlex()

                fixtabletable = mydataset.Tables(1)
                Session("fix") = fixtabletable
                BindDataFix()

            Catch ex As Exception
                'show error
                Dim err As String
                err = Strings.Replace(ex.Message, "'", " ")
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "alertWarning('001 : " & err & "')"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
            End Try
        End If
    End Sub
    Public Sub BindDataFlex()
        gvPaymentFlex.DataSource = flexibletable
        gvPaymentFlex.DataBind()
    End Sub

    Public Sub BindDataFix()
        gvPaymentFix.DataSource = fixtabletable
        gvPaymentFix.DataBind()
    End Sub

    Public Sub BindDataOnetime()
        gvOneTime.DataSource = onetimetable
        gvOneTime.DataBind()
    End Sub

    Private Sub SetCboContractType(obj As Object)
        Dim ag As New Contract

        obj.DataSource = ag.ContractTypeCbo()
        obj.DataValueField = "agtypeid"
        obj.DataTextField = "agtype"
        obj.DataBind()
    End Sub

    Private Function ValidateData() As Boolean
        Dim result As Boolean = True
        Dim scriptKey As String
        Dim javaScript As String

        'If String.IsNullOrEmpty(txtbranch.Text) Then
        '    result = False
        '    scriptKey = "UniqueKeyForThisScript"
        '    javaScript = "alertWarning('003 : กรุณาระบุข้อมูลสาขา')"
        '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript, True)
        'End If
        Return result
    End Function


    Private Function createFix() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("agid", GetType(Double))
        dt.Columns.Add("flatrateid", GetType(Double))
        dt.Columns.Add("recurringtype", GetType(String))
        dt.Columns.Add("frequency", GetType(Integer))
        dt.Columns.Add("paymenttypeid", GetType(Integer))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("duedate", GetType(DateTime))
        dt.Columns.Add("amount", GetType(Double))


        Return dt
    End Function


    Private Function createFlex() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("agid", GetType(Double))
        dt.Columns.Add("flexrateid", GetType(Double))
        dt.Columns.Add("recurringtype", GetType(String))
        dt.Columns.Add("frequency", GetType(Integer))
        dt.Columns.Add("paymenttypeid", GetType(Integer))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("duedate", GetType(DateTime))
        dt.Columns.Add("amount", GetType(Double))
        dt.Columns.Add("sale_volume", GetType(Double))

        Return dt
    End Function

    Private Function createOnetime() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("agid", GetType(Double))
        dt.Columns.Add("paymenttypeid", GetType(Integer))
        dt.Columns.Add("paymenttype", GetType(String))
        dt.Columns.Add("duedate", GetType(String))
        dt.Columns.Add("amount", GetType(String))


        Return dt
    End Function


End Class