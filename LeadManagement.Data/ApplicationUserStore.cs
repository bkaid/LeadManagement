using LeadManagement.Model.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeadManagement.Data
{
    public class ApplicationUserStore : UserStore<User>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
    }
}
