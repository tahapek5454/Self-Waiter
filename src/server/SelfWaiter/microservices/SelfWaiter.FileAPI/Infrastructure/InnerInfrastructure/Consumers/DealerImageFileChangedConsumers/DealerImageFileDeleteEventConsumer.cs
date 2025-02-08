using MassTransit;
using SelfWaiter.FileAPI.Core.Application.Mapper;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Consumers.DealerImageFileChangedConsumers
{
    public class DealerImageFileDeleteEventConsumer(IStorageService _storageService, IDealerImageFileRepository _dealerImageFileRepository, IIntegrationBus _bus, IFileUnitOfWork _fileUnitOfWork) : IConsumer<DealerImageFileDeleteEvent>
    {
        public async Task Consume(ConsumeContext<DealerImageFileDeleteEvent> context)
        {
          
            await _storageService.DeleteAsync(context.Message.Path, context.Message.FileName);
          

            var entity = _dealerImageFileRepository.Query().FirstOrDefault(x => x.FileName.Equals(context.Message.FileName));

            if (entity!=null)
            {
                await _dealerImageFileRepository.DeleteAsync(entity);

                await _fileUnitOfWork.SaveChangesAsync();
            }

            var @event = ObjectMapper.Mapper.Map<DealerImageFileDeleteReceivedEvent>(context.Message);

            await _bus.SendAsync(@event);
        }
    }
}
