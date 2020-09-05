using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests {

    public class NavigationMenuComponentTests {

        [Fact]
        public void Can_Select_Categories() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                    new Product { ProductID = 4, Name = "P4", Category = "Oranges" },
                }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            string[] results = (
                (IEnumerable<string>)
                (target.Invoke()as ViewViewComponentResult).ViewData.Model
            ).ToArray();

            string[] expected = new string[] { "Apples", "Oranges", "Plums" };

            Assert.True(Enumerable.SequenceEqual(expected, results));
        }

        [Fact]
        public void Indicates_Selected_Category() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                    new Product { ProductID = 4, Name = "P4", Category = "Oranges" },
                }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext {
                ViewContext = new ViewContext {
                RouteData = new RouteData()
                }
            };

            string selectedCategory = "Apples";
            target.RouteData.Values["category"] = selectedCategory;

            string result = (string)(target.Invoke() as ViewViewComponentResult)
                .ViewData["SelectedCategory"];
            
            Assert.Equal(selectedCategory, result);
        }
    }
}