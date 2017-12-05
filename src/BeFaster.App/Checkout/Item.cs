namespace BeFaster.App.Checkout
{
    public class Item 
    {
        public char Sku { get; }
        public int Price { get; }
        
        public Item(char sku, int price)
        {
            Sku = sku;
            Price = price;
        }
        
        protected bool Equals(Item other)
        {
            return Sku == other.Sku && Price == other.Price;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Sku.GetHashCode();
                hashCode = (hashCode * 397) ^ Price;
                return hashCode;
            }
        }
    }
}