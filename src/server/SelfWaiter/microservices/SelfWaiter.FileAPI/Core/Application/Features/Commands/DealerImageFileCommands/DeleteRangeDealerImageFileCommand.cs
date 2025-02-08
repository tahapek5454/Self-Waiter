using MediatR;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.FileAPI.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands
{
    public class DeleteRangeDealerImageFileCommand: IRequest<bool>
    {
        public IEnumerable<string> FileNames { get; set; }

        public class DeleteRangeDealerImageFileCommandHandler(IStorageService _storageService, IDealerImageFileRepository _dealerImageFileRepository, IIntegrationBus _bus) : IRequestHandler<DeleteRangeDealerImageFileCommand, bool>
        {
            public async Task<bool> Handle(DeleteRangeDealerImageFileCommand request, CancellationToken cancellationToken)
            {
                var files = _dealerImageFileRepository.Where(x => request.FileNames.Contains(x.FileName)).ToList();

                foreach (var item in files)
                {
                    await _bus.SendAsync(new DealerImageFileChangedStartedEvent()
                    {
                        FileId = item.Id,
                        FileName = item.FileName,
                        Path = item.Path,
                        Storage = _storageService.StorageName,
                        RelationId = item.RelationId,
                        OperationType = Shared.Core.Domain.Enums.OpeartionTypeEnum.Delete
                    });
                }


                return await Task.FromResult<bool>(true);

                request.FileNames.Foreach(async item =>
                {
                    await _storageService.DeleteAsync(PathOrContainerNames.DealerImages, item);
                });

                var entites = _dealerImageFileRepository.Where(x => request.FileNames.Contains(x.FileName)).ToList();

                if (entites.Any())
                {
                    await _dealerImageFileRepository.DeleteRangeAsync(entites);
                }

                return true;
            }
        }
    }
}
