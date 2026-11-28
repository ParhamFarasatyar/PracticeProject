using System.Data.Entity.Migrations;
using PracticeProject.Persistence.Contexts;

namespace PracticeProject.Persistence.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<DataBaseContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(DataBaseContext context)
    {
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method
        //  to avoid creating duplicate seed data.
    }
}