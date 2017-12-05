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
            {'G', new Item('G', 20)},
            {'H', new Item('H', 10)},
            {'I', new Item('I', 35)},
            {'J', new Item('J', 60)},
            {'K', new Item('K', 80)},
            {'L', new Item('L', 90)},
            {'M', new Item('M', 15)},
            {'N', new Item('N', 40)},
            {'O', new Item('O', 10)},
            {'P', new Item('P', 50)},
            {'Q', new Item('Q', 30)},
            {'R', new Item('R', 50)},
            {'S', new Item('S', 30)},
            {'T', new Item('T', 20)},
            {'U', new Item('U', 40)},
            {'V', new Item('V', 50)},
            {'W', new Item('W', 20)},
            {'X', new Item('X', 90)},
            {'Y', new Item('Y', 10)},
            {'Z', new Item('Z', 50)}
        };

        private readonly IList<IDiscount> configuredDiscounts = new List<IDiscount>
        {
            new Discount('A', 3, 20), 
            new Discount('A', 5, 50),
            new Discount('B', 2, 15),
            new Discount('F', 3, 10),
            new FreeItemDiscount('E', 2, new Item('B', 30)),
            new Discount('H', 5, 5),
            new Discount('H', 10, 20),
            new Discount('K', 2, 10),
            new FreeItemDiscount('N', 3, new Item('M', 15)),
            new Discount('P', 5, 50),
            new Discount('Q', 3, 10),
            new FreeItemDiscount('R', 3, new Item('Q', 30)),
            new Discount('U', 4, 40)

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
