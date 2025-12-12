using System.Collections.Generic;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Inventory.Interfaces
{
    public interface IInventory
    {
        int Capacity { get; }
        int UsedSlots { get; }
        int TotalItems { get; }
        
        bool AddItem(IItem item, int count = 1);
        bool RemoveItem(int itemId, int count = 1);
        
        IItem GetItem(int slotIndex);
        
        int GetItemCount(int itemId);
        bool HasItem(int itemId, int count = 1);
        bool HasSpaceFor(IItem item, int count = 1);
        void ClearSlot(int slotIndex);
        
        IEnumerable<InventorySlot> GetAllSlots();
        IEnumerable<IItem> GetAllItems();
    }
}