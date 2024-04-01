Imports System.Security.Principal
Imports System.Net.Mail
Imports Microsoft.Reporting.WebForms
Imports System.Net
Imports System.Windows

<Serializable()>
Public NotInheritable Class MyReportServerCredentials
    Implements IReportServerCredentials
    Public userName As String = ConfigurationManager.AppSettings("rvUser")
    Public password As String = ConfigurationManager.AppSettings("rvPassword")
    Public domain As String = ConfigurationManager.AppSettings("rvDomain")
    Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
            Implements IReportServerCredentials.ImpersonationUser
        Get
            'Use the default windows user.  Credentials will be
            'provided by the NetworkCredentials property.
            Return Nothing
        End Get
    End Property
    Public ReadOnly Property NetworkCredentials() As ICredentials _
            Implements IReportServerCredentials.NetworkCredentials
        Get
            'Read the user information from the web.config file. 
            'By reading the information on demand instead of storing
            'it, the credentials will not be stored in session,
            'reducing the vulnerable surface area to the web.config
            'file, which can be secured with an ACL.
            If (String.IsNullOrEmpty(userName)) Then
                Throw New Exception("Missing user name from web.config file")
            End If
            If (String.IsNullOrEmpty(password)) Then
                Throw New Exception("Missing password from web.config file")
            End If
            If (String.IsNullOrEmpty(domain)) Then
                Throw New Exception("Missing domain from web.config file")
            End If
            Return New NetworkCredential(userName, password, domain)
        End Get
    End Property
    Public Function GetFormsCredentials(ByRef authCookie As Cookie,
                                        ByRef userName As String,
                                        ByRef password As String,
                                        ByRef authority As String) _
                                        As Boolean _
            Implements IReportServerCredentials.GetFormsCredentials
        authCookie = Nothing
        userName = Nothing
        password = Nothing
        authority = Nothing
        'Not using form credentials
        Return False
    End Function
End Class
Module global_module
    Public dbOPS As String = "PTEC_OPS" ' System.Configuration.ConfigurationManager.AppSettings.GetValues("dbops").ToString()
    Public dbLogin As String = "ptec" 'System.Configuration.ConfigurationManager.AppSettings.GetValues("dbLogin").ToString()
    Public dbPassword As String = "ptec@pure" 'System.Configuration.ConfigurationManager.AppSettings.GetValues("dbPassword").ToString()
    Public dbServer As String = "PTECDBA" 'System.Configuration.ConfigurationManager.AppSettings.GetValues("dbServer").ToString()
    Public dbPTECMNG As String = "PTEC_MNG" 'System.Configuration.ConfigurationManager.AppSettings.GetValues("dbptecmng").ToString()


    Public Sub SendMail(ClientEmail As String, subject As String, msg As String)
        'Dim ClientEmail As String '= "pns@rpcthai.com"
        Dim emailsubject, emailmessage, mailserver, mailuser, mailpassword As String
        Dim smtpport As Integer

        mailserver = "smtp.gmail.com" '"smtp.live.com:587" 'smtp-relay.gmail.com'aspmx.l.google.com:25
        mailuser = "noreply.ptec@gmail.com" 'pab.pure@hotmail.com:pab@pure
        'สร้างไว้ส่งเมล์อัตโนมัติ ผู้กับ pab@rpcthai.com
        mailpassword = "ptec@pure"
        smtpport = 587
        Try

            Dim Mail As MailMessage = New MailMessage
            Mail.From = New Net.Mail.MailAddress(mailuser)

            emailsubject = subject
            emailmessage = msg
            'Mail.To.Add("")
            Mail.To.Add(ClientEmail)
            Mail.Subject = emailsubject
            Mail.IsBodyHtml = True
            Mail.Body = emailmessage


            Dim smtp As New SmtpClient(mailserver)
            smtp.UseDefaultCredentials = True 'ต้องอยู่ก่อน  smtp.Credentials = New System.Net.NetworkCredential(mailuser, mailpassword)
            smtp.Port = smtpport
            smtp.Host = mailserver
            smtp.EnableSsl = True

            smtp.Credentials = New System.Net.NetworkCredential(mailuser, mailpassword)
            smtp.Send(Mail)
        Catch ex As Exception

        End Try
    End Sub


    Public Sub SetCboProjectStatus(obj As Object)
        Dim ag As New Contract

        obj.DataSource = ag.Ag_Status_list
        obj.DataValueField = "agstatusid"
        obj.DataTextField = "agstatus"
        obj.DataBind()


    End Sub

    Public Sub SetCboAssetGroup(obj As Object)
        Dim assets As New Assets

        obj.DataSource = assets.Grouplist
        obj.DataValueField = "assetgroupid"
        obj.DataTextField = "name"
        obj.DataBind()


    End Sub
    Public Sub SetCboAssetType(obj As Object, grpid As Integer)
        Dim assets As New Assets

        obj.DataSource = assets.Typelist(grpid)
        obj.DataValueField = "assettypeid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub
    Public Sub SetCboAssetItemType(obj As Object)
        Dim assets As New Assets

        obj.DataSource = assets.ItemTypelist()
        obj.DataValueField = "assetitemtypeid"
        obj.DataTextField = "name"
        obj.DataBind()

    End Sub

    Public Sub SetCboJobType(obj As Object, usercode As String, Optional mode As String = "all")
        Dim job As New jobs

        obj.DataSource = job.JobType_List(usercode, mode)
        obj.DataValueField = "jobtypeid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub

    Public Sub SetCboJobCate(obj As Object, DepID As Integer)
        Try
            Dim job As New jobs

            obj.DataSource = job.JobCate_List(DepID)
            obj.DataValueField = "ID"
            obj.DataTextField = "Catename"
            obj.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub SetCboJobGroup(obj As Object, CateID As Integer)
        Try
            Dim job As New jobs

            obj.DataSource = job.JobGroup_List(CateID)
            obj.DataValueField = "ID"
            obj.DataTextField = "Groupname"
            obj.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub SetCboJobDescription(obj As Object, GroupID As Integer)
        Try
            Dim job As New jobs

            obj.DataSource = job.JobDescription_List(GroupID)
            obj.DataValueField = "ID"
            obj.DataTextField = "Jobname"
            obj.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Public Sub SetCboJobTypeByDepID(obj As Object, depid As String, Optional mode As String = "all")
        Dim job As New jobs

        obj.DataSource = job.JobType_List_By_Depid(depid, mode)
        obj.DataValueField = "jobtypeid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub

    Public Sub SetCboUnit(obj As Object)
        Dim unit As New units

        obj.DataSource = unit.List()
        obj.DataValueField = "unitid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboUsers(obj As Object)
        Dim user As New Users

        obj.DataSource = user.List()
        obj.DataValueField = "userid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboUsersCO(obj As Object)
        Dim user As New Users

        obj.DataSource = user.ListCO()
        obj.DataValueField = "userid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboUsersOnly(obj As Object)
        Dim user As New Users
        Dim dt As New DataTable
        dt = user.List()
        dt.Rows(0).Delete()

        obj.DataSource = dt
        obj.DataValueField = "userid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Function LoadMenu(usercode As String) As DataTable
        Dim menu As New menu
        Dim result As DataTable
        result = menu.list(usercode)
        Return result
    End Function

    Public Function LoadMenuAll() As DataSet
        Dim menu As New menu
        Dim result As DataSet
        result = menu.menu_listAll()
        Return result
    End Function

    Public Function LoadBranchAll() As DataTable
        Dim branch As New Branch
        Dim result As DataTable
        result = branch.BranchListAll()
        Return result
    End Function


End Module

