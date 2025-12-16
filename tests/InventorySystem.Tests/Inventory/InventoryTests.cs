using Xunit;
using Moq;
using InventorySystem.Core.Inventory;
using InventorySystem.Core.Items.Interfaces;
using System;
using System.Linq;

namespace InventorySystem.Tests.Inventory
{
    public class InventoryTests
    {
        [Fact]
        public void Constructor_YesCapacity()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);

            Assert.Equal(10, inventory.Capacity);
            Assert.Equal(0, inventory.UsedSlots);
            Assert.Equal(0, inventory.TotalItems);
        }

        [Fact]
        public void Constructor_NoCapacity()
        {
            Assert.Throws<ArgumentException>(() => new InventorySystem.Core.Inventory.Inventory(0));
        }

        [Fact]
        public void AddItem_ToEmptyInventory()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            var result = inventory.AddItem(mockItem.Object, 5);

            Assert.True(result);
            Assert.Equal(1, inventory.UsedSlots);
            Assert.Equal(5, inventory.TotalItems);
        }

        [Fact]
        public void RemoveItem_ExistingItem()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 10);
            var result = inventory.RemoveItem(101, 5);

            Assert.True(result);
            Assert.Equal(5, inventory.TotalItems);
        }

        [Fact]
        public void GetItemCount_ExistingItem()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 7);

            var count = inventory.GetItemCount(101);

            Assert.Equal(7, count);
        }

        [Fact]
        public void HasItem_EnoughItems()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 5);

            var hasItem = inventory.HasItem(101, 3);

            Assert.True(hasItem);
        }

        [Fact]
        public void HasSpaceFor_EnoughSpace()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            var hasSpace = inventory.HasSpaceFor(mockItem.Object, 5);

            Assert.True(hasSpace);
        }

        [Fact]
        public void ClearSlot_RemovesItem()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 5);
            inventory.ClearSlot(0);

            Assert.Equal(0, inventory.UsedSlots);
            Assert.Equal(0, inventory.TotalItems);
        }

        [Fact]
        public void GetAllSlots_ClonedSlots()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 5);

            var slots = inventory.GetAllSlots().ToList();

            Assert.NotEmpty(slots);
        }

        [Fact]
        public void GetAllItems_ClonedItems()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 3);

            var items = inventory.GetAllItems().ToList();

            Assert.Single(items);
            Assert.NotSame(mockItem.Object, items.First());
        }

        [Fact]
        public void GetItem_WithValidIndex_ClonedItem()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(101);
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            inventory.AddItem(mockItem.Object, 5);

            var item = inventory.GetItem(0);

            Assert.NotNull(item);
            Assert.NotSame(mockItem.Object, item);
        }

        [Fact]
        public void GetItem_WithInvalidIndex()
        {
            var inventory = new InventorySystem.Core.Inventory.Inventory(10);

            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.GetItem(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.GetItem(10));
        }
    }
}