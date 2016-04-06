<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class MainMenu
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
        Me.lblTitle = New System.Windows.Forms.Label
        Me.panel2 = New System.Windows.Forms.Panel
        Me.label3 = New System.Windows.Forms.Label
        Me.cmdIssue = New System.Windows.Forms.Button
        Me.lblMessage = New System.Windows.Forms.Label
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.cmdReturn = New System.Windows.Forms.Button
        Me.panel1 = New System.Windows.Forms.Panel
        Me.pnlMenu = New System.Windows.Forms.Panel
        Me.btnBack = New System.Windows.Forms.Button
        Me.panel1.SuspendLayout()
        Me.pnlMenu.SuspendLayout()
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
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.panel2.Location = New System.Drawing.Point(4, 106)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(232, 2)
        '
        'label3
        '
        Me.label3.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(4, 80)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(233, 26)
        Me.label3.Text = "Main Menu"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdIssue
        '
        Me.cmdIssue.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.cmdIssue.Location = New System.Drawing.Point(21, 141)
        Me.cmdIssue.Name = "cmdIssue"
        Me.cmdIssue.Size = New System.Drawing.Size(200, 40)
        Me.cmdIssue.TabIndex = 13
        Me.cmdIssue.Text = "Container Issue"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.White
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblMessage.ForeColor = System.Drawing.Color.Teal
        Me.lblMessage.Location = New System.Drawing.Point(7, 115)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(227, 171)
        Me.lblMessage.Text = "Please select an option below"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        'cmdReturn
        '
        Me.cmdReturn.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.cmdReturn.Location = New System.Drawing.Point(21, 191)
        Me.cmdReturn.Name = "cmdReturn"
        Me.cmdReturn.Size = New System.Drawing.Size(200, 40)
        Me.cmdReturn.TabIndex = 18
        Me.cmdReturn.Text = "Container Return"
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.panel1.Controls.Add(Me.lblTitle)
        Me.panel1.Controls.Add(Me.lblCompanyName)
        Me.panel1.Location = New System.Drawing.Point(4, 9)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(233, 60)
        '
        'pnlMenu
        '
        Me.pnlMenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnlMenu.Controls.Add(Me.btnBack)
        Me.pnlMenu.Location = New System.Drawing.Point(0, 280)
        Me.pnlMenu.Name = "pnlMenu"
        Me.pnlMenu.Size = New System.Drawing.Size(240, 40)
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.btnBack.Location = New System.Drawing.Point(2, 2)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(117, 36)
        Me.btnBack.TabIndex = 0
        Me.btnBack.Text = "BACK"
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.Controls.Add(Me.pnlMenu)
        Me.Controls.Add(Me.panel2)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.cmdIssue)
        Me.Controls.Add(Me.cmdReturn)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.lblMessage)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "MainMenu"
        Me.Text = "MainMenu"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel1.ResumeLayout(False)
        Me.pnlMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblTitle As System.Windows.Forms.Label
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents cmdIssue As System.Windows.Forms.Button
    Private WithEvents lblMessage As System.Windows.Forms.Label
    Private WithEvents lblCompanyName As System.Windows.Forms.Label
    Private WithEvents cmdReturn As System.Windows.Forms.Button
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlMenu As System.Windows.Forms.Panel
    Friend WithEvents btnBack As System.Windows.Forms.Button
End Class
