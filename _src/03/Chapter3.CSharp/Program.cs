using System.Data.Entity;
using System.Linq;

namespace Chapter3.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            AddPersonWithPhoneNumbers();
            //ForceDatabaseToRebuild();
        }

        private static void AddPersonWithPhoneNumbers()
        {
            using (var context = new Context())
            {
                var person = new Person
                {
                    LastName = "Doe",
                    FirstName = "John",
                    IsActive = true
                };
                person.Phones.Add(new Phone { PhoneNumber = "123-446-7890" });
                person.Phones.Add(new Phone { PhoneNumber = "123-446-7891" });
                context.People.Add(person);
                context.SaveChanges();
            }
        }

        private static void ForceDatabaseToRebuild()
        {
            using (var context = new Context())
            {
                var total = context.People.Count();
            }
        }
    }
}
