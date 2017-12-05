﻿using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Checkout
{
    public class Discount
    {
        public readonly KeyValuePair<int, int> Condition;
        public char Sku { get; }

        public Discount(char sku, KeyValuePair<int, int> condition)
        {
            Condition = condition;
            Sku = sku;
        }

        public Discount(char sku, int amount, int numberOfItems)
        {
            Sku = sku;
            Condition = new KeyValuePair<int, int>(numberOfItems, amount);
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