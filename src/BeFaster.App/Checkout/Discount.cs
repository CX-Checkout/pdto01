using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class Discount
    {
        public readonly KeyValuePair<int, int> Condition;
        public char Sku { get; }
        public int Matching { get; }
        public int Value { get; }

        public Discount(char sku, int matchingQuantity, int discountAmount)
        {
            Sku = sku;
            Matching = matchingQuantity;
            Value = discountAmount;
        }

        public bool CanApplyTo(IList<Item> itemsLeft)
        {
            return itemsLeft.Count(item => item.Sku == Sku) >= Condition.Key;
        }

        public int ApplyDiscount(ref IList<Item> itemsLeft)
        {
            var itemDiscounted = itemsLeft.Where(item => item.Sku == Sku).Take(Condition.Key).ToList();
            foreach (var item in itemDiscounted)
            {
                itemsLeft.Remove(item);
            }
            return Condition.Value;
        }
    }
}
