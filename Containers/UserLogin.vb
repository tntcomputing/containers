Imports SQL = System.Data.SqlClient
Public Class UserLogin
    Dim dh As New DataHandling
    Public lRelaunch As Boolean = False
    Private Sub lstUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Me.InputPanel1.Enabled = False
        If txtPassword.Text.Length < 2 Then
            Exit Sub
        End If

        If (Me.lstUsers.Text = String.Empty) Then
            Exit Sub

        End If
        Dim dt As Data.DataTable = dh.GetDataTable("Select count(*) from IQ_LS_User.tblUser where Username = '" & Me.lstUsers.Text.Trim & "' and Password = cast(hashbytes('sha1',cast(upper('" & Me.txtPassword.Text.Trim & "') as nvarchar(50))) as nvarchar(50))")
        If dt.Rows(0).Item(0) = 0 Then
            Me.panel_invalid.Visible = True
            Me.txtPassword.Text = ""
        Else
            Dim MainCol As New Loading
            MainCol.Show()
            MainCol.BeginCollecting(Me.lstUsers.SelectedValue)
            MainCol.Hide()
            MainCol.Dispose()

            Me.lRelaunch = True
            Me.Close()
        End If

           
             


               
               

    End Sub
    Private Sub cmdAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        Dim frmAbout As New About
        frmAbout.ShowDialog()
        frmAbout.Dispose()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        If (MessageBox.Show("Are you sure you want to exit the system?", "Exit System?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes) Then


            Me.lRelaunch = False
            Me.Close()
        End If
    End Sub
    Private Sub LoadUserList()

        Dim dt As Data.DataTable

        dt = dh.GetDataTable("SELECT * FROM IQ_LS_User.tblUser")
        If (dt.Rows.Count > 0) Then

            Me.lstUsers.DataSource = dt
            Me.lstUsers.ValueMember = "USERID"
            Me.lstUsers.DisplayMember = "USERNAME"
        End If


    End Sub


    Private Sub UserLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtPassword.PasswordChar = "\u25CF"
        Dim DH As New DataHandling
        Me.LoadUserList()
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress, lstUsers.KeyPress
        Me.panel_invalid.Visible = False

        If (e.KeyChar = Chr(13)) And (txtPassword.Text.Length > 2) Then
            Me.cmdLogin_Click(Nothing, Nothing)
        End If
    End Sub

End Class