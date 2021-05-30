
Imports System.Net.Mail
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

    Public Sub SetCboJobType(obj As Object, usercode As String)
        Dim job As New jobs

        obj.DataSource = job.JobType_List(usercode)
        obj.DataValueField = "jobtypeid"
        obj.DataTextField = "name"
        obj.DataBind()
    End Sub
    Public Sub SetCboJobTypeByDepID(obj As Object, depid As String)
        Dim job As New jobs

        obj.DataSource = job.JobType_List_By_Depid(depid)
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
    Public Function LoadMenu(usercode As String) As DataTable
        Dim menu As New menu
        Dim result As DataTable
        result = menu.list(usercode)
        Return result
    End Function


End Module

