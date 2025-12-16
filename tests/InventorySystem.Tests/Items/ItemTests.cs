using Xunit;
using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Tests.Items
{
    public class ItemTests
    {
        private class TestItem : ItemBase
        {
            public TestItem(int id, string name, int maxStackSize = 64, ItemRarity rarity = ItemRarity.Common)
                : base(id, name, maxStackSize, rarity)
            {
            }

            public override IItem Clone()
            {
                return new TestItem(this.Id, this.Name, this.MaxStackSize, this.Rarity)
                {
                    CurrentStack = this.CurrentStack
                };
            }
        }

        [Fact]
        public void ItemBase_HasCorrectProperties()
        {
            var item = new TestItem(101, "тестовый предмет", 32, ItemRarity.Rare);

            Assert.Equal(101, item.Id);
            Assert.Equal("тестовый предмет", item.Name);
            Assert.Equal(32, item.MaxStackSize);
            Assert.Equal(1, item.CurrentStack);
            Assert.Equal(ItemRarity.Rare, item.Rarity);
        }

        [Fact]
        public void CanStackWith_SimilarItem_ReturnsTrue()
        {
            var item1 = new TestItem(101, "предмет");
            var item2 = new TestItem(101, "предмет");

            var result = item1.CanStackWith(item2);

            Assert.True(result);
        }

        [Fact]
        public void CanStackWith_DifferentId_ReturnsFalse()
        {
            var item1 = new TestItem(101, "предмет");
            var item2 = new TestItem(102, "предмет");

            var result = item1.CanStackWith(item2);

            Assert.False(result);
        }

        [Fact]
        public void CanStackWith_NullOther_ReturnsFalse()
        {
            var item = new TestItem(101, "предмет");

            var result = item.CanStackWith(null);

            Assert.False(result);
        }

        [Fact]
        public void CanStackWith_FullStack_ReturnsFalse()
        {
            var item1 = new TestItem(101, "предмет");
            var item2 = new TestItem(101, "предмет");
            item2.CurrentStack = item2.MaxStackSize;

            var result = item1.CanStackWith(item2);

            Assert.False(result);
        }

        [Fact]
        public void Clone_CreatesNewInstance()
        {
            var original = new TestItem(101, "предмет", 64, ItemRarity.Legendary);
            original.CurrentStack = 5;

            var clone = original.Clone();

            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
            Assert.Equal(original.Id, clone.Id);
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.MaxStackSize, clone.MaxStackSize);
            Assert.Equal(original.CurrentStack, clone.CurrentStack);
            Assert.Equal(original.Rarity, clone.Rarity);
        }
    }
}