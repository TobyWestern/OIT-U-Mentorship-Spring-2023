namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class GeoJsonFeature
    {
        public string Type => "Feature";

        public GeoJsonPoint Geometry { get; set; }

        public GeoJsonProperties Properties { get; set; }

        public GeoJsonFeature(Restaurant restaurant)
        {
            Geometry = new GeoJsonPoint(restaurant);
            Properties = new GeoJsonProperties(restaurant);
        }
    }
}
