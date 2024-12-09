using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Services.Abstractions;

namespace ProjectLottery.V1.Domain.Services.Implementations
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repositoryBase;

        public ServiceBase(IGenericRepository<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase ?? throw new ArgumentNullException(nameof(repositoryBase));
        }


    }
}
