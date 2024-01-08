using Dapper;
using GeoSearchApi.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace GeoSearchApi.Repositories
{
    public class LocationsRepository
    {
        private readonly string connectionString;
        public LocationsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<LocationEntity> FindByCity(string name)
        {
            try
            {
                var parameters = new
                {
                    Name = name + "%"
                };
                string sqlQuery = $"SELECT * FROM Location WHERE City LIKE @Name";

                using (var conn = new SqlConnection(connectionString))
                {
                    var locations = conn.Query<LocationEntity>(sqlQuery, parameters).ToList();
                    
                    return locations;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return new List<LocationEntity >();
        }
    }
}
