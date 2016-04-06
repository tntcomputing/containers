Public Class MainMenu
    Public cReturnValue As String = ""
    
    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.cReturnValue = "Back"
        Me.Close()
    End Sub
    
    

   
    Private Sub cmdIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIssue.Click
        Me.cReturnValue = "Issue"
        Me.Close()
    End Sub

    Private Sub cmdReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.cReturnValue = "Return"
        Me.Close()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.cReturnValue = "Back"
        Me.Close()
    End Sub
End Class