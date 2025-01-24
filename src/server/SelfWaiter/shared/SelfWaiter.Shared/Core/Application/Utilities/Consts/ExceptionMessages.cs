namespace SelfWaiter.Shared.Core.Application.Utilities.Consts
{
    public static class ExceptionMessages
    {
        #region Global
        public const string InconsistencyExceptionMessage = "İstenilen durum ile beklenen durum tutarlı değildir.";
        public const string InvalidQuery = "Beklenmedik bir istek ile karşılaşıldı.";
        #endregion

        #region Country
        public const string CountryNotFound = "İstenilen ülke bulunamadı.";
        public const string CountryAlreadyExist = "Eklenmek istenin ülke zaten mevcut";
        #endregion

        #region City
        public const string CityNotFound = "İstenilen şehir bulunamadı.";
        public const string CityAlreadyExist = "Eklenmek istenin şehir zaten mevcut";
        #endregion
    }
}
