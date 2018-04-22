namespace TestInvoice.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestInvoice.Context.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestInvoice.Context.AppContext context)
        {
            #region inserted data
            context.Customers.AddOrUpdate(
                new Models.Customer { Id = 1, Fname = "کمال", Lname = "منصوری", RegDate = DateTime.Now.AddYears(-25) },
                new Models.Customer { Id = 2, Fname = "فرزین", Lname = "ملکی", RegDate = DateTime.Now.AddYears(-40) }
                );

            context.Commodities.AddOrUpdate(
                new Models.Commodity { Id = 1, Title = "ماست پرچرب", RegDate = DateTime.Now.AddYears(-25) },
                new Models.Commodity { Id = 2, Title = "کره حیوانی", RegDate = DateTime.Now.AddYears(-40) }
                );

            context.Stores.AddOrUpdate(
                new Models.Store { Id = 1, Title = "انبار مرکزی" }
                );
            #endregion

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
