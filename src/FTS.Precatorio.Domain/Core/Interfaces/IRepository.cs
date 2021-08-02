using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;

namespace FTS.Precatorio.Domain.Core.Interfaces
{
    public interface IRepositoryDynamo<TEntity> : IDisposable, IRepositoryDynamo where TEntity : Entity<TEntity>
    {
        void Update(TEntity obj);
        void SaveChanges(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> FindAsync(ScanFilter conditions, int limit, string paginationToken = null);
        Task<IEnumerable<TEntity>> GetAll(int limit, string paginationToken = null);
    }

    public interface IRepository<TEntity> : IDisposable, IRepository where TEntity : Entity<TEntity>
    {
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        void SaveChanges();
        Task<TEntity> GetById(Guid id, params Expression<Func<TEntity, object>>[] include);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);
        Task<IEnumerable<TEntity>> GetAll();
    }

    public interface IRepository : IRepositoryBase
    {
        ICoreContext GetContext();
    }

    public interface IRepositoryDynamo : IRepositoryBase
    {
        ICoreContextDynamo GetContext();
    }

    public interface IRepositoryBase
    {
        bool IgnoreGroup { get; set; }
        void SetGroupId(Guid groupId);
        Guid GetGroupId();
    }
}