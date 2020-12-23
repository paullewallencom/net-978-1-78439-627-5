using System;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using Chapter4.CSharp.Models;

namespace Chapter4.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());

            //RunBasicQuery();
            //RunFilterQuery();
            //RunOrderByQuery();
            //RunRelatedFilterSortQuery();
            //ElementOperations();
            //Quantifiers();
            //LazyEagerLoading();
            //InsertData();
            //UpdateData();
            //NoTracking();
            //DeleteData();
            RunLocalQuery();
        }

        private static void RunBasicQuery()
        {
            using (var context = new Context())
            {
                var query = context.People;
                var data = query.ToList();
            }
        }

        private static void RunFilterQuery()
        {
            using (var context = new Context())
            {
                var query = from person in context.People
                            where
                                person.HeightInFeet >= 6 &&
                                person.FirstName.Contains("J") &&
                                person.IsActive
                            select person;

                Console.WriteLine(query.ToList().Count);

                var methodQuery = context.People
                    .Where(p =>
                        p.HeightInFeet >= 6 &&
                        p.FirstName.Contains("J") &&
                        p.IsActive);
                Console.WriteLine(methodQuery.ToList().Count);
                Prompt();
            }
        }

        private static void RunOrderByQuery()
        {
            using (var context = new Context())
            {
                var query = from person in context.People
                            where person.IsActive
                            orderby person.LastName descending, person.FirstName descending
                            select person;

                Console.WriteLine(query.ToList().Count);

                var methodQuery = context.People
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName);
                Console.WriteLine(methodQuery.ToList().Count);
                Prompt();
            }
        }

        private static void RunRelatedFilterSortQuery()
        {
            using (var context = new Context())
            {
                var query = from person in context.People
                            where person.IsActive &&
                            person.Phones.Any(ph => ph.PhoneNumber.StartsWith("1"))
                            select person;

                Console.WriteLine(query.ToList().Count);

                var methodQuery = context.People
                    .Where(p => p.IsActive &&
                        p.Phones.Any(ph => ph.PhoneNumber.StartsWith("1")));
                Console.WriteLine(methodQuery.ToList().Count);
                Prompt();
            }
        }

        private static void ElementOperations()
        {
            using (var context = new Context())
            {
                var query = from person in context.People
                            where person.LastName == "Doe"
                            select person;
                var first = query.First();
                Console.WriteLine(first.LastName);

                var methodQuery = context.People.Where(p => p.LastName == "Doe");
                first = methodQuery.First();
                Console.WriteLine(first.LastName);
                first = context.People.First(p => p.LastName == "Doe");
                Console.WriteLine(first.LastName);
                Prompt();
            }
        }


        private static void Quantifiers()
        {
            using (var context = new Context())
            {
                var hasDoes = (from person in context.People
                               where person.LastName == "Doe"
                               select person).Any();

                Console.WriteLine(hasDoes);
                hasDoes = context.People.Any(p => p.LastName == "Doe");
                Console.WriteLine(hasDoes);

                var allHaveJ = context.People.All(p => p.FirstName.Contains("J"));
                Console.WriteLine(allHaveJ);
                Prompt();
            }
        }

        private static void LazyEagerLoading()
        {
            using (var context = new Context())
            {
                var query = from person in context.People
                            select person;

                foreach (var person in query.ToList())
                {
                    Console.WriteLine(person.LastName);
                    foreach (var phone in person.Phones)
                    {
                        Console.WriteLine(phone.PhoneNumber);
                    }
                }

            }

            using (var context = new Context())
            {
                var query = from person in context.People.Include("Phones")
                            select person;

                foreach (var person in query)
                {
                    Console.WriteLine(person.LastName);
                    foreach (var phone in person.Phones)
                    {
                        Console.WriteLine(phone.PhoneNumber);
                    }
                }



                Prompt();
            }
        }

        private static void InsertData()
        {
            var person = new Person
            {
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "John",
                HeightInFeet = 6.1M,
                IsActive = true,
                LastName = "Doe",
                MiddleName = "M"
            };

            person.Phones.Add(new Phone { PhoneNumber = "1-222-333-4444" });
            person.Phones.Add(new Phone { PhoneNumber = "1-333-4444-5555" });

            using (var context = new Context())
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            Console.WriteLine(person.PersonId);

            var person2 = new Person
            {
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "James",
                HeightInFeet = 6.1M,
                IsActive = true,
                LastName = "Jones",
                MiddleName = "M"
            };

            person2.Phones.Add(new Phone { PhoneNumber = "1-222-333-4444" });
            person2.Phones.Add(new Phone { PhoneNumber = "1-333-4444-5555" });

            using (var context = new Context())
            {
                context.Entry(person2).State = EntityState.Added;
                context.SaveChanges();
            }
            Console.WriteLine(person2.PersonId);
            Prompt();
        }

        private static void UpdateData()
        {
            using (var context = new Context())
            {
                var person = context.People.Find(1);
                person.FirstName = "New Name";
                context.SaveChanges();
                Console.WriteLine(person.FirstName);
            }

            var person2 = new Person
            {
                PersonId = 1,
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "Jonathan",
                HeightInFeet = 6.1m,
                IsActive = true,
                LastName = "Smith",
                MiddleName = "M"
            };
            person2.Phones.Add(new Phone
            {
                PhoneNumber = "updated 1",
                PhoneId = 1,
                PersonId = 1
            });
            person2.Phones.Add(new Phone
            {
                PhoneNumber = "updated 2",
                PhoneId = 2,
                PersonId = 1
            });
            using (var context = new Context())
            {
                context.Entry(person2).State = EntityState.Modified;
                foreach (var phone in person2.Phones)
                {
                    context.Entry(phone).State = EntityState.Modified;
                }
                context.SaveChanges();
            }

            var person3 = new Person
            {
                PersonId = 1,
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "Jonathan",
                HeightInFeet = 6.1m,
                IsActive = true,
                LastName = "Smith",
                MiddleName = "M"
            };
            using (var context = new Context())
            {
                context.People.Attach(person3);
                person3.LastName = "Updated";
                context.SaveChanges();
            }

            Prompt();
        }

        private static void DeleteData()
        {
            var person = AddPerson();
            var personId = person.PersonId;
            Console.WriteLine(personId);
            using (var context = new Context())
            {
                var toDelete = context.People.Find(personId);
                toDelete.Phones.ToList().ForEach(phone => context.Phones.Remove(phone));
                context.People.Remove(toDelete);
                context.SaveChanges();
            }

            person = AddPerson();
            personId = person.PersonId;
            var phoneId1 = person.Phones.First().PhoneId;
            var phoneId2 = person.Phones.Skip(1).First().PhoneId;
            var toDeleteByState = new Person { PersonId = personId };
            toDeleteByState.Phones.Add(new Phone
            {
                PhoneId = phoneId1,
                PersonId = personId
            });
            toDeleteByState.Phones.Add(new Phone
            {
                PhoneId = phoneId2,
                PersonId = personId
            });

            using (var context = new Context())
            {
                context.People.Attach(toDeleteByState);
                foreach (var phone in toDeleteByState.Phones.ToList())
                {
                    context.Entry(phone).State = EntityState.Deleted;
                }
                context.Entry(toDeleteByState).State = EntityState.Deleted;
                context.SaveChanges();
            }

            Prompt();
        }

        private static Person AddPerson()
        {
            var person = new Person
            {
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "John",
                HeightInFeet = 6.1M,
                IsActive = true,
                LastName = "Delete",
                MiddleName = "M"
            };

            person.Phones.Add(new Phone { PhoneNumber = "1-222-333-4444" });
            person.Phones.Add(new Phone { PhoneNumber = "1-333-4444-5555" });

            using (var context = new Context())
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            return person;
        }

        private static void NoTracking()
        {
            using (var context = new Context())
            {
                var query = context.People.Include(p => p.Phones).AsNoTracking();
                foreach (var person in query)
                {
                    Console.WriteLine(person.LastName);
                    foreach (var phone in person.Phones)
                    {
                        Console.WriteLine(phone.PhoneNumber);
                    }
                }
            }

        }

        private static void RunLocalQuery()
        {
            using (var context = new Context())
            {
                var query = context.People.ToList();
                var localQuery = context.People.Local.Where(p => p.LastName.Contains("o")).ToList();
                var findQuery = context.People.Find(1);
                Console.WriteLine(query.Count);
                Console.WriteLine(localQuery.Count);
                foreach (var dbEntityEntry in context.ChangeTracker.Entries<Person>())
                {
                    Console.WriteLine(dbEntityEntry.State);
                    Console.WriteLine(dbEntityEntry.Entity.LastName);
                }
                Prompt();
            }
        }

        private static void Prompt()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
