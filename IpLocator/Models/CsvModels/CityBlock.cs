using CsvHelper.Configuration.Attributes;

namespace IpLocator.Models.CsvModels
{
    public class CityBlock
    {
        [Name("network")]
        public string Network { get; set; }

        [Name("geoname_id")]
        public string GeonameId { get; set; }

        [Name("registered_country_geoname_id")]
        public string RegisteredCountryGeonameId { get; set; }

        [Name("represented_country_geoname_id")]
        public string RepresentedCountryGeonameId { get; set; }

        [Name("is_anonymous_proxy")]
        public bool IsAnonymousProxy { get; set; }

        [Name("is_satellite_provider")]
        public bool IsSatelliteProvider { get; set; }

        [Name("postal_code")]
        public string PostalCode { get; set; }

        [Name("latitude")]
        public decimal? Latitude { get; set; }

        [Name("longitude")]
        public decimal? Longitude { get; set; }

        [Name("accuracy_radius")]
        public int? AccuracyRadius { get; set; }
    }
}
