using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Infrastructure.Wow;

namespace Tests.Core
{
    public partial class ConfigTest
    {
        public readonly IUnitOfWork UnitOfWork;

        public ConfigTest()
        {
            UnitOfWork = new UnitOfWork();
        }
    }
}