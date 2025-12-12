using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Potion
{
    public class StrengthPotion : PotionBase
    {
        public float DamageMultiplier { get; private set; }
        
        public StrengthPotion(int id = 403, string name = "зелье силы", float damageMultiplier = 2.0f, float duration = 20f) 
            : base(id, name, duration, maxStackSize: 4, maxUses: 1, rarity: ItemRarity.Legendary)
        {
            DamageMultiplier = damageMultiplier;
        }
        
        protected override IEffect CreateEffect()
        {
            return new StrengthEffect(DamageMultiplier, Duration);
        }
        
        public override IItem Clone()
        {
            return new StrengthPotion(Id, Name, DamageMultiplier, Duration)
            {
                UsesRemaining = this.UsesRemaining,
                CurrentStack = this.CurrentStack
            };
        }

        private class StrengthEffect : IEffect
        {
            private readonly float _damageMultiplier;
            
            public string Name => "увеличение силы";
            public float Duration { get; private set; }
            
            public StrengthEffect(float damageMultiplier, float duration)
            {
                _damageMultiplier = damageMultiplier;
                Duration = duration;
            }
            
            public void Apply(ICharacter target)
            {
                if (target != null)
                {
                    target.DamageMultiplier *= _damageMultiplier;
                }
            }
            
            public void Remove(ICharacter target)
            {
                if (target != null)
                {
                    target.DamageMultiplier /= _damageMultiplier;
                }
            }
        }
    }
}