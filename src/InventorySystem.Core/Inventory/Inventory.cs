using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Core.Inventory
{
    public class Inventory : IInventory
    {
        private readonly InventorySlot[] _slots;
        
        public int Capacity => _slots.Length;
        public int UsedSlots => _slots.Count(slot => !slot.IsEmpty);
        public int TotalItems => _slots.Sum(slot => slot.Count);
        
        public Inventory(int capacity = 20)
        {
            if (capacity <= 0) 
                throw new ArgumentException("вместимость должна быть больше 0 :(", nameof(capacity));
            
            _slots = new InventorySlot[capacity];
            for (int i = 0; i < capacity; i++)
            {
                _slots[i] = new InventorySlot(i);
            }
        }
        
        public bool AddItem(IItem item, int count = 1)
        {
            if (item == null || count <= 0) return false;
            
            if (item.MaxStackSize > 1)
            {
                foreach (var slot in _slots)
                {
                    if (!slot.IsEmpty && slot.Item!.CanStackWith(item))
                    {
                        int spaceInSlot = item.MaxStackSize - slot.Count;
                        int toAdd = Math.Min(spaceInSlot, count);
                        
                        if (toAdd > 0)
                        {
                            slot.AddItem(item, toAdd);
                            count -= toAdd;
                            
                            if (count == 0) return true;
                        }
                    }
                }
            }
            
            if (count > 0)
            {
                foreach (var slot in _slots)
                {
                    if (slot.IsEmpty)
                    {
                        int stackSize = Math.Min(count, item.MaxStackSize);
                        slot.AddItem(item, stackSize);
                        count -= stackSize;
                        
                        if (count == 0) return true;
                    }
                }
            }
            
            return count == 0;
        }
        
        public bool RemoveItem(int itemId, int count = 1)
        {
            if (count <= 0) return false;
            
            int remaining = count;
            
            var slotsWithItem = _slots
                .Where(slot => !slot.IsEmpty && slot.Item!.Id == itemId)
                .OrderBy(slot => slot.Count)
                .ToList();
            
            foreach (var slot in slotsWithItem)
            {
                if (remaining <= 0) break;
                
                int toRemove = Math.Min(slot.Count, remaining);
                slot.RemoveItem(toRemove);
                remaining -= toRemove;
            }
            
            return remaining == 0;
        }
        
        public IItem GetItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= Capacity)
                throw new ArgumentOutOfRangeException(nameof(slotIndex));
            
            return _slots[slotIndex].Item!.Clone();
        }
        
        public int GetItemCount(int itemId)
        {
            return _slots
                .Where(slot => !slot.IsEmpty && slot.Item!.Id == itemId)
                .Sum(slot => slot.Count);
        }
        
        public bool HasItem(int itemId, int count = 1)
        {
            return GetItemCount(itemId) >= count;
        }
        
        public bool HasSpaceFor(IItem item, int count = 1)
        {
            if (item == null || count <= 0) return false;
            
            int remaining = count;
            
            if (item.MaxStackSize > 1)
            {
                foreach (var slot in _slots)
                {
                    if (!slot.IsEmpty && slot.Item!.CanStackWith(item))
                    {
                        int spaceInSlot = item.MaxStackSize - slot.Count;
                        remaining -= Math.Min(spaceInSlot, remaining);
                        
                        if (remaining <= 0) return true;
                    }
                }
            }
            
            if (remaining > 0)
            {
                int emptySlots = _slots.Count(slot => slot.IsEmpty);
                int maxPerSlot = item.MaxStackSize;
                int maxInEmptySlots = emptySlots * maxPerSlot;
                
                return remaining <= maxInEmptySlots;
            }
            
            return true;
        }
        
        public void ClearSlot(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < Capacity)
            {
                _slots[slotIndex].Clear();
            }
        }
        
        public IEnumerable<InventorySlot> GetAllSlots()
        {
            return _slots.Select(slot => slot.Clone()).ToList();
        }
        
        public IEnumerable<IItem> GetAllItems()
        {
            return _slots
                .Where(slot => !slot.IsEmpty)
                .Select(slot => slot.Item!.Clone())
                .ToList();
        }
    }
}