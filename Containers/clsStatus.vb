Public Class clsStatus
    Private DH As DataHandling
    Public Sub New()

        DH = New DataHandling

    End Sub
    
    Public Sub SetItemStatus(ByVal strAUID As String, ByVal lngStatus As Long)

        Dim strSql As String

    
        strSql = "UPDATE "
        strSql = strSql & "tblItem "
        strSql = strSql & "SET "
        strSql = strSql & "Status = " & lngStatus & " "
        strSql = strSql & "WHERE "
        strSql = strSql & "AUID = '" & strAUID & "' "

        'objData.ExecuteSQL (strSQL)
        DH.ExecuteNonQuery(strSql)

    End Sub
    Public Function GetItemStatus(ByVal strAUID As String) As Long

        Dim strSql As String
        Dim dt As Data.DataTable

        strSql = "SELECT "
        strSql = strSql & "Status "
        strSql = strSql & "FROM "
        strSql = strSql & "tblItem "
        strSql = strSql & "WHERE "
        strSql = strSql & "AUID = '" & strAUID & "' "

        dt = DH.GetDataTable(strSql)

        If dt.Rows.Count > 0 Then
            GetItemStatus = IIf(IsDBNull(dt.Rows(0).Item("Status")), 0, dt.Rows(0).Item("Status"))
        Else
            GetItemStatus = 0
        End If


    End Function

End Class
