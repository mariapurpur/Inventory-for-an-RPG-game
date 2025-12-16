using InventorySystem.Core.Crafting.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Crafting.Recipes;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Core.Crafting
{
    public class CraftingSystem
    {
        private readonly List<ICraftingRecipe> _recipes;
        
        public CraftingSystem()
        {
            _recipes = new List<ICraftingRecipe>();
        }
        
        public void AddRecipe(ICraftingRecipe recipe)
        {
            _recipes.Add(recipe);
        }
        
        public IItem? Craft(string recipeName, IInventory inventory)
        {
            var recipe = _recipes.FirstOrDefault(r => r.RecipeName == recipeName);
            if (recipe == null)
                return null;
                
            if (!recipe.CanCraft(inventory))
                return null;
                
            return recipe.Craft(inventory);
        }
        
        public bool CanCraft(string recipeName, IInventory inventory)
        {
            var recipe = _recipes.FirstOrDefault(r => r.RecipeName == recipeName);
            return recipe != null && recipe.CanCraft(inventory);
        }
        
        public List<ICraftingRecipe> GetAvailableRecipes(IInventory inventory)
        {
            return _recipes.Where(recipe => recipe.CanCraft(inventory)).ToList();
        }
        
        public List<ICraftingRecipe> GetAllRecipes()
        {
            return new List<ICraftingRecipe>(_recipes);
        }
        
        public void LoadDefaultRecipes()
        {
            AddRecipe(new SwordRecipe());
            AddRecipe(new BowRecipe());
            AddRecipe(new TorchRecipe());
        }
    }
}