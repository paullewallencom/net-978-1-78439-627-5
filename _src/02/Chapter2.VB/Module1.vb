Imports System.Data.Entity

Module Module1

    Sub Main()
        Database.SetInitializer(New Initializer)
        'VerifyDatabaseExists()
        'InsertNewRow()
        'QueryData()
        'UpdateData()
        'DeleteData()
        QueryCompanies()
    End Sub

    Private Sub VerifyDatabaseExists()
        Using context = New Context()
            context.Database.CreateIfNotExists()
        End Using
    End Sub

    Private Sub InsertNewRow()
        Using context = New Context()
            Dim person = New Person With {
                .FirstName = "John",
                .LastName = "Doe"
            }
            context.People.Add(person)
            context.SaveChanges()
        End Using
    End Sub

    Private Sub QueryData()
        Using context = New Context()
            Dim savedPeople = context.People
            For Each person In savedPeople
                Console.WriteLine("Last name:{0},first name:{1},id {2}",
                                  person.LastName, person.FirstName, person.PersonId)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Private Sub QueryCompanies()
        Using context = New Context()
            For Each company In context.Companies
                Console.WriteLine("Name:{0},id {1}",
                                  company.Name, company.CompanyId)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Private Sub UpdateData()
        Using context = New Context()
            Dim savedPeople = context.People
            If savedPeople.Any() Then
                With savedPeople.First()
                    .FirstName = "Johnny"
                    .LastName = "Benson"
                End With
                context.SaveChanges()
            End If
        End Using
        QueryData()
    End Sub

    Private Sub DeleteData()
        Using context = New Context()
            Dim personId As Integer = 4
            Dim person = context.People.Find(personId)
            If person IsNot Nothing Then
                context.People.Remove(person)
                context.SaveChanges()

            End If
        End Using
        QueryData()
    End Sub

End Module
