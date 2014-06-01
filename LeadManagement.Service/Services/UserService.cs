using System.Security.Claims;
using System.Threading.Tasks;
using LeadManagement.Core.Contracts.Mapping;
using LeadManagement.Data.Contracts;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;
using LeadManagement.Service.Contracts;
using Microsoft.AspNet.Identity;

namespace LeadManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public UserService(IMapper mapper, IUserRepository userRepository, IEmailService emailService, IUserTokenProvider<User, string> userTokenProvider)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userManager = new UserManager<User>(userRepository) { UserTokenProvider = userTokenProvider };
            _emailService = emailService;
        }

        public async Task<User> FindAsync(string email, string password)
        {
            var result = await _userManager.FindAsync(email, password);
            return result;
        }

        public async Task<User> FindByNameAsync(string email)
        {
            var result = await _userManager.FindByNameAsync(email);
            return result;
        }

        public async Task<ValidationResultViewModel> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return _mapper.Map<IdentityResult, ValidationResultViewModel>(result);
        }

        public async Task<ValidationResultViewModel> ConfirmEmailAsync(string userId, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return _mapper.Map<IdentityResult, ValidationResultViewModel>(result);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            var result = await _userManager.GeneratePasswordResetTokenAsync(userId);
            return result;
        }

        public async Task SendEmailAsync(string userId, string subject, string message)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
                await _emailService.SendEmailAsync(user.Email, subject, message);
        }

        public async Task<ValidationResultViewModel> ResetPasswordAsync(string userId, string code, string password)
        {
            var result = await _userManager.ResetPasswordAsync(userId, code, password);
            return _mapper.Map<IdentityResult, ValidationResultViewModel>(result);
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            var result = await _userManager.CreateIdentityAsync(user, authenticationType);
            return result;
        }

        public async Task<ValidationResultViewModel> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            return _mapper.Map<IdentityResult, ValidationResultViewModel>(result);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            return result;
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            return result;
        }
    }
}
