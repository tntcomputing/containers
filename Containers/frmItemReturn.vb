Public Class frmItemReturn
    Dim MaintenanceLocation As String
    Dim m_PersonnelID As String = ""
    Dim m_Forename As String = ""
    Dim m_Surname As String = ""
    Dim cReturnValue As String = ""
    Dim strProcess As String = ""
    Dim m_UserID As Int32
    Dim objQuestionSet As clsQuestionSet
    Dim objNextTestDue As TestStructure
    Dim strTestSetID As String
    Dim strInspectionNo As String
    Dim dtQuestions As Data.DataTable
    Dim QuestionIndex As Integer
    Dim bTestResult As Boolean
    Dim strQuestionAnswer As String
    Dim strTestLog As String
    Dim strAuditor As String
    Public BatchReturn As BatchReturn

    Public Sub New(ByVal UserID As Int32)
        InitializeComponent()
        m_UserID = UserID
        Dim objUser As New clsUser
        strAuditor = objUser.GetUserFullName(UserID)
        BatchReturn = New BatchReturn()
    End Sub
    Private Sub SetupOpenBatchList(ByVal DT As System.Data.DataTable)
        Me.txtTestLog.Visible = False
        Me.lstBatch.Visible = True
        Me.lstItems.Items.Clear()
        Me.lblFunction.Text = "Open Return Batches"
        lstBatch.Items.Clear()
        Dim r As Data.DataRow
        For Each r In DT.Rows
            Dim listViewItem1 = New ListViewItem(r.Item("TMPRCBNUMBER").ToString)
            Dim listViewSubItem1 = New ListViewItem.ListViewSubItem()
            Dim listViewSubItem2 = New ListViewItem.ListViewSubItem()


            listViewSubItem1.Text = r.Item("LocationID").ToString
            listViewSubItem2.Text = r.Item("LastModified").ToString

            listViewItem1.SubItems.Add(listViewSubItem1)
            listViewItem1.SubItems.Add(listViewSubItem2)
            lstBatch.Items.Add(listViewItem1)
            Me.btnSaveReturnBatch.Visible = False
            Me.lstItems.Visible = False


        Next
    End Sub
    Private Sub SetupScanFrameNoOpenBatches()
        Me.txtTestLog.Visible = True
        Me.lstBatch.Visible = False
        Me.lstBatch.Items.Clear()
        Me.lstItems.Items.Clear()
        Me.lstItems.Visible = False
        Me.btnSaveReturnBatch.Visible = False
        'Me.lblFunction.Text = "Return Questions"
        Me.lblFunction.Text = "Items in current return batch"
    End Sub
    Private Sub frmItemReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objEnv As New clsEnvironment
        MaintenanceLocation = objEnv.ReturnEnvironmentSetting("SETUP", "MAINTLOCID", 1)
        Dim DT As System.Data.DataTable = BatchReturn.getDTOfOpenReturnBatches
        If DT.Rows.Count > 0 Then
            'If existing show list of open return batches
            'else
            SetupOpenBatchList(DT)
        Else
            SetupScanFrameNoOpenBatches()
        End If
        strProcess = "ITEM"
        Me.txtAuid.Focus()

    End Sub

    

    Private Sub mnuBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cReturnValue = "Abandon"
        Me.Close()
    End Sub

    Private Sub LoadBatchList(ByVal LocationID As String)
        BatchReturn.LoadTMPReturnBatch(LocationID)
        If BatchReturn.LocationID <> "" Then
            'Existing Batch
            'Load list
            Me.lstItems.Items.Clear()
            Dim strAuid As String
            For Each strAuid In BatchReturn.lstAUID
                Me.lstItems.Items.Add(GetListBoxString(strAuid))
            Next

        End If
        Me.SetupScanFrameOpenBatch(Me.lstItems.Items.Count)
    End Sub
    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim strMessage As String = ""
        Dim AUID As String = Me.txtAuid.Text.Trim(Chr(13))
        Select Case strProcess.ToUpper
            Case "ITEM"
                If DoesAUIDExist(AUID, strMessage) = False Then
                    MessageBox.Show(strMessage)
                    Me.txtAuid.Text = ""
                    Exit Sub
                End If
                If CheckAUID(AUID, strMessage) = False Then
                    MessageBox.Show(strMessage)
                    Me.txtAuid.Text = ""
                    Exit Sub
                End If
                'To be here item exists, isn't scrapped or bonded and is out on issue
                Dim DH As New DataHandling
                Dim dtItem As Data.DataTable

                dtItem = DH.GetDataTable("Select * from tblitem where auid='" & AUID & "'")

                Dim objMove As New clsMove(m_UserID)
                objMove.IssueMove = "Move"
                objMove.ReturnFlag = dtItem.Rows(0).Item("ReturnFlag")
                'Get the orderNo and current location(customer)
                Dim Locationid As String = dtItem.Rows(0).Item("CurrentLocation")
                Dim orderno As String = DH.ExecuteStrScalar("Select orderNo from tblissue_move where datereturned is null and itemid = '" & AUID & "'")
                If Locationid <> BatchReturn.LocationID Then
                    'To be here this is a new batch 
                    If BatchReturn.LocationID <> "" Then
                        'To be here we have an existing batch
                        'do we need to save existing batch
                        Dim dialogResult As DialogResult
                        Dim defaultButton As System.Windows.Forms.MessageBoxDefaultButton = MessageBoxDefaultButton.Button1
                        dialogResult = MessageBox.Show("Do you wish to save return batch for Customer: " & BatchReturn.LocationID & " before returning this container belonging to " & Locationid & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton)
                        If dialogResult = Windows.Forms.DialogResult.Yes Then

                            BatchReturn.SaveBatch(m_UserID, Date.Now)
                            Me.lstItems.Items.Clear()
                            BatchReturn = New BatchReturn
                        Else
                            'Blank batch
                            Me.lstItems.Items.Clear()
                            BatchReturn = New BatchReturn
                        End If
                    End If
                    'Does batch exit if so load it
                    LoadBatchList(Locationid)
                Else

                End If

                'If currentLocation <> locationid then
                'If open batch
                'if message(savebatch)
                'save batch
                'else
                'Close batch
                'end if
                'If currentlocation already on batch
                'Open batch
                'else
                'Create new batch
                'endif

                objMove.ItemReturnToWSLoc(dtItem)
                GetInspectionType(AUID)
                IsReturnsProcedureNeeded(AUID)
                GetInspectionType(AUID)

                dtQuestions = GetQuestionsForEquipmentType(objNextTestDue.TestSetID, objNextTestDue.InspectionNo)
                If dtQuestions.Rows.Count > 0 Then

                    AskQuestions(Locationid, orderno)
                End If


        End Select
    End Sub
    
    Private Sub IsReturnsProcedureNeeded(ByVal AUID As String)
        '--------------------------------------------
        'If the last test was passed then
        '   If the respirator bag is not sealed then
        '       Set Return Flag
        '       if the item is on isse then
        '           Move to workstation location
        '       end if
        '    End If
        'End if
        '--------------------------------------------



        Dim objTest As New clsTest
        Dim fCheckForAfterUse



        If Not objTest.HasAUIDGotActiveTest(AUID) Then
            'There is no active test so need to find out if a returns procedure is needed.
            If HasAUIDGotHistory(AUID) Then
                'Item has history so need to check whether it passed its last test.
                'If it has failed the test then we can assume
                'It has not been issued
                'It is not bagged.
                fCheckForAfterUse = objNextTestDue.LastTestPass
            Else
                'No History but may of been issued so need to check if it has been used
                fCheckForAfterUse = objNextTestDue.LastTestPass
                'fCheckForAfterUse = True
            End If

            If fCheckForAfterUse Then

                'THe last recorded test was a pass so we need to see if
                'An after use test is required.
                Dim objMove As New clsMove(m_UserID)
                objMove.IssueMove = "Move"
                'The following function establishes whether the respirator needs an afteruse test.
                'If yes then the return flag will be set

                objMove.setReturnFlag(AUID, objNextTestDue.OverDue)

                If objMove.IsItemOnIssue(AUID) Then
                    'Item is on issue so need to retun it to maintenance
                    Dim DH As New DataHandling
                    Dim dtitem As Data.DataTable = DH.GetDataTable("Select * from tblitem where auid='" & AUID & "")
                    objMove.ItemReturnToWSLoc(dtitem)

                End If

            End If

        End If




    End Sub
    
    Private Function HasAUIDGotHistory(ByVal AUID As String) As Boolean
        '--------------------------------------------
        'Checks to see if any inspections have been carried out on item
        '--------------------------------------------


        
        Dim objHistory As New clsHistory

        Return objHistory.HasHistory(AUID)
        
        
    End Function

    Sub GetInspectionType(ByVal AUID As String)
        'This sub establishes either what the next test should be or
        'what is the current active test
        'Displays Inspection Type in textbox




        Dim objTestSchedule As New clsTestSchedule

        objTestSchedule = New clsTestSchedule
        objNextTestDue = objTestSchedule.NextTestDue(AUID)
        strInspectionNo = objNextTestDue.InspectionNo
        ''''fNeedBondingLabel = objNextTestDue.NextTestHasBondingLabel
        
        
    End Sub

    Private Function GetQuestionsForEquipmentType(ByVal TestsetID As String, ByVal InspectionNo As Integer) As Data.DataTable

        Dim objTest As New clsTest


        objQuestionSet = New clsQuestionSet


        'strTestSetID = objTest.GetTestSetID(txtAuid.Text)
        'strInspectionNo = objTest.GetAfterUseInspectionNo(strTestSetID)

        Return objQuestionSet.GetQuestionSetWithMultipleInspections(TestsetID, InspectionNo, "Q01")



    End Function
    Private Function DoesAUIDExist(ByVal auid As String, ByRef strMessage As String) As Boolean
        Dim DH As New DataHandling
        Dim dtItem As Data.DataTable

        dtItem = DH.GetDataTable("Select count(*) from tblitem where auid='" & auid & "'")

        If dtItem.Rows(0).Item(0) = 0 Then

            'ShowMessage "OK", Scan1.Value & " does not exist"
            strMessage = auid & " does not exist"
            Return False
            Exit Function

        End If
        Return True
    End Function
    Private Function CheckAUID(ByVal AUID As String, ByRef strMessage As String) As Boolean

        
        Dim DH As New DataHandling
        Dim dtItem As Data.DataTable

        dtItem = DH.GetDataTable("Select * from tblitem where auid='" & AUID & "'")
        If dtItem.Rows(0).Item("Scrapped") = -1 Then
            strMessage = AUID & " cannot be returned as it has been Scapped or Bonded"
            Return False
            Exit Function
        End If
        Dim objMove As New clsMove(m_UserID)
        If objMove.IsItemOnIssue(AUID) = False Then
            strMessage = AUID & " Item is not issued, no return test required"
            Return False
            Exit Function
        End If
        Return True

        
    End Function
    Private Sub AskQuestions(ByVal LocationID As String, ByVal OrderNo As String)
        Me.lblFunction.Text = "Return Questions"
        Me.lstItems.Visible = False
        Me.btnSaveReturnBatch.Visible = False
        QuestionIndex = 0
        bTestResult = True

        strQuestionAnswer = ""
        strTestLog = "Return Test" & vbCrLf & _
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
            BatchReturn.AddItem(txtAuid.Text, LocationID)
            Me.txtAuid.Text = ""
            SetupScanFrameOpenBatch(True)
            Exit Sub

        End If
        If ((QuestionIndex + 1) > dtQuestions.Rows.Count) Or (dr.Item("IFPOSITIVEDOFUNCTION").ToString = "PASSTESTWITHMOVE") Or (dr.Item("IFPOSITIVEDOFUNCTION").ToString = "PASSTEST") Then
            'No more questions must have passed
            objStatus.SetItemStatus(txtAuid.Text, 4)
            'Enter Successful Test
            Dim objtest As New clsTest
            Call objtest.EnterNonMaintenanceTest(txtAuid.Text, strAuditor, CLng(strInspectionNo), "", _
                                                 "", "", strTestLog, True, m_UserID)
            BatchReturn.AddItem(txtAuid.Text, LocationID)
            Me.lstItems.Items.Add(GetListBoxString(txtAuid.Text))
            objtest = Nothing
            Me.txtAuid.Text = ""
            SetupScanFrameOpenBatch(True)
            Exit Sub
        End If
        GoTo AskQuestion



    End Sub
    Private Sub SetupScanFrameOpenBatch(ByVal btnVisible As Boolean)
        strProcess = "ITEM"
        ' Me.txtAuid.Text = ""
        Me.txtTestLog.Text = ""
        Me.lstItems.Visible = True
        Me.lstBatch.Visible = False
        Me.btnSaveReturnBatch.Visible = btnVisible
    End Sub
    Private Function GetListBoxString(ByVal auid) As String
        Dim dh As New DataHandling
        Dim dt As Data.DataTable = dh.GetDataTable("Select AUID + ': ' + Category2 from tblitem I join tblCategory C on I.CategoryID = C.CategoryID where I.AUID = '" & auid & "'")
        Return dt.Rows(0)(0)
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
    
    Private Sub txtAuid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAuid.KeyPress
        If e.KeyChar = Chr(13) Then
            btnContinue_Click(sender, e)
        End If
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

    

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        cReturnValue = "Abandon"
        Me.Close()
    End Sub

    
    Private Sub btnSaveReturnBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveReturnBatch.Click
        BatchReturn.SaveBatch(m_UserID, Now())
        Me.lstItems.Items.Clear()
        BatchReturn = New BatchReturn
        Dim DT As System.Data.DataTable = BatchReturn.getDTOfOpenReturnBatches
        If DT.Rows.Count > 0 Then
            'If existing show list of open return batches
            'else
            SetupOpenBatchList(DT)
        Else
            SetupScanFrameNoOpenBatches()
        End If

    End Sub

    Private Sub lstBatch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBatch.SelectedIndexChanged
        If Me.lstBatch.SelectedIndices.Count <= 0 Then
            Exit Sub
        End If
        Dim selNdx = Me.lstBatch.SelectedIndices(0)
        If selNdx >= 0 Then
            'label3.Text = lstBatch.Items(selNdx).Text
            BatchReturn = New BatchReturn(lstBatch.Items(selNdx).Text)
            LoadBatchList(Me.lstBatch.Items(selNdx).SubItems(0).Text)
            
        End If
    End Sub
End Class