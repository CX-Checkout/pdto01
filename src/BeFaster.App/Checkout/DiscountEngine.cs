using System.Collections.Generic;

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
}