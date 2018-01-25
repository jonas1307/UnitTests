using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class TestingFundamentals
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
    }
}