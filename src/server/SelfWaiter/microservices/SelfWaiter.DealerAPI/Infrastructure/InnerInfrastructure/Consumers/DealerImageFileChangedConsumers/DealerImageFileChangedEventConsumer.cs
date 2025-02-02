using MassTransit;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Services;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Consumers.DealerImageFileChangedConsumers
{
    public class DealerImageFileChangedEventConsumer(
        IDealerRepository _dealerRepository,
        IDealerUnitOfWork _unitOfWork,
        IIntegrationBus _bus,
        ILogger<DealerImageFileChangedEventConsumer> _logger) : IConsumer<DealerImageFileChangedEvent>
    {
        public async Task Consume(ConsumeContext<DealerImageFileChangedEvent> context)
        {
            _logger.LogInformation("Consumer - consume started {requestName} for correlationId : {correlationId}", nameof(DealerImageFileChangedEventConsumer), context.Message.CorrelationId);

            var dealer = await _dealerRepository
                .Query()
                .Include(x => x.DealerImages)
                .FirstOrDefaultAsync(x => x.Id.Equals(context.Message.RelationId));

            if(dealer is null)
            {
                _logger.LogError("Consumer - consume error dealer not found {requestName} for correlationId : {correlationId}", nameof(DealerImageFileChangedEventConsumer), context.Message.CorrelationId);
                throw new SelfWaiterException(ExceptionMessages.DealerNotFound);
            }

            dealer.DealerImages ??= new();
            var dealerImage = ObjectMapper.Mapper.Map<DealerImage>(context.Message);
            dealer.DealerImages.Add(dealerImage);
            await _unitOfWork.SaveChangesAsync();

            var successEvent = ObjectMapper.Mapper.Map<DealerImageFileReceivedEvent>(context.Message);
            await _bus.SendAsync(successEvent);
            _logger.LogInformation("Consumer - consume successfully ended {requestName} for correlationId : {correlationId}", nameof(DealerImageFileChangedEventConsumer), context.Message.CorrelationId);

        }
    }
}
