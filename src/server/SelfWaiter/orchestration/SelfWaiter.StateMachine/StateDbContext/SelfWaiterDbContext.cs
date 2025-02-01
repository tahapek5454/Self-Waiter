using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.StateMachine.StateMaps;

namespace SelfWaiter.StateMachine.StateDbContext
{
    public class SelfWaiterDbContext : SagaDbContext
    {
        public SelfWaiterDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new DealerImageFileChangedStateMap();
            }
        }
    }
}
