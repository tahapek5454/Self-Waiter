using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.StateMachine.StateInstances;

namespace SelfWaiter.StateMachine.StateMaps
{
    public class DealerImageFileChangedStateMap: SagaClassMap<DealerImageFileChangedStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<DealerImageFileChangedStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.OperationType).IsRequired();
            entity.Property(x => x.Storage).IsRequired();
            entity.Property(x => x.FileId).IsRequired();
            entity.Property(x => x.FileName).IsRequired();
            entity.Property(x => x.Path).IsRequired();

            base.Configure(entity, model);
        }
    }
}
