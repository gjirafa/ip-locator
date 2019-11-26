# IpLocator

IpLocator is a very fast and simple geolocation open-source service, written in .NET Core. It can be used in your applications where you need to know more information about the geolocation of your users based on their IP addresses. It works only for IP V4 addresses.


## How it works

IpLocator uses [GeoLite2 Free Downloadable Databases](https://dev.maxmind.com/geoip/geoip2/geolite2/) as a source for IP geolocation info. Since the geolocation info are in IP ranges, we use a balanced tree ([Red&Black tree](https://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/RedBlackBST.java.html)) to store all the ranges in memory (<1GB used) for fast lookup for any ip address. This guarantees us a search time complexity of *logN*, where N in our case is ~4M ranges. (<1ms lookup from our benchmarks).

## Response example

IpLocator has three endpoints:
1) **/location/{ip_address}** returns information about geolocation 
example: /location/1.1.1.1 returns:

```json
{
  "localeCode": "en",
  "continentCode": "OC",
  "continentName": "Oceania",
  "countryIsoCode": "AU",
  "countryName": "Australia",
  "cityName": "",
  "timeZone": "Australia/Sydney",
  "isInEuropeanUnion": false,
  "latitude": -33.494,
  "longitude": 143.2104,
  "accuracyRadius": 1000,
  "postalCode": ""
} 
```

2) **/asn/{ip_address}** returns information about Autonomous system
example: /asn/1.1.1.1 returns:
```json
{
  "autonomousSystemNumber": 13335,
  "autonomousSystemOrganization": "Cloudflare, Inc."
} 
```

3) **/details/{ip_address}** returns information about both geolocation and asn
example: /details/1.1.1.1 returns:
```json
{
  "asn": {
    "autonomousSystemNumber": 13335,
    "autonomousSystemOrganization": "Cloudflare, Inc."
  },
  "location": {
    "localeCode": "en",
    "continentCode": "OC",
    "continentName": "Oceania",
    "countryIsoCode": "AU",
    "countryName": "Australia",
    "cityName": "",
    "timeZone": "Australia/Sydney",
    "isInEuropeanUnion": false,
    "latitude": -33.494,
    "longitude": 143.2104,
    "accuracyRadius": 1000,
    "postalCode": ""
  }
}
```


## Important

IpLocator reads all the ranges for Geolocation and ASN info at project startup, which means it may take sometime (depending on the hardware) until the projects starts running. This was done so that other requests will be processed faster.

Main data sources are placed as .csv files inside the /Data folder. If you want to update the ranges you can download them from the source below and just place them inside the /Data folder using the same names.  

## Credits

This project includes GeoLite2 data created by MaxMind, available from <a href="https://www.maxmind.com">https://www.maxmind.com</a>.

This project includes the [Red&Black tree implementation](https://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/RedBlackBST.java.html) from the algs4.jar library which accompanies the textbook Algorithms, 4th edition by Robert Sedgewick and Kevin Wayne, Addison-Wesley Professional, 2011, ISBN 0-321-57351-X. http://algs4.cs.princeton.edu

This project uses [CsvHelper](https://github.com/JoshClose/CsvHelper) for parsing csv files.
