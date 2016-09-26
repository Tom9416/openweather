using System;
using FluentAssertions;
using NUnit.Framework;
using OpenWeather.Console.Provider;

namespace OpenWeather.Tests.Integration
{
    public class WeatherClientTests
    {
        [Test]
        public void WhenValidLocationIsProvided_ThenTheResponseContainsATemperature()
        {
            var weatherClient = new WeatherClient();

            var weather = weatherClient.GetWeather("peterborough", "Metric");

            weather.Should().NotBeNull();
            weather.Name.ToLower().Should().Be("peterborough");
            weather.Main.Should().NotBeNull();
            weather.Main.Temp.Should().BeInRange(-20, 50);
        }

        [Test]
        
        public void WhenValidLocationIsProvided_ThenTheResponseShouldBeInTheMinimumAndMaximumRangeSpecified()
        {
            var weatherClient = new WeatherClient();

            var weather = weatherClient.GetWeather("peterborough", "Metric");

            weather.Should().NotBeNull();
            weather.Main.Temp.Should().BeInRange(-20, 50);
            weather.Main.Temp_Min.Should().NotBe(0).And.BeInRange(-10, 20);
            weather.Main.Temp_Max.Should().NotBe(0).And.BeInRange(0, 40);
        }

        [Test]
        [TestCase("sdfghiusacjnkx")]
        [TestCase("gffdhfjfghsd")]
        public void WhenAnInvalidLocationIsEntered_ThenAnErrorMessageIsReturned(string location)
        {
            var weatherClient = new WeatherClient();

            Action getDodgyWeather = () => weatherClient.GetWeather(location, "metric");

            getDodgyWeather.ShouldThrow<NullReferenceException>()
                .And.Message.Should().Be($"The specified location [{location}] is not valid");
        }

        [Test]
        public void WhenAMetricValueIsRequestedAndAnImperialValueIsRequested_ThenTheyAreNotTheSame() 
        {
            var weatherclient = new WeatherClient();

            var weatherInMetric = weatherclient.GetWeather("SD", "Metric");
            var weatherInImperial = weatherclient.GetWeather("SD", "Imperial");
            weatherInMetric.Should().NotBeNull();

            weatherInMetric.Main.Temp.Should().NotBe(weatherInImperial.Main.Temp);
            weatherInImperial.Main.Temp.Should().BeGreaterThan(weatherInMetric.Main.Temp);

        }

        [Test]
        public void WhenAImperalValueIsRequestedAndAnMetricValueIsRequested_ThenTheyAreNotTheSame() 
        {
            var weatherclient = new WeatherClient();

            var weatherInMetric = weatherclient.GetWeather("SD", "Metric");
            var weatherInImperial = weatherclient.GetWeather("SD", "Imperial");
            weatherInMetric.Should().NotBeNull();

            weatherInImperial.Main.Temp.Should().NotBe(weatherInMetric.Main.Temp);
            weatherInMetric.Main.Temp.Should().BeLessThan(weatherInImperial.Main.Temp);

        }
        
        [Test]
        public void WhenAnInvalidlValueIsRequested_ThenValueIsShownInKelvinUnits() 
        {
            var weatherclient = new WeatherClient();
             
            var weatherkelvin = weatherclient.GetWeather("SD", "cvfhgdgfhj");
            
            weatherkelvin.Should().NotBeNull();

            weatherkelvin.Main.Temp.Should().BeGreaterThan(200.0m);
            


        }
    }

}
