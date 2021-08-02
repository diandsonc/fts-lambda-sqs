using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FTS.Precatorio.Domain.Core.Interfaces;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Context
{
    public abstract class CoreContext : DynamoDBContext, ICoreContextDynamo
    {
        public Guid Id { get; }
        public bool IgnoreGroup { get; set; }
        private Guid _control_GroupId { get; set; }

        public CoreContext(IAmazonDynamoDB client) : base(client)
        {
            Id = Guid.NewGuid();
        }

        public CoreContext(IUserToken token, IAmazonDynamoDB client) : this(client)
        {
            _control_GroupId = token.GetControlId();
        }

        public T Add<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dynamic buff = entity;

            buff.Control_DataAlter = DateTimeOffset.Now;
            buff.Control_DataInc = DateTimeOffset.Now;
            buff.Control_UsuAlter = "adm";
            buff.Control_UsuInc = "adm";

            return buff;
        }

        public T Update<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dynamic buff = entity;

            buff.Control_DataAlter = DateTimeOffset.Now;
            buff.Control_UsuAlter = "adm";

            return buff;
        }

        public int SaveChanges<T>(T value, CancellationToken cancellationToken)
        {
            bool saveFailed;
            int countFailBack = 0;
            do
            {
                saveFailed = false;

                if (countFailBack > 2) break;

                try
                {
                    base.SaveAsync<T>(value, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    saveFailed = true;
                    countFailBack++;
                }
            } while (saveFailed);

            return !saveFailed ? 1 : 0;
        }

        public DynamoDBOperationConfig ConfigureTenantFilter()
        {
            if (IgnoreGroup) return null;

            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition> { new ScanCondition("Control_GrupoId", ScanOperator.Equal, GetGroupControl()) }
            };

            return config;
        }

        public Task<TEntity> LoadAsync<TEntity>(Guid id)
        {
            return LoadAsync<TEntity>(id, ConfigureTenantFilter());
        }

        public AsyncSearch<TEntity> FindAsync<TEntity>(int limit = 20, string paginationToken = null)
        {
            var config = new ScanOperationConfig { Limit = limit, PaginationToken = paginationToken };

            return FromScanAsync<TEntity>(config, ConfigureTenantFilter());
        }

        public AsyncSearch<TEntity> FindAsync<TEntity>(ScanFilter conditions, int limit = 20, string paginationToken = null)
        {
            var config = new ScanOperationConfig { Filter = conditions, Limit = limit, PaginationToken = paginationToken };

            return FromScanAsync<TEntity>(config, ConfigureTenantFilter());
        }

        public AsyncSearch<TEntity> FindAsync<TEntity>(IEnumerable<ScanCondition> conditions)
        {
            return ScanAsync<TEntity>(conditions, ConfigureTenantFilter());
        }

        public virtual Guid GetGroupControl()
        {
            return _control_GroupId;
        }

        public void SetGroupControl(Guid controlId)
        {
            _control_GroupId = controlId;
        }
    }
}