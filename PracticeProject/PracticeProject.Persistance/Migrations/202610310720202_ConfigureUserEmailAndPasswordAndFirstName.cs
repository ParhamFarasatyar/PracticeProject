namespace PracticeProject.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureUserEmailAndPasswordAndFirstName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
        }
    }
}
