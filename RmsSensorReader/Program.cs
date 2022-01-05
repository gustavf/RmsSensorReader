﻿using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Common;
using RmsSensorReader;
using UnitsNet;

Console.WriteLine("Hello DHT!");
Console.WriteLine();

int pin = 12;

Console.WriteLine($"Reading temperature and humidity on DHT22, pin {pin}");
using (Dht22 dht22 = new(pin))
{
    Dht(dht22);
}    

void Dht(DhtBase dht)
{
    while (true)
    {
        Temperature temperature = default;
        RelativeHumidity humidity = default;
        bool success = dht.TryReadHumidity(out humidity) && dht.TryReadTemperature(out temperature);
        // You can only display temperature and humidity if the read is successful otherwise, this will raise an exception as
        // both temperature and humidity are NAN
        if (success)
        {
            Console.WriteLine($"Temperature: {temperature.DegreesCelsius:F1}\u00B0C, Relative humidity: {humidity.Percent:F1}%");

            // WeatherHelper supports more calculations, such as saturated vapor pressure, actual vapor pressure and absolute humidity.
            Console.WriteLine(
                $"Heat index: {WeatherHelper.CalculateHeatIndex(temperature, humidity).DegreesCelsius:F1}\u00B0C");
            Console.WriteLine(
                $"Dew point: {WeatherHelper.CalculateDewPoint(temperature, humidity).DegreesCelsius:F1}\u00B0C");
        }
        else
        {
            Console.WriteLine("Error reading DHT sensor");
        }

        // You must wait some time before trying to read the next value
        Thread.Sleep(5000);
    }
}
