Public Class frmBatchIssue
    Public cReturnValue As String = ""
    Public cFunction As String
    Dim m_UserID As Int32
    Public BatchIssue As BatchIssue
    Dim objQuestionSet As clsQuestionSet
    Dim dtQuestions As System.Data.DataTable
    Dim strTesting As String = ""
    Dim bTestResult As Boolean
    Dim strTestLog As String = ""
    Dim QuestionIndex As Integer = 0
    Dim strAuditor As String
    Dim strInspectionNo As String = ""
    Dim strProcess As String = ""
    Dim strQuestionAnswer As String



    Public Sub New(ByVal UserID As Int32)
        InitializeComponent()
        m_UserID = UserID
        Dim objUser As New clsUser
        strAuditor = objUser.GetUserFullName(UserID)
        BatchIssue = New BatchIssue(UserID)
    End Sub
    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cReturnValue = "Abandon"
        Me.Close()
    End Sub
    Private Sub cmdReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cReturnValue = Me.lstLocations.SelectedValue.ToString
        Me.Close()
    End Sub
    
    
   
    Private Sub lstLocations_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstLocations.SelectedIndexChanged
        If Me.lstLocations.SelectedIndex <> 0 Then

            Me.btnNext.Enabled = True
        Else

            Me.btnNext.Enabled = False
        End If
    End Sub
    Private Sub DataFrames()
        Me.pnlLocation.Visible = False
        Me.pnlSelectLocation.Visible = False
        Me.pnlItems.Visible = False
        Me.pnlBatch.Visible = False
        Me.pnlDispatchDate.Visible = False
        Select Case Me.cFunction.ToUpper
            Case "SELECT LOCATION"
                BatchIssue.SetComboBoxLocation(Me.lstLocations)
                If BatchIssue.getDTOfOpenBatches.Rows.Count = 0 Then
                    Me.Label4.Visible = False
                    Me.btnViewListOfOpenBatches.Visible = False
                Else
                    Me.Label4.Visible = True
                    Me.btnViewListOfOpenBatches.Visible = True
                End If
                Me.pnlSelectLocation.Visible = True
                'Me.pnlSelectLocation.Location = New Drawing.Point(3, 106)
                
                Me.btnNext.Text = "NEXT"
                Me.btnNext.Enabled = False
            Case "SELECT ITEMS"
                Me.lblLocation.Text = "Customer: " & BatchIssue.LocationID

                Me.pnlLocation.Visible = True
                'Me.pnlSelectLocation.Location = New Drawing.Point(3, 106)
                Me.pnlItems.Visible = True

                
                Me.btnNext.Text = "Issue Item(s)"
                Me.btnNext.Enabled = False
                Me.lstItems.Items.Clear()
                If BatchIssue.TMPCBNUMBER > 0 Then
                    'We need to reload a previous batch
                    Dim auid As String
                    For Each auid In BatchIssue.lstAUID
                        Me.lstItems.Items.Add(GetListBoxString(auid))
                    Next
                End If
                If Me.lstItems.Items.Count > 0 Then
                    Me.btnNext.Enabled = True
                End If
                Me.txtAuid.Focus()
            Case "SELECT DATE"
                Me.pnlDispatchDate.Visible = True
                Me.DateTimePicker1.Value = Date.Now

            Case "SELECT BATCH"

                lstBatch.Items.Clear()


                Dim dt As Data.DataTable = BatchIssue.getDTOfOpenBatches
                Dim r As Data.DataRow
                For Each r In dt.Rows
                    Dim listViewItem1 = New ListViewItem(r.Item("TMPCBNUMBER").ToString)
                    Dim listViewSubItem1 = New ListViewItem.ListViewSubItem()
                    Dim listViewSubItem2 = New ListViewItem.ListViewSubItem()


                    listViewSubItem1.Text = r.Item("LocationID").ToString
                    listViewSubItem2.Text = r.Item("LastModified").ToString

                    listViewItem1.SubItems.Add(listViewSubItem1)
                    listViewItem1.SubItems.Add(listViewSubItem2)
                    lstBatch.Items.Add(listViewItem1)


                Next
                Me.pnlBatch.Visible = True

                Me.btnNext.Text = "NEXT"
                Me.btnNext.Enabled = False
        End Select
    End Sub

    Private Sub txtAuid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAuid.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim strMessage As String = ""
            If BatchIssue.CanAuidBeIssued(Me.txtAuid.Text.Trim(Chr(13)), strMessage) = False Then
                MessageBox.Show(strMessage)
                Me.txtAuid.Text = ""
                Exit Sub
            End If
            If IssueTest(Me.txtAuid.Text.Trim(Chr(13))) = False Then
                MessageBox.Show("Container has failed the issue test and cannot be issued")
                Me.txtAuid.Text = ""
                Exit Sub
            End If
            BatchIssue.AddItem(Me.txtAuid.Text.Trim(Chr(13)))

            Me.lstItems.Items.Add(GetListBoxString(Me.txtAuid.Text.Trim(Chr(13))))
            Me.txtAuid.Text = ""

            Me.btnNext.Enabled = True
        End If
    End Sub
    Private Function IssueTest(ByVal AUID As String) As Boolean
        Dim objTest As New clsTest
        Dim strTestsetid As String = objTest.GetTestSetID(AUID)
        strInspectionNo = objTest.GetIssueInspectionNo(strTestsetid)
        dtQuestions = GetQuestionsForEquipmentType(strTestsetid, strInspectionNo)
        If dtQuestions.Rows.Count > 0 Then

            Return AskQuestions()
            Exit Function
        End If
        Return True
    End Function
    Private Function GetQuestionsForEquipmentType(ByVal TestsetID As String, ByVal InspectionNo As Integer) As Data.DataTable

        Dim objTest As New clsTest


        objQuestionSet = New clsQuestionSet


        'strTestSetID = objTest.GetTestSetID(txtAuid.Text)
        'strInspectionNo = objTest.GetAfterUseInspectionNo(strTestSetID)

        Return objQuestionSet.GetQuestionSetWithMultipleInspections(TestsetID, InspectionNo, "Q01")



    End Function
    Private Sub txtAuid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAuid.TextChanged
        
    End Sub
    Private Function GetListBoxString(ByVal auid) As String
        Dim dh As New DataHandling
        Dim dt As Data.DataTable = dh.GetDataTable("Select AUID + ': ' + Category2 from tblitem I join tblCategory C on I.CategoryID = C.CategoryID where I.AUID = '" & auid & "'")
        Return dt.Rows(0)(0)
    End Function

    Private Sub frmBatchIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cFunction = "Select Location"
        DataFrames()

    End Sub

    Private Sub cmdAbandon_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        cReturnValue = "Abandon"
        Me.Close()
    End Sub
    Private Function AskQuestions() As Boolean
        Me.txtTestLog.Visible = True
        QuestionIndex = 0
        bTestResult = True

        strQuestionAnswer = ""
        strTestLog = "Dispatch and Decontam" & vbCrLf & _
                     "--------------" & vbCrLf & _
                     "Inspector: " & strAuditor
        Dim defaultButton As System.Windows.Forms.MessageBoxDefaultButton
