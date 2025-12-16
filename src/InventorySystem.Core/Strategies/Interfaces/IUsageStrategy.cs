using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player;

namespace InventorySystem.Core.Strategies.Interfaces;

public interface IUsageStrategy
{
    void Use(InventorySystem.Core.Player.Player player, IItem item);
}