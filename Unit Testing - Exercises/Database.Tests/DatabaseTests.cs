namespace Database.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;
        private int[] arrayWithMoreThan16Lenght = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        private int[] arrayWith16Lenght = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private int[] arrayWithLengthBelow16 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        [Test]
        public void Test_ConstructorShouldThrowExceptionIfLenghtIsOver16()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                database = new Database(arrayWithMoreThan16Lenght);
            });
        }

        [Test]
        public void Test_AddShouldIncreceCount()
        {
            database = new Database(1, 2, 3, 4, 5);

            database.Add(6);

            Assert.AreEqual(6, database.Count);
        }

        [Test]
        public void Test_AddShouldThrowExceptionIfLenthIs16()
        {
            database = new Database(arrayWith16Lenght);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(17);
            });
        }

        [Test]
        public void Test_RemoveShouldDecreaceCountEachTime()
        {
            database = new Database(1, 2, 3, 4, 5);

            database.Remove();

            Assert.AreEqual(4, database.Count);
        }

        [Test]
        public void Test_RemoveShouldTrhowExceptionIfDatabaseIsEmpty()
        {
            database = new Database();

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
            });
        }

        [Test]
        public void Test_FetchShouldReturnCopyOfTheArray()
        {
            database = new Database(arrayWithLengthBelow16);
            int[] copy = database.Fetch();

            Assert.AreEqual(arrayWithLengthBelow16, copy);
        }
    }
}
