using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.States.Interfaces;

namespace InventorySystem.Core.Player.States.ItemStates
{
    public class BrokenState : IItemState
    {
        public string StateName => "сломався";
        
        public float GetEffectivenessMultiplier() => 0f;
        
        public bool CanUse() => false;
        
        public bool CanRepair() => true;
        
        public bool CanBreak() => false;
        
        public void OnUse(IItem item)
        {
        }
        
        public void OnRepair(IItem item)
        {
            if (item is IStatefulItem statefulItem)
            {
                statefulItem.SetState(new DamagedState());
            }
        }
        
        public void OnBreak(IItem item)
        {
        }
    }
}