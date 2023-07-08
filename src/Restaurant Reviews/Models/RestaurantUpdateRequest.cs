namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class RestaurantUpdateRequest : IRestaurant
    {
        /// <summary>
        ///  The city where the restaurant is located
        /// </summary>
        /// <example>Milwaukee</example>
        public string? City { get; set; }

        /// <summary>
        ///  Unique identifier for a restaurant
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  The name of the restaurant
        /// </summary>
        /// <example>Park Place Cafe</example>
        public string? Name { get; set; }

        /// <summary>
        ///  The postal code for the restaurant's address
        /// </summary>
        /// <example>53224</example>
        public string? PostalCode { get; set; }

        /// <summary>
        ///  The state for the restaurant's address
        /// </summary>
        /// <example>WI</example>
        public string? State { get; set; }

        /// <summary>
        ///  The street number and name for the restaurant's address
        /// </summary>
        /// <example>10843 W Park Pl</example>
        public string? StreetAddress { get; set; }
    }
}
