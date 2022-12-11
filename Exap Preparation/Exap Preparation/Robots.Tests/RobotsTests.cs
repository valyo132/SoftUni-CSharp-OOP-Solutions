namespace Robots.Tests
{
    using System;

    using NUnit.Framework;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager manager;

        [SetUp]
        public void SetUp()
        {
            robot = new Robot("test", 100);
            manager = new RobotManager(2);

            manager.Add(robot);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Test_ConstructorCapacity_ShouldThrowException(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                manager = new RobotManager(capacity);
            });
        }

        [Test]
        public void Test_Capacity_ShouldWork()
        {
            Assert.That(manager.Capacity, Is.EqualTo(2));
        }

        [Test]
        public void Test_Count_ShouldWork()
        {
            Assert.That(manager.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_Add_ShouldThrowException_ForAlreadyExistingRobot()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Add(robot);
            });
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            Assert.That(robot.Battery, Is.EqualTo(100));
            Assert.That(robot.MaximumBattery, Is.EqualTo(100));
            Assert.That(robot.Name, Is.EqualTo("test"));
        }

        [Test]
        public void Test_Add_ShouldThrowException_ForNotEnoughCapacity()
        {
            manager.Add(new Robot("invalid", 10));
            Assert.That(manager.Count, Is.EqualTo(2));

            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Add(new Robot("invalid2", 10));
            });
        }

        [Test]
        public void Test_Add_ShouldWork()
        {
            manager.Add(new Robot("invalid", 10));

            Assert.That(manager.Count, Is.EqualTo(2));
        }

        [Test]
        public void Test_Remove_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Remove("invalid");
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Remove(null);
            });
        }

        [Test]
        public void Test_Remove_ShouldWork()
        {
            manager.Remove("test");
            Assert.That(manager.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Work_ShouldThrowException_ForNonExistingRobot()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Work("invalid", "job", 20);
            });
        }

        [Test]
        public void Test_Work_ShouldThrowException_ForNonLowBattery()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Work("test", "job", 200);
            });
        }

        [Test]
        public void Test_Work_ShouldWork()
        {
            manager.Work("test", "job", 20);

            Assert.That(robot.Battery, Is.EqualTo(80));
            Assert.That(manager.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_Charge_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Charge("invalid");
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Charge(null);
            });
        }

        [Test]
        public void Test_Charge_ShouldWotk()
        {
            manager.Work("test", "job", 40);
            Assert.That(robot.Battery, Is.EqualTo(60));

            manager.Charge("test");
            Assert.That(robot.Battery, Is.EqualTo(robot.MaximumBattery));
        }
    }
}
