using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Strategies.Interfaces;

namespace InventorySystem.Core.Strategies.Usage;

public class EquipStrategy : IUsageStrategy
{
    public void Use(InventorySystem.Core.Player.Player player, IItem item)
    {
        if (item is IEquippable equippable)
        {
            player.Equip(equippable);
        }
        else
        {
            throw new InvalidOperationException($"вы попытались надеть {item.Name}, но это довольно быстро оказалось невозможным.");
        }
    }
}