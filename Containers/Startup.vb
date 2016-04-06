Imports System.Reflection
Public Class Startup
   
    Private Sub Startup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim myassembly As Assembly = Assembly.GetExecutingAssembly
        Dim myassemblyname As AssemblyName = myassembly.GetName
        Dim myversion As Version = myassemblyname.Version
        Me.lblVersion.Text = "Version " + myversion.ToString

        Cursor.Current = Cursors.Default

        While (True)


            Dim frmLogin As New UserLogin
            frmLogin.ShowDialog()

            If frmLogin.lRelaunch = False Then
                Exit While
            End If
            'Cursor.Current = Cursors.WaitCursor
            Me.Refresh()
            Application.DoEvents()
            System.Threading.Thread.Sleep(100)


            frmLogin.Dispose()
            'Cursor.Current = Cursors.Default
        End While


        Application.Exit()

    End Sub

    Private Sub LinkUrl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkUrl.Click
        System.Diagnostics.Process.Start("IExplore.exe", "http://www.initbusy.com")

    End Sub

    Private Sub imgIBS_Logo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgIBS_Logo.Click
        Me.LinkUrl_Click(Nothing, Nothing)
    End Sub
End Class