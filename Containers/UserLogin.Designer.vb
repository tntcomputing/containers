<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class UserLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserLogin))
        Me.lblTitle = New System.Windows.Forms.Label
        Me.panel_inner = New System.Windows.Forms.Panel
        Me.pictureBox2 = New System.Windows.Forms.PictureBox
        Me.label5 = New System.Windows.Forms.Label
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.panel_invalid = New System.Windows.Forms.Panel
        Me.panel2 = New System.Windows.Forms.Panel
        Me.panel1 = New System.Windows.Forms.Panel
        Me.lblCaption = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.lstUsers = New System.Windows.Forms.ComboBox
        Me.lblPassword = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.cmdEXIT = New System.Windows.Forms.MenuItem
        Me.cmdAbout = New System.Windows.Forms.MenuItem
        Me.cmdLogin = New System.Windows.Forms.MenuItem
        Me.InputPanel1 = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.panel_inner.SuspendLayout()
        Me.panel_invalid.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(3, 22)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(227, 38)
        Me.lblTitle.Text = "IQ WORKABOUT CONTAINER SYSTEM"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panel_inner
        '
        Me.panel_inner.BackColor = System.Drawing.SystemColors.Info
        Me.panel_inner.Controls.Add(Me.pictureBox2)
        Me.panel_inner.Controls.Add(Me.label5)
        Me.panel_inner.Location = New System.Drawing.Point(3, 3)
        Me.panel_inner.Name = "panel_inner"
        Me.panel_inner.Size = New System.Drawing.Size(200, 51)
        '
        'pictureBox2
        '
        Me.pictureBox2.Image = CType(resources.GetObject("pictureBox2.Image"), System.Drawing.Image)
        Me.pictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(44, 45)
        '
        'label5
        '
        Me.label5.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.label5.Location = New System.Drawing.Point(55, 9)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(137, 34)
        Me.label5.Text = "Invalid Login Credentials"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblCompanyName.ForeColor = System.Drawing.Color.White
        Me.lblCompanyName.Location = New System.Drawing.Point(3, 0)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(227, 22)
        Me.lblCompanyName.Text = "Initiative Business Systems Ltd"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panel_invalid
        '
        Me.panel_invalid.BackColor = System.Drawing.Color.Gray
        Me.panel_invalid.Controls.Add(Me.panel_inner)
        Me.panel_invalid.Location = New System.Drawing.Point(17, 229)
        Me.panel_invalid.Name = "panel_invalid"
        Me.panel_invalid.Size = New System.Drawing.Size(206, 57)
        Me.panel_invalid.Visible = False
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.panel2.Controls.Add(Me.lblTitle)
        Me.panel2.Controls.Add(Me.lblCompanyName)
        Me.panel2.Location = New System.Drawing.Point(4, 8)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(233, 60)
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.panel1.Location = New System.Drawing.Point(4, 100)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(232, 2)
        '
        'lblCaption
        '
        Me.lblCaption.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.lblCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.lblCaption.Location = New System.Drawing.Point(4, 74)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(233, 26)
        Me.lblCaption.Text = "User Login"
        Me.lblCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.txtPassword.Location = New System.Drawing.Point(19, 188)
        Me.txtPassword.MaxLength = 30
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(200, 26)
        Me.txtPassword.TabIndex = 8
        '
        'lstUsers
        '
        Me.lstUsers.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lstUsers.Location = New System.Drawing.Point(19, 130)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.Size = New System.Drawing.Size(200, 27)
        Me.lstUsers.TabIndex = 11
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblPassword.ForeColor = System.Drawing.Color.Teal
        Me.lblPassword.Location = New System.Drawing.Point(4, 165)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(233, 20)
        Me.lblPassword.Text = "Password"
        '
        'lblUserName
        '
        Me.lblUserName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblUserName.ForeColor = System.Drawing.Color.Teal
        Me.lblUserName.Location = New System.Drawing.Point(4, 107)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(233, 20)
        Me.lblUserName.Text = "Username"
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.Add(Me.MenuItem2)
        Me.mainMenu1.MenuItems.Add(Me.cmdLogin)
        '
        'MenuItem2
        '
        Me.MenuItem2.MenuItems.Add(Me.cmdEXIT)
        Me.MenuItem2.MenuItems.Add(Me.cmdAbout)
        Me.MenuItem2.Text = "OPTIONS"
        '
        'cmdEXIT
        '
        Me.cmdEXIT.Text = "EXIT"
        '
        'cmdAbout
        '
        Me.cmdAbout.Text = "ABOUT"
        '
        'cmdLogin
        '
        Me.cmdLogin.Text = "LOGIN"
        '
        'UserLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.panel_invalid)
        Me.Controls.Add(Me.panel2)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.lblCaption)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lstUsers)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUserName)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Menu = Me.mainMenu1
        Me.MinimizeBox = False
        Me.Name = "UserLogin"
        Me.Text = "UserLogin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel_inner.ResumeLayout(False)
        Me.panel_invalid.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblTitle As System.Windows.Forms.Label
    Private WithEvents panel_inner As System.Windows.Forms.Panel
    Private WithEvents pictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents lblCompanyName As System.Windows.Forms.Label
    Private WithEvents panel_invalid As System.Windows.Forms.Panel
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents lblCaption As System.Windows.Forms.Label
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents lstUsers As System.Windows.Forms.ComboBox
    Private WithEvents lblPassword As System.Windows.Forms.Label
    Private WithEvents lblUserName As System.Windows.Forms.Label
    Private WithEvents mainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents cmdEXIT As System.Windows.Forms.MenuItem
    Friend WithEvents cmdAbout As System.Windows.Forms.MenuItem
    Friend WithEvents cmdLogin As System.Windows.Forms.MenuItem
    Friend WithEvents InputPanel1 As Microsoft.WindowsCE.Forms.InputPanel
End Class
