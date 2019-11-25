using CsvHelper.Configuration.Attributes;

namespace IpLocator.Models.CsvModels
{
    public class CityLocation
    {
        [Name("geoname_id")]
        public string GeonameId { get; set; }

        [Name("locale_code")]
        public string LocaleCode { get; set; }

        [Name("continent_code")]
        public string ContinentCode { get; set; }

        [Name("continent_name")]
        public string ContinentName { get; set; }

        [Name("country_iso_code")]
        public string CountryIsoCode { get; set; }

        [Name("country_name")]
        public string CountryName { get; set; }

        [Name("city_name")]
        public string CityName { get; set; }

        [Name("time_zone")]
        public string TimeZone { get; set; }

        [Name("is_in_european_union")]
        public bool IsInEuropeanUnion { get; set; }
    }
}
