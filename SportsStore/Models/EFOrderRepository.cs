using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
    public class EFOrderRepository : IOrderRepository {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext context) { 
            this.context = context;
        }

        public IQueryable<Order> Orders => this.context.Orders
            .Include(order => order.Lines)
            .ThenInclude(line => line.Product);

        public void SaveOrder(Order order) {
            context.AttachRange(order.Lines.Select(line => line.Product));
            if (order.OrderID == 0) {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }

}