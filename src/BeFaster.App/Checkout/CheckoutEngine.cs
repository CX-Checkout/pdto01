using System.Collections.Generic;

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
            {'E', new Item('D', 40)}
        };

        private readonly IList<Discount> configuredDiscounts = new List<Discount>
        {
            new Discount('A', new KeyValuePair<int, int>(3,20)), 
            new Discount('A', new KeyValuePair<int,int>(5,50)),
            new Discount('B', 15, 2) ,
            //new Discount('E', 'B', 2)
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