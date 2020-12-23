using System;
using System.Data.Entity;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp
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
                    Address = new Address
                    {
                        City = "Atlanta",
                        State = "GA",
                        Street = "100 Main Street",
                        Zip = "30000"
                    }
                
                });
            var company2 = context.Companies.Add(new Company
            {
                CompanyName = "DEF",
                Address = new Address
                {
                    City = "Atlanta",
                    State = "GA",
                    Street = "2 Main Street",
                    Zip = "30333"
                } 
            
            });

            var person = new Person
            {
                BirthDate = new DateTime(1980, 1, 2),
                FirstName = "John",
                HeightInFeet = 6.1M,
                IsActive = true,
                LastName = "Doe",
                MiddleName = "M",
                PersonType = type1,
                Address = new Address
                {
                    City = "Atlanta",
                    State = "GA",
                    Street = "1 Main Street",
                    Zip = "33000"
                }
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
                PersonType = type2,
                Address = new Address
                {
                    City = "Atlanta",
                    State = "GA",
                    Street = "22 Main Street",
                    Zip = "33000"
                }
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
                PersonType = type2,
                Address = new Address
                {
                    City = "Lilburn",
                    State = "GA",
                    Street = "2 Main Street",
                    Zip = "33000"
                }
            };
            context.People.Add(person);
            person.Phones.Add(new Phone { PhoneNumber = "1-555-666-7777" });
            person.Phones.Add(new Phone { PhoneNumber = "1-888-999-3333" });

            company2.Persons.Add(person);


            person = new Person
            {
                BirthDate = new DateTime(1970, 1, 2),
                FirstName = "Tom",
                HeightInFeet = 5.2M,
                IsActive = true,
                LastName = "Johnson",
                MiddleName = "K",
                Address = new Address
                {
                    City = "Lilburn",
                    State = "GA",
                    Street = "33 Main Street",
                    Zip = "33000"
                }
            };
            context.People.Add(person);
            person.Phones.Add(new Phone { PhoneNumber = "1-333-666-7777" });
            person.Phones.Add(new Phone { PhoneNumber = "1-444-999-3333" });

            company2.Persons.Add(person);
            context.SaveChanges();
        }
    }
}
