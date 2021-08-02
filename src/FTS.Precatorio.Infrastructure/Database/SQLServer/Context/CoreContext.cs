using System;
using System.Linq;
using System.Reflection;
using FTS.Precatorio.Domain.Core;
using FTS.Precatorio.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Context
{
    public abstract class CoreContext : DbContext, ICoreContext
    {
        public Guid Id { get; }
        public bool IgnoreGroup { get; set; }
        private Guid _control_GroupId { get; set; }

        public CoreContext()
        {
            Id = Guid.NewGuid();
        }
        
        public CoreContext(IUserToken token) : this()
        {
            _control_GroupId = token.GetControlId();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configureTenantMethod = GetType().BaseType.GetTypeInfo().DeclaredMethods.Single(m => m.Name == nameof(ConfigureTenantFilter));
            var args = new object[] { modelBuilder };
            var tenantEntityTypes = modelBuilder.Model.GetEntityTypes().Where(t => !(typeof(IEntityControlGroup).IsAssignableFrom(t.ClrType)));

            foreach (var entityType in tenantEntityTypes)
            {
                configureTenantMethod.MakeGenericMethod(entityType.ClrType).Invoke(this, args);
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public void ConfigureTenantFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : Entity<TEntity>
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(e => IgnoreGroup || e.Control_GrupoId == GetGroupControl());
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
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

            return base.Add(entity);
        }

        public override EntityEntry Add(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Add<object>(entity);
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dynamic buff = entity;

            buff.Control_DataAlter = DateTimeOffset.Now;
            buff.Control_UsuAlter = "adm";

            var entry = base.Update(entity);

            //cancell update transactions ADD fields
            entry.Property("Control_UsuInc").IsModified = false;
            entry.Property("Control_DataInc").IsModified = false;

            return entry;
        }

        public override EntityEntry Update(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Update<object>(entity);
        }

        public override int SaveChanges()
        {
            bool saveFailed;
            int countFailBack = 0;
            int dataReturn = 0;
            do
            {
                saveFailed = false;

                if (countFailBack > 2)
                {
                    break;
                }

                try
                {
                    dataReturn = base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    saveFailed = true;

                    // Update original values from the database
                    var entry = ex.Entries.Single();

                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    countFailBack++;
                }
            } while (saveFailed);

            return dataReturn;
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