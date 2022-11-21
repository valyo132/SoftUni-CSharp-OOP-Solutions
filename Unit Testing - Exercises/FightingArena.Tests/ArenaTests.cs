namespace FightingArena.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        Arena arena;
        Warrior attacker;
        Warrior defender;

        [SetUp]
        public void SetUp()
        {
            attacker = new Warrior("Pesho", 100, 100);
            defender = new Warrior("Gosho", 50, 150);

            arena = new Arena();
        }

        [Test]
        public void Test_ConstructorShouldWorkAndSetTheIReadOnlyCollection()
        {
            List<Warrior> list = new List<Warrior>();
            CollectionAssert.AreEqual(list, arena.Warriors);
        }

        [Test]
        public void Test_CountProperyShouldWork()
        {
            arena.Enroll(attacker);

            Assert.That(arena.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_EnrollShouldThrowExceptionIfParameterWarriorExistsInTheCollection()
        {
            arena.Enroll(attacker);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(attacker);
            });
        }

        [Test]
        public void Test_EnrollAddsTwoWarriorsShlouldWork()
        {
            arena.Enroll(attacker);
            arena.Enroll(defender);

            Assert.That(arena.Count, Is.EqualTo(2));
        }

        [Test]
        public void Test_EnrollShouldAddTheParameterWarriorToTheCollection()
        {
            arena.Enroll(attacker);

            Assert.That(arena.Warriors.Any(x => x.Name == "Pesho"));
        }

        [Test]
        public void Test_FightShouldThrowExceptionIfAttackerIsInvalid()
        {
            arena.Enroll(attacker);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight("Pesho", "NullName");
            });
        }

        [Test]
        public void Test_FightShouldThrowExceptionIDeffenderIsInvalid()
        {
            arena.Enroll(attacker);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight("NullName", "Pesho");
            });
        }

        [Test]
        public void Test_FightShouldWorkWithValidWarriors()
        {
            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight("Pesho", "Gosho");

            Assert.That(defender.HP, Is.EqualTo(50));
            Assert.That(attacker.HP, Is.EqualTo(50));
        }
    }
}
