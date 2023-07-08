using System.Text.Json.Serialization;

namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class GoogleGeocodeResultGeometry
    {
        public GoogleGeocodeResultGeometryLocation Location { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }
    }
}
