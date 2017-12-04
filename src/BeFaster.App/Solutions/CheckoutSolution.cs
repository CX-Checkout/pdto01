using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions
{
    public static class CheckoutSolution
    {
        private static readonly CheckoutEngine CheckoutEngine = new CheckoutEngine();

        public static int Checkout(string skus)
        {
            foreach (char sku in skus)
            {
                CheckoutEngine.Add(sku);
            }
            return CheckoutEngine.CalculateTotal();
        }

        
    }

    public class CheckoutEngine
    {
        private readonly IDictionary<char, Item> _priceList = new Dictionary<char, Item>
        {
            {'A', new Item('A', 50)},
            {'B', new Item('B', 30)},
            {'C', new Item('C', 20)},
            {'D', new Item('D', 15)}
        };

        private readonly IList<Item> _basket = new List<Item>();

        public bool Contains(char sku)
        {
            return _priceList.ContainsKey(sku);
        }

        public void Add(char sku)
        {
            if (!_priceList.ContainsKey(sku))
            {
                _basket.Add(null);
                return;
            }
            _basket.Add(_priceList[sku]);
        }

        public int CalculateTotal()
        {
            if (_basket.Contains(null)) return -1;
            return _basket.Select(x=>x.Price).Sum();
        }
    }

    public class Item 
    {
        public char Sku { get; }
        public int Price { get; }

        public Item(char sku, int price)
        {
            Sku = sku;
            Price = price;
        }
    }
}
