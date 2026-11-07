namespace PracticeProject.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueIndexToUserPersonId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "PersonId", unique: true, name: "IX_User_PersonId");
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastName", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropIndex("dbo.Users", "IX_User_PersonId");
            DropColumn("dbo.Users", "PersonId");
            DropTable("dbo.People");
        }
    }
}
