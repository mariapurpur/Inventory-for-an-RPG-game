using Xunit;
using Moq;
using InventorySystem.Core.Crafting.Recipes;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Weapon;
using System.Collections.Generic;

namespace InventorySystem.Tests.Crafting
{
    public class SwordRecipeTests
    {
        private readonly SwordRecipe _recipe;

        public SwordRecipeTests()
        {
            _recipe = new SwordRecipe();
        }

        [Fact]
        public void RecipeName_Sword()
        {
            Assert.Equal("меч", _recipe.RecipeName);
        }

        [Fact]
        public void RequiredItems_IronAndWood()
        {
            var requiredItems = _recipe.RequiredItems;

            Assert.Equal(2, requiredItems.Count);
            Assert.Equal(2, requiredItems[201]);
            Assert.Equal(1, requiredItems[202]);
        }

        [Fact]
        public void CanCraft_AllItems_True()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.True(canCraft);
        }

        [Fact]
        public void CanCraft_MissingIron_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_MissingWood_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void Craft_CanCraft_Sword()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(202, 1)).Returns(true);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.NotNull(result);
            Assert.IsType<Sword>(result);
            Assert.Equal(101, result.Id);
            Assert.Equal("крутой меч", result.Name);
        }

        [Fact]
        public void Craft_NoCraft_Null()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.Null(result);
        }
    }
}