using InventorySystem.Core.Items.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Core.Items.Enchantment
{
    public class EnchantmentDecorator : IEnchantment
    {
        private readonly IEnchantment _baseEnchantment;
        private readonly List<IEnchantment> _additionalEnchantments = new List<IEnchantment>();
        
        public string Name => _baseEnchantment.Name + " (зачаровано)";
        public EnchantmentType Type => _baseEnchantment.Type;
        
        public EnchantmentDecorator(IEnchantment baseEnchantment)
        {
            _baseEnchantment = baseEnchantment;
        }
        
        public void AddEnchantment(IEnchantment enchantment)
        {
            if (enchantment.Type == this.Type)
            {
                _additionalEnchantments.Add(enchantment);
            }
        }
        
        public float GetBonus(string statType)
        {
            float totalBonus = _baseEnchantment.GetBonus(statType);
            
            foreach (var enchantment in _additionalEnchantments)
            {
                totalBonus += enchantment.GetBonus(statType);
            }
            
            return totalBonus;
        }
        
        public void ApplyEffect(ICharacter target)
        {
            _baseEnchantment.ApplyEffect(target);
            
            foreach (var enchantment in _additionalEnchantments)
            {
                enchantment.ApplyEffect(target);
            }
        }
    }
}