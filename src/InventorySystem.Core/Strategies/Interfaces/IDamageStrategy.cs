using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Strategies.Interfaces;

public interface IDamageStrategy
{
    int Calculate(int baseDamage, List<IEnchantment> enchantments);
}