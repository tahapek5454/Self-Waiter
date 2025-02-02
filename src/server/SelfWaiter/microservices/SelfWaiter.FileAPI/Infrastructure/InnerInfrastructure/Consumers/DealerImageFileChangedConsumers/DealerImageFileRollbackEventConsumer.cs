using MassTransit;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.FileAPI.Core.Application.Mapper;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Consumers.DealerImageFileChangedConsumers
{
    public class DealerImageFileRollbackEventConsumer(
        IDealerImageFileRepository _dealerImageFileRepository,
        IFileUnitOfWork _unitOfWork,
        IStorageService _storageService,
        IIntegrationBus _bus,
        ILogger<DealerImageFileRollbackEventConsumer> _logger) : IConsumer<DealerImageFileRollbackEvent>
    {
        public async Task Consume(ConsumeContext<DealerImageFileRollbackEvent> context)
        {
            _logger.LogInformation("{requestNmae} consume started.", nameof(DealerImageFileRollbackEventConsumer));

            var dealerImageFile = await _dealerImageFileRepository.Query().FirstOrDefaultAsync(x => x.FileName.Equals(context.Message.FileName));

            if (dealerImageFile == null) return;

            await _dealerImageFileRepository.DeleteAsync(dealerImageFile);
            await _unitOfWork.SaveChangesAsync();

            await _storageService.DeleteAsync(context.Message.Path, context.Message.FileName);

            var @event = ObjectMapper.Mapper.Map<DealerImageFileRollbackReceivedEvent>(context.Message);
            await _bus.SendAsync(@event);

            _logger.LogInformation("{requestNmae} consume successfully ended.", nameof(DealerImageFileRollbackEventConsumer));

        }
    }
}
