using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace Gyms.Tests
{
    public class GymsTests
    {
        private Athlete athlete;
        private Gym gym;

        [SetUp]
        public void SetUp()
        {
            athlete = new Athlete("test");
            gym = new Gym("Jungle", 2);

            gym.AddAthlete(athlete);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_NameProperty_ShouldThrowException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                gym = new Gym(name, 2);
            });
        }

        [Test]
        public void Test_NameProperty_ShouldWork()
        {
            gym = new Gym("valid", 2);

            Assert.That(gym.Name, Is.EqualTo("valid"));
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Test_CapacityProperty_ShouldThrowException(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                gym = new Gym("valid", capacity);
            });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void Test_CapacityProperty_ShouldWork(int capacity)
        {
            gym = new Gym("valid", capacity);

            Assert.That(gym.Capacity, Is.EqualTo(capacity));
        }

        [Test]
        public void Test_CountPorperty_ShouldWork()
        {
            Assert.That(gym.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_Add_ShouldThrowException()
        {
            gym.AddAthlete(new Athlete("Misho"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(new Athlete("Pesho"));
            });
        }

        [Test]
        public void Test_Add_ShouldWork()
        {
            gym.AddAthlete(new Athlete("Misho"));

            Assert.That(gym.Count, Is.EqualTo(2));
        }

        [TestCase("invalid")]
        [TestCase(null)]
        public void Test_Remove_ShouldThrowException(string name)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete(name);
            });
        }

        [Test]
        public void Test_Remome_ShouldWork()
        {
            gym.RemoveAthlete("test");

            Assert.That(gym.Count, Is.EqualTo(0));
        }

        [TestCase("invalid")]
        [TestCase(null)]
        public void Test_InjureAthlete_ShouldThrowException(string name)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete(name);
            });
        }

        [Test]
        public void Test_InjureAthlete_ShouldWork()
        {
            var actual = gym.InjureAthlete("test");

            Assert.That(actual, Is.EqualTo(athlete));
            Assert.IsTrue(athlete.IsInjured);
        }

        [Test]
        public void Test_Report_ShouldWokr()
        {
            string actual = gym.Report();
            string expected = "Active athletes at Jungle: test";

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
