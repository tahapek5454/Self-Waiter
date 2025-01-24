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
    }
}
