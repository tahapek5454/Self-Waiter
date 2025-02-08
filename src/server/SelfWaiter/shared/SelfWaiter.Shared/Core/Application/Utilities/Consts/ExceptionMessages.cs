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

        #region District
        public const string DistrictNotFound = "İstenilen ilçe bulunamadı.";
        public const string DistrictAlreadyExist = "Eklenmek istenin ilçe zaten mevcut";
        #endregion

        #region Dealer
        public const string DealerNotFound = "İstenilen şube bulunamadı.";
        public const string DealerImageNotFound = "İstenilen şube resmi bulunamadı.";
        public const string DealerAlreadyExist = "Eklenmek istenin şube isminde bir kayıt zaten mevcut";
        #endregion
    }
}
