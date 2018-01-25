using System.Collections.Generic;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace UnitTests.Fixtures
{
    [TestFixture]
    public class TestingFundamentalsFixture
    {
        [Test]
        public void Assertions()
        {
            Assert.That(2 + 2, Is.EqualTo(4));
            Assert.Pass();
            Assert.Warn("This is not good.");
            Assert.Inconclusive();
        }

        [Test]
        public void Warnings()
        {
            Warn.If(2 + 2 != 5);
            Warn.If(2 + 2, Is.Not.EqualTo(5));
            Warn.If(() => 2 + 2, Is.Not.EqualTo(5).After(2000));

            Warn.Unless(2 + 2 == 5);
            Warn.Unless(2 + 2, Is.EqualTo(5));
            Warn.Unless(() => 2 + 2, Is.EqualTo(5).After(2000));

            Assert.Warn("I'm warning ya!");
        }

        [Test]
        public void MultipleAssertions()
        {
            Assert.Multiple(() =>
            {
                Assert.That(1, Is.EqualTo(0));
                Assert.That(2, Is.LessThan(1));
            });
        }

        [Test]
        public void Test3()
        {
            dotMemory.Check(m =>
            {
                Assert.That(m.GetObjects(
                    w => w.Type.Is<Solve>()
                ).ObjectsCount, Is.EqualTo(0));
            });
        }

        [Test]
        public void Test4()
        {
            var checkpoint1 = dotMemory.Check();

            //...

            dotMemory.Check(m =>
            {
                Assert.That(m.GetTrafficFrom(checkpoint1)
                        .Where(w => w.Interface.Is<IEnumerable<int>>()).AllocatedMemory.SizeInBytes,
                    Is.LessThan(1000));
            });
        }
    }
}