namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Xml.Linq;

    public class AquariumsTests
    {
        private Fish fish;
        private Aquarium aqua;

        [SetUp]
        public void SetUp()
        {
            fish = new Fish("fish");
            aqua = new Aquarium("Aqua", 5);

            aqua.Add(fish);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Name_ShouldThrowException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                aqua = new Aquarium(name, 5);
            });
        }

        [TestCase("Name")]
        public void Test_Name_ShouldWork(string name)
        {
            aqua = new Aquarium(name, 5);

            Assert.That(aqua.Name, Is.EqualTo(name));
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Test_Capacity_ShouldThrowException(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                aqua = new Aquarium("name", capacity);
            });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void Test_Capacity_ShouldWork(int capacity)
        {
            aqua = new Aquarium("name", capacity);

            Assert.That(aqua.Capacity, Is.EqualTo(capacity));
        }

        [Test]
        public void Test_Count_ShouldWork()
        {
            Assert.That(aqua.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_Add_ShouldThrowException()
        {
            aqua = new Aquarium("name", 1);
            aqua.Add(fish);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aqua.Add(new Fish("name"));
            });
        }

        [Test]
        public void Test_Add_ShouldWork()
        {
            Assert.That(aqua.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_RemoveFish_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                aqua.RemoveFish("Invalid name");
            });
        }

        [Test]
        public void Test_RemoveFish_ShouldWork()
        {
            aqua.RemoveFish("fish");

            Assert.That(aqua.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_SellFish_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                aqua.SellFish("Invalid name");
            });
        }

        [Test]
        public void Test_SellFish_ShouldWork()
        {
            var selled = aqua.SellFish("fish");

            Assert.That(selled, Is.EqualTo(fish));
            Assert.That(selled.Available, Is.EqualTo(false));
        }

        [Test]
        public void Test_Report_ShouldWork()
        {
            var fish2 = new Fish("fish2");
            aqua.Add(fish2);

            string actual = aqua.Report();
            string expected = "Fish available at Aqua: fish, fish2";

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
