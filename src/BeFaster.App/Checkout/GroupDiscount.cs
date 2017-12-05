using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class GroupDiscount : IDiscount
    {
        public char[] Skus { get; }
        public int MatchingQuantity { get; }
        public int ForfaitPrice { get; }

        public GroupDiscount(string skus, int matchingQuantity, int forfaitPrice)
        {
            Skus = skus.ToCharArray();
            MatchingQuantity = matchingQuantity;
            ForfaitPrice = forfaitPrice;
        }

        public bool CanApplyTo(IList<Item> itemsLeft)
        {
            return itemsLeft.Count(item => Skus.Contains(item.Sku)) >= MatchingQuantity;
        }

        public int ApplyDiscount(ref IList<Item> itemsLeft)
        {
            throw new System.NotImplementedException();
        }

        public int DiscountValue { get; }
    }
}