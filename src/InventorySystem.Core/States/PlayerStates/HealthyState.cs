using InventorySystem.Core.Player;

namespace InventorySystem.Core.Player.States.PlayerStates
{
    public class HealthyState : PlayerState
    {
        public override string Name => "здоровый";
        
        public override int GetEffectivenessMultiplier(string actionType)
        {
            return actionType switch
            {
                "атаковать" => 2,
                "защищать" => 2,
                "крафтить" => 2,
                _ => 1
            };
        }
        
        public override void OnEnter(Player player)
        {
            player.Health = player.MaxHealth;
        }
        
        public override void Update(Player player)
        {
            player.ConsumeHunger(1);
            
            if (player.Hunger < 30)
            {
                player.CurrentState = new HungryState();
                player.CurrentState.OnEnter(player);
            }
        }
        
        public override bool CanPerformAction(string action)
        {
            return true;
        }
    }
}