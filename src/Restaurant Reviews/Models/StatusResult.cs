namespace OIT.Spring2023.RestaurantReviews.Models
{
    public class StatusResult
    {
        /// <summary>
        ///  The status of the service
        /// </summary>
        /// <example>The service is operation.</example>
        public string Message { get; set; }

        /// <summary>
        ///  The version of the service
        /// </summary>
        /// <example>1.0.0</example>
        public string Version { get; set; }

        public StatusResult(string message, string version)
        {
            Message = message;
            Version = version;
        }
    }
}
