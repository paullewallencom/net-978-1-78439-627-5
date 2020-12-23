using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            //QueryView();
            //ExecuteProcedure();
            //UseStoredProcForActions();
            //RunAsyncMethods();
            ConcurrencyExample();
        }

        private static void ConcurrencyExample()
        {
            var person = new Person
            {
                BirthDate = new DateTime(1970, 1, 2),
                FirstName = "Aaron",
                HeightInFeet = 6M,
                IsActive = true,
                LastName = "Smith"
            };
            int personId;
            using (var context = new Context())
            {
                context.People.Add(person);
                context.SaveChanges();
                personId = person.PersonId;
            }
            //simulate second user
            using (var context = new Context())
            {
                context.People.Find(personId).IsActive = false;
                context.SaveChanges();
            }

            //back to first user
            try
            {
                using (var context = new Context())
                {
                    context.Entry(person).State = EntityState.Unchanged;
                    person.IsActive = false;
                    context.SaveChanges();
                }
                Console.WriteLine("Concurrency error should occur!");
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("Expected concurrency error");
            }
            Console.ReadKey();
        }

        private static void RunAsyncMethods()
        {
            var companies = GetCompaniesAsync().Result;
            foreach (var company in companies)
            {
                Console.WriteLine(company.CompanyName);
            }

            var task = AddCompanyAsync(new Company
            {
                CompanyName = "Async company",
                DateAdded = DateTime.Now,
                IsActive = true
            });
            task.Wait();
            var companyId = task.Result.CompanyId;
            Console.WriteLine(companyId);

            Console.WriteLine(
                FindCompanyAsync(companyId).Result.CompanyName);

            Console.WriteLine(
              ComputeCountAsync().Result);

            var loopTask = LoopAsync();
            loopTask.Wait();
            Console.ReadKey();
        }

        private static async Task LoopAsync()
        {
            using (var context = new Context())
            {
                await context.Companies.ForEachAsync(c =>
                {
                    c.IsActive = true;
                });
                await context.SaveChangesAsync();
            }
        }

        private static async Task<int> ComputeCountAsync()
        {
            using (var context = new Context())
            {
                return await context.Companies
                    .CountAsync(c => c.IsActive);
            }
        }

        private static async Task<Company> FindCompanyAsync(int companyId)
        {
            using (var context = new Context())
            {
                return await context.Companies
                    .FindAsync(companyId);
            }
        }

        private static async Task<Company> AddCompanyAsync(Company company)
        {
            using (var context = new Context())
            {
                context.Companies.Add(company);
                await context.SaveChangesAsync();
                return company;
            }
        }

        private static async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            using (var context = new Context())
            {
                return await context.Companies
                    .OrderBy(c => c.CompanyName)
                    .ToListAsync();
            }
        }

        private static void UseStoredProcForActions()
        {
            var companyId = 0;
            using (var context = new Context())
            {
                var company = new Company
                {
                    CompanyName = "New",
                    DateAdded = DateTime.Now,
                    IsActive = false
                };
                context.Companies.Add(company);
                context.SaveChanges();
                Console.WriteLine("New company id is " + company.CompanyId);
                companyId = company.CompanyId;
            }
            using (var context = new Context())
            {
                var company = new Company
                {
                    CompanyId = companyId,
                    CompanyName = "Updated",
                    DateAdded = DateTime.Now,
                    IsActive = false
                };
                context.Entry(company).State = EntityState.Modified;
                context.SaveChanges();
            }

            using (var context = new Context())
            {
                var company = new Company
                {
                    CompanyId = companyId
                };
                context.Entry(company).State = EntityState.Deleted;
                context.SaveChanges();
            }

            Console.ReadKey();
        }

        private static void ExecuteProcedure()
        {
            using (var context = new Context())
            {
                var sql = @"UpdateCompanies {0}, {1}";
                var rowsAffected =
                    context.Database.ExecuteSqlCommand(
                        sql, DateTime.Now, true);
                Console.WriteLine("{0} Rows affected", rowsAffected);


                sql = @"SelectCompanies {0}";
                var companies = context.Database.SqlQuery<CompanyInfo>(
                    sql,
                    DateTime.Today.AddYears(-10));
                foreach (var companyInfo in companies)
                {
                    Console.WriteLine(companyInfo.CompanyName);
                }
            }
            Console.ReadKey();
        }

        private static void QueryView()
        {
            using (var context = new Context())
            {
                var people = context.PersonView
                    .Where(p => p.PersonId > 0)
                    .OrderBy(p => p.LastName)
                    .ToList();
                foreach (var personViewInfo in people)
                {
                    Console.WriteLine(personViewInfo.LastName);
                }

                var sql = @"SELECT * FROM PERSONVIEW WHERE PERSONID > {0} ";
                var peopleViaCommand = context.Database.SqlQuery<PersonViewInfo>(
                    sql,
                    0);
                foreach (var personViewInfo in peopleViaCommand)
                {
                    Console.WriteLine(personViewInfo.LastName);
                }

            }
            Console.ReadKey();
        }

        private static void ForceDatabaseToRebuild()
        {
            using (var context = new Context())
            {
                var count = context.People.Count();
            }
        }
    }
}
