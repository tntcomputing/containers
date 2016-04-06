Imports System.Net
Public Class Loading
    Public Sub BeginCollecting(ByVal UserID As Int32)
        While (True)
            Dim frmMainMenu As New MainMenu
            frmMainMenu.ShowDialog()
            Select Case frmMainMenu.cReturnValue
                Case "Back"
                    Exit While
                Case "Issue"
                    Dim frmBatchIssue As New frmBatchIssue(UserID)
                    frmBatchIssue.ShowDialog()
                    'MessageBox.Show("Issue functionality")
                Case "Return"
                    Dim frmItemReturn As New frmItemReturn(UserID)
                    frmItemReturn.ShowDialog()

            End Select
        End While
    End Sub

   
End Class