using IpLocator.Models.Responses;
using IpLocator.Services.IpLocator;
using Microsoft.AspNetCore.Mvc;

namespace IpLocator.Controllers
{
    [ApiController]
    public class IpLocatorController : ControllerBase
    {
        private readonly IIpLocatorService _ipLocatorService;

        public IpLocatorController(
            IIpLocatorService ipLocatorService)
        {
            _ipLocatorService = ipLocatorService;
        }

        [Route("/")]
        public string Index()
        {
            return "Ip Locator from Gjirafa, Inc.";
        }

        [Route("/details/{ip}")]
        public DetailsResponse Details(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return null;

            return _ipLocatorService.Details(ip);
        }

        [Route("/location/{ip}")]
        public LocationResponse Location(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return null;

            return _ipLocatorService.Location(ip);
        }

        [Route("/asn/{ip}")]
        public AsnResponse Asn(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return null;

            return _ipLocatorService.Asn(ip);
        }
    }
}