using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Core.Items.Abstractions
{
    public abstract class WeaponBase : ItemBase, IEquippable, IEnchantable
    {
        public InventorySystem.Core.Player.Equipment.EquipmentSlot Slot { get; protected set; }
        public int Durability { get; set; }
        public int MaxDurability { get; protected set; }
        
        public int MaxEnchantments { get; protected set; }
        public IEnumerable<IEnchantment> Enchantments => _enchantments.AsReadOnly();
        
        protected List<IEnchantment> _enchantments = new List<IEnchantment>();
        
        public int BaseDamage { get; protected set; }
        
        protected WeaponBase(
            int id, 
            string name, 
            int baseDamage,
            EquipmentSlot slot = EquipmentSlot.Hand,
            int maxDurability = 100,
            ItemRarity rarity = ItemRarity.Rare) 
            : base(id, name, 1, rarity)
        {
            BaseDamage = baseDamage;
            Slot = slot;
            MaxDurability = maxDurability;
            Durability = maxDurability;
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
        
        public virtual int CalculateDamage()
        {
            int damageMultiplier = 1 + GetEnchantmentBonus("урон");
            return (int)(BaseDamage * damageMultiplier);
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
        
        public int GetEnchantmentBonus(string statType)
        {
            return _enchantments.Sum(e => e.GetBonus(statType));
        }
    }
}