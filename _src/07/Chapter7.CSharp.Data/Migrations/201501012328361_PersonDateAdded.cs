namespace Chapter7.CSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PersonDateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "DateAdded",
                c => c.DateTime(nullable: false, defaultValueSql: "GetDate()"));
        }

        public override void Down()
        {
            DropColumn("dbo.People", "DateAdded");
        }
    }
}
