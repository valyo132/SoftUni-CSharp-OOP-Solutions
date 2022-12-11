using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballPlayer player;
        private FootballTeam team;

        [SetUp]
        public void Setup()
        {
            player = new FootballPlayer("Messi", 10, "Forward");
            team = new FootballTeam("Man Unt", 15);

            team.AddNewPlayer(player);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Name_ShouldThroxException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                team = new FootballTeam(name, 16);
            });
        }

        [TestCase(14)]
        [TestCase(1)]
        public void Test_Capacity_ShouldThrow(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                team = new FootballTeam("test", capacity);
            });
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            Assert.That(team.Name, Is.EqualTo("Man Unt"));
            Assert.That(team.Capacity, Is.EqualTo(15));
        }

        [Test]
        public void Test_Playes_SouldWork()
        {
            List<FootballPlayer> expceted = new List<FootballPlayer> { player };

            Assert.That(team.Players, Is.EqualTo(expceted));
        }

        [Test]
        public void Test_Add_ShouldThrowException()
        {
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));
            team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));

            string actual = team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));

            Assert.That(actual, Is.EqualTo("No more positions available!"));
            Assert.That(team.Players.Count, Is.EqualTo(15));
        }

        [Test]
        public void Test_Add_ShouldWork()
        {
            string actual = team.AddNewPlayer(new FootballPlayer("test", 1, "Forward"));

            Assert.That(actual, Is.EqualTo("Added player test in position Forward with number 1"));
        }

        [Test]
        public void Test_PickPlayer_ShouldWork()
        {
            var actual = team.PickPlayer("Messi");

            Assert.That(actual, Is.EqualTo(player));
        }

        [Test]
        public void Test_PickPlayer_ShouldReturnNull()
        {
            var actual = team.PickPlayer("invalid");

            Assert.That(actual, Is.EqualTo(null));
        }

        [Test]
        public void Test_PlayerScore_ShouldWork()
        {
            string actual = team.PlayerScore(10);

            Assert.That(actual, Is.EqualTo("Messi scored and now has 1 for this season!"));
            Assert.That(player.ScoredGoals, Is.EqualTo(1));
        }
    }
}