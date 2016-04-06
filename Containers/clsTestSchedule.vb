Public Class clsTestSchedule
    Public Function OKToIssue(ByVal AUID As String, ByVal lngNoOfDays As Long) As Boolean

        Dim Test As TestStructure
       
        

        Test = Me.NextTestDue(AUID)

        If IsTestInHeirarchy(Test.InspectionNo, Trim(Test.Testsetid)) Or Me.IsItemReturnFlagSet(AUID) = False Then
            If Test.LastTestPass Then

                If Test.Overdue = False Then
                    If lngNoOfDays = 0 Then
                        OKToIssue = True
                    Else
                        If DateDiff("d", Date.Now, Test.NextTestDate) <= lngNoOfDays Then
                            OKToIssue = False
                        Else
                            OKToIssue = True
                        End If
                    End If
                    'MsgBox "OK to issue"
                Else
                    OKToIssue = False
                    'MsgBox "Not OK to Issue" & vbCrLf & "Test Overdue"

                End If

            Else
                If IsTestAnIssueTest(Test.InspectionNo, Trim(Test.Testsetid)) Then
                    OKToIssue = False
                Else
                    OKToIssue = True
                End If

            End If
            'Set DT = Me.ViewTestSchedule
            'DT.Filter "[Heirarchy]=-1"
            '
        Else
            If Me.IsItemReturnFlagSet(AUID) Then
                OKToIssue = False
            Else
                OKToIssue = True
            End If

            'MsgBox "Not OK to Issue" & vbCrLf & "next test due not in heirarchy"

        End If

        'If MsgBox("IS it OK to issue?", vbYesNo) = vbYes Then
        '    OKToIssue = True
        'Else
        '    OKToIssue = False
        'End If

        

    End Function
    Public Function NextTestDue(ByVal strAUID As String) As TestStructure
        'Returns the Next Test Due

        Dim DH As New DataHandling
        Dim cmd As Data.SqlClient.SqlCommand = New Data.SqlClient.SqlCommand("up_NextTestDue", DH.GetConnection)
        cmd.CommandType = Data.CommandType.StoredProcedure
        Dim prmAUID As New Data.SqlClient.SqlParameter("@AUID", strAUID)
        prmAUID.Direction = Data.ParameterDirection.Input
        prmAUID.DbType = Data.DbType.String
        prmAUID.Size = 16
        cmd.Parameters.Add(prmAUID)

        Dim prmTS As New Data.SqlClient.SqlParameter()
        prmTS.ParameterName = "@strTestSetID"
        prmTS.DbType = Data.DbType.String
        prmTS.Size = 20
        prmTS.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(prmTS)

        Dim prmInspectionID As New Data.SqlClient.SqlParameter
        prmInspectionID.ParameterName = "@intInspectionID"
        prmInspectionID.DbType = Data.DbType.Int32
        prmInspectionID.Direction = Data.ParameterDirection.Output
        prmInspectionID.Value = 0
        cmd.Parameters.Add(prmInspectionID)

        Dim prmTestDescription As New Data.SqlClient.SqlParameter()
        prmTestDescription.ParameterName = "@TestDescription"
        prmTestDescription.DbType = Data.DbType.String
        prmTestDescription.Size = 50
        prmTestDescription.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(prmTestDescription)

        Dim prmNextTestDate As New Data.SqlClient.SqlParameter
        prmNextTestDate.ParameterName = "@NextTestDate"
        prmNextTestDate.DbType = Data.DbType.DateTime
        prmNextTestDate.Direction = Data.ParameterDirection.Output
        prmNextTestDate.Value = "2000-01-01"
        cmd.Parameters.Add(prmNextTestDate)


        Dim prmOverDue As New Data.SqlClient.SqlParameter
        prmOverDue.ParameterName = "@Overdue"
        prmOverDue.DbType = Data.DbType.Boolean
        prmOverDue.Direction = Data.ParameterDirection.Output
        prmOverDue.Value = False
        cmd.Parameters.Add(prmOverDue)
        cmd.Connection.Open()
        cmd.ExecuteNonQuery()
        ' Dim dr As Data.SqlClient.SqlDataReader = cmd.ExecuteReader

        Dim TmpTestStructure As New TestStructure

        If Not IsDBNull(prmTS.Value) Then
            TmpTestStructure.TestSetID = prmTS.Value
        Else
            TmpTestStructure.TestSetID = ""
        End If

        If Not IsDBNull(prmInspectionID.Value) Then
            TmpTestStructure.InspectionNo = prmInspectionID.Value
        Else
            TmpTestStructure.InspectionNo = 0
        End If

        If Not IsDBNull(prmTestDescription.Value) Then
            TmpTestStructure.TestDescription = prmTestDescription.Value
        Else
            TmpTestStructure.TestDescription = ""
        End If

        If Not IsDBNull(prmNextTestDate.Value) Then
            TmpTestStructure.NextTestDate = prmNextTestDate.Value
        Else
            TmpTestStructure.NextTestDate = "2000-01-01"
        End If

        If Not IsDBNull(prmOverDue.Value) Then
            TmpTestStructure.OverDue = prmOverDue.Value
        Else
            TmpTestStructure.OverDue = 0
        End If
        cmd = Nothing
        cmd = New Data.SqlClient.SqlCommand("up_GetLastInspectionDetails", DH.GetConnection)
        cmd.CommandType = Data.CommandType.StoredProcedure

        prmAUID = New Data.SqlClient.SqlParameter("@AUID", strAUID)
        prmAUID.Direction = Data.ParameterDirection.Input
        prmAUID.DbType = Data.DbType.String
        prmAUID.Size = 16
        cmd.Parameters.Add(prmAUID)
        'Parameter 0
        Dim prmServiceID As New Data.SqlClient.SqlParameter
        prmServiceID.ParameterName = "@ServiceID"
        prmServiceID.DbType = Data.DbType.Int32
        prmServiceID.Direction = Data.ParameterDirection.Output
        prmServiceID.Value = 0
        cmd.Parameters.Add(prmServiceID)

        Dim prmTestDate As New Data.SqlClient.SqlParameter
        prmTestDate.ParameterName = "@TestDate"
        prmTestDate.DbType = Data.DbType.Date
        prmTestDate.Direction = Data.ParameterDirection.Output
        prmTestDate.Value = "2000-01-01"
        cmd.Parameters.Add(prmTestDate)

        Dim prmResult As New Data.SqlClient.SqlParameter
        prmResult.ParameterName = "@result"
        prmResult.DbType = Data.DbType.Boolean
        prmResult.Direction = Data.ParameterDirection.Output
        prmResult.Value = False
        cmd.Parameters.Add(prmResult)
        'Parameter 3
        
        Cmd.connection.open()
        cmd.ExecuteNonQuery()
        If Not IsDBNull(prmServiceID.Value) Then
            TmpTestStructure.LastTestID = prmServiceID.Value
        End If

        If Not IsDBNull(prmTestDate.Value) Then
            TmpTestStructure.LastTestDate = prmTestDate.Value
        End If

        If Not IsDBNull(prmResult.Value) Then
            TmpTestStructure.LastTestPass = prmResult.Value
        End If

        'TmpTestStructure.BondingLabel = GetLabelDetail("Bonding", TmpTestStructure.Testsetid, TmpTestStructure.InspectionNo)
        'TmpTestStructure.FailureLabel = GetLabelDetail("Failure", TmpTestStructure.Testsetid, TmpTestStructure.InspectionNo)

        Return TmpTestStructure

        

    End Function
    Public Function IsTestInHeirarchy(ByVal InspectionNo As Integer, ByVal Testsetid As String) As Boolean

        Dim dh As New DataHandling
        Dim dt As Data.DataTable = dh.GetDataTable("Select Heirarchy from tblTestSetDetail where TestSetID='" & Testsetid & "' and InspectionNo = " & InspectionNo)
        Dim dr As Data.DataRow
        'If Heirarchy = true then Test is in Heirarchy
        If dt.Rows.Count = 0 Then
            Throw New Exception("No Test Found")

        End If
        dr = dt.Rows(0)
        If dr.Item("Heirarchy") Then
            Return True
        Else
            Return False
        End If

        
        
    End Function
    Public Function IsItemReturnFlagSet(ByVal AUID As String) As Boolean
        Dim dh As New DataHandling
        Dim dt As Data.DataTable = dh.GetDataTable("Select ReturnFlag from tblitem where auid = '" & AUID & "'")
        If dt.Rows.Count = 0 Then
            Throw New Exception(AUID & " not found in database, Function IsTemReturnFlagSet")
            Exit Function
        End If
        If dt.Rows(0).Item("ReturnFlag") = -1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function IsTestAnIssueTest(ByVal InspectionNo As Integer, ByVal Testsetid As String) As Boolean

        Dim dh As New DataHandling
        Dim dt As Data.DataTable = dh.GetDataTable("Select CheckOnIssue from tblTestSetDetail where TestSetID='" & Testsetid & "' and InspectionNo = " & InspectionNo)
        If dt.Rows.Count = 0 Then
            Throw New Exception("Function IsTestAnIssueTest")
            Exit Function
        End If

        
        
        'If CheckOnIssue = true then test must be checked before issue
        If dt.Rows(0).Item("CheckOnIssue") = 0 Then
            Return False
        Else
            Return True
        End If

        
    End Function
    Public Function UpdateTestSchedule(ByVal TestID As Long) As Boolean

        Dim Test As New TestStructure
        Dim objTest As New clsTest

        
        ' Get Test Schedule
        Test = objTest.GetTestInfo(TestID)
        objTest = Nothing

        If Test.AUID = "" Then
            'ShowMessage "OK", "Test Not Found"
            MsgBox("Test Not Found")
            UpdateTestSchedule = False
            Exit Function
        End If
        If IsItemReturnFlagSet(Test.AUID) Then
            UpdateNextTestDueReturn(TestID, Test.AUID, Test.Testsetid, Test.InspectionNo)
        End If
        'Else
        If Test.LastTestPass Then
            'MsgBox "Test Passed"
            If IsTestInHeirarchy(Test.InspectionNo, Test.Testsetid) Then
                'MsgBox "Test In Heirarchy"
                UpdateHeirarchyTestSchedule(Test.AUID, TestID, Test.InspectionNo, Test.Testsetid, Test.LastTestDate)
            Else
                'MsgBox "Test Not In Heirarchy"

                'Set LastTestID = TestID
                If Test.Frequency > 0 And Test.Frequency <> 999 Then
                    UpdateNotInHeirarchyTestScheduleWITHNextTest(Test.AUID, TestID, Test.InspectionNo, Test.Testsetid, Test.LastTestDate, Test.Frequency)
                Else
                    UpdateNotInHeirarchyTestSchedule(Test.AUID, TestID, Test.InspectionNo, Test.Testsetid)
                End If
            End If
        Else
            'Test Failed
            'Set LastTestID = TestID
            'Set Next TestDate = null
            UpdateNotInHeirarchyTestSchedule(Test.AUID, TestID, Test.InspectionNo, Test.Testsetid)

        End If
        'End If
        Return True

       
    End Function

    Private Function GlobalFrequency() As String
        Dim objE As New clsEnvironment
        Select Case objE.ReturnEnvironmentSetting("SETUP", "FREQUENCY", 1).ToUpper
            Case "WEEKS"
                Return "weeks"
            Case "DAYS"
                Return "days"
            Case "YEARS"
                Return "years"
            Case "MONTHS"
                Return "months"

            Case Else
                Return "months"
        End Select
    End Function
    Private Function UpdateHeirarchyTestSchedule(ByVal AUID As String, ByVal TestID As Long, ByVal InspectionNo As Integer, ByVal Testsetid As String, ByVal TestDate As Date) As Boolean

        Dim DH As New DataHandling
        Dim DT As Data.DataTable
        Dim intOrder As Integer
        Dim strQuery As String
        Dim strFreq As String = ""

        
        strQuery = "SELECT tblTestSetDetail.testOrder FROM tblItemTestSchedule, tblTestSetDetail WHERE tblItemTestSchedule.InspectionNo = tblTestSetDetail.InspectionNo AND tblItemTestSchedule.TestSetID = tblTestSetDetail.TestSetID AND tblItemTestSchedule.TestSetID='" & Testsetid & "' AND tblItemTestSchedule.InspectionNo=" & InspectionNo & " AND tblItemTestSchedule.AUID='" & AUID & "';"
        DT = DH.GetDataTable(strQuery)

        'Debug.Print "SELECT tblTestSetDetail.Order FROM tblItemTestSchedule INNER JOIN tblTestSetDetail ON (tblItemTestSchedule.InspectionNo = tblTestSetDetail.InspectionNo) AND (tblItemTestSchedule.TestSetID = tblTestSetDetail.TestSetID) WHERE (((tblItemTestSchedule.TestSetID)='" & TestSetID & "') AND ((tblItemTestSchedule.InspectionNo)=" & InspectionNo & ") AND ((tblItemTestSchedule.AUID)='" & AUID & "'));"
        'MsgBox "Test ID " & TestID & vbCrLf & "InspectionNo " & InspectionNo & vbCrLf & "TestSetID " & TestSetID
        If DT.Rows.Count = 0 Then
            

            Return False
            Exit Function
        End If

        Select Case UCase(globalFrequency)
            Case "DAYS"
                strFreq = "d"
            Case "WEEKS"
                strFreq = "ww"
            Case "MONTHS"
                strFreq = "m"
            Case "YEARS"
                strFreq = "yyyy"
        End Select
        intOrder = DT.Rows(0).Item("TestOrder")
        
        
        strQuery = "SELECT tblItemTestSchedule.LastTest, tblItemTestSchedule.NextTest, tblItemTestSchedule.InspectionNo," & _
            "tblTestSetDetail.frequency FROM tblItemTestSchedule, tblTestSetDetail " & _
            "WHERE tblItemTestSchedule.InspectionNo = tblTestSetDetail.InspectionNo " & _
            "AND tblItemTestSchedule.TestSetID = tblTestSetDetail.TestSetID " & _
            "AND tblItemTestSchedule.TestSetID = '" & Testsetid & "' " & _
            "AND tblTestSetDetail.testOrder >= " & intOrder & " " & _
            "AND tblItemTestSchedule.AUID = '" & AUID & "' " & _
            "AND tblTestSetDetail.Heirarchy =-1 ORDER BY tblTestSetDetail.testOrder"
        DT = DH.GetDataTable(strQuery)
        
        Dim dr As Data.DataRow
        For Each dr In DT.Rows
            strQuery = "Update tblitemtestschedule " & _
            "set lasttest = " & TestID & ", " & _
            "NextTest = " & DH.FormatDate(DateAdd(strFreq, CInt(dr.Item("Frequency")), TestDate)) & " " & _
            " where Inspectionno = " & dr.Item("InspectionNo") & " " & _
            "AND tblItemTestSchedule.AUID = '" & AUID & "' "
            DH.ExecuteNonQuery(strQuery)
        Next
        
        Return True

       
    End Function
    Public Sub UpdateNextTestDueReturn(ByVal TestID As Long, ByVal AUID As String, ByRef strTestSetID As String, ByVal intInspectionid As Integer)

        Dim objTest As New clsTest
        Dim TestInfo As New TestStructure

       
        TestInfo = objTest.GetTestInfo(TestID)

        If TestInfo.LastTestPass Then
            If HowManyMoreReturnTests(AUID) > 1 Then
                CompleteReturnTest(AUID, TestInfo.InspectionNo, TestInfo.Testsetid)

            Else
                'Complete inspection then reset tmp flag and delete tmpreturn records
                CompleteReturnTest(AUID, TestInfo.InspectionNo, TestInfo.Testsetid)
                DeleteReturnTests(AUID)
                'Reset Return Flag
                ResetReturnFlag(AUID)
            End If
        Else
            If HowManyMoreReturnTests(AUID) = 1 Then
                If IsTestInHeirarchy(TestInfo.InspectionNo, TestInfo.Testsetid) Then
                    'Complete inspection then reset tmp flag and delete tmpreturn records
                    CompleteReturnTest(AUID, TestInfo.InspectionNo, TestInfo.Testsetid)
                    UpdateNotInHeirarchyTestSchedule(AUID, TestID, TestInfo.InspectionNo, TestInfo.Testsetid)

                    DeleteReturnTests(AUID)
                    'Reset Return Flag
                    ResetReturnFlag(AUID)
                Else
                    'Reset Return Flag
                    ResetReturnFlag(AUID)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If

        
    End Sub
    Private Function HowManyMoreReturnTests(ByVal AUID As String) As Integer
        Dim DH As New DataHandling
        Dim DT As Data.DataTable

        

        DT = DH.GetDataTable("Select count(testorder) as cntTestOrder from tbltmpreturn where completed=0 and auid='" & AUID & "'")

        Return DT.Rows(0).Item("cntTestOrder")

        
    End Function
    Private Sub CompleteReturnTest(ByVal AUID As String, ByVal intInspectionNo As Integer, ByVal strTestSetID As String)

        Dim dh As New DataHandling
        dh.ExecuteNonQuery("UPDATE TBLTMPRETURN SET TBLTMPRETURN.COMPLETED = -1 WHERE (((TBLTMPRETURN.AUID)='" & AUID & "') AND ((TBLTMPRETURN.TESTSETID)='" & strTestSetID & "') AND ((TBLTMPRETURN.INSPECTIONNO)=" & intInspectionNo & "))")
        
    End Sub
    Private Sub DeleteReturnTests(ByVal AUID As String)
        Dim DH As New DataHandling
        DH.ExecuteNonQuery("DELETE FROM TBLTMPRETURN WHERE auid='" & AUID & "'")
        
    End Sub
    Private Sub ResetReturnFlag(ByVal strAUID As String)

        Dim DH As New DataHandling
        DH.ExecuteNonQuery("Update tblItem set ReturnFlag=0 where AUID='" & strAUID & "'")

        
    End Sub
    Private Function UpdateNotInHeirarchyTestSchedule(ByVal AUID As String, ByVal TestID As Long, ByVal InspectionNo As Integer, ByVal Testsetid As String) As Boolean
        Dim dh As New DataHandling
        Dim dt As Data.DataTable

        
        dt = dh.GetDataTable("Select * from tblItemTestSchedule where AUID='" & AUID & "' and TestSetID='" & Testsetid & "' and InspectionNo= " & InspectionNo)

        If dt.Rows.Count = 0 Then
            
            Return False
            Exit Function
        End If
        Dim strQuery As String
            
        strQuery = "UPDATE TBLITEMTESTSCHEDULE "
        strQuery = strQuery & "SET "
        strQuery = strQuery & "LASTTEST = " & TestID & ", "
        strQuery = strQuery & "NEXTTEST = Null "
        strQuery = strQuery & "WHERE "
        strQuery = strQuery & "AUID='" & AUID & "' and TestSetID='" & Testsetid & "' and InspectionNo= " & InspectionNo
        dh.ExecuteNonQuery(strQuery)
            


        Return True

        
    End Function
    Private Function UpdateNotInHeirarchyTestScheduleWITHNextTest(ByVal AUID As String, ByVal TestID As Long, ByVal InspectionNo As Integer, ByVal Testsetid As String, ByVal TestDate As Date, ByVal FREQ As Integer) As Boolean
        Dim DH As New DataHandling
        Dim DT As Data.DataTable
        Dim strFreq As String = ""
        Dim strSQL As String

        
        Select Case UCase(globalFrequency)
            Case "DAYS"
                strFreq = "d"
            Case "WEEKS"
                strFreq = "ww"
            Case "MONTHS"
                strFreq = "m"
            Case "YEARS"
                strFreq = "yyyy"
        End Select

        DT = DH.GetDataTable("Select * from tblItemTestSchedule where AUID='" & AUID & "' and TestSetID='" & Testsetid & "' and InspectionNo= " & InspectionNo)

        If DT.Rows.Count = 0 Then
          
            Return False
            Exit Function
        End If


       
        strSQL = "UPDATE "
        strSQL = strSQL & "tblItemTestSchedule "
        strSQL = strSQL & "SET "
        strSQL = strSQL & "LASTTEST = " & TestID & ", "
        strSQL = strSQL & "NEXTTEST = " & DH.FormatDate(DateAdd(strFreq, FREQ, TestDate)) & " "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "AUID='" & AUID
        strSQL = strSQL & "'AND "
        strSQL = strSQL & "TestSetID='" & Testsetid & "' "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "InspectionNo= " & InspectionNo & " "
        DH.ExecuteNonQuery(strSQL)
       
        Return True

       
    End Function

End Class
