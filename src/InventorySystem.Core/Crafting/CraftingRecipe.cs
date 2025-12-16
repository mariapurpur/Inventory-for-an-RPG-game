using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Crafting.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Core.Crafting
{
    public abstract class CraftingRecipe : ICraftingRecipe
    {
        public abstract string RecipeName { get; }
        public abstract Dictionary<int, int> RequiredItems { get; }
        
        public virtual bool CanCraft(IInventory inventory)
        {
            foreach (var itemReq in RequiredItems)
            {
                if (inventory.GetItemCount(itemReq.Key) < itemReq.Value)
                    return false;
            }
            return true;
        }
        
        public abstract IItem? Craft(IInventory inventory);
        
        protected bool ConsumeItems(IInventory inventory)
        {
            foreach (var itemReq in RequiredItems)
            {
                if (!inventory.RemoveItem(itemReq.Key, itemReq.Value))
                    return false;
            }
            return true;
        }
    }
}