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
        #endregion

        #region City
        public const string CityNotFound = "İstenilen şehir bulunamadı.";
        #endregion
    }
}
