using System.Data.Entity.ModelConfiguration;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp.Maps
{
    public class CompanyMap :
        EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            MapToStoredProcedures(config =>
                {
                    config.Delete(
                        procConfig =>
                        {
                            procConfig.HasName("CompanyDelete");
                            procConfig.Parameter(company => company.CompanyId, "companyId");
                        });
                    config.Insert(procConfig => procConfig.HasName("CompanyInsert"));
                    config.Update(procConfig => procConfig.HasName("CompanyUpdate"));
                });
        }
    }
}
