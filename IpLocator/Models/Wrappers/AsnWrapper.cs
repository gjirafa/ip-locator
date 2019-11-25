using IpLocator.Models.CsvModels;

namespace IpLocator.Models.Wrappers
{
    public class AsnWrapper
    {
        public long FirstIp { get; set; }
        public long LastIp { get; set; }
        public AsnBlock AsnBlock { get; set; }
    }
}
