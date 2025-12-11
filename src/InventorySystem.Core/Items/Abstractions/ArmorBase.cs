using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Abstractions
{
    public abstract class ArmorBase : ItemBase, IEquippable, IEnchantable
    {
        public EquipmentSlot Slot { get; protected set; }
        public int Durability { get; set; }
        public int MaxDurability { get; protected set; }
        public int MaxEnchantments { get; protected set; }
        public IEnumerable<IEnchantment> Enchantments => _enchantments.AsReadOnly();
        
        protected List<IEnchantment> _enchantments = new List<IEnchantment>();
        
        public int BaseDefense { get; protected set; }
        
        protected ArmorBase(
            int id,
            string name,
            int baseDefense,
            EquipmentSlot slot,
            int maxDurability = 100,
            int maxEnchantments = 2,
            ItemRarity rarity = ItemRarity.Rare)
            : base(id, name, 1, rarity)
        {
            BaseDefense = baseDefense;
            Slot = slot;
            MaxDurability = maxDurability;
            Durability = maxDurability;
            MaxEnchantments = maxEnchantments;
        }
        
        public bool Equip()
        {
            return !IsBroken();
        }
        
        public void Unequip()
        {
        }
        
        public bool IsBroken()
        {
            return Durability <= 0;
        }
        
        public virtual int CalculateDefense()
        {
            float defenseMultiplier = 1.0f + GetEnchantmentBonus("Defense");
            return (int)(BaseDefense * defenseMultiplier);
        }
        
        public bool AddEnchantment(IEnchantment enchantment)
        {
            if (_enchantments.Count >= MaxEnchantments) return false;
            
            _enchantments.Add(enchantment);
            return true;
        }
        
        public bool RemoveEnchantment(IEnchantment enchantment)
        {
            return _enchantments.Remove(enchantment);
        }
        
        public bool HasEnchantment(string enchantmentType)
        {
            return _enchantments.Any(e => e.Type.ToString() == enchantmentType);
        }
        
        public float GetEnchantmentBonus(string statType)
        {
            return _enchantments.Sum(e => e.GetBonus(statType));
        }
    }
}