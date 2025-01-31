namespace SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Services
{
    public static class NameOperations
    {
        public static string CharecterRegulatory(string name)
        {
            return name.Replace('İ', 'I')
                        .Replace('Ü', 'U')
                        .Replace('Ş', 'S')
                        .Replace('Ç', 'C')
                        .Replace('Ö', 'O')
                        .Replace('Ğ', 'G')
                        .Replace('ü', 'u')
                        .Replace('ş', 's')
                        .Replace('ç', 'c')
                        .Replace('ı', 'i')
                        .Replace('ö', 'o')
                        .Replace('ğ', 'g');
        }
    }
}
