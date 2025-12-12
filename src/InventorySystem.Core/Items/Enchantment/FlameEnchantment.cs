using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Enchantment
{
    public class FlameEnchantment : BaseEnchantment
    {
        public FlameEnchantment() 
            : base($"огненная стрела", EnchantmentType.Flame)
        {
        }
        
        protected override void InitializeBonuses()
        {
            _bonuses["огненный урон"] = 3f; // урон огня +3
            _bonuses["урон"] = 0.05f; // урон +5%
        }
        
        public override void ApplyEffect(ICharacter target)
        {
            if (target != null)
            {
                float fireDamage = GetBonus("огненный урон");
                target.Health -= fireDamage;
            }
        }
    }
}