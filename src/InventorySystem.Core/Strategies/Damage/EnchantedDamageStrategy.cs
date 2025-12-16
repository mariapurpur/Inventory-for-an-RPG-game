using InventorySystem.Core.Strategies.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Core.Strategies.Damage;

public class EnchantedDamageStrategy : IDamageStrategy
{
    public int Calculate(int baseDamage, List<IEnchantment> enchantments)
    {
        int totalDamage = baseDamage;
        foreach (var enchantment in enchantments)
        {
            totalDamage += enchantment.GetBonus("урон");
        }
        return totalDamage;
    }
}