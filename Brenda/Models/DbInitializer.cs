using System;
using System.Collections.Generic;
using System.Linq;

namespace Brenda.Models
{
    public static class DbInitializer
    {
        public static void Initialize(BrendaContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Forecasts.Any())
            {
                return;   // DB has been seeded
            }
            List<Forecast> forcasts = new List<Forecast>();
            for (int i = 0; i < 20; i++)
            {
                forcasts.Add(new Forecast
                {
                    Date = DateTime.Today.AddDays(i),
                    TemperatureC = Convert.ToInt32((Math.Sin(2 * Math.PI * i / 20) + 1) * 75 / 2 - 20)
                });
            }
            context.Forecasts.AddRange(forcasts);
            context.SaveChanges();
        }
    }

}
