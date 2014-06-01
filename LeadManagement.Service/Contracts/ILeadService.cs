using System.Threading.Tasks;
using LeadManagement.Model.ViewModels;

namespace LeadManagement.Service.Contracts
{
    public interface ILeadService
    {
        Task<LeadViewModel> GetLeadForUserAsync(string userId);
        Task ProcessLeadAsync(int leadId, string userId);
    }
}
