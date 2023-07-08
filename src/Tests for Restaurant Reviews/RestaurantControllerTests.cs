using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Contrib.HttpClient;
using OIT.Spring2023.RestaurantReviews.Controllers;
using OIT.Spring2023.RestaurantReviews.Models;
using Xunit;

namespace OIT.Spring2023.RestaurantReviews.Test
{
    public class RestaurantControllerTests
    {
        [Fact]
        public void GetGeocodeSuccess()
        {
            // Arrange

            RestaurantList restaurants = new RestaurantList();

            IOptions<GoogleGeocodeApiOptions> options = Options.Create(
                new GoogleGeocodeApiOptions
                {
                    ApiKey = "My API Key"
                }
            );

            Mock<HttpMessageHandler> handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest().ReturnsJsonResponse(new GoogleGeocodeResponse
            {
                Status = "OK",
                Results = new List<GoogleGeocodeResult>
                {
                    new GoogleGeocodeResult
                    {
                        Geometry = new GoogleGeocodeResultGeometry
                        {
                            Location = new GoogleGeocodeResultGeometryLocation
                            {
                                Latitude = 50,
                                Longitude = 55
                            }
                        }
                    }
                }
            });

            IHttpClientFactory factory = handler.CreateClientFactory();

            Mock.Get(factory).Setup(x => x.CreateClient("Google_Maps"))
                .Returns(() =>
                {
                    var client = handler.CreateClient();
                    client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
                    return client;
                });

            ILogger<RestaurantController> logger = new NullLogger<RestaurantController>();

            RestaurantController controller = new RestaurantController(restaurants, options, factory, logger);

            RestaurantCreateRequest requestBody = new RestaurantCreateRequest
            {
                City = "Milwaukee",
                Name = "My Place",
                PostalCode = "53224",
                State = "WI",
                StreetAddress = "100 Main Street"
            };

            // Act

            ActionResult<Restaurant> response = controller.Create(requestBody).Result;

            // Assert

            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            Assert.IsType<CreatedResult>(response.Result);

            CreatedResult result = (CreatedResult)response.Result;

            Assert.NotNull(result.Value);
            Assert.IsType<Restaurant>(result.Value);

            Restaurant restaurant = (Restaurant)result.Value;

            Assert.NotEqual<Guid>(Guid.Empty, restaurant.Id);
            Assert.Equal("My Place", restaurant.Name);
            Assert.Equal("100 Main Street", restaurant.StreetAddress);
            Assert.Equal("Milwaukee", restaurant.City);
            Assert.Equal("WI", restaurant.State);
            Assert.Equal("53224", restaurant.PostalCode);
            Assert.Equal(50, restaurant.Latitude);
            Assert.Equal(55, restaurant.Longitude);
        }
    }
}
