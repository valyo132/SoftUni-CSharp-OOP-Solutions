using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private Smartphone defaultPhone;
        private Shop shop;

        [SetUp]
        public void SetUp()
        {
            shop = new Shop(3);
            defaultPhone = new Smartphone("Test", 100);

            shop.Add(defaultPhone);
        }

        [Test]
        public void Test_SmartphoneConstructor_ShouldWork()
        {
            Assert.That(defaultPhone.ModelName, Is.EqualTo("Test"));
            Assert.That(defaultPhone.MaximumBatteryCharge, Is.EqualTo(100));
            Assert.That(defaultPhone.CurrentBateryCharge, Is.EqualTo(100));
        }

        [Test]
        public void Test_ShopConstructor_ShouldWork()
        {
            Assert.That(shop.Count, Is.EqualTo(1));
            Assert.That(shop.Capacity, Is.EqualTo(3));
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Test_Capacity_ShouldThrowException_WithNegativeNumber(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                shop = new Shop(capacity);
            });
        }

        [Test]
        public void Test_Add_ShouldThrowEXception_ForExistingPhone()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(defaultPhone);
            });
        }

        [Test]
        public void Test_Add_ShouldThrowException_ForFullCapacity()
        {
            Smartphone second = new Smartphone("IOS", 50);
            Smartphone third = new Smartphone("Android", 50);

            shop.Add(second);
            shop.Add(third);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(new Smartphone("asd", 1));
            });
        }

        [Test]
        public void Test_Remove_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("No");
            });
        }

        [Test]
        public void Test_Remove_ShouldWork()
        {
            shop.Remove("Test");

            Assert.That(shop.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_TestPhone_ShouldThrowException_ForNonExistingPhone()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("No", 69);
            });
        }

        [TestCase(200)]
        public void Test_TestPhone_ShouldThrowException_ForBateryUsage(int battery)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Test", battery);
            });
        }

        [TestCase(50)]
        public void Test_TestPhone_ShouldWork(int battery)
        {
            shop.TestPhone("Test", battery);

            Assert.That(defaultPhone.CurrentBateryCharge, Is.EqualTo(50));
        }

        [Test]
        public void Test_ChargePhone_ShouldThrowException_ForNonExsitingPhone()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("No");
            });
        }

        [Test]
        public void Test_ChargePhone_ShouldWork()
        {
            shop.TestPhone("Test", 50);

            shop.ChargePhone("Test");

            Assert.That(defaultPhone.CurrentBateryCharge, Is.EqualTo(100));
        }
    }
}