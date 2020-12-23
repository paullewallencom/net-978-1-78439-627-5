Imports System.Data.Entity
Imports Chapter4.VB.Models
Imports Enumerable = System.Linq.Enumerable

Module Module1

    Sub Main()
        'Database.SetInitializer(New Initializer())
        ' comment out the lone below to force database check code to run
        ' if you are running both VB and C# from the same solution, run C# project first to create the database.
        Database.SetInitializer(Of Context)(Nothing)
        'RunBasicQuery()
        'RunFilterQuery()
        'RunOrderByQuery()
        'RunRelatedFilterSortQuery()
        'ElementOperations()
        'Quantifiers()
        'LazyEagerLoading()
        'InsertData()
        'UpdateData()
        'NoTracking()
        'DeleteData()
        RunLocalQuery()
    End Sub



    Private Sub RunBasicQuery()
        Using context = New Context()
            Dim query = context.People
            Dim data = query.ToList()
        End Using
    End Sub

    Private Sub RunFilterQuery()
        Using context = New Context()
            Dim query = From person In context.People
                        Where
                            person.HeightInFeet >= 6 And
                            person.FirstName.Contains("J") And
                            person.IsActive
                        Select person

            Console.WriteLine(query.ToList().Count)

            Dim methodQuery = context.People _
                                .Where(Function(p) p.HeightInFeet >= 6 And
                                            p.FirstName.Contains("J") And
                                            p.IsActive)
            Console.WriteLine(methodQuery.ToList().Count)
            Prompt()
        End Using
    End Sub

    Private Sub RunOrderByQuery()
        Using context = New Context()
            Dim query = From person In context.People
                        Where person.IsActive
                        Order By person.LastName Descending, person.FirstName
                        Select person

            Console.WriteLine(query.ToList().Count)

            Dim methodQuery = context.People _
                                .Where(Function(p) p.IsActive) _
                                .OrderBy(Function(p) p.LastName) _
                                .ThenBy(Function(p) p.FirstName)

            Console.WriteLine(methodQuery.ToList().Count)
            Prompt()
        End Using
    End Sub

    Sub RunRelatedFilterSortQuery()
        Using context = New Context()
            Dim query = From person In context.People
                        Where person.IsActive And
                        person.Phones.Any(Function(ph) ph.PhoneNumber.StartsWith("1"))
                        Select person

            Console.WriteLine(query.ToList().Count)

            Dim methodQuery = context.People _
                                .Where(Function(p) p.IsActive And
                                p.Phones.Any(Function(ph) ph.PhoneNumber.StartsWith("1")))

            Console.WriteLine(methodQuery.ToList().Count)
            Prompt()
        End Using
    End Sub

    Private Sub ElementOperations()
        Using context = New Context()
            Dim query = From person In context.People
                        Where person.LastName = "Doe"
                        Select person

            Dim first = query.First()
            Console.WriteLine(first.LastName)

            Dim methodQuery = context.People _
                                .Where(Function(p) p.LastName = "Doe")
            first = methodQuery.First()
            Console.WriteLine(first.LastName)
            first = context.People.First(Function(p) p.LastName = "Doe")
            Console.WriteLine(first.LastName)
            Prompt()
        End Using
    End Sub

    Private Sub Quantifiers()
        Using context = New Context()
            Dim hasDoes = (From person In context.People
                        Where person.LastName = "Doe"
                        Select person).Any()

            Console.WriteLine(hasDoes)
            hasDoes = context.People.Any(Function(p) p.LastName = "Doe")
            Console.WriteLine(hasDoes)

            Dim allHaveJ = context.People.All(Function(p) p.FirstName.Contains("J"))
            Console.WriteLine(allHaveJ)
            Prompt()
        End Using
    End Sub

    Private Sub LazyEagerLoading()
        Using context = New Context()

            Dim query = From person In context.People
                       Select person

            For Each person As Person In query.ToList()
                Console.WriteLine(person.LastName)
                For Each phone As Phone In person.Phones
                    Console.WriteLine(phone.PhoneNumber)
                Next
            Next
        End Using

        Using context = New Context()

            Dim query = From person In context.People.Include(Function(p) p.Phones)
                        Select person

            For Each person As Person In query
                Console.WriteLine(person.LastName)
                For Each phone As Phone In person.Phones
                    Console.WriteLine(phone.PhoneNumber)
                Next
            Next
            Prompt()
        End Using
    End Sub

    Private Sub InsertData()
        Dim person = New Person() With {
                .BirthDate = New DateTime(1980, 1, 2),
                .FirstName = "John",
                .HeightInFeet = 6.1D,
                .IsActive = True,
                .LastName = "Doe",
                .MiddleName = "M"
        }
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-222-333-4444"})
        person.Phones.Add(New Phone() With {.PhoneNumber = "1-333-4444-5555"})
        Using context = New Context()
            context.People.Add(person)
            context.SaveChanges()
        End Using


        Dim person2 = New Person() With {
                .BirthDate = New DateTime(1980, 1, 2),
                .FirstName = "James",
                .HeightInFeet = 6.1D,
                .IsActive = True,
                .LastName = "Jones",
                .MiddleName = "M"
        }
        person2.Phones.Add(New Phone() With {.PhoneNumber = "1-222-333-4444"})
        person2.Phones.Add(New Phone() With {.PhoneNumber = "1-333-4444-5555"})
        Using context = New Context()
            context.Entry(person2).State = EntityState.Added
            context.SaveChanges()
        End Using
        Prompt()
    End Sub

    Private Sub UpdateData()
        Using context = New Context()
            Dim person = context.People.Find(1)
            person.FirstName = "New Name VB"
            context.SaveChanges()
        End Using


        Dim person2 = New Person() With {
            .PersonId = 1,
            .BirthDate = New DateTime(1980, 1, 2),
            .FirstName = "Jonathan",
            .HeightInFeet = 6.1D,
            .IsActive = True,
            .LastName = "Smith",
            .MiddleName = "M"
        }
        person2.Phones.Add(New Phone() With {.PhoneNumber = "updated 1", .PhoneId = 1, .PersonId = 1})
        person2.Phones.Add(New Phone() With {.PhoneNumber = "updated 2", .PhoneId = 2, .PersonId = 1})
        Using context = New Context()
            context.Entry(person2).State = EntityState.Modified
            For Each phone In person2.Phones
                context.Entry(phone).State = EntityState.Modified
            Next
            context.SaveChanges()
        End Using

        Dim person3 = New Person() With {
            .PersonId = 1,
            .BirthDate = New DateTime(1980, 1, 2),
            .FirstName = "Jonathan",
            .HeightInFeet = 6.1D,
            .IsActive = True,
            .LastName = "Smith",
            .MiddleName = "M"
        }
        Using context = New Context()
            context.People.Attach(person3)
            context.Entry(person3).State = EntityState.Unchanged
            person3.LastName = "Updated2"
            context.SaveChanges()
        End Using

        Prompt()
    End Sub

    Private Sub DeleteData()
        Dim person As Person = AddPerson()
        Dim personId = person.PersonId
        Console.WriteLine(personId)

        Using context = New Context()
            Dim toDelete = context.People.Find(personId)
            toDelete.Phones.ToList().ForEach(Function(phone) context.Phones.Remove(phone))
            context.People.Remove(toDelete)
            context.SaveChanges()
        End Using

        person = AddPerson()
        personId = person.PersonId
        Dim phoneId1 = person.Phones(0).PhoneId
        Dim phoneId2 = person.Phones(1).PhoneId
        Dim toDeleteByState = New Person With {.PersonId = personId}
        toDeleteByState.Phones.Add(New Phone With {.PhoneId = phoneId1, .PersonId = personId})
        toDeleteByState.Phones.Add(New Phone With {.PhoneId = phoneId2, .PersonId = personId})

        Using context = New Context()
            context.People.Attach(toDeleteByState)
            For Each phone In toDeleteByState.Phones.ToList()
                context.Entry(phone).State = EntityState.Deleted
            Next
            context.Entry(toDeleteByState).State = EntityState.Deleted
            context.SaveChanges()
        End Using

        Prompt()
    End Sub

    Private Function AddPerson() As Person

        Dim person = New Person() With { _
                .BirthDate = New DateTime(1980, 1, 2), _
                .FirstName = "John", _
                .HeightInFeet = 6.1D, _
                .IsActive = True, _
                .LastName = "Delete", _
                .MiddleName = "M" _
                }

        person.Phones.Add(New Phone() With { _
                             .PhoneNumber = "1-222-333-4444" _
                             })
        person.Phones.Add(New Phone() With { _
                             .PhoneNumber = "1-333-4444-5555" _
                             })

        Using context = New Context()
            context.People.Add(person)
            context.SaveChanges()
        End Using
        Return person
    End Function

    Private Sub NoTracking()
        Using context = New Context()
            Dim query = From person In context.People.Include(Function(p) p.Phones).AsNoTracking()
                        Select person
            For Each person As Person In query
                Console.WriteLine(person.LastName)
                For Each phone As Phone In person.Phones
                    Console.WriteLine(phone.PhoneNumber)
                Next
            Next
            Prompt()
        End Using
    End Sub

    Private Sub RunLocalQuery()
        Using context = New Context()
            Dim query = context.People.ToList()
            Dim localQuery = context.People.Local.Where(Function(p) p.LastName.Contains("o")).ToList()
            Dim findQuery = context.People.Find(1)
            Console.WriteLine(query.Count)
            Console.WriteLine(localQuery.Count)
            For Each dbEntityEntry In context.ChangeTracker.Entries(Of Person)()
                Console.WriteLine(dbEntityEntry.State)
                Console.WriteLine(dbEntityEntry.Entity.LastName)
            Next
            Prompt()
        End Using
    End Sub

    Private Sub Prompt()
        Console.WriteLine("Press any key to exit")
        Console.ReadKey()
    End Sub
End Module
