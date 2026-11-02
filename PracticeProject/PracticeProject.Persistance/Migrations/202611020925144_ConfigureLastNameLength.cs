namespace PracticeProject.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureLastNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String());
        }
    }
}
