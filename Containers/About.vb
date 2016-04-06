Imports System.Reflection
Public Class About

    Private Sub imgIBS_Logo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgIBS_Logo.Click
        Me.LinkUrl_Click(Nothing, Nothing)
    End Sub
    Private Sub LinkUrl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkUrl.Click
        System.Diagnostics.Process.Start("IExplore.exe", "http://www.initbusy.com")
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub About_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myassembly As Assembly = Assembly.GetExecutingAssembly
        Dim myassemblyname As AssemblyName = myassembly.GetName
        Dim myversion As Version = myassemblyname.Version
        Me.lblVersion.Text = "Version " + myversion.ToString
    End Sub
End Class