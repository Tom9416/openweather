using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeather.Console.Provider;

namespace OpenWeather.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("enter a location:");
                var location = System.Console.ReadLine();
                System.Console.WriteLine("enter a unit type:");
                var unit = System.Console.ReadLine();

                var client = new WeatherClient();

                var weather = client.GetWeather(location, unit);
                if (weather.Main.Temp > 200.0m)
                {
                    System.Console.WriteLine($"You entered an incorrect unit called {unit} therfore current temperature is shown in kelvin by default");
                }
                System.Console.WriteLine($"Temperature in {weather.Name} is {weather.Main.Temp}");
                System.Console.WriteLine($"Temperature in {weather.Name} is displayed in {unit}");
                System.Console.WriteLine($"Min temp today in {weather.Name} is {weather.Main.Temp_Min}");
                System.Console.WriteLine($"Max temp today in {weather.Name} is {weather.Main.Temp_Max}");
                System.Console.ReadLine();
                
            }
            catch (NullReferenceException ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.ReadLine();
            }
        }
    }
}