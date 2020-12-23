Imports System.Data.Entity
Imports Chapter7.VB.Data.Migrations

Public Class Initializer
    inherits MigrateDatabaseToLatestVersion(Of Context, Configuration)
End Class
