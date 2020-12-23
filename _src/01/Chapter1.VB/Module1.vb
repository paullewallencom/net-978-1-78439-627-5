Imports System.Data.SqlClient
Imports System.Configuration

Module Module1

    Sub Main()
        Dim connectionString =
            ConfigurationManager.ConnectionStrings("chapter1").
                ConnectionString
        Using connection = New SqlConnection(connectionString)
            connection.Open()
            Dim people = New List(Of Person)()
            Using command = connection.CreateCommand()
                command.CommandText = _
                    "SELECT PersonId, FirstName, LastName " + _
                    "FROM Person"
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        Dim person = New Person() With { _
                                .PersonId = reader.GetInt32(0), _
                                .FirstName = reader.GetString(1), _
                                .LastName = reader.GetString(2) _
                        }
                        people.Add(person)
                    End While
                End Using
            End Using
            people.ForEach(Sub(onePerson) _
                                Console.WriteLine("{0} {1} ({2})",
                                                    onePerson.FirstName,
                                                    onePerson.LastName,
                                                    onePerson.PersonId))
            Console.ReadKey()
        End Using
    End Sub

End Module
