using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.Interface
{
    public interface IRepository<TAggregate, TKey>
    {
        Task<TAggregate?> TryFindAsync(TKey key);
        void Add(TAggregate aggregate);
        void Delete(TAggregate aggregate);
        public async Task Delete(TKey key)
        {
            var aggregate = await TryFindAsync(key);
            Guard.Against.Null(aggregate, nameof(aggregate), $"Aggregate with key {key} not found.");
            Delete(aggregate);
        }
        Task<bool> AnyAsync(TKey key);
    }
}
