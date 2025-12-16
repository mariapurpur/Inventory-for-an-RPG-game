using InventorySystem.Core.Strategies.Interfaces;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Strategies.Damage;

public class SimpleDamageStrategy : IDamageStrategy
{
    public int Calculate(int baseDamage, List<IEnchantment> enchantments)
    {
        return baseDamage;
    }
}