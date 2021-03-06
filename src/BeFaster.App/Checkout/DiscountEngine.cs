﻿using System.Collections.Generic;

namespace BeFaster.App.Checkout
{
    public class DiscountEngine
    {
        private readonly Discounts _configuredDiscounts;

        public DiscountEngine(IList<IDiscount> configuredDiscounts)
        {
            _configuredDiscounts = new Discounts(configuredDiscounts);
        }

        public int CalculateDiscount(IList<Item> items)
        {
            var itemsLeft = items;
            int totalDiscount = 0;
            while (_configuredDiscounts.CanApplyTo(itemsLeft))
            {
                totalDiscount += _configuredDiscounts.ApplyDiscount(ref itemsLeft);
            }
            return totalDiscount;
        }
    }
}