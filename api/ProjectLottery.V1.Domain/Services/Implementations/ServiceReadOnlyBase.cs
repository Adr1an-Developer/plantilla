using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Services.Abstractions;

namespace ProjectLottery.V1.Domain.Services.Implementations
{
    public class ServiceReadOnlyBase<TEntity> : IServiceReadOnlyBase<TEntity> where TEntity : class
    {
        protected readonly IGenericReadOnlyRepository<TEntity> _repository;

        public ServiceReadOnlyBase(IGenericReadOnlyRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}