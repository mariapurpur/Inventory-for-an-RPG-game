using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Weapon;
using System.Collections.Generic;

namespace InventorySystem.Core.Crafting.Recipes
{
    public class BowRecipe : CraftingRecipe
    {
        public override string RecipeName => "лук";
        
        public override Dictionary<int, int> RequiredItems => new Dictionary<int, int>
        {
            { 202, 3 }, // id дерева
            { 203, 2 }  // id нитки
        };
        
        public override IItem? Craft(IInventory inventory)
        {
            if (!CanCraft(inventory))
                return null;
                
            if (!ConsumeItems(inventory))
                return null;

            return new Bow(id: 102, name: "новый лук");
        }
    }
}