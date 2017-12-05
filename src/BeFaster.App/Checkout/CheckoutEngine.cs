using System.Collections.Generic;
using System.Diagnostics;

namespace BeFaster.App.Checkout
{
    public class CheckoutEngine
    {
        private readonly IDictionary<char, Item> _priceList = new Dictionary<char, Item>
        {
            {'A', new Item('A', 50)},
            {'B', new Item('B', 30)},
            {'C', new Item('C', 20)},
            {'D', new Item('D', 15)},
            {'E', new Item('E', 40)},
            {'F', new Item('F', 10)},
        };

        private readonly IList<IDiscount> configuredDiscounts = new List<IDiscount>
        {
            new Discount('A', 3, 20), 
            new Discount('A', 5, 50),
            new Discount('B', 2, 15),
            new Discount('F', 3, 10),
            new FreeItemDiscount('E', 2, new Item('B', 30)),
        };

        private readonly Basket _basket = new Basket();

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
            return _basket.CalculateTotal(configuredDiscounts);
        }
    }

    
}