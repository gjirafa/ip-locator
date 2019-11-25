namespace IpLocator.Models.Responses
{
    public class LocationResponse
    {
        public string LocaleCode { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryIsoCode { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string TimeZone { get; set; }
        public bool IsInEuropeanUnion { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? AccuracyRadius { get; set; }
        public string PostalCode { get; set; }
    }
}
