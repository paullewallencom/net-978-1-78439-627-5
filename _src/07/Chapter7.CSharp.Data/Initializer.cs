using System.Data.Entity;
using Chapter7.CSharp.Data.Migrations;

namespace Chapter7.CSharp.Data
{
    public class Initializer :
        MigrateDatabaseToLatestVersion<Context, Configuration>
    {
        public override void InitializeDatabase(Context context)
        {
            base.InitializeDatabase(context);
        }
    }
}
