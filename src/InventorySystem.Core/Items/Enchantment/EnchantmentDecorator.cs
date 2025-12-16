using InventorySystem.Core.Items.Interfaces;
using System.Collections.Generic;
using System.Security;
using InventorySystem.Core.Player;

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
            _additionalEnchantments.Add(enchantment);
        }
        
        public int GetBonus(string statType)
        {
            int totalBonus = _baseEnchantment.GetBonus(statType);
            
            foreach (var enchantment in _additionalEnchantments)
            {
                totalBonus += enchantment.GetBonus(statType);
            }
            
            return totalBonus;
        }
        
        public void ApplyEffect(InventorySystem.Core.Player.Player player)
        {
            _baseEnchantment.ApplyEffect(player);
            
            foreach (var enchantment in _additionalEnchantments)
            {
                enchantment.ApplyEffect(player);
            }
        }
    }
}