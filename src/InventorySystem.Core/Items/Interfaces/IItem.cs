using System;

namespace InventorySystem.Core.Items.Interfaces
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; }
        int MaxStackSize { get; }
        int CurrentStack { get; set; }
        ItemRarity Rarity { get; }
        
        bool CanStackWith(IItem other);
        IItem Clone();
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Legendary
    }
}