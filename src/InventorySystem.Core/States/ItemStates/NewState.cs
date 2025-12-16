using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.States.Interfaces;

namespace InventorySystem.Core.Player.States.ItemStates
{
    public class NewState : IItemState
    {
        public string StateName => "новое";
        
        public float GetEffectivenessMultiplier() => 1f;
        
        public bool CanUse() => true;
        
        public bool CanRepair() => false;
        
        public bool CanBreak() => true;
        
        public void OnUse(IItem item)
        {
            var random = new System.Random();
            if (random.NextDouble() < 0.1)
            {
                OnBreak(item);
            }
        }
        
        public void OnRepair(IItem item)
        {
        }
        
        public void OnBreak(IItem item)
        {
            if (item is IStatefulItem statefulItem)
            {
                statefulItem.SetState(new DamagedState());
            }
        }
    }
}