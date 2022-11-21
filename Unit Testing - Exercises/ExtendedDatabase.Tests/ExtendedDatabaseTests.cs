namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private static Person[] people;
        private Database database16;
        private Database database17;
        private Database databaseBelow16;

        [SetUp]
        public void Setup()
        {
            people = MakePeople(16);
            database16 = new Database(people);

            Person[] personBelow16 = MakePeople(10);
            databaseBelow16 = new Database(personBelow16);
        }

        [Test]
        public void Test_AddRangeShouldThrowExceptionIfPeopleAreOver16()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                people = MakePeople(17);
                database17 = new Database(people);
            });
        }

        [Test]
        public void Test_AddRangeShouldSetCountProperly()
        {
            databaseBelow16.Add(new Person(1435, "hdtrhd"));
            Assert.That(databaseBelow16.Count, Is.EqualTo(11));
        }

        [Test]
        public void Test_AddSholdIncreaseCountBy1()
        {
            databaseBelow16.Add(new Person(2452, "hshg"));
            Assert.That(databaseBelow16.Count, Is.EqualTo(11));
        }

        [Test]
        public void Test_AddShouldThrowExceptionIfCountIs16()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                database16.Add(new Person(1001, "dntdh"));
            });
        }

        [Test]
        public void Test_AddShouldThrowExceptionIfThereIsTheSameId()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                databaseBelow16.Add(new Person(1, "smhdbjd"));
            });
        }

        [Test]
        public void Test_AddPersonToTheCollection()
        {
            Person p = new Person(100, "John");
            databaseBelow16.Add(p);
            Person exp = databaseBelow16.FindByUsername("John");

            Assert.That(exp, Is.EqualTo(p));
        }

        [Test]
        public void Test_RemoveTheLastPersonShouldWork()
        {
            Person p = new Person(100, "John");
            databaseBelow16.Add(p);
            databaseBelow16.Remove();

            Assert.Throws<InvalidOperationException>(() =>
            {
                Person exp = databaseBelow16.FindByUsername("John");
            });
        }

        [Test]
        public void Test_AddShouldThrowExceptionIfThereIsTheSameUsername()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                databaseBelow16.Add(new Person(142352, "A"));
            });
        }

        [Test]
        public void Test_RemoveShouldThrowExceptionIfCountIs0()
        {
            Database emptyDatabase = new Database();

            Assert.Throws<InvalidOperationException>(() =>
            {
                emptyDatabase.Remove();
            });
        }

        [Test]
        public void Test_RemoveShouldDecreaseCountWith1()
        {
            var exp = databaseBelow16.Count - 1;
            databaseBelow16.Remove();

            Assert.That(exp, Is.EqualTo(databaseBelow16.Count));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_FindByNameShouldThrowExceptionIfParameterIsNull(string username)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                databaseBelow16.FindByUsername(username);
            });
        }

        [Test]
        public void Test_FindByNameShouldReturnTheCorrectPerson()
        {
            Person p = new Person(123, "Dimitrichko");
            databaseBelow16.Add(p);

            Person exp = databaseBelow16.FindByUsername("Dimitrichko");

            Assert.That(exp.UserName, Is.EqualTo("Dimitrichko"));
        }

        [Test]
        public void Test_FindByIdShouldReturnTheCorrectPerson()
        {
            Person p = new Person(123, "Dimitrichko");
            databaseBelow16.Add(p);

            Person exp = databaseBelow16.FindById(123);

            Assert.That(exp.Id, Is.EqualTo(123));
        }

        [Test]
        public void Test_FindByNameShouldThrowExceptionIfUsernameIsNotPresented()
        {
            string name = "Valyo";

            Assert.Throws<InvalidOperationException>(() =>
            {
                databaseBelow16.FindByUsername(name);
            });
        }

        [Test]
        public void Test_FindByIdShouldThrowExceptionIfIdIsNotPresented()
        {
            int id = 1526326;

            Assert.Throws<InvalidOperationException>(() =>
            {
                databaseBelow16.FindById(id);
            });
        }

        [Test]
        public void Test_FindByIdShouldThrowExceptionIfIdIsNegative()
        {
            int id = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                databaseBelow16.FindById(id);
            });
        }

        private static Person[] MakePeople(int peopleCount)
        {
            string username = "a";
            Person[] arr = new Person[peopleCount];

            for (int i = 0; i < peopleCount; i++)
            {
                username = ((char)('A' + i)).ToString();
                arr[i] = new Person(i, username);
            }

            return arr;
        }
    }
}