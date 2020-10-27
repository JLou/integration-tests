using Brenda.Models;
using System;
using System.Collections.Generic;

namespace Brenda.IntegrationTests.Infrastructure
{
    public static class Utilities
    {
        static internal void InitializeDbForTests(BrendaContext db)
        {
            db.Forecasts.RemoveRange(db.Forecasts);
            db.Forecasts.AddRange(SeedForecasts);
            db.SaveChanges();
        }

        private static List<Forecast> SeedForecasts => new List<Forecast>
        {
            new Forecast{ Date = new DateTime(2020, 05, 01), ID = 1, TemperatureC = 20},
            new Forecast{ Date = new DateTime(2020, 01, 01), ID = 2, TemperatureC = 15},
            new Forecast{ Date = new DateTime(2020, 03, 01), ID = 3, TemperatureC = 17},
            new Forecast{ Date = new DateTime(2020, 02, 01), ID = 4, TemperatureC = 16},
            new Forecast{ Date = new DateTime(2020, 06, 01), ID = 5, TemperatureC = 21},
        };
    }
}
