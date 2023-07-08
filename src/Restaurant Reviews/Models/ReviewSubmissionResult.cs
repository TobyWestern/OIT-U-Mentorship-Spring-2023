namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class ReviewSubmissionResult
    {
        public string Review { get; set; }

        public long Count {
            get
            {
                return Review.Split(' ').LongLength;
            }
        }

        public string Sentiment { get; set;}

        public ReviewSubmissionResult(string review, float label)
        {
            Review = review;
            Sentiment = label > 0 ? "Positive" : "Negative";
        }
    }
}
