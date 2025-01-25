namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations
{
    public static class ValidationMessages
    {
        #region Country
        public const string CountryNameCanNotBeEmpty = "Ülke ismi girilmesi zorunludur.";
        public const string CountriesCanNotBeEmpty = "Ülke girilmesi zorunludur.";
        public const string CountryIdCanNotBeEmpty = "Ülke id girilmesi zorunludur.";
        #endregion

        #region City
        public const string CityIdCanNotBeEmpty = "Şehir id girilmesi zorunludur.";
        public const string CityNameCanNotBeEmpty = "Şehir ismi girilmesi zorunludur.";
        public const string City_CountryIdCanNotBeEmpty = "Şehir eklenirken ülke seçilmesi zorunludur.";
        public const string CitiesCanNotBeEmpty = "Şehir girilmesi zorunludur.";


        #endregion

        #region District
        public const string DistrictIdCanNotBeEmpty = "İlçe id girilmesi zorunludur.";
        public const string DistrictNameCanNotBeEmpty = "İlçe ismi girilmesi zorunludur.";
        public const string District_CityIdCanNotBeEmpty = "İlçe eklenirken şehir seçilmesi zorunludur.";
        public const string DistrictsCanNotBeEmpty = "İlçe girilmesi zorunludur.";
        #endregion

        #region Dealer
        public const string DealerIdCanNotBeEmpty = "Şube id girilmesi zorunludur.";
        public const string DealerNameCanNotBeEmpty = "Şube ismi girilmesi zorunludur.";
        public const string DealerNameMaxLength = "Şube ismi 75 karakterden büyük olamaz.";
        public const string Dealer_DistrictIdCanNotBeEmpty = "Şube eklenirken ilçe seçilmesi zorunludur.";
        public const string Dealer_CreatorUserIdCanNotBeEmpty = "Şube eklenirken kullanıcı girişi zorunludur.";
        public const string Dealer_AdressMaxLength = "Adres bilgisi maksimum 250 karakter olmalıdır.";
        public const string Dealer_PhoneNumberMaxLength = "Telefon numarası bilgisi maksimum 25 karakter olmalıdır.";
        #endregion
    }
}
