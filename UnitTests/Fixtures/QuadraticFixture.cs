using System;
using NUnit.Framework;

namespace UnitTests.Fixtures
{
    [TestFixture]
    public class QuadraticFixture
    {
        [Test]
        public void Test()
        {
            Solve.Quadratic(1, 10, 16);
        }

        [Test]
        public void Test2()
        {
            Assert.Throws<Exception>(() =>
            {
                Solve.Quadratic(1, 1, 1);
            });
        }
    }
}
