
using OIT.Spring2023.RestaurantReviews.Controllers;
using OIT.Spring2023.RestaurantReviews.Models;

namespace OIT.Spring2023.RestaurantReviews
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                options.IncludeXmlComments(filePath);
            });
            builder.Services.AddSingleton(typeof(RestaurantList));
            builder.Services.AddHttpClient("Google_Maps",
                client =>
                {
                    client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
                });

            builder.Services.Configure<GoogleGeocodeApiOptions>(builder.Configuration.GetSection(GoogleGeocodeApiOptions.Section));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}