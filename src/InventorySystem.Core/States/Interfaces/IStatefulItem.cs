using InventorySystem.Core.Player.States.Interfaces;

namespace InventorySystem.Core.Items.Interfaces
{
    public interface IStatefulItem : IItem
    {
        void SetState(IItemState newState);
    }
}