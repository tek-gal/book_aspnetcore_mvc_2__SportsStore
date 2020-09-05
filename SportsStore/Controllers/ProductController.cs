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

        public ViewResult List(string category, int productPage = 1) => 
            View(new ProductListViewModel {
                Products = this.repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * this.PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = productPage,
                    ItemsPerPage = this.PageSize,
                    TotalItems = category == null 
                        ? this.repository.Products.Count()
                        : this.repository.Products
                            .Where(p => p.Category == category)
                            .Count()
                },
                CurrentCategory = category
            });
    }

}
