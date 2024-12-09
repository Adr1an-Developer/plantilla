using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Data.Abstractions
{
    public interface IEfDbContext : IUnitOfWork
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

    }
}
