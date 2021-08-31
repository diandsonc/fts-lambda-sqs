using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Context;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace test.Unity.Configs
{
    public class ConfigDbMock
    {
        public static void ConfigContext<C, T>(Mock<C> context, IQueryable<T> query, Mock<DbSet<T>> dbSetMock) where C : FTSPrecatorioContext where T : class
        {
            dbSetMock.As<IAsyncEnumerable<T>>()
                .Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new MockAsyncEnumerator<T>(query.GetEnumerator()));

            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(new MockAsyncQueryProvider<T>(query.Provider));
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(query.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(query.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => query.GetEnumerator());

            dbSetMock.Setup(r => r.Add(It.IsAny<T>())).Callback<T>((s) => query.ToList().Add(s));
            dbSetMock.Setup(r => r.Remove(It.IsAny<T>())).Callback<T>((s) => query.ToList().Remove(s));

            context.Setup(r => r.Set<T>()).Returns(dbSetMock.Object);
        }
    }
}