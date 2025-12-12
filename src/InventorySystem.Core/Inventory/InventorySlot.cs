using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Inventory
{
    public class InventorySlot
    {
        public int Index { get; }
        public IItem? Item { get; private set; }
        public int Count { get; private set; }
        public bool IsEmpty => Item == null || Count == 0;
        
        public InventorySlot(int index)
        {
            Index = index;
            Item = null;
            Count = 0;
        }
        
        public bool CanAddItem(IItem item, int count = 1)
        {
            if (item == null || count <= 0) return false;
            
            if (IsEmpty)
            {
                return true;
            }
            
            return Item!.CanStackWith(item) && 
                   Count + count <= item.MaxStackSize;
        }
        
        public bool AddItem(IItem item, int count = 1)
        {
            if (!CanAddItem(item, count)) return false;
            
            if (IsEmpty)
            {
                Item = item;
                Count = count;
                item.CurrentStack = count;
            }
            else
            {
                Count += count;
                Item!.CurrentStack = Count;
            }
            
            return true;
        }
        
        public bool RemoveItem(int count = 1)
        {
            if (IsEmpty || count <= 0 || count > Count) return false;
            
            Count -= count;
            Item!.CurrentStack = Count;
            
            if (Count == 0)
            {
                Clear();
            }
            
            return true;
        }
        
        public void Clear()
        {
            Item = null;
            Count = 0;
        }
        
        public InventorySlot Clone()
        {
            var slot = new InventorySlot(Index);
            if (!IsEmpty)
            {
                slot.Item = Item!.Clone();
                slot.Count = Count;
            }
            return slot;
        }
    }
}