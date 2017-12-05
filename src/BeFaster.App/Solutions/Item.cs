namespace BeFaster.App.Solutions
{
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