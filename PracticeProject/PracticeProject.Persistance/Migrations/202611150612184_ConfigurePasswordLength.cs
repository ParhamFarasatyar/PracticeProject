namespace PracticeProject.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigurePasswordLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
