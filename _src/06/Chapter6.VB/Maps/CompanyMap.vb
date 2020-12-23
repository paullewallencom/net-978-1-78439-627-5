Imports System.Data.Entity.ModelConfiguration
Imports Chapter6.VB.Models

Namespace Maps

    Public Class CompanyMap
        Inherits EntityTypeConfiguration(Of Company)
        Public Sub New()
            MapToStoredProcedures( _
                Sub(config)

                    config.Delete(
                        Sub(procConfig)

                        procConfig.HasName("CompanyDelete")
                        procConfig.Parameter(Function(company) company.CompanyId, "companyId")
                    End Sub
                    )
                    config.Insert(Function(procConfig) procConfig.HasName("CompanyInsert"))
                    config.Update(Function(procConfig) procConfig.HasName("CompanyUpdate"))
                End Sub
            )
        End Sub
    End Class
End Namespace