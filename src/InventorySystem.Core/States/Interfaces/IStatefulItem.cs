using InventorySystem.Core.Player.States.Interfaces;

namespace InventorySystem.Core.Items.Interfaces
{
    public interface IStatefulItem : IItem
    {
        IItemState CurrentState { get; }
        void SetState(IItemState newState);
        float GetCurrentEffectiveness();
    }
}