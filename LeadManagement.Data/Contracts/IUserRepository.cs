using LeadManagement.Model.Domain;
using Microsoft.AspNet.Identity;

namespace LeadManagement.Data.Contracts
{
    public interface IUserRepository : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
    {
    }
}
