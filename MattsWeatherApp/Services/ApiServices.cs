using MattsWeatherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattsWeatherApp.Services
{
    public static class ApiServices
    {
        public static async Task<Root> GetMattsWeather(double latitude,double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=e8791c806c56a3657beb21a150f3b77d",latitude, longitude));

            return JsonConvert.DeserializeObject<Root>(response);
        
        }

        public static async Task<Root> GetMattsWeatherByCity(string city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=e8791c806c56a3657beb21a150f3b77d", city));

            return JsonConvert.DeserializeObject<Root>(response);

        }
    }
}
