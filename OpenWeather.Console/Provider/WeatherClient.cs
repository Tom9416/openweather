using System;
using OpenWeather.Console.Entities;
using RestSharp;

namespace OpenWeather.Console.Provider
{
    public class WeatherClient
    {
        public Weather GetWeather(string location, string units)
        {
            var client = new RestClient("http://api.openweathermap.org");

            var request = new RestRequest("data/2.5/weather");
            request.AddParameter("q", location);
            request.AddParameter("units", units);
            request.AddParameter("appid", "798b9e9175080137f4c1cdded5ecbbbf");
           
            var response = client.Execute<Weather>(request);

            if (response.Data.Name == null)
            {
                throw new NullReferenceException($"The specified location [{location}] is not valid");
            }

            return response.Data;
        }
    }
}