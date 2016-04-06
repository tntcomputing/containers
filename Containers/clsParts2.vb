Public Class clsParts2
    Private m_AUID As String            'Holds the AUID of the class
    Private m_PartsReplace As String
    Property AUID() As String
        Get
            Return m_AUID
        End Get
        Set(ByVal value As String)
            m_AUID = value
        End Set
    End Property
    Public Function PartsReplaced() As String
        '**********************************************************************************
        'Returns a string showing all the parts replaced
        '**********************************************************************************

        
        CompileStringofPartsReplaced()
        Return m_PartsReplace

        
    End Function
    Private Sub CompileStringofPartsReplaced()
        '**********************************************************************************
        'Compiles a string of Parts Replaced for an AUID from the tbltmppartsreplacedTable
        '**********************************************************************************

        Dim DH As New DataHandling
        Dim DT As Data.DataTable

        DT = DH.GetDataTable("select R.AUID,R.PARTSCODE,R.Status,R.Notes,R.TimeToReplace, P.Description,R.FOC from tbltmppartsreplaced2 R join  tblparts P on R.partscode = P.partcode where p.testsetid='" & GetTestSetID & "' and R.AUID='" & m_AUID & "';")

        If DT.Rows.Count = 0 Then
            m_PartsReplace = "No parts replaced"
        Else
            m_PartsReplace = "Parts Replaced" & vbCrLf & "*******************" & vbCrLf
            Dim dr As Data.DataRow
            For Each dr In DT.Rows
                m_PartsReplace = m_PartsReplace & Trim(dr.Item("Partscode")) & ": " & vbTab & Trim(dr.Item("Description")) & vbCrLf
                m_PartsReplace = m_PartsReplace & "--------------------------------------------" & vbCrLf
                m_PartsReplace = m_PartsReplace & "Time To Replace: " & dr.Item("TimeToReplace") & vbCrLf
                m_PartsReplace = m_PartsReplace & "Free of Charge: "
                If dr.Item("FOC") = True Then
                    m_PartsReplace = m_PartsReplace & "YES" & vbCrLf
                Else
                    m_PartsReplace = m_PartsReplace & "NO" & vbCrLf
                End If
                If dr.Item("Notes") <> "" Then
                    m_PartsReplace = m_PartsReplace & "Notes: " & dr.Item("Notes") & vbCrLf
                End If
                m_PartsReplace = m_PartsReplace & "--------------------------------------------" & vbCrLf & vbCrLf

            Next
           
            m_PartsReplace = m_PartsReplace & "*******************"
        End If

       
    End Sub
    Private Function GetTestSetID() As String
        Dim dh As New DataHandling
        Dim DT As Data.DataTable
        

        DT = dh.GetDataTable("Select top 1 testsetid from tblitemtestschedule where auid = '" & m_AUID & "'")

        Return DT.Rows(0).Item("TestSetID")
        
    End Function

End Class
