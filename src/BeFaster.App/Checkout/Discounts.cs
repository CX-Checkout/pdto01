using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
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
            return nextDiscount.ApplyDiscount(ref itemsLeft);
        }
    }
}