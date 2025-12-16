using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Core.Items.Weapon
{
    public class Hammer : WeaponBase
    {
        public bool IsCharged { get; private set; }
        
        public Hammer(int id = 103, string name = "молот") 
            : base(
                id: id,
                name: name,
                baseDamage: 15,
                slot: EquipmentSlot.BothHands,
                maxDurability: 200,
                rarity: ItemRarity.Rare)
        {
            IsCharged = false;
        }
        
        public void Charge()
        {
            if (!IsBroken())
            {
                IsCharged = true;
            }
        }
        
        public override int CalculateDamage()
        {
            int baseDamage = base.CalculateDamage();
            
            if (IsCharged)
            {
                baseDamage *= 2;
                IsCharged = false;
            }
            
            return baseDamage;
        }
        
        public override IItem Clone()
        {
            return new Hammer(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                Durability = this.Durability,
                Rarity = this.Rarity,
                IsCharged = this.IsCharged
            };
        }
    }
}