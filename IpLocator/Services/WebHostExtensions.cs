using IpLocator.Services.IpLocator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IpLocator.Services
{
    public static class WebHostExtensions
    {
        public static IHost InitIp(this IHost host)
        {
            // This methods calls the ip locator service for an IP addres info
            // just for the purpose to load the IP ranges into memory
            // so that it doesn't have to wait a long time in the first request

            var ipLocatorService = host.Services.GetService<IIpLocatorService>();
            ipLocatorService.Details("1.1.1.1");
            return host;
        }
    }
}
