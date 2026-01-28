using System.Collections.Generic;
using System.Text.Json;
using Xunit;

using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using Weather.Models;

namespace Weather.Tests;

public class WeatherTest
{
	[Fact]
	public void TestGetCityWeatherFound()
	{
		var request = new APIGatewayProxyRequest();
		request.QueryStringParameters = new Dictionary<string, string> { { "city", "Sydney" } } ;
		var context = new TestLambdaContext();

		var expectedResponse = new APIGatewayProxyResponse
		{
			Body = JsonSerializer.Serialize(new WeatherResponse
			{
				city = "Sydney",
				tempC = 22,
				condition = "Sunny"
			}),
			StatusCode = 200,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};

		var function = new Weather();
		var response = function.FunctionHandler(request, context);

		Assert.Equal(expectedResponse.Body, response.Body);
		Assert.Equal(expectedResponse.Headers, response.Headers);
		Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
	}

	[Fact]
	public void TestGetCityWeatherNotFound()
	{
		var request = new APIGatewayProxyRequest();
		request.QueryStringParameters = new Dictionary<string, string> { { "city", "Port Niranda" } } ;
		var context = new TestLambdaContext();

		var expectedResponse = new APIGatewayProxyResponse
		{
			Body = JsonSerializer.Serialize(new Dictionary<string, string> { { "message", "City not found" } }),
			StatusCode = 404,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};

		var function = new Weather();
		var response = function.FunctionHandler(request, context);

		Assert.Equal(expectedResponse.Body, response.Body);
		Assert.Equal(expectedResponse.Headers, response.Headers);
		Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
	}

	[Fact]
	public void TestGetCityWeatherNoCitySpecified()
	{
		var request = new APIGatewayProxyRequest();
		var context = new TestLambdaContext();

		var expectedResponse = new APIGatewayProxyResponse
		{
			Body = JsonSerializer.Serialize(new Dictionary<string, string> { { "message", "No city specified" } }),
			StatusCode = 400,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};

		var function = new Weather();
		var response = function.FunctionHandler(request, context);

		Assert.Equal(expectedResponse.Body, response.Body);
		Assert.Equal(expectedResponse.Headers, response.Headers);
		Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
	}

	[Fact]
	public void TestGetCityWeatherEmptyCity()
	{
		var request = new APIGatewayProxyRequest();
		request.QueryStringParameters = new Dictionary<string, string> { { "city", "" } } ;
		var context = new TestLambdaContext();

		var expectedResponse = new APIGatewayProxyResponse
		{
			Body = JsonSerializer.Serialize(new Dictionary<string, string> { { "message", "No city specified" } }),
			StatusCode = 400,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};

		var function = new Weather();
		var response = function.FunctionHandler(request, context);

		Assert.Equal(expectedResponse.Body, response.Body);
		Assert.Equal(expectedResponse.Headers, response.Headers);
		Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
	}
}