using DockerizedTestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DockerizedTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDistributedCache _distributedCache;
        private readonly WeatherForecastContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache distributedCache, WeatherForecastContext context)
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _context = context;

            _logger.LogInformation("Started");
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("WeatherForecastAPI is called");
            string cacheKey = "weatherList";

            try
            {
                //MQTTClient mqttClient = new MQTTClient(_logger);

                byte[] redisWeatherList = _distributedCache.Get(cacheKey);
                if (redisWeatherList != null)
                {
                    string serializedWeatherList = Encoding.UTF8.GetString(redisWeatherList);
                    List<WeatherForecast> weatherList = JsonConvert.DeserializeObject<List<WeatherForecast>>(serializedWeatherList);

                    //mqttClient.Connect();
                    //mqttClient.Publish(serializedWeatherList);
                    //mqttClient.Subscribe();

                    _logger.LogInformation($"API Response : {serializedWeatherList}");
                    return Ok(new Tuple<IEnumerable<WeatherForecast>, string>(weatherList, "Data fetched from cache"));
                }
                else
                {
                    List<WeatherForecast> weatherList = _context.Weather.ToList();
                    string serializedWeatherList = JsonConvert.SerializeObject(weatherList);
                    redisWeatherList = Encoding.UTF8.GetBytes(serializedWeatherList);
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddSeconds(10));
                    _distributedCache.Set(cacheKey, redisWeatherList, options);

                    //mqttClient.Connect();
                    //mqttClient.Publish(serializedWeatherList);
                    //mqttClient.Subscribe();

                    _logger.LogInformation($"API Response : {serializedWeatherList}");
                    return Ok(new Tuple<IEnumerable<WeatherForecast>>(weatherList));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
