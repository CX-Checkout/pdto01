using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions
{
    public static class CheckoutSolution
    {
        public static int Checkout(string skus)
        {
            var checkoutEngine = new CheckoutEngine();
            foreach (char sku in skus)
            {
                checkoutEngine.Add(sku);
            }
            return checkoutEngine.CalculateTotal();
        }
    }

    public class CheckoutEngine
    {
        private readonly IDictionary<char, Item> _priceList = new Dictionary<char, Item>
        {
            {'A', new Item('A', 50, new Discount(20, 3))},
            {'B', new Item('B', 30, new Discount(15, 2))},
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

    public class Discount
    {
        public int Amount { get; }
        public int NumberOfItems { get; }

        public Discount(int amount, int numberOfItems)
        {
            Amount = amount;
            NumberOfItems = numberOfItems;
        }
    }

    public class Item 
    {
        public char Sku { get; }
        public int Price { get; }
        private Discount Discount { get; }

        public Item(char sku, int price) : this(sku, price, null)
        {}

        public Item(char sku, int price, Discount discount)
        {
            Sku = sku;
            Price = price;
            Discount = discount;
        }
    }
}
