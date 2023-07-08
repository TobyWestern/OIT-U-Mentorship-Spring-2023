namespace OIT.Spring2023.RestaurantReviews.Models
{
    public interface IRestaurant
    {
        public string? City { get; set; }

        public string? Name { get; set; }

        public string? PostalCode { get; set; }

        public string? State { get; set; }

        public string? StreetAddress { get; set; }
    }
}
