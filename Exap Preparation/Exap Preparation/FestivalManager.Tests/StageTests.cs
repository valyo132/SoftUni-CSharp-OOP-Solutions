// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using System;
    using System.Linq;

    using FestivalManager.Entities;
    using NUnit.Framework;

    [TestFixture]
    public class StageTests
    {
        private Song song;
        private Performer singer;
        private Stage stage;

        [SetUp]
        public void SetUp()
        {
            stage = new Stage();
            song = new Song("song1", new TimeSpan(0, 3, 10));
            singer = new Performer("Valyo", "markov", 18);

            singer.SongList.Add(song);
            stage.AddPerformer(singer);
            stage.AddSong(song);
        }

        [Test]
        public void Test_Constructor_ShouldSetTheListsProperly()
        {
            Assert.That(stage.Performers.Count, Is.EqualTo(1));
            Assert.That(stage.Performers.First(), Is.EqualTo(singer));
        }

        [Test]
        public void Test_ValidateNullValue_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                stage.AddPerformer(null);
            });
        }

        [TestCase(17)]
        [TestCase(1)]
        [TestCase(-1)]
        public void Test_AddPerformer_ShouldThrowException(int age)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                stage.AddPerformer(new Performer("asd", "ASD", age));
            });
        }

        [TestCase(18)]
        [TestCase(100)]
        public void Test_AddPerformer_ShouldWork(int age)
        {
            var newSinger = new Performer("asd", "ASD", age);

            stage.AddPerformer(newSinger);

            Assert.That(stage.Performers.Last(), Is.EqualTo(newSinger));
            Assert.That(stage.Performers.Count, Is.EqualTo(2));
        }

        [Test]
        public void Test_AddSong_ShouldThrowException_ForTooShortSong()
        {
            var newSong = new Song("shortOne", new TimeSpan(0, 0, 59));

            Assert.Throws<ArgumentException>(() =>
            {
                stage.AddSong(newSong);
            });
        }

        [Test]
        public void Test_AddSong_ShouldWork()
        {
            var newSong = new Song("shortOne", new TimeSpan(0, 1, 0));

            stage.AddSong(newSong);

            stage.AddSongToPerformer("shortOne", "Valyo markov");
            Assert.That(singer.SongList.Contains(newSong));
        }

        [Test]
        public void Test_AddSongToPerformer_ShouldWork()
        {
            var newSong = new Song("shortOne", new TimeSpan(0, 1, 0));

            stage.AddSong(newSong);

            string actual = stage.AddSongToPerformer("shortOne", "Valyo markov");
            string expected = $"{newSong} will be performed by {singer}";

            Assert.That(singer.SongList.Contains(newSong));
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Play_ShouldWork()
        {
            var newSong = new Song("shortOne", new TimeSpan(0, 1, 59));
            singer.SongList.Add(newSong);

            string actual = stage.Play();
            string expected = "1 performers played 2 songs";

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_GetPerformer_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                stage.AddSongToPerformer("shortOne", "Invalid");
            });
        }

        [Test]
        public void Test_GetPerSong_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                stage.AddSongToPerformer("Invalid", "Valyo markov");
            });
        }
    }
}