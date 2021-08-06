using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly CoreContext Db;

        public Repository(CoreContext context)
        {
            Db = context;
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

        public virtual async Task Update(TEntity obj)
        {
            var cancellationToken = new CancellationTokenSource();

            obj = Db.Update<TEntity>(obj);

            await Db.SaveChangesAsync<TEntity>(obj, cancellationToken.Token);
        }

        public async Task Add(TEntity obj)
        {
            var cancellationToken = new CancellationTokenSource();

            obj = Db.Add<TEntity>(obj);

            await Db.SaveChangesAsync<TEntity>(obj, cancellationToken.Token);
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await Db.LoadAsync<TEntity>(id);

            return data;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(ScanFilter conditions, int limit, string paginationToken = null)
        {
            if (conditions == null) return null;

            var data = await Db.FindAsync<TEntity>(conditions, limit, paginationToken).GetRemainingAsync();

            return data.AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(int limit, string paginationToken = null)
        {
            var data = await Db.FindAsync<TEntity>(limit, paginationToken).GetRemainingAsync();

            return data.AsEnumerable();
        }

        public ICoreContext GetContext()
        {
            return Db;
        }
    }
}