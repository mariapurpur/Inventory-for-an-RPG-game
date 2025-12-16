using System.Collections.Generic;

namespace InventorySystem.Core.Strategies.Interfaces;

public interface IEffectStrategy
{
    void ApplyEffect(InventorySystem.Core.Player.Player player);
}