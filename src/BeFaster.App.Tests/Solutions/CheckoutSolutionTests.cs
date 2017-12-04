using BeFaster.App.Solutions;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions
{
    [TestFixture]
    public class CheckoutSolutionTests
    {
        [TestCase("AABBCCDDAAB", 325)]
        [TestCase("AAAAA", 200)]
        [TestCase("ABCD", 115)]
        [TestCase("fsdfd", -1)]
        public void Checkout(string skus, int expectedTotal)
        {
            Assert.That(CheckoutSolution.Checkout(skus), Is.EqualTo(expectedTotal));
        }
    }
}
