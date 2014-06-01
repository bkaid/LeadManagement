using System.Data.Entity.ModelConfiguration;
using LeadManagement.Model.Domain;

namespace LeadManagement.Data.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(c => c.Email).IsRequired();
        }
    }
}
