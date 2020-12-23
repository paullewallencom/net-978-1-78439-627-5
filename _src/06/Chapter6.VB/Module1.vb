Imports System.Data.Entity
Imports Chapter6.VB.Models
Imports System.Data.Entity.Infrastructure

Module Module1

    Sub Main()
        Database.SetInitializer(New Initializer())
        ' comment out the line below to force database check code to run
        ' if you are running both VB and C# from the same solution, run C# project first to create the database.
        'Database.SetInitializer(Of Context)(Nothing)

        'QueryView()
        'ExecuteProcedure()
        'UseStoredProcForActions()
        'RunAsyncMethods()
        ConcurrencyExample()
    End Sub


    Private Sub ConcurrencyExample()
        Dim person = New Person() With {
            .BirthDate = New DateTime(1970, 1, 2),
            .FirstName = "Aaron",
            .HeightInFeet = 6D,
            .IsActive = True,
            .LastName = "Smith"
        }
        Dim personId As Integer
        Using context = New Context()
            context.People.Add(person)
            context.SaveChanges()
            personId = person.PersonId
        End Using
        'simulate second user
        Using context = New Context()
            context.People.Find(personId).IsActive = False
            context.SaveChanges()
        End Using

        'back to first user
        Try
            Using context = New Context()
                context.Entry(person).State = EntityState.Unchanged
                person.IsActive = False
                context.SaveChanges()
            End Using
            Console.WriteLine("Concurrency error should occur!")
        Catch exception As DbUpdateConcurrencyException
            Console.WriteLine("Expected concurrency error")
        End Try
        Console.ReadKey()
    End Sub


    Private Sub RunAsyncMethods()
        Dim companies = GetCompaniesAsync().Result
        For Each company In companies
            Console.WriteLine(company.CompanyName)
        Next

        Dim task = AddCompanyAsync(New Company() With {
             .CompanyName = "Async company",
             .DateAdded = DateTime.Now,
             .IsActive = True
        })
        task.Wait()
        Dim companyId = task.Result.CompanyId
        Console.WriteLine(companyId)

        Console.WriteLine(FindCompanyAsync(companyId).Result.CompanyName)

        Console.WriteLine(ComputeCountAsync().Result)

        Dim loopTask = LoopAsync()
        loopTask.Wait()
        Console.ReadKey()
    End Sub


    Private Async Function LoopAsync() As Task
        Using context = New Context()
            Await context.Companies.ForEachAsync( _
                Sub(c)
                    c.IsActive = True
                End Sub)
            Await context.SaveChangesAsync()
        End Using
    End Function

    Private Async Function ComputeCountAsync() As Task(Of Integer)
        Using context = New Context()
            Return Await context.Companies.CountAsync( _
                Function(c) c.IsActive)
        End Using
    End Function

    Private Async Function FindCompanyAsync(companyId As Integer) As Task(Of Company)
        Using context = New Context()
            Return Await context.Companies.FindAsync(companyId)
        End Using
    End Function

    Private Async Function AddCompanyAsync(company As Company) As Task(Of Company)
        Using context = New Context()
            context.Companies.Add(company)
            Await context.SaveChangesAsync()
            Return company
        End Using
    End Function

    Private Async Function GetCompaniesAsync() As Task(Of IEnumerable(Of Company))
        Using context = New Context()
            Return Await context.Companies.OrderBy( _
                Function(c) c.CompanyName).ToListAsync()
        End Using
    End Function




    Private Sub UseStoredProcForActions()
        Dim companyId = 0
        Using context = New Context()
            Dim company = New Company() With { _
                .CompanyName = "New", _
                .DateAdded = DateTime.Now, _
                .IsActive = False _
            }
            context.Companies.Add(company)
            context.SaveChanges()
            Console.WriteLine("New company id is " + company.CompanyId.ToString())
            companyId = company.CompanyId
        End Using
        Using context = New Context()
            Dim company = New Company() With { _
                .CompanyId = companyId, _
                .CompanyName = "Updated", _
                .DateAdded = DateTime.Now, _
                .IsActive = False _
            }
            context.Entry(company).State = EntityState.Modified
            context.SaveChanges()
        End Using

        Using context = New Context()
            Dim company = New Company() With { _
                .CompanyId = companyId _
            }
            context.Entry(company).State = EntityState.Deleted
            context.SaveChanges()
        End Using

        Console.ReadKey()
    End Sub


    Private Sub ExecuteProcedure()
        Using context = New Context()
            Dim sql = "UpdateCompanies {0}, {1}"
            Dim rowsAffected =
                context.Database.ExecuteSqlCommand( _
                    sql, DateTime.Now, True)
            Console.WriteLine("{0} Rows affected", rowsAffected)

            sql = "SelectCompanies {0}"
            Dim companies = context.Database.SqlQuery(Of CompanyInfo)(
                sql,
                DateTime.Today.AddYears(-10))
            For Each companyInfo As CompanyInfo In companies
                Console.WriteLine(companyInfo.CompanyName)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Private Sub QueryView()
        Using context = New Context()
            Dim people = context.PersonView _
                    .Where(Function(p) p.PersonId > 0) _
                    .OrderBy(Function(p) p.LastName) _
                    .ToList()
            For Each personViewInfo In people
                Console.WriteLine(personViewInfo.LastName)
            Next

            Dim sql = "SELECT * FROM PERSONVIEW WHERE PERSONID > {0} "
            Dim peopleViaCommand = context.Database.SqlQuery(Of PersonViewInfo)(sql, 0)
            For Each personViewInfo In peopleViaCommand
                Console.WriteLine(personViewInfo.LastName)
            Next
        End Using
        Console.ReadKey()
    End Sub

    Private Sub ForceDatabaseToRebuild()
        Using context = New Context()
            Dim total = context.People.Count()
        End Using
    End Sub

End Module
