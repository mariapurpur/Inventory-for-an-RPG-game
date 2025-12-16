using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.States.Interfaces;

namespace InventorySystem.Core.Player.States.ItemStates
{
    public class DamagedState : IItemState
    {
        public string StateName => "испорчен, на грани поломки";
        
        public float GetEffectivenessMultiplier() => 0.5f;
        
        public bool CanUse() => true;
        
        public bool CanRepair() => true;
        
        public bool CanBreak() => true;
        
        public void OnUse(IItem item)
        {
            var random = new System.Random();
            if (random.NextDouble() < 0.2)
            {
                OnBreak(item);
            }
        }
        
        public void OnRepair(IItem item)
        {
            if (item is IStatefulItem statefulItem)
            {
                statefulItem.SetState(new NewState());
            }
        }
        
        public void OnBreak(IItem item)
        {
            if (item is IStatefulItem statefulItem)
            {
                statefulItem.SetState(new BrokenState());
            }
        }
    }
}