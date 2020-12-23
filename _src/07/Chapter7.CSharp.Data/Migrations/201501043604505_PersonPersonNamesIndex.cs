namespace Chapter7.CSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PersonPersonNamesIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex(
                "People",
                new[] { "LastName", "FirstName" },
                name: "IX_PERSON_NAMES");
        }

        public override void Down()
        {
            DropIndex("People", "IX_PERSON_NAMES");
        }
    }
}
