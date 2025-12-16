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
        
        public void Use(InventorySystem.Core.Player.Player player)
        {
            if (IsConsumed() || player == null) return;
            
            UsesRemaining--;

            player.Health = Math.Min(player.MaxHealth, player.Health + HealthRestore);
            player.Hunger = Math.Min(player.MaxHunger, player.Hunger + HungerRestore);
        }
        
        public bool IsConsumed()
        {
            return UsesRemaining <= 0;
        }
    }
}