using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private Weapon weapon;

            [SetUp]
            public void SetUp()
            {
                weapon = new Weapon("test", 10, 3);
            }

            [TestCase(-1)]
            [TestCase(-100)]
            public void Test_Price_ShouldThrowException(int price)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    weapon = new Weapon("test", price, 3);
                });
            }

            [Test]
            public void Test_Constructor_ShouldWork()
            {
                Assert.IsFalse(weapon.IsNuclear);
                Assert.That(weapon.Price, Is.EqualTo(10));
                Assert.That(weapon.Name, Is.EqualTo("test"));
                Assert.That(weapon.DestructionLevel, Is.EqualTo(3));
            }
        }
    }
}
