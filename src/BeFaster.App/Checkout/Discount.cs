using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class Discount : IDiscount
    {
        public char Sku { get; }
        public int MatchingQuantity { get; }
        public int DiscountValue { get; }

        public Discount(char sku, int matchingQuantity, int discountAmount)
        {
            Sku = sku;
            MatchingQuantity = matchingQuantity;
            DiscountValue = discountAmount;
        }

        public bool CanApplyTo(IList<Item> itemsLeft)
        {
            return itemsLeft.Count(item => item.Sku == Sku) >= MatchingQuantity;
        }

        public int ApplyDiscount(ref IList<Item> itemsLeft)
        {
            var itemDiscounted = itemsLeft.Where(item => item.Sku == Sku).Take(MatchingQuantity).ToList();
            foreach (var item in itemDiscounted)
            {
                itemsLeft.Remove(item);
            }
            return DiscountValue;
        }
    }

    public interface IDiscount
    {
        bool CanApplyTo(IList<Item> itemsLeft);
        int ApplyDiscount(ref IList<Item> itemsLeft);
        int DiscountValue { get; }
    }

    public class FreeItemDiscount : IDiscount
    {
        public FreeItemDiscount(char c, int matchingQuantity, Item freeItem)
        {

        }
    }
}
