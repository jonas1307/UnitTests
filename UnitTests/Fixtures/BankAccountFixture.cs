using System;
using NUnit.Framework;

namespace UnitTests.Fixtures
{
    [TestFixture]
    public class BankAccountFixture
    {
        private BankAccount _ba;

        [SetUp]
        public void Setup()
        {
            _ba = new BankAccount(100);
        }

        [Test]
        public void BankAccountShouldIncreaseOnPositiveDeposit()
        {
            // AAA: Arrange, Act and Assert

            _ba.Deposit(100);

            Assert.That(_ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void BankAccountShouldThrowOnNonPositiveAmount()
        {
            var ex = Assert.Throws<ArgumentException>(() => _ba.Deposit(-100));

            StringAssert.StartsWith("Deposit amount should be positive", ex.Message);
        }

        [Test]
        [TestCase(50, true, 50)]
        [TestCase(100, true, 0)]
        [TestCase(1000, false, 100)]
        public void TestMultipleWithdrawScenarios(int amountToWithdraw, bool shouldSucceed, int expectedBalance)
        {
            var result = _ba.Withdraw(amountToWithdraw);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(shouldSucceed));
                Assert.That(_ba.Balance, Is.EqualTo(expectedBalance));
            });
        }
    }
}