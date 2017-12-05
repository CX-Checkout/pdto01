using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class Basket
    {
        private readonly IList<Item> _basket = new List<Item>();

        public void Add(Item item)
        {
            _basket.Add(item);
        }

        public int CalculateTotal(IList<Discount> configuredDiscounts)
        {
            if (_basket.Contains(null)) return -1;
            var total = _basket.Select(x => x.Price).Sum();
            return total - CalculateDiscount(configuredDiscounts);
        }

        private int CalculateDiscount(IList<Discount> configuredDiscounts)
        {
            int totalDiscount = 0;
            foreach (Discount discount in configuredDiscounts)
            {
                totalDiscount += CalculateItemDiscount(discount);
            }
            return totalDiscount;
        }

        private int CalculateItemDiscount(Discount discount)
        {
            var items = _basket.Where(item => item.Sku == discount.Sku);
            if (!items.Any()) return 0;

            int discountAmount = 0;
            if (items.Count() >= discount.Max())
            {
                var applicableTimes = _basket.Count(x => x.Sku.Equals(discount.Sku)) / discount.Max();
                discountAmount += applicableTimes * discount.MaxValue();
            }
            if (discountAmount >= 0 && discount.Min()!= discount.Max())
            {
                var applicableTimes = (_basket.Count(x => x.Sku.Equals(discount.Sku)) % discount.Max()) / discount.Min();
                discountAmount += applicableTimes * discount.MinValue();
            }
            return discountAmount;
        }
    }
}
