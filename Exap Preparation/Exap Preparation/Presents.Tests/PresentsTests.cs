namespace Presents.Tests
{
    using Presents;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using System.Collections.Generic;
    using System;

    [TestFixture]
    public class PresentsTests
    {
        private Present present;
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
            present = new Present("test", 5);

            bag.Create(present);
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            Assert.That(bag.GetPresents(), Is.EqualTo(new List<Present>() { present }));
        }

        [Test]
        public void Test_Create_ShouldThrowException_ForNullPresent()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                bag.Create(null);
            });
        }

        [Test]
        public void Test_Create_ShouldThrowException_ForAlreadyExistingPresent()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                bag.Create(present);
            });
        }

        [Test]
        public void Test_Create_ShouldWorkt()
        {
            Present newPresent = new Present("new", 1);

            string actual = bag.Create(newPresent);
            string expected = "Successfully added present new.";

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Remove_ShouldWork()
        {
            bool actual = bag.Remove(present);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Test_GetPresentWithLeastMagic_SgouldWork()
        {
            Present newPresent = new Present("new", 1);
            bag.Create(newPresent);

            var actual = bag.GetPresentWithLeastMagic();

            Assert.That(actual, Is.EqualTo(newPresent));
        }

        [Test]
        public void Test_GetPresent_ShouldWork()
        {
            Present newPresent = new Present("new", 1);
            bag.Create(newPresent);

            var actual = bag.GetPresent("new");
            Assert.That(actual, Is.EqualTo(newPresent));
        }
    }
}
