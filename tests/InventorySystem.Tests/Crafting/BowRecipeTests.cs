using Xunit;
using Moq;
using InventorySystem.Core.Crafting.Recipes;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Items.Weapon;
using System.Collections.Generic;

namespace InventorySystem.Tests.Crafting
{
    public class BowRecipeTests
    {
        private readonly BowRecipe _recipe;

        public BowRecipeTests()
        {
            _recipe = new BowRecipe();
        }

        [Fact]
        public void RecipeName_Bow()
        {
            Assert.Equal("лук", _recipe.RecipeName);
        }

        [Fact]
        public void RequiredItems_WoodAndThread()
        {
            var requiredItems = _recipe.RequiredItems;

            Assert.Equal(2, requiredItems.Count);
            Assert.Equal(3, requiredItems[202]);
            Assert.Equal(2, requiredItems[203]);
        }

        [Fact]
        public void CanCraft_AllItems_True()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(203, 2)).Returns(true);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.True(canCraft);
        }

        [Fact]
        public void CanCraft_MissingWood_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_MissingThread_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(203, 2)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void Craft_CanCraft_Bow()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(203, 2)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(203, 2)).Returns(true);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.NotNull(result);
            Assert.IsType<Bow>(result);
            Assert.Equal(102, result.Id);
            Assert.Equal("новый лук", result.Name);
        }

        [Fact]
        public void Craft_NoCraft_Null()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(false);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.Null(result);
        }
    }
}