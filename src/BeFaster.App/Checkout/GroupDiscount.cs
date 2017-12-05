using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class GroupDiscount : IDiscount
    {
        public char[] Skus { get; }
        public int MatchingQuantity { get; }
        public int ForfaitPrice { get; }
        public int DiscountValue => 15; 

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
            var itemDiscounted = itemsLeft.Where(item => Skus.Contains(item.Sku)).OrderByDescending(item => item.Price).Take(MatchingQuantity).ToList();
            foreach (var item in itemDiscounted)
            {
                itemsLeft.Remove(item);
            }
            return itemDiscounted.Sum(item=>item.Price) - ForfaitPrice;
        }
    }
}