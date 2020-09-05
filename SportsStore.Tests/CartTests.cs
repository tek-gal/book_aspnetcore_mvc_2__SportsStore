using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests {

    public class CartTests {
        [Fact]
        public void Can_Add_New_Lines() {
            Product product1 = new Product { ProductID = 1, Name = "P1" };
            Product product2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);

            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(product1, results[0].Product);
            Assert.Equal(product2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines() {
            Product product1 = new Product { ProductID = 1, Name = "P1" };
            Product product2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);
            target.AddItem(product1, 10);

            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_line() {
            Product product1 = new Product { ProductID = 1, Name = "P1" };
            Product product2 = new Product { ProductID = 2, Name = "P2" };
            Product product3 = new Product { ProductID = 3, Name = "P3" };

            Cart target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);
            target.AddItem(product3, 5);
            target.AddItem(product2, 10);

            target.RemoveLine(product2);

            Assert.Equal(0, target.Lines.Where(l => l.Product == product2).Count());
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Can_Calculate_Total() {
            Product product1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product product2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 4);

            decimal result = target.ComputeTotalValue();

            Assert.Equal(300M, result);
        }

        [Fact]
        public void Can_Clear_Contents() {
            Product product1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product product2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 4);

            target.Clear();

            Assert.Equal(0, target.Lines.Count());
        }
    }

}