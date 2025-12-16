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
            _bonuses["огненный урон"] = 3; // урон огня +3
            _bonuses["урон"] = 1; // урон +1
        }
        
        public override void ApplyEffect(InventorySystem.Core.Player.Player player)
        {
            if (player != null)
            {
                float fireDamage = GetBonus("огненный урон");
                player.Health -= fireDamage;
            }
        }
    }
}