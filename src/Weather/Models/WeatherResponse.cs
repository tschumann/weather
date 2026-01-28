namespace Weather.Models;

public record WeatherResponse
{
	public required string city { get; init; }

	public required int tempC { get; init; }

	public required string condition { get; init; }
}