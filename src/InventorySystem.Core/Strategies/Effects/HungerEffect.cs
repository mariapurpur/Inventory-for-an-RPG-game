using InventorySystem.Core.Strategies.Interfaces;
using InventorySystem.Core.Player;

namespace InventorySystem.Core.Strategies.Effects
{
    public class HungerEffect : IEffectStrategy
    {
        private readonly int _hungerAmount;

        public HungerEffect(int hungerAmount)
        {
            _hungerAmount = hungerAmount;
        }

        public void ApplyEffect(InventorySystem.Core.Player.Player player)
        {
            if (player == null) return;
            
            player.Hunger += _hungerAmount;
            
            if (player.Hunger > player.MaxHunger)
            {
                player.Hunger = player.MaxHunger;
            }
        }
    }
}