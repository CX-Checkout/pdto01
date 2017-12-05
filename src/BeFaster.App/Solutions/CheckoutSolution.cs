using BeFaster.App.Checkout;

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
}
