using System.Data.Entity;

namespace Chapter3.CSharp
{
    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
    }
}
