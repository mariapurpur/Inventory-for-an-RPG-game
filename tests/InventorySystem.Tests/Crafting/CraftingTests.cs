using Xunit;
using Moq;
using InventorySystem.Core.Crafting;
using InventorySystem.Core.Crafting.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Tests.Crafting
{
    public class CraftingTests
    {
        private readonly CraftingSystem _craftingSystem;

        public CraftingTests()
        {
            _craftingSystem = new CraftingSystem();
        }

        [Fact]
        public void AddRecipe_AddOne()
        {
            var recipe = new Mock<ICraftingRecipe>();
            recipe.Setup(r => r.RecipeName).Returns("test_recipe");

            _craftingSystem.AddRecipe(recipe.Object);

            var recipes = _craftingSystem.GetAllRecipes();
            Assert.Single(recipes);
            Assert.Contains(recipe.Object, recipes);
        }

        [Fact]
        public void LoadDefaultRecipes_Three()
        {
            _craftingSystem.LoadDefaultRecipes();

            var recipes = _craftingSystem.GetAllRecipes();
            Assert.Equal(3, recipes.Count);
            Assert.Contains(recipes, r => r.RecipeName == "меч");
            Assert.Contains(recipes, r => r.RecipeName == "лук");
            Assert.Contains(recipes, r => r.RecipeName == "Факел");
        }

        [Fact]
        public void Craft_ExistsCanCraft_Sword()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true); // железо
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true); // дерево
            mockInventory.Setup(inv => inv.RemoveItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.RemoveItem(202, 1)).Returns(true);

            var swordResult = new Mock<IItem>();
            swordResult.Setup(i => i.Id).Returns(101);
            swordResult.Setup(i => i.Name).Returns("крутой меч");

            var recipeMock = new Mock<ICraftingRecipe>();
            recipeMock.Setup(r => r.RecipeName).Returns("меч");
            recipeMock.Setup(r => r.CanCraft(mockInventory.Object)).Returns(true);
            recipeMock.Setup(r => r.Craft(mockInventory.Object)).Returns(swordResult.Object);

            _craftingSystem.AddRecipe(recipeMock.Object);

            var result = _craftingSystem.Craft("меч", mockInventory.Object);

            Assert.NotNull(result);
            Assert.Equal(101, result.Id);
            Assert.Equal("крутой меч", result.Name);
        }

        [Fact]
        public void Craft_DoesNotExist_Null()
        {
            var mockInventory = new Mock<IInventory>();

            var result = _craftingSystem.Craft("несуществующий", mockInventory.Object);

            Assert.Null(result);
        }

        [Fact]
        public void Craft_CannotCraft_Null()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var recipeMock = new Mock<ICraftingRecipe>();
            recipeMock.Setup(r => r.RecipeName).Returns("меч");
            recipeMock.Setup(r => r.CanCraft(mockInventory.Object)).Returns(false);

            _craftingSystem.AddRecipe(recipeMock.Object);

            var result = _craftingSystem.Craft("меч", mockInventory.Object);

            Assert.Null(result);
        }

        [Fact]
        public void CanCraft_ExistsHasItems_True()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true);

            var recipeMock = new Mock<ICraftingRecipe>();
            recipeMock.Setup(r => r.RecipeName).Returns("меч");
            recipeMock.Setup(r => r.CanCraft(mockInventory.Object)).Returns(true);

            _craftingSystem.AddRecipe(recipeMock.Object);

            var result = _craftingSystem.CanCraft("меч", mockInventory.Object);

            Assert.True(result);
        }

        [Fact]
        public void CanCraft_DoesNotExist_False()
        {
            var mockInventory = new Mock<IInventory>();

            var result = _craftingSystem.CanCraft("несуществующий", mockInventory.Object);

            Assert.False(result);
        }

        [Fact]
        public void CanCraft_ExistsCannotCraft_False()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var recipeMock = new Mock<ICraftingRecipe>();
            recipeMock.Setup(r => r.RecipeName).Returns("меч");
            recipeMock.Setup(r => r.CanCraft(mockInventory.Object)).Returns(false);

            _craftingSystem.AddRecipe(recipeMock.Object);

            var result = _craftingSystem.CanCraft("меч", mockInventory.Object);

            Assert.False(result);
        }

        [Fact]
        public void GetAvailableRecipes_CraftableRecipes()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(203, 1)).Returns(true);

            var swordRecipe = new Mock<ICraftingRecipe>();
            swordRecipe.Setup(r => r.RecipeName).Returns("меч");
            swordRecipe.Setup(r => r.CanCraft(mockInventory.Object)).Returns(true);

            var bowRecipe = new Mock<ICraftingRecipe>();
            bowRecipe.Setup(r => r.RecipeName).Returns("лук");
            bowRecipe.Setup(r => r.CanCraft(mockInventory.Object)).Returns(false);

            var torchRecipe = new Mock<ICraftingRecipe>();
            torchRecipe.Setup(r => r.RecipeName).Returns("Факел");
            torchRecipe.Setup(r => r.CanCraft(mockInventory.Object)).Returns(false);

            _craftingSystem.AddRecipe(swordRecipe.Object);
            _craftingSystem.AddRecipe(bowRecipe.Object);
            _craftingSystem.AddRecipe(torchRecipe.Object);

            var availableRecipes = _craftingSystem.GetAvailableRecipes(mockInventory.Object);

            Assert.Single(availableRecipes);
            Assert.Contains(availableRecipes, r => r.RecipeName == "меч");
            Assert.DoesNotContain(availableRecipes, r => r.RecipeName == "лук");
            Assert.DoesNotContain(availableRecipes, r => r.RecipeName == "Факел");
        }

        [Fact]
        public void GetAllRecipes_AllAddedRecipes()
        {
            var recipe1 = new Mock<ICraftingRecipe>();
            var recipe2 = new Mock<ICraftingRecipe>();
            var recipe3 = new Mock<ICraftingRecipe>();

            recipe1.Setup(r => r.RecipeName).Returns("recipe1");
            recipe2.Setup(r => r.RecipeName).Returns("recipe2");
            recipe3.Setup(r => r.RecipeName).Returns("recipe3");

            _craftingSystem.AddRecipe(recipe1.Object);
            _craftingSystem.AddRecipe(recipe2.Object);
            _craftingSystem.AddRecipe(recipe3.Object);

            var allRecipes = _craftingSystem.GetAllRecipes();

            Assert.Equal(3, allRecipes.Count);
            Assert.Contains(recipe1.Object, allRecipes);
            Assert.Contains(recipe2.Object, allRecipes);
            Assert.Contains(recipe3.Object, allRecipes);
        }

        [Fact]
        public void CraftingRecipe_CraftMultipleItems()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(true);
            mockInventory.Setup(inv => inv.HasItem(202, 1)).Returns(true);

            var recipeMock = new Mock<ICraftingRecipe>();
            recipeMock.Setup(r => r.CanCraft(mockInventory.Object)).Returns(true);

            var canCraft = recipeMock.Object.CanCraft(mockInventory.Object);

            Assert.True(canCraft);
        }

        [Fact]
        public void CraftingRecipe_CraftFalse_MissingItems()
        {
            var mockInventory = new Mock<IInventory>();
            mockInventory.Setup(inv => inv.HasItem(201, 2)).Returns(false);

            var recipeMock = new Mock<ICraftingRecipe>();
            recipeMock.Setup(r => r.CanCraft(mockInventory.Object)).Returns(false);

            var canCraft = recipeMock.Object.CanCraft(mockInventory.Object);

            Assert.False(canCraft);
        }
    }
}