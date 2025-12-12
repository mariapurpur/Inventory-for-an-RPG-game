using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Enchantment
{
    public class SharpnessEnchantment : BaseEnchantment
    {
        public SharpnessEnchantment() 
            : base($"острота", EnchantmentType.Sharpness)
        {
        }
        
        protected override void InitializeBonuses()
        {
            _bonuses["наносимый урон"] = 0.20f; // к урону +20%
            _bonuses["атака"] = 0.10f;
        }
    }
}