using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Enchantment
{
    public abstract class BaseEnchantment : IEnchantment
    {
        public string Name { get; protected set; }
        public EnchantmentType Type { get; protected set; }
        
        protected Dictionary<string, float> _bonuses = new Dictionary<string, float>();
        
        protected BaseEnchantment(string name, EnchantmentType type)
        {
            Name = name;
            Type = type;
            InitializeBonuses();
        }
        
        protected abstract void InitializeBonuses();
        
        public virtual float GetBonus(string statType)
        {
            if (_bonuses.ContainsKey(statType))
                return _bonuses[statType];
            return 0f;
        }
        
        public virtual void ApplyEffect(ICharacter target)
        {
        }
    }
}