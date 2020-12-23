using System;
using System.Data.Entity;
using System.Linq;

namespace Chapter2.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            //VerifyDatabaseExists();
            //InsertNewRow();
            //QueryData();
            //UpdateData();
            //DeleteData();
            QueryCompanies();
        }

        private static void VerifyDatabaseExists()
        {
            using (var context = new Context())
            {
                context.Database.CreateIfNotExists();
            }
        }

        private static void InsertNewRow()
        {
            using (var context = new Context())
            {
                var person = new Person
                {
                    FirstName = "John",
                    LastName = "Doe"
                };

                context.People.Add(person);

                context.SaveChanges();
            }
        }

        private static void QueryData()
        {
            using (var context = new Context())
            {
                var savedPeople = context.People;
                foreach (var person in savedPeople)
                {
                    Console.WriteLine("Last name:{0},first name:{1},id {2}",
                        person.LastName, person.FirstName, person.PersonId);
                }
            }
            Console.ReadKey();
        }

        private static void QueryCompanies()
        {
            using (var context = new Context())
            {
                foreach (var company in context.Companies)
                {
                    Console.WriteLine("Name:{0},id:{1}",
                        company.Name, company.CompanyId);
                }
            }
            Console.ReadKey();
        }

        private static void UpdateData()
        {
            using (var context = new Context())
            {
                var savedPeople = context.People;
                if (savedPeople.Any())
                {
                    var person = savedPeople.First();
                    person.FirstName = "Johnny";
                    person.LastName = "Benson";
                    context.SaveChanges();
                }
            }
            QueryData();
        }

        private static void DeleteData()
        {
            using (var context = new Context())
            {
                var personId = 2;
                var person = context.People.Find(personId);
                if (person != null)
                {
                    context.People.Remove(person);
                    context.SaveChanges();
                }

            }
            QueryData();
        }
    }
}
