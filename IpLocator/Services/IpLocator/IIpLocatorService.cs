using IpLocator.Models.Responses;

namespace IpLocator.Services.IpLocator
{
    public interface IIpLocatorService
    {
        DetailsResponse Details(string ip);
        LocationResponse Location(string ip);
        AsnResponse Asn(string ip);
    }
}
