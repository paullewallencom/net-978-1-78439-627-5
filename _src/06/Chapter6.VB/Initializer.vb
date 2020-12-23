Imports System.Data.Entity
Imports Chapter6.VB.Models

Public Class Initializer
    Inherits DropCreateDatabaseIfModelChanges(Of Context)

    Protected Overrides Sub Seed(ByVal context As Context)

        Dim type1 = context.PersonTypes.Add(New PersonType() With {.TypeName = "Friend"})
        Dim type2 = context.PersonTypes.Add(New PersonType() With {.TypeName = "Co-worker"})

        Dim company1 = context.Companies.Add(
            New Company() With {.CompanyName = "ABC", .DateAdded = DateTime.Today.AddDays(-10), .IsActive = True})

        Dim company2 = context.Companies.Add(
            New Company() With {.CompanyName = "DEF", .DateAdded = DateTime.Today.AddDays(-20), .IsActive = True})

        Dim person = New Person() With {
            .BirthDate = New DateTime(1980, 1, 2),
            .FirstName = "John",
            .HeightInFeet = 6.1D,
            .IsActive = True,
            .LastName = "Doe",
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
            .PersonType = type2
        }
        context.People.Add(person)
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-555-666-7777"})
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-888-999-3333"})

        company2.Persons.Add(person)

        context.SaveChanges()

        context.Database.ExecuteSqlCommand("DROP TABLE PersonView")
        context.Database.ExecuteSqlCommand(<![CDATA[
            CREATE VIEW [dbo].[PersonView]
            AS
            SELECT 
	            dbo.People.PersonId, 
	            dbo.People.FirstName, 
	            dbo.People.LastName, 
	            dbo.PersonTypes.TypeName
            FROM     
	            dbo.People 
            INNER JOIN dbo.PersonTypes 
	            ON dbo.People.PersonTypeId = dbo.PersonTypes.PersonTypeId]]>.Value())

        context.Database.ExecuteSqlCommand(<![CDATA[
            CREATE PROCEDURE dbo.UpdateCompanies
	            @dateAdded as DateTime,
	            @activeFlag as Bit
            AS
            BEGIN
	            UPDATE Companies
	            Set DateAdded = @dateAdded,
		            IsActive = @activeFlag
            END
            ]]>.Value())

        context.Database.ExecuteSqlCommand(<![CDATA[
            CREATE PROCEDURE [dbo].[SelectCompanies]
	            @dateAdded as DateTime
            AS
            BEGIN
	            SET NOCOUNT ON
	            SELECT CompanyId, CompanyName 
                FROM Companies
	            WHERE DateAdded > @dateAdded
            END
            ]]>.Value())

        

    End Sub
End Class
