using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Weather.Models;
using Weather.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Weather;

public class Weather
{
	private readonly ILogger logger;

	private readonly WeatherService weatherService;

	public Weather()
	{
		var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
			.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
			.AddConsole()
		);

		logger = loggerFactory.CreateLogger("Weather");

		weatherService = new WeatherService(logger);
	}

    public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
    {
		var cityName = apigProxyEvent?.QueryStringParameters?["city"];

		if (StringValues.IsNullOrEmpty(cityName))
		{
			return new APIGatewayProxyResponse
			{
				Body = JsonSerializer.Serialize(new Dictionary<string, string> { { "message", "No city specified" } }),
				StatusCode = 400,
				Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
			};
		}

		var cityWeather = weatherService.GetWeatherForCity(cityName);

		logger.LogInformation("Getting weather for city {0}", cityName);

		if (cityWeather == null)
		{
			return new APIGatewayProxyResponse
			{
				Body = JsonSerializer.Serialize(new Dictionary<string, string> { { "message", "City not found" } }),
				StatusCode = 404,
				Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
			};
		}
		else
		{
			return new APIGatewayProxyResponse
			{
				Body = JsonSerializer.Serialize(cityWeather),
				StatusCode = 200,
				Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
			};
		}
    }
}