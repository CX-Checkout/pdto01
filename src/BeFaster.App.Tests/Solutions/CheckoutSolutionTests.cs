using BeFaster.App.Solutions;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions
{
    [TestFixture]
    public class CheckoutSolutionTests
    {
        [TestCase("AABBCCDDAAB", 325)]
        public void Checkout(string skus, int expectedTotal)
        {
            Assert.That(CheckoutSolution.Checkout(skus), Is.EqualTo(325));
        }
    }
}
