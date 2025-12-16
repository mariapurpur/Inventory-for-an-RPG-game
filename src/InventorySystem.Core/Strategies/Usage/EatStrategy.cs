using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Strategies.Interfaces;

namespace InventorySystem.Core.Strategies.Usage;

public class EatStrategy : IUsageStrategy
{
    public void Use(InventorySystem.Core.Player.Player player, IItem item)
    {
        if (item is IConsumable consumable)
        {
            consumable.Use(player);
        }
        else
        {
            throw new InvalidOperationException($"вы укусили {item.Name}. вы задумались о своих жизненных выборах.");
        }
    }
}