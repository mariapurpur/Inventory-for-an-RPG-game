using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Enchantment
{
    public class ProtectionEnchantment : BaseEnchantment
    {
        public ProtectionEnchantment() 
            : base($"защита", EnchantmentType.Protection)
        {
        }
        
        protected override void InitializeBonuses()
        {
            _bonuses["защита"] = 0.15f; // защита +15%
            _bonuses["прочность"] = 0.10f; // прочность +10%
        }
    }
}