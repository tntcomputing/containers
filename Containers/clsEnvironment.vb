Public Class clsEnvironment
    Private Const MODULENAME = "clsEnvironment"
    Private strSQL As String
    Dim DH As DataHandling
    Public Sub New()
        DH = New DataHandling
    End Sub

    Public Sub WriteEnvironmentSetting(ByVal strCat As String, ByVal strSubCat As String, ByVal lngFieldNo As Long, ByVal strValue As String)


        strSQL = "SELECT "
        strSQL = strSQL & "Field" & Trim(lngFieldNo) & " "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblEnvironment "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "CatType = '" & strCat & "' "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "CatSubType = '" & strSubCat & "' "

        Dim dt As Data.DataTable
        dt = DH.GetDataTable(strSQL)
        
        If dt.Rows.Count > 0 Then

            strSQL = "UPDATE "
            strSQL = strSQL & "tblEnvironment "
            strSQL = strSQL & "SET "
            strSQL = strSQL & "Field" & Trim(lngFieldNo) & " = '" & DH.CleanText(strValue, True, False) & "' "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "CatType = '" & strCat & "' "
            strSQL = strSQL & "AND "
            strSQL = strSQL & "CatSubType = '" & strSubCat & "' "

            'Call objData.ExecuteSQL(strSql)

            DH.ExecuteNonQuery(strSQL)

        Else

            strSQL = "INSERT INTO "
            strSQL = strSQL & "tblEnvironment "
            strSQL = strSQL & "("
            strSQL = strSQL & "CatType, "
            strSQL = strSQL & "CatSubType, "
            strSQL = strSQL & "Field" & Trim(lngFieldNo) & " "
            strSQL = strSQL & ") "
            strSQL = strSQL & "Values("
            strSQL = strSQL & "'" & strCat & "', "
            strSQL = strSQL & "'" & strSubCat & "', "
            strSQL = strSQL & "'" & DH.CleanText(strValue, True, False) & "' "
            strSQL = strSQL & ") "

            DH.ExecuteNonQuery(strSQL)

        End If


    End Sub
    Public Function ReturnEnvironmentSettingBool(ByVal strCat As String, ByVal strSubCat As String, ByVal strFieldNo As Long) As Boolean

        If UCase(ReturnEnvironmentSetting(strCat, strSubCat, 1)) = "YES" Then
            ReturnEnvironmentSettingBool = True
        Else
            ReturnEnvironmentSettingBool = False
        End If
    End Function
    Public Function ReturnEnvironmentSetting(ByVal strCat As String, ByVal strSubCat As String, ByVal strFieldNo As Long) As String

        
        strSQL = "SELECT "
        strSQL = strSQL & "Field" & Trim(strFieldNo) & " "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblEnvironment "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "CatType = '" & strCat & "' "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "CatSubType = '" & strSubCat & "' "
        Dim dt As Data.DataTable = DH.GetDataTable(strSQL)
       
        If dt.Rows.Count > 0 Then
            If IsDBNull(dt.Rows(0).Item("Field" & Trim(strFieldNo))) Then
                ReturnEnvironmentSetting = ""
            Else
                ReturnEnvironmentSetting = dt.Rows(0).Item("Field" & Trim(strFieldNo))
            End If
        Else
            ReturnEnvironmentSetting = ""
        End If



        
    End Function
    Public Function ReturnEnvironmentSettingDataTable(ByVal strCat As String, ByVal strSubCat As String) As Data.DataTable


        strSQL = "SELECT "
        strSQL = strSQL & "Field1, "
        strSQL = strSQL & "Field2, "
        strSQL = strSQL & "Field3, "
        strSQL = strSQL & "Field4, "
        strSQL = strSQL & "Field5, "
        strSQL = strSQL & "Field6, "
        strSQL = strSQL & "Field7, "
        strSQL = strSQL & "Field8, "
        strSQL = strSQL & "Field9, "
        strSQL = strSQL & "Field10 "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "tblEnvironment "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "CatType = '" & strCat & "' "
        strSQL = strSQL & "AND "
        strSQL = strSQL & "CatSubType = '" & strSubCat & "' "
        Return DH.GetDataTable(strSQL)

    End Function
    Public Function ReturnServerDate() As Date

        
        strSQL = "SELECT getdate() as Today from tblEnvironment "
        Dim DT As Data.DataTable = DH.GetDataTable(strSQL)
        
        If DT.Rows.Count = 1 Then
            Return DT.Rows(0)(0)
        Else
            Return Nothing
        End If


    End Function

End Class
