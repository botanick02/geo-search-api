﻿using GeoCoordinatePortable;
using GeoSearchApi.Models;
using Newtonsoft.Json;
using System.Diagnostics;

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
                    var key = location.City.Substring(0, i + 1).ToLower();

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
            if (!citiesIdsMatrix[name.Length - 1].ContainsKey(name.ToLower()))
            {
                return new List<LocationEntityMini>();
            }

            var foundLocationsIds = citiesIdsMatrix[name.Length - 1][name.ToLower()];

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

        public LocationEntity FindByCoordinates(double latitude, double longitude)
        {
            LocationEntity locationEntity = locationsDict[1];

            var sCoord = new GeoCoordinate(latitude, longitude);
            var eCoord = new GeoCoordinate(locationEntity.Latitude, locationEntity.Longitude);
            var distance = sCoord.GetDistanceTo(eCoord);

            foreach (var location in locationsDict.Values)
            {
                eCoord = new GeoCoordinate(location.Latitude, location.Longitude);

                var newDist = sCoord.GetDistanceTo(eCoord);

                if (newDist < distance)
                {
                    distance = newDist;
                    locationEntity = location;
                }
            }

            return locationEntity;
        }
    }
}
