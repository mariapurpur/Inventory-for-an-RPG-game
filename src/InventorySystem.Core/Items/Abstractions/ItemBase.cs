using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Abstractions
{
    public abstract class ItemBase : IItem
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public int MaxStackSize { get; protected set; }
        public int CurrentStack { get; set; }
        public ItemRarity Rarity { get; protected set; }

        protected ItemBase(int id, string name, int maxStackSize = 64, ItemRarity rarity = ItemRarity.Common)
        {
            Id = id;
            Name = name;
            MaxStackSize = maxStackSize;
            CurrentStack = 1;
            Rarity = rarity;
        }

        public virtual bool CanStackWith(IItem other)
        {
            return other != null && 
                   other.GetType() == this.GetType() &&
                   other.Id == this.Id &&
                   other.CurrentStack < other.MaxStackSize;
        }

        public abstract IItem Clone();
    }
}