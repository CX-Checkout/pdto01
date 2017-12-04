using System;
using System.Collections.Generic;
using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions
{
    public static class CheckoutSolution
    {
        private static readonly PriceList PriceList = new PriceList();

        public static int Checkout(string skus)
        {
            foreach (char sku in skus)
            {
                if (!PriceList.Contains(sku)) return -1;
            }
            throw new Exception();
        }

        
    }

    public class PriceList
    {
        private readonly IDictionary<char, Item> _items = new Dictionary<char, Item>();

        public bool Contains(char sku)
        {
            return _items.ContainsKey(sku);
        }
    }

    public class Item 
    {
    }
}
