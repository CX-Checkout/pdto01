using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class DiscountEngine
    {
        private readonly Discounts _configuredDiscounts;
        private IList<Item> _items;

        public DiscountEngine(IList<Discount> configuredDiscounts)
        {
            _configuredDiscounts = new Discounts(configuredDiscounts);
        }

        public int CalculateDiscount(IList<Item> items)
        {
            _items = items;
            var itemsLeft = _items;
            int totalDiscount = 0;
            while (_configuredDiscounts.CanApplyTo(itemsLeft))
            {
                totalDiscount += _configuredDiscounts.ApplyDiscount(ref itemsLeft);
            }
            return totalDiscount;
        }
    }

    public class Discounts
    {
        private readonly IList<Discount> _configuredDiscounts;

        public Discounts(IList<Discount> configuredDiscounts)
        {
            _configuredDiscounts = configuredDiscounts.OrderByDescending(d=>d.Condition.Value).ToList();
        }

        public bool CanApplyTo(IList<Item> itemsLeft)
        {
            return _configuredDiscounts.Any(d => d.CanApplyTo(itemsLeft));
        }

        public int ApplyDiscount(ref IList<Item> itemsLeft)
        {
            var items = itemsLeft;
            var nextDiscount = _configuredDiscounts.First(d => d.CanApplyTo(items));
            return nextDiscount.CalculateAmount(ref itemsLeft);
        }
    }
}
