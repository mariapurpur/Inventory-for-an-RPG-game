using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player;

namespace InventorySystem.Core.Items.Enchantment
{
    public abstract class BaseEnchantment : IEnchantment
    {
        public string Name { get; protected set; }
        public EnchantmentType Type { get; protected set; }
        
        protected Dictionary<string, int> _bonuses = new Dictionary<string, int>();
        
        protected BaseEnchantment(string name, EnchantmentType type)
        {
            Name = name;
            Type = type;
            InitializeBonuses();
        }
        
        protected abstract void InitializeBonuses();
        
        public virtual int GetBonus(string statType)
        {
            if (_bonuses.ContainsKey(statType))
                return _bonuses[statType];
            return 0;
        }
        
        public virtual void ApplyEffect(InventorySystem.Core.Player.Player player)
        {
        }
    }
}