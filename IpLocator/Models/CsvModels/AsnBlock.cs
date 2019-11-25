using CsvHelper.Configuration.Attributes;

namespace IpLocator.Models.CsvModels
{
    public class AsnBlock
    {
        [Name("network")]
        public string Network { get; set; }

        [Name("autonomous_system_number")]
        public int? AutonomousSystemNumber { get; set; }

        [Name("autonomous_system_organization")]
        public string AutonomousSystemOrganization { get; set; }
    }
}
