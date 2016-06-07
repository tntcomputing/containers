Imports SQL = System.Data.SqlClient
Public Class BatchReturn

    Dim dh As DataHandling
    Dim m_lstAUID As New List(Of String)

    Public ReadOnly Property lstAUID() As List(Of String)
        Get
            Return m_lstAUID
        End Get
    End Property
    Public Sub New()
        dh = New DataHandling

    End Sub
    Public Sub New(ByVal tmpRCBNumber As Int32)
        dh = New DataHandling

        LoadTMPReturnBatch(tmpRCBNumber)
    End Sub
    Public Function getDTOfOpenReturnBatches() As System.Data.DataTable
        Return dh.GetDataTable("Select * from tblTMPCONTAINERRETURNBATCH order by TMPRCBNumber desc")
    End Function
    Public Function DoesBatchAlreadyExist(ByVal LocationID As String) As Boolean
        Return Convert.ToBoolean(dh.ExecuteIntScalar("Select count(TMPRCBNumber) from tblTMPCONTAINERRETURNBATCH where LocationID = '" & LocationID & "'"))

    End Function
    Private Sub LoadTMPReturnBatch(ByVal TMPRCBNumber As Int32)
        Dim DT As System.Data.DataTable = dh.GetDataTable("SELECT * FROM tblTMPCONTAINERRETURNBATCH WHERE tmprcbnumber=" & TMPRCBNumber)
        If DT.Rows.Count > 0 Then
            Dim arr() As String = DT.Rows(0).Item("auids").ToString.Split("|")

            ' Convert to List.
            m_lstAUID = arr.ToList()
            Me.m_LocationID = DT.Rows(0).Item("LocationID")
            Me.m_tmpRCBNUMBER = DT.Rows(0).Item("TMPRCBNUMBER")
        End If

    End Sub
    Public Sub LoadTMPReturnBatch(ByVal LocationID As String)
        Dim DT As System.Data.DataTable = dh.GetDataTable("SELECT * FROM tblTMPCONTAINERRETURNBATCH WHERE LocationID='" & LocationID & "'")
        If DT.Rows.Count > 0 Then
            Dim arr() As String = DT.Rows(0).Item("auids").ToString.Split("|")

            ' Convert to List.
            m_lstAUID = arr.ToList()
            Me.m_LocationID = DT.Rows(0).Item("LocationID")
            Me.m_tmpRCBNUMBER = DT.Rows(0).Item("TMPRCBNUMBER")
        End If

    End Sub
    Private Sub SaveTMPReturnBatch()
        If Me.TMPRCBNUMBER = 0 Then
            'first save so no tmpcbnumber
            GeneratetmpDBNumber()
        End If
        Dim strAUIDS As String = String.Join("|", m_lstAUID.ToArray)
        Me.RemoveTMPReturnBatch()
        dh.ExecuteNonQuery("INSERT INTO tblTMPCONTAINERRETURNBATCH(TMPRCBNUMBER,LocationID,AUIDS,LASTMODIFIED) VALUES(" & Me.TMPRCBNUMBER & ",'" & Me.m_LocationID & "','" & strAUIDS & "',GETDATE())")

    End Sub
    Private Sub RemoveTMPReturnBatch(Optional ByVal clearMemberVariable As Boolean = False)

        dh.ExecuteNonQuery("Delete from tblTMPCONTAINERRETURNBATCH where TMPRCBNUMBER = " & Me.TMPRCBNUMBER)
        If clearMemberVariable Then
            Me.m_tmpRCBNUMBER = 0
        End If
    End Sub
    Private Sub GeneratetmpDBNumber()
        m_tmpRCBNUMBER = dh.ExecuteIntScalar("select isnull(MAX(tmprcbnumber),0)+1 from tblTMPCONTAINERRETURNBATCH")
    End Sub
    Dim m_RCBNUMBER As String = ""
    Public ReadOnly Property RCBNUMBER() As String
        Get
            Return m_RCBNUMBER
        End Get

    End Property
    Dim m_tmpRCBNUMBER As Int32 = 0
    Public ReadOnly Property TMPRCBNUMBER() As Int32
        Get
            Return m_tmpRCBNUMBER
        End Get

    End Property
    Dim m_LocationID As String
    Public ReadOnly Property LocationID() As String
        Get
            Return m_LocationID
        End Get
    End Property
    Public Sub AddItem(ByVal AUID As String, ByVal LocationID As String)
        If m_lstAUID.Contains(AUID) = False Then
            If LocationID <> Me.m_LocationID Then

                'New Batch create new batch if not exist
                If DoesBatchAlreadyExist(LocationID) Then
                    LoadTMPReturnBatch(LocationID)
                Else
                    Me.m_LocationID = LocationID

                End If

            End If
            m_lstAUID.Add(AUID)
            Me.SaveTMPReturnBatch()
        End If
    End Sub
    Public Sub RemoveItem(ByVal AUID As String)

        m_lstAUID.Remove(AUID)
        Me.SaveTMPReturnBatch()

    End Sub
    Private Sub GetNewReturnBatchNumber()

        Dim dt As Data.DataTable
        Dim RCB_Number As Integer
        dt = dh.GetDataTable("SELECT isnull(max(cast(replace(RCBNUMBER,'RCB','') as numeric(10,0))),0) + 1 as RCBNUMBER FROM tblCONTAINERRETURNBATCH")
        If dt.Rows.Count = 0 Then
            RCB_Number = 1
        Else
            Try
                RCB_Number = dt.Rows(0).Item("RCBNUMBER")


            Catch ex As Exception

                Throw New Exception("Corrupt Return Batch Number data, unable to continue")
            End Try

        End If
        m_RCBNUMBER = "RCB" & RCB_Number.ToString

    End Sub
    Public Sub AbandonBatch()
        Me.RemoveTMPReturnBatch(True)

    End Sub
    Public Sub SaveBatch(ByVal UserID As Integer, ByVal ReturnDate As Date)

        Dim auid As String
        
        GetNewReturnBatchNumber()
        Dim m_cn As SQL.SqlConnection = dh.GetConnection
        m_cn.Open()
        'Dim cmd As New SQL.SqlCommand("insert into tblissue_move(Issue_MoveID,ItemID,LocationID,WearerID,DateAllocated,Issue_Move,OrderNo,UserID) values(?,?,?,?,?,?,?,?)", m_cn)
        'cmd.CommandType = System.Data.CommandType.Text
        ' Dim transaction As SQL.SqlTransaction

        ' Start a local transaction
        ' transaction = m_cn.BeginTransaction("SampleTransaction")
        Dim cbnumber As String
        dh.ExecuteNonQuery("INSERT INTO tblCONTAINERRETURNBATCH VALUES('" & Me.RCBNUMBER & "','" & Me.LocationID & "','" & ReturnDate.Year & "-" & ReturnDate.Month & "-" & ReturnDate.Day & "',getDate())")

        Dim objMove As New clsMove(UserID)
        For Each auid In m_lstAUID
            cbnumber = dh.ExecuteStrScalar("select top 1 orderno from tblissue_move where dateremoved is not null and orderno like 'CB%' and itemid = '" & auid & "' and convert(date,dateremoved) <= '" & ReturnDate.Year & "-" & ReturnDate.Month & "-" & ReturnDate.Day & "' order by issue_moveid desc")

            dh.ExecuteNonQuery("EXEC [FillContainerReturnBatchDetail] '" & Me.RCBNUMBER & "','" & auid & "','" & cbnumber & "'")


        Next
        m_lstAUID.Clear()

        Me.RemoveTMPReturnBatch(True)


    End Sub
End Class
