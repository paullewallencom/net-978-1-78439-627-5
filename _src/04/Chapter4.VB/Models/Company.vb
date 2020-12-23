Namespace Models
    Public Class Company
        Public Sub New()
            Persons = New HashSet(Of Person)
        End Sub

        Property CompanyId() As Integer
        Property CompanyName() As String
        Overridable Property Persons() As ICollection(Of Person)
    End Class
End Namespace