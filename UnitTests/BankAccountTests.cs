using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount _bankAccount;

        [SetUp]
        public void Setup()
        {
            _bankAccount = new BankAccount(100);
        }

        [Test]
        public void BankAccountShouldIncreaseOnPositiveDeposit()
        {
            // AAA: Arrange, Act and Assert

            _bankAccount.Deposit(100);

            Assert.That(_bankAccount.Balance, Is.EqualTo(200));
        }

        [Test]
        public void BankAccountShouldThrowOnNonPositiveAmount()
        {
            var ex = Assert.Throws<ArgumentException>(() => _bankAccount.Deposit(-100));

            StringAssert.StartsWith("Deposit amount should be positive", ex.Message);
        }
    }
}