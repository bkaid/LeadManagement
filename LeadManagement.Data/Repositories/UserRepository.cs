using System.Data.Entity;
using System.Threading.Tasks;
using LeadManagement.Data.Contracts;
using LeadManagement.Model.Domain;
using Microsoft.AspNet.Identity;

namespace LeadManagement.Data.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationDbContext>, IUserRepository
    {
        private readonly IUserStore<User> _userStore;

        public UserRepository(ApplicationDbContext context, IUserStore<User> userStore)
            : base(context)
        {
            _userStore = userStore;
        }

        public void Dispose()
        {
            if (_userStore != null)
                _userStore.Dispose();
        }

        public async Task CreateAsync(User user)
        {
            await _userStore.CreateAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userStore.UpdateAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            await _userStore.DeleteAsync(user);
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _userStore.FindByIdAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userStore.FindByNameAsync(userName);
        }

        #region IUserPasswordStore<User>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user != null)
                user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user == null ? null : user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(user != null && user.PasswordHash != null);
        }
        #endregion

        #region IUserEmailStore<User>
        public Task SetEmailAsync(User user, string email)
        {
            if (user != null)
                user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult(user == null ? null : user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(user != null && user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            if (user != null)
                user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        #endregion
    }
}
