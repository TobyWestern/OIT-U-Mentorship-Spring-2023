using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OIT.Spring2023.RestaurantReviews.Models;
using System.Web;

namespace OIT.Spring2023.RestaurantReviews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        public RestaurantController(
            RestaurantList restaurantList,
            IOptions<GoogleGeocodeApiOptions> options,
            IHttpClientFactory httpClientFactory,
            ILogger<RestaurantController> logger
        )
        {
            this.logger = logger;
            this.restaurantList = restaurantList;
            this.apiKey = options.Value.ApiKey;
            this.httpClient = httpClientFactory.CreateClient("Google_Maps");
        }

        /// <summary>
        ///  Adds a new restaurant to the list of restaurants
        /// </summary>
        /// <param name="restaurantRequest">
        ///  The values for the restaurant to be added
        /// </param>
        [HttpPost]
        public async Task<ActionResult<Restaurant>> Create(RestaurantCreateRequest restaurantRequest)
        {
            Restaurant restaurant = new Restaurant();
            restaurant.Load(restaurantRequest);

            await Geocode(restaurant);

            restaurantList.Add(restaurant.Id, restaurant);
            return Created(restaurant.Id.ToString(), restaurant);
        }

        /// <summary>
        ///  Delete a restaurant
        /// </summary>
        /// <param name="id">
        ///  The id of the restaurant to be deleted
        /// </param>
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            if (restaurantList.ContainsKey(id))
            {
                restaurantList.Remove(id);
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        ///  Updates a restaurant
        /// </summary>
        /// <param name="restaurantRequest">
        ///  The updated values for the restaurant
        /// </param>
        [HttpPut]
        public async Task<ActionResult<Restaurant>> Update(RestaurantUpdateRequest restaurantRequest)
        {
            if (restaurantList.ContainsKey(restaurantRequest.Id))
            {
                Restaurant restaurant = restaurantList[restaurantRequest.Id];
                restaurant.Load(restaurantRequest);

                await Geocode(restaurant);

                return restaurant;
            }

            return NotFound(restaurantRequest.Id);
        }

        /// <summary>
        ///  Retrieves all restaurants
        /// </summary>
        [HttpGet]
        public ActionResult<List<Restaurant>> GetAll()
        {
            return restaurantList.Values.ToList();
        }

        /// <summary>
        ///  Retrieves the restaurant having the specified id
        /// </summary>
        /// <param name="id">
        ///  The id of the restaurant to be retrieved.
        /// </param>
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetById(Guid id)
        {
            if (restaurantList.ContainsKey(id))
            {
                return restaurantList[id];
            }

            return NotFound(id);
        }

        /// <summary>
        ///  Retrieves a map containing all the restaurants
        /// </summary>
        [HttpGet("Map")]
        public async Task<FileContentResult> GetMap()
        {
            byte[] image = await GenerateMap();

            return File(image, "image/png");
        }

        /// <summary>
        ///  Retrieves all restaurants encoded in GeoJson
        /// </summary>
        [HttpGet("Export/{format}")]
        public async Task<ActionResult<GeoJsonResult>> Export(string format = "geojson")
        {
            return await Task.FromResult(new GeoJsonResult(restaurantList));
        }

        private async Task Geocode(Restaurant restaurant)
        {
            string address = $"{restaurant.StreetAddress},{restaurant.City},{restaurant.State}";
            address = HttpUtility.UrlEncode(address);

            GoogleGeocodeResponse? message = await httpClient.GetFromJsonAsync<GoogleGeocodeResponse>($"geocode/json?address={address}&key={apiKey}");

            if (message?.Status == "OK")
            {
                restaurant.Longitude = message.Results[0].Geometry.Location.Longitude;
                restaurant.Latitude = message.Results[0].Geometry.Location.Latitude;
            }
        }

        private async Task<byte[]> GenerateMap()
        {
            string markers = "";

            foreach(Restaurant restaurant in restaurantList.Values)
            {
                if (restaurant.Latitude.HasValue && restaurant.Longitude.HasValue)
                {
                    markers += "&" + $"markers=color:blue%7Clabel:P%7C{restaurant.Latitude},{restaurant.Longitude}";
                }
            }

            HttpResponseMessage message = await httpClient.GetAsync($"staticmap?zoom=14&size=400x400{markers}&key={apiKey}");

            return await message.Content.ReadAsByteArrayAsync();
        }

        private readonly ILogger<RestaurantController> logger;
        private readonly RestaurantList restaurantList;
        private readonly HttpClient httpClient;
        private readonly string? apiKey;
    }
}
