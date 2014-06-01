using System.Security.Claims;
using System.Threading.Tasks;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;

namespace LeadManagement.Service.Contracts
{
    public interface IUserService
    {
        Task<User> FindAsync(string email, string password);
        Task<User> FindByNameAsync(string email);
        Task<ValidationResultViewModel> CreateAsync(User user, string password);
        Task<ValidationResultViewModel> ConfirmEmailAsync(string userId, string code);
        Task<string> GeneratePasswordResetTokenAsync(string userId);
        Task SendEmailAsync(string id, string subject, string message);
        Task<ValidationResultViewModel> ResetPasswordAsync(string userId, string code, string password);
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);
        Task<ValidationResultViewModel> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<User> FindByIdAsync(string userId);
    }
}
