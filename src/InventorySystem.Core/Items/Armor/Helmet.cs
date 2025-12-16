using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Core.Items.Armor
{
    public class Helmet : ArmorBase
    {
        public Helmet(int id = 301, string name = "укреплённый шлем") 
            : base(
                id: id,
                name: name,
                baseDefense: 5,
                slot: EquipmentSlot.Head,
                maxDurability: 100,
                maxEnchantments: 2,
                rarity: ItemRarity.Common)
        {
        }
        
        public override IItem Clone()
        {
            return new Helmet(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                Durability = this.Durability,
                Rarity = this.Rarity
            };
        }
    }
}