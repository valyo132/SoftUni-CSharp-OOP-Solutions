namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        private Book book;

        [SetUp]
        public void SetUp()
        {
            book = new Book("Test", "Valyo");
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            Assert.That(book.BookName, Is.EqualTo("Test"));
            Assert.That(book.Author, Is.EqualTo("Valyo"));
            Assert.That(book.FootnoteCount, Is.EqualTo(0));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_BookName_ShouldThrowExceptom(string bookName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                book = new Book(bookName, "Valyo");
            });
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Author_ShouldThrowExceptom(string author)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                book = new Book("Test", author);
            });
        }

        [Test]
        public void Test_AddFootNote_ShouldThrowException()
        {
            book.AddFootnote(1, "asd");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(1, "asd");
            });
        }

        [Test]
        public void Test_AddFootNote_ShouldWork()
        {
            book.AddFootnote(1, "asd");

            Assert.That(book.FootnoteCount, Is.EqualTo(1));
        }

        [Test]
        public void Test_FindFootNote_ShouldThrowException()
        {
            book.AddFootnote(1, "asd");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(62366757);
            });
        }

        [Test]
        public void Test_FindFootNote_ShouldWork()
        {
            book.AddFootnote(1, "asd");

            string actual = book.FindFootnote(1);
            string expected = "Footnote #1: asd";

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_AlterFoodnote_ShouldThrowException()
        {
            book.AddFootnote(1, "asd");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AlterFootnote(62366757, "new");
            });
        }

        [Test]
        public void Test_AlterFoodnote_ShouldWork()
        {
            book.AddFootnote(1, "asd");

            book.AlterFootnote(1, "new");

            string actual = book.FindFootnote(1);
            string expected = "Footnote #1: new";

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}