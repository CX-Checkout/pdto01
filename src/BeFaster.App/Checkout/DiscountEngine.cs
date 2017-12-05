using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class DiscountEngine
    {
        private readonly IList<Discount> _configuredDiscounts;
        private IList<Item> _items;

        public DiscountEngine(IList<Discount> configuredDiscounts)
        {
            _configuredDiscounts = configuredDiscounts;
        }

        public int CalculateDiscount(IList<Item> items)
        {
            _items = items;
            int totalDiscount = 0;
            foreach (Discount discount in _configuredDiscounts)
            {
                totalDiscount += CalculateItemDiscount(discount);
            }
            return totalDiscount;
        }

        private int CalculateItemDiscount(Discount discount)
        {
            var items = _items.Where(item => item.Sku == discount.Sku);
            if (!items.Any()) return 0;

            int discountAmount = 0;
            if (items.Count() >= discount.Max())
            {
                var applicableTimes = _items.Count(x => x.Sku.Equals(discount.Sku)) / discount.Max();
                discountAmount += applicableTimes * discount.MaxValue();
            }
            if (discountAmount >= 0 && discount.Min() != discount.Max())
            {
                var applicableTimes = (_items.Count(x => x.Sku.Equals(discount.Sku)) % discount.Max()) / discount.Min();
                discountAmount += applicableTimes * discount.MinValue();
            }
            return discountAmount;
        }
    }
}