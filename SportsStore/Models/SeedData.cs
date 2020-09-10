using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models {

    public static class SeedData {

        public static void EnsurePopulated (IApplicationBuilder appBuilder) {
            ApplicationDbContext context = appBuilder.ApplicationServices
                .GetRequiredService<ApplicationDbContext> ();
            // an article about set up and connetion to sql server on linux
            // https://docs.microsoft.com/ru-ru/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-ver15
            context.Database.Migrate ();
            if (!context.Products.Any ()) {
                context.Products.AddRange (
                    new Product {
                        Name = "Kayak",
                            Description = "A boat for one person",
                            Category = "Watersports",
                            Price = 275M
                    },
                    new Product {
                        Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            Category = "Watersports",
                            Price = 48.95M
                    },
                    new Product {
                        Name = "Soccer ball",
                            Description = "FIFA-approved size and weight",
                            Category = "Soccer",
                            Price = 19.50M
                    },
                    new Product {
                        Name = "Corner Flags",
                            Description = "Give your playing field a professional touch",
                            Category = "Soccer",
                            Price = 34.95M
                    },
                    new Product {
                        Name = "Stadium",
                            Description = "Flat-packed 35,000-seat stadium",
                            Category = "Soccer",
                            Price = 79500
                    },
                    new Product {
                        Name = "Thinking Cap",
                            Description = "Improve brain efficiency by 75%",
                            Category = "Chess",
                            Price = 16
                    },
                    new Product {
                        Name = "Unsteady Chair",
                            Description = "Secretly give your opponent a disadvantage",
                            Category = "Chess",
                            Price = 29.95M
                    },
                    new Product {
                        Name = "Human Chess Board",
                            Description = "A fun game for the family",
                            Category = "Chess",
                            Price = 75
                    },
                    new Product {
                        Name = "Bling-Bling King",
                            Description = "Gold-plated, diamond-studded King",
                            Category = "Chess",
                            Price = 1200
                    }
                );
                context.SaveChanges ();
            }
        }

    }

}