using InventorySystem.Core.Strategies.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player;

namespace InventorySystem.Core.Strategies.Effects
{
    public class HealthEffect : IEffectStrategy
    {
        private readonly int _healthAmount;

        public HealthEffect(int healthAmount)
        {
            _healthAmount = healthAmount;
        }

        public void ApplyEffect(InventorySystem.Core.Player.Player player)
        {
            if (player == null) return;
            
            player.Health += _healthAmount;
            
            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }
        }
    }
}