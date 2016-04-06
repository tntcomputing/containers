Public Class clsHistoryItem
    Public Function HasHistory(ByVal AUID) As Boolean
        '-------------------------------------------------
        'Returns true if the object has any history

        Dim strSQL As String
        Dim DH As New DataHandling
        Dim DT As Data.DataTable

       
        strSQL = "Select "
        strSQL = strSQL & "count(*) "
        strSQL = strSQL & "from "
        strSQL = strSQL & "tblItemTest "
        strSQL = strSQL & "where "
        strSQL = strSQL & "tblItemTest.AUID='" & AUID & "'"

        ' 
        DT = DH.GetDataTable(strSQL)

        If DT.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If

       
        
    End Function
End Class