AskQuestion:
        Dim dr As Data.DataRow = dtQuestions.Rows(QuestionIndex)

        Select Case dr.Item("TYPE").ToString.ToUpper
            Case "YES/NO", "PASS/FAIL"
                Select Case dr.Item("NegativeAnswer").ToString.ToUpper
                    Case "YES", "PASS"
                        defaultButton = MessageBoxDefaultButton.Button2
                    Case "NO", "FAIL"
                        defaultButton = MessageBoxDefaultButton.Button1
                End Select
                Dim dialogResult As DialogResult
                dialogResult = MessageBox.Show(dr.Item("Description"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton)
                If dialogResult = Windows.Forms.DialogResult.Yes Then
                    Select Case dr.Item("NegativeAnswer").ToString.ToUpper
                        Case "YES", "PASS"
                            'Fail Question
                            HandleFailQuestion(dr)
                        Case "NO", "FAIL"
                            'Pass Question
                            HandlePassQuestion(dr)
                    End Select
                End If
                If dialogResult = Windows.Forms.DialogResult.No Then
                    Select Case dr.Item("NegativeAnswer").ToString.ToUpper
                        Case "YES", "PASS"
                            'Pass question
                            HandlePassQuestion(dr)
                        Case "NO", "FAIL"
                            'Fail Question
                            HandleFailQuestion(dr)
                    End Select
                End If
            Case "NUMERIC", "TEXT"
                Dim strResponse As String = ""
                Dim ValidResponse As Boolean = False
                While ValidResponse = False
                    Dim InputBox As New frmDialog(dr.Item("Description"))
                    If InputBox.ShowDialog = Windows.Forms.DialogResult.OK Then
                        strResponse = InputBox.Text
                    End If
                    'strResponse = InputBox(dr.Item("Description"), "Container System")
                    If strResponse = "" Then
                        MessageBox.Show("You must enter a value")
                    End If
                    If dr.Item("Type").ToString.ToUpper = "NUMERIC" Then
                        If IsNumeric(strResponse) Then
                            ValidResponse = True
                        Else
                            MessageBox.Show("You must enter a numeric value")
                        End If
                    Else
                        ValidResponse = True
                    End If
                End While
                Select Case dr.Item("Type").ToString.ToUpper
                    Case "TEXT"
                        strTestLog = strTestLog & vbCrLf & dr.Item("Description") & " " & strResponse
                        Me.txtTestLog.Text = strTestLog
                        QuestionIndex = QuestionIndex + 1
                    Case "NUMERIC"
                        Select Case NumericResult(strResponse, dr.Item("NegativeAnswer"))
                            Case True
                                strTestLog = strTestLog & vbCrLf & dr.Item("Description") & " " & strResponse

                                Me.txtTestLog.Text = strTestLog
                                QuestionIndex = QuestionIndex + 1

                            Case False
                                HandleFailQuestion(dr, strResponse)
                        End Select
                End Select
        End Select

        Dim objStatus As New clsStatus
        If bTestResult = False Then
            'HandleSaveFail

            Dim objtest As New clsTest
            Call objtest.EnterNonMaintenanceTest(txtAuid.Text, strAuditor, CLng(strInspectionNo), "", _
                                             "", "", strTestLog, False, m_UserID)
            Call objStatus.SetItemStatus(txtAuid.Text, 1)
            strProcess = "ITEM"
            'Me.txtAuid.Text = ""
            Me.txtTestLog.Text = ""
            Me.txtTestLog.Visible = False
            Return False

        End If
        If ((QuestionIndex + 1) > dtQuestions.Rows.Count) Or (dr.Item("IFPOSITIVEDOFUNCTION").ToString = "PASSTESTWITHMOVE") Or (dr.Item("IFPOSITIVEDOFUNCTION").ToString = "PASSTEST") Then
            'No more questions must have passed
            objStatus.SetItemStatus(txtAuid.Text, 4)
            'Enter Successful Test
            Dim objtest As New clsTest
            Call objtest.EnterNonMaintenanceTest(txtAuid.Text, strAuditor, CLng(strInspectionNo), "", _
                                                 "", "", strTestLog, True, m_UserID)
            objtest = Nothing
            strProcess = "ITEM"
            'Me.txtAuid.Text = ""
            Me.txtTestLog.Text = ""
            Me.txtTestLog.Visible = False
            Return True
        End If
        GoTo AskQuestion



    End Function
    Sub HandleFailQuestion(ByVal dr As Data.DataRow)
        strTestLog = strTestLog & vbCrLf & dr.Item("Description") & " " & dr.Item("NEGATIVEANSWER")
        If dr.Item("IfNegativeFailTest") = -1 Then
            strTestLog = strTestLog & vbCrLf & "------------------------" & vbCrLf & "TEST FAILED"
            Me.txtTestLog.Text = strTestLog
            bTestResult = False
            Exit Sub
        End If
        If Not IsDBNull(dr.Item("IfNegativeGoTo")) Then
            QuestionIndex = dr.Item("IfNegativeGoTo") - 1
            Me.txtTestLog.Text = strTestLog
            Exit Sub
        End If
        Me.txtTestLog.Text = strTestLog
        QuestionIndex = QuestionIndex + 1
    End Sub
    Sub HandleFailQuestion(ByVal dr As Data.DataRow, ByVal strResponse As String)
        strTestLog = strTestLog & vbCrLf & dr.Item("Description") & " " & strResponse
        If dr.Item("IfNegativeFailTest") = -1 Then
            strTestLog = strTestLog & vbCrLf & "------------------------" & vbCrLf & "TEST FAILED"
            Me.txtTestLog.Text = strTestLog
            bTestResult = False
            Exit Sub
        End If
        If Not IsDBNull(dr.Item("IfNegativeGoTo")) Then
            QuestionIndex = dr.Item("IfNegativeGoTo") - 1
            Me.txtTestLog.Text = strTestLog
            Exit Sub
        End If
        Me.txtTestLog.Text = strTestLog
        QuestionIndex = QuestionIndex + 1
    End Sub
    Sub HandlePassQuestion(ByVal dr As Data.DataRow)
        strTestLog = strTestLog & vbCrLf & dr.Item("Description") & " "
        Select Case dr.Item("NEGATIVEANSWER").ToString.ToUpper
            Case "YES"
                strTestLog = strTestLog & "NO"
            Case "NO"
                strTestLog = strTestLog & "YES"
            Case "PASS"
                strTestLog = strTestLog & "FAIL"
            Case "FAIL"
                strTestLog = strTestLog & "PASS"
        End Select
        Me.txtTestLog.Text = strTestLog
        QuestionIndex = QuestionIndex + 1
    End Sub
    Private Function NumericResult(ByVal dAnswer As Double, ByVal NegativeAnswer As String) As Boolean

        Dim strNegative() As String
        Dim fResultArray() As Boolean
        Dim fResult As Boolean 'True if test result equates to a negative answer

        Dim intCount As Integer
        Dim intX As Integer


        strNegative = Split(Trim(NegativeAnswer), " ")
        intCount = ((UBound(strNegative) - 1) / 3)

        'MsgBox UBound(strNegative)
        ReDim fResultArray(intCount)

        For intX = 0 To UBound(fResultArray)
            fResultArray(intX) = TestComparitor(strNegative(intX * 3), CDbl(strNegative((intX * 3) + 1)), dAnswer)
        Next

        If intCount >= 1 Then
            'MsgBox fResultArray(0) & vbCrLf & fResultArray(1) & vbCrLf & strNegative(2)
            Select Case UCase(strNegative(2))
                Case "AND"
                    fResult = fResultArray(0) And fResultArray(1)

                Case "OR"
                    fResult = fResultArray(0) Or fResultArray(1)

            End Select

        Else
            fResult = fResultArray(0)
        End If



        NumericResult = Not fResult


    End Function
    Private Function TestComparitor(ByVal strOperator As String, ByVal dNumber As Double, ByVal dAnswer As Double) As Boolean


        Select Case strOperator
            Case "="
                If dAnswer = dNumber Then
                    Return True
                Else
                    Return False
                End If

            Case ">"
                If dAnswer > dNumber Then
                    Return True
                Else
                    Return False
                End If

            Case ">="
                If dAnswer >= dNumber Then
                    Return True
                Else
                    Return False
                End If

            Case "<"
                If dAnswer < dNumber Then
                    Return True
                Else
                    Return False
                End If

            Case "<="
                If dAnswer <= dNumber Then
                    Return True
                Else
                    Return False
                End If

            Case "<>"
                If dAnswer <> dNumber Then
                    Return True
                Else
                    Return False
                End If

        End Select


    End Function

    

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Select Case cFunction.ToUpper
            Case "SELECT LOCATION"
                BatchIssue.LocationID = Me.lstLocations.SelectedValue
                cFunction = "Select Items"
                DataFrames()
            Case "SELECT DATE"
                cFunction = ""
                BatchIssue.SaveBatch(m_UserID, Me.DateTimePicker1.Value)
                Me.lstItems.Items.Clear()
                Me.lstLocations.SelectedIndex = 0
                cFunction = "Select Location"
                DataFrames()
            Case "SELECT ITEMS"
                cFunction = "SELECT DATE"
                
                DataFrames()
        End Select
    End Sub

    Private Sub btnAbandon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbandon.Click
        If (cFunction.ToUpper = "SELECT ITEMS" Or cFunction.ToUpper = "SELECT DATE") And BatchIssue.TMPCBNUMBER > 0 Then
            'If on the select item screen then given the opportunity to abandon batch if there is any items to save
            Dim dialogResult As DialogResult
            Dim defaultButton As System.Windows.Forms.MessageBoxDefaultButton = MessageBoxDefaultButton.Button1
            dialogResult = MessageBox.Show("Do you wish to save this batch for future issue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton)
            If dialogResult = Windows.Forms.DialogResult.No Then

                BatchIssue.AbandonBatch()
            End If
        End If
        cReturnValue = "Abandon"
        Me.Close()
    End Sub

    
    Private Sub btnViewListOfOpenBatches_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewListOfOpenBatches.Click
        cFunction = "SELECT BATCH"
        DataFrames()
    End Sub

    Private Sub lstBatch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBatch.SelectedIndexChanged
        'Me.btnNext.Enabled = True
        'BatchIssue = New BatchIssue(UserID, Me.lstBatch.s)
        If Me.lstBatch.SelectedIndices.Count <= 0 Then
            Exit Sub
        End If
        Dim selNdx = Me.lstBatch.SelectedIndices(0)
        If selNdx >= 0 Then
            'label3.Text = lstBatch.Items(selNdx).Text
            BatchIssue = New BatchIssue(m_UserID, lstBatch.Items(selNdx).Text)
            cFunction = "SELECT ITEMS"
            DataFrames()
        End If
    End Sub
End Class