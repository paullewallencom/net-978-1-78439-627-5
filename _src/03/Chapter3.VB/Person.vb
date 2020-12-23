Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class Person
Public Sub New
    Phones = new HashSet(Of Phone)
End Sub
    
    Property PersonId() As Integer
    Property FirstName() As String
    Property LastName() As String
    Property MiddleName() As String
    Property BirthDate() As DateTime?
    Property HeightInFeet() As Decimal
    Property Photo() As Byte()
    Property IsActive() As Boolean
    Property NumberOfCars() As Integer
    Overridable Property Phones() As ICollection(Of Phone)
End Class

'Public Class Person
'    Property PersonId() As Integer

'    <MaxLength(30, ErrorMessage:="First name cannot be longer than 30")>
'    Property FirstName() As String

'    <MaxLength(30)>
'    Property LastName() As String

'    <StringLength(1, MinimumLength:=1)>
'    <Column(TypeName:="char")>
'    Property MiddleName() As String

'End Class
