// Controllers/WeatherController.cs
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Services;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly OpenWeatherService _openWeatherService;

    public WeatherController(OpenWeatherService openWeatherService)
    {
        _openWeatherService = openWeatherService;
    }

    [HttpGet]
    [Route("GetOpenWeatherResponse")]
    public async Task<IActionResult> GetWeather(double latitude, double longitude)
    {
        try
        {
            string weatherData = await _openWeatherService.GetWeatherDataAsync(latitude, longitude);
            return Ok(weatherData);
        }
        catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
        {
            HttpWebResponse response = (HttpWebResponse)ex.Response;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound("Weather data not found.");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.StatusDescription);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
