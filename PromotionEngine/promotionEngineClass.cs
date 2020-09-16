using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{

    public class Product
    {
        public string Id { get; set; }
        public decimal Price { get; set; }

        public Product(string id)
        {
            this.Id = id;
            switch (id)
            {
                case "A":
                    this.Price = 50m;

                    break;
                case "B":
                    this.Price = 30m;

                    break;
                case "C":
                    this.Price = 20m;

                    break;
                case "D":
                    this.Price = 15m;
                    break;
            }
        }
    }

    public class Promotion
    {
        public int PromotionID { get; set; }
        public Dictionary<string, int> ProductInfo { get; set; }
        public decimal PromoPrice { get; set; }
        public decimal PromoPriceDiscount { get; set; }
        public Promotion(int _promID, Dictionary<string, int> _prodInfo, decimal _pp, decimal ppd)
        {
            this.PromotionID = _promID;
            this.ProductInfo = _prodInfo;
            this.PromoPrice = _pp;
            this.PromoPriceDiscount = ppd;
        }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public List<Product> Products { get; set; }

        public Order(int _oid, List<Product> _prods)
        {
            this.OrderID = _oid;
            this.Products = _prods;
        }
    }

    public static class PromotionChecker
    {
        //get count of promoted products from promotion and return discount value
        public static decimal GetTotalPriceDiscount(Order ord, Promotion prom)
        {
            decimal d = 0M;
            var copp = ord.Products
                .GroupBy(x => x.Id)
                .Where(grp => prom.ProductInfo.Any(y => grp.Key == y.Key && grp.Count() >= y.Value))
                .Select(grp => grp.Count())
                .Sum();
            
            int ppc = prom.ProductInfo.Sum(kvp => kvp.Value);
            while (copp >= ppc)
            {
                d += prom.PromoPriceDiscount;
                copp -= ppc;
            }
            return d;
        }
    }
}
