using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions
{
    public static class CheckoutSolution
    {
        public static int Checkout(string skus)
        {
            var checkoutEngine = new CheckoutEngine();
            foreach (char sku in skus)
            {
                checkoutEngine.Add(sku);
            }
            return checkoutEngine.CalculateTotal();
        }
    }

    public class CheckoutEngine
    {
        private readonly IDictionary<char, Item> _priceList = new Dictionary<char, Item>
        {
            {'A', new Item('A', 50)},
            {'B', new Item('B', 30)},
            {'C', new Item('C', 20)},
            {'D', new Item('D', 15)},
            {'E', new Item('D', 40)}
        };

        private readonly IList<Discount> configuredDiscounts = new List<Discount>
        {
            new Discount('A', new KeyValuePair<int, int>(3,20), new KeyValuePair<int,int>(5,50)),
            new Discount('B', 15, 2) ,
            //new Discount('E', 'B', 2)
        };

        private readonly Basket _basket = new Basket();

        public void Add(char sku)
        {
            if (!_priceList.ContainsKey(sku))
            {
                _basket.Add(null);
                return;
            }
            _basket.Add(_priceList[sku]);
        }

        public int CalculateTotal()
        {
            return _basket.CalculateTotal(configuredDiscounts);
        }
    }

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

    public class Item 
    {
        public char Sku { get; }
        public int Price { get; }
        public Discount Discount { get; }

        public Item(char sku, int price) : this(sku, price, null)
        {}

        public Item(char sku, int price, Discount discount)
        {
            Sku = sku;
            Price = price;
            Discount = discount;
        }

        public bool HasDiscount()
        {
            return Discount != null;
        }
        
        protected bool Equals(Item other)
        {
            return Sku == other.Sku && Price == other.Price && Equals(Discount, other.Discount);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Sku.GetHashCode();
                hashCode = (hashCode * 397) ^ Price;
                hashCode = (hashCode * 397) ^ (Discount != null ? Discount.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
