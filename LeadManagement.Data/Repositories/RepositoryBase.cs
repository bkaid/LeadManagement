using System.Data.Entity;

namespace LeadManagement.Data.Repositories
{
    public abstract class RepositoryBase<T> where T: DbContext
    {
        private readonly T _dbContext;

        protected RepositoryBase(T dbContext)
        {
            _dbContext = dbContext;
        }

        protected T Context { get { return _dbContext; } }
    }
}
