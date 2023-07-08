namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class Restaurant
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
        ///  The latitude of the restaurant address
        /// </summary>
        /// <example>43.1489409</example>
        public decimal? Latitude { get; set;}

        /// <summary>
        ///  The longitude of the restaurant address
        /// </summary>
        /// <example>-88.0459661</example>
        public decimal? Longitude { get; set;}

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

        public string Rating
        {
            get
            {
                return _rating > 0 ? "+" : "-";
            }
        }

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

        public Restaurant()
        {
            Id = Guid.NewGuid();
        }

        public void Load(IRestaurant restaurant)
        {
            City = restaurant.City;
            Name = restaurant.Name;
            PostalCode = restaurant.PostalCode;
            State = restaurant.State;
            StreetAddress = restaurant.StreetAddress;
        }

        public void ProcessReview(float label)
        {
            if (label > 0)
            {
                _rating++;
            }
            else
            {
                _rating--;
            }
        }

        private long _rating;

    }
}
