Namespace Models
    Public Class Company
        Public Sub New()
            Persons = New HashSet(Of Person)
            Address = New Address()
        End Sub

        Property CompanyId() As Integer
        Property CompanyName() As String
        Property Address() As Address
        Overridable Property Persons() As ICollection(Of Person)
    End Class
End Namespace