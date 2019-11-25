using IpLocator.Models;
using IpLocator.Models.CsvModels;
using IpLocator.Models.Responses;
using IpLocator.Models.Wrappers;
using IpLocator.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IpLocator.Services.IpLocator
{
    public class RedBlackTreeIpLocatorService : IIpLocatorService
    {
        private readonly IStorageService _storageService;
        private readonly RedBlackTree<AsnWrapper> _asnTree;
        private readonly RedBlackTree<CityWrapper> _cityTree;
        private readonly Dictionary<string, CityLocation> _cityLocationsDict;

        public RedBlackTreeIpLocatorService(
            IStorageService storageService)
        {
            _storageService = storageService;
            _asnTree = LoadAsnTree();
            _cityTree = LoadCityTree();
            _cityLocationsDict = LoadCityLocationsDict();
        }

        public LocationResponse Location(string ip)
        {
            var ipCasted = CastIp(ip);
            var floorIp = _cityTree.Floor(ipCasted);

            if (floorIp < 0)
                return null;

            var cityWrapper = _cityTree.Get(floorIp);
            if (cityWrapper == null)
                return null;

            if (ipCasted <= cityWrapper.FirstIp || ipCasted >= cityWrapper.LastIp)
                return null;

            var cityBlock = cityWrapper.CityBlock;
            var cityLocation = _cityLocationsDict.ContainsKey(cityBlock.GeonameId) ?
                                    _cityLocationsDict[cityBlock.GeonameId] : null;


            return new LocationResponse
            {
                LocaleCode = cityLocation?.LocaleCode,
                ContinentCode = cityLocation?.ContinentCode,
                ContinentName = cityLocation?.ContinentName,
                CountryIsoCode = cityLocation?.CountryIsoCode,
                CountryName = cityLocation?.CountryName,
                CityName = cityLocation?.CityName,
                TimeZone = cityLocation?.TimeZone,
                IsInEuropeanUnion = cityLocation != null && cityLocation.IsInEuropeanUnion,
                Latitude = cityBlock.Latitude,
                Longitude = cityBlock.Longitude,
                AccuracyRadius = cityBlock.AccuracyRadius,
                PostalCode = cityBlock.PostalCode
            };
        }

        public AsnResponse Asn(string ip)
        {
            var ipCasted = CastIp(ip);
            var floorIp = _asnTree.Floor(ipCasted);

            if (floorIp < 0)
                return null;

            var asnWrapper = _asnTree.Get(floorIp);
            if (asnWrapper == null)
                return null;

            if (ipCasted <= asnWrapper.FirstIp || ipCasted >= asnWrapper.LastIp)
                return null;

            var asnBlock = asnWrapper.AsnBlock;

            return new AsnResponse
            {
                AutonomousSystemNumber = asnBlock.AutonomousSystemNumber,
                AutonomousSystemOrganization = asnBlock.AutonomousSystemOrganization
            };
        }

        public DetailsResponse Details(string ip)
        {
            var location = Location(ip);
            var asn = Asn(ip);

            return new DetailsResponse
            {
                Location = location,
                Asn = asn
            };
        }

        private RedBlackTree<CityWrapper> LoadCityTree()
        {
            var cityBlocks = _storageService.ReadCityBlocks();
            var tree = new RedBlackTree<CityWrapper>();
            foreach (var cityBlock in cityBlocks)
            {
                var ipBlock = new IpBlock(cityBlock.Network);
                tree.Put(ipBlock.FirstIp, new CityWrapper
                {
                    FirstIp = ipBlock.FirstIp,
                    LastIp = ipBlock.LastIp,
                    CityBlock = cityBlock
                });
            }
            return tree;
        }

        private RedBlackTree<AsnWrapper> LoadAsnTree()
        {
            var asnBlocks = _storageService.ReadAsnBlocks();
            var tree = new RedBlackTree<AsnWrapper>();
            foreach (var asnBlock in asnBlocks)
            {
                var ipBlock = new IpBlock(asnBlock.Network);
                tree.Put(ipBlock.FirstIp, new AsnWrapper
                {
                    FirstIp = ipBlock.FirstIp,
                    LastIp = ipBlock.LastIp,
                    AsnBlock = asnBlock
                });
            }
            return tree;
        }

        private Dictionary<string, CityLocation> LoadCityLocationsDict()
        {
            var cityLocations = _storageService.ReadCityLocations();
            return cityLocations.ToDictionary(x => x.GeonameId);
        }

        private long CastIp(
            string ip)
        {
            var address = IPAddress.Parse(ip);
            var addressBytes = address.GetAddressBytes();
            var networkOrder = BitConverter.ToInt32(addressBytes, 0);
            return (uint)IPAddress.NetworkToHostOrder(networkOrder);
        }
    }
}
