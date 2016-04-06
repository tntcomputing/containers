Public Class clsReportStructure
    Private Const MODULENAME = "ReportStructure"

    Private m_strReportName As String
    Private m_enumShowPrintDialog As enumPrintDialog
    Private m_enumPrintReport As enumPrintReport
    Public Enum enumPrintReport
        vbPrintReport = 1
        vbPrintPreviewReport = 2
    End Enum

    Public Enum enumPrintDialog
        vbShowPrintDialog = -1
        vbNoPrintDialog = 0
    End Enum
    Property ReportName() As String
        Get
            Return m_strReportName
        End Get
        Set(ByVal value As String)
            m_strReportName = value
        End Set
    End Property

    Property ShowPrintDialog() As enumPrintDialog
        Get
            Return m_enumShowPrintDialog
        End Get
        Set(ByVal value As enumPrintDialog)
            m_enumShowPrintDialog = value
        End Set
    End Property
    Property PrintReport() As enumPrintReport
        Get
            Return m_enumPrintReport
        End Get
        Set(ByVal value As enumPrintReport)
            m_enumPrintReport = value
        End Set
    End Property


End Class
