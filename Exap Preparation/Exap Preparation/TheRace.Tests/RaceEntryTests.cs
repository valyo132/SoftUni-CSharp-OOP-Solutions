using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private UnitDriver driver;
        private UnitCar car;
        private RaceEntry race;


        [SetUp]
        public void Setup()
        {
            car = new UnitCar("Peugeout", 115, 1.6);
            driver = new UnitDriver("Yanko", car);

            race = new RaceEntry();

            race.AddDriver(driver);
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            Assert.That(race.Counter, Is.EqualTo(1));
        }

        [Test]
        public void Test_AddShoulThrowException_ForNullDriver()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                race.AddDriver(null);
            });
        }

        [Test]
        public void Test_AddShoulThrowException_ForExistingDriver()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                race.AddDriver(driver);
            });
        }

        [Test]
        public void Test_AddShouldWork()
        {
            var newCar = new UnitCar("Test", 1000, 5);
            var newDriver = new UnitDriver("Valyo", newCar);

            string actual = race.AddDriver(newDriver);
            string expected = string.Format("Driver {0} added in race.", newDriver.Name);

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(race.Counter, Is.EqualTo(2));
        }

        [Test]
        public void Test_CalculateAvarageHorsePower_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                race.CalculateAverageHorsePower();
            });
        }

        [Test]
        public void Test_CalculateHorsePower_ShouldWork()
        {
            var newCar = new UnitCar("Test", 1000, 5);
            var newDriver = new UnitDriver("Valyo", newCar);

            race.AddDriver(newDriver);

            double actual = race.CalculateAverageHorsePower();
            double expected = 557.5;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}