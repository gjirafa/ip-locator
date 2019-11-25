using System;

namespace IpLocator.Models
{
    public class IpBlock
    {
        public long FirstIp { get; }
        public long LastIp { get; }

        public IpBlock(string network)
        {
            if (string.IsNullOrEmpty(network))
                throw new ArgumentNullException(nameof(network));

            var parts = network.Split('.', '/');
            var ipNumber = (Convert.ToUInt32(parts[0]) << 24) | (Convert.ToUInt32(parts[1]) << 16) | (Convert.ToUInt32(parts[2]) << 8) | Convert.ToUInt32(parts[3]);

            var maskBits = Convert.ToInt32(parts[4]);
            var mask = 0xffffffff;
            mask <<= (32 - maskBits);

            FirstIp = ipNumber & mask;
            LastIp = ipNumber | (mask ^ 0xffffffff);
        }
    }
}
