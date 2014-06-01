using System.Data.Entity;
using LeadManagement.Data.Configuration;
using LeadManagement.Model.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeadManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<Lead> Leads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new LeadConfiguration());
        }
    }
}
