using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FTS.Precatorio.Domain.Core;
using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Context;
using Microsoft.EntityFrameworkCore;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        private CoreContext Db;
        private DbSet<TEntity> DbSet;

        public Repository(CoreContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public bool IgnoreGroup
        {
            get { return Db.IgnoreGroup; }
            set { Db.IgnoreGroup = value; }
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        #region Operations
        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void SaveChanges()
        {
            Db.SaveChanges();
        }

        #endregion


        #region Consult
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            return Find<TEntity>(predicate, include);
        }

        protected IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include) where T : Entity<T>
        {
            if (predicate == null) return null;

            return QueryFind(include).Where(predicate).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            return await FindAsync<TEntity>(predicate, include);
        }

        protected async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include) where T : Entity<T>
        {
            if (predicate == null) return null;

            return await QueryFind(include).Where(predicate).ToListAsync();
        }

        private IQueryable<T> QueryFind<T>(params Expression<Func<T, object>>[] include) where T : Entity<T>
        {
            IQueryable<T> query;

            query = Db.Set<T>().AsNoTracking();

            if (IgnoreGroup)
                query = query.IgnoreQueryFilters();

            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        protected T Find<T>(Guid id) where T : Entity<T>
        {
            return Find<T>(x => x.Id == id).FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            if (IgnoreGroup)
            {
                return await DbSet.IgnoreQueryFilters().ToListAsync();
            }

            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id, params Expression<Func<TEntity, object>>[] include)
        {
            Expression<Func<TEntity, bool>> predicate = x => x.Id == id;

            return (await FindAsync(predicate, include)).FirstOrDefault();
        }
        #endregion

        public void SetGroupId(Guid groupId)
        {
            Db.SetGroupControl(groupId);
        }

        public virtual Guid GetGroupId()
        {
            return Db.GetGroupControl();
        }

        public ICoreContext GetContext()
        {
            return Db;
        }
    }
}