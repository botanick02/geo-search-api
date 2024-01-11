using GeoSearchApi.Models;
using Newtonsoft.Json;

namespace GeoSearchApi.Repositories
{
    public class LocationsRepository
    {
        private string defaultLocationsPath = @"./Resources/locations.json";
        private readonly Dictionary<int, LocationEntity> locationsDict = new Dictionary<int, LocationEntity>();
        private readonly List<Dictionary<string, List<int>>> citiesIdsMatrix = new List<Dictionary<string, List<int>>>();

        public LocationsRepository()
        {
            var locations = JsonConvert.DeserializeObject<List<LocationEntity>>(File.ReadAllText(defaultLocationsPath));

            if (locations == null)
            {
                throw new FileNotFoundException("Locations were failed to receive");
            }

            locationsDict = locations.ToDictionary(x => x.Id, x => x);

            int biggestCityName = 0;
            int id = 0;
            foreach (var location in locationsDict.Values)
            {
                if (location.City.Length > biggestCityName)
                {
                    biggestCityName = location.City.Length;
                    id = location.Id;
                }
            }

            for (int i = 0; i < biggestCityName; i++)
            {
                var dict = new Dictionary<string, List<int>>();

                foreach (var location in locationsDict.Values)
                {
                    if (location.City.Length <= i)
                    {
                        continue;
                    }
                    var key = location.City.Substring(0, i + 1);

                    var idList = new List<int>();
                    if (dict.TryGetValue(key, out idList))
                    {
                        idList.Add(location.Id);
                    }
                    else
                    {
                        idList = new List<int>() { location.Id };
                        dict.Add(key, idList);
                    }
                }
                citiesIdsMatrix.Add(dict);
            }
        }

        public List<LocationEntityMini> FindByCity(string name, int? resultsNumber = null)
        {
            var foundLocationsIds = citiesIdsMatrix[name.Length - 1][name];

            var foundLocations = new List<LocationEntityMini>();

            for (int i = 0; i < foundLocationsIds.Count; i++)
            {
                var location = locationsDict[foundLocationsIds[i]];

                foundLocations.Add(new LocationEntityMini() { Id = location.Id, Name = location.City + ", " + location.Country });

                if (resultsNumber != null && i + 1 >= resultsNumber)
                {
                    break;
                }
            }

            return foundLocations;
        }

        public LocationEntity FindById(int id)
        {
            return locationsDict[id];
        }
    }
}
