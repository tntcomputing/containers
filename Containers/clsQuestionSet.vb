Public Class clsQuestionSet
    Public Function GetQuestionSetWithMultipleInspections(ByVal TestSetID As String, ByVal InspectionNos As String, ByVal QuestionSetID As String) As Data.DataTable
        Dim dh As New DataHandling
        

        Return dh.GetDataTable("Select * from tblQuestion where TestSetID='" & TestSetID & "' and  InspectionNo in (" & InspectionNos & ") and QuestionSetID ='" & QuestionSetID & "' order by QuestionNo")



    End Function
End Class
