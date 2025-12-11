using System.Collections.Generic;

namespace InventorySystem.Core.Items.Interfaces
{
    public interface IEnchantable : IItem
    {
        int MaxEnchantments { get; }
        IEnumerable<IEnchantment> Enchantments { get; }
        
        bool AddEnchantment(IEnchantment enchantment);
        bool RemoveEnchantment(IEnchantment enchantment);
        bool HasEnchantment(string enchantmentType);
        float GetEnchantmentBonus(string statType);
    }

    public interface IEnchantment
    {
        string Name { get; }
        EnchantmentType Type { get; }
        
        float GetBonus(string statType);
        void ApplyEffect(ICharacter target);
    }

    public enum EnchantmentType
    {
        Sharpness,
        Protection,
        Flame
    }
}