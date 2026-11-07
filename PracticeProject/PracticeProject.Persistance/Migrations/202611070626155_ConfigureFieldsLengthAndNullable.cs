namespace PracticeProject.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureFieldsLengthAndNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.People", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Age", c => c.Int());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
        }
    }
}
