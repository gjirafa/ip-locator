using IpLocator.Models.CsvModels;

namespace IpLocator.Models.Wrappers
{
    public class CityWrapper
    {
        public long FirstIp { get; set; }
        public long LastIp { get; set; }
        public CityBlock CityBlock { get; set; }
    }
}
