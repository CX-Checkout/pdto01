using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class Basket
    {
        private readonly IList<Item> _items = new List<Item>();

        public void Add(Item item)
        {
            _items.Add(item);
        }

        public int CalculateTotal(IList<IDiscount> configuredDiscounts)
        {
            if (_items.Contains(null)) return -1;
            var total = _items.Select(x => x.Price).Sum();
            var discount = configuredDiscounts != null ? new DiscountEngine(configuredDiscounts).CalculateDiscount(_items) : 0;
            return total - discount;
        }
    }
}