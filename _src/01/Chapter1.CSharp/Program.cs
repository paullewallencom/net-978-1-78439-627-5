using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Chapter1.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["chapter1"]
                .ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var people = new List<Person>();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT PersonId, FirstName, LastName " +
                        "FROM Person";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var person = new Person
                            {
                                Personid = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            };
                            people.Add(person);
                        }
                    }
                }

                people.ForEach(onePerson =>
                    Console.WriteLine("{0} {1} ({2})",
                    onePerson.FirstName, onePerson.LastName, onePerson.Personid));
                Console.ReadKey();
            }
        }
    }
}
