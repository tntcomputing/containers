Public Class clsUser
    Public Sub New()

    End Sub
    Public Function GetUserFullName(ByVal lngUserID As Long) As String
        Dim DH As New DataHandling
        Dim DT As Data.DataTable

        Dim strSQL As String
        strSQL = "SELECT "
        strSQL = strSQL & "Forename,Surname "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "iq_ls_user.tblUser "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "Userid = " & lngUserID

        DT = DH.GetDataTable(strSQL)

        If DT.Rows.Count > 0 Then
            Return DT.Rows(0).Item("Forename") & " " & DT.Rows(0).Item("Surname")
        Else
            Return ""
        End If


    End Function
    Public Function GetUserID(ByVal strUserName As String) As Long
        Dim DH As New DataHandling
        Dim DT As Data.DataTable
        Dim strSQL As String
        strSQL = "SELECT "
        strSQL = strSQL & "UserID "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "iq_ls_user.tblUser "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "UPPER(Username) = '" & UCase(strUserName) & "' "

        DT = DH.GetDataTable(strSQL)

        If DT.Rows.Count > 0 Then
            Return DT.Rows(0).Item("userid")
        Else
            Return 0
        End If


    End Function

End Class
