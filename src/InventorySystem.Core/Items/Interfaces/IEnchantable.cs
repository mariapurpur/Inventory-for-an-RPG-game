using System.Collections.Generic;
using InventorySystem.Core.Player;

namespace InventorySystem.Core.Items.Interfaces
{
    public interface IEnchantable : IItem
    {
        int MaxEnchantments { get; }
        IEnumerable<IEnchantment> Enchantments { get; }
        
        bool AddEnchantment(IEnchantment enchantment);
        bool RemoveEnchantment(IEnchantment enchantment);
        bool HasEnchantment(string enchantmentType);
        int GetEnchantmentBonus(string statType);
    }

    public interface IEnchantment
    {
        string Name { get; }
        EnchantmentType Type { get; }
        
        int GetBonus(string statType);
        void ApplyEffect(InventorySystem.Core.Player.Player player);
    }

    public enum EnchantmentType
    {
        Sharpness,
        Protection,
        Flame
    }
}