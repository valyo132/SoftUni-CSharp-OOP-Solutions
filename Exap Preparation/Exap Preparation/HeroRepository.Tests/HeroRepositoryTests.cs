using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private Hero hero;
    private HeroRepository repository;

    [SetUp]
    public void SetUp()
    {
        hero = new Hero("test", 10);
        repository = new HeroRepository();

        repository.Create(hero);
    }

    [Test]
    public void Test_Constructor_ShouldWork()
    {
        IReadOnlyCollection<Hero> expected = new List<Hero> { hero };

        Assert.That(repository.Heroes, Is.EqualTo(expected));
        Assert.That(repository.Heroes.Count, Is.EqualTo(1));
    }

    [Test]
    public void Test_Create_ShouldThrowException_ForAlreadyExistingHero()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            repository.Create(hero);
        });
    }

    [Test]
    public void Test_Create_ShouldThrowException_ForNullParameter()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            repository.Create(null);
        });
    }

    [Test]
    public void Test_Create_ShouldWork()
    {
        var hero2 = new Hero("name", 1);

        string actual = repository.Create(hero2);
        string expected = "Successfully added hero name with level 1";

        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(repository.Heroes.Count, Is.EqualTo(2));
        Assert.That(repository.Heroes.Last(), Is.EqualTo(hero2));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Test_Remove_ShouldThrowException(string name)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            repository.Remove(name);
        });
    }

    [Test]
    public void Test_Remove_ShouldWork()
    {
        bool actual = repository.Remove("test");

        Assert.IsTrue(actual);
        Assert.IsFalse(repository.Heroes.Contains(hero));
    }

    [Test]
    public void Test_GetHeroWithHighestLevel_ShouldWork()
    {
        var hero2 = new Hero("name", 1);
        repository.Create(hero2);

        var topLevel = repository.GetHeroWithHighestLevel();

        Assert.That(hero, Is.EqualTo(topLevel));
    }

    [Test]
    public void Test_GetHero_ShouldWork()
    {
        var actual = repository.GetHero("test");

        Assert.That(actual, Is.EqualTo(hero));
    }

    [Test]
    public void Test_GetHero_ShouldReturnNull()
    {
        var actual = repository.GetHero("invalid");

        Assert.That(actual, Is.EqualTo(null));
    }
}