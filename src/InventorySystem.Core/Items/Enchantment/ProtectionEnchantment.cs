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
            _bonuses["защита"] = 2; // защита +2
            _bonuses["прочность"] = 2; // прочность +2
        }
    }
}