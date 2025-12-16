using Xunit;
using Moq;
using InventorySystem.Core.Inventory;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Tests.Inventory
{
    public class InventorySlotTests
    {
        [Fact]
        public void Constructor_Initial()
        {
            var slot = new InventorySlot(5);

            Assert.Equal(5, slot.Index);
            Assert.Null(slot.Item);
            Assert.Equal(0, slot.Count);
            Assert.True(slot.IsEmpty);
        }

        [Fact]
        public void CanAddItem_EmptySlot_True()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            var result = slot.CanAddItem(mockItem.Object, 10);

            Assert.True(result);
        }

        [Fact]
        public void CanAddItem_FullSlot_False()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(10);

            slot.AddItem(mockItem.Object, 10);

            var result = slot.CanAddItem(mockItem.Object, 5);

            Assert.False(result);
        }

        [Fact]
        public void AddItem_ToEmptySlot()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            var result = slot.AddItem(mockItem.Object, 5);

            Assert.True(result);
            Assert.NotNull(slot.Item);
            Assert.Equal(5, slot.Count);
            Assert.False(slot.IsEmpty);
        }

        [Fact]
        public void AddItem_ToStackableSlot()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);
            mockItem.Setup(i => i.CanStackWith(It.IsAny<IItem>())).Returns(true);

            slot.AddItem(mockItem.Object, 10);
            var result = slot.AddItem(mockItem.Object, 5);

            Assert.True(result);
            Assert.Equal(15, slot.Count);
        }

        [Fact]
        public void RemoveItem_FromSlot()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            slot.AddItem(mockItem.Object, 10);
            var result = slot.RemoveItem(3);

            Assert.True(result);
            Assert.Equal(7, slot.Count);
        }

        [Fact]
        public void RemoveItem()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            slot.AddItem(mockItem.Object, 5);
            var result = slot.RemoveItem(5);

            Assert.True(result);
            Assert.Null(slot.Item);
            Assert.Equal(0, slot.Count);
            Assert.True(slot.IsEmpty);
        }

        [Fact]
        public void Clear_ResetsSlot()
        {
            var slot = new InventorySlot(0);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            slot.AddItem(mockItem.Object, 5);
            slot.Clear();

            Assert.Null(slot.Item);
            Assert.Equal(0, slot.Count);
            Assert.True(slot.IsEmpty);
        }

        [Fact]
        public void Clone_CopiesSlotData()
        {
            var original = new InventorySlot(1);
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.MaxStackSize).Returns(64);

            original.AddItem(mockItem.Object, 5);

            var clone = original.Clone();

            Assert.Equal(original.Index, clone.Index);
            Assert.Equal(original.Count, clone.Count);
            Assert.NotNull(clone.Item);
            Assert.NotSame(original.Item, clone.Item);
        }
    }
}