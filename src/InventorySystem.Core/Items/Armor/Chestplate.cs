using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Armor
{
    public class Chestplate : ArmorBase
    {
        public Chestplate(int id = 302, string name = "кожаные наплечники") 
            : base(
                id: id,
                name: name,
                baseDefense: 10,
                slot: EquipmentSlot.Chest,
                maxDurability: 150,
                maxEnchantments: 3,
                rarity: ItemRarity.Common)
        {
        }
        
        public override IItem Clone()
        {
            return new Chestplate(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                Durability = this.Durability,
                Rarity = this.Rarity
            };
        }
    }
}