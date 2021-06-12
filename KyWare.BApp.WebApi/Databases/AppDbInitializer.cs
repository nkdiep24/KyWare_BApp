using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using KyWare.BApp.Data.Models;

namespace KyWare.BApp.WebApi.Databases
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "Book1",
                            Description = "Book1",
                            DateAdd = DateTime.Now,
                        },
                        new Book()
                        {
                            Title = "Book2",
                            Description = "Book2",
                            DateAdd = DateTime.Now,
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
