namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class GoogleGeocodeResponse
    {
        public List<GoogleGeocodeResult> Results { get; set; }

        public string Status { get; set; }
    }
}
