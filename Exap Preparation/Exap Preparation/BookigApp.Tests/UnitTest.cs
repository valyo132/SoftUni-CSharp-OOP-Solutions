using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Room room;
        private Booking booking;
        private Hotel hotel;

        [SetUp]
        public void Setup()
        {
            room = new Room(3, 100);
            booking = new Booking(1, room, 3);
            hotel = new Hotel("test", 5);

            hotel.AddRoom(room);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Test_NameShouldThrowException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                hotel = new Hotel(name, 4);
            });
        }

        [TestCase(0)]
        [TestCase(6)]
        public void Test_Category_ShouldThrowException(int num)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel = new Hotel("asd", num);
            });
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            Assert.That(hotel.FullName, Is.EqualTo("test"));
            Assert.That(hotel.Category, Is.EqualTo(5));
            Assert.That(hotel.Turnover, Is.EqualTo(0));
            Assert.That(hotel.Rooms.Count, Is.EqualTo(1));
            Assert.That(hotel.Bookings.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Add_ShouldWork()
        {
            Assert.That(hotel.Rooms.Count, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_BookRoom_ShouldThrowException_ForInvalidAdults(int adults)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, 1, 4, 30);
            });
        }

        [TestCase(-1)]
        public void Test_BookRoom_ShouldThrowException_ForInvalidKids(int kids)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(2, kids, 4, 30);
            });
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_BookRoom_ShouldThrowException_ForInvalidDuration(int duration)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(2, 1, duration, 30);
            });
        }

        [Test]
        public void Test_BookRoom_ShouldWork()
        {
            hotel.BookRoom(2, 1, 3, 1000);

            Assert.That(hotel.Bookings.Count, Is.EqualTo(1));
            Assert.That(hotel.Turnover, Is.EqualTo(300));
        }
    }
}