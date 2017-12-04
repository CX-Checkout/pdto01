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
            new Discount('A', 20, 3) ,
            new Discount('A', 50, 5) ,
            new Discount('B', 15, 2) ,
            new Discount('E', 'B', 2)
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
            return _basket.CalculateTotal();
        }
    }

    public class Basket
    {
        private readonly IList<Item> _basket = new List<Item>();

        public void Add(Item item)
        {
            _basket.Add(item);
        }

        public int CalculateTotal()
        {
            if (_basket.Contains(null)) return -1;
            var total = _basket.Select(x => x.Price).Sum();
            return total - CalculateDiscount();
        }

        private int CalculateDiscount()
        {
            int totalDiscount = 0;
            var distinctItems = _basket.Where(item=>item.HasDiscount()).Distinct();
            foreach (Item listedItem in distinctItems)
            {
                totalDiscount += CalculateItemDiscount(listedItem);
            }
            return totalDiscount;
        }

        private int CalculateItemDiscount(Item listedItem)
        {
            var applicableTimes = _basket.Count(x => x.Sku.Equals(listedItem.Sku)) / listedItem.Discount.NumberOfItems;
            return listedItem.Discount.Amount * applicableTimes;
        }
    }

    public class Discount
    {
        public char Sku { get; }
        public int Amount { get; }
        public int NumberOfItems { get; }

        public Discount(char sku, int amount, int numberOfItems)
        {
            Sku = sku;
            Amount = amount;
            NumberOfItems = numberOfItems;
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
