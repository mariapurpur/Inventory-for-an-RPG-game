using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Player.States.Interfaces
{
    public interface IItemState
    {
        string StateName { get; }
        float GetEffectivenessMultiplier();
        bool CanUse();
        bool CanRepair();
        bool CanBreak();
        void OnUse(IItem item);
        void OnRepair(IItem item);
        void OnBreak(IItem item);
    }
}