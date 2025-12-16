using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Weapon;
using System.Collections.Generic;

namespace InventorySystem.Core.Crafting.Recipes
{
    public class SwordRecipe : CraftingRecipe
    {
        public override string RecipeName => "меч";
        
        public override Dictionary<int, int> RequiredItems => new Dictionary<int, int>
        {
            { 201, 2 }, // id железа
            { 202, 1 }  // id дерева
        };
        
        public override IItem? Craft(IInventory inventory)
        {
            if (!CanCraft(inventory))
                return null;
                
            if (!ConsumeItems(inventory))
                return null;
            
            return new Sword(id: 101, name: "крутой меч");
        }
    }
}