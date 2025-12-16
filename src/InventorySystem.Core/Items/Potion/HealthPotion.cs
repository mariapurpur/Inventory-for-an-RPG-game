using InventorySystem.Core.Items.Interfaces;
using System;

namespace InventorySystem.Core.Items.Potion
{
    public class HealthPotion : PotionBase
    {
        public int HealAmount { get; private set; }
        
        public HealthPotion(int id = 401, string name = "зелье здоровья", int healAmount = 20, float duration = 0f) 
            : base(id, name, duration, maxStackSize: 4, maxUses: 1, rarity: ItemRarity.Legendary)
        {
            HealAmount = healAmount;
        }
        
        protected override IEffect CreateEffect()
        {
            return new HealthEffect(HealAmount, Duration);
        }
        
        public override void Use(InventorySystem.Core.Player.Player player)
        {
            if (IsConsumed() || player == null) 
                return;
            
            if (Duration == 0)
            {
                player.Health = Math.Min(player.MaxHealth, player.Health + HealAmount);
                UsesRemaining--;
            }
            else
            {
                base.Use(player);
            }
        }
        
        public override IItem Clone()
        {
            return new HealthPotion(Id, Name, HealAmount, Duration)
            {
                UsesRemaining = this.UsesRemaining,
                CurrentStack = this.CurrentStack
            };
        }
        
        private class HealthEffect : IEffect
        {
            private readonly int _healAmount;
            
            public string Name => "восстановление здоровья";
            public float Duration { get; private set; }
            
            public HealthEffect(int healAmount, float duration)
            {
                _healAmount = healAmount;
                Duration = duration;
            }
            
            public void Apply(ICharacter target)
            {
                if (target != null && Duration > 0)
                {
                    target.Health = Math.Min(target.MaxHealth, target.Health + _healAmount);
                }
            }
            
            public void Remove(ICharacter target)
            {
            }
        }
    }
}