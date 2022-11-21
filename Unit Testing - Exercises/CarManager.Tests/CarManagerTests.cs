namespace CarManager.Tests
{
    using System;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]
        public void SetUp()
        {
            car = new Car("Merc", "Benz", 5, 40);
        }

        [Test]
        public void Test_ConstructorShouldSetTheValuesPropery()
        {
            Car car = new Car("Bmw", "A3", 2.5, 40);

            Assert.That(car.Make, Is.EqualTo("Bmw"));
            Assert.That(car.Model, Is.EqualTo("A3"));
            Assert.That(car.FuelConsumption, Is.EqualTo(2.5));
            Assert.That(car.FuelCapacity, Is.EqualTo(40));
            Assert.That(car.FuelAmount, Is.EqualTo(0));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_MakePropertySohuldThrowExceptionIfValueIsNullOrEmpty(string make)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(make, "A3", 2.5, 40);
            });
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_ModelPropertySohuldThrowExceptionIfValueIsNullOrEmpty(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Bmw", model, 2.5, 40);
            });
        }

        [Test]
        public void Test_FuelConsumptionPropertyShouldThrowExceptionIfValueIsZeroOrNegative()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Bmw", "A3", 0, 40);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Bmw", "A3", -1, 40);
            });
        }

        [Test]
        public void Test_FuelCapacityPropertyShouldThrowExceptionIfValueIsZeroOrNegative()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Bmw", "A3", 2.5, 0);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Bmw", "A3", 2.5, -1);
            });
        }

        [Test]
        public void Test_RefuelShouldThrowExceptionIfParameterIsZeroOrNegativte()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(0);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(-1);
            });
        }

        [Test]
        public void Test_RefuelShouldIncreaseFuelAmont()
        {
            car.Refuel(10);

            Assert.That(car.FuelAmount, Is.EqualTo(10));
        }

        [Test]
        public void Test_RefuelFuelAmontShouldBeEqualToFuelCapacityIfItIsBigger()
        {
            car.Refuel(100);

            Assert.That(car.FuelAmount, Is.EqualTo(car.FuelCapacity));
        }

        [Test]
        public void Test_DriveShouldThrowExceptionIfFuelNeededIsBiggerFuelAmonth()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(10);
            });
        }

        [Test]
        public void Test_DriveShouldDecreaseFuelAmonthByFuelNeeded()
        {
            car.Refuel(40);
            car.Drive(500);

            Assert.That(car.FuelAmount, Is.EqualTo(15));
        }
    }
}