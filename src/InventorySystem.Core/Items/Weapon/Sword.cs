using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Core.Items.Weapon
{
    public class Sword : WeaponBase
    {
        public Sword(int id = 101, string name = "меч") 
            : base(
                id: id,
                name: name,
                baseDamage: 10,
                slot: EquipmentSlot.Hand,
                maxDurability: 150,
                rarity: ItemRarity.Rare)
        {
        }
        
        public override IItem Clone()
        {
            return new Sword(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                Durability = this.Durability,
                Rarity = this.Rarity
            };
        }
    }
}