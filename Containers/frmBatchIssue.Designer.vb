<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmBatchIssue
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
        Me.panel2 = New System.Windows.Forms.Panel
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.panel1 = New System.Windows.Forms.Panel
        Me.lblTitle = New System.Windows.Forms.Label
        Me.pnlSelectLocation = New System.Windows.Forms.Panel
        Me.btnViewListOfOpenBatches = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.lstLocations = New System.Windows.Forms.ComboBox
        Me.lblMessage = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.pnlLocation = New System.Windows.Forms.Panel
        Me.lblLocation = New System.Windows.Forms.Label
        Me.pnlItems = New System.Windows.Forms.Panel
        Me.txtTestLog = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lstItems = New System.Windows.Forms.ListBox
        Me.txtAuid = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.pnlMenu = New System.Windows.Forms.Panel
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnAbandon = New System.Windows.Forms.Button
        Me.pnlBatch = New System.Windows.Forms.Panel
        Me.lstBatch = New System.Windows.Forms.ListView
        Me.ColumnID = New System.Windows.Forms.ColumnHeader
        Me.ColumnCustomer = New System.Windows.Forms.ColumnHeader
        Me.ColumnLastModified = New System.Windows.Forms.ColumnHeader
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlDispatchDate = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.panel1.SuspendLayout()
        Me.pnlSelectLocation.SuspendLayout()
        Me.pnlLocation.SuspendLayout()
        Me.pnlItems.SuspendLayout()
        Me.pnlMenu.SuspendLayout()
        Me.pnlBatch.SuspendLayout()
        Me.pnlDispatchDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.panel2.Location = New System.Drawing.Point(4, 106)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(232, 2)
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
        Me.lblTitle.Location = New System.Drawing.Point(3, 22)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(227, 38)
        Me.lblTitle.Text = "IQ WORKABOUT CONTAINER SYSTEM"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlSelectLocation
        '
        Me.pnlSelectLocation.Controls.Add(Me.btnViewListOfOpenBatches)
        Me.pnlSelectLocation.Controls.Add(Me.Label4)
        Me.pnlSelectLocation.Controls.Add(Me.lstLocations)
        Me.pnlSelectLocation.Controls.Add(Me.lblMessage)
        Me.pnlSelectLocation.Location = New System.Drawing.Point(3, 106)
        Me.pnlSelectLocation.Name = "pnlSelectLocation"
        Me.pnlSelectLocation.Size = New System.Drawing.Size(239, 169)
        '
        'btnViewListOfOpenBatches
        '
        Me.btnViewListOfOpenBatches.Location = New System.Drawing.Point(21, 99)
        Me.btnViewListOfOpenBatches.Name = "btnViewListOfOpenBatches"
        Me.btnViewListOfOpenBatches.Size = New System.Drawing.Size(200, 27)
        Me.btnViewListOfOpenBatches.TabIndex = 32
        Me.btnViewListOfOpenBatches.Text = "Open Existing Batch"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.ForeColor = System.Drawing.Color.Teal
        Me.Label4.Location = New System.Drawing.Point(6, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(227, 24)
        Me.Label4.Text = "OR"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstLocations
        '
        Me.lstLocations.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lstLocations.Location = New System.Drawing.Point(21, 34)
        Me.lstLocations.Name = "lstLocations"
        Me.lstLocations.Size = New System.Drawing.Size(200, 27)
        Me.lstLocations.TabIndex = 28
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.White
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblMessage.ForeColor = System.Drawing.Color.Teal
        Me.lblMessage.Location = New System.Drawing.Point(7, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(227, 24)
        Me.lblMessage.Text = "Select a customer"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label3
        '
        Me.label3.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(4, 73)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(233, 26)
        Me.label3.Text = "Batch Issue"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlLocation
        '
        Me.pnlLocation.Controls.Add(Me.lblLocation)
        Me.pnlLocation.Location = New System.Drawing.Point(3, 106)
        Me.pnlLocation.Name = "pnlLocation"
        Me.pnlLocation.Size = New System.Drawing.Size(236, 40)
        '
        'lblLocation
        '
        Me.lblLocation.BackColor = System.Drawing.Color.White
        Me.lblLocation.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblLocation.ForeColor = System.Drawing.Color.Teal
        Me.lblLocation.Location = New System.Drawing.Point(5, 8)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(227, 24)
        Me.lblLocation.Text = "Customer: "
        '
        'pnlItems
        '
        Me.pnlItems.Controls.Add(Me.txtTestLog)
        Me.pnlItems.Controls.Add(Me.Label2)
        Me.pnlItems.Controls.Add(Me.lstItems)
        Me.pnlItems.Controls.Add(Me.txtAuid)
        Me.pnlItems.Controls.Add(Me.Label1)
        Me.pnlItems.Location = New System.Drawing.Point(3, 141)
        Me.pnlItems.Name = "pnlItems"
        Me.pnlItems.Size = New System.Drawing.Size(236, 137)
        '
        'txtTestLog
        '
        Me.txtTestLog.Location = New System.Drawing.Point(3, 37)
        Me.txtTestLog.Multiline = True
        Me.txtTestLog.Name = "txtTestLog"
        Me.txtTestLog.Size = New System.Drawing.Size(230, 97)
        Me.txtTestLog.TabIndex = 5
        Me.txtTestLog.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.ForeColor = System.Drawing.Color.Teal
        Me.Label2.Location = New System.Drawing.Point(4, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(227, 24)
        Me.Label2.Text = "Selected Items"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstItems
        '
        Me.lstItems.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstItems.Location = New System.Drawing.Point(7, 63)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(223, 67)
        Me.lstItems.TabIndex = 3
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
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(4, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(4, 104)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(232, 2)
        '
        'pnlMenu
        '
        Me.pnlMenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnlMenu.Controls.Add(Me.btnNext)
        Me.pnlMenu.Controls.Add(Me.btnAbandon)
        Me.pnlMenu.Location = New System.Drawing.Point(0, 280)
        Me.pnlMenu.Name = "pnlMenu"
        Me.pnlMenu.Size = New System.Drawing.Size(240, 40)
        '
        'btnNext
        '
        Me.btnNext.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.btnNext.Location = New System.Drawing.Point(121, 2)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(117, 36)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "NEXT"
        '
        'btnAbandon
        '
        Me.btnAbandon.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.btnAbandon.Location = New System.Drawing.Point(2, 2)
        Me.btnAbandon.Name = "btnAbandon"
        Me.btnAbandon.Size = New System.Drawing.Size(117, 36)
        Me.btnAbandon.TabIndex = 0
        Me.btnAbandon.Text = "ABANDON"
        '
        'pnlBatch
        '
        Me.pnlBatch.Controls.Add(Me.lstBatch)
        Me.pnlBatch.Controls.Add(Me.Label5)
        Me.pnlBatch.Location = New System.Drawing.Point(3, 106)
        Me.pnlBatch.Name = "pnlBatch"
        Me.pnlBatch.Size = New System.Drawing.Size(239, 169)
        '
        'lstBatch
        '
        Me.lstBatch.Columns.Add(Me.ColumnID)
        Me.lstBatch.Columns.Add(Me.ColumnCustomer)
        Me.lstBatch.Columns.Add(Me.ColumnLastModified)
        Me.lstBatch.FullRowSelect = True
        Me.lstBatch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstBatch.Location = New System.Drawing.Point(1, 34)
        Me.lstBatch.Name = "lstBatch"
        Me.lstBatch.Size = New System.Drawing.Size(233, 134)
        Me.lstBatch.TabIndex = 2
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
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.ForeColor = System.Drawing.Color.Teal
        Me.Label5.Location = New System.Drawing.Point(6, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(227, 24)
        Me.Label5.Text = "Select an Open Batch"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlDispatchDate
        '
        Me.pnlDispatchDate.Controls.Add(Me.DateTimePicker1)
        Me.pnlDispatchDate.Controls.Add(Me.Label6)
        Me.pnlDispatchDate.Location = New System.Drawing.Point(3, 106)
        Me.pnlDispatchDate.Name = "pnlDispatchDate"
        Me.pnlDispatchDate.Size = New System.Drawing.Size(233, 169)
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Label6.ForeColor = System.Drawing.Color.Teal
        Me.Label6.Location = New System.Drawing.Point(4, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(227, 24)
        Me.Label6.Text = "Select the Dispatch Date"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(1, 46)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(226, 22)
        Me.DateTimePicker1.TabIndex = 2
        '
        'frmBatchIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.Controls.Add(Me.pnlDispatchDate)
        Me.Controls.Add(Me.pnlBatch)
        Me.Controls.Add(Me.pnlSelectLocation)
        Me.Controls.Add(Me.pnlMenu)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.pnlItems)
        Me.Controls.Add(Me.pnlLocation)
        Me.Controls.Add(Me.panel2)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.panel1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "frmBatchIssue"
        Me.Text = "frmBatchIssue"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel1.ResumeLayout(False)
        Me.pnlSelectLocation.ResumeLayout(False)
        Me.pnlLocation.ResumeLayout(False)
        Me.pnlItems.ResumeLayout(False)
        Me.pnlMenu.ResumeLayout(False)
        Me.pnlBatch.ResumeLayout(False)
        Me.pnlDispatchDate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents lblCompanyName As System.Windows.Forms.Label
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlSelectLocation As System.Windows.Forms.Panel
    Private WithEvents lstLocations As System.Windows.Forms.ComboBox
    Private WithEvents lblMessage As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents pnlLocation As System.Windows.Forms.Panel
    Private WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents pnlItems As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAuid As System.Windows.Forms.TextBox
    Private WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstItems As System.Windows.Forms.ListBox
    Friend WithEvents txtTestLog As System.Windows.Forms.TextBox
    Friend WithEvents pnlMenu As System.Windows.Forms.Panel
    Friend WithEvents btnAbandon As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnViewListOfOpenBatches As System.Windows.Forms.Button
    Friend WithEvents pnlBatch As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lstBatch As System.Windows.Forms.ListView
    Friend WithEvents ColumnID As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnCustomer As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnLastModified As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlDispatchDate As System.Windows.Forms.Panel
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Private WithEvents Label6 As System.Windows.Forms.Label
End Class
