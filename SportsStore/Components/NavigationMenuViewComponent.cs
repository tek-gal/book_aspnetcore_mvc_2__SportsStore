using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components {

    public class NavigationMenuViewComponent : ViewComponent {
        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repository) {
            this.repository = repository;
        }

        // Invoke is used when component is used in Razor view
        public IViewComponentResult Invoke() {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(this.repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }

    }

}