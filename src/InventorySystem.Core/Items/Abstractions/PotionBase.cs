using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Items.Abstractions;
using System;

namespace InventorySystem.Core.Items.Potion
{
    public abstract class PotionBase : ItemBase, IConsumable
    {
        public int UsesRemaining { get; set; }
        public int MaxUses { get; protected set; }
        public int HealthRestore => 0;
        public int HungerRestore => 0;
    
        public float Duration { get; protected set; }
        protected IEffect _effect;
    
        protected PotionBase(
            int id,
            string name,
            float duration,
            int maxStackSize = 4,
            int maxUses = 1,
            ItemRarity rarity = ItemRarity.Legendary) : base(id, name, maxStackSize, rarity)
        {
            Id = id;
            Name = name;
            MaxStackSize = maxStackSize;
            CurrentStack = 1;
            Rarity = rarity;
            Duration = duration;
            MaxUses = maxUses;
            UsesRemaining = maxUses;
            _effect = CreateEffect();
        }
        
        protected abstract IEffect CreateEffect();
        
        public virtual void Use(ICharacter target)
        {
            if (IsConsumed() || target == null) return;
            
            UsesRemaining--;
            
            if (_effect != null)
            {
                _effect.Apply(target);
            }
        }
        
        public bool IsConsumed()
        {
            return UsesRemaining <= 0;
        }
        
        public override abstract IItem Clone();
        
        public override bool CanStackWith(IItem other)
        {
            if (other is PotionBase otherPotion)
            {
                return base.CanStackWith(other) && 
                       this.UsesRemaining == this.MaxUses && 
                       otherPotion.UsesRemaining == otherPotion.MaxUses;
            }
            return false;
        }
    }
    
    public interface IEffect
    {
        string Name { get; }
        float Duration { get; }
        void Apply(ICharacter target);
        void Remove(ICharacter target);
    }
}