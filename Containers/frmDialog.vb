Public Class frmDialog
    Dim m_Moving As Boolean = False
    Dim m_Offset As Point
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmDialog_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        m_Moving = True
        m_Offset = New Point(e.X, e.Y)
    End Sub

    Private Sub frmDialog_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If m_Moving Then
            Dim NewLocation As Point = Me.Location
            NewLocation.X += e.X - m_Offset.X
            NewLocation.Y += e.Y - m_Offset.Y
            Me.Location = NewLocation
        End If
    End Sub

    Private Sub frmDialog_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        m_Moving = False
    End Sub

    Public Sub New(ByVal strDescription As String, Optional ByVal strTitle As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.lblTitle.Text = strTitle
        Me.lblDescription.Text = strDescription
        Me.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - Me.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - Me.Height / 2)
        Me.txtInput.Focus()

    End Sub
    Public Overrides Property Text() As String
        Get
            Return Me.txtInput.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property
End Class