using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers {

    public class ProductController : Controller {

        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repository) {
            this.repository = repository;
        }

        public ViewResult List(int productPage = 1) => 
            View(new ProductListViewModel {
                Products = this.repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * this.PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = productPage,
                    TotalItems = this.repository.Products.Count(),
                    ItemsPerPage = this.PageSize
                }
            });
    }

}
