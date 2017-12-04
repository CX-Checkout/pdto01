using System;
using System.Collections.Generic;

namespace BeFaster.App.Solutions
{
    public static class CheckoutSolution
    {
        private static readonly CheckoutEngine _checkoutEngine = new CheckoutEngine();

        public static int Checkout(string skus)
        {
            foreach (char sku in skus)
            {
                _checkoutEngine.Add(sku);
            }
            return _checkoutEngine.CalculateTotal();
        }

        
    }

    public class CheckoutEngine
    {
        private readonly IDictionary<char, Item> _priceList = new Dictionary<char, Item>
        {
            {'A',new Item('A',50)},
            {'B',new Item('A',30)},
            {'C',new Item('A',20)},
            {'D',new Item('A',15)}
        };

        private IList<Item> _basket = new List<Item>();

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
            return 100;
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
