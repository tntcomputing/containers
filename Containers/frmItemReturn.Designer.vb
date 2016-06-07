<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmItemReturn
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
        Me.label3 = New System.Windows.Forms.Label
        Me.panel1 = New System.Windows.Forms.Panel
        Me.lblTitle = New System.Windows.Forms.Label
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.pnlItems = New System.Windows.Forms.Panel
        Me.btnSaveReturnBatch = New System.Windows.Forms.Button
        Me.lstItems = New System.Windows.Forms.ListBox
        Me.lstBatch = New System.Windows.Forms.ListView
        Me.ColumnID = New System.Windows.Forms.ColumnHeader
        Me.ColumnCustomer = New System.Windows.Forms.ColumnHeader
        Me.ColumnLastModified = New System.Windows.Forms.ColumnHeader
        Me.txtTestLog = New System.Windows.Forms.TextBox
        Me.lblFunction = New System.Windows.Forms.Label
        Me.txtAuid = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlMenu = New System.Windows.Forms.Panel
        Me.btnContinue = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.panel1.SuspendLayout()
        Me.pnlItems.SuspendLayout()
        Me.pnlMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(4, 73)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(233, 26)
        Me.label3.Text = "Container Return"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(3, 23)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(227, 38)
        Me.lblTitle.Text = "IQ WORKABOUT CONTAINER SYSTEM"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblCompanyName.ForeColor = System.Drawing.Color.White
        Me.lblCompanyName.Location = New System.Drawing.Point(3, 1)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(227, 22)
        Me.lblCompanyName.Text = "Initiative Business Systems Ltd"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlItems
        '
        Me.pnlItems.Controls.Add(Me.btnSaveReturnBatch)
        Me.pnlItems.Controls.Add(Me.lstItems)
        Me.pnlItems.Controls.Add(Me.lstBatch)
        Me.pnlItems.Controls.Add(Me.txtTestLog)
        Me.pnlItems.Controls.Add(Me.lblFunction)
        Me.pnlItems.Controls.Add(Me.txtAuid)
        Me.pnlItems.Controls.Add(Me.Label1)
        Me.pnlItems.Location = New System.Drawing.Point(2, 100)
        Me.pnlItems.Name = "pnlItems"
        Me.pnlItems.Size = New System.Drawing.Size(236, 178)
        '
        'btnSaveReturnBatch
        '
        Me.btnSaveReturnBatch.Location = New System.Drawing.Point(48, 141)
        Me.btnSaveReturnBatch.Name = "btnSaveReturnBatch"
        Me.btnSaveReturnBatch.Size = New System.Drawing.Size(136, 33)
        Me.btnSaveReturnBatch.TabIndex = 10
        Me.btnSaveReturnBatch.Text = "Save Return Batch"
        '
        'lstItems
        '
        Me.lstItems.Location = New System.Drawing.Point(1, 63)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(230, 72)
        Me.lstItems.TabIndex = 9
        '
        'lstBatch
        '
        Me.lstBatch.Columns.Add(Me.ColumnID)
        Me.lstBatch.Columns.Add(Me.ColumnCustomer)
        Me.lstBatch.Columns.Add(Me.ColumnLastModified)
        Me.lstBatch.FullRowSelect = True
        Me.lstBatch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstBatch.Location = New System.Drawing.Point(1, 63)
        Me.lstBatch.Name = "lstBatch"
        Me.lstBatch.Size = New System.Drawing.Size(230, 112)
        Me.lstBatch.TabIndex = 8
        Me.lstBatch.View = System.Windows.Forms.View.Details
        '
        'ColumnID
        '
        Me.ColumnID.Text = "ID"
        Me.ColumnID.Width = 25
        '
        'ColumnCustomer
        '
        Me.ColumnCustomer.Text = "Customer"
        Me.ColumnCustomer.Width = 100
        '
        'ColumnLastModified
        '
        Me.ColumnLastModified.Text = "Last Modified"
        Me.ColumnLastModified.Width = 100
        '
        'txtTestLog
        '
        Me.txtTestLog.Location = New System.Drawing.Point(1, 63)
        Me.txtTestLog.Multiline = True
        Me.txtTestLog.Name = "txtTestLog"
        Me.txtTestLog.Size = New System.Drawing.Size(230, 112)
        Me.txtTestLog.TabIndex = 5
        '
        'lblFunction
        '
        Me.lblFunction.BackColor = System.Drawing.Color.White
        Me.lblFunction.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblFunction.ForeColor = System.Drawing.Color.Teal
        Me.lblFunction.Location = New System.Drawing.Point(4, 36)
        Me.lblFunction.Name = "lblFunction"
        Me.lblFunction.Size = New System.Drawing.Size(227, 24)
        Me.lblFunction.Text = "Return Questions"
        Me.lblFunction.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtAuid
        '
        Me.txtAuid.Location = New System.Drawing.Point(48, 8)
        Me.txtAuid.Name = "txtAuid"
        Me.txtAuid.Size = New System.Drawing.Size(183, 21)
        Me.txtAuid.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.ForeColor = System.Drawing.Color.Teal
        Me.Label1.Location = New System.Drawing.Point(5, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 24)
        Me.Label1.Text = "Scan:"
        '
        'pnlMenu
        '
        Me.pnlMenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnlMenu.Controls.Add(Me.btnContinue)
        Me.pnlMenu.Controls.Add(Me.btnBack)
        Me.pnlMenu.Location = New System.Drawing.Point(0, 280)
        Me.pnlMenu.Name = "pnlMenu"
        Me.pnlMenu.Size = New System.Drawing.Size(240, 40)
        '
        'btnContinue
        '
        Me.btnContinue.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.btnContinue.Location = New System.Drawing.Point(121, 2)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(117, 36)
        Me.btnContinue.TabIndex = 1
        Me.btnContinue.Text = "CONTINUE"
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
        'frmItemReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.Controls.Add(Me.pnlMenu)
        Me.Controls.Add(Me.pnlItems)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.panel1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "frmItemReturn"
        Me.Text = "frmItemReturn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel1.ResumeLayout(False)
        Me.pnlItems.ResumeLayout(False)
        Me.pnlMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents lblTitle As System.Windows.Forms.Label
    Private WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents pnlItems As System.Windows.Forms.Panel
    Friend WithEvents txtAuid As System.Windows.Forms.TextBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTestLog As System.Windows.Forms.TextBox
    Private WithEvents lblFunction As System.Windows.Forms.Label
    Friend WithEvents pnlMenu As System.Windows.Forms.Panel
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lstBatch As System.Windows.Forms.ListView
    Friend WithEvents ColumnID As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnCustomer As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnLastModified As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstItems As System.Windows.Forms.ListBox
    Friend WithEvents btnSaveReturnBatch As System.Windows.Forms.Button
End Class
