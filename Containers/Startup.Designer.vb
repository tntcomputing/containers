<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Startup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Startup))
        Me.lblVersion = New System.Windows.Forms.Label
        Me.imgIBS_Logo = New System.Windows.Forms.PictureBox
        Me.label2 = New System.Windows.Forms.Label
        Me.lblLoading = New System.Windows.Forms.Label
        Me.LinkUrl = New System.Windows.Forms.LinkLabel
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.ForeColor = System.Drawing.Color.Green
        Me.lblVersion.Location = New System.Drawing.Point(3, 92)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(234, 17)
        Me.lblVersion.Text = "Version "
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'imgIBS_Logo
        '
        Me.imgIBS_Logo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.imgIBS_Logo.Image = CType(resources.GetObject("imgIBS_Logo.Image"), System.Drawing.Image)
        Me.imgIBS_Logo.Location = New System.Drawing.Point(36, 204)
        Me.imgIBS_Logo.Name = "imgIBS_Logo"
        Me.imgIBS_Logo.Size = New System.Drawing.Size(173, 60)
        Me.imgIBS_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'label2
        '
        Me.label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label2.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold)
        Me.label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.label2.Location = New System.Drawing.Point(3, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(234, 89)
        Me.label2.Text = "IQ WORKABOUT CONTAINER SYSTEM"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblLoading
        '
        Me.lblLoading.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLoading.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.lblLoading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblLoading.Location = New System.Drawing.Point(3, 137)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(234, 27)
        Me.lblLoading.Text = "Loading..."
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LinkUrl
        '
        Me.LinkUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkUrl.Location = New System.Drawing.Point(3, 302)
        Me.LinkUrl.Name = "LinkUrl"
        Me.LinkUrl.Size = New System.Drawing.Size(234, 19)
        Me.LinkUrl.TabIndex = 24
        Me.LinkUrl.Text = "http://www.initbusy.com"
        Me.LinkUrl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label1
        '
        Me.label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label1.Location = New System.Drawing.Point(3, 270)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(234, 31)
        Me.label1.Text = "© Initiative Business Systems Ltd 2015 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "All rights reserved"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Startup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.imgIBS_Logo)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.lblLoading)
        Me.Controls.Add(Me.LinkUrl)
        Me.Controls.Add(Me.label1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Startup"
        Me.Text = "Startup"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblVersion As System.Windows.Forms.Label
    Private WithEvents imgIBS_Logo As System.Windows.Forms.PictureBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents lblLoading As System.Windows.Forms.Label
    Private WithEvents LinkUrl As System.Windows.Forms.LinkLabel
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
