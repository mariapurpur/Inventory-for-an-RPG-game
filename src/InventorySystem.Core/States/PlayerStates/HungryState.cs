using InventorySystem.Core.Player;

namespace InventorySystem.Core.Player.States.PlayerStates
{
    public class HungryState : PlayerState
    {
        public override string Name => "хочет кушац";
        
        public override int GetEffectivenessMultiplier(string actionType)
        {
            return actionType switch
            {
                "атаковать" => 1,
                "защищать" => 1,
                "крафтить" => 0,
                _ => 1
            };
        }
        
        public override void OnEnter(Player player)
        {
            player.TakeDamage(10);
        }
        
        public override void Update(Player player)
        {
            player.TakeDamage(1);
            
            if (player.Hunger > 70)
            {
                player.CurrentState = new HealthyState();
                player.CurrentState.OnEnter(player);
            }
            else if (player.Hunger < 10)
            {
                player.TakeDamage(1);
            }
        }
        
        public override bool CanPerformAction(string action)
        {
            return action != "крафтить";
        }
    }
}