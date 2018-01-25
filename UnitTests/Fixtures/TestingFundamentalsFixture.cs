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
    }
}