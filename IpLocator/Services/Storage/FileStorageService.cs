using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using IpLocator.Models.CsvModels;

namespace IpLocator.Services.Storage
{
    public class FileStorageService : IStorageService
    {
        public List<AsnBlock> ReadAsnBlocks()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/asn-blocks.csv");
            return ReadRecords<AsnBlock>(filePath);
        }

        public List<CityBlock> ReadCityBlocks()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/city-blocks.csv");
            return ReadRecords<CityBlock>(filePath);
        }

        public List<CityLocation> ReadCityLocations()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/city-locations.csv");
            return ReadRecords<CityLocation>(filePath);
        }

        public List<T> ReadRecords<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader);
            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
