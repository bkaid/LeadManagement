using System.Data.Entity.ModelConfiguration;
using LeadManagement.Model.Domain;

namespace LeadManagement.Data.Configuration
{
    internal class LeadConfiguration : EntityTypeConfiguration<Lead>
    {
        public LeadConfiguration()
        {
            HasKey(p => p.Id);
        }
    }
}
