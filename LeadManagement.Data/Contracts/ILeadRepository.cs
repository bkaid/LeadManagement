using System.Threading.Tasks;
using LeadManagement.Model.Domain;

namespace LeadManagement.Data.Contracts
{
    public interface ILeadRepository
    {
        Task<Lead> GetLeadAsync(string userId);
        Task ProcessLeadAsync(int leadId, string userId);
    }
}
