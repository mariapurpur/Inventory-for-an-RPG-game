using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Core.Crafting.Recipes
{
    public class TorchRecipe : CraftingRecipe
    {
        public override string RecipeName => "Факел";
        
        public override Dictionary<int, int> RequiredItems => new Dictionary<int, int>
        {
            { 204, 1 }, // id угля
            { 205, 1 }  // id палки
        };
        
        public override IItem? Craft(IInventory inventory)
        {
            if (!CanCraft(inventory))
                return null;
                
            if (!ConsumeItems(inventory))
                return null;
            
            return new SimpleItem(
                id: 301,
                name: "Факел",
                maxStackSize: 16,
                rarity: ItemRarity.Common
            );
        }
    }

    public class SimpleItem : IItem
    {
        public int Id { get; }
        public string Name { get; }
        public int MaxStackSize { get; }
        public int CurrentStack { get; set; }
        public ItemRarity Rarity { get; }
        
        public SimpleItem(int id, string name, int maxStackSize = 1, ItemRarity rarity = ItemRarity.Common)
        {
            Id = id;
            Name = name;
            MaxStackSize = maxStackSize;
            CurrentStack = 1;
            Rarity = rarity;
        }
        
        public bool CanStackWith(IItem other)
        {
            return other is SimpleItem otherItem && 
                   otherItem.Id == this.Id &&
                   otherItem.CurrentStack < otherItem.MaxStackSize;
        }
        
        public IItem Clone()
        {
            return new SimpleItem(Id, Name, MaxStackSize, Rarity)
            {
                CurrentStack = this.CurrentStack
            };
        }
    }
}