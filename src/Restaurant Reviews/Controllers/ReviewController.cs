using Microsoft.AspNetCore.Mvc;
using OIT.Spring2023.RestaurantReviews.Models;
using Restaurant_Reviews;
using System.Web.Http;

namespace OIT.Spring2023.RestaurantReviews.Controllers
{
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public ReviewController(
            RestaurantList restaurants,
            ILogger<ReviewController> logger)
        {
            this.logger = logger;
            this.restaurants = restaurants;
        }

        /// <summary>
        ///  Adds a new review to the restaurant.
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpPost()]
        [Microsoft.AspNetCore.Mvc.Route("Restaurant/{restaurantId}/Review")]
        public ActionResult<ReviewSubmissionResult> Create(Guid restaurantId, [Microsoft.AspNetCore.Mvc.FromBody] string review)
        {
            logger.LogDebug("Review endpoint called.");

            if (!restaurants.ContainsKey(restaurantId))
            {
                return NotFound();
            }

            //Load sample data
            ReviewAnalysis.ModelInput sampleData = new ReviewAnalysis.ModelInput()
            {
                Col0 = review,
            };

            //Load model and predict output
            ReviewAnalysis.ModelOutput result = ReviewAnalysis.Predict(sampleData);

            restaurants[restaurantId].ProcessReview(result.PredictedLabel);

            return new ReviewSubmissionResult(review, result.PredictedLabel);
        }

        private readonly ILogger<ReviewController> logger;
        private readonly RestaurantList restaurants;
    }
}
