using System;
using System.Dynamic;
using NUnit.Framework;
using UnitTests;
using ImpromptuInterface;

namespace UnitTests
{
    public interface ILog
    {
        bool Write(string msg);
    }

    public class ConsoleLog : ILog
    {
        public bool Write(string msg)
        {
            Console.WriteLine(msg);
            return true;
        }
    }

    public class BankAccountNew
    {
        public int Balance { get; set; }
        private readonly ILog _log;

        public BankAccountNew(ILog log)
        {
            _log = log;
        }

        public void Deposit(int amount)
        {
            if (_log.Write($"Depositing $ {amount}"))
                Balance += amount;
        }
    }
}

public class NullLog : ILog
{
    public bool Write(string msg)
    {
        return true;
    }
}

public class NullLogWithResult : ILog
{
    private readonly bool _expectedResult;

    public NullLogWithResult(bool expectedResult)
    {
        _expectedResult = expectedResult;
    }
    public bool Write(string msg)
    {
        return _expectedResult;
    }
}

public class Null<T> : DynamicObject where T : class
{
    public static T Instance
    {
        get { return new Null<T>().ActLike<T>(); }
    }

    public override bool TryInvokeMember(InvokeMemberBinder binder,
        object[] args, out object result)
    {
        result = Activator.CreateInstance(typeof(T).GetMethod(binder.Name).ReturnType);

        return true;
    }
}

[TestFixture]
public class BankAccountNewTests
{
    private BankAccountNew ba;

    [Test]
    public void DepositIntegrationTest()
    {
        ba = new BankAccountNew(new ConsoleLog()) { Balance = 100 };
        ba.Deposit(100);

        Assert.That(ba.Balance, Is.EqualTo(200));
    }

    [Test]
    public void DepositUnitTestWithFake()
    {
        var log = new NullLog();

        ba = new BankAccountNew(log) { Balance = 100 };
        ba.Deposit(100);

        Assert.That(ba.Balance, Is.EqualTo(200));
    }

    [Test]
    public void DepositUnitTestWithDynamicFake()
    {
        // Dynamic fakes are good to instantiate, but they don't act like the class.
        var log = Null<ILog>.Instance;

        ba = new BankAccountNew(log) { Balance = 100 };
        ba.Deposit(100);

        Assert.That(ba.Balance, Is.EqualTo(200));
    }

    [Test]
    public void DepositUnitTestWithStub()
    {
        var log = new NullLogWithResult(true);

        ba = new BankAccountNew(log) { Balance = 100 };
        ba.Deposit(100);

        Assert.That(ba.Balance, Is.EqualTo(200));
    }
}