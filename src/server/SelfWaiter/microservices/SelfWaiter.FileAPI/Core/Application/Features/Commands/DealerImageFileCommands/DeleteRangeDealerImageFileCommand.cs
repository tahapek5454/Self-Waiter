using MediatR;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.FileAPI.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Extension;

namespace SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands
{
    public class DeleteRangeDealerImageFileCommand: IRequest<bool>
    {
        public IEnumerable<string> FileNames { get; set; }

        public class DeleteRangeDealerImageFileCommandHandler(IStorageService _storageService, IDealerImageFileRepository _dealerImageFileRepository) : IRequestHandler<DeleteRangeDealerImageFileCommand, bool>
        {
            public async Task<bool> Handle(DeleteRangeDealerImageFileCommand request, CancellationToken cancellationToken)
            {
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
