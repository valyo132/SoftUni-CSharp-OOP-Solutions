namespace FightingArena.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;

        [Test]
        public void Test_ConstructorShouldSetTheValuesProperly()
        {
            warrior = new Warrior("Valyo", 100, 200);

            Assert.That(warrior.Name, Is.EqualTo("Valyo"));
            Assert.That(warrior.Damage, Is.EqualTo(100));
            Assert.That(warrior.HP, Is.EqualTo(200));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Test_NamePropertySouldThrowExceptionIfNameIsNullOrEmptyOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(name, 100, 200);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_DamageShouldThrowExceptionIfValueIsZeroOrNegative(int damage)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior("Valyo", damage, 200);
            });
        }

        [TestCase(1)]
        public void Test_DamageShouldSetPropery(int damage)
        {
            warrior = new Warrior("Valyo", damage, 200);

            Assert.That(warrior.Damage, Is.EqualTo(1));
        }

        [TestCase(-1)]
        public void Test_HPPropertyShouldThrowExceptionIfValueIsNegative(int hp)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior("Valyo", 100, hp);
            });
        }

        [TestCase(0)]
        public void Test_HPPropertyShouldSetProperly(int hp)
        {
            warrior = new Warrior("Valyo", 100, hp);

            Assert.That(warrior.HP, Is.EqualTo(0));
        }

        [TestCase(30)]
        [TestCase(29)]
        public void Test_AttackShouldThrowExceptionIfCurrWarriorHpIsBelowOrEqual30(int hp)
        {
            warrior = new Warrior("Pesho", 100, hp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(new Warrior("John", 50, 100));
            });
        }

        [Test]
        public void Test_AttackShouldWorkWithHPeOver30()
        {
            warrior = new Warrior("Pesho", 40, 40);
            Warrior attackedWarrior = new Warrior("John", 39, 100);

            warrior.Attack(attackedWarrior);

            Assert.That(attackedWarrior.HP, Is.EqualTo(60));
            Assert.That(warrior.HP, Is.EqualTo(1));
        }

        [TestCase(30)]
        [TestCase(29)]
        public void Test_AttackShouldThrowExceptionIfParameterWarriorHpIsBelowOrEqual30(int hp)
        {
            warrior = new Warrior("Pesho", 100, 100);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(new Warrior("John", 40, hp));
            });
        }

        [TestCase(40)]
        public void Test_AttackShouldWorkIfParameterWarriorHPisOver30(int hp)
        {
            warrior = new Warrior("Pesho", 35, 100);
            Warrior attackedWarrior = new Warrior("John", 34, hp);

            warrior.Attack(attackedWarrior);

            Assert.That(attackedWarrior.HP, Is.EqualTo(5));
            Assert.That(warrior.HP, Is.EqualTo(66));
        }

        [Test]
        public void Test_AttackShouldThrowExceptionIfCurrentWarriorHpIsBelowParameterWarriorDamage()
        {
            warrior = new Warrior("Pesho", 40, 40);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(new Warrior("John", 45, 100));
            });
        }

        [Test]
        public void Test_AttackShouldDecraseCurrentWarriorHP()
        {
            warrior = new Warrior("Pesho", 50, 100);
            Warrior attackedWarrior = new Warrior("John", 49, 31);

            warrior.Attack(attackedWarrior);

            Assert.That(warrior.HP, Is.EqualTo(51));
            Assert.That(attackedWarrior.HP, Is.EqualTo(0));
        }

        [Test]
        public void Test_AttackIfCurrentWarriorDamageIsBiggerThanParameterWarriorHPWarriorHPShouldBeZero()
        {
            warrior = new Warrior("Pesho", 50, 100);
            Warrior attackedWarrior = new Warrior("John", 49, 35);

            warrior.Attack(attackedWarrior);

            Assert.That(attackedWarrior.HP, Is.EqualTo(0));
        }

        [Test]
        public void Test_AttackIfCurrentWarriorDamageIsBelowOrEqualToWarriorHPThenWarriorHPSohuldBeDecreasedByCurrentWarriorDamage()
        {
            warrior = new Warrior("Pesho", 49, 100);
            Warrior attackedWarrior = new Warrior("John", 49, 50);

            warrior.Attack(attackedWarrior);
            Assert.That(attackedWarrior.HP, Is.EqualTo(1));
            Assert.That(warrior.HP, Is.EqualTo(51));

            warrior = new Warrior("Dimitrichko", 50, 100);
            attackedWarrior = new Warrior("John", 49, 50);

            warrior.Attack(attackedWarrior);
            Assert.That(attackedWarrior.HP, Is.EqualTo(0));
            Assert.That(warrior.HP, Is.EqualTo(51));
        }
    }
}