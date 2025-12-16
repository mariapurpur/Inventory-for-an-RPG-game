using Xunit;
using Moq;
using InventorySystem.Core.Crafting.Recipes;
using InventorySystem.Core.Inventory.Interfaces;
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
        public void RecipeName_ShouldBe_Torch()
        {
            Assert.Equal("Факел", _recipe.RecipeName);
        }

        [Fact]
        public void RequiredItems_ShouldContain_CoalAndStick()
        {
            var requiredItems = _recipe.RequiredItems;

            Assert.Equal(2, requiredItems.Count);
            Assert.Equal(1, requiredItems[204]);
            Assert.Equal(1, requiredItems[205]);
        }

        [Fact]
        public void CanCraft_WhenInventoryHasAllItems_ShouldReturnTrue()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.GetItemCount(204)).Returns(1);
            mockInventory.Setup(inv => inv.GetItemCount(205)).Returns(1);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.True(canCraft);
        }

        [Fact]
        public void CanCraft_WhenMissingCoal_ShouldReturnFalse()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void CanCraft_WhenMissingStick_ShouldReturnFalse()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(205, 1)).Returns(false);

            var canCraft = _recipe.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }

        [Fact]
        public void Craft_WhenCanCraft_ShouldReturnTorch()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.GetItemCount(204)).Returns(2);
            mockInventory.Setup(inv => inv.GetItemCount(205)).Returns(2);

            mockInventory.Setup(inv => inv.RemoveItem(204, 1)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(205, 1)).Returns(true);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.NotNull(result);
            Assert.IsType<SimpleItem>(result);
            var torch = result as SimpleItem;
            Assert.Equal(301, torch!.Id);
            Assert.Equal("Факел", torch.Name);
        }

        [Fact]
        public void Craft_WhenCannotCraft_ShouldReturnNull()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(204, 1)).Returns(false);

            var result = _recipe.Craft(mockInventory.Object);

            Assert.Null(result);
        }
    }
}