using IpLocator.Models.CsvModels;
using System.Collections.Generic;

namespace IpLocator.Services.Storage
{
    public interface IStorageService
    {
        List<CityBlock> ReadCityBlocks();
        List<AsnBlock> ReadAsnBlocks();
        List<CityLocation> ReadCityLocations();
    }
}
