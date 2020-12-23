namespace Chapter7.CSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);

            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        NickName = c.String(nullable: false, maxLength: 40),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
        }

        public override void Down()
        {
            DropTable("dbo.People");
            DropTable("dbo.Companies");
        }
    }
}
