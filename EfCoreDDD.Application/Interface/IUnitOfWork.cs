using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.Interface
{
    public interface IUnitOfWork
    {
        IRepository<TAggregate, TKey> GetRepository<TAggregate, TKey>() where TAggregate : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
