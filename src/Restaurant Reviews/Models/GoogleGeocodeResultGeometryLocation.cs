using System.Text.Json.Serialization;

namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class GoogleGeocodeResultGeometryLocation
    {
        [JsonPropertyName("lat")]
        public decimal? Latitude { get; set; }

        [JsonPropertyName("lng")]
        public decimal? Longitude { get; set;}
    }
}
