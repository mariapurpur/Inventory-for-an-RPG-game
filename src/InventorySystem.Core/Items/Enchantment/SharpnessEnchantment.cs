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
            _bonuses["урон"] = 3; // к урону +3
            _bonuses["атака"] = 2;
        }
    }
}