Imports System.Data.Entity
Imports Chapter4.VB.Models

Public Class Initializer
    Inherits DropCreateDatabaseIfModelChanges(Of Context)

    Protected Overrides Sub Seed(ByVal context As Context)

        Dim type1 = context.PersonTypes.Add(New PersonType() With {.TypeName = "Friend"})
        Dim type2 = context.PersonTypes.Add(New PersonType() With {.TypeName = "Co-worker"})

        Dim company1 = context.Companies.Add(New Company() With {.CompanyName = "ABC"})
        Dim company2 = context.Companies.Add(New Company() With {.CompanyName = "DEF"})

        Dim person = New Person() With {
             .BirthDate = New DateTime(1980, 1, 2),
             .FirstName = "John",
             .HeightInFeet = 6.1D,
             .IsActive = True,
             .LastName = "Doe",
             .MiddleName = "M",
             .PersonType = type1
        }
        context.People.Add(person)
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-222-333-4444"})
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-333-4444-5555"})
        company1.Persons.Add(person)

        person = New Person() With {
             .BirthDate = New DateTime(1970, 1, 2),
             .FirstName = "John",
             .HeightInFeet = 5.6D,
             .IsActive = True,
             .LastName = "Johnson",
             .MiddleName = "J",
             .PersonType = type2
        }
        context.People.Add(person)
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-555-666-7777"})
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-888-999-3333"})

        company1.Persons.Add(person)

        person = New Person() With {
             .BirthDate = New DateTime(1970, 1, 2),
             .FirstName = "Jean",
             .HeightInFeet = 5.2D,
             .IsActive = True,
             .LastName = "Abrams",
             .MiddleName = "K",
             .PersonType = type2
        }
        context.People.Add(person)
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-555-666-7777"})
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-888-999-3333"})

        company2.Persons.Add(person)
        context.SaveChanges()

    End Sub
End Class
