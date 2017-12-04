using BeFaster.App.Solutions;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions
{
    [TestFixture]
    public class HelloSolutionTest
    {
       
        [Test]
        public void Hello()
        {
            Assert.That(HelloSolution.Hello("my Friend"), Is.EqualTo("Hello, my Friend!"));
        }
    }
}
