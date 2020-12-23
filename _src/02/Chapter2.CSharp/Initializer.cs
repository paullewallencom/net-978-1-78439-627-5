using System.Data.Entity;

namespace Chapter2.CSharp
{
    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            context.Companies.Add(new Company
            {
                Name = "My company"
            });
        }
    }
}
