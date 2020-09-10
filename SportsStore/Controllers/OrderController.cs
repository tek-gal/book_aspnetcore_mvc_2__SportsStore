using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers { 
    public class OrderController: Controller {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repository, Cart cart) {
            this.repository = repository;
            this.cart = cart;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order) {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry your cart is empty");
            }
            if (ModelState.IsValid) {
                order.Lines = cart.Lines.ToArray();
                this.repository.SaveOrder(order);
                return RedirectToAction(nameof(this.Completed));
            } else {
                return View(order);
            }
        }

        public ViewResult Completed() {
            this.cart.Clear();
            return View();
        }
    }
}