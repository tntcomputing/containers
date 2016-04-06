Public Class clsHistory
    Dim objHistoryItem As clsHistoryItem

    Public Function HasHistory(ByVal AUID As String) As Boolean
        '-----------------------------------------------------------------------------
        'Returns true if the object has any history.  Used for equipment and lcoations




        objHistoryItem = New clsHistoryItem
        HasHistory = objHistoryItem.HasHistory(AUID)
        objHistoryItem = Nothing



    End Function
End Class
