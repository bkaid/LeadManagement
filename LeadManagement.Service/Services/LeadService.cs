using System;
using System.Threading.Tasks;
using LeadManagement.Core.Contracts.Mapping;
using LeadManagement.Data.Contracts;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;
using LeadManagement.Service.Contracts;

namespace LeadManagement.Service.Services
{
    public class LeadService : ILeadService
    {
        private readonly IMapper _mapper;
        private readonly ILeadRepository _leadRepository;

        public LeadService(IMapper mapper, ILeadRepository leadRepository)
        {
            _mapper = mapper;
            _leadRepository = leadRepository;
        }

        /// <summary>
        /// Retrieve a lead for a user from, locking the lead.
        /// </summary>
        /// <param name="userId">Current logged in user.</param>
        /// <returns>A locked lead record.</returns>
        public async Task<LeadViewModel> GetLeadForUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("userId", "User is required.");
            
            var lead = await _leadRepository.GetLeadAsync(userId);
            return _mapper.Map<Lead, LeadViewModel>(lead);
        }

        public async Task ProcessLeadAsync(int leadId, string userId)
        {
            await _leadRepository.ProcessLeadAsync(leadId, userId);
        }
    }
}
