using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Items.Interfaces;
using System;
using System.Collections.Generic;

namespace InventorySystem.Core.Inventory
{
    public class InventoryManager
    {
        private readonly IInventory _inventory;
        
        public InventoryManager(IInventory inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }
        
        public List<IEquippable> FindAllWeapons()
        {
            var weapons = new List<IEquippable>();
            
            foreach (var slot in _inventory.GetAllSlots())
            {
                if (!slot.IsEmpty && slot.Item is IEquippable equippable)
                {
                    weapons.Add(equippable);
                }
            }
            
            return weapons;
        }
        
        public List<IConsumable> FindAllFood()
        {
            var food = new List<IConsumable>();
            
            foreach (var slot in _inventory.GetAllSlots())
            {
                if (!slot.IsEmpty && slot.Item is IConsumable consumable)
                {
                    food.Add(consumable);
                }
            }
            
            return food;
        }
        
        public List<IEnchantable> FindEnchantedItems()
        {
            var enchanted = new List<IEnchantable>();
            
            foreach (var slot in _inventory.GetAllSlots())
            {
                if (!slot.IsEmpty && slot.Item is IEnchantable enchantable)
                {
                    enchanted.Add(enchantable);
                }
            }
            
            return enchanted;
        }
        
        public bool CanCraft(Dictionary<int, int> recipe)
        {
            foreach (var requirement in recipe)
            {
                if (!_inventory.HasItem(requirement.Key, requirement.Value))
                {
                    return false;
                }
            }
            return true;
        }
        
        public bool CraftItem(Dictionary<int, int> recipe)
        {
            if (!CanCraft(recipe)) return false;
            
            foreach (var requirement in recipe)
            {
                _inventory.RemoveItem(requirement.Key, requirement.Value);
            }
            
            return true;
        }
        
        public string GetInventorySummary()
        {
            var summary = new System.Text.StringBuilder();
            summary.AppendLine($"-- состояние инвентаря --");
            summary.AppendLine($"вместимость: {_inventory.Capacity}");
            summary.AppendLine($"использовано слотов: {_inventory.UsedSlots}");
            summary.AppendLine($"всего предметов: {_inventory.TotalItems}");
            
            var itemCounts = new Dictionary<int, (string name, int count)>();
            
            foreach (var slot in _inventory.GetAllSlots())
            {
                if (!slot.IsEmpty)
                {
                    if (!itemCounts.ContainsKey(slot.Item!.Id))
                    {
                        itemCounts[slot.Item.Id] = (slot.Item.Name, 0);
                    }
                    
                    var current = itemCounts[slot.Item.Id];
                    itemCounts[slot.Item.Id] = (current.name, current.count + slot.Count);
                }
            }
            
            if (itemCounts.Count > 0)
            {
                summary.AppendLine("предметы:");
                foreach (var item in itemCounts.Values)
                {
                    summary.AppendLine($"{item.name}: {item.count} шт.");
                }
            }
            
            return summary.ToString();
        }
    }
}