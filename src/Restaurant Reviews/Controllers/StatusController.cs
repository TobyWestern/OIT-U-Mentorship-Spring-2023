using Microsoft.AspNetCore.Mvc;
using OIT.Spring2023.RestaurantReviews.Models;

namespace OIT.Spring2023.RestaurantReviews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        public StatusController(ILogger<StatusController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        ///  Retrieves service status and version
        /// </summary>
        [HttpGet(Name = "Status")]
        public StatusResult Get()
        {
            logger.LogDebug("Status endpoint called.");

            return new StatusResult("Service is operational.", "1.0.0");
        }

        private readonly ILogger<StatusController> logger;
    }
}