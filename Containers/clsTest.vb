Public Class clsTest
    Private DH As New DataHandling
    Private objParts As clsParts2
    Private dtTmpTest As Data.DataTable   'Contains the recordset of the current Test
    Private dtTmpTestCertNo As Data.DataTable
    Private m_lngTmpServiceID As Long           'Contains the TmpServiceID of the currentTest
    ReadOnly Property TmpTestID() As Long
        Get
            Return m_lngTmpServiceID
        End Get
    End Property
    '###################################################################################################
    Public Function GetTestInfo(ByVal TestID As Long) As TestStructure
        '*********************************************************************
        'Returns the structure of a test given it's TestID
        'This should be moved to History as it is specific to Equipment as it stands at the moment
        '*********************************************************************

        Dim DT As Data.DataTable
        Dim Test As New TestStructure
        Dim strSQL As String
        'Dim objBondingLabel As clsReportStructure
        'Dim objFailureLabel As clsReportStructure



        strSQL = "SELECT "
        strSQL = strSQL & "tblItemTest.Auid, "
        strSQL = strSQL & "tblItemTest.TestSetID, "
        strSQL = strSQL & "tblItemTest.InspectionNo, "
        strSQL = strSQL & "tblItemTest.TestDate, "
        strSQL = strSQL & "tblItemTest.Result, "
        strSQL = strSQL & "tblTestSetDetail.TestName, "
        strSQL = strSQL & "tblTestSetDetail.Frequency, "
        strSQL = strSQL & "tblTestSetDetail.Bonding_Label "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblTestSetDetail, "
        strSQL = strSQL & "tblItemTest "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "(tblTestSetDetail.TestSetID = tblItemTest.TestSetID) "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "(tblTestSetDetail.InspectionNo = tblItemTest.InspectionNo) "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "tblItemTest.ServiceID=" & TestID & " "
        strSQL = strSQL & "ORDER BY "
        strSQL = strSQL & "tblItemTest.TestDate Desc "

        DT = DH.GetDataTable(strSQL)
        Dim DR As Data.DataRow
        DR = DT.Rows(0)

        Test.AUID = DR.Item("AUID")
        Test.InspectionNo = DR.Item("InspectionNo")
        Test.LastTestDate = Year(DR.Item("TestDate")) & "-" & Month(DR.Item("TestDate")) & "-" & DateAndTime.Day(DR.Item("TestDate"))
        Test.LastTestPass = DR.Item("Result")
        Test.TestSetID = DR.Item("Testsetid")
        Test.TestDescription = DR.Item("TestName")
        Test.Frequency = DR.Item("Frequency")
        ''''TEST.NextTestHasBondingLabel = dr.item("bonding_label
        'Test.BondingLabel = GetLabelDetail("Bonding", DR.Item("Testsetid"), DR.Item("InspectionNo"))
        'Test.FailureLabel = GetLabelDetail("Failure", DR.Item("Testsetid"), DR.Item("InspectionNo"))

        GetTestInfo = Test


    End Function
    Public Function GetTestSetID(ByVal strAUID As String) As String

        Dim strSQL As String
        Dim dh As New DataHandling
        Dim DT As Data.DataTable
        Dim strTestSetID As String

       
        strTestSetID = ""

        strSQL = "SELECT "
        strSQL = strSQL & "tblCategory.TestSetID "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblItem, "
        strSQL = strSQL & "tblCategory "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "AUID = '" & strAUID & "' "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "tblItem.CategoryID = tblCategory.CategoryID "

        DT = dh.GetDataTable(strSQL)

        If DT.Rows.Count > 0 Then
            strTestSetID = DT.Rows(0).Item("TestSetID")
        End If

        Return strTestSetID

       
       
    End Function
    Public Function GetAfterUseInspectionNo(ByVal strTestSetID As String) As String

        Dim strSQL As String
        Dim DH As New DataHandling
        Dim DT As Data.DataTable
        Dim strInspectionNo As String

        
        strInspectionNo = "0"

        strSQL = "SELECT "
        strSQL = strSQL & "InspectionNo "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblTestSetDetail "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "ReturnTests > 0 "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "TestsetID = '" & strTestSetID & "' "
        strSQL = strSQL & "ORDER BY "
        strSQL = strSQL & "InspectionNo Asc "

        DT = DH.GetDataTable(strSQL)

        If DT.Rows.Count > 0 Then
            strInspectionNo = CStr(DT.Rows(0).Item("InspectionNo"))
        End If

        GetAfterUseInspectionNo = strInspectionNo

       
        
    End Function
    Public Sub EnterNonMaintenanceTest(ByVal strAUID As String, ByVal strUserName As String, ByVal lngInspectionNo As Long, _
                                    ByVal strCertType As String, ByVal strCalibrationDate As String, _
                                    ByVal strCalibrationCertNo As String, ByVal strLog As String, ByVal bPassed As Boolean, ByVal UserID As Long)


        Call StartNewTest(GetTestSetID(strAUID), lngInspectionNo, strUserName, strAUID)
        'Call EnterTestNotes(strLog)
        Call TestPassed(bPassed)
        If strCertType <> "" Then
            Call AddTmpTestCertNo(m_lngTmpServiceID, strCertType, strCalibrationCertNo)
        End If
        Call AddTestToHistory(UserID, strLog)


    End Sub
    Public Function StartNewTest(ByVal Testsetid As String, ByVal InspectionNo As Integer, ByVal CurrentUserName As String, ByVal AUID As String) As Boolean
        '*********************************************************************
        'Returns true if a new TmpTest is created else returns false
        '**********************************************************************


        'Is there a test already being carried out
        If HasAUIDGotActiveTest(AUID) Then
            'To be here there must already be a tmpTest, no further action is required
            Return False
            Exit Function
        End If

        'Find next temp test ID
        m_lngTmpServiceID = FindTempNextTestID

        'Create temp test
        If CreateTempTest(Testsetid, InspectionNo, CurrentUserName, AUID) Then
            'The test has been created, function will return true
            Return True
        Else
            'The test has not been created, function will return false
            Return False
        End If


    End Function
    Public Function HasAUIDGotActiveTest(ByVal AUID) As Boolean
        '*********************************************************
        'If no active test is found then returns false
        'else returns true
        '**********************************************************

        Dim DT As Data.DataTable

        
        DT = DH.GetDataTable("select tbltmptest.tmpserviceid from tblTmpTest where AUID='" & AUID & "'")

        If DT.Rows.Count = 0 Then
            'No test was found in tblTmpTest
            Return False
        Else
            'There is already a test in tbltmptest
            m_lngTmpServiceID = DT.Rows(0).Item("tmpServiceID")
            dtTmpTest = DH.GetDataTable("select * from tblTmpTest where AUID='" & AUID & "'")

            Return True

        End If

        
    End Function

    Public Sub EnterTestNotes(ByVal strComments As String)
        '*********************************************************************
        'Updates Test notes
        'The notes run thoughout the testing process
        '*********************************************************************

        If dtTmpTest.Rows(0).Item("Notes") = "" Then
            'nothing previously entered
            DH.ExecuteNonQuery("Update tbltmptest set Notes = '" & strComments & "' where tmpserviceid = " & m_lngTmpServiceID)


        Else
            If Left(strComments, 5) = "NOTES" Then
                'Replace note comment with new notes
                DH.ExecuteNonQuery("Update tbltmptest set Notes = '" & strComments & "' where tmpserviceid = " & m_lngTmpServiceID)


            Else
                'Add Fault to end of notes section
                DH.ExecuteNonQuery("Update tbltmptest set Notes = Notes + '" & vbCrLf & strComments & "' where tmpserviceid = " & m_lngTmpServiceID)


            End If
            dtTmpTest = DH.GetDataTable("Select * from tbltmptest where tmpserviceid = " & m_lngTmpServiceID)

        End If
        
    End Sub
    Private Function FindTempNextTestID() As Long
        '*******************************************************************
        'Returns the next tmpTestID
        'Table tmpTest will have a test for all the AUID's currently undergoing maintenance
        'THe tmpServiceID is used to identify which test is being carried out
        '********************************************************************

        Dim DT As Data.DataTable

        
        DT = DH.GetDataTable("SELECT Max(tblTmpTest.TmpServiceID) AS MaxOfTmpServiceID FROM tblTmpTest;")

        If IsDBNull(DT.Rows(0).Item("maxOfTmpServiceID")) Then
            'To be here no temporary tests were found in tblTmpTest
            Return 1
        Else
            'There are already tests in the tblTmpTest so the new service ID will need to be calculated
            Return DT.Rows(0).Item("maxOfTmpServiceID") + 1
        End If

        
    End Function
    Private Function CreateTempTest(ByVal Testsetid As String, ByVal InspectionNo As Integer, ByVal CurrentUserName As String, ByVal AUID As String) As Boolean
        '*********************************************
        'This function creates a new Test in the temporary test table tblTmpTest
        'If the test is created succesfully then the function return true
        'If a test is already found then the function returns false
        '*********************************************

        
        dtTmpTest = DH.GetDataTable("select * from tblTmpTest where AUID='" & AUID & "'")

        If dtTmpTest.Rows.Count > 0 Then
            'To be here there must already be a tmpTest with the AUID selected
            Return False
        Else
            'OK to create a new tmp
            Dim StrSQL As String = "insert into tbltmptest(AUID,TMPSERVICEID,TESTSETID,INSPECTIONNO,AUDITOR,TESTDATE,NOTES) VALUES("
            StrSQL = StrSQL & "'" & AUID & "', "
            StrSQL = StrSQL & m_lngTmpServiceID & ", "
            StrSQL = StrSQL & "'" & Testsetid & "', "
            StrSQL = StrSQL & InspectionNo & ", "
            StrSQL = StrSQL & "'" & CurrentUserName & "', "
            StrSQL = StrSQL & "GetDate(), '')"


            DH.ExecuteNonQuery(StrSQL)
            dtTmpTest = DH.GetDataTable("select * from tblTmpTest where AUID='" & AUID & "'")

            Return True
        End If

        
    End Function
    Public Sub TestPassed(ByVal fPassed As Boolean)
        '*********************************************************************
        'Sets the pass or fail status of the test
        '-1 passed
        '0 failed
        '1 not complete
        '*********************************************************************

        DH.ExecuteNonQuery("Update tbltmptest set Result = " & IIf(fPassed, -1, 0) & " where tmpserviceid = " & m_lngTmpServiceID)
        dtTmpTest = DH.GetDataTable("select * from tblTmpTest where tmpserviceid = " & m_lngTmpServiceID)

    End Sub
    Public Sub AddTmpTestCertNo(ByVal lngServiceID As Long, ByVal strDescription As String, ByVal strValue As String)

        Dim strSQL As String
        Dim DT As Data.DataTable

        
        'If Type Of Cert already stored in temp table
        'Then overwrite else add record

        strSQL = "SELECT "
        strSQL = strSQL & "TmpServiceID "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "TblTmpTestCertNo "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "TmpServiceID = " & lngServiceID & " "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "Description = '" & strDescription & "' "


        DT = DH.GetDataTable(strSQL)

        If DT.Rows.Count > 0 Then

            strSQL = "UPDATE "
            strSQL = strSQL & "TblTmpTestCertNo "
            strSQL = strSQL & "SET "
            strSQL = strSQL & "Value = '" & strValue & "' "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "TmpServiceID = " & lngServiceID & " "
            strSQL = strSQL & "AND "
            strSQL = strSQL & "Description = '" & strDescription & "' "

            'Call objData.ExecuteSQL(strSql)
            DH.ExecuteNonQuery(strSQL)

        Else

            strSQL = "INSERT INTO "
            strSQL = strSQL & "TblTmpTestCertNo "
            strSQL = strSQL & "("
            strSQL = strSQL & "TmpServiceID, "
            strSQL = strSQL & "Description, "
            strSQL = strSQL & "Value "
            strSQL = strSQL & ") "
            strSQL = strSQL & "Values("
            strSQL = strSQL & "" & lngServiceID & ", "
            strSQL = strSQL & "'" & strDescription & "', "
            strSQL = strSQL & "'" & strValue & "' "
            strSQL = strSQL & ") "

            'Call objData.ExecuteSQL(strSql)
            DH.ExecuteNonQuery(strSQL)

        End If

        
        
    End Sub
    Public Function AddTestToHistory(ByVal UserID As Long, ByVal strTestDetail As String) As Boolean
        '*********************************************************************
        'This function adds the temporary test to the main tblTest
        'The test is now part of the history
        'To add the test a TestID is found to replace the TmpServiceID used while testing
        'The three tables
        'tblTmpTest,tblTmpQuestionSet and tbltmpQuestionSetLookups

        'all have their related records removed
        '*********************************************************************

        Dim strResult As String = ""
        Dim strNotes As String = ""
        Dim strPartsReplaced As String
        Dim lngTestID As Long
        Dim objTestSchedule As clsTestSchedule





        'strResult = GetTestResultsSoFar()
        'strNotes = GetNotesFromQuestionSets()


        strPartsReplaced = ""

        lngTestID = FindNextID()

        Dim strSQL As String = "INSERT INTO TBLITEMTEST(SERVICEID,AUID,TESTSETID,TESTDATE,AUDITOR,DETAIL,NOTES,PARTSREPLACED,RESULT,INSPECTIONNO) VALUES("
        strSQL = strSQL & lngTestID & ", "
        strSQL = strSQL & "'" & dtTmpTest.Rows(0).Item("AUID") & "', "
        strSQL = strSQL & "'" & dtTmpTest.Rows(0).Item("TESTSETID") & "', "
        strSQL = strSQL & "DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), "
        Dim usr As New clsUser
        strSQL = strSQL & "'" & usr.GetUserFullName(UserID) & "', "
        strSQL = strSQL & "'" & Trim(strTestDetail) & "', "
        strSQL = strSQL & "'" & Trim(strNotes) & "','', "
        If IsDBNull(dtTmpTest.Rows(0).Item("Result")) Then
            strSQL = strSQL & "-1, "
        Else
            strSQL = strSQL & CInt(dtTmpTest.Rows(0).Item("Result")) & ", "

        End If
        strSQL = strSQL & dtTmpTest.Rows(0).Item("InspectionNo") & ") "
        'MsgBox rstNew.Supports(adAddNew)
        DH.ExecuteNonQuery(strSQL)
        'Add Any Cert Nos
        Call AddTestCertNo(m_lngTmpServiceID, lngTestID)

        RemoveTmpTestDetails(Trim(dtTmpTest.Rows(0).Item("AUID")))


        objTestSchedule = New clsTestSchedule
        objTestSchedule.UpdateTestSchedule(lngTestID)
        Return True


    End Function
    Public Function GetTestResultsSoFar() As String
        '*********************************************************************
        'This will return a string with all the results of completed questionSets
        '*********************************************************************

        Dim strResult As String = ""
        Dim dt As Data.DataTable

       
        dt = DH.GetDataTable("Select * from tblTmpQuestionSet where tmpServiceID=" & m_lngTmpServiceID)
        Dim dr As Data.DataRow
        For Each dr In dt.Rows
            strResult = strResult & dr.Item("Result")
        Next
       
        Return strResult


    End Function
    Public Function GetNotesFromQuestionSets() As String

        Dim strNotes As String = ""
        Dim DR As Data.DataRow
        For Each DR In dtTmpTest.Rows
            strNotes = strNotes & dr.Item("Notes")
        Next
        Return strNotes


    End Function
    Private Function FindNextID() As Long
        '*******************************************************************
        'This allocates a serviceID to be used when saving the test to
        'History
        '*******************************************************************

        Dim DT As Data.DataTable

        
        DT = DH.GetDataTable("SELECT Max(tblItemTest.ServiceID) AS MaxOfServiceID FROM tblItemTest;")

        If IsDBNull(DT.Rows(0).Item("maxOfServiceID")) Then
            'No tests in History
            Return 1
        Else
            'There are already Tests in the history, just need to add 1
            Return CLng(DT.Rows(0).Item("maxOfServiceID")) + 1
        End If

        
    End Function
    Public Sub AddTestCertNo(ByVal lngTmpServiceID As Long, ByVal lngServiceID As Long)

        Dim strSQL As String
        Dim DT As Data.DataTable

        
        strSQL = "SELECT "
        strSQL = strSQL & "TmpServiceID, "
        strSQL = strSQL & "Description, "
        strSQL = strSQL & "Value "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "TblTmpTestCertNo "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "TmpServiceID = " & lngTmpServiceID & " "

        DT = DH.GetDataTable(strSQL)

        If DT.Rows.Count > 0 Then
            Dim Dr As Data.DataRow
            For Each Dr In DT.Rows
                strSQL = "INSERT INTO "
                strSQL = strSQL & "TblTestCertNo "
                strSQL = strSQL & "("
                strSQL = strSQL & "ServiceID, "
                strSQL = strSQL & "Description, "
                strSQL = strSQL & "Value "
                strSQL = strSQL & ") "
                strSQL = strSQL & "Values("
                strSQL = strSQL & "" & lngServiceID & ", "
                strSQL = strSQL & "'" & Dr.Item("Description") & "', "
                strSQL = strSQL & "'" & Dr.Item("Value") & "' "
                strSQL = strSQL & ") "

                'Call objData.ExecuteSQL(strSql)
                DH.ExecuteNonQuery(strSQL)

            Next

           
        End If

        Call DeleteTmpTestCertNo(lngTmpServiceID)

        
    End Sub
    Public Sub DeleteTmpTestCertNo(ByVal lngServiceID As Long)

        Dim strSQL As String

        
        strSQL = "DELETE FROM tblTmpTestCertNo WHERE TmpServiceID = " & lngServiceID & " "

        'Call objData.ExecuteSQL(strSql)
        DH.ExecuteNonQuery(strSQL)

        Exit Sub

    End Sub
    Public Sub RemoveTmpTestDetails(ByVal strAUID As String)

        Dim dt As Data.DataTable
        Dim lngTmpServiceID As Long

        'Set rst = objData.Connection.Execute("Select TmpServiceID from tbltmpTest where AUID='" & strAUID & "'")
        dt = DH.GetDataTable("Select TmpServiceID from tbltmpTest where AUID='" & strAUID & "'")

        If dt.Rows.Count > 0 Then
            lngTmpServiceID = dt.Rows(0).Item("tmpserviceid")
            'objData.Connection.Execute ("Delete from tbltmpQuestionSetLookups where tmpserviceid='" & lngTmpServiceID & "'")
            'objData.ExecuteSQL ("Delete from tbltmpQuestionSetLookups where tmpserviceid='" & lngTmpServiceID & "'")
            DH.ExecuteNonQuery("Delete from tbltmpQuestionSetLookups where tmpserviceid='" & lngTmpServiceID & "'")

            'objData.Connection.Execute ("Delete from tbltmpquestionset where tmpserviceid=" & lngTmpServiceID)
            'objData.ExecuteSQL ("Delete from tbltmpquestionset where tmpserviceid=" & lngTmpServiceID)
            DH.ExecuteNonQuery("Delete from tbltmpquestionset where tmpserviceid=" & lngTmpServiceID)
            'objData.Connection.Execute ("Delete from tbltmpTest where tmpserviceid=" & lngTmpServiceID)
            'objData.ExecuteSQL ("Delete from tbltmpTest where tmpserviceid=" & lngTmpServiceID)
            DH.ExecuteNonQuery("Delete from tbltmpTest where tmpserviceid=" & lngTmpServiceID)
            'objData.Connection.Execute ("Delete from tbltmppartsreplaced where auid = '" & strAUID & "' ")
            'objData.ExecuteSQL ("Delete from tbltmppartsreplaced where auid = '" & strAUID & "' ")
            DH.ExecuteNonQuery("Delete from tbltmppartsreplaced where auid = '" & strAUID & "' ")
            'objData.ExecuteSQL ("Delete from tbltmpReturn where auid = '" & strAUID & "' ")
        End If

    End Sub
    Public Function GetIssueInspectionNo(ByVal strTestSetID As String) As String

        Dim strSQL As String
        Dim dt As System.Data.DataTable
        Dim strInspectionNo As String

        

        strInspectionNo = "0"

        strSQL = "SELECT "
        strSQL = strSQL & "InspectionNo "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblTestSetDetail "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "IssueTests IS NOT NULL "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "TestSetID = '" & strTestSetID & "' "
        strSQL = strSQL & "ORDER BY "
        strSQL = strSQL & "InspectionNo Asc "

        dt = DH.GetDataTable(strSQL)

        If dt.Rows.Count > 0 Then
            strInspectionNo = CStr(dt.Rows(0).Item("InspectionNo"))
        End If

        Return strInspectionNo

        
        
    End Function

End Class
