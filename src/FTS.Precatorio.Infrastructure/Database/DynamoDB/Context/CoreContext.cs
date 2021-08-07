using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FTS.Precatorio.Infrastructure.User;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Context
{
    public abstract class CoreContext : DynamoDBContext, ICoreContext
    {
        public Ulid Id { get; }
        public bool IgnoreTenant { get; set; }
        private Guid _control_TenantId { get; set; }

        public CoreContext(IAmazonDynamoDB client) : base(client)
        {
            Id = Ulid.NewUlid();
        }

        public CoreContext(IUserToken token, IAmazonDynamoDB client) : this(client)
        {
            _control_TenantId = token.GetTenantId();
        }

        public T Add<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dynamic buff = entity;

            buff.DataInc = DateTime.UtcNow;
            buff.UsuInc = "adm";

            return buff;
        }

        public T Update<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dynamic buff = entity;

            // buff.DataAlter = DateTimeOffset.Now;
            // buff.UsuAlter = "adm";

            return buff;
        }

        public async Task SaveChangesAsync<T>(T value, CancellationToken cancellationToken)
        {
            bool saveFailed;
            int countFailBack = 0;
            do
            {
                saveFailed = false;

                if (countFailBack > 2) break;

                try
                {
                    await base.SaveAsync<T>(value, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    saveFailed = true;
                    countFailBack++;
                }
            } while (saveFailed);
        }

        public DynamoDBOperationConfig ConfigureTenantFilter()
        {
            if (IgnoreTenant) return null;

            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition> { new ScanCondition("PK", ScanOperator.Equal, _control_TenantId) }
            };

            return config;
        }

        public Task<TEntity> LoadAsync<TEntity>(Ulid id)
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
    }
}