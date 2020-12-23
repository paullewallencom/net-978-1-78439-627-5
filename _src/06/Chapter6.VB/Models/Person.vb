

Namespace Models
    Public Class Person
        Public Sub New()
            Phones = New HashSet(Of Phone)
            Companies = New HashSet(Of Company)
        End Sub

        Property PersonId() As Integer
        Property PersonTypeId() As Integer?
        Overridable Property PersonType() As PersonType
        Property FirstName() As String
        Property LastName() As String
        Property BirthDate() As DateTime?
        Property HeightInFeet() As Decimal
        Property RowVersion() As Byte()
        Property IsActive() As Boolean
        Overridable Property Phones() As ICollection(Of Phone)
        Overridable Property Companies() As ICollection(Of Company)
    End Class

End Namespace