using SelfWaiter.FileAPI.Core.Application.Services.Storage.Local;

namespace SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Services.Storages.Locals
{
    public class LocalStorage(IWebHostEnvironment _webHostEnviroment) : Storage, ILocalStorage
    {    
        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directory = new DirectoryInfo(pathOrContainerName);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            var mainPah = Path.Combine(_webHostEnviroment.WebRootPath, pathOrContainerName);
            var fullPath = Path.Combine(mainPah, fileName);
            return System.IO.File.Exists(fullPath);
        }


        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            string mainPah = Path.Combine(_webHostEnviroment.WebRootPath, pathOrContainerName);
            System.IO.File.Delete(Path.Combine(mainPah, fileName));

            return Task.CompletedTask;
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnviroment.WebRootPath, pathOrContainerName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName, HasFile);

                _ = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);

                datas.Add((fileNewName, Path.Combine(pathOrContainerName, fileNewName)));

            }

            return datas;

        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            return true;
        }
    }
}
