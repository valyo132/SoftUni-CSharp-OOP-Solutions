using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy = null;
        private Dummy deadDummy = null;
        private int health = 10;
        private int experience = 10;
        private int attackPoints = 1;

        [SetUp]
        public void SetUp()
        {
            dummy = new Dummy(health, experience);
            deadDummy = new Dummy(health, experience);
            deadDummy.TakeAttack(health + 20);
        }

        [Test]
        public void Test_DummyConstructorShouldSetTheValuesProperly()
        {
            Assert.AreEqual(health, dummy.Health);
        }

        [Test]
        public void Test_DummyShouldLooseHealthEachAttack()
        {
            dummy.TakeAttack(attackPoints);
            Assert.AreEqual(health - attackPoints, dummy.Health);
        }

        [Test]
        public void Test_DummyShoudThrowExceptionIfDeadAndHealthIsZero()
        {
            dummy.TakeAttack(health);

            Assert.Throws<InvalidOperationException>(() =>
            {
                deadDummy.TakeAttack(1);
            });
        }

        [Test]
        public void Test_DummyShoudThrowExceptionIfDeadAndHealthIsBelowZero()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                deadDummy.TakeAttack(1);
            });
        }

        [Test]
        public void Test_DummyShouldReturnXPIfDead()
        {
            var XP = deadDummy.GiveExperience();
            Assert.AreEqual(experience, XP);
        }

        [Test]
        public void Test_DummyShouldNotReturnXPIfAlive()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.GiveExperience();
            });
        }
    }
}