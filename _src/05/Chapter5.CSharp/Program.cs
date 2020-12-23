using System;
using System.Data.Entity;
using System.Linq;
using Chapter5.CSharp.CustomModels;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            //RunProjectionQiery();
            //Aggregations();
            //MultiStepQuery();
            //Paging();
            //Joins();
            //Groups();
            //LeftOuterJoins();
            //SelectMany();
            SetOperations();
        }

        private static void SetOperations()
        {
            using (var context = new Context())
            {
                var uniqueQuery = context.People
                    .Select(p => p.PersonType.TypeName)
                    .Distinct();
                foreach (var oneType in uniqueQuery)
                {
                    Console.WriteLine(oneType);
                }

                var unionQuery = context.People
                    .Select(p => new
                    {
                        Name = p.LastName + " " + p.FirstName,
                        RowType = "Person"
                    })
                    .Union(context.Companies.Select(c => new
                    {
                        Name = c.CompanyName,
                        RowType = "Company"
                    }))
                    .OrderBy(result => result.RowType)
                    .ThenBy(result => result.Name);
                foreach (var name in unionQuery)
                {
                    Console.WriteLine("{0} {1}",
                        name.Name, name.RowType);
                }

                var intersectQuery = context.People
                    .Select(p => new
                    {
                        Name = p.LastName + " " + p.FirstName,
                        RowType = "Person"
                    })
                    .Intersect(context.Companies.Select(c => new
                    {
                        Name = c.CompanyName,
                        RowType = "Company"
                    }))
                    .OrderBy(result => result.RowType)
                    .ThenBy(result => result.Name);
                foreach (var name in intersectQuery)
                {
                    Console.WriteLine("{0} {1}",
                        name.Name, name.RowType);
                }

                var exceptQuery = context.People
                    .Select(p => new
                    {
                        Name = p.LastName + " " + p.FirstName,
                        RowType = "Person"
                    })
                    .Except(context.Companies.Select(c => new
                    {
                        Name = c.CompanyName,
                        RowType = "Company"
                    }))
                    .OrderBy(result => result.RowType)
                    .ThenBy(result => result.Name);
                foreach (var name in exceptQuery)
                {
                    Console.WriteLine("{0} {1}",
                        name.Name, name.RowType);
                }
            }
            Console.ReadKey();
        }


        private static void SelectMany()
        {
            using (var context = new Context())
            {
                var query =
                    from onePerson in context.People
                    from onePhone in onePerson.Phones
                    orderby onePerson.LastName, onePhone.PhoneNumber
                    select new
                    {
                        onePerson.LastName,
                        onePerson.FirstName,
                        onePhone.PhoneNumber
                    };
                foreach (var person in query)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.LastName, person.FirstName, person.PhoneNumber);
                }

                var methodQuery =
                    context.People
                        .SelectMany(person => person.Phones, (person, phone) => new
                        {
                            person.LastName,
                            person.FirstName,
                            phone.PhoneNumber
                        })
                        .OrderBy(p => p.LastName)
                        .ThenBy(p => p.PhoneNumber);
                foreach (var person in methodQuery)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.LastName, person.FirstName, person.PhoneNumber);
                }
            }
            Console.ReadKey();
        }

        private static void Groups()
        {
            using (var context = new Context())
            {
                var query =
                    from onePerson in context.People
                    group onePerson by new { onePerson.BirthDate.Value.Month }
                        into monthGroup
                        select new
                        {
                            Month = monthGroup.Key.Month,
                            Count = monthGroup.Count()
                        };
                foreach (var group in query)
                {
                    Console.WriteLine("{0} {1}",
                        group.Month, group.Count);
                }

                var methodQuery =
                    context.People
                    .GroupBy(
                        onePerson => new { onePerson.BirthDate.Value.Month },
                        monthGroup => monthGroup)
                    .Select(monthGroup => new
                    {
                        Month = monthGroup.Key.Month,
                        Count = monthGroup.Count()
                    });
                foreach (var group in methodQuery)
                {
                    Console.WriteLine("{0} {1}",
                        group.Month, group.Count);
                }
            }
            Console.ReadKey();
        }

        private static void LeftOuterJoins()
        {
            using (var context = new Context())
            {

                var query =
                    from person in context.People
                    join personType in context.PersonTypes
                                on person.PersonTypeId equals personType.PersonTypeId into finalGroup
                    from groupedData in finalGroup.DefaultIfEmpty()
                    select new
                    {
                        person.LastName,
                        person.FirstName,
                        TypeName = groupedData.TypeName ?? "Unknown"
                    };
                foreach (var person in query)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.FirstName, person.LastName, person.TypeName);
                }

                var methodQuery = context.People
                    .GroupJoin(
                        context.PersonTypes,
                        person => person.PersonTypeId,
                        personType => personType.PersonTypeId,
                        (person, type) => new
                        {
                            Person = person,
                            PersonType = type
                        })
                    .SelectMany(groupedData =>
                        groupedData.PersonType.DefaultIfEmpty(),
                        (group, personType) => new
                        {
                            group.Person.LastName,
                            group.Person.FirstName,
                            TypeName = personType.TypeName ?? "Unknown"
                        });


                foreach (var person in methodQuery)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.FirstName, person.LastName, person.TypeName);
                }

            }

            Console.ReadKey();
        }

        private static void Joins()
        {
            using (var context = new Context())
            {

                var people = from person in context.People
                             join personType in context.PersonTypes
                             on person.PersonTypeId equals personType.PersonTypeId
                             select new
                             {
                                 person.LastName,
                                 person.FirstName,
                                 personType.TypeName
                             };
                foreach (var person in people)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.FirstName, person.LastName, person.TypeName);
                }

                people = context.People
                    .Join(
                        context.PersonTypes,
                        person => person.PersonTypeId,
                        personType => personType.PersonTypeId,
                        (person, type) => new
                        {
                            Person = person,
                            PersonType = type
                        })
                    .Select(p => new
                    {
                        p.Person.LastName,
                        p.Person.FirstName,
                        p.PersonType.TypeName
                    });
                foreach (var person in people)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.FirstName, person.LastName, person.TypeName);
                }

            }

            Console.ReadKey();
        }

        private static void Aggregations()
        {

            using (var context = new Context())
            {
                var min = context.People.Min(p => p.BirthDate);
                Console.WriteLine(min);

                var count = context.People.Count(p =>
                    p.PersonState == PersonState.Active);
                Console.WriteLine(count);

                var people = context.People
                    .Select(p => new
                    {
                        p.LastName,
                        p.FirstName,
                        p.Phones.Count
                    });
                foreach (var person in people)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.FirstName, person.LastName, person.Count);
                }

                Console.ReadKey();
            }

        }

        private static void Paging()
        {
            using (var context = new Context())
            {
                var criteria = new { PageNumber = 1, PageSize = 2 };
                var people = context.People
                    .OrderBy(p => p.LastName)
                    .Skip((criteria.PageNumber - 1) * criteria.PageSize)
                    .Take(criteria.PageSize);
                foreach (var person in people)
                {
                    Console.WriteLine("{0} {1}",
                        person.FirstName, person.LastName);
                }

                Console.ReadKey();
            }
        }

        private static void MultiStepQuery()
        {

            using (var context = new Context())
            {
                var query = from onePerson in context.People
                            where onePerson.PersonState == PersonState.Active
                            select new
                            {
                                onePerson.HeightInFeet,
                                onePerson.PersonId
                            };
                query = query.OrderBy(p => p.HeightInFeet);
                var sum = query.Sum(p => p.HeightInFeet);
                Console.WriteLine(sum);

                var criteria = new { FilterActive = true };
                query = from onePerson in context.People
                        where !criteria.FilterActive ||
                            (criteria.FilterActive &&
                                onePerson.PersonState == PersonState.Active)
                        select new
                        {
                            onePerson.HeightInFeet,
                            onePerson.PersonId
                        };
                query = query.OrderBy(p => p.HeightInFeet);
                sum = query.Sum(p => p.HeightInFeet);
                Console.WriteLine(sum);


                query = from onePerson in context.People
                        where DbFunctions.AddDays(onePerson.BirthDate, 2) >
                            new DateTime(1970, 1, 1)
                        select new
                        {
                            onePerson.HeightInFeet,
                            onePerson.PersonId
                        };
                sum = query.Sum(p => p.HeightInFeet);
                Console.WriteLine(sum);
                Console.ReadKey();
            }

        }


        private static void RunProjectionQiery()
        {
            using (var context = new Context())
            {
                var people = context.People
                    .Where(p => p.PersonState == PersonState.Active)
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .Select(p => new
                    {
                        p.LastName,
                        p.FirstName,
                        p.PersonType.TypeName
                    });
                foreach (var person in people)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.FirstName, person.LastName, person.TypeName);
                }

                var query = from onePerson in context.People
                            where onePerson.PersonState == PersonState.Active
                            orderby onePerson.LastName, onePerson.FirstName
                            select new
                            {
                                Last = onePerson.LastName,
                                First = onePerson.FirstName,
                                onePerson.PersonType.TypeName
                            };
                foreach (var person in query)
                {
                    Console.WriteLine("{0} {1} {2}",
                        person.First, person.Last, person.TypeName);
                }

                var explicitQuery =
                    from onePerson in context.People
                    where onePerson.PersonState == PersonState.Active
                    orderby onePerson.LastName, onePerson.FirstName
                    select new PersonInfo
                    {
                        LastName = onePerson.LastName,
                        FirstName = onePerson.FirstName,
                        PersonType = onePerson.PersonType.TypeName,
                        PersonId = onePerson.PersonId,
                        Phones = onePerson.Phones.Select(ph => ph.PhoneNumber)
                    };
                foreach (var person in explicitQuery)
                {
                    Console.WriteLine("{0} {1} {2} {3}",
                        person.FirstName, person.LastName,
                        person.PersonType, person.PersonId);
                    foreach (var phone in person.Phones)
                    {
                        Console.WriteLine("   " + phone);
                    }
                }

                Console.ReadKey();
            }
        }
    }
}
