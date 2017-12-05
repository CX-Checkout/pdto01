using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions
{
    public class Discount
    {
        readonly IList<KeyValuePair<int, int>> _conditions = new List<KeyValuePair<int, int>>();
        public char Sku { get; }

        public Discount(char sku, params KeyValuePair<int, int>[] conditions)
        {
            _conditions = conditions.ToList();
            Sku = sku;
        }

        public Discount(char sku, int amount, int numberOfItems)
        {
            Sku = sku;
            _conditions.Add(new KeyValuePair<int, int>(numberOfItems, amount));
        }

        public int Max()
        {
            return _conditions.Max(c => c.Key);
        }

        public int MaxValue()
        {
            return _conditions.Max(c => c.Value);
        }

        public int Min()
        {
            return _conditions.Min(c => c.Key);
        }

        public int MinValue()
        {
            return _conditions.Min(c => c.Value);
        }
    }
}