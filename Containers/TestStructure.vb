Public Class TestStructure
    Private Const MODULENAME = "TestStructure"
    Private m_strAUID As String
    Private m_strTestSetId As String
    Private m_IntInspectionNo As Integer
    Private m_lngLastTestID
    Private m_lngFrequency As Long
    Private m_dLastTestDate As Date
    Private m_dNextTestDate As Date
    Private m_fOverdue As Boolean
    Private m_fLastTestPass As Boolean
    Private m_strTestDescription As String
    Private m_objBondingLabel As clsReportStructure
    Private m_objFailureLabel As clsReportStructure

    Property AUID() As String
        Get
            Return m_strAUID
        End Get
        Set(ByVal value As String)
            m_strAUID = value
        End Set
    End Property
    Property TestSetID() As String
        Get
            Return m_strTestSetId
        End Get
        Set(ByVal value As String)
            m_strTestSetId = value
        End Set
    End Property
    Property InspectionNo() As Integer
        Get
            Return m_IntInspectionNo
        End Get
        Set(ByVal value As Integer)
            m_IntInspectionNo = value
        End Set
    End Property
    Property LastTestDate() As Date
        Get
            Return m_dLastTestDate
        End Get
        Set(ByVal value As Date)
            m_dLastTestDate = value
        End Set
    End Property
    Property NextTestDate() As Date
        Get
            Return m_dNextTestDate
        End Get
        Set(ByVal value As Date)
            m_dNextTestDate = value
        End Set
    End Property
    Property FailureLabel() As clsReportStructure
        Get
            Return m_objFailureLabel
        End Get
        Set(ByVal value As clsReportStructure)
            m_objFailureLabel = value
        End Set
    End Property
    Property BondingLabel() As clsReportStructure
        Get
            Return m_objBondingLabel
        End Get
        Set(ByVal value As clsReportStructure)
            m_objBondingLabel = value
        End Set
    End Property
    Property OverDue() As Boolean
        Get
            Return m_fOverdue
        End Get
        Set(ByVal value As Boolean)
            m_fOverdue = value
        End Set
    End Property

    Property TestDescription() As String
        Get
            Return m_strTestDescription
        End Get
        Set(ByVal value As String)
            m_strTestDescription = value
        End Set
    End Property
    Property LastTestID() As Long
        Get
            Return m_lngLastTestID
        End Get
        Set(ByVal value As Long)
            m_lngLastTestID = value
        End Set
    End Property
    Property LastTestPass() As Boolean
        Get
            Return m_fLastTestPass
        End Get
        Set(ByVal value As Boolean)
            m_fLastTestPass = value
        End Set
    End Property
    Property Frequency() As Long
        Get
            Return m_lngFrequency
        End Get
        Set(ByVal value As Long)
            m_lngFrequency = value
        End Set
    End Property
End Class
