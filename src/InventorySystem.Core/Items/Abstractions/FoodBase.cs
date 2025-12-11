using InventorySystem.Core.Items.Interfaces;
using System;

namespace InventorySystem.Core.Items.Abstractions
{
    public abstract class FoodBase : ItemBase, IConsumable
    {
        public int UsesRemaining { get; set; }
        public int MaxUses { get; protected set; }
        
        public int HealthRestore { get; protected set; }
        public int HungerRestore { get; protected set; }
        
        protected FoodBase(
            int id,
            string name,
            int healthRestore,
            int hungerRestore,
            int maxStackSize = 64,
            int maxUses = 1,
            ItemRarity rarity = ItemRarity.Common)
            : base(id, name, maxStackSize, rarity)
        {
            HealthRestore = healthRestore;
            HungerRestore = hungerRestore;
            MaxUses = maxUses;
            UsesRemaining = maxUses;
        }
        
        public void Use(ICharacter target)
        {
            if (IsConsumed() || target == null) return;
            
            UsesRemaining--;

            target.Health = Math.Min(target.MaxHealth, target.Health + HealthRestore);
            target.Hunger = Math.Min(target.MaxHunger, target.Hunger + HungerRestore);
        }
        
        public bool IsConsumed()
        {
            return UsesRemaining <= 0;
        }
    }
}