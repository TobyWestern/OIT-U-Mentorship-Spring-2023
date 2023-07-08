using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using OIT.Spring2023.RestaurantReviews.Controllers;
using OIT.Spring2023.RestaurantReviews.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace OIT.Spring2023.RestaurantReviews.Test
{
    public class ReviewControllerTests
    {
        [Fact]
        public void RestaurantNotFound()
        {
            // Arrange

            ILogger<ReviewController> logger = new NullLogger<ReviewController>();

            RestaurantList restaurants = new RestaurantList();

            ReviewController controller = new ReviewController(restaurants, logger);

            // Act

            ActionResult<ReviewSubmissionResult> response = controller.Create(Guid.NewGuid(), "The food here was horrible. Do not recommend.");

            // Assert

            Assert.NotNull(response);

            Assert.NotNull(response.Result);
            Assert.IsType<NotFoundResult>(response.Result);

            Assert.Null(response.Value);
        }

        [Fact]
        public void RestaurantFound()
        {
            // Arrange

            ILogger<ReviewController> logger = new NullLogger<ReviewController>();

            RestaurantList restaurants = new RestaurantList();

            Restaurant restaurant = new Restaurant
            {
                Id = Guid.NewGuid()
            };
            restaurants.Add(restaurant.Id, restaurant);

            ReviewController controller = new ReviewController(restaurants, logger);

            // Act

            ActionResult<ReviewSubmissionResult> response = controller.Create(restaurant.Id, "The food here was horrible. Do not recommend.");

            // Assert

            Assert.NotNull(response);

            Assert.Null(response.Result);

            Assert.NotNull(response.Value);
            Assert.IsType<ReviewSubmissionResult>(response.Value);
        }
    }
}
