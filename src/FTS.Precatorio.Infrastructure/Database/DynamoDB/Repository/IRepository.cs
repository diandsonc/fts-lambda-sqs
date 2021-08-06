using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Update(TEntity obj);
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> FindAsync(ScanFilter conditions, int limit, string paginationToken = null);
        Task<IEnumerable<TEntity>> GetAll(int limit, string paginationToken = null);
        ICoreContext GetContext();
        bool IgnoreGroup { get; set; }
    }
}