using BeFaster.App.Solutions;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions
{
    [TestFixture]
    public class CheckoutSolutionTests
    {
        [TestCase("HHHHHGHHHHHGHHHHH", 165)]
        [TestCase("AABBCCDDAABEEFFF", 395)]
        [TestCase("AABBCCDDAABEE", 375)]
        [TestCase("ABCDEE", 165)]
        [TestCase("NNNPPPPP", 320)]
        [TestCase("NNNPPPPPM", 320)]
        [TestCase("NNNNNNPPPPPM", 440)]
        [TestCase("NNNNNNPPPPPMM", 440)]
        [TestCase("NNNNNNPPPPPMMM", 455)]
        [TestCase("PPPPP", 200)]
        [TestCase("PPPPPBB", 245)]
        [TestCase("AABBCCDDAAB", 325)]
        [TestCase("AAAAA", 200)]
        [TestCase("BB", 45)]
        [TestCase("KK", 150)]
        [TestCase("FFF", 20)]
        [TestCase("FFFVV", 110)]
        [TestCase("QQQ", 80)]
        [TestCase("QQQVVV", 210)]
        [TestCase("RRR", 150)]
        [TestCase("VVUVUVUV", 340)]
        [TestCase("UUU", 120)]
        [TestCase("UUUU", 120)]
        [TestCase("UUUUU", 160)]
        [TestCase("VWXYZ", 220)]
        [TestCase("QQRRR", 180)]
        [TestCase("RRQRQQ", 210)]
        [TestCase("RRQR", 150)]
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
