using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private Item item;
        private BankVault bankVault;

        [SetUp]
        public void Setup()
        {
            item = new Item("Valyo", "123");
            bankVault = new BankVault();

            bankVault.AddItem("A1", item);
        }

        [Test]
        public void Test_Constructor_ShouldWork()
        {
            var expected = new Dictionary<string, Item>
            {
                {"A1", item},
                {"A2", null},
                {"A3", null},
                {"A4", null},
                {"B1", null},
                {"B2", null},
                {"B3", null},
                {"B4", null},
                {"C1", null},
                {"C2", null},
                {"C3", null},
                {"C4", null},
            };

            Assert.That(bankVault.VaultCells, Is.EqualTo(expected));
        }

        [Test]
        public void Test_AddItem_ShouldThrowException_ForNotExistingCell()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.AddItem("F8", item);
            });
        }

        [Test]
        public void Test_AddItem_ShouldThrowException_ForAlreadyExistingItemInACell()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.AddItem("A1", new Item("Bary", "154325"));
            });
        }

        [Test]
        public void Test_AddItem_ShouldThrowException_ForAlreadyExistingItem()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                bankVault.AddItem("A2", new Item("Bary", "123"));
            });
        }

        [Test]
        public void Test_AddItem_ShouldWork()
        {
            Item item2 = new Item("Bary", "321");
            bankVault.AddItem("A2", item2);

            Assert.That(bankVault.VaultCells["A2"], Is.EqualTo(item2));
            Assert.That(bankVault.VaultCells["A1"], Is.EqualTo(item));
        }

        [Test]
        public void Test_RemoveItem_ShouldThrowException_ForNotExistingCell()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("F8", item);
            });
        }

        [Test]
        public void Test_RemoveItem_ShouldThrowException_ForNonExistingItemInTheCell()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("A1", new Item("Invalid", "asd"));
            });
        }

        [Test]
        public void Test_RemoveItemShouldWork()
        {
            string actual = bankVault.RemoveItem("A1", item);
            string expected = "Remove item:123 successfully!";

            Assert.That(bankVault.VaultCells["A1"], Is.EqualTo(null));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}