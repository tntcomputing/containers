Imports SQL = System.Data.SqlClient
Public Class BatchIssue
    Dim dh As DataHandling
    Dim m_lstAUID As New List(Of String)
    Public ReadOnly Property lstAUID() As List(Of String)
        Get
            Return m_lstAUID
        End Get
    End Property
    Dim UserID As Int32
    Public Sub New(ByVal UserID As Int32)
        dh = New DataHandling
        UserID = UserID
    End Sub
    Public Sub New(ByVal UserID As Int32, ByVal tmpCBNumber As Int32)
        dh = New DataHandling
        UserID = UserID
        LoadTMPBatch(tmpCBNumber)
    End Sub
    Public Function getDTOfOpenBatches() As System.Data.DataTable
        Return dh.GetDataTable("Select * from tblTMPCONTAINERBATCH order by TMPCBNumber desc")
    End Function

    Private Sub LoadTMPBatch(ByVal TMPCBNumber As Int32)
        Dim DT As System.Data.DataTable = dh.GetDataTable("SELECT * FROM tblTMPCONTAINERBATCH WHERE tmpcbnumber=" & TMPCBNumber)
        If DT.Rows.Count > 0 Then
            Dim arr() As String = DT.Rows(0).Item("auids").ToString.Split("|")

            ' Convert to List.
            m_lstAUID = arr.ToList()
            Me.m_LocationID = DT.Rows(0).Item("LocationID").ToString
            Me.m_tmpCBNUMBER = DT.Rows(0).Item("TMPCBNUMBER")
        End If

    End Sub
    
    Private Sub SaveTMPBatch()
        If Me.TMPCBNUMBER = 0 Then
            'first save so no tmpcbnumber
            GeneratetmpDBNumber()
        End If
        Dim strAUIDS As String = String.Join("|", m_lstAUID.ToArray)
        Me.RemoveTMPBatch()
        dh.ExecuteNonQuery("INSERT INTO TBLTMPCONTAINERBATCH(TMPCBNUMBER,LOCATIONID,AUIDS,LASTMODIFIED) VALUES(" & Me.TMPCBNUMBER & ",'" & Me.LocationID & "','" & strAUIDS & "',GETDATE())")

    End Sub
    Private Sub RemoveTMPBatch(Optional ByVal clearMemberVariable As Boolean = False)

        dh.ExecuteNonQuery("Delete from tblTMPCONTAINERBATCH where TMPCBNUMBER = " & Me.TMPCBNUMBER)
        If clearMemberVariable Then
            Me.m_tmpCBNUMBER = 0
        End If
    End Sub
    Private Sub GeneratetmpDBNumber()
        m_tmpCBNUMBER = dh.ExecuteIntScalar("select isnull(MAX(tmpcbnumber),0)+1 from tblTMPCONTAINERBATCH")
    End Sub
    Dim m_CBNUMBER As String = ""
    Public ReadOnly Property CBNUMBER() As String
        Get
            Return m_CBNUMBER
        End Get

    End Property
    Dim m_tmpCBNUMBER As Int32 = 0
    Public ReadOnly Property TMPCBNUMBER() As Int32
        Get
            Return m_tmpCBNUMBER
        End Get

    End Property
    Dim m_LocationID As String = ""
    Public Property LocationID() As String
        Get
            Return m_LocationID
        End Get
        Set(ByVal value As String)
            m_LocationID = value
        End Set
    End Property
    Public Sub AddItem(ByVal AUID As String)
        If m_lstAUID.Contains(AUID) = False Then
            m_lstAUID.Add(AUID)
            Me.SaveTMPBatch()
        End If
    End Sub
    Public Sub RemoveItem(ByVal AUID As String)

        m_lstAUID.Remove(AUID)
        Me.SaveTMPBatch()

    End Sub
    Private Sub GetNewBatchNumber()

        Dim dt As Data.DataTable
        Dim CB_Number As Integer
        dt = dh.GetDataTable("SELECT TOP 1 [ORDERNO] FROM [TBLISSUE_MOVE] where orderno like 'CB%' order by dateallocated desc")
        If dt.Rows.Count = 0 Then
            CB_Number = 1
        Else
            Try
                CB_Number = CType(Right(dt.Rows(0).Item("OrderNo"), Len(dt.Rows(0).Item("OrderNo")) - 2), Integer) + 1


            Catch ex As Exception

                Throw New Exception("Corrupt Batch Number data, unable to continue")
            End Try

        End If
        m_CBNUMBER = "CB" & CB_Number.ToString

    End Sub
    Public Sub SetComboBoxLocation(ByRef CB As ComboBox)
        If CB.Items.Count > 1 Then
            Exit Sub
        End If
        Dim dt As Data.DataTable
        dt = dh.GetDataTable("SELECT LOCATIONID FROM TBLLOCATION WHERE ISSUELOCATION = -1 ORDER BY LOCATIONID")
        Dim dr As Data.DataRow = dt.NewRow
        dr.Item(0) = ""


        dt.Rows.InsertAt(dr, 0)
        CB.DataSource = dt
        CB.DisplayMember = "LOCATIONID"
        CB.ValueMember = "LOCATIONID"



    End Sub
    Private Function GetIssue_MoveID() As Integer
        Return dh.ExecuteIntScalar("Select isnull(max(issue_moveID),0) + 1 from tblissue_move")
    End Function
    Public Sub AbandonBatch()
        Me.RemoveTMPBatch(True)

    End Sub
    Public Sub SaveBatch(ByVal UserID As Integer, ByVal DispatchDate As Date)
        If Me.LocationID = "" Then
            Throw New Exception("No Location Set")

        End If
        Dim Issue_MoveID As Integer = Me.GetIssue_MoveID
        Dim auid As String
        Dim dteIssueDate As Date
        Dim lngMoveID As Long
        GetNewBatchNumber()
        Dim m_cn As SQL.SqlConnection = dh.GetConnection
        m_cn.Open()
        'Dim cmd As New SQL.SqlCommand("insert into tblissue_move(Issue_MoveID,ItemID,LocationID,WearerID,DateAllocated,Issue_Move,OrderNo,UserID) values(?,?,?,?,?,?,?,?)", m_cn)
        'cmd.CommandType = System.Data.CommandType.Text
        ' Dim transaction As SQL.SqlTransaction

        ' Start a local transaction
        ' transaction = m_cn.BeginTransaction("SampleTransaction")
        dh.ExecuteNonQuery("INSERT INTO TBLCONTAINERBATCH VALUES('" & Me.CBNUMBER & "','" & m_LocationID & "','" & DispatchDate.Year & "-" & DispatchDate.Month & "-" & DispatchDate.Day & "',getDate())")

        Dim objMove As New clsMove(UserID)
        For Each auid In m_lstAUID
            dh.ExecuteNonQuery("Update tblItem set PersonnelID ='',status=5,Issued=-1 where auid = '" & auid & "'")

            objMove.OrderNumber = Me.CBNUMBER
            objMove.IssueMove = "Issue"
            objMove.ItemID = auid
            objMove.WearerID = ""
            Dim objEnvironment = New clsEnvironment
            objMove.NoOfDays = CType(objEnvironment.ReturnEnvironmentSetting("SETUP", "DEFAULTNODAYS", 1), Integer)
            objMove.itemIssueOrMove(auid, m_LocationID)
            dteIssueDate = objMove.DateAllocated
            lngMoveID = objMove.Issue_MoveID

            '      cmd.Parameters.Clear()
            '     cmd.Parameters.Add(Issue_MoveID)
            '    cmd.Parameters.Add(auid)
            '   cmd.Parameters.Add(Me.LocationID)
            '  cmd.Parameters.Add(0)
            ' cmd.Parameters.Add(Now)
            ' cmd.Parameters.Add("Issue")
            ' cmd.Parameters.Add(Me.CBNUMBER)
            ' cmd.Parameters.Add(UserID)
            ' cmd.ExecuteNonQuery()


            Issue_MoveID += 1

        Next
        m_lstAUID.Clear()
        dh.ExecuteNonQuery("EXEC FillContainerBatchDetail '" & Me.CBNUMBER & "'")
        Me.RemoveTMPBatch(True)
        'Try
        '    transaction.Commit()
        'Catch ex As Exception
        '    Try
        '        transaction.Rollback()
        '    Catch ex2 As Exception
        '        Throw ex2
        '    End Try
        '    Throw ex
        'End Try

    End Sub
    Public Function CanAuidBeIssued(ByVal AUID As String, ByRef strMessage As String) As Boolean



        Dim bIssue As Boolean



        'When AUID has been entered:
        '   -   Check it exists
        Dim dtItem As Data.DataTable
        Dim drItem As Data.DataRow
        dtItem = dh.GetDataTable("Select * from tblitem where auid='" & AUID & "'")

        If dtItem.Rows.Count = 0 Then

            'ShowMessage "OK", Scan1.Value & " does not exist"
            strMessage = AUID & " does not exist"
            Return False
            Exit Function

        End If

        drItem = dtItem.Rows(0)
        If drItem.Item("Scrapped") <> 0 Then
            'ShowMessage "OK", Scan1.Value & " has been scrapped"

            strMessage = AUID & " has been scrapped/bonded"
            Return False
            Exit Function



        End If

        Dim r As Data.DataRow
        For Each r In Me.getDTOfOpenBatches.Rows
            Dim arr() As String = r.Item("auids").ToString.Split("|")
            If arr.ToList.Contains(AUID) Then
                strMessage = AUID & " is already in an open batch"
                Return False
                Exit Function
            End If
        Next
        'txtAUID = txtAUID.Text

        'Perform Check That the Item is ready for Issue
        '**********************************************
        '**********************************************

        If CheckItemForIssue(drItem, strMessage) Then

            'Perform Check That the Item can be Issued to Wearer
            '***************************************************
            '***************************************************
            If CheckItemCanBeIssued(drItem) Then

                'Perform Check On Fit Test
                'If return = 0 then No Fit Test Present
                'If Less than 30 then put up Warning
                bIssue = True
                '**********************************************
                '**********************************************
                'Check Item Is Not Already in Issue List
                Dim strItem As String
                For Each strItem In m_lstAUID
                    If UCase(strItem) = UCase(AUID) Then
                        strMessage = "This item is already included in this Issue"
                        Return False
                        Exit Function
                    End If
                Next

                Return True    'ContinueIssue
            Else
                Return False
            End If




        Else
            'ShowMessage "OK", m_strMess
            Return False


        End If






    End Function
    Private Function CheckItemForIssue(ByVal drItem As Data.DataRow, ByRef strMessage As String) As Boolean

        Dim objMove As New clsMove(UserID)




        CheckItemForIssue = True
        Dim dt As Data.DataTable = dh.GetDataTable("Select * from tblIssue_Move where itemid ='" & drItem("AUID") & "' and Issue_Move='Issue' and Dateremoved is Null")
        If dt.Rows.Count > 0 Then
            strMessage = "The Container is already out on issue"
            Return False
            Exit Function
        End If
        If Not objMove.CheckMaintenance(drItem.Item("AUID")) Then
            strMessage = "The Container requires maintenance so cannot be issued"
            Return False
            Exit Function
        End If

        'If drItem.Item("ReturnFlag") Then

        '    strMessage = "The Container requires maintenance so cannot be issued"
        '    Return False
        '    Exit Function
        'Else

        '    If Not objMove.CheckMaintenance(drItem.Item("AUID")) Then
        '        strMessage = "The Container requires maintenance so cannot be issued"
        '        Return False
        '        Exit Function
        '    Else

        '        If drItem("Issued") = -1 Then

        '            strMessage = "The Container is already out on Issue"
        '            Return False
        '            Exit Function
        '        End If

        '    End If

        'End If

        Return True


    End Function
    Private Function CheckItemCanBeIssued(ByVal drItem As Data.DataRow) As Boolean

        'No Personnel so true
        Return True


    End Function


End Class
