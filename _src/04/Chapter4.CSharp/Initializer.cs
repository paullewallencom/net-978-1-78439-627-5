using System;
using System.Data.Entity;
using Chapter4.CSharp.Models;

namespace Chapter4.CSharp
{
    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var type1 = context.PersonTypes.Add(new PersonType { TypeName = "Friend" });
            var type2 = context.PersonTypes.Add(new PersonType { TypeName = "Co-worker" });

            var company1 = context.Companies.Add(new Company { CompanyName = "ABC" });
            var company2 = context.Companies.Add(new Company { CompanyName = "DEF" });

            var person = new Person
            {
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "John",
                HeightInFeet = 6.1M,
                IsActive = true,
                LastName = "Doe",
                MiddleName = "M",
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
                MiddleName = "J",
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
                MiddleName = "K",
                PersonType = type2
            };
            context.People.Add(person);
            person.Phones.Add(new Phone { PhoneNumber = "1-555-666-7777" });
            person.Phones.Add(new Phone { PhoneNumber = "1-888-999-3333" });

            company2.Persons.Add(person);
            context.SaveChanges();
        }
    }
}
