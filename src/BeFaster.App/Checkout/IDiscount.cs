using System.Collections.Generic;

namespace BeFaster.App.Checkout
{
    public interface IDiscount
    {
        bool CanApplyTo(IList<Item> itemsLeft);
        int ApplyDiscount(ref IList<Item> itemsLeft);
        int DiscountValue { get; }
    }
}