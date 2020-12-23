Namespace Models

    Public Class Person
        Public Sub New()
            Phones = New HashSet(Of Phone)
            Companies = New HashSet(Of Company)
            Address = New Address()
        End Sub

        Property PersonId() As Integer
        Property PersonTypeId() As Integer?
        Overridable Property PersonType() As PersonType
        Property FirstName() As String
        Property LastName() As String
        Property MiddleName() As String
        Public Property FullName() As String
            Get

                Return String.Format("{0} {1}", FirstName, LastName)
            End Get
            Set(value As String)
                Dim names = value.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                FirstName = names(0)
                LastName = names(1)
            End Set
        End Property
        Property BirthDate() As DateTime?
        Property HeightInFeet() As Decimal
        Property IsActive() As Boolean
        Property PersonState() As PersonState
        Property Address() As Address
        Property Photo() As Byte()
        Property FamilyPicture() As Byte()
        Overridable Property Phones() As ICollection(Of Phone)
        Overridable Property Companies() As ICollection(Of Company)
    End Class
End Namespace