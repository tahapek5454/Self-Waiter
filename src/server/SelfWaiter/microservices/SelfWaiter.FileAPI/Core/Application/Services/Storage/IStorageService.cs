namespace SelfWaiter.FileAPI.Core.Application.Services.Storage
{
    public interface IStorageService: IStorage
    {
        public string StorageName { get; }
    }
}
