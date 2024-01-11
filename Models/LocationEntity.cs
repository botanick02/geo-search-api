namespace GeoSearchApi.Models
{
    public class LocationEntity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }

    }

    public class LocationEntityMini
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
