using Xunit;
using Moq;
using InventorySystem.Core.Crafting.Recipes;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Tests.Crafting
{
    public class TorchRecipeTests
    {
        private readonly TorchRecipe _recipe;

        public TorchRecipeTests()
        {
            _recipe = new TorchRecipe();
        }

        [Fact]
        public void RecipeName_Torch()
        {
            Assert.Equal("Факел", _recipe.RecipeName);
        }

        [Fact]
        public void RequiredItems_ContainsCoalAndStick()
        {
            var requiredItems = _recipe.RequiredItems;

            Assert.Equal(2, requiredItems.Count);
            Assert.Equal(1, requiredItems[204]);
            Assert.Equal(1, requiredItems[205]);
        }

        [Fact]
        public void CanCraft_HasAllItems_True()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(205, 1)).Returns(true);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.True(canCraft);
        }

        [Fact]
        public void CanCraft_NoCoal_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_NoStick_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(205, 1)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_ReturnTorch()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(205, 1)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(204, 1)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(205, 1)).Returns(true);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.NotNull(result);
            Assert.IsType<SimpleItem>(result);
            var torch = result as SimpleItem;
            Assert.NotNull(torch);
            Assert.Equal(301, torch.Id);
            Assert.Equal("Факел", torch.Name);
            Assert.Equal(16, torch.MaxStackSize);
            Assert.Equal(ItemRarity.Common, torch.Rarity);
        }

        [Fact]
        public void NoCraft_ReturnNull()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(false);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.Null(result);
        }

        [Fact]
        public void SimpleItem_CanStackItem_True()
        {
            var item1 = new SimpleItem(301, "Факел", 16);
            var item2 = new SimpleItem(301, "Факел", 16);
            item2.CurrentStack = 1;

            var canStack = item1.CanStackWith(item2);

            Assert.True(canStack);
        }

        [Fact]
        public void SimpleItem_CanStackDiffItem_False()
        {
            var item1 = new SimpleItem(301, "Факел", 16);
            var item2 = new SimpleItem(302, "Другой", 16);

            var canStack = item1.CanStackWith(item2);

            Assert.False(canStack);
        }

        [Fact]
        public void SimpleItem_CanStackFull_False()
        {
            var item1 = new SimpleItem(301, "Факел", 16);
            var item2 = new SimpleItem(301, "Факел", 16);
            item2.CurrentStack = 16;

            var canStack = item1.CanStackWith(item2);

            Assert.False(canStack);
        }

        [Fact]
        public void SimpleItem_Clone_NewInstance()
        {
            var original = new SimpleItem(301, "Факел", 16)
            {
                CurrentStack = 5
            };

            var clone = original.Clone() as SimpleItem;

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