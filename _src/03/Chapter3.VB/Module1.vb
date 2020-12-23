Imports System.Data.Entity

Module Module1

    Sub Main()
        Database.SetInitializer(New Initializer)
        AddPersonWithPhoneNumbers()
        'ForceDatabaseToRebuild()
    End Sub

    Private Sub AddPersonWithPhoneNumbers()
        Using context = New Context()
            Dim person As New Person With {
                    .LastName = "Doe",
                    .FirstName = "John",
                    .IsActive = True
            }
            person.Phones.Add(New Phone() With {.PhoneNumber = "123-446-7890"})
            person.Phones.Add(New Phone() With {.PhoneNumber = "123-446-7891"})
            context.People.Add(person)
            context.SaveChanges()

        End Using
    End Sub

    Private Sub ForceDatabaseToRebuild()
        Using context = New Context()
            Dim total = context.People.Count()
        End Using
    End Sub

End Module
