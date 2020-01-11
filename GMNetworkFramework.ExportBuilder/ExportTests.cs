using GMNetworkFramework.Export;
using NUnit.Framework;

namespace GMNetworkFramework.Tests
{
    [TestFixture]
    class ExportTests
    {
        [Test]
        public void TestAdd()
        {
            Assert.AreEqual(4, Main.Add(2, 2));
        }

        [Test]
        public void TestMin()
        {
            Assert.AreEqual(-1, Main.Minus(3,4));
        }
    }
}
