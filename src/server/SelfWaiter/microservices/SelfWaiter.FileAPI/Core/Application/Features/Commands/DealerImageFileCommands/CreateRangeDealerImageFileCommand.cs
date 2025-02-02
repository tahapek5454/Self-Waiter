using MediatR;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.FileAPI.Core.Application.Utilities.Consts;
using SelfWaiter.FileAPI.Core.Domain;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands
{
    public class CreateRangeDealerImageFileCommand: IRequest<bool>
    {
        public Guid RelationId { get; set; }
        public IFormFileCollection? FormFileCollection { get; set; }

        public class CreateRangeDealerImageFileCommandHandler(
            IStorageService _storageService,
            IDealerImageFileRepository _dealerImageFileRepository,
            IIntegrationBus _bus) : IRequestHandler<CreateRangeDealerImageFileCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeDealerImageFileCommand request, CancellationToken cancellationToken)
            {
                var result = await _storageService.UploadAsync(PathOrContainerNames.DealerImages, request.FormFileCollection);
                List<DealerImageFile> dealerImageFiles = new();
                result.ForEach(async item =>
                {
                    var id = Guid.NewGuid();

                    dealerImageFiles.Add(new DealerImageFile()
                    {
                        Id = id,
                        FileName = item.fileName,
                        Path = item.pathOrContainerName,
                        Storage = _storageService.StorageName,
                        RelationId = request.RelationId
                    });

                    await _bus.SendAsync(new DealerImageFileChangedStartedEvent()
                    {
                        FileId = id,
                        FileName = item.fileName,
                        Path = item.pathOrContainerName,
                        Storage = _storageService.StorageName,
                        RelationId = request.RelationId,
                        OperationType = Shared.Core.Domain.Enums.OpeartionTypeEnum.Create
                    });
                   
                });

                if (dealerImageFiles.Any())
                {
                    await _dealerImageFileRepository.CreateRangeAsync(dealerImageFiles);
                }
                return true;
            }
        }
    }
}
