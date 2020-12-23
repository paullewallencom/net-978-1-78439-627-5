using System;
using System.Data.Entity;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp
{
    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var type1 = context.PersonTypes.Add(new PersonType { TypeName = "Friend" });
            var type2 = context.PersonTypes.Add(new PersonType { TypeName = "Co-worker" });

            var company1 = context.Companies.Add(
                new Company
                {
                    CompanyName = "ABC",
                    DateAdded = DateTime.Today.AddDays(-10),
                    IsActive = true
                });
            var company2 = context.Companies.Add(
                new Company
                {
                    CompanyName = "DEF",
                    DateAdded = DateTime.Today.AddDays(-20),
                    IsActive = true
                });



            var person = new Person
            {
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "John",
                HeightInFeet = 6.1M,
                IsActive = true,
                LastName = "Doe",
                PersonType = type1
            };
            context.People.Add(person);
            person.Phones.Add(new Phone { PhoneNumber = "1-222-333-4444" });
            person.Phones.Add(new Phone { PhoneNumber = "1-333-4444-5555" });
            company1.Persons.Add(person);

            person = new Person
            {
                BirthDate = new DateTime(1970, 1, 2),
                FirstName = "John",
                HeightInFeet = 5.6M,
                IsActive = true,
                LastName = "Johnson",
                PersonType = type2
            };
            context.People.Add(person);
            person.Phones.Add(new Phone { PhoneNumber = "1-555-666-7777" });
            person.Phones.Add(new Phone { PhoneNumber = "1-888-999-3333" });

            company1.Persons.Add(person);

            person = new Person
            {
                BirthDate = new DateTime(1970, 1, 2),
                FirstName = "Jean",
                HeightInFeet = 5.2M,
                IsActive = true,
                LastName = "Abrams",
                PersonType = type2
            };
            context.People.Add(person);
            person.Phones.Add(new Phone { PhoneNumber = "1-555-666-7777" });
            person.Phones.Add(new Phone { PhoneNumber = "1-888-999-3333" });

            company2.Persons.Add(person);

            context.SaveChanges();

            context.Database.ExecuteSqlCommand("DROP TABLE PersonView");
            context.Database.ExecuteSqlCommand(
                @"CREATE VIEW [dbo].[PersonView]
                AS
                SELECT 
	                dbo.People.PersonId, 
	                dbo.People.FirstName, 
	                dbo.People.LastName, 
	                dbo.PersonTypes.TypeName
                FROM     
	                dbo.People 
                INNER JOIN dbo.PersonTypes 
	                ON dbo.People.PersonTypeId = dbo.PersonTypes.PersonTypeId
            ");
            
            context.Database.ExecuteSqlCommand(
                @"CREATE PROCEDURE dbo.UpdateCompanies
	                @dateAdded as DateTime,
	                @activeFlag as Bit
                AS
                BEGIN
	                UPDATE Companies
	                Set DateAdded = @dateAdded,
		                IsActive = @activeFlag
                END
            ");

            context.Database.ExecuteSqlCommand(
                @"CREATE PROCEDURE [dbo].[SelectCompanies]
	                @dateAdded as DateTime
                AS
                BEGIN
	                SELECT CompanyId, CompanyName 
                    FROM Companies
	                WHERE DateAdded > @dateAdded
                END
            ");

            
        }
    }
}
