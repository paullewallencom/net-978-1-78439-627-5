Imports System.Data.Entity
Imports Chapter5.VB.Models
Imports Chapter5.VB.CustomModels
Imports System.Net.NetworkInformation

Module Module1

    Sub Main()
        'Database.SetInitializer(New Initializer())
        ' comment out the lone below to force database check code to run
        ' if you are running both VB and C# from the same solution, run C# project first to create the database.
        Database.SetInitializer(Of Context)(Nothing)
        'RunProjectionQiery()
        'Aggregations()
        'MultiStepQuery()
        'Paging()
        'Joins()
        'Groups()
        'LeftOuterJoins()
        'SelectMany()
        SetOperations()
    End Sub

    Sub SetOperations()
        Using context = New Context()
            Dim unqueQuery = context.People _
                    .Select(Function(p) p.PersonType.TypeName) _
                    .Distinct()
            For Each oneType In unqueQuery
                Console.WriteLine(oneType)
            Next


            Dim unionQuery = context.People _
                    .Select(Function(p) New With
                    {
                        .Name = p.LastName + " " + p.FirstName,
                        .RowType = "Person"
                    }) _
                    .Union(context.Companies.Select(Function(c) New With
                    {
                        .Name = c.CompanyName,
                        .RowType = "Company"
                    })) _
                    .OrderBy(Function(result) result.RowType) _
                    .ThenBy(Function(result) result.Name)
            For Each name In unionQuery
                Console.WriteLine("{0} {1}",
                        name.Name, name.RowType)
            Next

            Dim intersectQuery = context.People _
                    .Select(Function(p) New With
                    {
                        .Name = p.LastName + " " + p.FirstName,
                        .RowType = "Person"
                    }) _
                    .Intersect(context.Companies.Select(Function(c) New With
                    {
                        .Name = c.CompanyName,
                        .RowType = "Company"
                    })) _
                    .OrderBy(Function(result) result.RowType) _
                    .ThenBy(Function(result) result.Name)
            For Each name In intersectQuery
                Console.WriteLine("{0} {1}",
                        name.Name, name.RowType)
            Next


            Dim exceptQuery = context.People _
                    .Select(Function(p) New With
                    {
                        .Name = p.LastName + " " + p.FirstName,
                        .RowType = "Person"
                    }) _
                    .Except(context.Companies.Select(Function(c) New With
                    {
                        .Name = c.CompanyName,
                        .RowType = "Company"
                    })) _
                    .OrderBy(Function(result) result.RowType) _
                    .ThenBy(Function(result) result.Name)
            For Each name In exceptQuery
                Console.WriteLine("{0} {1}",
                        name.Name, name.RowType)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Sub SelectMany()
        Using context = New Context()
            Dim query =
                    From onePerson In context.People
                    From onePhone In onePerson.Phones
                    Order By onePerson.LastName, onePhone.PhoneNumber
                    Select New With
                    {
                        onePerson.LastName,
                        onePerson.FirstName,
                        onePhone.PhoneNumber
                    }
            For Each person In query
                Console.WriteLine("{0} {1} {2}",
                    person.FirstName, person.LastName, person.PhoneNumber)
            Next

            Dim methodQuery =
                    context.People _
                        .SelectMany( _
                            Function(person) person.Phones, _
                            Function(person, phone) New With
                        {
                            person.LastName,
                            person.FirstName,
                            phone.PhoneNumber
                        }) _
                        .OrderBy(Function(p) p.LastName) _
                        .ThenBy(Function(p) p.PhoneNumber)
            For Each person In methodQuery
                Console.WriteLine("{0} {1} {2}",
                    person.FirstName, person.LastName, person.PhoneNumber)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Sub LeftOuterJoins()
        Using context = New Context()

            Dim query =
                From person In context.People
                Group Join personType In context.PersonTypes
                    On person.PersonTypeId Equals personType.PersonTypeId Into finalGroup = Group
                From groupedData In finalGroup.DefaultIfEmpty()
                Select New With
                {
                    .LastName = person.LastName,
                    .FirstName = person.FirstName,
                    .TypeName = If(groupedData.TypeName Is Nothing, "Unknown", groupedData.TypeName)
                }

            For Each person In query
                Console.WriteLine("{0} {1} {2}",
                    person.FirstName, person.LastName, person.TypeName)
            Next

            Dim methodQuery =
                context.People _
                .GroupJoin(
                    context.PersonTypes,
                    Function(person) person.PersonTypeId,
                    Function(personType) personType.PersonTypeId,
                    Function(person, type) New With
                    {
                        .Person = person,
                        .PersonType = type
                    }) _
                .SelectMany(Function(groupedData) _
                    groupedData.PersonType.DefaultIfEmpty(),
                    Function(group, personType) New With
                    {
                        .LastName = group.Person.LastName,
                        .FirstName = group.Person.FirstName,
                        .TypeName = If(personType.TypeName Is Nothing, "Unknown", personType.TypeName)
                    })
            For Each person In methodQuery
                Console.WriteLine("{0} {1} {2}",
                    person.FirstName, person.LastName, person.TypeName)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Sub Groups()
        Using context = New Context()

            Dim query =
                    From onePerson In context.People
                    Group onePerson By personWithBirthday =
                        New With {.Month = onePerson.BirthDate.Value.Month}
                        Into monthGroup = Group
                    Select New With
                        {
                            .Count = monthGroup.Count(),
                            .Month = personWithBirthday.Month
                        }
            For Each group In query
                Console.WriteLine("{0} {1}",
                       group.Month, group.Count)
            Next


            Dim methodQuery =
                context.People _
                .GroupBy(Function(person) New With {.Month = person.BirthDate.Value.Month}, _
                            Function(monthGroup) monthGroup) _
                .Select(Function(monthGroup) New With
                    {
                        .Month = monthGroup.Key.Month,
                        .Count = monthGroup.Count()
                    })

            For Each group In methodQuery
                Console.WriteLine("{0} {1}",
                       group.Month, group.Count)
            Next

        End Using
        Console.ReadKey()
    End Sub

    Sub Joins()
        Using context = New Context()
            Dim people =
                From person In context.People
                Join personType In context.PersonTypes
                    On person.PersonTypeId Equals personType.PersonTypeId
                Select New With
                {
                    person.LastName,
                    person.FirstName,
                    personType.TypeName
                }
            For Each person In people
                Console.WriteLine("{0} {1} {2}",
                    person.FirstName, person.LastName, person.TypeName)
            Next

            people = context.People _
                .Join(context.PersonTypes, _
                        Function(person) person.PersonTypeId, _
                        Function(personType) personType.PersonTypeId, _
                        Function(person, personType) New With {
                            .Person = person,
                            .PersonType = personType}) _
            .Select(Function(p) New With {
                    p.Person.LastName,
                    p.Person.FirstName,
                    p.PersonType.TypeName
                })

            For Each person In people
                Console.WriteLine("{0} {1} {2}",
                    person.FirstName, person.LastName, person.TypeName)
            Next

        End Using

        Console.ReadKey()
    End Sub

    Sub Paging()
        Using context = New Context()
            Dim criteria = New With {.PageNumber = 1, .PageSize = 2}

            Dim query = context.People _
                        .OrderBy(Function(p) p.LastName) _
                        .Skip((criteria.PageNumber - 1) * criteria.PageSize) _
                        .Take(criteria.PageSize)

            For Each person In query
                Console.WriteLine("{0} {1}",
                                    person.FirstName,
                                    person.LastName)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Sub MultiStepQuery()
        Using context = New Context()
            Dim query = From onePerson In context.People _
                        Where onePerson.PersonState = PersonState.Active
                        Select New With { _
                            onePerson.HeightInFeet, _
                            onePerson.PersonId
                        }
            query = query.OrderBy(Function(p) p.HeightInFeet)
            Dim sum = query.Sum(Function(p) p.HeightInFeet)
            Console.WriteLine(sum)


            Dim criteria = New With {.FilterActive = True}
            query = From onePerson In context.People _
                        Where Not criteria.FilterActive Or _
                            (criteria.FilterActive And
                                onePerson.PersonState = PersonState.Active)
                        Select New With { _
                            onePerson.HeightInFeet, _
                            onePerson.PersonId
                        }
            query = query.OrderBy(Function(p) p.HeightInFeet)
            sum = query.Sum(Function(p) p.HeightInFeet)
            Console.WriteLine(sum)

            query = From onePerson In context.People _
                        Where DbFunctions.AddDays(onePerson.BirthDate, 2) >
                            New DateTime(1970, 1, 1)
                        Select New With { _
                            onePerson.HeightInFeet, _
                            onePerson.PersonId
                        }
            query = query.OrderBy(Function(p) p.HeightInFeet)
            sum = query.Sum(Function(p) p.HeightInFeet)
            Console.WriteLine(sum)
            Console.ReadKey()
        End Using
    End Sub

    Sub Aggregations()
        Using context = New Context()
            Dim min = context.People.Max(Function(p) p.BirthDate)
            Console.WriteLine(min)
            Dim count = context.People.Count( _
                Function(p) p.PersonState = PersonState.Active)
            Console.WriteLine(count)


            Dim people = context.People _
                    .Select(Function(p) New With { _
                    p.LastName, _
                    p.FirstName, _
                    p.Phones.Count
                })
            For Each person In people
                Console.WriteLine("{0} {1} {2}",
                                    person.FirstName,
                                    person.LastName,
                                    person.Count)
            Next
            Console.ReadKey()
        End Using
    End Sub

    Sub RunProjectionQiery()
        Using context = New Context()
            Dim people = context.People _
                        .Where(Function(p) p.PersonState = PersonState.Active) _
                        .OrderBy(Function(p) p.LastName) _
                        .ThenBy(Function(p) p.FirstName) _
                        .Select(Function(p) New With { _
                        p.LastName, _
                        p.FirstName, _
                        p.PersonType.TypeName _
                    })
            For Each person In people
                Console.WriteLine("{0} {1} {2}",
                                    person.FirstName,
                                    person.LastName,
                                    person.TypeName)
            Next

            Dim query = From onePerson In context.People
                        Where onePerson.PersonState = PersonState.Active
                        Order By onePerson.LastName, onePerson.FirstName
                        Select New With { _
                            .Last = onePerson.LastName, _
                            .First = onePerson.FirstName, _
                            onePerson.PersonType.TypeName _
            }
            For Each person In query
                Console.WriteLine("{0} {1} {2}",
                                    person.First,
                                    person.Last,
                                    person.TypeName)
            Next

            Dim explicitQuery = _
                From onePerson In context.People
                Where onePerson.PersonState = PersonState.Active
                Order By onePerson.LastName, onePerson.FirstName
                Select New PersonInfo With { _
                    .LastName = onePerson.LastName, _
                    .FirstName = onePerson.FirstName, _
                    .PersonType = onePerson.PersonType.TypeName, _
                    .PersonId = onePerson.PersonId, _
                    .Phones = onePerson.Phones.Select( _
                        Function(ph) ph.PhoneNumber)
                }
            For Each person In explicitQuery
                Console.WriteLine("{0} {1} {2} {3}",
                                    person.FirstName,
                                    person.LastName,
                                    person.PersonType,
                                    person.PersonId)
                For Each phone In person.Phones
                    Console.WriteLine("   " + phone)
                Next
            Next

            Console.ReadKey()
        End Using

    End Sub

End Module
