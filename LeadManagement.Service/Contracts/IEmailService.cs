using System.Threading.Tasks;

namespace LeadManagement.Service.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string message);
    }
}
