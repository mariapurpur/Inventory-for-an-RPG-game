using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player;

namespace InventorySystem.Core.Player
{
    public abstract class PlayerState
    {
        public abstract string Name { get; }
        
        public virtual int GetEffectivenessMultiplier(string actionType)
        {
            return 1;
        }
        
        public virtual void OnEnter(Player player)
        {
        }
        
        public virtual void OnExit(Player player)
        {
        }
        
        public virtual void Update(Player player)
        {
        }

        public virtual bool CanPerformAction(string action)
        {
            return true;
        }
    }
}