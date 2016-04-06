<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Loading
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
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Regular)
        Me.label1.ForeColor = System.Drawing.Color.Silver
        Me.label1.Location = New System.Drawing.Point(4, 128)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(233, 38)
        Me.label1.Text = "Loading..."
        Me.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Loading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.Controls.Add(Me.label1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Loading"
        Me.Text = "Loading"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
