using MediatR;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Domain.Abstract;
using SelfWaiter.Shared.Core.Application.Extension;

namespace SelfWaiter.FileAPI.Core.Application.Behaviors.Dispatchers
{
    public class DomainEventsDispatcher(IMediator _mediator, IFileUnitOfWork _dealerUnitOfWork) : IDomainEventsDispatcher
    {
        public async Task DispatchEventsAsync()
        {
            var domainEntities = (await _dealerUnitOfWork.GetEntitiesAsync())
                                            .OfType<NotifiableEntity>()
                                            .Where(x => x.HasDomainEvents());


            if (domainEntities?.Any() != true)
                return;

            var domainEvents = domainEntities.SelectMany(x => x.GetDomainEvents()).ToList();

            domainEntities.Foreach(x => x.ClearDomainEvents());

            var tasks = domainEvents.Select(x => _mediator.Publish(x));

            await Task.WhenAll(tasks);
        }
    }

    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
