Public Class clsMove
    Dim m_IssueMove As String
    Dim m_ReturnFlag As Boolean
    Dim m_CurrLoc As String
    Dim m_PrevLoc As String
    Dim m_Notes As String
    Dim m_DateAllocated As Date
    Dim m_WearerID As String
    Dim m_IssueMoveID As Long
    Dim m_ItemID As String
    Dim m_OrderNumber As String
    Dim m_NumberOfDays As Integer
    Dim UserID As Integer
    Dim objLocation As clsLocation



    Public Function CheckMaintenance(ByVal strAUID As String, Optional ByVal lngNoOfDays As Long = 0) As Boolean
        '-------------------------------------------------------------------
        'Returns true if the maintenance for the item passed is up to date

        Dim objTestSched As clsTestSchedule




        objTestSched = New clsTestSchedule
        CheckMaintenance = objTestSched.OKToIssue(strAUID, lngNoOfDays)

        objTestSched = Nothing


    End Function
    Public Property NoOfDays() As Integer
        Get
            Return m_NumberOfDays
        End Get
        Set(ByVal value As Integer)
            m_NumberOfDays = value
        End Set
    End Property
    Public Property ItemID() As String
        Get
            Return m_ItemID
        End Get
        Set(ByVal value As String)
            m_ItemID = value
        End Set
    End Property
    Public Property DateAllocated() As Date
        Get
            Return m_DateAllocated
        End Get
        Set(ByVal value As Date)
            m_DateAllocated = value
        End Set
    End Property
    Public Property IssueMove() As String
        Get
            Return m_IssueMove
        End Get
        Set(ByVal value As String)
            m_IssueMove = value
        End Set
    End Property
    Public Property ReturnFlag() As Boolean
        Get
            Return m_ReturnFlag
        End Get
        Set(ByVal value As Boolean)
            m_ReturnFlag = value
        End Set
    End Property
    Public Property PreviousLocation() As String
        Get
            Return m_PrevLoc
        End Get
        Set(ByVal value As String)
            m_PrevLoc = value
        End Set
    End Property
    Public Property CurrentLocation() As String
        Get
            Return m_CurrLoc
        End Get
        Set(ByVal value As String)
            m_CurrLoc = value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return m_Notes
        End Get
        Set(ByVal value As String)
            m_Notes = value
        End Set
    End Property
    Public Property WearerID() As String
        Get
            Return m_WearerID
        End Get
        Set(ByVal value As String)
            m_WearerID = value
        End Set
    End Property
    Public Property OrderNumber() As String
        Get
            Return m_OrderNumber
        End Get
        Set(ByVal value As String)
            m_OrderNumber = value
        End Set
    End Property
    Public Function setReturnFlag(ByVal AUID As String, ByVal bTestOverDue As Boolean, Optional ByVal fBatch As Boolean = False) As Boolean
        '-----------------------------------------------------------------------------------------------
        'rstItem will need to contain
        '       -   ReturnFlag

        'Need to check that the item is not recorded as not been sealed.  If it is leave it as set to true
        'false- item has not been used so it is not a return
        'true - item has been used so is a return

        'Dim strAnswer As String

        Dim strsql As String
        Dim DH As New DataHandling
        Dim RS As Data.DataRow



        RS = Nothing

        m_ReturnFlag = DH.GetDataTable("Select ReturnFlag from tblitem where auid = '" & AUID & "'").Rows(0)(0)

        If m_ReturnFlag Then
            setReturnFlag = True
            Exit Function
        End If

        If fBatch = False Then


            If Not bTestOverDue Then


                m_ReturnFlag = True
                strsql = "UPDATE tblItem SET ReturnFlag = -1, Status = 2 WHERE AUID = '" & AUID & "' "

                DH.ExecuteNonQuery(strsql)

            End If
        End If



        Return m_ReturnFlag


    End Function
    Public Function IsItemOnIssue(ByVal strAUID As String) As Boolean
        '----------------------------------------------------------------
        'For Item AUID passed, return TRUE if it is on issue
        'Copy of Andy's
        'Check ORDERNO
        Dim DH As New DataHandling
        Dim dtIssue As Data.DataTable




        dtIssue = DH.GetDataTable("Select tblIssue_Move.* FROM tblIssue_Move WHERE ItemID = '" & strAUID & "' AND tblIssue_Move.DateRemoved is Null and not (tblIssue_Move.OrderNo) is Null;")

        If dtIssue.Rows.Count >= 1 Then
            Return True
        Else
            Return False
        End If


    End Function
    Public Sub ItemReturnToWSLoc(ByRef dtItemRecordset As Data.DataTable, Optional ByVal fBatch As Boolean = False)
        '---------------------------------------------------------------------------------------------------------
        'rstItemRecordset will need to contain:
        '           -   CurrentLocation
        '           -   LastLocation
        '           -   AUID
        '           -   DateRemoved
        '           -   ReturnFlag

        'Moves item to workstation location (maintenance).
        'WIll be more than one item if part of a batch

        'Find out about processing and rolling back transaction

        Dim dr As Data.DataRow
        For Each dr In dtItemRecordset.Rows
            ItemID = dr.Item("AUID")
            objLocation = New clsLocation
            m_CurrLoc = objLocation.WorkstationLoc                  'get and set current workstation location
            PreviousLocation = dr.Item("CurrentLocation")     'set previous location
            If Trim(dr.Item("CurrentLocation")) <> Trim(m_CurrLoc) Then

                'Remove Issue & PersonnelID From Item
                dr.Item("Issued") = 0
                dr.Item("PersonnelID") = ""

                ItemIssueMove(ItemID)
            End If
        Next


    End Sub
    Public Sub ItemReturnToSpecifiedLoc(ByRef dtItemRecordset As Data.DataTable, ByVal LocID As String, Optional ByVal fBatch As Boolean = False)
        'Sub based on ItemReturnToWSLoc, extended to allow user to define Return Location
        '---------------------------------------------------------------------------------------------------------
        'rstItemRecordset will need to contain:
        '           -   CurrentLocation
        '           -   LastLocation
        '           -   AUID
        '           -   DateRemoved
        '           -   ReturnFlag

        'Moves item to workstation location (maintenance).
        'WIll be more than one item if part of a batch

        'Find out about processing and rolling back transaction
        Dim ItemID As String
        Dim PreviousLocation As String
        Dim dr As Data.DataRow
        For Each dr In dtItemRecordset.Rows
            ItemID = dr.Item("AUID")

            CurrentLocation = LocID
            PreviousLocation = dr.Item("CurrentLocation")     'set previous location
            If Trim(dr.Item("CurrentLocation")) <> Trim(CurrentLocation) Then

                'Remove Issue & PersonnelID From Item
                dr.Item("Issued") = 0
                dr.Item("PersonnelID") = ""

                ItemIssueMove(ItemID)
            End If

        Next
        
    End Sub
    Private Sub ItemIssueMove(ByRef AUID As String)
        Me.ItemID = AUID
        UpdateItem(AUID, m_CurrLoc)      'Update item Table
        SetDateRemoved(AUID)                                'set dateremoved of last issue/move record to current date

        SetUpMoveRec()                                 'call temp sub to enter fields not yet catered for
        AddIssueMoveRecord()                            'new issue/move record with details of current location

        'end transaction ---------------------


    End Sub
    Private Sub UpdateItem(ByVal AUID As String, ByVal strCurrLoc As String)

        Dim dh As New DataHandling
        dh.ExecuteNonQuery("Update tblItem set LastLocation = '" & PreviousLocation & "',CurrentLocation = '" & CurrentLocation & "', ReturnFlag = " & IIf(ReturnFlag, -1, 0) & " where AUID = '" & AUID & "'")


    End Sub
    Private Sub SetDateRemoved(ByVal AUID As String)
        '--------------------------------------------------
        'Finds the latest issuemove record for the item and sets the date removed to the current date
        Dim dh As New DataHandling
        Dim dt As Data.DataTable = dh.GetDataTable("Select count(*) from tblissue_move where itemid='" & AUID & "'")
        Dim strsql As String
        If dt.Rows.Count > 0 Then
            strsql = "Update tblIssue_Move set DateRemoved = getDate()  where itemid='" & AUID & "' and DateRemoved is null "
            dh.ExecuteNonQuery(strsql)
        End If
        
    End Sub
    Private Sub SetUpMoveRec()
        '---------------------------
        'Probably a temporary sub to add fields not yet catered for

        
        DateAllocated = Now
        Notes = " "
        If m_WearerID = "" Then
            WearerID = "0"
        End If

        
    End Sub
    Private Sub AddIssueMoveRecord()
        Dim DH As New DataHandling
        Dim dt As Data.DataTable = DH.GetDataTable("Select isnull(max(Issue_MoveID),0) + 1 as NewIssueMoveID from tblIssue_Move")
        m_IssueMoveID = dt.Rows(0)(0)
        '-------------------------------
        'Adds an issue/move record

        Dim strsql As String
        strsql = "Insert into tblIssue_Move(Issue_MoveID,ItemID,WearerID,LocationID,DateAllocated,Notes,Issue_Move,UserID,OrderNo,NoOfDays) values("
        strsql = strsql & m_IssueMoveID & ", "
        strsql = strsql & "'" & m_ItemID & "', "
        strsql = strsql & "'" & m_WearerID & "', "
        strsql = strsql & "'" & m_CurrLoc & "', "
        strsql = strsql & "GetDate(), "
        strsql = strsql & "'" & m_Notes & "', "
        strsql = strsql & "'" & m_IssueMove & "', "
        strsql = strsql & UserID & ", "
        If m_IssueMove = "Issue" Then

            strsql = strsql & "'" & m_OrderNumber & "', "
        Else
            strsql = strsql & "Null, "
        End If
        If m_NumberOfDays = Nothing Then
            strsql = strsql & "Null)"
        Else
            strsql = strsql & m_NumberOfDays & ")"
        End If
        DH.ExecuteNonQuery(strsql)
        DeleteTempIssue(m_ItemID)

        
       
    End Sub
    Public Sub DeleteTempIssue(ByVal AUID As String)
        '--------------------------------------------------------------
        'Deletes temporary issues
        Dim dh As New DataHandling
        dh.ExecuteNonQuery("Delete from tbltmpIssue where AUID = '" & AUID & "'")


    End Sub

    Public Sub New(ByVal UserID As Integer)
        UserID = UserID
    End Sub

    Public Sub itemIssueOrMove(ByVal AUID As String, ByVal strRequiredLocation As String)

        '-------------------------------------------------------------------------------------------------------
        'rstItemRecordset will need to contain:
        '       -   AUID
        '       -   CurrentLocation
        '       -   LastLocation
        '       -   ReturnFlag

        'Performs issues and moves (for returns see ItemReturn)
        'will be more than one item in recordset if it is part of batch
        '************** there may be tests to carry out for issues
        Dim DH As New DataHandling
        Dim dr As Data.DataRow
        Dim DT As Data.DataTable = DH.GetDataTable("Select * from tblitem where auid = '" & AUID & "'")
        dr = DT.Rows(0)
        CurrentLocation = strRequiredLocation
        If dr.Item("CurrentLocation") <> strRequiredLocation Or IsDBNull(dr.Item("CurrentLocation")) Then
            If Not IsDBNull(dr.Item("CurrentLocation")) Then
                PreviousLocation = dr.Item("CurrentLocation")
            Else
                PreviousLocation = ""
            End If
            ItemIssueMove(ItemID)
            If m_IssueMove = "Issue" Then
                DH.ExecuteNonQuery("Update tblitem set issued= -1, status = 5 where auid = '" & ItemID & "'")
            End If


        End If





    End Sub
    Public ReadOnly Property Issue_MoveID() As Long
        Get
            Return m_IssueMoveID
        End Get
    End Property
    End Class
