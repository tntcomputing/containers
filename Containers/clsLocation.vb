Public Class clsLocation
    Dim DH As DataHandling
    Private m_Location(5) As String
    Dim m_LocationLevels As Integer
    Private m_WorkStationLocation As String
    Public Sub New()
        '-----------------------------


        DH = New DataHandling
        SetCaptions()
        SetWorkStationLoc()


    End Sub
    Private Sub SetCaptions()
        '-----------------------
        'These may change according to system

        Dim dt As New Data.DataTable
        Dim strSQL As String

        

        strSQL = "SELECT * FROM tblEnvironment WHERE CatType = 'LOCATION' AND CatSubType = 'LABELS' "

        dt = DH.GetDataTable(strSQL)

        If dt.Rows.Count > 0 Then

            If Not IsDBNull(dt.Rows(0).Item("Field2")) Then
                m_Location(0) = dt.Rows(0).Item("Field2")
            Else
                m_Location(0) = ""
            End If

            If Not IsDBNull(dt.Rows(0).Item("Field3")) Then
                m_Location(1) = dt.Rows(0).Item("Field3")
            Else
                m_Location(1) = ""
            End If

            If Not IsDBNull(dt.Rows(0).Item("Field4")) Then
                m_Location(2) = dt.Rows(0).Item("Field4")
            Else
                m_Location(2) = ""
            End If


            If Not IsDBNull(dt.Rows(0).Item("Field5")) Then
                m_Location(3) = dt.Rows(0).Item("Field5")
            Else
                m_Location(3) = ""
            End If


            If Not IsDBNull(dt.Rows(0).Item("Field6")) Then
                m_Location(4) = dt.Rows(0).Item("Field6")
            Else
                m_Location(4) = ""
            End If

            If Not IsDBNull(dt.Rows(0).Item("Field7")) Then
                m_Location(5) = dt.Rows(0).Item("Field7")
            Else
                m_Location(5) = ""
            End If

            'number of levels allowed
            If Not IsDBNull(dt.Rows(0).Item("Field1")) Then
                m_LocationLevels = CInt(dt.Rows(0).Item("Field1"))
            Else
                m_LocationLevels = 0
            End If

        Else

            m_Location(0) = "Location ID"
            m_Location(1) = "Cost Centre"
            m_Location(2) = "Site"
            m_Location(3) = "Building"
            m_Location(4) = "Area"
            m_Location(5) = ""                'not used in current system
            m_LocationLevels = 4              'number of levels allowed

        End If
    End Sub

    Private Sub SetWorkStationLoc()
        '------------------------------
        'Done at startup
        'Workstation details are held on text file 'workstation.txt'.  This will be held locally
        'There will be a text file for each workstation.  It will contain the locationid of the workstation

        'Dim fs, f
        'Dim objINIReader As New clsINIReader



        '    Set fs = CreateObject("Scripting.FileSystemObject")
        '    Set f = fs.OpenTextFile(App.Path & "\workstation.txt", 1, 0)

        '    m_WorkstationLocation = f.readline

        'm_WorkstationLocation = objINIReader.GetINILine("WORKSTATION", "LOCATION")
        If globalWorkstationLocation = "" Then
            Dim objEnv As New clsEnvironment
            globalWorkstationLocation = objEnv.ReturnEnvironmentSetting("SETUP", "MAINTLOCID", 1)
        End If
        m_WorkStationLocation = globalWorkstationLocation

        'Set objINIReader = Nothing


    End Sub
    Public ReadOnly Property WorkstationLoc() As String
        Get
            Return m_WorkStationLocation
        End Get
    End Property
End Class
