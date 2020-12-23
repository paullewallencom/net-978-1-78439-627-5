Namespace CustomModels
    Public Class PersonInfo
        Property PersonId() As Integer
        Property PersonType() As String
        Property FirstName() As String
        Property LastName() As String
        Property Phones() As IEnumerable(Of String)
    End Class
End Namespace