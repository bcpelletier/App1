using System;
using NUnit.Framework;


namespace UnitTestApp1
{
    [TestFixture]
    public class TestsSample
    {

        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public void Pass()
        {
            Console.WriteLine("test1");
            Assert.True(true);
        }

        [Test]
        public void Fail()
        {
            Assert.False(true);
        }

        [Test]
        [Ignore("another time")]
        public void Ignore()
        {
            Assert.True(false);
        }

        [Test]
        public void Inconclusive()
        {
            Assert.Inconclusive("Inconclusive");
        }

        [Test]
        public void Prueba()
        {
            ProductViewModel p = new ProductViewModel();

            p.Search();

            Assert.True(false);
        }
    }
}