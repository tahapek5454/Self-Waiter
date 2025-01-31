using MediatR;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.FileAPI.Core.Application.Utilities.Consts;
using SelfWaiter.FileAPI.Core.Domain;

namespace SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands
{
    public class CreateRangeDealerImageFileCommand: IRequest<bool>
    {
        public Guid RelationId { get; set; }
        public IFormFileCollection? FormFileCollection { get; set; }

        public class CreateRangeDealerImageFileCommandHandler(IStorageService _storageService, IDealerImageFileRepository _dealerImageFileRepository) : IRequestHandler<CreateRangeDealerImageFileCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeDealerImageFileCommand request, CancellationToken cancellationToken)
            {
                var result = await _storageService.UploadAsync(PathOrContainerNames.DealerImages, request.FormFileCollection);

                List<DealerImageFile> dealerImageFiles = new();
                result.ForEach(item =>
                {
                    dealerImageFiles.Add(new()
                    {
                        FileName = item.fileName,
                        Path = item.pathOrContainerName,
                        Storage = _storageService.StorageName,
                        RelationId = request.RelationId              
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
