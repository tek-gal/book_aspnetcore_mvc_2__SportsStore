using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests {

    public class ProductControllerTests {

        [Fact]
        public void Can_Paginate() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" },
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductListViewModel result = controller.List(null, 2).ViewData.Model as ProductListViewModel;

            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);

        }

        [Fact]
        public void Can_Send_Pagination_View_Model() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((
                new Product[] {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" },
                }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };

            ProductListViewModel result = controller.List(null, 2).ViewData.Model as ProductListViewModel;

            PagingInfo pagingInfo = result.PagingInfo;

            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" },
                }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };

            string currentCategory = "Cat1";
            ProductListViewModel result = controller.List(currentCategory, 1).ViewData.Model as ProductListViewModel;

            Product[] products = result.Products.ToArray();

            Assert.Equal(2, products.Length);
            Assert.True(products[0].Name == "P1" && products[0].Category == currentCategory);
            Assert.True(products[1].Name == "P3" && products[1].Category == currentCategory);
        }

        [Fact]
        public void Generate_Category_Specifict_Product_Count() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" },
                }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };
            controller.PageSize = 3;

            Func<ViewResult, ProductListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductListViewModel;

            int? cat1Count = GetModel(controller.List("Cat1"))?.PagingInfo.TotalItems;
            int? cat2Count = GetModel(controller.List("Cat2"))?.PagingInfo.TotalItems;
            int? cat3Count = GetModel(controller.List("Cat3"))?.PagingInfo.TotalItems;
            int? allCount = GetModel(controller.List(null))?.PagingInfo.TotalItems;

            Assert.Equal(2, cat1Count);
            Assert.Equal(2, cat2Count);
            Assert.Equal(1, cat3Count);
            Assert.Equal(5, allCount);
        }
    }
}