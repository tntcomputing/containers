Imports SQL = System.Data.SqlClient
Imports System.Data
Public Class DataHandling
    Public m_cn As SQL.SqlConnection
    Public ReadOnly Property GetConnection() As SQL.SqlConnection
        Get
            CreateConnection()
            Return (Me.m_cn)
        End Get

    End Property
   
    Public Function GetSQLString(ByVal ListName As String) As String
        Return Me.ExecuteStrScalar("Select StrSQL from tblSQL where SQLID=?", ListName)
    End Function
    Private Sub CreateConnection()
        m_cn = New SQL.SqlConnection
        m_cn.ConnectionString = clsSetup.SQLConnectionString
    End Sub
   
    Public Function GetDataSet(ByVal strsql As String) As System.Data.DataSet
        Dim DA As SQL.SqlDataAdapter
        Try
            DA = Me.GetDataAdapter(strsql)
        Catch ex As Exception
            Throw New Exception("Get DataSet" & vbCrLf & ex.ToString)
        End Try
        Dim DS As New DataSet
        Try
            DA.Fill(DS)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return DS
    End Function
    Public Function GetDataTable(ByVal strsql As String) As System.Data.DataTable
        Dim DA As SQL.SqlDataAdapter

        Try
            DA = Me.GetDataAdapter(strsql)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Dim DT As New DataTable
        Try
            DA.Fill(DT)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
            m_cn.Close()
            m_cn = Nothing
        End Try
        m_cn.Close()
        m_cn = Nothing
        Return DT
    End Function
    Public Function GetDataSet(ByVal strsql As String, ByVal ParamArray Param() As String) As System.Data.DataSet
        Dim DA As SQL.SqlDataAdapter
        Try
            DA = Me.GetDataAdapter(strsql, Param)
        Catch ex As Exception
            Throw New Exception("Get DataSet" & vbCrLf & ex.ToString)
        End Try

        Dim DS As New DataSet
        Try
            DA.Fill(DS)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return DS
    End Function
    Public Function GetDataTable(ByVal strsql As String, ByVal ParamArray Param() As String) As System.Data.DataTable
        Dim DA As SQL.SqlDataAdapter
        Try
            DA = Me.GetDataAdapter(strsql, Param)
        Catch ex As Exception
            Throw New Exception("Get DataSet" & vbCrLf & ex.ToString)
        End Try

        Dim DT As New DataTable
        Try
            DA.Fill(DT)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
            m_cn.Close()
            m_cn = Nothing
        End Try
        m_cn.Close()
        m_cn = Nothing
        Return DT
    End Function
    Public Function GetDataReader(ByVal strsql As String, ByVal ParamArray Param() As String) As SQL.SqlDataReader
        CreateConnection()
        Dim DR As SQL.SqlDataReader
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        cmd.CommandType = CommandType.Text
        Dim I As Integer

        For I = 0 To UBound(Param)
            Dim P As New SQL.SqlParameter("P" & I, Param(I))
            cmd.Parameters.Add(P)

        Next
        Try
            m_cn.Open()
            DR = cmd.ExecuteReader

        Catch ex As Exception
            Throw New Exception("Get DataReader" & vbCrLf & ex.ToString)
        End Try


        Return DR
    End Function
    Public Function GetDataAdapter(ByVal strsql As String, ByVal ParamArray Param() As String) As SQL.SqlDataAdapter
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        Dim I As Integer
        For I = 0 To UBound(Param)

            Dim P As New SQL.SqlParameter("P" & I, Param(I))
            cmd.Parameters.Add(P)
        Next
        Dim DA As New SQL.SqlDataAdapter(cmd)
        Try
            m_cn.Open()
        Catch ex As Exception
            Throw New Exception("Get DataAdapter" & vbCrLf & ex.ToString)
        End Try
        Return DA
    End Function
    Public Function GetDataAdapter(ByVal strsql As String) As SQL.SqlDataAdapter
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)

        Dim DA As New SQL.SqlDataAdapter(cmd)
        Try
            m_cn.Open()
        Catch ex As Exception
            Throw New Exception("Get DataAdapter" & vbCrLf & ex.ToString)
        End Try
        Return DA
    End Function
    Public Sub FillDataSet(ByRef DS As DataSet, ByVal Adapter As SQL.SqlDataAdapter, ByVal TableName As String)

        Try
            Adapter.Fill(DS, TableName)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try


    End Sub
    Public Function DoesTableExist(ByVal strTableName As String) As Boolean
        Dim strSql As String
        strSql = "SELECT Count(table_name) FROM Information_schema.tables WHERE Table_name = '" & strTableName & "'"
        Dim RetVal As Integer
        RetVal = ExecuteIntScalar(strSql)
        If RetVal = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
   Private Sub SetDataBaseStructure()

        Dim DT As DataTable = Me.GetDataTable("Select tablename from tbltableupload")
        Dim R As DataRow
        For Each R In DT.Rows
            Me.ExecuteNonQueryBackup(Me.GetSQLString("CREATEBACKUP" & CType(R.Item("tablename"), String).ToUpper))
        Next

    End Sub
    Public Function ExecuteNonQueryBackup(ByVal strsql As String) As Boolean
        Dim cn As New SQL.SqlConnection

        Dim cmd As New SQL.SqlCommand(strsql, cn)
        cn.Open()
        Try
            cmd.ExecuteNonQuery()
            cn.Close()
            Return True
        Catch ex As Exception
            cn.Close()
            Return False
        End Try

    End Function
    
    Public Function ExecuteNonQuery(ByVal strSQL As String) As Boolean
        CreateConnection()
        Dim cmd As New SQL.SqlCommand(strSQL, m_cn)
        m_cn.Open()
        Try
            cmd.ExecuteNonQuery()
            m_cn.Close()
            Return True
        Catch ex As Exception
            m_cn.Close()
            Return False
        End Try
    End Function

    Public Function ExecuteNonQuery(ByVal strSQL As String, ByVal ParamArray Param() As String) As Boolean
        CreateConnection()
        Dim cmd As New SQL.SqlCommand(strSQL, m_cn)
        cmd.CommandType = CommandType.Text
        Dim I As Integer

        For I = 0 To UBound(Param)
            Dim P As New SQL.SqlParameter("P" & I, Param(I))
            cmd.Parameters.Add(P)
        Next
        m_cn.Open()

        Try
            cmd.ExecuteNonQuery()
            m_cn.Close()
            Return True
        Catch ex As Exception
            m_cn.Close()
            Return False
        End Try
    End Function
    Public Function ExecuteIntScalar(ByVal strsql As String, ByVal ParamArray Param() As String) As Integer
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        cmd.CommandType = CommandType.Text
        Dim I As Integer

        For I = 0 To UBound(Param)
            Dim P As New SQL.SqlParameter("P" & I, Param(I))
            cmd.Parameters.Add(P)
        Next
        m_cn.Open()
        Dim retVal As Integer
        Try
            retVal = CType(cmd.ExecuteScalar, Integer)
        Catch ex As Exception
            retVal = 0
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return retVal
    End Function
    Public Function ExecuteIntScalar(ByVal strsql As String) As Integer
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        m_cn.Open()
        Dim retVal As Integer
        Try
            retVal = CType(cmd.ExecuteScalar, Integer)
        Catch ex As Exception
            retVal = 0
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return retVal
    End Function
    Public Function ExecuteStrScalar(ByVal strsql As String) As String
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        m_cn.Open()
        Dim retVal As String
        Try
            retVal = cmd.ExecuteScalar.ToString
        Catch ex As Exception
            retVal = ""
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return retVal
    End Function
    Public Function ExecuteStrScalar(ByVal strsql As String, ByVal ParamArray Param() As String) As String
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        cmd.CommandType = CommandType.Text
        Dim I As Integer

        For I = 0 To UBound(Param)
            Dim P As New SQL.SqlParameter("P" & I, Param(I))
            cmd.Parameters.Add(P)

        Next
        m_cn.Open()
        Dim retVal As String
        Try
            retVal = cmd.ExecuteScalar.ToString
        Catch ex As Exception
            retVal = ""
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return retVal
    End Function
    Public Function ExecuteDateTimeScalar(ByVal strsql As String) As DateTime
        CreateConnection()
        Dim cmd As SQL.SqlCommand
        cmd = New SQL.SqlCommand(strsql, m_cn)
        m_cn.Open()
        Dim retVal As DateTime
        Try
            retVal = CType(cmd.ExecuteScalar, DateTime)
        Catch ex As Exception
            retVal = Nothing
        End Try

        m_cn.Close()
        m_cn = Nothing
        Return retVal
    End Function
    Public Sub New()
        If clsSetup.SQLConnectionString = "" Then
            clsSetup.Setup()
        End If
    End Sub
    Public Function CleanText(ByVal strDirtyText As String, ByVal bQuoteAdder As Boolean, _
                          ByVal bCommaRemover As Boolean) As String

        Dim strOut As String
        strOut = strDirtyText

        If bQuoteAdder Then
            strOut = QuoteAdder(strDirtyText)
        End If

        If bCommaRemover Then
            strOut = CommaRemover(strOut)
        End If
        Return strOut
        
    End Function
    Private Function QuoteAdder(ByVal istrin As String) As String

        Dim iPos As Integer
        Dim iPos2 As Integer
        Dim strIN As String

        
        strIN = istrin

        'If there is any "'", double them up so that a syntax error does not happen in sql
        iPos = InStr(strIN, "'")

        Do While (iPos <> 0)

            strIN = Mid(strIN, 1, iPos - 1) + "''" + Mid(strIN, iPos + 1)
            iPos2 = iPos
            iPos = InStr(iPos2 + 2, strIN, "'")

        Loop

        Return strIN

        
    End Function
    Private Function CommaRemover(ByVal istrin As String) As String

        Dim iPos As Integer
        Dim iPos2 As Integer
        Dim strIN As String

        
        strIN = istrin

        'If there is any ",", Remove them
        iPos = InStr(strIN, Chr(44))

        Do While (iPos <> 0)

            strIN = Mid(strIN, 1, iPos - 1) & Mid(strIN, iPos + 1)
            iPos2 = iPos
            iPos = InStr(iPos2 + 1, strIN, Chr(44))

        Loop

        Return strIN

        
    End Function
    Public Function RemoveSpaces(ByVal istrin As String) As String

        
        Return Replace(istrin, " ", "", 1, , vbBinaryCompare)

        
    End Function
    Public Function FormatDate(ByVal dte As Date) As String

        
        Return "'" & Year(dte) & "-" & Month(dte) & "-" & DateAndTime.Day(dte) & "'"
        
        'Return "'" & Format(dte, "dd/mmm/yyyy") & "'"

            
       

    End Function

End Class
