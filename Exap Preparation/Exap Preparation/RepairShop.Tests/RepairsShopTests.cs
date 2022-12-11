using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            private Car car;
            private Garage garage;

            [SetUp]
            public void SetUp()
            {
                car = new Car("bmw", 2);
                garage = new Garage("test", 2);

                garage.AddCar(car);
            }

            [TestCase(null)]
            [TestCase("")]
            public void Test_NameProperty_ShouldThrowException(string name)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    garage = new Garage(name, 2);
                });
            }

            [TestCase(0)]
            [TestCase(-1)]
            [TestCase(-100)]
            public void Test_MechanicsAbalable_ShouldThrowException(int mechanics)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    garage = new Garage("test", mechanics);
                });
            }

            [Test]
            public void Test_Properties_ShouldWork()
            {
                Assert.That(garage.Name, Is.EqualTo("test"));
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(2));
            }

            [Test]
            public void Test_Count_ShouldWork()
            {
                Assert.That(garage.CarsInGarage, Is.EqualTo(1));
            }

            [Test]
            public void Test_AddCar_ShouldThrowExcption()
            {
                garage.AddCar(new Car("test", 1));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(new Car("invalid", 2));
                });
            }

            [Test]
            public void Test_AddCar_ShouldWokr()
            {
                garage.AddCar(new Car("test", 1));

                Assert.That(garage.CarsInGarage, Is.EqualTo(2));
            }

            [Test]
            public void Test_FixCar_ShouldThrowException()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("invalid");
                });
            }

            [Test]
            public void Test_FixCar_ShouldWork()
            {
                var fixedCar = garage.FixCar("bmw");

                Assert.That(car.NumberOfIssues, Is.EqualTo(0));
                Assert.That(fixedCar, Is.EqualTo(car));
            }

            [Test]
            public void Test_RemoveFixedCars_ShouldThrowException()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                });
            }

            [Test]
            public void Test_RemoveFixedCars_ShouldWork()
            {
                var newCar = new Car("test", 1);
                garage.AddCar(newCar);
                garage.FixCar("test");

                int actual = garage.RemoveFixedCar();

                Assert.That(actual, Is.EqualTo(1));
                Assert.That(garage.CarsInGarage, Is.EqualTo(1));
            }

            [Test]
            public void Test_Report_ShouldWork()
            {
                var newCar = new Car("test", 1);
                garage.AddCar(newCar);

                string actual = garage.Report();
                string expcected = "There are 2 which are not fixed: bmw, test.";

                Assert.That(actual, Is.EqualTo(expcected));
            }
        }
    }
}