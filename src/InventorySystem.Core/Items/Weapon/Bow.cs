using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Core.Items.Weapon
{
    public class Bow : WeaponBase
    {
        public int ArrowCount { get; private set; }
        public int MaxArrowCount { get; private set; }
        
        public Bow(int id = 102, string name = "лук") 
            : base(
                id: id,
                name: name,
                baseDamage: 8,
                slot: EquipmentSlot.BothHands,
                maxDurability: 120,
                rarity: ItemRarity.Rare)
        {
            MaxArrowCount = 64;
            ArrowCount = 0;
        }
        
        public bool LoadArrows(int count)
        {
            if (ArrowCount + count > MaxArrowCount || count <= 0)
                return false;
                
            ArrowCount += count;
            return true;
        }
        
        public bool Shoot()
        {
            if (ArrowCount <= 0 || IsBroken())
                return false;
                
            ArrowCount--;
            Durability -= 1; // износ
            return true;
        }
        
        public override int CalculateDamage()
        {
            int baseDamage = base.CalculateDamage();
            
            if (ArrowCount == MaxArrowCount)
            {
                baseDamage = (int)(baseDamage * 2); // *2 
            }
            
            return baseDamage;
        }
        
        public override IItem Clone()
        {
            return new Bow(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                Durability = this.Durability,
                Rarity = this.Rarity,
                ArrowCount = this.ArrowCount
            };
        }
    }
}