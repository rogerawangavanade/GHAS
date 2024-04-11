// Services/OpenWeatherService.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenAIApp.Congurations;

namespace OpenAIApp.Services
{
    public class OpenWeatherService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenWeatherService(IOptions<OpenWeatherConfig> config, HttpClient httpClient)
        {
            _apiKey = config.Value.ApiKey;
            _httpClient = httpClient;
        }

        public async Task<string> GetWeatherDataAsync(double latitude, double longitude)
        {
            try
            {
                string apiUrl = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitude}&lon={longitude}&appid={_apiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching weather data from OpenWeather API.", ex);
            }
        }

    }
}
