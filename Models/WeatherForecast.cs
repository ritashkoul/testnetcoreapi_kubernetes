using System;
using System.ComponentModel.DataAnnotations;

namespace DockerizedTestAPI.Models
{
    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }

        public DateTime WeatherDate { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
