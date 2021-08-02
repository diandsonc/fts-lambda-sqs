using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using FTS.Precatorio.Domain.Core;
using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
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

        public virtual void Update(TEntity obj)
        {
            var cancellationToken = new CancellationTokenSource();

            obj = Db.Update<TEntity>(obj);

            Db.SaveChanges<TEntity>(obj, cancellationToken.Token);
        }

        public void Add(TEntity obj)
        {
            var cancellationToken = new CancellationTokenSource();

            obj = Db.Add<TEntity>(obj);

            Db.SaveChanges<TEntity>(obj, cancellationToken.Token);
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