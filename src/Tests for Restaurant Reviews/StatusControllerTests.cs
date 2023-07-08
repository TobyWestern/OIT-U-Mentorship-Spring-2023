using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OIT.Spring2023.RestaurantReviews.Controllers;
using OIT.Spring2023.RestaurantReviews.Models;
using Xunit;

namespace OIT.Spring2023.RestaurantReviews.Test
{
    public class StatusControllerTests
    {
        [Fact]
        public void StatusResultOutput()
        {
            // Arrange

            ILogger<StatusController> logger = new NullLogger<StatusController>();

            StatusController controller = new StatusController(logger);

            // Act

            StatusResult response = controller.Get();

            // Assert

            Assert.NotNull(response);
        }
    }
}