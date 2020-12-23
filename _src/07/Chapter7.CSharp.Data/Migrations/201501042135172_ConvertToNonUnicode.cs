namespace Chapter7.CSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ConvertToNonUnicode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.People", "NickName", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.People", "NickName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Companies", "Name", c => c.String());
        }
    }
}
