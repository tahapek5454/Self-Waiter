using SelfWaiter.FileAPI.Core.Application.Services.Storage;

namespace SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Services.Storages
{
    public class StorageService(IStorage _storage) : IStorageService
    {
        public string StorageName => _storage.GetType().Name;

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => await _storage.UploadAsync(pathOrContainerName, files);
    }
}
