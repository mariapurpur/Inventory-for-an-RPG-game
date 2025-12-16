using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Core.Crafting.Interfaces
{
    public interface ICraftingRecipe
    {
        string RecipeName { get; }
        Dictionary<int, int> RequiredItems { get; }
        bool CanCraft(IInventory inventory);
        IItem? Craft(IInventory inventory);
    }
}