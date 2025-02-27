Imports System.Security.Cryptography


Public Class GlobalClass

    Public dbserver As String = "10.15.100.227" '"ptecdba"
    Public dbuser As String = "ptec"
    Public dbpass As String = "ptec@pure"
    Public dbcatalog As String = "ptecmng"
    Public cnnstr As String = "Server=" & dbserver & ";Database=" & dbcatalog & ";User Id=" & dbuser & ";Password=" & dbpass & ";"

    Public hqserver As String = "10.15.100.224" '"ptechq"
    Public hquser As String = "ptec"
    Public hqpass As String = "ptec@pure"
    Public hqcatalog As String = "ptecho"
    Public hqcnnstr As String = "Server=" & dbserver & ";Database=" & dbcatalog & ";User Id=" & dbuser & ";Password=" & dbpass & ";"

    Public Sub showmsg(msg As String)
        'HttpResponse.Write("<script language=javascript>alert('" & msg & "')</script>")
    End Sub
End Class

Public Class Crypto

    Private Shared DES As New TripleDESCryptoServiceProvider
    Private Shared MD5 As New MD5CryptoServiceProvider

    Public Shared Function MD5Hash(ByVal value As String) As Byte()
        Return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value))
    End Function

    Public Shared Function Encrypt(ByVal stringToEncrypt As String, ByVal key As String) As String
        DES.Key = Crypto.MD5Hash(key)
        DES.Mode = CipherMode.ECB
        Dim Buffer As Byte() = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt)
        Return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
    End Function

    Public Shared Function Decrypt(ByVal encryptedString As String, ByVal key As String) As String
        'Try
        DES.Key = Crypto.MD5Hash(key)
            DES.Mode = CipherMode.ECB
            Dim Buffer As Byte() = Convert.FromBase64String(encryptedString)
            Return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
        'Catch ex As Exception
        '    MessageBox.Show("Invalid Key", "Decryption Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End Try
    End Function

End Class
