using System;
using System.Data.Entity;
using System.IO;

namespace LeadManagement.Data
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "leads.sql");
            if (File.Exists(path))
            {
                var sql = File.ReadAllText(path);
                context.Database.ExecuteSqlCommand(sql);
            }
        }

        public static void Initialize()
        {
            Database.SetInitializer(new SampleData());
            var context = new ApplicationDbContext();
            context.Database.Initialize(false);
        }
    }
}
