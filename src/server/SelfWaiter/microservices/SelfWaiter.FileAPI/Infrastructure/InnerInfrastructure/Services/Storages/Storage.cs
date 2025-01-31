namespace SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Services.Storages
{
    public class Storage
    {
        private int Counter { get; set; } = 1;

        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string path, string fileName, HasFile hasFileMethod, bool first = true)
        {

            string newFileName = await Task.Run<string>(async () =>
            {
                string extenitons = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);

                if (!first)
                {
                    int idx = oldName.LastIndexOf('-');
                    string before = oldName.Substring(0, idx);
                    oldName = $"{before}-{Counter}";
                }

                string newFileName = NameOperations.CharecterRegulatory(oldName) + extenitons;

                if (hasFileMethod(path, newFileName))
                {
                    Counter = Counter + 1;
                    if (first)
                        return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}-{Counter}{Path.GetExtension(newFileName)}", hasFileMethod, false);
                    return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}{Path.GetExtension(newFileName)}", hasFileMethod, false);
                }
                else
                {
                    Counter = 1;
                    return newFileName;
                }
            });

            return newFileName;
        }
    }
}
