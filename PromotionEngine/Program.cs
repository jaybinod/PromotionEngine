using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //create list of promotions
            //we need to add information about Product's count
            Dictionary<String, int> offer1 = new Dictionary<String, int>();
            offer1.Add("A", 3);
            Dictionary<String, int> offer2 = new Dictionary<String, int>();
            offer2.Add("B", 2);
            Dictionary<String, int> offer3= new Dictionary<String, int>();
            offer3.Add("C", 1);
            offer3.Add("D", 1);

            //Adding here product offer promotion rate and discount value
            List<Promotion> promotions = new List<Promotion>()
            {
                new Promotion(1, offer1, 130, 20),
                new Promotion(2, offer2, 45, 15),
                new Promotion(3, offer3, 30, 5)
            };

            //create orders
            List<Order> orders = new List<Order>();
            Order order1 = new Order(1, new List<Product>() { new Product("A"), new Product("B"), new Product("C") });
            Order order2 = new Order(2, new List<Product>() { new Product("A"), new Product("A"), new Product("A"), new Product("A"), new Product("A"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("C") });
            Order order3 = new Order(3, new List<Product>() { new Product("A"), new Product("A"), new Product("A"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("C"), new Product("D") });
            orders.AddRange(new Order[] {order1, order2, order3 });
            //check if order meets promotion
            foreach (Order ord in orders)
            {
                List<decimal> promoPrices = promotions
                    .Select(promo => PromotionChecker.GetTotalPriceDiscount(ord, promo))
                    .ToList();
                decimal origPrice = ord.Products.Sum(prod => prod.Price);
                decimal promoPriceDiscount = promoPrices.Sum();
                Console.WriteLine($"OrderID: {ord.OrderID} => Original price: {origPrice.ToString("0.00")} | Promotional Discount: {promoPriceDiscount.ToString("0.00")} | Final price: {(origPrice - promoPriceDiscount).ToString("0.00")}");
            }
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        
    }
}

