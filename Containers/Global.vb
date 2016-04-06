Imports System.Collections.Specialized
Imports System.Data












Public Class clsSetup
    Public Shared SQLConnectionString As String

    Private Shared m_Settings As NameValueCollection
    
   

    <System.Runtime.InteropServices.DllImport("coredll.dll")> _
 Shared Function GetDeviceUniqueID(ByVal appdata As Byte(), ByVal cbApplictionData As Integer, ByVal dwDeviceIDVersion As Integer, ByVal deviceIDOuput As Byte(), ByRef pcbDeviceIDOutput As Integer) As Integer
    End Function

    Shared Function GetDeviceId(ByVal appData As String) As Byte()

        Dim appDataBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(appData)
        Dim outputSize As Integer = 20
        Dim output(19) As Byte

        Dim result As Integer = GetDeviceUniqueID(appDataBytes, appDataBytes.Length, 1, output, outputSize)

        Return output

    End Function
    
    
    
    
    
    
    
    Public Shared Sub Setup()
        getSettings()

        
        
    End Sub
    
    Public Shared Sub getSettings()
        Dim LenCodeBase As Integer = Len(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase.ToString)
        Dim lenAppName As Integer = Len(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name.ToString) + 4
        Dim strPath As String = System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase.Substring(0, LenCodeBase - lenAppName)
        Dim SettingsFile As String = strPath & "DataHandling.xml"
        Dim xdoc As New System.Xml.XmlDocument
        xdoc.Load(SettingsFile)
        Dim root As System.Xml.XmlElement = xdoc.DocumentElement
        Dim nodelist As System.Xml.XmlNodeList = root.ChildNodes.Item(0).ChildNodes

        m_Settings = New NameValueCollection

        m_Settings.Add("ConnectionString", nodelist.Item(0).Attributes("value").Value)
        SQLConnectionString = m_Settings("ConnectionString").ToString
       
        

    End Sub
End Class



