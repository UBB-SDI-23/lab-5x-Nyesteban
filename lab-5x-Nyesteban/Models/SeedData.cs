using lab_1_Nyesteban.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace lab_1_Nyesteban.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StoreContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<StoreContext>>()))
        {
            if (context.Apps.Any())
            {
                return;   // DB has been seeded
            }
            context.Apps.AddRange(
                new App
                {
                    AppName = "app1",
                    AppVersion = "ver1",
                    AppDescription = "first app",
                    AppSize = 128,
                    AppPrice = 7.99M
                }
            );
            context.SaveChanges();
        }
    }
}