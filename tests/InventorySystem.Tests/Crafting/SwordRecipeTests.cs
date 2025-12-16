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
        public void RecipeName_ShouldBe_Sword()
        {
            Assert.Equal("меч", _recipe.RecipeName);
        }

        [Fact]
        public void RequiredItems_ShouldContain_IronAndWood()
        {
            var requiredItems = _recipe.RequiredItems;

            Assert.Equal(2, requiredItems.Count);
            Assert.Equal(2, requiredItems[201]);
            Assert.Equal(1, requiredItems[202]);
        }

        [Fact]
        public void CanCraft_WhenInventoryHasAllItems_ShouldReturnTrue()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_WhenMissingIron_ShouldReturnFalse()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_WhenMissingWood_ShouldReturnFalse()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void Craft_WhenCanCraft_ShouldReturnSword()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.GetItemCount(201)).Returns(4);
            mockInventory.Setup(inv => inv.GetItemCount(202)).Returns(2);

            mockInventory.Setup(inv => inv.RemoveItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(202, 1)).Returns(true);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.NotNull(result);
            Assert.IsType<Sword>(result);
            Assert.Equal(101, result.Id);
            Assert.Equal("меч", result.Name);
        }

        [Fact]
        public void Craft_WhenCannotCraft_ShouldReturnNull()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.Null(result);
        }
    }
}