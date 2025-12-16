using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Core.Player.Equipment
{
    public class EquipmentManager
    {
        private readonly Dictionary<EquipmentSlot, IEquippable?> _equippedItems;
        private readonly Player _player;
        
        public event System.Action<EquipmentSlot, IEquippable?>? OnEquipmentChanged;
        
        public EquipmentManager(Player player)
        {
            _player = player;
            _equippedItems = new Dictionary<EquipmentSlot, IEquippable?>();
            
            foreach (var slot in EquipmentSlots.AllSlots)
            {
                _equippedItems[slot] = null;
            }
        }
        
        public bool Equip(IEquippable item)
        {
            if (item == null) return false;
            
            if (!item.Equip())
                return false;
            
            EquipmentSlot slot = item.Slot; 

            var oldItem = _equippedItems[slot];
            if (oldItem != null)
            {
                Unequip(slot);
            }
            
            _equippedItems[slot] = item;
            
            ApplyItemBonuses(item);
            
            OnEquipmentChanged?.Invoke(slot, item);
            return true;
        }

        public bool Unequip(EquipmentSlot slot)
        {
            var item = _equippedItems[slot];
            if (item == null) return false;
            
            item.Unequip();
            _equippedItems[slot] = null;
            
            RemoveItemBonuses(item);
            
            OnEquipmentChanged?.Invoke(slot, null);
            return true;
        }
        
        public IEquippable? GetEquippedItem(EquipmentSlot slot)
        {
            return _equippedItems.ContainsKey(slot) ? _equippedItems[slot] : null;
        }

        public Dictionary<EquipmentSlot, IEquippable?> GetAllEquippedItems()
        {
            return new Dictionary<EquipmentSlot, IEquippable?>(_equippedItems);
        }

        public IEnumerable<IEquippable?> GetItemsWithBonus(string bonusType)
        {
            return _equippedItems.Values
                .Where(item => item != null && item is IEnchantable enchantable 
                    && enchantable.GetEnchantmentBonus(bonusType) > 0);
        }

        public int GetTotalBonus(string statType)
        {
            int total = 0;
            foreach (var item in _equippedItems.Values.Where(i => i != null))
            {
                if (item is IEnchantable enchantable)
                {
                    total += enchantable.GetEnchantmentBonus(statType);
                }
            }
            return total;
        }
        
        public bool IsEquipped(IEquippable item)
        {
            return _equippedItems.ContainsValue(item);
        }

        private void ApplyItemBonuses(IEquippable item)
        {
            if (item is IEnchantable enchantable)
            {
                _player.DamageMultiplier += enchantable.GetEnchantmentBonus("урон") / 100;
            }
        }

        private void RemoveItemBonuses(IEquippable item)
        {
            if (item is IEnchantable enchantable)
            {
                _player.DamageMultiplier -= enchantable.GetEnchantmentBonus("урон") / 100;
            }
        }

        public EquipmentStats GetEquipmentStats()
        {
            return new EquipmentStats
            {
                TotalItems = _equippedItems.Values.Count(i => i != null),
                TotalDefense = GetTotalBonus("защита"),
                TotalDamage = GetTotalBonus("урон"),
                TotalSlots = EquipmentSlots.AllSlots.Length
            };
        }
        
        public class EquipmentStats
        {
            public int TotalItems { get; set; }
            public int TotalDefense { get; set; }
            public int TotalDamage { get; set; }
            public int TotalSlots { get; set; }
        }
    }
}