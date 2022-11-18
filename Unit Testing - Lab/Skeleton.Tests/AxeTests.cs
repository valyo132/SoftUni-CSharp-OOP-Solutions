using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe = null;
        private int attackPoints = 5;
        private int durabilityPoints = 10;

        private Dummy dummy = null;
        private int health = 10;
        private int experience = 10;

        [SetUp]
        public void SetUp()
        {
            axe = new Axe(attackPoints, durabilityPoints);
            dummy = new Dummy(health, experience);
        }

        [Test]
        public void Test_AxeConstructorShouldSetTheValuesProperly()
        {
            Assert.AreEqual(attackPoints, axe.AttackPoints);
            Assert.AreEqual(durabilityPoints, axe.DurabilityPoints);
        }

        [Test]
        public void Test_AxeDurabilityShoudDropAfterAnAttack()
        {
            axe.Attack(dummy);
            Assert.AreEqual(durabilityPoints - 1, axe.DurabilityPoints);
        }

        [Test]
        public void Test_AxeShuldTrowExceptionWhenIsBroken()
        {
            axe = new Axe(10, 0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                axe.Attack(dummy);
            });

            axe = new Axe(10, -1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                axe.Attack(dummy);
            });
        }
    }
}