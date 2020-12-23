Namespace Models

    Public Class PersonType
        Property PersonTypeId() As Integer
        Property TypeName() As String
        Property Persons() As ICollection(Of Person)
    End Class
End NameSpace