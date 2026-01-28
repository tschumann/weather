using Microsoft.Extensions.Logging;

using Weather.Models;

namespace Weather.Services;

public class WeatherService
{
	private readonly ILogger _logger;

	private readonly Dictionary<string, WeatherResponse> weatherResponses = new Dictionary<string, WeatherResponse>()
	{
		{
			"Brisbane", new WeatherResponse
			{
				city = "Brisbane",
				tempC = 34,
				condition = "Hot"
			}
		},
		{
			"Sydney", new WeatherResponse
			{
				city = "Sydney",
				tempC = 22,
				condition = "Sunny"
			}
		},
		{
			"Melbourne", new WeatherResponse
			{
				city = "Melbourne",
				tempC = 10,
				condition = "Rainy"
			}
		}
	};

	public WeatherService(ILogger logger)
	{
		_logger = logger;
	}

	public WeatherResponse? GetWeatherForCity(string? cityName)
	{
		if (cityName == null)
		{
			return null;
		}

		if (!this.weatherResponses.ContainsKey(cityName))
		{
			_logger.LogInformation("No weather found for cityName {0}", cityName);

			return null;
		}
		else
		{
			return weatherResponses[cityName];
		}
	}
}