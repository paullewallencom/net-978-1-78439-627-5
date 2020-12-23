Namespace Models

    Public Class Company
        Public Sub New
            Persons = new HashSet(Of Person)
        End Sub
        
        Property CompanyId() As Integer
        Property CompanyName() As String
        Property DateAdded() As DateTime
        Property IsActive() As Boolean
        Overridable Property Persons() As ICollection(Of Person)
    End Class
End Namespace