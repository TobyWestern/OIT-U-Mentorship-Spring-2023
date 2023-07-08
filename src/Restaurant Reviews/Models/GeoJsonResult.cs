namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class GeoJsonResult
    {
        public string Type => "FeatureCollection";

        public List<GeoJsonFeature> Features { get; set; }

        public GeoJsonResult(RestaurantList restaurants)
        {
            Features = new List<GeoJsonFeature>();

            foreach(Restaurant restaurant in restaurants.Values)
            {
                // Features.Add(new GeoJsonFeature());
            }
        }
    }
}
