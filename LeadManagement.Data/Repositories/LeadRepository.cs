using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using LeadManagement.Data.Contracts;
using LeadManagement.Model.Domain;

namespace LeadManagement.Data.Repositories
{
    public class LeadRepository : RepositoryBase<ApplicationDbContext>, ILeadRepository
    {
        public LeadRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Get the current lead for the user.
        /// </summary>
        public async Task<Lead> GetLeadAsync(string userId)
        {
            // use a transaction scope that will perform a database lock on the records that are read until they can be updated.
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead }, TransactionScopeAsyncFlowOption.Enabled))
            {
                // get a lead that is locked by the current user or grab an available lead
                var query = from l in Context.Leads
                            where !l.IsProcessed && (l.LockedBy == null || l.LockedBy == userId)
                            orderby l.LockedBy == userId descending
                            select l;

                var lead = await query.FirstOrDefaultAsync();

                if (lead != null && lead.LockedBy != userId)
                {
                    // mark the record as assigned to a user.
                    lead.LockedBy = userId;
                    await Context.SaveChangesAsync();
                }

                // clear the database lock on the record.
                transactionScope.Complete();
                return lead;
            }
        }

        /// <summary>
        /// Mark a lead as processed if the row is still locked by the user.
        /// </summary>
        public async Task ProcessLeadAsync(int leadId, string userId)
        {
            var lead = await Context.Leads.FirstOrDefaultAsync(p => p.Id == leadId && p.LockedBy == userId && !p.IsProcessed);
            if (lead != null)
            {
                lead.IsProcessed = true;
                lead.LockedBy = null;
                await Context.SaveChangesAsync();
            }
        }
    }
}
