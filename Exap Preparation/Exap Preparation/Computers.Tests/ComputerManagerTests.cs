using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer pc;
        private ComputerManager manager;

        [SetUp]
        public void Setup()
        {
            pc = new Computer("Test", "pc", 1000);
            manager = new ComputerManager();

            manager.AddComputer(pc);
        }

        [Test]
        public void Test_ComputerConstructor_ShouldWork()
        {
            Assert.That(pc.Manufacturer, Is.EqualTo("Test"));
            Assert.That(pc.Model, Is.EqualTo("pc"));
            Assert.That(pc.Price, Is.EqualTo(1000));
        }

        [Test]
        public void Test_ValidateNullValue_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.AddComputer(null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetComputer(null, "pc");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetComputer("Test", null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetComputersByManufacturer(null);
            });
        }

        [Test]
        public void Test_AddComputer_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                manager.AddComputer(pc);
            });
        }

        [Test]
        public void Test_AddComputer_ShouldWork()
        {
            Assert.That(manager.Count, Is.EqualTo(1));
            Assert.That(manager.Computers.Count, Is.EqualTo(1));
            Assert.That(manager.Computers.First, Is.EqualTo(pc));
        }

        [Test]
        public void Test_RemoveComputer_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var actual = manager.RemoveComputer("Test", "nonExisting");
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var actual = manager.RemoveComputer("Invalid", "pc");
            });
        }

        [Test]
        public void Test_RemoveComputer_ShouldWork()
        {
            var comp = manager.RemoveComputer("Test", "pc");

            Assert.That(comp, Is.EqualTo(pc));
            Assert.That(manager.Count, Is.EqualTo(0));
            Assert.That(manager.Computers.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_GetComputersByManufacturer_ShouldZeroComputers()
        {
            var actual = manager.GetComputersByManufacturer("Invalid");

            Assert.IsEmpty(actual);
        }

        [Test]
        public void Test_GetComputersByManufacturer_ShouldReturnWorks()
        {
            Computer second = new Computer("Test", "test", 1522);
            manager.AddComputer(second);

            var actual = manager.GetComputersByManufacturer("Test").ToList();

            var expected = new List<Computer>();
            expected.Add(pc);
            expected.Add(second);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_GetComputer_ThrowsForNonExistingComputer()
        {
            Assert.Throws<ArgumentException>(
                () => manager.GetComputer("Missing", "Invalid"));
        }

        [Test]
        public void Test_GetComputer_ShouldWork()
        {
            var expected = manager.GetComputer("Test", "pc");

            Assert.AreEqual(pc, expected);
        }
    }
}