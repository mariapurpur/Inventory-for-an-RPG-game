using Xunit;
using Moq;
using InventorySystem.Core.Crafting.Recipes;
using InventorySystem.Core.Inventory.Interfaces;
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
        public void RecipeName_ShouldBe_Bow()
        {
            Assert.Equal("лук", _recipe.RecipeName);
        }

        [Fact]
        public void RequiredItems_ShouldContain_WoodAndThread()
        {
            var requiredItems = _recipe.RequiredItems;

            Assert.Equal(2, requiredItems.Count);
            Assert.Equal(3, requiredItems[202]);
            Assert.Equal(2, requiredItems[203]);
        }

        [Fact]
        public void CanCraft_WhenInventoryHasAllItems_ShouldReturnTrue()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(203, 2)).Returns(true);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_WhenMissingWood_ShouldReturnFalse()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_WhenMissingThread_ShouldReturnFalse()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(203, 2)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void Craft_WhenCanCraft_ShouldReturnBow()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.GetItemCount(202)).Returns(6);
            mockInventory.Setup(inv => inv.GetItemCount(203)).Returns(4);

            mockInventory.Setup(inv => inv.RemoveItem(202, 3)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(203, 2)).Returns(true);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.NotNull(result);
            Assert.IsType<Bow>(result);
            Assert.Equal(102, result.Id);
            Assert.Equal("новый лук", result.Name);
        }

        [Fact]
        public void Craft_WhenCannotCraft_ShouldReturnNull()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(202, 3)).Returns(false);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.Null(result);
        }
    }
}