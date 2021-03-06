﻿using System;
using NUnit.Framework;

namespace UnitTests.Fixtures
{
    [TestFixture]
    public class QuadraticFixture
    {
        [Test]
        public void QuadraticPositiveValue()
        {
            var result = Solve.Quadratic(1, 10, 16);

            Assert.Multiple(() =>
            {
                Assert.That(result.Item1, Is.EqualTo(-2));
                Assert.That(result.Item2, Is.EqualTo(-13));
            });
        }

        [Test]
        public void QuadraticNegativeValue()
        {
            Assert.Throws<Exception>(() =>
            {
                Solve.Quadratic(1, 1, 1);
            });
        }
    }
}
