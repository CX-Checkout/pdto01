using BeFaster.App.Solutions;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions
{
    [TestFixture]
    public class CheckoutSolutionTests
    {
        [TestCase("AABBCCDDAABEEFFF", 395)]
        [TestCase("AABBCCDDAABEE", 375)]
        [TestCase("ABCDEE", 165)]
        [TestCase("AABBCCDDAAB", 325)]
        [TestCase("AAAAA", 200)]
        [TestCase("BB", 45)]
        [TestCase("FFF", 20)]
        [TestCase("AAAAAAAA", 330)]
        [TestCase("AAAAAAAAAAAAA", 530)]
        [TestCase("ABCDE", 155)]
        [TestCase("fsdfd", -1)]
        public void Checkout(string skus, int expectedTotal)
        {
            Assert.That(CheckoutSolution.Checkout(skus), Is.EqualTo(expectedTotal));
        }
    }
}
